Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Form1
    Dim MiStrindDeConexion As String
    Dim MiConexion As SqlConnection
    Dim MiDataSet As DataSet
    Dim MiSqlComand As SqlCommand
    Dim MiSqlAdapter As SqlDataAdapter
    Dim MiDataTable As DataTable
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataGridView1.Columns.Add("0", "Legajo")
        Me.DataGridView1.Columns.Add("1", "Nombre")
        Me.DataGridView1.Columns.Add("2", "Apellido")
    End Sub

    Sub ConsultarGrilla()
        MiStrindDeConexion = "Data source=AGUSTIN\SQLEXPRESS;Initial Catalog=Alumnos;Integrated Security = True"
        Dim MiConsulta As String = ("Select * from DATOS")
        MiConexion = New SqlConnection(MiStrindDeConexion)
        MiSqlComand = New SqlCommand(MiConsulta)
        MiSqlComand.Connection = MiConexion
        MiConexion.Open()

        MiDataSet = New DataSet
        MiDataTable = New DataTable
        MiSqlAdapter = New SqlDataAdapter

        MiSqlAdapter.SelectCommand = MiSqlComand
        MiSqlAdapter.Fill(MiDataSet)

        MiDataTable = MiDataSet.Tables(0)

        MiConexion.Close()

        Me.DataGridView1.Rows.Clear()
        Dim fila As Integer = 0

        For Each miFila As DataRow In MiDataTable.Rows
            Me.DataGridView1.Rows.Add()

            Me.DataGridView1.Rows(fila).Cells(0).Value = miFila.Item(0)
            Me.DataGridView1.Rows(fila).Cells(1).Value = miFila.Item(1)
            Me.DataGridView1.Rows(fila).Cells(2).Value = miFila.Item(2)
            fila += 1
        Next
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ConsultarGrilla()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MiStrindDeConexion = "Data source=AGUSTIN\SQLEXPRESS;Initial Catalog=Alumnos;Integrated Security = True"

        Dim MiConsulta As String = ("INSERT INTO DATOS VALUES('" & TextBox1.Text & "','" & TextBox2.Text & " ','" & TextBox3.Text & " ')")
        MiConexion = New SqlConnection(MiStrindDeConexion)
        MiSqlComand = New SqlCommand(MiConsulta)
        MiSqlComand.Connection = MiConexion

        MiConexion.Open()
        MiSqlComand.ExecuteNonQuery()
        MiConexion.Close()

        MsgBox("El estudiante ah sido de alta")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ConsultarGrilla()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MiStrindDeConexion = "Data source=AGUSTIN\SQLEXPRESS;Initial Catalog=Alumnos;Integrated Security = True"

        Dim MiConsulta As String = ("DELETE FROM DATOS WHERE IDalumno= '" & TextBox1.Text & "'")
        MiConexion = New SqlConnection(MiStrindDeConexion)
        MiSqlComand = New SqlCommand(MiConsulta)
        MiSqlComand.Connection = MiConexion

        MiConexion.Open()
        MiSqlComand.ExecuteNonQuery()
        MiConexion.Close()

        MsgBox("El estudiante ah sido de baja")
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ConsultarGrilla()
    End Sub

    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        TextBox1.Text = DataGridView1.SelectedRows(0).Cells(0).Value()
        TextBox2.Text = DataGridView1.SelectedRows(0).Cells(1).Value()
        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(2).Value()
    End Sub
End Class
