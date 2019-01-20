﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotesController : MonoBehaviour
{
    public Transform leftSpawnerBig;
    public Transform leftSpawnerSmall;
    public Transform rightSpawnerBig;
    public Transform rightSpawnerSmall;

    public GameObject leftBigRing;
    public GameObject leftSmallRing;
    public GameObject rightBigRing;
    public GameObject rightSmallRing;
    public GameObject parentObject;

    List<TimingPoints> timingPointsList = new List<TimingPoints>();
    List<HitObject> hitObjectsList = new List<HitObject>();

    int index;
    long startTime;

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

    void spawnLeftBigRing()
    {
        var currentRing = Instantiate(leftBigRing, leftSpawnerBig.position, leftSpawnerBig.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);
    }

    void spawnLeftSmallRing()
    {
        var currentRing = Instantiate(leftSmallRing, leftSpawnerSmall.position, leftSpawnerSmall.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);
    }

    void spawnRightBigRing()
    {
        var currentRing = Instantiate(rightBigRing, rightSpawnerBig.position, rightSpawnerBig.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);
    }

    void spawnRightSmallRing()
    {
        var currentRing = Instantiate(rightSmallRing, rightSpawnerSmall.position, rightSpawnerSmall.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        loadLevel("Assets/Scripts/Alan-Walker-Faded.memw");
        startTime = DateTime.Now.Ticks;
    }


    // Update is called once per frame
    void Update()
    {
        while (index < hitObjectsList.Count)
        {
            long offsetTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
            HitObject hitObject = hitObjectsList[index];
            
            if (!hitObject.IsNote())
            {
                index++;
            } else if (offsetTime >= hitObject.getOffset())
            {
                if (hitObject.getX() == 64)
                {
                    spawnLeftBigRing();
                }
                else if (hitObject.getX() == 192)
                {
                    spawnLeftSmallRing();
                }
                else if (hitObject.getX() == 320)
                {
                    spawnRightBigRing();
                }
                else if (hitObject.getX() == 448)
                {
                    spawnRightSmallRing();
                }
                index++;
            } else
            {
                break;
            }
        }
    }
}
