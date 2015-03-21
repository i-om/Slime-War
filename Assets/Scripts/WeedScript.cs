using UnityEngine;
using System.Collections;

public class WeedScript : MonoBehaviour {
	
	private GameObject player;
	
	private GameObject gameManagerObject;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
		if (player.transform.position.z > gameObject.transform.position.z) {
			constantForce.force = new Vector3(0, 0, 10);
		} else {
			constantForce.force = new Vector3(0, 0, -10);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.name == player.transform.name) {
			gameManager.PlayerLoseHP(1);
		}
		Destroy(gameObject);
	}
}
