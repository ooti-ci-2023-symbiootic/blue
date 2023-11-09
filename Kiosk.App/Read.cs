using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;


namespace Kiosk.App;



class Read {
    public void Run() {
        Console.WriteLine("Questions file contains:");
        Console.WriteLine(ReadQuestionsFile());
    }

    

    public string parseQuestions(string input){

        // Split input by delimiter and remove empty entries
        string[] entries = input.Split(new[] { "----------------------" }, StringSplitOptions.RemoveEmptyEntries);

        // Process each entry to create the desired structure
        Dictionary<int, Dictionary<string, object>> result = new Dictionary<int, Dictionary<string, object>>();
        int currentKey = 1;

        foreach (var entry in entries)
        {
            string[] lines = entry.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length >= 3)
            {
                string[] options = lines[2].Split(',');

                Dictionary<int, string> optionsDictionary = new Dictionary<int, string>();
                for (int i = 0; i < options.Length; i++)
                {
                    optionsDictionary.Add(i + 1, options[i].Trim());
                }

                result[currentKey] = new Dictionary<string, object>
                {
                    { "question", lines[1].Trim() },
                    { "type", lines[0].Trim() },
                    { "answers", optionsDictionary }
                };
                currentKey++;
            }
        }

        // Convert the result to JSON
        string jsonResult = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        return jsonResult;
    }

    // this is just an example of how to read a file,
    // modify/delete as you see fit.
    public string ReadQuestionsFile() {
        // we run in <root>/Kiosk.App/bin/Debug/net6.0, so gotta go up 4 levels
        var rootDir = AppContext.BaseDirectory + "/../../../../";

        var textQuestions = File.ReadAllText(rootDir + "questions.txt");
        // Console.WriteLine(textQuestions);
        return parseQuestions(textQuestions);
    }
}