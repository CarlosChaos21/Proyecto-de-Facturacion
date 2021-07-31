Imports MySql.Data.MySqlClient

Public Class FACTURA



    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT   CLIENTE.Nombre, CLIENTE.Direccion, CLIENTE.NIT, CLIENTE.Telefono, DetalleFactura.DetalleFactura ,FACTURA.Total
                       from ((FACTURA 
                            inner join CLIENTE on FACTURA.CLIENTE_idCliente=Cliente.idCliente)
                            inner join DetalleFactura on FACTURA.DetalleFactura_id=DetalleFactura.id);

                                       "
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "FACTURA")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "FACTURA"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub FACTURA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
        Timer1.Enabled = True
        Timer1.Interval = 10000

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RefreshGrid()
    End Sub
End Class