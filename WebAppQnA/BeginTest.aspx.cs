using System;
using System.Web;

namespace QuizApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void BeginExamButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                HttpCookie emailCookie = new HttpCookie("UserEmail", EmailTextBox.Text);
                emailCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(emailCookie);
                Response.Redirect("QuestionPage.aspx");
            }
        }
    }
}
