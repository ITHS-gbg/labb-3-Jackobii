using System.Collections.ObjectModel;
using System.Data;
using System.Text.Json.Serialization;

namespace Labb3_NET22.DataModels;

public class Question
{
    [JsonInclude]
    public string Statement { get; }
    [JsonInclude]
    public string[] Answers { get; }
    [JsonInclude]
    public int CorrectAnswer { get; }

    public Question(string statement, string[] answers, int correctAnswer)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}