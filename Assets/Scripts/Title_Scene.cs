using UnityEngine;
using System.Collections;

public class Title_Scene : MonoBehaviour {
	public Variables GameData;
	public AudioClip clickVoice;
	public AudioClip cancleVoice;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		
		Rect startButton = new Rect(Screen.width/2-50, Screen.height/4*3, 100, 30);
		Rect closeButton = new Rect(Screen.width/2-50, Screen.height/4*3+40, 100, 30);
		
		if (GUI.Button(startButton, "PLAY")) {
			audio.PlayOneShot(clickVoice);
			Application.LoadLevel("LevelMenu");
		}
		
		if (GUI.Button(closeButton, "EXIT")) {
			audio.PlayOneShot(cancleVoice);
			Application.Quit();
		}
	}
}
