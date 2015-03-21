using UnityEngine;
using System.Collections;

public class Gas : MonoBehaviour {
	
	public float killTime = 2;
	public float range = 0.5f;
	
	private float spawnTime;
	private GameManager gameManager;
	private GameObject player;
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	    player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 thisSize = transform.localScale;
		transform.localScale = new Vector3(thisSize.x, thisSize.y+range*Time.deltaTime, thisSize.z+range*Time.deltaTime);
		if (Time.time >= spawnTime + killTime) {
			Destroy(gameObject);
		}
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		print(thisObject.transform.name);
		if (thisObject.transform.name == player.transform.name) {
			
			gameManager.PlayerLoseHP(1);
			return;
		}
		if (thisObject.transform.tag == "Bullet") {
			Destroy(thisObject.gameObject);
		}
	}
}
