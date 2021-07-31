Imports MySql.Data.MySqlClient

Public Class INVEN

    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT  INVENTARIO.IdInventario, INVENTARIO.FechaIngreso, INVENTARIO.Cantidad, PRODUCTO.Nombre as 'Producto', PROVEEDOR.Nombre as 'Proveedor'
                       from INVENTARIO inner join PRODUCTO on INVENTARIO.PRODUCTO_idProducto=Producto.idProducto
                                       inner join PROVEEDOR on INVENTARIO.PROVEEDOR_idProveedor=PROVEEDOR.idProveedor "
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "INVENTARIO")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "INVENTARIO"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub CargarProd()
        Try
            Sql = "SELECT * FROM PRODUCTO"
            adaptador = New MySqlDataAdapter(Sql, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PRODUCTO")
            ComboBox1.DataSource = dts.Tables(0)
            ComboBox1.DisplayMember = "Nombre"
            ComboBox1.ValueMember = "idProducto"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub CargarProv()
        Try
            Sql = "SELECT * FROM PROVEEDOR"
            adaptador = New MySqlDataAdapter(Sql, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PROVEEDOR")
            ComboBox2.DataSource = dts.Tables(0)
            ComboBox2.DisplayMember = "Nombre"
            ComboBox2.ValueMember = "idProveedor"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()

    End Sub

    Private Sub INVEN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
        Timer1.Enabled = True
        Timer1.Interval = 50000
        Call CargarProd()
        Call CargarProv()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            comandos = New MySqlCommand("INSERT INTO INVENTARIO(FechaIngreso,Cantidad,PRODUCTO_idPRoducto,PROVEEDOR_idProveedor)" & Chr(13) &
                                        "VALUES(@FechaIngreso,@Cantidad,@PRODUCTO_idPRoducto,@PROVEEDOR_idProveedor)", conexion)

            comandos.Parameters.AddWithValue("@FechaIngreso", DateTimePicker1.Value)
            comandos.Parameters.AddWithValue("@Cantidad", TextBox2.Text)
            comandos.Parameters.AddWithValue("@Descripcion", TextBox2.Text)
            comandos.Parameters.AddWithValue("@PRODUCTO_idPRoducto", ComboBox1.SelectedValue)
            comandos.Parameters.AddWithValue("@PROVEEDOR_idProveedor", ComboBox2.SelectedValue)
            comandos.ExecuteNonQuery()

            TextBox2.Clear()
            MsgBox("Datos correctamente guardados")
            Call RefreshGrid()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CargarProv()
        CargarProd()

    End Sub
End Class