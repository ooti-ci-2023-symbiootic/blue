using System;
using System.Collections.Generic;
using System.Text;

namespace Kiosk.App
{
    class PieChart
    {
        public static void Run()
        {
            // Sample frequency data
            Dictionary<string, int> frequencyData = new Dictionary<string, int>
            {
                { "Category1", 20 },
                { "Category2", 30 },
                { "Category3", 50 }
            };

            // Generate HTML with embedded JavaScript for the pie chart
            string html = GeneratePieChartHTML("My Pie Chart", frequencyData);

            Console.WriteLine(html);
        }

        public static string GeneratePieChartHTML(string chartTitle, Dictionary<string, int> data)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // HTML head
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<title>Pie Chart Example</title>");
            htmlBuilder.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/chart.js\"></script>"); // Chart.js library
            htmlBuilder.AppendLine("</head>");

            // HTML body with embedded JavaScript
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<canvas id=\"pieChart\" width=\"400\" height=\"400\"></canvas>");
            htmlBuilder.AppendLine("<script>");
            htmlBuilder.AppendLine("var ctx = document.getElementById('pieChart').getContext('2d');");
            htmlBuilder.AppendLine($"var data = {GetChartDataJson(data)};");
            htmlBuilder.AppendLine("var options = { responsive: false };"); // Adjust options as needed
            htmlBuilder.AppendLine("var pieChart = new Chart(ctx, { type: 'pie', data: data, options: options });");
            htmlBuilder.AppendLine("</script>");
            htmlBuilder.AppendLine("</body>");

            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }

        static string GetChartDataJson(Dictionary<string, int> data)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.AppendLine("{");
            jsonBuilder.AppendLine("labels: [");

            // Add category labels
            foreach (var category in data.Keys)
            {
                jsonBuilder.AppendLine($"'{category}',");
            }

            jsonBuilder.AppendLine("],");
            jsonBuilder.AppendLine("datasets: [{");
            jsonBuilder.AppendLine("data: [");

            // Add data values
            foreach (var value in data.Values)
            {
                jsonBuilder.AppendLine($"{value},");
            }

            jsonBuilder.AppendLine("],");
            jsonBuilder.AppendLine("backgroundColor: [");

            // Add background colors for each category
            foreach (var _ in data)
            {
                // Add your color logic here
                jsonBuilder.AppendLine("'#'+(Math.random()*0xFFFFFF<<0).toString(16),");
            }

            jsonBuilder.AppendLine("],");
            jsonBuilder.AppendLine("}]");
            jsonBuilder.AppendLine("}");

            return jsonBuilder.ToString();
        }
    }
}
