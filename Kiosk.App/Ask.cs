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
        string userName;

        // Dictionary to store user answers for all users
        var allUserAnswers = new List<Dictionary<string, string>>();

        while (true)
        {
            Console.Write("Please enter your name (or '#' to exit): ");
            userName = Console.ReadLine();

            if (userName == "#")
            {
                // User wants to exit, so print answers for all users and exit
                PrintAllUserAnswers(allUserAnswers);
                return;
            }

            var userAnswers = new Dictionary<string, string>();

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

            foreach (var question in questions)
            {
                Console.WriteLine(question.Value.question);

                if (question.Value.type == "single-choice")
                {
                    foreach (var answer in question.Value.answers)
                    {
                        Console.WriteLine($"{answer.Key}: {answer.Value}");
                    }

                    Console.Write("Your choice (or '#' to exit): ");
                    string userChoice = Console.ReadLine();

                    if (userChoice == "#")
                    {
                        // User wants to exit, so print answers for this user and continue to the next user
                        break;
                    }

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

            // Console.WriteLine("Thank you, " + userName + "! Your answers for this round:");
            // PrintAnswers(userAnswers);

            // Add user answers to the list of all user answers
            allUserAnswers.Add(userAnswers);
        }
    }

    private void PrintAnswers(Dictionary<string, string> userAnswers)
    {
        foreach (var answer in userAnswers)
        {
            Console.WriteLine($"Question {answer.Key}: {answer.Value}");
        }
    }

    private void PrintAllUserAnswers(List<Dictionary<string, string>> allUserAnswers)
    {
        Console.WriteLine("\nAll User Answers:");
        for (int i = 0; i < allUserAnswers.Count; i++)
        {
            Console.WriteLine($"User {i + 1} Answers:");
            PrintAnswers(allUserAnswers[i]);
            Console.WriteLine();
        }
    }
}

