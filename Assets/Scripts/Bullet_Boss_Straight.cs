using UnityEngine;
using System.Collections;

public class Bullet_Boss_Straight : MonoBehaviour {

	private GameObject player;
	
	private GameObject gameManagerObject;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
		
			//constantForce.force = new Vector3(0, 0, -20);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.tag == "Bullet") {
			return;
		}
		if (thisObject.transform.name == player.transform.name) {
			gameManager.PlayerLoseHP(1);
		}
		print(thisObject.transform.tag);
		Destroy(gameObject);
	}
}

