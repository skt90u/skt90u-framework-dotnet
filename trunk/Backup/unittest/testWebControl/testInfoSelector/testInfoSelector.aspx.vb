Imports JFramework
Imports JFramework.WebControl

Partial Class testInfoSelector_testInfoSelector
    Inherits System.Web.UI.Page

    Private Sub DataBindSelector(ByVal SelectSQL As String, ByVal lb As Label, ByVal selector As InfoSelector)
        Dim initRecords As List(Of OrderedDictionary)
        Try
            selector.SelectSQL = SelectSQL
            initRecords = WebControlUtil.Deserialize(Of List(Of OrderedDictionary))(lb.Text)
            selector.Show(initRecords)
            If initRecords IsNot Nothing Then initRecords = Nothing
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lb01.Text = "[{""F1"":""Beverages"" ,""F2"":""100"" ,""F3"":""2011/12/31""} ,{""F1"":""Dairy Products"" ,""F2"":""200"" ,""F3"":""2099/01/01""}]"
            'lb02.Text = "[{""F1"":""Beverages"" ,""F2"":""100"" ,""F3"":""2021/12/31""} ,{""F1"":""Dairy Products"" ,""F2"":""200"" ,""F3"":""2099/02/02""}]"
        End If
    End Sub

    Protected Sub btn01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn01.Click
        Try
            Dim SelectSQL As String = "select CategoryName as F1, '' as F2, '' as F3  from Categories"
            DataBindSelector(SelectSQL, lb01, InfoSelector01)
        Catch ex As Exception
            Dim o As Object = Nothing
        End Try
    End Sub

    Protected Sub InfoSelector01_Submit(ByVal lst As List(Of OrderedDictionary)) Handles InfoSelector01.Submit
        lb01.Text = WebControlUtil.Serialize(lst)
    End Sub

    'Protected Sub btn02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn02.Click
    '    Try
    '        Dim SelectSQL As String = "select CategoryName as F1, '' as F2, '' as F3  from Categories"
    '        DataBindSelector(SelectSQL, lb02, InfoSelector02)
    '    Catch ex As Exception
    '        Dim o As Object = Nothing
    '    End Try
    'End Sub

    'Protected Sub InfoSelector02_Submit(ByVal lst As List(Of OrderedDictionary)) Handles InfoSelector02.Submit
    '    lb02.Text = WebControlUtil.Serialize(lst)
    'End Sub
End Class
