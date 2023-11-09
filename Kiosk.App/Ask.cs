using System;
using System.Collections.Generic;

class Question
{
    public string question { get; set; }
    public string type { get; set; }
    public Dictionary<string, string> answers { get; set; }
}

class Ask
{
    public void Run()
    {
        Console.WriteLine("Ask");

        string json = @"
        {
            ""1"": {
                ""question"": ""Who is better?"",
                ""type"": ""single-choice"",
                ""answers"": {
                    ""1"": ""cats"",
                    ""2"": ""dogs""
                }
            },
            ""2"": {
                ""question"": ""How old are you?"",
                ""type"": ""number"",
                ""answers"": {
                    ""1"": ""User Input""
                }
            }
        }";

        var questions = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Question>>(json);
        var userAnswers = new Dictionary<string, string>();

        foreach (var question in questions)
        {
            Console.WriteLine(question.Value.question);

            if (question.Value.type == "single-choice")
            {
                foreach (var answer in question.Value.answers)
                {
                    Console.WriteLine($"{answer.Key}: {answer.Value}");
                }

                Console.Write("Your choice: ");
                string userChoice = Console.ReadLine();

                if (question.Value.answers.ContainsKey(userChoice))
                {
                    userAnswers[question.Key] = question.Value.answers[userChoice];
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose a valid option.");
                }
            }
            else if (question.Value.type == "number")
            {
                Console.Write("Enter your age: ");
                string age = Console.ReadLine();
                userAnswers[question.Key] = age;
            }
        }

        Console.WriteLine("\nResults:");
        foreach (var answer in userAnswers)
        {
            Console.WriteLine($"Question {answer.Key}: {questions[answer.Key].question}");
            Console.WriteLine($"Your answer: {answer.Value}");
        }
    }
}


