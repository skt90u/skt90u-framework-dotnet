Imports System.Runtime.CompilerServices
Imports System.Web.UI

''' <summary>Enhance control functionality</summary>
Public Module ExtControl

    ''' <summary>
    ''' find the first id-matched and type-matched control from startingControl
    ''' </summary>
    ''' <returns>
    ''' return null if can't be found
    ''' </returns>
    ''' <remarks>
    ''' we must declare T as Control, because we want to use 'found.ID' statement
    ''' </remarks>
    <Extension()> _
    Public Function XFindControl(Of T As Control)(ByVal startingControl As Control, ByVal id As String) As T
        Dim found As T = Nothing
        Dim controlCount As Integer = startingControl.Controls.Count
        If controlCount > 0 Then
            For i As Integer = 0 To controlCount - 1
                Dim activeControl As Control = startingControl.Controls(i)
                If TypeOf activeControl Is T Then
                    found = TryCast(startingControl.Controls(i), T)
                    If String.Compare(id, found.ID, True) = 0 Then
                        Exit For
                    Else
                        found = Nothing
                    End If
                Else
                    found = XFindControl(Of T)(activeControl, id)
                    If found IsNot Nothing Then
                        Exit For
                    End If
                End If
            Next
        End If
        Return found
    End Function

    ''' <summary>
    ''' find the first id-matched control from startingControl
    ''' </summary>
    ''' <returns>
    ''' return null if can't be found
    ''' </returns>
    <Extension()> _
    Public Function XFindControl(ByVal container As Control, ByVal id As String) As Control
        Dim ctrl As Control = container.FindControl(id)
        If ctrl Is Nothing Then
            For i As Integer = 0 To container.Controls.Count - 1
                ctrl = XFindControl(container.Controls(i), id)
                If ctrl IsNot Nothing Then
                    Exit For
                End If
            Next
        End If
        Return ctrl
    End Function
End Module
