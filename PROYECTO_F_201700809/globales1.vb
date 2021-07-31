Imports MySql.Data.MySqlClient

Module globales1
    Public conexion As New MySqlConnection()
    Public comandos As New MySqlCommand()
    Public dr As MySqlDataReader()
    Public adaptador As New MySqlDataAdapter()
    Public dts As New DataSet
    Public Sql As String
    Public ConGrid As String
    Public ConB As String
    Public Lista As Byte
    Public Eliminar As String
    Public si As Byte
    Public actualizar As String

    Public Sub conectar()
        Try
            conexion.ConnectionString = "server=localhost;user=root;database=bd_201700809;password=root"
            conexion.Open()
        Catch ex As Exception

        End Try


    End Sub

    Public Sub desconectar()
        conexion.Close()

    End Sub



End Module
