using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 playerPosition;
	public AIState aiState;
	private float lastFired;
	//private float duration = 5f;
	private float startTime;
	private CharacterControl.FaceDirection dir = CharacterControl.FaceDirection.left;
	private bool moveToRight;
	//private Transform myTransform;
	//private ConstantForce constantForce;
	public GameManager gameManager;
	
	public int attackDistance = 10;
	public int movingDistance = 50;
	public float attackRate = 2;
	
	public float speed = 1;
	public GameObject boss;
	public GameObject player;
	public GameObject endpoint;
	public GameObject startPoint;
	public GameObject bulletDown;
	public GameObject bulletUp;
	public GameObject bulletStraight;
	public GameObject bulletSpawnPoint;
	public GameObject grassSlime;
	public GameObject slimePoint;
	public Material faceLeft;
	public Material faceRight;
	
	public enum AIState {
		
		Idle,
		Moving,
		Attacking
	}
	
	public int bossHealth = 10;
	
	private int attackTiming;
	private int hitTime;
	private float hitStartTime;
	private bool grassSpawn = false;
	private bool getsHit = false;
	
	public enum FaceDirection {
		
		right, left
	}
	// Use this for initialization

	void Start () {
		
		//startTime = Time.time;
		startPosition = startPoint.transform.position;
		endPosition = endpoint.transform.position;
		SetAIState(AIState.Idle);
		lastFired = Time.time + 1;
	}
	
	// Update is called once per frame
	
	void Update () {
	    if (bossHealth <= 0) {
			return;
		}
		if (getsHit) {
			if (hitTime % 8 > 3) {
				transform.localScale = new Vector3(0.001f, 3, 0);
			} else {
				transform.localScale = new Vector3(0.001f, 3, 3);
			}
			hitTime += 1;
			hitTime %= 8;
			if (Time.time >= hitStartTime + 3) {
				getsHit = false;
				transform.localScale = new Vector3(0.001f, 3, 3);
			}
		}
		
		//ResetConstantForce();
	
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
	
	void OnGUI(){
		string hpText = "Boss HP: " + bossHealth.ToString();
		GUI.Box(new Rect(Screen.width/2-100, 50, 200, 25), hpText);
		
		if (bossHealth <= 0) {
			Rect box = new Rect(Screen.width/2-100, Screen.height/2-50, 200, 100);
			GUI.Box(box, "YOU WIN!");
			if (GUI.Button(new Rect(box.x + 50, box.y + 50, 100, 30), "OK")) {
				Application.LoadLevel("LevelMenu");
			}
		}
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.name == player.transform.name) {
			if (gameManager.PlayerRockAttack()) {
				if (getsHit != true){
    				TakeDamage();
				}
			} else {
    			gameManager.PlayerLoseHP(2);
			}
		}
		if (thisObject.transform.tag == "PlayerBullet" && getsHit != true) {
			TakeDamage();
		}
		
	}
	
	private void Idle () {
		
		if (Vector3.Distance(player.transform.position, transform.position) < movingDistance) {
			SetAIState(AIState.Moving);
		}
	}
	
	private void Moving(){
		
		if (dir == CharacterControl.FaceDirection.left){
		    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-speed*Time.deltaTime);
			if (transform.position.z <= startPosition.z){
				dir = CharacterControl.FaceDirection.right;
				transform.renderer.material = faceRight;
			}
		}
		if (dir == CharacterControl.FaceDirection.right) {
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);
			if (transform.position.z >= endPosition.z){
				dir = CharacterControl.FaceDirection.left;
				transform.renderer.material = faceLeft;
			}
		}

		if(Vector3.Distance(player.transform.position, transform.position) < attackDistance){
			SetAIState(AIState.Attacking);
		}
	} 
	
	
	private void Attacking (){
		
		if (dir == CharacterControl.FaceDirection.left){
			//if (lastFired <= Time.time - attackRate) 
			if (attackTiming == 0) {
				if (grassSpawn){
				Instantiate(grassSlime, slimePoint.transform.position, slimePoint.transform.rotation);
					grassSpawn = false;
				}
				Instantiate (bulletUp,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			}
			if (attackTiming == 20) {
				//Instantiate (bulletStraight,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			}
			if (attackTiming == 60) {
				//Instantiate (bulletDown,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			}
			attackTiming += 1;
			attackTiming %= 30;
	
			//lastFired = Time.time;
		    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-speed*Time.deltaTime);
			if (transform.position.z <= startPosition.z){
				dir = CharacterControl.FaceDirection.right;
				transform.renderer.material = faceRight;
			}
		}
		if (dir == CharacterControl.FaceDirection.right) {
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+speed*Time.deltaTime);
			if (transform.position.z >= endPosition.z){
				dir = CharacterControl.FaceDirection.left;
				transform.renderer.material = faceLeft;
				grassSpawn = true;
			}
		}
		
		
		
	/*	if (player.transform.position.z < transform.position.z) {
			transform.eulerAngles = new Vector3(0,180,0);
		}
		
		else {
			transform.eulerAngles = new Vector3(0,0,0);
		}*/
		
		
	}
		

	

	public AIState GetAIState () {
		
		return aiState;
	}
	
	public void SetAIState (AIState thisState) {
		
		aiState = thisState;
		if (aiState==AIState.Attacking){
			attackTiming = 0;
			
		}
			
	}
	
	private void TakeDamage(){
		if (dir == CharacterControl.FaceDirection.left){
			return;
		}
		bossHealth -= 1;
		getsHit = true;
		hitTime = 0;
		hitStartTime = Time.time;
		if (bossHealth <= 0) {
			gameObject.transform.localScale = new Vector3(0, 0, 0);
			gameManager.PlayerGainScore(2000);
		}
	}
		
}
