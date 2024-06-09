using System.Reflection;

namespace AnasheedArchive;

class ExtractTabs
{
    public static string[] ModifyFiles(string contentPath, string[]? filePaths = null)
    {
        filePaths ??= GetFilesPath(contentPath);
        for (int i = 0; i < filePaths.Length; i++)
        {
            List<string>? editedFile = ConvertLyricsToTabs(File.ReadAllLines(filePaths[i]).ToList(), filePaths[i]);
            if (editedFile is not null)
            {
                File.WriteAllLines(filePaths[i], editedFile);
            }
        }
        return filePaths;
    }

    private static List<string>? ConvertLyricsToTabs(List<string> content, string fileName)
    {
        string identifier = "@@";

        int translationPos = content.FindIndex(x => x.StartsWith(identifier));
        if (translationPos == -1) return null;

        Dictionary<string, List<string>> lyrics = new();
        string lastLanguage = "";

        for (int i = translationPos; i <= content.Count; i++)
        {

            if (i > content.Count - 1) break;
            if (content[i].Trim().Length == 0) continue;
            if (i == content.Count || content[i].StartsWith('#')) break;

            if (content[i].StartsWith(identifier))
            {
                lastLanguage = content[i].Substring(identifier.Length + 1);
                lyrics[lastLanguage] = [""];
                content.RemoveAt(i);
                i--;
                continue;
            }

            // var verse = content[i];
            lyrics[lastLanguage].Add(content[i]);
            content.RemoveAt(i);
            i--;
        }

        content.InsertRange(translationPos, ["", ""]);
        AddBootstrapColumns(ref content, ref lyrics, translationPos);

        return content;
    }

    private static void AddBootstrapTabs(ref List<string> content, ref Dictionary<string, List<string>> lyrics, int startPos)
    {
        string tabList = "<nav> <div class='nav nav-tabs' id='nav-tab' role='tablist'>";
        List<string> tabContent = new() { "<div class='tab-content' id='nav-tabContent'>" };

        foreach (var key in lyrics.Keys)
        {
            tabList += $"<button class='nav-link' id='nav-{key}-tab' data-bs-toggle='tab' data-bs-target='#nav-{key}' type='button' role='tab' aria-controls='nav-{key}' aria-selected='true'>{key}</button>";

            tabContent.Add($"<div class='tab-pane fade show text-center' id='nav-{key}' role='tabpanel' aria-labelledby='nav-{key}-tab' tabindex='0'>");
            tabContent.AddRange(lyrics[key]);
            tabContent.Add("</div>");
        }
        tabList += "</div> </nav>";
        tabContent.Add("</div>");

        content.InsertRange(startPos + 1, tabContent);
        content.Insert(startPos, tabList);

    }

    private static void AddBootstrapTables(ref List<string> content, ref Dictionary<string, List<string>> lyrics, int startPos)
    {
        string table = "<div class='table-responsive'><table class='table text-center table-borderless align-middle'>";
        string thead = $"<thead><tr>{GenerateTableHead(lyrics.Keys)}</tr></thead>";
        string tbody = "<tbody>";

        int i = 0;
        while (i < lyrics.FirstOrDefault().Value.Count - 1)
        {
            tbody += "<tr>";
            foreach (var lang in lyrics.Keys)
            {
                tbody += Tabalize(lyrics[lang][i]);
            }
            tbody += "</tr>";
            i++;
        }
        // thead += "</tr></thead>";
        tbody += "</tbody>";
        table += thead + tbody + "</table></div>";
        content.Insert(startPos, table);

    }

    private static string GenerateTableHead(Dictionary<string, List<string>>.KeyCollection keys)
    {
        var sum = "";
        foreach (string key in keys)
        {
            sum += $"<th scope='col' class='pb-4'>{key}</th>";
        }
        return sum;
    }

    private static string Tabalize(string verse)
    {
        verse = verse.Trim();
        if (verse.Length == 0)
        {
            return "";
        }
        if (verse.StartsWith('-'))
        {
            verse = verse.Remove(0, 1);
        }
        return $"<td class='pb-3'>{verse}</td>";
    }

    private static void AddBootstrapColumns(ref List<string> content, ref Dictionary<string, List<string>> lyrics, int startPos)
    {
        string container = "<div class='container'>";
        string row = "<div class='text-center row row-cols-1 xx'>";
        row = row.Replace("xx", $"row-cols-sm-{lyrics.Count}"); // shittiest code ever :D
        string column = "<div class='col pb-5'>";

        foreach (var translation in lyrics)
        {
            string nestedRow = $"<div class='row row-cols-1'>" + column + "<p class='h3'>" + translation.Key + "</p>" + "</div>";
            foreach (var verse in translation.Value)
            {
                var verse2 = verse.Trim();
                if (verse2.Length == 0)
                {
                    continue;
                }
                if (verse2.StartsWith('-'))
                {
                    verse2 = verse2.Remove(0, 1);
                }
                nestedRow += column + verse2 + "</div>";
            }

            row += nestedRow + "</div>";

            if (translation.Key != lyrics.LastOrDefault().Key)
            {
                // todo: make the ruler hidden on mobile view
                // row += "<div class='vr' style='padding: 0px; color: #ffffff'></div>";
            }
        }

        content.Insert(startPos, container + row + "</div></div>");

    }
    private static string GenerateGridHead(Dictionary<string, List<string>>.KeyCollection keys)
    {
        var sum = "";
        foreach (string key in keys)
        {
            sum += $"<th scope='col' class='pb-4'>{key}</th>";
        }
        return sum;
    }

    private static string[] GetFilesPath(string contentPath)
    {
        EnumerationOptions x = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
        };
        string execFolder = Directory.GetParent(Assembly.GetEntryAssembly()!.Location)!.FullName;
        return Directory.GetFiles(Path.Combine(execFolder, contentPath), "*.md", x);
    }
}
