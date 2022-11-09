using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    private IEnumerable<Quiz> _allQuizzes = new ObservableCollection<Quiz>();
    private readonly string _myDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JacobFQuizFolder");
    private string _myQuizPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JacobFQuizFolder", "quiz.json");

    public string MyQuizPath
    {
        get { return _myQuizPath; }
        set { _myQuizPath = value; }
    }
    public IEnumerable<Quiz> AllQuizzes => _allQuizzes;
    public QuizManager()
    {
        LoadAllQuizzes();
    }
    private async Task LoadAllQuizzes()
    {
        string[] quizDirectories = Directory.GetDirectories(_myDirectoryPath);
        foreach (var directory in quizDirectories)
        {
            if (File.Exists(Path.Combine(directory, "quiz.json")))
            {
                string text = string.Empty;
                using StreamReader sr = new StreamReader(Path.Combine(directory, "quiz.json"));

                text = await sr.ReadToEndAsync();
                ((ObservableCollection<Quiz?>)_allQuizzes).Add(JsonSerializer.Deserialize<Quiz>(text));
            }
        }
    } // DONE
    public void RemoveQuiz(Quiz quiz) // DONE
    {
        ((ObservableCollection<Quiz>)_allQuizzes).Remove(quiz); // Ta bort ur den globala listan

        string folderPath = GenerateQuizFolderName(quiz);
        Directory.Delete(Path.Combine(_myDirectoryPath, folderPath), true);
    }
    public void UpdateExistingQuiz(Quiz oldQuiz, Quiz newQuiz)
    {
        if (oldQuiz.Title.Equals(newQuiz.Title))
        {
            RemoveQuiz(oldQuiz);
            SaveToFileQuiz(newQuiz);
            ((ObservableCollection<Quiz>)_allQuizzes).Add(newQuiz);
        }
    } // Ej implementerad
    public void SaveToFileQuiz(Quiz quiz) // DONE
    {
        string folderName = GenerateQuizFolderName(quiz);
        GenerateQuizDirectory(folderName);
        UpdateMyQuizPath(folderName);
        SaveQuiz(quiz);
    }
    public async Task SaveQuiz(Quiz quiz) // DONE
    {
        ((ObservableCollection<Quiz>)_allQuizzes).Add(quiz); // Lägg till i den globala listan så att den kan spelas/redigeras

        var json = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
        using StreamWriter sw = new StreamWriter(MyQuizPath); 
        await sw.WriteAsync(json);
    }

    public string GenerateQuizFolderName(Quiz quiz) // DONE
    {
        string quizFolderName = quiz.Title;
        char[] invalidPathChars = Path.GetInvalidFileNameChars();
        foreach (char a in invalidPathChars)
        {
            quizFolderName.Replace(a, (char)'-');
        }
        return quizFolderName;
    }
    public void GenerateQuizDirectory(string quizFolderName) // DONE
    {
        if (!File.Exists(Path.Combine(_myDirectoryPath, quizFolderName)))
        {
            Directory.CreateDirectory(Path.Combine(_myDirectoryPath, quizFolderName));
        }
    }
    public void UpdateMyQuizPath(string folderName) // DONE
    {
        MyQuizPath = Path.Combine(_myDirectoryPath, folderName, "quiz.json");
    }
}