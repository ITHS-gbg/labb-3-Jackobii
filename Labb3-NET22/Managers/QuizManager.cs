using System;
using System.Collections.Generic;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.Managers;

public static class QuizManager
{
    public static IEnumerable<Quiz> AllQuizzes = new List<Quiz>();

    private static IEnumerable<Quiz> LoadAllQuizzes()
    {
        // Ladda in från fil?
    }
    public static void CreateQuiz()
    {
        Quiz newQuiz = new Quiz();

    }
    public static void SaveQuiz()
    {

    }
    public static void RemoveQuiz()
    {

    }
    public static void UpdateExistingQuiz(Quiz oldQuiz, Quiz newQuiz)
    {
        
    }
}