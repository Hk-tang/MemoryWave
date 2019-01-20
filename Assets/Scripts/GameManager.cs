using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
	
	float songPosition;
	float currentOffset;

	int nextIndex = 0;

    List<TimingPoints> timingPointsList = new List<TimingPoints>();
    List<HitObject> hitObjectsList = new List<HitObject>();
    long startTime;
    int index;

    public GameObject simonSaysController;
    public GameObject noteController;
    

    void loadLevel(string filename)
    {
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
        //Debug.Log("yay");
    }

    void Start() {
        // File parser for Kevin to do stuff with.		
        loadLevel("Assets/Scripts/Alan-Walker-Faded.memw");
        // Start Song

        startTime = 0;
        index = 0;
        //GetComponent<AudioSource>().Play();

    }
	
	void Update() {
        if(startTime == 0)
        {
            startTime = DateTime.Now.Ticks;
        }
        while (index < hitObjectsList.Count)
        {
            long offsetTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
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
		
		// Receive hit status, for use in score
		
		// Start bleep input 
		// Receive bleep status
		
		
		// End bleep input 
		
		
		// score calculations 
		
		
	}
	
}