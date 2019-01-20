using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simonSaysManager : MonoBehaviour
{
 

	public GameObject gameButtonPrefab;

    public List<ButtonSetting> buttonSettings;

    public Transform gameFieldPanelTransform;

	
	public GameObject redButton;
	public GameObject blueButton;
	public GameObject yellowButton;
	public GameObject greenButton;
	
    List<GameObject> gameButtons;

    int bleepCount = 3;

    List<int> bleeps = new List<int>();
    public List<int> playerBleeps = new List<int>();

	
	public static simonSaysManager instance;
	
	AudioSource[] buttonSounds;
	
    bool inputEnabled = false;
	
	// Calls to init button objects  
    void Start() {
		instance = this;
        gameButtons = new List<GameObject>();
	
        ButtonSetter(0, redButton);
		ButtonSetter(1, blueButton);
		ButtonSetter(2, yellowButton);
		ButtonSetter(3, greenButton);
		buttonSounds = GetComponents<AudioSource>();
		
    }

	
	// actually create the buttons 
	void ButtonSetter(int index, GameObject gameButton) {
		gameButton.GetComponent<Image>().color = buttonSettings[index].normalColor;
		gameButtons.Add(gameButton);
    }
	

	
	// Play the audio upon button press
	public void PlayAudio(int index) {
		buttonSounds[index].Play();
	
    }
	
	
	// Call to show a BLEEP. 
	public void Bleep (int index) {
		LeanTween.value(gameButtons[index], buttonSettings[index].normalColor, buttonSettings[index].highlightColor, 0.25f).setOnUpdate((Color color) => {
			gameButtons[index].GetComponent<Image>().color = color;
		});

        LeanTween.value(gameButtons[index], buttonSettings[index].highlightColor, buttonSettings[index].normalColor, 0.25f)
            .setDelay(0.5f)
            .setOnUpdate((Color color) => {
                gameButtons[index].GetComponent<Image>().color = color;
            });

        PlayAudio(index);
	
	}
	
	// Store the given color into the list of bleeps to be pressed. 
	public void StoreBleep(int index) {
		Bleep(index);
		bleeps.Add(index);
		bleepCount++;
    }
	
	
	// Press the given button. 
	public void MemoryInput(int index) {
		
		Bleep(index);
		playerBleeps.Add(index);
		
		if(bleeps[playerBleeps.Count - 1] != index) {
			Missed();
			return;
		}

		if(bleeps.Count == playerBleeps.Count) {
			Success();
		}
	}
	
	void Update() {
        if(!inputEnabled)
        {
            return;
        }
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			MemoryInput(0);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			MemoryInput(1);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			MemoryInput(3);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			MemoryInput(2);
		}

	}
	
    public void enableInput()
    {
        inputEnabled = true;
    }

    public void disableInput()
    {
        inputEnabled = false;
    }


    void Success() {
		// handle success
	}
	
	void Missed() {
		inputEnabled = false;
	}
	
}
