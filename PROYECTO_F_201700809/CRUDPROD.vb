Imports MySql.Data.MySqlClient


Public Class CRUDPROD


    Public Sub CargarCatego()
        Try
            Sql = "SELECT * FROM CATEGORIA"
            adaptador = New MySqlDataAdapter(Sql, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CATEGORIA")
            ComboBox1.DataSource = dts.Tables(0)
            ComboBox1.DisplayMember = "Nombre"
            ComboBox1.ValueMember = "idCategoria"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT PRODUCTO.IdProducto, PRODUCTO.Nombre, PRODUCTO.Marca, PRODUCTO.Descripcion, PRODUCTO.Precio, CATEGORIA.Nombre as 'Categoria'
                        from PRODUCTO inner join CATEGORIA on PRODUCTO.CATEGORIA_idCategoria=CATEGORIA.idCategoria"
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PRODUCTO")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "PRODUCTO"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub CRUDPROD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call RefreshGrid()
        Timer1.Enabled = True
        Timer1.Interval = 10000
        CargarCatego()





    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            comandos = New MySqlCommand("INSERT INTO PRODUCTO(Nombre,Marca,Descripcion,Precio,CATEGORIA_idCATEGORIA)" & Chr(13) &
                                        "VALUES(@Nombre,@Marca,@Descripcion,@Precio,@CATEGORIA_idCATEGORIA)", conexion)
            comandos.Parameters.AddWithValue("@Nombre", TextBox1.Text)
            comandos.Parameters.AddWithValue("@Marca", TextBox2.Text)
            comandos.Parameters.AddWithValue("@Descripcion", TextBox3.Text)
            comandos.Parameters.AddWithValue("@Precio", TextBox4.Text)
            comandos.Parameters.AddWithValue("@CATEGORIA_idCategoria", ComboBox1.SelectedValue)
            comandos.ExecuteNonQuery()
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
            MsgBox("Datos correctamente guardados")
            Call RefreshGrid()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox6.Text <> "" Then
            ConB = "SELECT * FROM PRODUCTO where Nombre='" & TextBox6.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PRODUCTO")
            Lista = dts.Tables("PRODUCTO").Rows.Count

        End If
        If Lista <> 0 Then
            TextBox1.Text = dts.Tables("PRODUCTO").Rows(0).Item("Nombre")
            TextBox2.Text = dts.Tables("PRODUCTO").Rows(0).Item("Marca")
            TextBox3.Text = dts.Tables("PRODUCTO").Rows(0).Item("Descripcion")
            TextBox4.Text = dts.Tables("PRODUCTO").Rows(0).Item("Precio")
            ComboBox1.Text = dts.Tables("PRODUCTO").Rows(0).Item("CATEGORIA_idCategoria")

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        si = MsgBox("¿Desea Elminar el producto?", vbYesNo, "Eliminar")
        If si = 6 Then
            Eliminar = "DELETE FROM PRODUCTO WHERE Nombre='" & TextBox6.Text & "'"
            comandos = New MySqlCommand(Eliminar, conexion)
            comandos.ExecuteNonQuery()
            MsgBox("Eliminado Correctamente")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
            actualizar = "alter table PRODUCTO AUTO_INCREMENT=1;"
            comandos = New MySqlCommand(actualizar, conexion)
            comandos.ExecuteNonQuery()

            Call RefreshGrid()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox6.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        actualizar = "UPDATE PRODUCTO SET Nombre='" & TextBox1.Text & "', Marca='" & TextBox2.Text & "' 
                     , Descripcion='" & TextBox3.Text & "', Precio='" & TextBox4.Text & "'
                     , CATEGORIA_idCategoria='" & ComboBox1.SelectedValue & "'WHERE Nombre='" & TextBox6.Text & "'"
        comandos = New MySqlCommand(actualizar, conexion)
        comandos.ExecuteNonQuery()
        MsgBox("Datos Actualizados ")
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox6.Clear()
        Call RefreshGrid()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CargarCatego()

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class