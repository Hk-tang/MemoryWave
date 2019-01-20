using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SongSelectParser : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    static HashSet<string> metaInfo = new HashSet<string>
        {
            "AudioFilename",
            "PreviewTime",
            "Title",
            "TitleUnicode",
            "Artist",
            "ArtistUnicode",
            "Creator",
            "Version",
            "Source",
            "Tags",
            "LevelID",
            "LevelSetID",
            "HPDrainRate",
            "OverallDifficulty",
            "ApproachRate"
        };

    void GenerateSongButton(Dictionary<string, string> song)
    {
        
        GameObject songButton = Instantiate(scrollItemPrefab);
        songButton.transform.SetParent(scrollContent.transform, false);
        songButton.transform.Find("SongTitle").gameObject.GetComponent<Text>().text = song["Title"];

    }

    List<Dictionary<string, string>> CreateSongList()
    {
        List<Dictionary<string, string>> songs = new List<Dictionary<string, string>>();
        
        foreach(string directory in Directory.GetDirectories(".\\Songs"))
        {
            string[] files = Directory.GetFiles(directory, "*.memw");

            StreamReader file = new StreamReader(files[0]);
            var songInfo = new Dictionary<string, string>();

            // We only ever need to read the first 19 lines of a memw file
            for (int i = 0; i < 20; i++)
            {
                var parts = file.ReadLine().Split(':');
                if (metaInfo.Contains(parts[0]))
                {
                    songInfo[parts[0]] = parts[1];
                }

            }
            songs.Add(songInfo);
        }
        
        return songs;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, string>> songs = CreateSongList();
        foreach (Dictionary<string, string> song in songs)
        {
            GenerateSongButton(song);
        }

        scrollView.verticalNormalizedPosition = 1;
    }
}
