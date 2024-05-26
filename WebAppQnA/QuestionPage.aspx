<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionPage.aspx.cs" Inherits="QuizApp.Quiz" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Quiz</title>
    <style>
        .left-aligned-container {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            align-items: flex-start;
            padding: 20px; /* Add some padding for spacing */
        }
        .form-group {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="left-aligned-container">
            <div class="form-group">
                <asp:Label ID="QuestionLabel" runat="server" Text=""></asp:Label>
            </div>
            <div class="form-group">
                <asp:RadioButtonList ID="AnswersRadioButtonList" runat="server"></asp:RadioButtonList>
            </div>
            <div class="form-group">
                <asp:Button ID="NextButton" runat="server" Text="Next" OnClick="NextButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
