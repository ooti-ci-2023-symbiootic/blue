using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
namespace Kiosk.App;

class Results {
    public void Run() {
        Console.WriteLine("Results");

       // try
       // {
            // Read the entire file content as a string
            string jsonContent = File.ReadAllText("userAnswers.txt");

            // Deserialize the JSON string into a dictionary
            List<Dictionary<string, string>> data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonContent);

        // Dictionary to store counts of each "1" value
        Dictionary<string, int> counts = new Dictionary<string, int>();

        // Count occurrences of each "1" value
        foreach (var item in data)
        {
            if (item.TryGetValue("1", out string value1))
            {
                if (counts.ContainsKey(value1))
                {
                    counts[value1]++;
                }
                else
                {
                    counts[value1] = 1;
                }
            }
        }

        List<Result> results = new List<Result>();

        foreach (var count in counts)
        {
            results.Add(new Result { Value = count.Key, Count = count.Value });
        }

        // Create HTML content with Chart.js
        string htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Pie Chart Example</title>
                <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
                <style>
                    body {{
                        display: flex;
                        flex-direction: column;
                        align-items: center;
                        justify-content: center;
                        height: 80vh;
                        margin: 0;
                    }}

                    canvas {{
                        width: 30%;
                        height: 30%;
                    }}
                </style>
            </head>
            <body>
                <canvas id='myPieChart'></canvas>
                <script>
                    var ctx = document.getElementById('myPieChart').getContext('2d');
                    var myPieChart = new Chart(ctx, {{
                        type: 'pie',
                        data: {{
                            labels: ['{results[0].Value}', '{results[1].Value}'],
                            datasets: [{{
                                data: [{results[0].Count}, {results[1].Count}],
                                backgroundColor: ['red', 'blue']
                            }}]
                        }},
                        options: {{
                            cutoutPercentage: 5  // Adjust this value to zoom out
                        }}
                    }});
                </script>
            </body>
            </html>
        ";

        // Write HTML to file
        File.WriteAllText("pie_chart.html", htmlContent);

        Console.WriteLine("HTML file with pie chart in the first half of the page generated successfully.");
    
    
    }
}

public class Result
    {
        public string Value { get; set; }
        public int Count { get; set; }
    }