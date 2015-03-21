using UnityEngine;
using System.Collections;

public class SFXKill : MonoBehaviour {
	
	public float killTime;
	private GameManager gameManager;
	private GameObject isTriggeredBy;
	
	// Use this for initialization
	void Start () {
		killTime = Time.time + killTime;
		isTriggeredBy = GameObject.FindGameObjectWithTag("Player");
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= killTime){
			Destroy (this.gameObject);
		}
	}
	
	private void OnCollisionEnter(Collision collidedObject)
	{
		//restart the level if the triggered object touches this trigger
		if (isTriggeredBy != null)	//check that we have an object that will trigger us
		{
			if (collidedObject.transform.name == isTriggeredBy.name && gameManager.PlayerRockAttack() != true)	//check if the object that has collided is the object we want to activate the trigger
			{
				gameManager.PlayerLoseHP(3);
				Destroy(gameObject);
			}
		}
		
		else
		{//print a warning message
			Debug.LogError("Please place a game object from your scene into the 'is TriggeredBy' variable in the trigger script attatched to: " + this.transform.name);	
		}
	}
}
