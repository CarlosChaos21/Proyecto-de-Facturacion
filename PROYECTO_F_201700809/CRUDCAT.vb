Imports MySql.Data.MySqlClient


Public Class CRUDCAT

    Public Sub RefreshGrid()
        Try
            ConGrid = "SELECT * FROM CATEGORIA"
            adaptador = New MySqlDataAdapter(ConGrid, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CATEGORIA")
            DataGridView2.DataSource = dts
            DataGridView2.DataMember = "CATEGORIA"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            comandos = New MySqlCommand("INSERT INTO CATEGORIA(Nombre)" & Chr(13) &
                                        "VALUES(@Nombre)", conexion)
            comandos.Parameters.AddWithValue("@Nombre", TextBox1.Text)

            comandos.ExecuteNonQuery()
            TextBox1.Clear()
            TextBox2.Clear()

            MsgBox("Datos correctamente guardados")
            Call RefreshGrid()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CRUDCAT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call RefreshGrid()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        desconectar()
        conectar()
        Me.Hide()
        MenuP.Show()

        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text <> "" Then
            ConB = "SELECT * FROM CATEGORIA where Nombre='" & TextBox2.Text & "'"
            adaptador = New MySqlDataAdapter(ConB, conexion)
            dts = New DataSet
            adaptador.Fill(dts, "CATEGORIA")
            Lista = dts.Tables("CATEGORIA").Rows.Count

        End If
        If Lista <> 0 Then
            TextBox1.Text = dts.Tables("CATEGORIA").Rows(0).Item("Nombre")

        Else
            MsgBox("Nombre del producto no encontrado, verifique el nombre del producto.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        si = MsgBox("¿Desea Elminar el producto?", vbYesNo, "Eliminar")
        If si = 6 Then
            Eliminar = "DELETE FROM CATEGORIA WHERE Nombre='" & TextBox2.Text & "'"
            comandos = New MySqlCommand(Eliminar, conexion)
            comandos.ExecuteNonQuery()

            MsgBox("Eliminado Correctamente")
            TextBox1.Clear()
            TextBox2.Clear()
            actualizar = "alter table CATEGORIA AUTO_INCREMENT=1;"
            comandos = New MySqlCommand(actualizar, conexion)
            comandos.ExecuteNonQuery()
            Call RefreshGrid()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        actualizar = "UPDATE CATEGORIA SET Nombre='" & TextBox1.Text & "'WHERE Nombre='" & TextBox2.Text & "'"
        comandos = New MySqlCommand(actualizar, conexion)
        comandos.ExecuteNonQuery()
        MsgBox("Datos Actualizados ")
        TextBox1.Clear()
        TextBox2.Clear()

        Call RefreshGrid()
    End Sub
End Class