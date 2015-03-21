using UnityEngine;
using System.Collections;

public class Rock_Slime : MonoBehaviour {
	
	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 playerPosition;
	private ConstantForce constantForce;
	private GameObject gameManagerObject;
	private GameManager gamemanager;
	
	public float speed = 1;
	public AIState aiState;
	public GameObject player;
	public GameObject endpoint;
	public GameObject startPoint;
	public Material faceLeft;
	public Material faceRight;
	
	public enum AIState
	{
		Moving,Attacking
	}
	
	private CharacterControl.FaceDirection dir = CharacterControl.FaceDirection.left;
	
	private bool moveToRight;

	// Use this for initialization
	void Start () {
		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
		gamemanager = gameManagerObject.GetComponent<GameManager>();
		transform.renderer.material = faceLeft;
		SetAIState(AIState.Moving);
		startPosition = startPoint.transform.position;
		endPosition = endpoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		playerPosition = player.transform.position;
		
		if (aiState == AIState.Moving){
			Moving();
		}
		else if (aiState == AIState.Attacking){
			Attacking();
		}
		
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.name == player.transform.name && gamemanager.PlayerRockAttack() != true) {
			gamemanager.PlayerLoseHP(1);
		}
	}
	
	private void Attacking (){
		
	}
	
	private void Moving (){
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

	}
	
	public AIState GetAIState(){
		return aiState;	
	}
	
	public void SetAIState(AIState thisState){
		aiState = thisState;
	}
}
