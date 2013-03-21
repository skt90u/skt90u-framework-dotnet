<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserPoll.ascx.cs" Inherits="UserPoll" %>

<div style="padding: 5px;">

<h3 style="color:Navy;">Online Poll</h3>

<asp:Panel ID="pnlSubmitPoll" runat="server" DefaultButton="btnSubmit">
     <div>
         How often do you buy flowers online?
     </div>
     
     <asp:RadioButtonList ID="rblAnswers" runat="server">
        <asp:ListItem Text="1-5 times a year" Value="1" />
        <asp:ListItem Text="6-10 times a year" Value="2" />
        <asp:ListItem Text="11-20 times a year" Value="3" />
        <asp:ListItem Text="More than 20 times a year" Value="4" />
        <asp:ListItem Text="Never" Value="5" />
     </asp:RadioButtonList>

     <br />
     <div style="float:left">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Submit_Click" />
     </div>
</asp:Panel>

<asp:Panel ID="pnlPollReceived" runat="server" Visible="false">
 <div>
   Thank you for participating. Your feedback is important to us!
 </div>
</asp:Panel>

</div>