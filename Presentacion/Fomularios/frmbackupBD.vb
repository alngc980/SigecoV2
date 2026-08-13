Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class frmbackupBD
    Private Sub frmbackupBD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarDatosConexion()
    End Sub

    Private Sub btnBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        If Me.cbxBaseDatos.Text = "" Then
            MessageBox.Show("Seleccione una base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.rbBak.Checked Then
            GenerarBackupBak()
        Else
            GenerarScriptSql()
        End If
    End Sub

    Private Sub CargarDatosConexion()
        Try
            Dim builder As New SqlConnectionStringBuilder(CadenaConexion)
            Me.cbxServidor.Items.Clear()
            Me.cbxServidor.Items.Add(builder.DataSource)
            Me.cbxServidor.SelectedIndex = 0

            Me.cbxBaseDatos.Items.Clear()
            Dim baseActual As String = builder.InitialCatalog
            builder.InitialCatalog = "master"

            Using cn As New SqlConnection(builder.ConnectionString)
                cn.Open()
                Using cmd As New SqlCommand("SELECT name FROM sys.databases WHERE state = 0 AND database_id > 4 ORDER BY name", cn)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Me.cbxBaseDatos.Items.Add(dr("name").ToString())
                        End While
                    End Using
                End Using
            End Using

            If Me.cbxBaseDatos.Items.Count = 0 And baseActual <> "" Then Me.cbxBaseDatos.Items.Add(baseActual)
            If baseActual <> "" And Me.cbxBaseDatos.Items.Contains(baseActual) Then
                Me.cbxBaseDatos.SelectedItem = baseActual
            ElseIf Me.cbxBaseDatos.Items.Count > 0 Then
                Me.cbxBaseDatos.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function CrearConexion(ByVal baseDatos As String) As SqlConnection
        Dim builder As New SqlConnectionStringBuilder(CadenaConexion)
        builder.InitialCatalog = baseDatos
        Return New SqlConnection(builder.ConnectionString)
    End Function

    Private Sub GenerarBackupBak()
        Dim destino As String = ObtenerRutaDestino("Backup SQL Server (*.bak)|*.bak", Me.cbxBaseDatos.Text & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".bak")
        If destino = "" Then Exit Sub

        Try
            Using cn As SqlConnection = CrearConexion("master")
                cn.Open()
                Dim sql As String = "BACKUP DATABASE " & Q(Me.cbxBaseDatos.Text) & " TO DISK = N'" & destino.Replace("'", "''") & "' WITH INIT, NAME = N'" & Me.cbxBaseDatos.Text.Replace("'", "''") & "', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
                Using cmd As New SqlCommand(sql, cn)
                    cmd.CommandTimeout = 0
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Backup .bak creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GenerarScriptSql()
        Dim destino As String = ObtenerRutaDestino("Script SQL (*.sql)|*.sql", Me.cbxBaseDatos.Text & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".sql")
        If destino = "" Then Exit Sub

        Try
            Using cn As SqlConnection = CrearConexion(Me.cbxBaseDatos.Text)
                cn.Open()
                Using sw As New StreamWriter(destino, False, Encoding.UTF8)
                    EscribirEncabezado(sw, Me.cbxBaseDatos.Text)
                    EscribirEsquemas(sw, cn)
                    EscribirTablas(sw, cn)
                    EscribirDatos(sw, cn)
                End Using
            End Using

            MessageBox.Show("Script SQL creado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ObtenerRutaDestino(ByVal filtro As String, ByVal nombreArchivo As String) As String
        Using dialogo As New SaveFileDialog()
            dialogo.Filter = filtro
            dialogo.FileName = nombreArchivo
            dialogo.Title = "Seleccione ubicación para guardar"
            dialogo.OverwritePrompt = True
            If dialogo.ShowDialog() = Windows.Forms.DialogResult.OK Then Return dialogo.FileName
        End Using

        Return ""
    End Function

    Private Sub EscribirEncabezado(ByVal sw As StreamWriter, ByVal baseDatos As String)
        sw.WriteLine("-- Script generado por SIGECO")
        sw.WriteLine("-- Base de datos: " & baseDatos)
        sw.WriteLine("-- Fecha: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
        sw.WriteLine("USE [master]")
        sw.WriteLine("GO")
        sw.WriteLine("IF DB_ID(N'" & baseDatos.Replace("'", "''") & "') IS NULL")
        sw.WriteLine("BEGIN")
        sw.WriteLine("    CREATE DATABASE " & Q(baseDatos))
        sw.WriteLine("END")
        sw.WriteLine("GO")
        sw.WriteLine("USE " & Q(baseDatos))
        sw.WriteLine("GO")
        sw.WriteLine("")
    End Sub

    Private Sub EscribirEsquemas(ByVal sw As StreamWriter, ByVal cn As SqlConnection)
        Using cmd As New SqlCommand("SELECT name FROM sys.schemas WHERE schema_id < 16384 AND name NOT IN ('dbo','guest','INFORMATION_SCHEMA','sys') ORDER BY name", cn)
            Using dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    Dim esquema As String = dr("name").ToString()
                    sw.WriteLine("IF SCHEMA_ID(N'" & esquema.Replace("'", "''") & "') IS NULL EXEC('CREATE SCHEMA " & Q(esquema) & "')")
                    sw.WriteLine("GO")
                End While
            End Using
        End Using
    End Sub

    Private Sub EscribirTablas(ByVal sw As StreamWriter, ByVal cn As SqlConnection)
        Dim tablas As DataTable = ObtenerTablas(cn)
        For Each tabla As DataRow In tablas.Rows
            Dim schemaName As String = tabla("schema_name").ToString()
            Dim tableName As String = tabla("table_name").ToString()
            Dim objectId As Integer = CInt(tabla("object_id"))
            Dim columnas As DataTable = ObtenerColumnas(cn, objectId)

            sw.WriteLine("IF OBJECT_ID(N'" & schemaName.Replace("'", "''") & "." & tableName.Replace("'", "''") & "', N'U') IS NULL")
            sw.WriteLine("BEGIN")
            sw.WriteLine("CREATE TABLE " & QT(schemaName, tableName) & " (")

            For i As Integer = 0 To columnas.Rows.Count - 1
                Dim col As DataRow = columnas.Rows(i)
                Dim linea As String = "    " & ObtenerDefinicionColumna(col)
                If i < columnas.Rows.Count - 1 Then linea &= ","
                sw.WriteLine(linea)
            Next

            sw.WriteLine(")")
            sw.WriteLine("END")
            sw.WriteLine("GO")
            EscribirPrimaryKey(sw, cn, objectId, schemaName, tableName)
            sw.WriteLine("")
        Next
    End Sub

    Private Sub EscribirPrimaryKey(ByVal sw As StreamWriter, ByVal cn As SqlConnection, ByVal objectId As Integer, ByVal schemaName As String, ByVal tableName As String)
        Dim sql As String = "SELECT kc.name constraint_name, i.type_desc, c.name column_name, ic.is_descending_key " & _
                            "FROM sys.key_constraints kc " & _
                            "JOIN sys.indexes i ON kc.parent_object_id = i.object_id AND kc.unique_index_id = i.index_id " & _
                            "JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id " & _
                            "JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id " & _
                            "WHERE kc.type = 'PK' AND kc.parent_object_id = @object_id ORDER BY ic.key_ordinal"
        Dim dt As New DataTable()
        Using da As New SqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@object_id", objectId)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim columnas As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            If i > 0 Then columnas.Append(", ")
            columnas.Append(Q(dt.Rows(i)("column_name").ToString()))
            If CBool(dt.Rows(i)("is_descending_key")) Then columnas.Append(" DESC") Else columnas.Append(" ASC")
        Next

        sw.WriteLine("IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = N'" & dt.Rows(0)("constraint_name").ToString().Replace("'", "''") & "' AND parent_object_id = OBJECT_ID(N'" & schemaName.Replace("'", "''") & "." & tableName.Replace("'", "''") & "'))")
        sw.WriteLine("ALTER TABLE " & QT(schemaName, tableName) & " ADD CONSTRAINT " & Q(dt.Rows(0)("constraint_name").ToString()) & " PRIMARY KEY " & dt.Rows(0)("type_desc").ToString().Replace("_", " ") & " (" & columnas.ToString() & ")")
        sw.WriteLine("GO")
    End Sub

    Private Sub EscribirForeignKeys(ByVal sw As StreamWriter, ByVal cn As SqlConnection)
        Dim sql As String = "SELECT fk.name fk_name, sch1.name parent_schema, tab1.name parent_table, col1.name parent_column, sch2.name ref_schema, tab2.name ref_table, col2.name ref_column " & _
                            "FROM sys.foreign_keys fk " & _
                            "JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id " & _
                            "JOIN sys.tables tab1 ON fkc.parent_object_id = tab1.object_id " & _
                            "JOIN sys.schemas sch1 ON tab1.schema_id = sch1.schema_id " & _
                            "JOIN sys.columns col1 ON fkc.parent_object_id = col1.object_id AND fkc.parent_column_id = col1.column_id " & _
                            "JOIN sys.tables tab2 ON fkc.referenced_object_id = tab2.object_id " & _
                            "JOIN sys.schemas sch2 ON tab2.schema_id = sch2.schema_id " & _
                            "JOIN sys.columns col2 ON fkc.referenced_object_id = col2.object_id AND fkc.referenced_column_id = col2.column_id " & _
                            "ORDER BY fk.name, fkc.constraint_column_id"
        Dim dt As New DataTable()
        Using da As New SqlDataAdapter(sql, cn)
            da.Fill(dt)
        End Using

        Dim i As Integer = 0
        While i < dt.Rows.Count
            Dim fkName As String = dt.Rows(i)("fk_name").ToString()
            Dim parentSchema As String = dt.Rows(i)("parent_schema").ToString()
            Dim parentTable As String = dt.Rows(i)("parent_table").ToString()
            Dim refSchema As String = dt.Rows(i)("ref_schema").ToString()
            Dim refTable As String = dt.Rows(i)("ref_table").ToString()
            Dim parentCols As New StringBuilder()
            Dim refCols As New StringBuilder()

            While i < dt.Rows.Count AndAlso dt.Rows(i)("fk_name").ToString() = fkName
                If parentCols.Length > 0 Then
                    parentCols.Append(", ")
                    refCols.Append(", ")
                End If
                parentCols.Append(Q(dt.Rows(i)("parent_column").ToString()))
                refCols.Append(Q(dt.Rows(i)("ref_column").ToString()))
                i += 1
            End While

            sw.WriteLine("IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'" & fkName.Replace("'", "''") & "')")
            sw.WriteLine("ALTER TABLE " & QT(parentSchema, parentTable) & " WITH CHECK ADD CONSTRAINT " & Q(fkName) & " FOREIGN KEY (" & parentCols.ToString() & ") REFERENCES " & QT(refSchema, refTable) & " (" & refCols.ToString() & ")")
            sw.WriteLine("GO")
        End While
    End Sub

    Private Sub EscribirObjetosProgramables(ByVal sw As StreamWriter, ByVal cn As SqlConnection)
        Dim sql As String = "SELECT o.type, sm.definition FROM sys.sql_modules sm JOIN sys.objects o ON sm.object_id = o.object_id WHERE o.is_ms_shipped = 0 AND o.type IN ('V','FN','IF','TF','P','TR') ORDER BY CASE o.type WHEN 'V' THEN 1 WHEN 'FN' THEN 2 WHEN 'IF' THEN 3 WHEN 'TF' THEN 4 WHEN 'P' THEN 5 WHEN 'TR' THEN 6 ELSE 7 END, o.name"
        Using cmd As New SqlCommand(sql, cn)
            cmd.CommandTimeout = 0
            Using dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    sw.WriteLine(dr("definition").ToString())
                    sw.WriteLine("GO")
                End While
            End Using
        End Using
    End Sub

    Private Sub EscribirDatos(ByVal sw As StreamWriter, ByVal cn As SqlConnection)
        Dim tablas As DataTable = ObtenerTablas(cn)
        For Each tabla As DataRow In tablas.Rows
            Dim schemaName As String = tabla("schema_name").ToString()
            Dim tableName As String = tabla("table_name").ToString()
            Dim objectId As Integer = CInt(tabla("object_id"))
            Dim columnas As DataTable = ObtenerColumnasInsert(cn, objectId)
            If columnas.Rows.Count = 0 Then Continue For

            Dim columnasSql As String = ObtenerListaColumnas(columnas)
            Dim tablaTieneIdentity As Boolean = TieneIdentity(columnas)
            Dim totalFilas As Integer = 0

            Using cmd As New SqlCommand("SELECT * FROM " & QT(schemaName, tableName), cn)
                cmd.CommandTimeout = 0
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If tablaTieneIdentity Then
                        sw.WriteLine("SET IDENTITY_INSERT " & QT(schemaName, tableName) & " ON")
                        sw.WriteLine("GO")
                    End If

                    While dr.Read()
                        totalFilas += 1
                        Dim valores As New StringBuilder()
                        For i As Integer = 0 To columnas.Rows.Count - 1
                            If i > 0 Then valores.Append(", ")
                            valores.Append(ValorSql(dr(columnas.Rows(i)("name").ToString())))
                        Next
                        sw.WriteLine("INSERT INTO " & QT(schemaName, tableName) & " (" & columnasSql & ") VALUES (" & valores.ToString() & ")")
                    End While

                    If totalFilas > 0 Then sw.WriteLine("GO")
                    If tablaTieneIdentity Then
                        sw.WriteLine("SET IDENTITY_INSERT " & QT(schemaName, tableName) & " OFF")
                        sw.WriteLine("GO")
                    End If
                End Using
            End Using
        Next
    End Sub

    Private Function ObtenerTablas(ByVal cn As SqlConnection) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT t.object_id, s.name schema_name, t.name table_name FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE t.is_ms_shipped = 0 ORDER BY s.name, t.name"
        Using da As New SqlDataAdapter(sql, cn)
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Private Function ObtenerColumnas(ByVal cn As SqlConnection, ByVal objectId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT c.column_id, c.name, ty.name type_name, c.max_length, c.precision, c.scale, c.is_nullable, c.is_identity, ic.seed_value, ic.increment_value, c.is_computed, cc.definition computed_definition, dc.definition default_definition " & _
                            "FROM sys.columns c JOIN sys.types ty ON c.user_type_id = ty.user_type_id " & _
                            "LEFT JOIN sys.identity_columns ic ON c.object_id = ic.object_id AND c.column_id = ic.column_id " & _
                            "LEFT JOIN sys.computed_columns cc ON c.object_id = cc.object_id AND c.column_id = cc.column_id " & _
                            "LEFT JOIN sys.default_constraints dc ON c.default_object_id = dc.object_id " & _
                            "WHERE c.object_id = @object_id ORDER BY c.column_id"
        Using da As New SqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@object_id", objectId)
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Private Function ObtenerColumnasInsert(ByVal cn As SqlConnection, ByVal objectId As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT name, is_identity FROM sys.columns WHERE object_id = @object_id AND is_computed = 0 ORDER BY column_id"
        Using da As New SqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@object_id", objectId)
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Private Function ObtenerDefinicionColumna(ByVal col As DataRow) As String
        If CBool(col("is_computed")) Then
            Return Q(col("name").ToString()) & " AS " & col("computed_definition").ToString()
        End If

        Dim tipo As String = TipoSql(col)
        Dim linea As String = Q(col("name").ToString()) & " " & tipo
        If CBool(col("is_identity")) Then linea &= " IDENTITY(" & col("seed_value").ToString() & "," & col("increment_value").ToString() & ")"
        If Not IsDBNull(col("default_definition")) Then linea &= " DEFAULT " & col("default_definition").ToString()
        If CBool(col("is_nullable")) Then linea &= " NULL" Else linea &= " NOT NULL"
        Return linea
    End Function

    Private Function TipoSql(ByVal col As DataRow) As String
        Dim tipo As String = col("type_name").ToString()
        Dim maxLength As Short = CShort(col("max_length"))

        Select Case tipo
            Case "varchar", "char", "varbinary", "binary"
                If maxLength = -1 Then Return tipo & "(MAX)"
                Return tipo & "(" & maxLength.ToString() & ")"
            Case "nvarchar", "nchar"
                If maxLength = -1 Then Return tipo & "(MAX)"
                Return tipo & "(" & (maxLength / 2).ToString() & ")"
            Case "decimal", "numeric"
                Return tipo & "(" & col("precision").ToString() & "," & col("scale").ToString() & ")"
        End Select

        Return tipo
    End Function

    Private Function ObtenerListaColumnas(ByVal columnas As DataTable) As String
        Dim sb As New StringBuilder()
        For i As Integer = 0 To columnas.Rows.Count - 1
            If i > 0 Then sb.Append(", ")
            sb.Append(Q(columnas.Rows(i)("name").ToString()))
        Next
        Return sb.ToString()
    End Function

    Private Function TieneIdentity(ByVal columnas As DataTable) As Boolean
        For Each row As DataRow In columnas.Rows
            If CBool(row("is_identity")) Then Return True
        Next
        Return False
    End Function

    Private Function ValorSql(ByVal valor As Object) As String
        If IsDBNull(valor) Then Return "NULL"
        If TypeOf valor Is String Then Return "N'" & valor.ToString().Replace("'", "''") & "'"
        If TypeOf valor Is DateTime Then Return "'" & CType(valor, DateTime).ToString("yyyyMMdd HH:mm:ss.fff") & "'"
        If TypeOf valor Is Boolean Then Return IIf(CBool(valor), "1", "0").ToString()
        If TypeOf valor Is Byte() Then Return "0x" & BitConverter.ToString(CType(valor, Byte())).Replace("-", "")
        If TypeOf valor Is Guid Then Return "'" & valor.ToString() & "'"
        Return Convert.ToString(valor, CultureInfo.InvariantCulture)
    End Function

    Private Function Q(ByVal nombre As String) As String
        Return "[" & nombre.Replace("]", "]]" ) & "]"
    End Function

    Private Function QT(ByVal esquema As String, ByVal tabla As String) As String
        Return Q(esquema) & "." & Q(tabla)
    End Function

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class