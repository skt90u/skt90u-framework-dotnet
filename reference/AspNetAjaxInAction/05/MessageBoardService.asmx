<%@ WebService Language="C#" Class="AspNetAjaxInAction.MessageBoardService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Configuration;
using System.Collections.Generic;

namespace AspNetAjaxInAction
{

  [WebService(Namespace = "http://aspnetajaxinaction.com/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [ScriptService]        
  public class MessageBoardService : System.Web.Services.WebService
  {
    public MessageBoardService()
    {
    }

    #region Public Methods

    [WebMethod]
    public void PostMessage(string subject, string text)
    {
      WriteMessageToDatabase(subject, text, DateTime.Now);
    }

    [WebMethod]
    public Message[] GetMessages(int numMessages, int messageIDAfter)
    {
      return GetMessagesFromDatabase(numMessages, messageIDAfter);
    }

    [WebMethod]
    public string GetMessageText(int messageID)
    {
      return GetMessageTextFromDatabase(messageID);
    }
    
    #endregion

    #region Private Helper Routines

    private static string GetMessageTextFromDatabase(int messageID)
    {
      using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MessageDatabase"].ConnectionString))
      using (SqlCommand cmd = new SqlCommand())
      {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT Text FROM Messages WHERE MessageId = @messageId";
        cmd.Parameters.AddWithValue("@messageId", messageID);
        return (string)cmd.ExecuteScalar();
      }
    }
    
    private static void WriteMessageToDatabase(string subject, string text, DateTime datePosted)
    { 
      using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MessageDatabase"].ConnectionString))
      using (SqlCommand cmd = new SqlCommand())
      {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText =
          @"BEGIN 
          INSERT INTO Messages (Subject, Text, PostedBy, PostedDate) VALUES (@subject, @text, @postedBy, @postedDate)
          SELECT @@IDENTITY
          END                    
          ";

        cmd.Parameters.AddWithValue("@subject", subject);
        cmd.Parameters.AddWithValue("@text", text);
        MembershipUser user = Membership.GetUser();

        //If it is not a valid user Id assume that it is anonymous Id
        object userId = user == null ? new Guid(HttpContext.Current.Request.AnonymousID) : user.ProviderUserKey;

        cmd.Parameters.AddWithValue("@postedBy", userId);
        cmd.Parameters.AddWithValue("@postedDate", DateTime.Now);
        object id = cmd.ExecuteScalar();
        System.Threading.Thread.Sleep(1000);
       }            
    }
    
    private static Message[] GetMessagesFromDatabase(int numMessages, int messageIDAfter)
    {
      using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MessageDatabase"].ConnectionString))
      using (SqlCommand cmd = new SqlCommand())
      {
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText =
          @"SELECT ISNULL(aspnet_Users.UserName, '(Anonymous)') AS UserName, Messages.Subject, Messages.MessageId, 
          Messages.Text, Messages.PostedDate 
          FROM Messages LEFT OUTER JOIN aspnet_Users ON 
          Messages.PostedBy = aspnet_Users.UserId 
          WHERE MessageId > @messageId
          ORDER BY MessageId DESC";

        cmd.Parameters.AddWithValue("@messageId", messageIDAfter);
        SqlDataReader reader = cmd.ExecuteReader();
        List<Message> messages = new List<Message>();

        int subjectIndex = reader.GetOrdinal("Subject");
        int userNameIndex = reader.GetOrdinal("UserName");
        int messageIdIndex = reader.GetOrdinal("MessageId");
        int textIndex = reader.GetOrdinal("Text");
        int postedDateIndex = reader.GetOrdinal("PostedDate");
            
        for (int i = 0; i < numMessages && reader.Read(); i++)
        {
           Message m = new Message();
           m.Subject = reader.GetString(subjectIndex);
           m.Text = reader.GetString(textIndex);
           m.MessageID = reader.GetInt32(messageIdIndex);
           m.DatePosted = reader.GetDateTime(postedDateIndex);
           m.PostedBy = reader.GetString(userNameIndex);

           messages.Add(m);
        }

        return messages.ToArray();
      }
    }
    
    #endregion

  }
}