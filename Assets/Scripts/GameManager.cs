using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
	
	float songPosition;
	float currentOffset;
	
	List<TimingPoints> timingPointsList = new List<TimingPoints>();
	List<HitObject> hitObjectsList = new List<HitObject>();
	
	string filename = "D:\\hackathon\\Alan-Walker-Faded.memw";
	
	int nextIndex = 0;
	
	void Start() {
		//FileParser fileParser;
		// File parser for Kevin to do stuff with.
		loadLevel(filename);
        Debug.Log("yay");
		
		
		
		
		// Start Song
		//GetComponent<AudioSource>().Play();
		
	}
	
	
	void loadLevel(string filename) {
		string line;  
		bool timingPointsStart = false;
		bool hitObjectsStart = false;
		string[] tmp;

		// Read the file and display it line by line.  
		System.IO.StreamReader file =   
		new System.IO.StreamReader(filename);  
		while((line = file.ReadLine()) != null)  
		{  
			System.Console.WriteLine(line);  
			line = line.Trim();
			if(line.Length == 0 || line[0] == '#')
			{
				continue;
			}

			if(hitObjectsStart)
			{
				tmp = line.Split(',');
				HitObject hitObjects = new HitObject();
				hitObjects.setX(tmp[0]);
				hitObjects.setY(tmp[1]);
				hitObjects.setOffset(tmp[2]);
				hitObjects.setIsNote(tmp[3]);
				hitObjects.setIsMine(tmp[4]);
				hitObjects.setColour(tmp[5]);
				hitObjects.setFlashBlack(tmp[6]);
				hitObjects.setIsHold(tmp[7]);
				hitObjectsList.Add(hitObjects);
			}

			if(timingPointsStart)
			{
				tmp = line.Split(',');
				TimingPoints timingPoints = new TimingPoints(Convert.ToInt32(tmp[0]), Convert.ToDouble(tmp[1]), Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[3]), Convert.ToInt32(tmp[4]));
				timingPointsList.Add(timingPoints);
			}

			if(line == "#TimingPoints")
			{
				hitObjectsStart = false;
				timingPointsStart = true;
			}
			if(line == "#HitObjects")
			{
				hitObjectsStart = true;
				timingPointsStart = false;
			}

		}  
		  
		file.Close();  
		
		
	}
	
	
	
	void Update() {		

		// Offset time has passed
		if (songPosition > currentOffset) {
			
			// Get instructions
			
		}
		
		//// Send corresponding inputs to their respective managers //// 
		
		// Spawn note

		
		// Receive hit status, for use in score
		///////
		///////
		
		
		// Show/Set bleep

		
		// Start bleep input 
		// Receive bleep status
		
		
		// End bleep input 
		
		
		// score calculations 
		
		
	}
	
}