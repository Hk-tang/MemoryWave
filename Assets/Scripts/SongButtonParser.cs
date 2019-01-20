using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SongButtonParser : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    static HashSet<string> metaInfo = new HashSet<string>
        {
            "PreviewTime",
            "Title",
            "Artist",
            "Creator",
            "Difficulty",
            "Source",
            "Tags",
            "LevelID",
            "HPDrainRate",
            "OverallDifficulty",
            "ApproachRate"
        };

    void GenerateSongButton(Dictionary<string, string> song)
    {
        GameObject songButton = Instantiate(scrollItemPrefab);

        songButton.transform.SetParent(scrollContent.transform, false);
        songButton.transform.Find("SongTitle").gameObject.GetComponent<TextMeshProUGUI>().text = song["Title"];
        songButton.transform.Find("ArtistCreator").gameObject.GetComponent<TextMeshProUGUI>().text = song["Artist"] + " // " + song["Creator"];
        songButton.transform.Find("SongPreview").gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(song["SongPreview"]);

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
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var parts = line.Split(':');
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
