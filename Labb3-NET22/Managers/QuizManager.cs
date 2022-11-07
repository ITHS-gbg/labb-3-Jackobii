using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Documents;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    private IEnumerable<Quiz> _allQuizzes = new ObservableCollection<Quiz>();
    string myDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JacobFQuizFolder");
    string myQuizPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JacobFQuizFolder", "quizzes.json");
    public IEnumerable<Quiz> AllQuizzes => _allQuizzes;

    public QuizManager()
    {
        LoadAllQuizzes();
    }
    private void LoadAllQuizzes()
    {
        if (File.Exists(myQuizPath))
        {
            string text = string.Empty;
            string? line = string.Empty;
            using StreamReader sr = new StreamReader(myQuizPath);

            while ((line = sr.ReadLine()) != null)
            {
                text += line;
            }
            _allQuizzes = JsonSerializer.Deserialize<ObservableCollection<Quiz>>(text);
        }
    }

    public void SaveQuiz()
    {
        var json = JsonSerializer.Serialize(_allQuizzes, new JsonSerializerOptions { WriteIndented = true });
        using StreamWriter sw = new StreamWriter(myQuizPath);
        sw.WriteLine(json);
    }
    public void SaveQuiz(Quiz quiz)
    {
        if (!File.Exists(myDirectoryPath))
        {
            Directory.CreateDirectory(myDirectoryPath);
        }
        ((ObservableCollection<Quiz>)_allQuizzes).Add(quiz);
        SaveQuiz();
    }
    public void RemoveQuiz(Quiz quiz)
    {
        ((ObservableCollection<Quiz>)_allQuizzes).Remove(quiz);
        SaveQuiz();
    }
    public void UpdateExistingQuiz(Quiz oldQuiz, Quiz newQuiz, int index)
    {
        if (oldQuiz.Title.Equals(newQuiz.Title))
        {
            RemoveQuiz(oldQuiz);
            ((ObservableCollection<Quiz>)_allQuizzes).Add(newQuiz);
        }
    }
}