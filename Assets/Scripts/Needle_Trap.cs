using UnityEngine;
using System.Collections;

public class Needle_Trap : MonoBehaviour {
	//Vector3 thisPosition;
	Vector3 minusSize;
	//Vector3 maxSize;
	bool raising = true;
	private GameManager gameManager;
	public float maximuneSize;
	private Vector3 normalSize;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	    Vector3 thisSize = gameObject.transform.localScale;
	    minusSize = gameObject.transform.localScale;
		//maxSize = new Vector3(thisSize.x, thisSize.y*maximuneSize, thisSize.z*maximuneSize);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 thisPos = gameObject.transform.localScale;
		Vector3 thisPosition = gameObject.transform.position;
		if (raising) {
			gameObject.transform.localScale = new Vector3(thisPos.x, thisPos.y+Time.deltaTime, thisPos.z);
			gameObject.transform.position = new Vector3(thisPosition.x, thisPosition.y+Time.deltaTime/2, thisPosition.z);
			if (gameObject.transform.localScale.y >= minusSize.y*maximuneSize) {
				raising = false;
			} 
		} else {
			gameObject.transform.localScale = new Vector3(thisPos.x, thisPos.y-Time.deltaTime, thisPos.z);
			gameObject.transform.position = new Vector3(thisPosition.x, thisPosition.y-Time.deltaTime/2, thisPosition.z);
			if (gameObject.transform.localScale.y <= minusSize.y) {
				raising = true;
			}
		}
	}
	
	void OnCollisionEnter (Collision thisObject) {
		if (thisObject.transform.tag == "Player" && gameManager.PlayerRockAttack() == false) {
			gameManager.PlayerLoseHP(3);
		}
	}
}
