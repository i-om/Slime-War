using UnityEngine;
using System.Collections;

public class Grass_Slime : MonoBehaviour {
	
	public float attackDistance = 20;
	public float attackRate = 1;
	private AIState aiState;
	///public float jumpSpeed = 100.0f;
	private float lastFired;
	
	public Material faceLeft;
	public Material faceRight;
	public GameObject grass;
	public GameObject player;
	public GameObject weed;
	public GameObject weed_spawn_point;
	public enum AIState
	{
		Idle, 
		Attacking
	}
	
	// Use this for initialization
	void Start () {
		
		SetAIState(AIState.Idle);
		lastFired = Time.time + 1;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(aiState == AIState.Idle){
			Idle();
		}
		else if(aiState == AIState.Attacking){
			Attacking();
		}	
	}
	private void Attacking(){
		
		if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance){
			if (player.transform.position.z < transform.position.z) {
				transform.eulerAngles = new Vector3(0, 180, 0);
				transform.renderer.material = faceLeft;
			} else 
			{transform.eulerAngles = new Vector3(0, 0, 0);
				transform.renderer.material = faceRight;
			}
			
			if (lastFired <= Time.time - attackRate) {
    			Instantiate(weed, weed_spawn_point.transform.position, weed_spawn_point.transform.rotation);
				lastFired = Time.time;
			}
		}
		//animation.Play("jump_pose");
		//rigidbody.AddForce(Vector3.up*jumpSpeed);
	}
	private void Idle(){
		if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance){
			SetAIState(AIState.Attacking);	
		}
	}
	
	public AIState GetAIState(){
		return aiState;	
	}
	
	public void SetAIState(AIState thisState){
		aiState = thisState;
	}
}
