Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraGrid.Columns

Namespace WindowsApplication767
	Partial Public Class Form1
		Inherits Form
				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), DateTime.Now.AddDays(i) })
			Next i
			Return tbl
				End Function


		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
		End Sub

		Public Function GetError(ByVal value As Object, ByVal rowHandle As Integer, ByVal column As GridColumn) As String
			If value Is Nothing Then
				Return "value is null"
			End If
			If Not(TypeOf value Is Integer) Then
				Return String.Empty
			End If

			If CInt(Fix(value)) Mod 2 = 0 Then
				Return "value is even"
			End If
			If CInt(Fix(value)) < 10 Then
				Return "value < 10"
			End If
			Return String.Empty
		End Function

		Private Sub SetError(ByVal cellInfo As BaseEditViewInfo, ByVal errorIconText As String)
			If errorIconText = String.Empty Then
				Return
			End If
			cellInfo.ErrorIconText = errorIconText
			cellInfo.ShowErrorIcon = True
			cellInfo.FillBackground = True
			cellInfo.ErrorIcon = DXErrorProvider.GetErrorIconInternal(ErrorType.Critical)
		End Sub


		Private Sub gridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gridView1.CustomDrawCell
			Dim info As BaseEditViewInfo = (CType(e.Cell, GridCellInfo)).ViewInfo
			Dim [error] As String = GetError(e.CellValue, e.RowHandle, e.Column)
			SetError(info, [error])
			info.CalcViewInfo(e.Graphics)
		End Sub


		Private Sub gridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles gridView1.CustomUnboundColumnData
			If e.IsGetData Then
				e.Value = e.RowHandle
			End If
		End Sub

		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.ShownEditor
			Dim [error] As String = GetError(gridView1.GetFocusedValue(), gridView1.FocusedRowHandle, gridView1.FocusedColumn)
			If [error] = String.Empty Then
				Return
			End If
			gridView1.SetColumnError(gridView1.FocusedColumn, [error])
		End Sub
	End Class

End Namespace