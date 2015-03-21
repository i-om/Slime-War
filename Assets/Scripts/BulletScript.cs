using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	private Vector3 force;
	private GameObject player;
	private GameObject gameManagerObject;
	private GameManager gameManager;
	private AudioClip shoot;
	
	private GameObject gameVariables;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		CharacterControl playerControl = player.GetComponent<CharacterControl>();
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
	    gameManager = gameManagerObject.GetComponent<GameManager>();
		float speed = playerControl.shootingForce;
		if (gameManager.GetFacing() == CharacterControl.FaceDirection.right) {
			force = new Vector3(0, 0, speed);
		} else {
			force = new Vector3(0, 0, -speed);
		}
		this.constantForce.force = force;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision thisObject)
	{
		
		if (thisObject.transform.tag == "Grass" || thisObject.transform.tag == "Rock" || thisObject.transform.tag == "Mushroom")
		{
			Destroy(thisObject.gameObject);
			gameManager.PlayerGainScore(100);
			
		}
		if (thisObject.transform.tag == "Player")
		{
			return;
		}
		Destroy(gameObject);
	}

}
