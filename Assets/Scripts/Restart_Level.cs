using UnityEngine;
using System.Collections;
/// <summary>
/// Place this script on an object which will act as the trigger point. e.g. a plane below the level or some spikes.
/// This script triggers a level restart if the "isTriggeredBy" game object makes contact with the object this script is attatched to.
/// </summary>
public class Restart_Level : MonoBehaviour {
	public Transform isTriggeredBy;
	
	private GameObject gameManagerObject;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = gameManagerObject.GetComponent<GameManager>();
		isTriggeredBy = GameObject.FindGameObjectWithTag("Player").transform;
		//make sure the user has ticked that this object is set to trigger
		//and that the object does not have a ridgedbody attatched, otherwise trigger can't be used
		if (this.transform.GetComponent<Collider>() == null)
		{
			this.gameObject.AddComponent<MeshCollider>();	
		}
		if ((this.transform.GetComponent<Collider>().isTrigger == false) &&
			(this.transform.GetComponent<Rigidbody>() == false))
			{
				this.transform.GetComponent<Collider>().isTrigger = true;
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnTriggerEnter(Collider triggerObject)
	{//restart the level if the triggered object touches this trigger
		if (isTriggeredBy != null)	//check that we have an object that will trigger us
		{
			if (triggerObject.name == isTriggeredBy.name)	//check if the object that has collided is the object we want to activate the trigger
			{
				if (gameManager.PlayerRockAttack() != true) {
    				gameManager.PlayerLoseLife();	//load the level we are currently playing in	
				} else {
					Destroy(gameObject);
				}
			}
		}
		
		else
		{//print a warning message
			Debug.LogError("Please place a game object from your scene into the 'is TriggeredBy' variable in the trigger script attatched to: " + this.transform.name);	
		}
	}
	
	
	private void OnCollisionEnter(Collision collidedObject)
	{
		//restart the level if the triggered object touches this trigger
		if (isTriggeredBy != null)	//check that we have an object that will trigger us
		{
			if (collidedObject.transform.name == isTriggeredBy.name && gameManager.PlayerRockAttack() != true)	//check if the object that has collided is the object we want to activate the trigger
			{
				gameManager.PlayerLoseLife();	//load the level we are currently playing in		
			}
		}
		
		else
		{//print a warning message
			//Debug.LogError("Please place a game object from your scene into the 'is TriggeredBy' variable in the trigger script attatched to: " + this.transform.name);	
		}
	}
	
}
