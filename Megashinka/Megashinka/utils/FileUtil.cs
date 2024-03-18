﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public static class FileUtil
{
    //　Pathにファイルがない場合作成し、ある場合その中身を消す
    public static void CreateOrClearCsvFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, string.Empty);
        }
        else
        {
            using (var fs = File.Create(filePath))
            {
                // 初期データを書き込む必要がある場合ここに記述
            }
        }
    }

    public static List<string> Tail(string filePath, int numberOfLines)
    {
        List<string> lines = new List<string>();
        int lineCount = 0;
        long position;
        byte[] buffer = new byte[512];
        StringBuilder line = new StringBuilder();
        Encoding encoding = Encoding.UTF8;

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            long fileLength = fs.Length;
            if (fileLength == 0){ return lines; }
            position = fileLength - 1;
            fs.Seek(position, SeekOrigin.Begin);

            while (position > 0 && lineCount < numberOfLines)
            {
                int bytesRead = fs.Read(buffer, 0, buffer.Length > position ? (int)(position + 1) : buffer.Length);
                for (int i = bytesRead - 1; i >= 0; i--)
                {
                    position--;
                    char ch = encoding.GetString(buffer, i, 1)[0];
                    if (ch == '\n' || position == -1)
                    {
                        if (line.Length > 0)
                        {
                            lines.Add(line.ToString());
                            line.Clear();
                            lineCount++;
                            if (lineCount == numberOfLines)
                                break;
                        }
                    }
                    else if (ch != '\r')
                    {
                        line.Insert(0, ch);
                    }
                }
                if (position > 0)
                {
                    fs.Seek(position - Math.Min(buffer.Length, position) + 1, SeekOrigin.Begin);
                }
            }
        }

        lines.Reverse();
        return lines;
    }


    // CSVから指定された行数を読み込み、trueの割合が閾値以上ならtrueを返す新しいメソッド
    public static bool CheckTruePercentageInRecentLines(string filePath, int numberOfLines, double threshold)
    {
        var lines = Tail(filePath, numberOfLines);
        int trueCount = 0;
        foreach (var line in lines)
        {
            // CSV行から、最初のカラム（文字列）を取得し、判定ロジックを適用
            var columns = line.Split(',');
            if (columns.Length > 0)
            {
                var value = columns[0].ToLower(); // 小文字に変換して比較
                if (value == "true" || value == "sleep")
                {
                    trueCount++;
                }
            }
        }
        if (lines.Count > 0)
        {
            double truePercentage = (double)trueCount / lines.Count;
            Console.WriteLine(truePercentage.ToString());
            return truePercentage >= threshold;
        }
        return false;
    }
}
