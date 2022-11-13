using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace Labb3_NET22.DataModels;   

public class Quiz
{
    private IEnumerable<Question> _questions;
    private string _title = string.Empty;

    public IEnumerable<Question> Questions
    {
        get { return _questions; }
    }
    public string Title
    {
        get { return _title; }
    }

    public Quiz()
    {
        _questions = new List<Question>();
    }

    [JsonConstructor]
    public Quiz(string title, IEnumerable<Question> questions)
    {
        _title = title;
        _questions = new List<Question>();
        _questions = questions.ToList();

    }

    public Question GetRandomQuestion()
    {
        if (!_questions.Any()) return null;

        Random rnd = new Random();
        int rndQuestionIndex = rnd.Next(_questions.Count());
        Question rndQuestion = _questions.ElementAt(rndQuestionIndex);
        RemoveQuestion(rndQuestionIndex);
        return rndQuestion;
    }

    public void AddQuestion(string statement, int correctAnswer, string pictureFilePath, Category category, string[] answers)
    {
        var newQuestion = new Question(statement, answers, correctAnswer, pictureFilePath, category);
        ((List<Question>)_questions)?.Add(newQuestion);
    }

    public void RemoveQuestion(int index)
    {
        ((List<Question>)_questions).RemoveAt(index);
    }
}