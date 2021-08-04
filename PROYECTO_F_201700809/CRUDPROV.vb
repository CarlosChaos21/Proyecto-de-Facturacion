Imports MySql.Data.MySqlClient
Public Class CRUDPROV

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()
    End Sub

    Public Sub RefreshGrid()
        'Method for update Datagrid View with windows form'
        Try
            ConGrid = "SELECT * FROM PROVEEDOR"
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PROVEEDOR")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "PROVEEDOR"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub CRUDPROV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox6.Text <> "" Then
            ConB = "SELECT * FROM PROVEEDOR where Nombre='" & TextBox6.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "PROVEEDOR")
            Lista = dts.Tables("PROVEEDOR").Rows.Count

        End If
        If Lista <> 0 Then
            TextBox1.Text = dts.Tables("PROVEEDOR").Rows(0).Item("Nombre")
            TextBox2.Text = dts.Tables("PROVEEDOR").Rows(0).Item("NITProveedor")
            TextBox3.Text = dts.Tables("PROVEEDOR").Rows(0).Item("direccion")
            TextBox4.Text = dts.Tables("PROVEEDOR").Rows(0).Item("Telefono")

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            comandos = New MySqlCommand("INSERT INTO PROVEEDOR(Nombre,Telefono,NITProveedor,direccion)" & Chr(13) &
                                        "VALUES(@Nombre,@Telefono,@NITProveedor,@direccion)", conexion)
            comandos.Parameters.AddWithValue("@Nombre", TextBox1.Text)
            comandos.Parameters.AddWithValue("@NITProveedor", TextBox2.Text)
            comandos.Parameters.AddWithValue("@direccion", TextBox3.Text)
            comandos.Parameters.AddWithValue("@Telefono", TextBox4.Text)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        si = MsgBox("¿Desea Elminar el proveedor?", vbYesNo, "Eliminar")
        If si = 6 Then
            Eliminar = "DELETE FROM PROVEEDOR WHERE Nombre='" & TextBox6.Text & "'"
            comandos = New MySqlCommand(Eliminar, conexion)
            comandos.ExecuteNonQuery()
            MsgBox("Eliminado Correctamente")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
            'Method for alter table and update checkpointer with id in the table auto_incremente)'
            actualizar = "alter table proveedor AUTO_INCREMENT=1;"
            comandos = New MySqlCommand(actualizar, conexion)
            comandos.ExecuteNonQuery()
            Call RefreshGrid()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        actualizar = "UPDATE PROVEEDOR SET Nombre='" & TextBox1.Text & "', NITProveedor='" & TextBox2.Text & "' 
                     , direccion='" & TextBox3.Text & "', Telefono='" & TextBox4.Text &
                     "'WHERE Nombre='" & TextBox6.Text & "'"
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
End Class