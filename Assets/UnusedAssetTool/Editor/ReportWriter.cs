using System.Collections.Generic;
using System.IO;
using System.Text;

public class ReportWriter 
{
    public StringBuilder GenerateHeader(int assetCount, int folderCount, int sceneCount, int usedAsseCount)
    {
        var header = new StringBuilder();
        header.AppendLine($"Folders : {folderCount}");
        header.AppendLine($"Scenes : {sceneCount}");
        header.AppendLine($"Assets : {assetCount}");
        header.AppendLine($"Unused assets : {assetCount - usedAsseCount}");
        header.AppendLine();
        return header;
    }

    public StringBuilder GenerateBody(IEnumerable<string> assets, List<string> usedAssets)
    {
        var report = new StringBuilder();
        var enumerator = assets.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (usedAssets.Contains(enumerator.Current)) continue;

            report.AppendLine(enumerator.Current);
        }

        return report;
    }

    public void WriteReport(string path, StringBuilder report, StringBuilder header = null)
    {
        var writer = new StreamWriter(path, false);
        if(header != null)
        {
            writer.WriteLine(header);
        }

        writer.Write(report);
        writer.Close();
    }
}