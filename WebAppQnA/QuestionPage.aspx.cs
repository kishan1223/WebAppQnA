using System;
using System.Configuration; 
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace QuizApp
{
    public partial class Quiz : System.Web.UI.Page
    {
        private static int currentQuestionIndex = 0;
        private static int totalQuestions = 0;
        private static string connectionString = ConfigurationManager.ConnectionStrings["QuizDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearUserAnswers();
                LoadQuestion();
            }
        }

        private void ClearUserAnswers()
        {
            string userEmail = Request.Cookies["UserEmail"]?.Value;
            if (userEmail != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM UserAnswers WHERE Email = @Email", con);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void LoadQuestion()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Questions", con);
                totalQuestions = (int)cmd.ExecuteScalar();

                if (currentQuestionIndex < totalQuestions)
                {
                    cmd = new SqlCommand("SELECT * FROM Questions ORDER BY QuestionID OFFSET @Index ROWS FETCH NEXT 1 ROWS ONLY", con);
                    cmd.Parameters.AddWithValue("@Index", currentQuestionIndex);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        QuestionLabel.Text = reader["QuestionText"].ToString();
                        AnswersRadioButtonList.Items.Clear();
                        AnswersRadioButtonList.Items.Add(new ListItem(reader["Answer1"].ToString(), "1"));
                        AnswersRadioButtonList.Items.Add(new ListItem(reader["Answer2"].ToString(), "2"));
                        AnswersRadioButtonList.Items.Add(new ListItem(reader["Answer3"].ToString(), "3"));
                        AnswersRadioButtonList.Items.Add(new ListItem(reader["Answer4"].ToString(), "4"));
                    }
                }
                else
                {
                    Response.Redirect("ExamResult.aspx");
                }
            }
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            if (AnswersRadioButtonList.SelectedItem != null)
            {
                string userEmail = Request.Cookies["UserEmail"].Value;
                int selectedAnswer = int.Parse(AnswersRadioButtonList.SelectedValue);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserAnswers (Email, QuestionID, SelectedAnswer) VALUES (@Email, @QuestionID, @SelectedAnswer)", con);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@QuestionID", currentQuestionIndex + 1);
                    cmd.Parameters.AddWithValue("@SelectedAnswer", selectedAnswer);
                    cmd.ExecuteNonQuery();
                }

                currentQuestionIndex++;
                LoadQuestion();
            }
        }
    }
}
