using UnityEngine;
using System.Collections;

public class PlayerGas : MonoBehaviour {

	public float killTime = 2;
	public float range = 0.5f;
	
	private float spawnTime;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	    
	}
	
	// Update is called once per frame
	void Update () {
		constantForce.force = new Vector3(0, 0, 0);
		if (Time.time >= spawnTime + killTime) {
			Destroy(gameObject);
		}
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.tag == "Bullet" || thisObject.transform.tag == "Grass" || thisObject.transform.tag == "Rock" || thisObject.transform.tag == "Mushroom") {
			Destroy(thisObject.gameObject);
			if (thisObject.transform.tag != "Bullet") {
				gameManager.PlayerGainScore(100);
			}
		}
	}
}
