using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SongDetailParser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    List<Dictionary<string, string>> CreateSongList()
    {
        List<Dictionary<string, string>> songs = new List<Dictionary<string, string>>();

        foreach (string directory in Directory.GetDirectories(".\\Assets\\Resources\\Songs"))
        {
            // Should only be one .memw file in each song directory
            string songMap = Directory.GetFiles(directory, "*.memw")[0];

            // Remove the file path for the mp3 and the extension
            string audioFile = Directory.GetFiles(directory, "*.mp3")[0].Replace(".\\Assets\\Resources\\", "").Replace(".mp3", "");
            var songInfo = new Dictionary<string, string>
            {
                ["SongPreview"] = audioFile
            };

            StreamReader file = new StreamReader(songMap);
            string line, prevLine = "";
            string[] parts;
            bool readTimingPoints = false, readHitObjects = false;
            int maxBpm = 0, startOffset = 0, memSegs = 0;
            while ((line = file.ReadLine()) != null)
            {
                parts = line.Split(':');
                //if (metaInfo.Contains(parts[0]))
                //{
                //    songInfo[parts[0]] = parts[1];
                //}
                if (parts[0] == "#TimingPoints")
                {
                    readTimingPoints = true;
                }
                else if (parts[0] == "#HitObjects")
                {
                    readTimingPoints = false;
                    readHitObjects = true;
                }
                else if (readTimingPoints)
                {
                    parts = line.Split(',');
                    Int32.TryParse(parts[1], out int bpm);
                    if (bpm > maxBpm)
                    {
                        maxBpm = bpm;
                    }
                    if (parts[4] == "1" || parts[4] == "-1")
                    {
                        memSegs++;
                    }
                }
                else if (readHitObjects)
                {
                    parts = line.Split(',');
                    Int32.TryParse(parts[2], out startOffset);
                    readHitObjects = false;
                }
                prevLine = line;
            }
            parts = prevLine.Split(',');
            Int32.TryParse(parts[2], out int endOffset);

            songInfo["MaxBpm"] = maxBpm.ToString();
            int mapLength = endOffset - startOffset;

            songInfo["MapLength"] = ((mapLength / 1000) / 60).ToString() + ":" + ((mapLength % 1000) % 60);
            songs.Add(songInfo);
        }

        return songs;
    }
}
