using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Kiosk.App;

class Results {
    public void Run() {
        Console.WriteLine("Results");


         // Example data
        int value1 = 30;
        int value2 = 70;

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
                            labels: ['Value 1', 'Value 2'],
                            datasets: [{{
                                data: [{value1}, {value2}],
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