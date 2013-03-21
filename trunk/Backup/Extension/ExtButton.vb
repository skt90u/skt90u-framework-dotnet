Imports System.Web.UI.WebControls
Imports System.Runtime.CompilerServices
Imports System.Reflection

''' <summary>Enhance button functionality</summary>
Public Module ExtButton

    ''' <summary>
    ''' raise server side click event on button even if we don't know 
    ''' click handler's name
    ''' </summary>
    ''' <remarks>
    ''' sometime, we want to fire a server side click event manually. 
    ''' but we have no ideal what is the click handler's name of callee button.
    ''' due to this requirement, I create this function can raise click event on
    ''' button even if we don't know the handler's name
    ''' </remarks>
    <Extension()> _
    Public Sub RaiseClick(ByVal btn As Button, Optional ByVal e As EventArgs = Nothing)
        Dim args() As Object = New Object() {e}
        Dim t As Type = btn.GetType()
        Dim mi As MethodInfo = t.GetMethod("OnClick", BindingFlags.NonPublic Or BindingFlags.Instance)
        mi.Invoke(btn, args)
    End Sub
End Module
