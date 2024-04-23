using BlazorStatic;

namespace AnasheedArchive;

class ExtractTabs
{
    public static string[] ModifyFiles(string[] filePaths)
    {
        for (int i = 0; i < filePaths.Length; i++)
        {
            List<string>? editedFile = ConvertLyricsToTabs(File.ReadAllLines(filePaths[i]).ToList());
            if (editedFile is not null)
            {
                File.WriteAllLines(filePaths[i], editedFile);
            }
        }
        return filePaths;
    }

    private static List<string>? ConvertLyricsToTabs(List<string> content)
    {
        string identifier = "@@";

        int translationPos = content.FindIndex(x => x.StartsWith(identifier));
        if (translationPos == -1) return null;

        Dictionary<string, List<string>> lyrics = new();
        string lastLanguage = "";
        for (int i = translationPos; i <= content.Count; i++)
        {
            if (content[i].Trim().Length == 0) continue;
            if (i == content.Count || content[i].StartsWith('#')) break;

            if (content[i].StartsWith(identifier))
            {
                lastLanguage = content[i].Substring(identifier.Length + 1);
                lyrics[lastLanguage] = new() { "" };
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
        AddBootstrapTabs(ref content, ref lyrics, translationPos);

        return content;
    }

    private static void AddBootstrapTabs(ref List<string> content, ref Dictionary<string, List<string>> lyrics, int start)
    {
        string tabList = "<nav> <div class='nav nav-tabs' id='nav-tab' role='tablist'>";
        List<string> tabContent = new() { "<div class='tab-content' id='nav-tabContent'>" };

        foreach (var key in lyrics.Keys)
        {
            tabList += $"<button class='nav-link' id='nav-{key}-tab' data-bs-toggle='tab' data-bs-target='#nav-{key}' type='button' role='tab' aria-controls='nav-{key}' aria-selected='true'>{key}</button>";

            tabContent.Add($"<div class='tab-pane fade show' id='nav-{key}' role='tabpanel' aria-labelledby='nav-{key}-tab' tabindex='0'>");
            tabContent.AddRange(lyrics[key]);
            tabContent.Add("</div>");
        }
        tabList += "</div> </nav>";
        tabContent.Add("</div>");

        content.InsertRange(start + 1, tabContent);
        content.Insert(start, tabList);

    }
}
