using UnityEngine;
using System.Collections;

public class LevelMenu : MonoBehaviour {
	
	private Variables gameData;
	public AudioClip clickVoice;
	public AudioClip cancleVoice;
	
	// Use this for initialization
	void Start () {
		gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<Variables>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		int levelPass = gameData.GetLevelPass();
		Rect b1 = new Rect(Screen.width/2-150, Screen.height/2-100, 300, 50);
		Rect b2 = new Rect(Screen.width/2-150, Screen.height/2-25, 300, 50);
		Rect b3 = new Rect(Screen.width/2-150, Screen.height/2+50, 300, 50);
		Rect b4 = new Rect(Screen.width/2-150, Screen.height/2+125,300, 50);
		string b1string = "LEVEL 1";
		string b2string;
		string b3string;
		string b4string = "RETURN";
		if (levelPass > 0) {
			b2string = "LEVEL 2";
			if (levelPass > 1) {
				b3string = "LEVEL 3";
			} else {
				b3string = "LOCKED";
			}
		} else {
			b2string = "LOCKED";
			b3string = "LOCKED";
		}
		
		if (GUI.Button(b1, b1string)) {
			Application.LoadLevel("Level1");
			audio.PlayOneShot(clickVoice);
		}
		if (GUI.Button(b2, b2string)) {
			if (levelPass < 1) {
				audio.PlayOneShot(cancleVoice);
				return;
			}
			audio.PlayOneShot(clickVoice);
			Application.LoadLevel("Level2");
		}
		if (GUI.Button(b3, b3string)) {
			if (levelPass < 2) {
				audio.PlayOneShot(cancleVoice);
				return;
			}
			audio.PlayOneShot(clickVoice);
			Application.LoadLevel("Level3");
		}
		if (GUI.Button(b4, b4string)) {
			audio.PlayOneShot(cancleVoice);
			Application.LoadLevel("Title");
		}
		
		if (GUI.Button(new Rect(0, 0, 100, 50), "Cheat")) {
			gameData.PassLevel(2);
		}
	}
}
