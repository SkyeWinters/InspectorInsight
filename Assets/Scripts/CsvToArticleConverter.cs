using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVToArticleConverter : EditorWindow
{
    private TextAsset csvFile;

    [MenuItem("Tools/CSV to Article Converter")]
    public static void ShowWindow()
    {
        GetWindow<CSVToArticleConverter>("CSV to Article Converter");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV to Article Converter", EditorStyles.boldLabel);
        csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", csvFile, typeof(TextAsset), false);

        if (GUILayout.Button("Convert CSV to Articles"))
        {
            if (csvFile != null)
            {
                ConvertCSVToArticles();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a CSV file first!", "OK");
            }
        }
    }

    private void ConvertCSVToArticles()
    {
        string[] lines = csvFile.text.Split('\n');

        // Ensure the local directory exists
        string localDirectory = "Assets/Articles";
        if (!AssetDatabase.IsValidFolder(localDirectory))
        {
            AssetDatabase.CreateFolder("Assets", "Articles");
        }

        // Assuming the first line is the header
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = ParseCSVLine(lines[i]);

            if (values.Length < 4)
            {
                Debug.LogWarning($"Line {i + 1} is malformed: {lines[i]}");
                continue;
            }

            Article newArticle = CreateInstance<Article>();
            newArticle.Label = values[0].Trim();
            newArticle.Title = values[1].Trim();
            newArticle.Description = values[2].Trim();

            if (System.Enum.TryParse(values[3].Trim(), out ArticleCategory category))
            {
                newArticle.Category = category;
            }
            else
            {
                Debug.LogWarning($"Category '{values[3].Trim()}' on line {i + 1} is not valid.");
                continue;
            }
            
            newArticle.Audio = FindAudioClip(newArticle.Label);

            var assetPath = $"{localDirectory}/{newArticle.Label}.asset";
            var existingArticle = AssetDatabase.LoadAssetAtPath<Article>(assetPath);
            if (existingArticle!= null)
            {
                existingArticle.CloneFrom(newArticle);
                EditorUtility.SetDirty(existingArticle);
            }
            else
            {
                AssetDatabase.CreateAsset(newArticle, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Conversion Completed", "CSV has been successfully converted to Articles!", "OK");
    }

    private string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string currentField = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"' && (i == 0 || line[i - 1] != '\\'))
            {
                inQuotes = !inQuotes; // Toggle state
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(currentField.Trim());
                currentField = "";
            }
            else
            {
                currentField += c;
            }
        }

        result.Add(currentField.Trim());
        return result.ToArray();
    }
    
    private AudioClip FindAudioClip(string label)
    {
        string audioFolderPath = "Assets/Audio";
        string[] audioFiles = Directory.GetFiles(audioFolderPath, "*.mp3", SearchOption.AllDirectories);

        foreach (string audioFile in audioFiles)
        {
            string audioFileName = Path.GetFileNameWithoutExtension(audioFile);

            if (audioFileName.Equals(label, System.StringComparison.OrdinalIgnoreCase))
            {
                return AssetDatabase.LoadAssetAtPath<AudioClip>(audioFile.Replace(Application.dataPath, "Assets"));
            }
        }

        Debug.LogWarning($"No matching audio file found for label: {label}");
        return null;
    }

}
