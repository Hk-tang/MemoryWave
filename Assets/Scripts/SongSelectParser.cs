using System;
using System.IO;
using System.Collections.Generic;

public class SongSelectParser
{
	public static void Main()
    {
        List<string[]> songs = new List<string[]>();
        foreach (string file in Directory.EnumerateFiles("..\\Songs", "*.memw"))
        {
            string[] lines = File.ReadAllLines(file);
            string[] metaInfo = new string[15];
            foreach (string line in lines)
            {
                var parts = line.Split(':');
                if(parts[0] == "AudioFilename")
                {
                    metaInfo[0] = parts[1];
                }
                else if (parts[0] == "PreviewTime") 
                {
                    metaInfo[1] = parts[1];
                }
                else if (parts[0] == "Title")
                {
                    metaInfo[2] = parts[1];
                }
                else if (parts[0] == "TitleUnicode")
                {
                    metaInfo[3] = parts[1];
                }
                else if (parts[0] == "Artist")
                {
                    metaInfo[4] = parts[1];
                }
                else if (parts[0] == "ArtistUnicode")
                {
                    metaInfo[5] = parts[1];
                }
                else if (parts[0] == "Creator")
                {
                    metaInfo[6] = parts[1];
                }
                else if (parts[0] == "Version")
                {
                    metaInfo[7] = parts[1];
                }
                else if (parts[0] == "Source")
                {
                    metaInfo[8] = parts[1];
                }
                else if (parts[0] == "Tags")
                {
                    metaInfo[9] = parts[1];
                }
                else if (parts[0] == "LevelID")
                {
                    metaInfo[10] = parts[1];
                }
                else if (parts[0] == "LevelSetID")
                {
                    metaInfo[11] = parts[1];
                }
                else if (parts[0] == "HPDrainRate")
                {
                    metaInfo[12] = parts[1];
                }
                else if (parts[0] == "OverallDifficulty")
                {
                    metaInfo[13] = parts[1];
                }
                else if (parts[0] == "ApproachRate")
                {
                    metaInfo[14] = parts[1];
                }
            }

            songs.Add(metaInfo);
        }
    }
}
