Public Class Cliente
    ' variables de propiedad
    Private msNombre, msApellidos, msDireccion, msDNI, msTelefono As String

    ' procedimientos de propiedad
    Public Property Nombre() As String
        Get
            Return msNombre
        End Get
        Set(ByVal Value As String)
            msNombre = Value
        End Set
    End Property
    Public Property Apellidos() As String
        Get
            Return msApellidos
        End Get
        Set(ByVal Value As String)
            msApellidos = Value
        End Set
    End Property
    Public Property Direccion() As String
        Get
            Return msDireccion
        End Get
        Set(ByVal Value As String)
            msDireccion = Value
        End Set
    End Property
    Public Property DNI() As String
        Get
            Return msDNI
        End Get
        Set(ByVal Value As String)
            msDNI = Value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return msTelefono
        End Get
        Set(ByVal Value As String)
            msTelefono = Value
        End Set
    End Property
End Class
Public Class Vendedor
    ' variables de propiedad
    Private miID, msNombre, msApellidos, msDireccion, msTelefono, msDNI As String
    Private mdbSueldo As Double

    ' procedimientos de propiedad
    Public Property Nombre() As String
        Get
            Return msNombre
        End Get
        Set(ByVal Value As String)
            msNombre = Value
        End Set
    End Property
    Public Property Apellidos() As String
        Get
            Return msApellidos
        End Get
        Set(ByVal Value As String)
            msApellidos = Value
        End Set
    End Property
    Public Property Direccion() As String
        Get
            Return msDireccion
        End Get
        Set(ByVal Value As String)
            msDireccion = Value
        End Set
    End Property
    Public Property DNI() As String
        Get
            Return msDNI
        End Get
        Set(ByVal Value As String)
            msDNI = Value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return msTelefono
        End Get
        Set(ByVal Value As String)
            msTelefono = Value
        End Set
    End Property
    Public Property Sueldo() As Double
        Get
            Return mdbSueldo
        End Get
        Set(ByVal Value As Double)
            mdbSueldo = Value
        End Set
    End Property
    Public Function calculaComision(ByVal tasInteres As Decimal, ByVal subTotal As Decimal) As Decimal

        Return tasInteres * subTotal

    End Function
End Class
Public Class Producto
    Private msjFraccion, vEnteras, vDecimales As Integer
    Public WriteOnly Property uEnteras() As Integer
        Set(ByVal Value As Integer)
            vEnteras = Value
        End Set
    End Property
    Public WriteOnly Property uDecimales() As Integer
        Set(ByVal Value As Integer)
            vDecimales = Value
        End Set
    End Property
    Public WriteOnly Property tipoFraccion() As String
        Set(ByVal Value As String)
            If Value = "fraccionKg" Then
                msjFraccion = vEnteras
            Else
                msjFraccion = vDecimales
            End If
        End Set
    End Property
    Public ReadOnly Property valFraccion() As Integer
        Get
            Return msjFraccion
        End Get
    End Property
    Public Function calculaInteres(ByVal tasInteres As Decimal, ByVal subTotal As Decimal) As Decimal

        Return tasInteres * subTotal

    End Function
    Public Function stringLetra(ByVal cadTipoDocumento As String, ByVal numDocumento As String, ByVal cadFecha As String, ByVal canLetras As String) As String

        Return cadTipoDocumento + numDocumento + Mid(cadFecha, 9, 2) + canLetras

    End Function
    Public Overloads Function importeLetra(ByVal precioUnitario As Decimal, ByVal cuotaInicial As Decimal, ByVal tasa As Decimal, ByVal tipoCredito As Byte) As Decimal
        Dim cuota As Decimal
        cuota = tasa * (precioUnitario - cuotaInicial)

        If tipoCredito = 1 Then
            cuota = cuota / 2
        Else
            If tipoCredito = 2 Then
                cuota = cuota / 4
            End If
        End If

        Return cuota

    End Function
    Public Overloads Function importeLetra(ByVal precioUnitario As Decimal, ByVal cuotaInicial As Decimal, ByVal tasa As Decimal, ByVal tipCambio As Decimal, ByVal tipoCredito As Byte) As Decimal
        Dim cuota As Decimal

        cuota = (tasa * (precioUnitario - cuotaInicial * tipCambio)) / tipCambio

        If tipoCredito = 1 Then
            cuota = cuota / 2
        Else
            If tipoCredito = 2 Then
                cuota = cuota / 4
            End If
        End If

        Return cuota

    End Function
End Class

