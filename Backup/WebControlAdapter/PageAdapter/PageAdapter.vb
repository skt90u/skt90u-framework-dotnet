Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Text
Imports JClientScriptManager = JFramework.WebControl.ClientScriptManager
Imports System.Reflection

#Region "rc.PageAdapter"
<Assembly: WebResource("JFramework.WebControlAdapter.PageAdapter.css", "text/css")> 
#End Region
#Region "rc.blockUI"
<Assembly: WebResource("JFramework.WebControlAdapter.jquery-1.4.4.js", "text/javascript")> 
<Assembly: WebResource("JFramework.WebControlAdapter.jquery.blockUI.js", "text/javascript")> 
<Assembly: WebResource("JFramework.WebControlAdapter.jquery.blockUI.css", "text/css", PerformSubstitution:=True)> 
<Assembly: WebResource("JFramework.WebControlAdapter.indicator.gif", "image/gif")> 
#End Region
#Region "rc.lightbox"
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.lightbox.css", "text/css", PerformSubstitution:=True)> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.prototype.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.effects.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.builder.js", "text/javascript")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.lightbox.js", "text/javascript", PerformSubstitution:=True)> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.loading.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.closelabel.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.nextlabel.gif", "image/gif")> 
<Assembly: System.Web.UI.WebResource("JFramework.WebControlAdapter.prevlabel.gif", "image/gif")> 
#End Region

Public Class PageAdapter
    Inherits System.Web.UI.Adapters.PageAdapter

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
    End Sub
    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        MyBase.OnPreRender(e)

        Dim type As Type = Me.GetType()
        ' CSS
        JClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControlAdapter.PageAdapter.css")
        RegisterFeatures()
    End Sub

    Private _featureFlags As New Specialized.BitVector32(0)
    Private initfeatureFlags As Boolean = False
    Private ReadOnly Property FeatureFlags() As Specialized.BitVector32
        Get
            ' <form id="form1" runat="server" FeatureFlags="BlockUI, LightBox">
            If Not initfeatureFlags Then
                Dim Flags As String = CType(Me.Control, Page).Form.Attributes("FeatureFlags")
                Flags = IIf(String.IsNullOrEmpty(Flags), String.Empty, Flags)
                Dim separator() As Char = {","}
                For Each value As String In Flags.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    value = value.Trim()
                    '[How to Get Enum Values with Reflection in C#]
                    ' http://geekswithblogs.net/shahed/archive/2006/12/06/100427.aspx
                    Dim nFeatures = System.Enum.GetNames(GetType(eFeature))
                    Dim vFeatures = System.Enum.GetValues(GetType(eFeature))
                    For i As Integer = 0 To nFeatures.Length - 1
                        If value = nFeatures(i) Then
                            _featureFlags.Item(CType(vFeatures(i), Integer)) = True
                        End If
                    Next
                Next
                initfeatureFlags = True
            End If
            Return _featureFlags
        End Get
    End Property

    Public Delegate Sub FeatureRegister(ByVal instance As JFramework.WebControlAdapter.PageAdapter)

    Class FeatureInfo
        Public Sub New(ByVal feature As eFeature, ByVal register As FeatureRegister)
            Me.feature = feature
            Me.register = register
        End Sub
        Public feature As eFeature = Nothing
        Public register As FeatureRegister = Nothing
    End Class

    Private Sub RegisterFeatures()
        For Each fi As FeatureInfo In arrFeatureInfo
            If FeatureFlags(fi.feature) Then
                fi.register(Me)
            End If
        Next
    End Sub

    Enum eFeature As Integer
        BlockUI = &H1
        LightBox = &H2
    End Enum

    Private Shared arrFeatureInfo() As FeatureInfo = New FeatureInfo() { _
            New FeatureInfo(eFeature.BlockUI, AddressOf JFramework.WebControlAdapter.PageAdapter.RegisterBlockUI), _
            New FeatureInfo(eFeature.BlockUI, AddressOf JFramework.WebControlAdapter.PageAdapter.RegisterLightBox) _
        }

    Private Shared Sub RegisterBlockUI(ByVal instance As JFramework.WebControlAdapter.PageAdapter)
        Dim type As Type = instance.GetType()
        ' CSS
        JClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControlAdapter.jquery.blockUI.css")
        ' JQuery
        JClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControlAdapter.jquery-1.4.4.js")
        ' JQuery.BlockUI
        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControlAdapter.jquery.blockUI.js")
        JClientScriptManager.RegisterCompositeScript(lstWebResource, "JFramework.WebControlAdapter")

        '頁面執行Submit時, 顯示blockUI
        JClientScriptManager.RegisterClientSubmitScript(instance.Control, "jQuery.blockUI();")
        '頁面載入時, 隱藏blockUI
        JClientScriptManager.RegisterClientApplicationLoadScript(instance.Control, "jQuery.unblockUI();")
    End Sub

    Private Shared Sub RegisterLightBox(ByVal instance As JFramework.WebControlAdapter.PageAdapter)
        Dim type As Type = instance.GetType()
        JClientScriptManager.RegisterEmbeddedCSS(type, "JFramework.WebControlAdapter.lightbox.css")
        JClientScriptManager.RegisterEmbeddedJs(type, "JFramework.WebControlAdapter.prototype.js")

        Dim lstWebResource As New List(Of String)
        lstWebResource.Add("JFramework.WebControlAdapter.effects.js")
        lstWebResource.Add("JFramework.WebControlAdapter.builder.js")
        lstWebResource.Add("JFramework.WebControlAdapter.lightbox.js")
        JClientScriptManager.RegisterCompositeScript(lstWebResource, "JFramework.WebControlAdapter")
    End Sub

End Class
