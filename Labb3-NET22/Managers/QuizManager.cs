using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
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
    private readonly string _noImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JacobFQuizFolder", "noImage.png");
    private string _myQuizPath;

    public string MyQuizPath
    {
        get { return _myQuizPath; }
        set { _myQuizPath = value; }
    }
    public string NoImagePath => _noImagePath;

    public IEnumerable<Quiz> AllQuizzes => (ObservableCollection<Quiz>)_allQuizzes;
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
                ((ObservableCollection<Quiz?>)AllQuizzes).Add(JsonSerializer.Deserialize<Quiz>(text));
            }
        }
    } // DONE
    public void RemoveQuiz(Quiz quiz) // DONE
    {
        ((ObservableCollection<Quiz>)AllQuizzes).Remove(quiz); // Ta bort ur den globala listan

        string folderPath = GenerateQuizFolderName(quiz);
        File.Delete(Path.Combine(_myDirectoryPath, folderPath, "quiz.json"));
    }
    public void DeleteQuiz(Quiz quiz)
    {
        ((ObservableCollection<Quiz>)AllQuizzes).Remove(quiz);
        string folderPath = GenerateQuizFolderName(quiz);
        Directory.Delete(Path.Combine(_myDirectoryPath, folderPath), true);
        
    }
    public void UpdateExistingQuiz(Quiz oldQuiz, Quiz newQuiz)
    {
        RemoveQuiz(oldQuiz);
        SaveToFileQuiz(newQuiz);
    } // DONE
    public void SaveToFileQuiz(Quiz quiz) // DONE
    {
        string folderName = GenerateQuizFolderName(quiz);
        GenerateQuizDirectory(folderName);
        GeneratePictureFolder(folderName);
        UpdateMyQuizPath(folderName);
        CopyPicturesToQuizFolder(quiz);
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
    public void GeneratePictureFolder(string quizFolderName)
    {
        if (!string.IsNullOrEmpty(quizFolderName))
        {
            string pictureFolderPath = Path.Combine(_myDirectoryPath, quizFolderName, "Pictures");
            if (!File.Exists(pictureFolderPath))
            {
                Directory.CreateDirectory(pictureFolderPath);
            }
        }
    } // DONE
    public void CopyPicturesToQuizFolder(Quiz quiz)
    {
        string newPicturePath = Path.Combine(_myDirectoryPath, GenerateQuizFolderName(quiz), "Pictures");
        foreach (var q in quiz.Questions)
        {
            string oldPicturePath = q.QuestionPicturePath;
            if (!File.Exists(Path.Combine(newPicturePath, Path.GetFileName(oldPicturePath))))
            {
                File.Copy(oldPicturePath, (Path.Combine(newPicturePath, Path.GetFileName(oldPicturePath))), true);
            }
            q.QuestionPicturePath = Path.Combine(newPicturePath, Path.GetFileName(oldPicturePath));
        }
    } // DONE
    public Quiz GenerateRandomQuiz(int index)
    {
        List<Question> questionList = new List<Question>();

        foreach (var qstn in AllQuizzes.SelectMany(qz => qz.Questions))
        {
            if ((int)qstn.QuestionCategory == index)
            {
                questionList.Add(qstn);
            }
        }
        return new Quiz("Kategori Quiz", questionList);
    }
}