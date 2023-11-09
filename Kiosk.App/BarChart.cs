using System;
using System.Collections.Generic;
using System.Text;

namespace Kiosk.App
{
    class BarChart
    {
        public void Run()
        {
            // Sample frequency data with numeric values on the X-axis
            Dictionary<int, int> frequencyData = new Dictionary<int, int>
            {
                { 1, 20 },
                { 2, 30 },
                { 3, 50 }
            };

            // Generate HTML with embedded JavaScript for the bar chart
            string html = GenerateBarChartHTML("My Bar Chart", frequencyData);

            Console.WriteLine(html);
        }

        static string GenerateBarChartHTML(string chartTitle, Dictionary<int, int> data)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // HTML head
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<title>Bar Chart Example</title>");
            htmlBuilder.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/chart.js\"></script>"); // Chart.js library
            htmlBuilder.AppendLine("</head>");

            // HTML body with embedded JavaScript
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<canvas id=\"barChart\" width=\"400\" height=\"400\"></canvas>");
            htmlBuilder.AppendLine("<script>");
            htmlBuilder.AppendLine("var ctx = document.getElementById('barChart').getContext('2d');");
            htmlBuilder.AppendLine($"var data = {GetChartDataJson(data)};");
            htmlBuilder.AppendLine("var options = { responsive: false, scales: { x: { type: 'linear', position: 'bottom' } } };"); // Adjust options as needed
            htmlBuilder.AppendLine("var barChart = new Chart(ctx, { type: 'bar', data: data, options: options });");
            htmlBuilder.AppendLine("</script>");
            htmlBuilder.AppendLine("</body>");

            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }

        static string GetChartDataJson(Dictionary<int, int> data)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.AppendLine("{");
            jsonBuilder.AppendLine("labels: [");

            // Add numeric values for the X-axis
            foreach (var label in data.Keys)
            {
                jsonBuilder.AppendLine($"{label},");
            }

            jsonBuilder.AppendLine("],");
            jsonBuilder.AppendLine("datasets: [{");
            jsonBuilder.AppendLine("label: 'Frequency',");
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
