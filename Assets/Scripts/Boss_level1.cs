using UnityEngine;
using System.Collections;

public class Boss_level1 : MonoBehaviour {
	
	private AIState aiState;
	private float lastFired;
	
	public int attackDistance = 10;
	public int movingDistance = 50;
	public float attackRate = 2;
	public GameObject boss;
	public GameObject player;
	public GameObject knife;
	public GameObject knifeSpawnPoint;
	
	public enum AIState{
		Idle,
		Moving,
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
		
		else if(aiState == AIState.Moving){
			Moving();
		}
		
		else if(aiState == AIState.Attacking){
			Attacking();
		}
	}
	
	private void Idle(){
		if(Vector3.Distance(player.transform.position, transform.position) < movingDistance){
			SetAIState(AIState.Moving);
		}
	}
	
	private void Moving(){
		if(Vector3.Distance(player.transform.position, transform.position) < attackDistance){
			SetAIState(AIState.Attacking);
		}
	} 
	
	private void Attacking(){
		
		if (player.transform.position.z < transform.position.z){
			transform.eulerAngles = new Vector3(0,180,0);
		}
		
		else {
			transform.eulerAngles = new Vector3(0,0,0);
		}
		
		if (lastFired <= Time.time - attackRate) {
			Instantiate(knife,knifeSpawnPoint.transform.position, knifeSpawnPoint.transform.rotation);
			lastFired = Time.time;
		}
	}
	
	public AIState GetAIState(){
		return aiState;
	}
	
	public void SetAIState(AIState thisState){
		aiState = thisState;
	}
}
