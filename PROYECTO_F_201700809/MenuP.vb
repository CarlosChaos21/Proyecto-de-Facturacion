Imports MySql.Data.MySqlClient
Public Class MenuP

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        INVEN.Show()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        VENTA.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        desconectar()
        conectar()
        Me.Hide()
        CRUDPROD.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        CRUDCAT.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        CRUDPROV.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        CRUDCLIE.Show()
    End Sub

    Private Sub MenuP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class