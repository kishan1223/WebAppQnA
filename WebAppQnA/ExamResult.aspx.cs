using System;
using System.Configuration;
using System.Data.SqlClient;

namespace QuizApp
{
    public partial class Result : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["QuizDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowResult();
            }
        }

        private void ShowResult()
        {
            string userEmail = Request.Cookies["UserEmail"]?.Value;
            if (userEmail == null)
            {
                Response.Redirect("BeginTest.aspx");
                return;
            }

            EmailLabel.Text = "Your email: " + userEmail;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM UserAnswers ua
                    INNER JOIN Questions q ON ua.QuestionID = q.QuestionID
                    WHERE ua.Email = @Email AND ua.SelectedAnswer = q.CorrectAnswer", con);
                cmd.Parameters.AddWithValue("@Email", userEmail);
                int correctAnswers = (int)cmd.ExecuteScalar();

                ResultLabel.Text = "You answered " + correctAnswers + " questions correctly.";
            }
        }
    }
}
