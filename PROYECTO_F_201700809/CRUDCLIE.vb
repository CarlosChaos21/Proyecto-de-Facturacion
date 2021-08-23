Imports MySql.Data.MySqlClient
Public Class CRUDCLIE

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        MenuP.Show()
    End Sub
    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT * FROM CLIENTE"
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CLIENTE")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "CLIENTE"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub CRUDCLIE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox6.Text <> "" Then
            ConB = "SELECT * FROM CLIENTE where Nombre='" & TextBox6.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CLIENTE")
            Lista = dts.Tables("CLIENTE").Rows.Count

        End If
        If Lista <> 0 Then
            TextBox1.Text = dts.Tables("CLIENTE").Rows(0).Item("Nombre")
            TextBox2.Text = dts.Tables("CLIENTE").Rows(0).Item("NIT")
            TextBox3.Text = dts.Tables("CLIENTE").Rows(0).Item("Direccion")
            If Convert.ToString((dts.Tables("CLIENTE").Rows(0).Item("Telefono"))) = String.Empty Then
                TextBox4.Text = ""
            Else
                TextBox4.Text = dts.Tables("CLIENTE").Rows(0).Item("Telefono")
            End If

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            comandos = New MySqlCommand("INSERT INTO CLIENTE(Nombre,Telefono,NIT,Direccion)" & Chr(13) &
                                        "VALUES(@Nombre,@Telefono,@NIT,@Direccion)", conexion)
            comandos.Parameters.AddWithValue("@Nombre", TextBox1.Text)
            comandos.Parameters.AddWithValue("@NIT", TextBox2.Text)
            comandos.Parameters.AddWithValue("@Direccion", TextBox3.Text)
            If TextBox4.Text = "" Then
                comandos.Parameters.AddWithValue("@Telefono", DBNull.Value)

            Else
                comandos.Parameters.AddWithValue("@Telefono", TextBox4.Text)
            End If


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
        si = MsgBox("¿Desea Elminar el cliente?", vbYesNo, "Eliminar")
        If si = 6 Then
            Eliminar = "DELETE FROM CLIENTE WHERE Nombre='" & TextBox6.Text & "'"
            comandos = New MySqlCommand(Eliminar, conexion)
            comandos.ExecuteNonQuery()
            MsgBox("Eliminado Correctamente")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
            actualizar = "alter table CLIENTE AUTO_INCREMENT=1;"
            comandos = New MySqlCommand(actualizar, conexion)
            comandos.ExecuteNonQuery()
            Call RefreshGrid()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = "" Then
            actualizar = "UPDATE CLIENTE SET Nombre='" & TextBox1.Text & "', NIT='" & TextBox2.Text & "' 
                     , Direccion='" & TextBox3.Text & "'
                     WHERE Nombre='" & TextBox6.Text & "'"
            comandos = New MySqlCommand(actualizar, conexion)
            comandos.ExecuteNonQuery()


            MsgBox("Datos Actualizados ")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
            Call RefreshGrid()

        Else
            actualizar = "UPDATE CLIENTE SET Nombre='" & TextBox1.Text & "', NIT='" & TextBox2.Text & "' 
                     , Direccion='" & TextBox3.Text & "', Telefono='" & TextBox4.Text &
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
        End If





    End Sub

End Class