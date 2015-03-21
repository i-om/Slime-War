using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	
	//private bool menuOn = false;
	public GameManager gameManager;
	public Texture tutorial;
	
	private bool tutorialOn = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI (){
		Rect menuButton = new Rect(50, Screen.height-100, 100, 50);
		Rect menuResume = new Rect(Screen.width/2-100, Screen.height/2-75, 200, 50);
		Rect menuHelp = new Rect(Screen.width/2-100, Screen.height/2-25, 200, 50);
		Rect menuRestart = new Rect(Screen.width/2-100, Screen.height/2+25, 200, 50);
		Rect menuTitle = new Rect(Screen.width/2-100, Screen.height/2+75, 200, 50);
		Rect helpPic = new Rect(Screen.width/2-320, Screen.height/2-240-30, 640, 480);
		Rect helpBox = new Rect(Screen.width/2-320, Screen.height/2-240-30, 640, 540);
		Rect helpButton = new Rect(Screen.width/2+170, Screen.height/2+180, 100, 50);
		
		//RefreshHealthBar and Score
		
		//Display tutorial
		if (tutorialOn) {
			GUI.Box(helpBox, "");
			GUI.DrawTexture(helpPic, tutorial);
			if (GUI.Button(helpButton, "Back")) {
				tutorialOn = false;
			}
			return;
		}
		
		if (gameManager.GameIsPause()) {
			if (GUI.Button(menuResume, "Resume")) {
				gameManager.GameResume();
			}
			if (GUI.Button(menuHelp, "Help")) {
				//turn on tutorial
				tutorialOn = true;
				GUI.DrawTexture(new Rect(0, 0, 640, 480), tutorial);
			}
			if (GUI.Button(menuRestart, "Restart")) {
				RestartLevel();
			}
			if (GUI.Button(menuTitle, "Title")) {
				Application.LoadLevel("Title");
			}
		} else if (GUI.Button(menuButton, "Menu")) {
			gameManager.GamePause();
		}
	}
	
	void RestartLevel(){
		//restore gameData
		Application.LoadLevel(Application.loadedLevel);
	}
}
