using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    static async Task PrintMsg(string Ggl_URL)
    {
        using HttpClient client = new HttpClient();
        string html = await client.GetStringAsync(Ggl_URL);

        HtmlDocument Doc = new HtmlDocument();
        Doc.LoadHtml(html);
        Dictionary<(int x, int y), string> grid = new();

        var rows = Doc.DocumentNode.SelectNodes("//tr");

        if (rows == null)
        {
            Console.WriteLine("rows not found");
            return;
        }

        foreach (var row in rows)
        {
            var cells = row.SelectNodes("./th|./td");

            if (cells == null || cells.Count < 3)
                continue;


            string A = cells[0].InnerText.Trim();
            string Char = cells[1].InnerText.Trim();
            string Txt = cells[2].InnerText.Trim();

            if (!int.TryParse(A, out int x))
                continue;

            if (!int.TryParse(Txt, out int y))
                continue;

            grid[(x, y)] = Char;
        }

        if (grid.Count == 0)
        {
            Console.WriteLine("data not found.");
            return;
        }

        int maxX = 0;
        int maxY = 0;

        foreach (var position in grid.Keys)
        {
            maxX = Math.Max(maxX, position.x);
            maxY = Math.Max(maxY, position.y);
        }

        for (int y = maxY; y >= 0; y--)
        {
            for (int x = 0; x <= maxX; x++)
            {
                if (grid.TryGetValue((x, y), out string Char))
                {
                    Console.Write(Char);
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
    }

    static async Task Main()
    {
        string Ggl_URL =
            "https://docs.google.com/document/d/e/2PACX-1vTMOmshQe8YvaRXi6gEPKKlsC6UpFJSMAk4mQjLm_u1gmHdVVTaeh7nBNFBRlui0sTZ-snGwZM4DBCT/pub";

        await PrintMsg(Ggl_URL);
    }
}