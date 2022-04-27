using System.Collections.Generic;
using System.IO;

public class ReportReader
{
    public string[] ReadReport(string path)
    {
        var reader = new StreamReader(path);
        var unusedAssets = new List<string>();
        string line;
        while((line = reader.ReadLine()) != null)
        {
            if(!line.StartsWith(ASSET_FOLDER_NAME)) continue;
            if(string.IsNullOrWhiteSpace(line)) continue;

            unusedAssets.Add(line);
        }

        reader.Close();

        return unusedAssets.ToArray();
    }

    private const string ASSET_FOLDER_NAME = "Assets";
}
