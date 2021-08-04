Imports MySql.Data.MySqlClient

Public Class VENTA
    Dim suma As Double
    Dim linea As DataGridViewRow
    Dim Total As Double
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()
    End Sub

    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT   Producto.Nombre, DETALLEFACTURA.Cantidad, Producto.Precio, DETALLEFACTURA.DetalleFactura, Producto.Precio * DETALLEFACTURA.Cantidad As 'SUBTOTAL'
                       from DETALLEFACTURA inner join PRODUCTO on DETALLEFACTURA.PRODUCTO_IdProducto=Producto.idProducto
                                       "
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "INVENTARIO")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "INVENTARIO"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub CargarCLie()
        Try
            Sql = "SELECT * FROM CLIENTE"
            adaptador = New MySqlDataAdapter(Sql, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CLIENTE")
            ComboBox1.DataSource = dts.Tables(0)
            ComboBox1.DisplayMember = "Nombre"
            ComboBox1.ValueMember = "idCliente"

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
            ComboBox2.DataSource = dts.Tables(0)
            ComboBox2.DisplayMember = "Nombre"
            ComboBox2.ValueMember = "idProducto"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        FACTURA.Show()
        si = MsgBox("¿Desea Generar su factura?", vbYesNo, " ")
        If si = 6 Then
            Try

                comandos = New MySqlCommand("INSERT INTO FACTURA(Total,CLIENTE_idCliente)" & Chr(13) &
                                            "VALUES(@Total,@CLIENTE_idCliente)", conexion)

                comandos.Parameters.AddWithValue("@Total", TextBox3.Text)
                comandos.Parameters.AddWithValue("@CLIENTE_idCliente", ComboBox1.SelectedValue)
                comandos.ExecuteNonQuery()

                MsgBox("Factura generada correctamente")


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub
    Public Sub SubTotal()
        suma = Val(TextBox4.Text) * Val(TextBox2.Text)
        TextBox6.Text = Val(suma)

    End Sub
    Public Sub TotalCash()
        DataGridView1.Rows.Add(TextBox6.Text)
        For Each linea In DataGridView1.Rows
            Total = Total + linea.Cells(0).Value

        Next
        TextBox3.Text = FormatNumber(Total, 2)

    End Sub
    Private Sub REAP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
        Call CargarCLie()
        Call CargarProd()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            comandos = New MySqlCommand("INSERT INTO DETALLEFACTURA(DetalleFactura,FechaFactura,Cantidad, PRODUCTO_idProducto)" & Chr(13) &
                                        "VALUES(@DetalleFactura,@FechaFactura,@Cantidad,@PRODUCTO_idProducto)", conexion)

            comandos.Parameters.AddWithValue("@DetalleFactura", TextBox5.Text)
            comandos.Parameters.AddWithValue("@FechaFactura", DateTimePicker1.Value)
            comandos.Parameters.AddWithValue("@Cantidad", TextBox4.Text)
            comandos.Parameters.AddWithValue("@PRODUCTO_idPRoducto", ComboBox2.SelectedValue)
            comandos.ExecuteNonQuery()
            Call SubTotal()
            Call TotalCash()
            MsgBox("Datos correctamente guardados")
            Call RefreshGrid()
            TextBox4.Clear()
            TextBox5.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ComboBox2.Text <> "" Then
            ConB = "SELECT PRODUCTO.Precio  FROM PRODUCTO where Nombre='" & ComboBox2.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PRODUCTO")
            Lista = dts.Tables("PRODUCTO").Rows.Count

        End If
        If Lista <> 0 Then

            TextBox2.Text = dts.Tables("PRODUCTO").Rows(0).Item("Precio")

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If

    End Sub


    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox2.Text <> "" Then
            ConB = "SELECT PRODUCTO.Precio  FROM PRODUCTO where Nombre='" & ComboBox2.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PRODUCTO")
            Lista = dts.Tables("PRODUCTO").Rows.Count

        End If
        If Lista <> 0 Then

            TextBox2.Text = dts.Tables("PRODUCTO").Rows(0).Item("Precio")

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class