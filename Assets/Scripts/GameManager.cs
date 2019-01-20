using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  	float songPosition;
	float currentOffset;

	int nextIndex = 0;

    List<TimingPoints> timingPointsList = new List<TimingPoints>();
    public List<HitObject> hitObjectsList = new List<HitObject>();
    long startTime;
    int index;
    int timingIndex;

    public GameObject simonSaysController;
    public GameObject noteController;
    public AudioSource audioSource;
    private float timer = 0.0f;
    private float songLength;
    public double score;
    private double simSaysBaseScore;
    private double noteBaseScore;
    public double baseScore;

    public int numNormalHit;
    public int numGoodHit;
	
	public Texture whiteTexture;
	

    public static GameManager instance;

    void loadLevel()
    {
        Dictionary<string, string> song = SongSelectParser.Instance.selectedSong;
        string filename = song["SongMap"];
        AudioClip audioClip = Resources.Load<AudioClip>(song["SongPreview"]);
        songLength = audioClip.length;
        Debug.Log(songLength);
        audioSource.PlayOneShot(audioClip);

        string line;
        bool timingPointsStart = false;
        bool hitObjectsStart = false;
        string[] tmp;
        

        // Read the file and display it line by line.  
        System.IO.StreamReader file =
        new System.IO.StreamReader(filename);
        while ((line = file.ReadLine()) != null)
        {
            System.Console.WriteLine(line);
            line = line.Trim();
            if (line.Length == 0 || line[0] == '/')
            {
                continue;
            }
            //Debug.Log(line);

            if (line == "#TimingPoints")
            {
                //Debug.Log("TimingPoints start");
                hitObjectsStart = false;
                timingPointsStart = true;
                continue;
            }
            if (line == "#HitObjects")
            {
                //Debug.Log("hitobject start");
                hitObjectsStart = true;
                timingPointsStart = false;
                continue;
            }

            if (hitObjectsStart)
            {
                //Debug.Log(line);
                tmp = line.Split(',');
                HitObject hitObjects = new HitObject();
                hitObjects.setX(tmp[0]);
                hitObjects.setY(tmp[1]);
                hitObjects.setOffset(tmp[2]);
                hitObjects.setIsNote(tmp[3]);
                hitObjects.setIsMine(tmp[3]);
                hitObjects.setColour(tmp[3]);
                hitObjects.setFlashBlack(tmp[3]);
                hitObjects.setIsHold(tmp[3]);
                hitObjectsList.Add(hitObjects);
            }

            if (timingPointsStart)
            {
                tmp = line.Split(',');
                TimingPoints timingPoints = new TimingPoints(Convert.ToInt32(tmp[0]), Convert.ToDouble(tmp[1]), Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[3]), Convert.ToInt32(tmp[4]));
                timingPointsList.Add(timingPoints);
            }
        }
        file.Close();
    }

    void Start() {
        instance = this;

        // File parser for Kevin to do stuff with.		
        loadLevel();
        // Start Song

        startTime = 0;
        index = 0;
        timingIndex = 0;
        score = 0;
        numNormalHit = 0;
        numGoodHit = 0;
        simSaysBaseScore = 100;
        noteBaseScore = 10;
        //GetComponent<AudioSource>().Play();

    }
	
    public void NoteHit(bool goodHit)
    {

        Debug.Log("note hit AAYYYYYYYYYYYYYYYYYYYYYY");
        Debug.Log(string.Format("good hit: {0}", goodHit));
        if(goodHit)
        {
            numGoodHit++;
        } else
        {
            numNormalHit++;
        }
        score += baseScore;
    }

    public void NoteMissed()
    {
        Debug.Log("note missed :(");
        score -= baseScore;
    }


    void Update() {

        timer += Time.deltaTime;

        if(startTime == 0)
        {
            startTime = DateTime.Now.Ticks;
        }

        //gets latest timing points
        long offsetTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
        if (timingIndex >= timingPointsList.Count && offsetTime >= timingPointsList[timingIndex].getOffset())
        {
            if(timingPointsList[timingIndex].getPlaymode() == 0) //note mode
            {
                baseScore = noteBaseScore;
                simonSaysController.GetComponent<simonSaysManager>().disableInput();
            } else
            {
                baseScore = simSaysBaseScore;
                simonSaysController.GetComponent<simonSaysManager>().enableInput();
            }
            timingIndex++;
        }

        //generate notes for simon says and ddr
        while (index < hitObjectsList.Count)
        {
            offsetTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
            HitObject hitObject = hitObjectsList[index];
            
            if (offsetTime >= hitObject.getOffset())
            {
                if (hitObject.isFlashRed())
                {
                    simonSaysController.GetComponent<simonSaysManager>().StoreBleep(0);
                }
                if (hitObject.isFlashBlue())
                {
                    simonSaysController.GetComponent<simonSaysManager>().StoreBleep(1);
                }
                if (hitObject.isFlashYellow())
                {
                    simonSaysController.GetComponent<simonSaysManager>().StoreBleep(2);
                }
                if (hitObject.isFlashGreen())
                {
                    simonSaysController.GetComponent<simonSaysManager>().StoreBleep(3);
                }
                if (hitObject.IsNote())
                {
                    noteController.GetComponent<NotesController>().spawnNotes(hitObject);
                } 
                index++;
            }
            else //if next hit object dont need to be spawned now
            {
                break;
            }
        }

        Debug.Log(timer);
        if (timer >= songLength)
        {
            SceneManager.LoadScene("RankingPanel");
        }

        // Start bleep input 
        // Receive bleep status


        // End bleep input 
    }
	
}