using UnityEngine;
using System.Collections;

public class NewLevel : MonoBehaviour {
	
	public int nextLevel;
	public GameObject triggeredBy;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.name == triggeredBy.transform.name) {
			gameManager.PlayerPassLevel(nextLevel-1);
			//string lv = "Level" + nextLevel.ToString();
			Application.LoadLevel("LevelMenu");
		}
	}
}
