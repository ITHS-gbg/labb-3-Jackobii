using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    public IEnumerable<Quiz> AllQuizzes = new List<Quiz>();

    //private IEnumerable<Quiz> LoadAllQuizzes()
    //{
    //    // Ladda in från fil?
    //}
    public QuizManager()
    {
        
    }

    public void SaveQuestion()
    {

    }
    public void SaveQuiz(Quiz quiz)
    {
        //AllQuizzes.Add(quiz);
    }
    public void RemoveQuiz()
    {

    }
    public void UpdateExistingQuiz(Quiz oldQuiz, Quiz newQuiz)
    {
        
    }
}