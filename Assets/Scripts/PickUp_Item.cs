using UnityEngine;
using System.Collections;

public class PickUp_Item : MonoBehaviour {
	
	private GameManager gameManager;
	public int rewardPoint = 100;
	public int rewardHealth = 0;
	public int rewardLives = 0;
	public AudioClip se;
	
	// Use this for initialization
	void Start () {
	    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.tag == "Player") {
			audio.PlayOneShot(se);
			gameManager.PlayerGainHP(rewardHealth);
			gameManager.PlayerGainScore(rewardPoint);
			gameManager.PlayerGainLife(rewardLives);
			Destroy(gameObject);
		}
	}
}
