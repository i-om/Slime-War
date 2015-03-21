using UnityEngine;
using System.Collections;

public class Mush_Slime : MonoBehaviour {
	
	public int attackDistance = 10;
	public AIState aiState;
	public float jumpSpeed = 100.0f;
	private GameObject player;
	public enum AIState
	{
		Idle, 
		Attacking
	}
	
	// Use this for initialization
	void Start () {
		
		SetAIState(AIState.Idle);
	}
	
	// Update is called once per frame
	void Update () {
	
		player = GameObject.FindWithTag("Player");
		if(aiState == AIState.Attacking){
			Attacking();
		}
		else if(aiState == AIState.Idle){
			Idle();
		}	
	}
	private void Attacking(){
		animation.Play("jump_pose");
		rigidbody.AddForce(Vector3.up*jumpSpeed);
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
