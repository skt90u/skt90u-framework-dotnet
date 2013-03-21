Imports System.Runtime.CompilerServices
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections.Specialized

''' <summary>Enhance GridviewRow functionality</summary>
Public Module ExtGridviewRow

    ''' <summary>
    ''' get button from GridViewRow relay on Button's CommandName
    ''' </summary>
    ''' <param name="aRow">GridviewRow</param>
    ''' <param name="sCommandName">name of Button (maybe it's same as id , not sure !!)</param>
    ''' <returns>return null if aRow is not in DataRow mode</returns>
    <Extension()> _
    Public Function GetButton(ByVal aRow As GridViewRow, ByVal sCommandName As String) As Button
        If aRow.RowType <> DataControlRowType.DataRow Then
            Return Nothing
        End If

        Dim oControl As Control = aRow.Cells(0)
        Dim oChildControl As Control
        For Each oChildControl In oControl.Controls
            If TypeOf (oChildControl) Is Button Then
                If String.Compare(CType(oChildControl, Button).CommandName, sCommandName, True) = 0 Then
                    Return oChildControl
                End If
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' set button's text from GridViewRow relay on Button's CommandName
    ''' </summary>
    <Extension()> _
    Public Sub SetButtonText(ByVal aRow As GridViewRow, ByVal sCommandName As String, ByVal txt As String)
        Dim btn As Button = GetButton(aRow, sCommandName)
        If btn IsNot Nothing Then
            btn.Text = txt
        End If
    End Sub

    ''' <summary>Extract GridViewRow's Values to an OrderedDictionary</summary>
    <Extension()> _
    Public Function ExtractValues(ByVal aRow As GridViewRow) As OrderedDictionary
        If aRow.RowType <> DataControlRowType.DataRow Then
            Return Nothing
        End If

        Dim grv As GridView = aRow.Parent.Parent
        Dim columns As DataControlFieldCollection = grv.Columns
        Dim nColumns As Integer = columns.Count
        Dim Result As OrderedDictionary = New OrderedDictionary(nColumns)

        For i As Integer = 0 To nColumns - 1
            Dim aColumn As DataControlField = columns.Item(i)

            If (TypeOf aColumn Is CommandField) Then Continue For

            Dim oDictionary As OrderedDictionary = New OrderedDictionary()
            If (TypeOf aColumn Is TemplateField) Then
                CType(aColumn, TemplateField).ExtractValuesFromCell(oDictionary, TryCast(aRow.Cells.Item(i), DataControlFieldCell), aRow.RowState, True)
            Else
                aColumn.ExtractValuesFromCell(oDictionary, TryCast(aRow.Cells.Item(i), DataControlFieldCell), aRow.RowState, True)
            End If

            For Each entry As DictionaryEntry In oDictionary
                Result.Item(entry.Key) = entry.Value
            Next
        Next
        Return Result
    End Function
End Module
