<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeginTest.aspx.cs" Inherits="QuizApp.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Start Quiz</title>
    <style>
        .centered-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            text-align: center;
        }
        .form-group {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="centered-container">
            <div class="form-group">
                <asp:Label ID="EmailLabel" runat="server" Text="Please Enter Your Email:"></asp:Label>
            </div>
            <div class="form-group">
                <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                    ControlToValidate="EmailTextBox"
                    ErrorMessage="Invalid Email Format."
                    ValidationExpression="\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="BeginExamButton" runat="server" Text="BEGIN EXAM" OnClick="BeginExamButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
