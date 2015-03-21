using UnityEngine;
using System.Collections;

public class Bullet_Boss_Up : MonoBehaviour {

	private GameObject player;
	//private Vector3 playerPosition;
	
	private GameObject gameManagerObject;
	private GameManager gameManager;
	private float bornTime;
	private float killTime = 2;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
		bornTime = Time.time;
		constantForce.force = new Vector3(0, 0, -20);

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= bornTime + killTime){
			Destroy(gameObject);
		}
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.tag == "Bullet") {
			return;
		}
		if (thisObject.transform.name == player.transform.name) {
			gameManager.PlayerLoseHP(1);
		}
		Destroy(gameObject);
	}
}
