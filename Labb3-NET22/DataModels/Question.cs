namespace Labb3_NET22.DataModels;

public class Question
{
    public string Statement { get; }
    public string[] Answers { get; }
    public int CorrectAnswer { get; }
    public string QuestionPicturePath  { get; set; }
    public Category QuestionCategory { get; }

    public Question(string statement, string[] answers, int correctAnswer, string questionPicturePath, Category questionCategory)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        QuestionPicturePath = questionPicturePath;
        QuestionCategory = questionCategory;
    }
}