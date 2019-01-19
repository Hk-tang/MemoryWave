using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simonSaysManager : MonoBehaviour
{
 

	public GameObject gameButtonPrefab;

    public List<ButtonSetting> buttonSettings;

    public Transform gameFieldPanelTransform;

	
	public Sprite redButton;
	public Sprite blueButton;
	public Sprite yellowButton;
	public Sprite greenButton;
	
	
    List<GameObject> gameButtons;

    int bleepCount = 3;

    List<int> bleeps;
    List<int> playerBleeps;

    bool inputEnabled = false;

	
	
	
	// Calls to init button objects  
    void Start() {
        gameButtons = new List<GameObject>();
	
        CreateGameButton(0, new Vector3(5, -70), redButton);
        CreateGameButton(1, new Vector3(-10, -50), blueButton);
        CreateGameButton(2, new Vector3(30, -55), yellowButton);
        CreateGameButton(3, new Vector3(4, -35), greenButton);
		
    }

	
	// actually create the buttons 
	void CreateGameButton(int index, Vector3 position, Sprite buttonColor) {
	GameObject gameButton = Instantiate(gameButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;

	gameButton.transform.SetParent(gameFieldPanelTransform);
	gameButton.transform.localPosition = position;
	gameButton.transform.localScale = new Vector3(8.0f, 30.0f, 2.0f);
	
	
	gameButton.GetComponent<Image>().sprite = buttonColor;
	gameButton.GetComponent<Image>().color = buttonSettings[index].normalColor;

	gameButtons.Add(gameButton);
    }
	

	
	// Play the audio upon button press
	public void PlayAudio(int index) {
	float length = 0.5f;
	float frequency = 0.001f * ((float)index + 1f);

	AnimationCurve volumeCurve = new AnimationCurve(new Keyframe(0f, 1f, 0f, -1f), new Keyframe(length, 0f, -1f, 0f));
	AnimationCurve frequencyCurve = new AnimationCurve(new Keyframe(0f, frequency, 0f, 0f), new Keyframe(length, frequency, 0f, 0f));

	LeanAudioOptions audioOptions = LeanAudio.options();
	audioOptions.setWaveSine();
	audioOptions.setFrequency(44100);

	AudioClip audioClip = LeanAudio.createAudio(volumeCurve, frequencyCurve, audioOptions);

	LeanAudio.play(audioClip, 0.5f);
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
	
	
	
	void Success() {
		// handle success
	}
	
	void Missed() {
		inputEnabled = false;
	}
	
}
