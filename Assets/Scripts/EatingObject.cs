using UnityEngine;
using System.Collections;

public class EatingObject : MonoBehaviour {

	public float killTime = 1;
	public AudioClip sound;
	
	private float timer;
	private GameObject player;
	private GameObject gameManagerObject;
	private GameManager gameManager;
	
	private GameObject gameVariables;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		CharacterControl playerControl = player.GetComponent<CharacterControl>();
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
	    gameManager = gameManagerObject.GetComponent<GameManager>();
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timer + killTime) {
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision thisObject)
	{
		print(thisObject.transform.tag);
		
		if (thisObject.transform.tag == "Grass")
		{
			audio.PlayOneShot(sound);
			gameManager.PlayerAte(CharacterControl.ChState.grass);
			Destroy(thisObject.gameObject);
			//return;
		}
		
		if (thisObject.transform.tag == "Rock")
		{
			audio.PlayOneShot(sound);
			gameManager.PlayerAte(CharacterControl.ChState.rock);
			Destroy(thisObject.gameObject);
			//return;
		}
		
		if (thisObject.transform.tag == "Mushroom") {
			audio.PlayOneShot(sound);
			gameManager.PlayerAte(CharacterControl.ChState.mushroom);
			Destroy(thisObject.gameObject);
		}
		Destroy(gameObject);
	}
}
