using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {
	
	public float walkSpeed;
	public float jumpHeight;
	public Material normalLeft;
	public Material normalRight;
	public Material grassLeft;
	public Material grassRight;
	public Material rockLeft;
	public Material rockRight;
	public Material mushroomLeft;
	public Material mushroomRight;
	
	private Material faceLeft;
	private Material faceRight;
	private Transform myTransform;
	private GameManager gameManager;
	private ConstantForce constantForce;
	//public CharacterController characterController;
	
	public GameObject spawnObject;
	public GameObject bulletObject;
	public GameObject eatingObject;
	public GameObject gasObject;
	public GameObject scoreObject;
	public float shootingRate = 1;
	public float shootingForce = 20;
	public float rockAttackDuration = 0.5f;
	public float rockAttackSpeed = 30;
	
	public AudioClip jumpSE;
	//public AudioClip eatSE;
	public AudioClip hurtSE;
	public AudioClip crushSE;
	
	public float jumpTimeOut = 1;
	private bool jumping = false;
	private bool isMoving = false;
	private bool rockAttack = false;
	private bool eating = false;
	private bool playerGetsHit = false;
	private float  playerFlash;
	private const float playerFlashTime = 3f;
	private float playerFlashStart;
	
	private float jumpBoostTime = -1;
	private float lastShoot = -1;
	
	public enum ChState{
	  normal, grass, rock, mushroom //etc
    }
	
	public enum FaceDirection{
		right, left
	}
	private ChState chState;
	private FaceDirection faceDir;
	private float posX;
	//private ConstantForce constantForce;
	
	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		constantForce = myTransform.constantForce;
		//characterController = GetComponent<CharacterController>();
		faceDir = FaceDirection.right;
		// Find game manager
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		// will be used
		chState = gameManager.GetCharacterState();
		ChTrans(chState);
		posX  = transform.position.x;
		// Reset player position
	}
	
	// Update is called once per frame
	void Update () {
		ResetConstantForce();
		if (gameManager.IsGameOver() != true) {
	    	ReadControl();
	        ApplyJump();
		}
	}
	
	void OnCollisionEnter(Collision thisObject){
		if (thisObject.transform.tag == "Terrain") {
			jumping = false;
			return;
		} else if (rockAttack && (thisObject.transform.tag != "Item" && thisObject.transform.tag != "Undestroyable")) {
			audio.PlayOneShot(crushSE);
			Destroy(thisObject.gameObject);
			gameManager.PlayerGainScore(100);
		}
	}
	
	void ApplyJump() {
		float y = gameObject.transform.position.y;
		float x = gameObject.transform.position.x;
		float z = gameObject.transform.position.z;
		if (jumping) {
			if (JumpReachTop()) {
				if (isMoving) {
					if (faceDir == FaceDirection.left) {
						gameObject.transform.position = new Vector3(x, y+1*Time.deltaTime, z - walkSpeed*Time.deltaTime);
					} else {
						gameObject.transform.position = new Vector3(x, y+1*Time.deltaTime, z + walkSpeed*Time.deltaTime);
					}
					return;
				}
				gameObject.transform.position = new Vector3(x, y+1*Time.deltaTime, z);
				return;
			} else {
				if (isMoving) {
					if (faceDir == FaceDirection.left) {
						gameObject.transform.position = new Vector3(x, y+jumpHeight*Time.deltaTime, z - walkSpeed*Time.deltaTime);
					} else {
						gameObject.transform.position = new Vector3(x, y+jumpHeight*Time.deltaTime, z + walkSpeed*Time.deltaTime);
					}
					return;
				}
				gameObject.transform.position = new Vector3(x, y+jumpHeight*Time.deltaTime, z);
			}
		}
	}
	
	void ResetConstantForce() {
		Vector3 pos = gameObject.transform.position;
		float speed;
		if (playerGetsHit) {
			Vector3 size = gameObject.transform.lossyScale;
			playerFlashStart += 1;
			if (playerFlashStart % 10 > 4) {
				transform.localScale = new Vector3(size.x, size.y, 0);
			} else {
				transform.localScale = new Vector3(size.x, size.y, 0.8f);
			}
			if (Time.time <= playerFlash+playerFlashTime/2){
				return;
			} else if (Time.time >= playerFlash+playerFlashTime){
				transform.localScale = new Vector3(size.x, size.y, 0.8f);
				playerGetsHit = false;
			}
		}
		constantForce.force = new Vector3(0, 0, 0);
		if (Input.GetKey("d") && gameManager.IsGameOver() != true){
			Turn(FaceDirection.right);
			isMoving = true;
			return;
		} else if (Input.GetKey("a") && gameManager.IsGameOver() != true) {
			Turn(FaceDirection.left);
			isMoving = true;
			return;
		} else if (rockAttack){
			if (Time.time >= lastShoot + rockAttackDuration) {
				isMoving = false;
				rockAttack = false;
				return;
			} else {
				float forward;
				if (faceDir == FaceDirection.left) {
					forward = -rockAttackSpeed*Time.deltaTime;
				} else {
					forward = rockAttackSpeed*Time.deltaTime;
				}
				gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z+forward);
				isMoving = true;
				return;
			} 
		} else {
			if (jumping) {
				return;
			} else {
		    isMoving = false;
			}
		}
	}
	
	void ReadControl() {
		if (playerGetsHit && Time.time <= (playerFlash + playerFlashTime)/2) {
			return;
		}
		if (gameManager.GameIsPause()){
			return;
		}
		if (rockAttack) {
			return;
		}
		// if w key pressed
		if (Input.GetKey("w")){
			if (jumping) {
				//return;
			} else {
     			Jump(jumpHeight);
				//return;
			}
		}
		
		// if d key pressed
		if (Input.GetKey("d")) {
			// move right
			if (faceDir != FaceDirection.left) {
				Turn(FaceDirection.right);
			}
			Move(walkSpeed, FaceDirection.right);
			//return;
		} 
		// if a key pressed
		if (Input.GetKey("a")) {
			// Move left
			if (faceDir != FaceDirection.right) {
				Turn(FaceDirection.left);
			}
			Move(walkSpeed, FaceDirection.left);
			//return;
		}
		//isMoving = false;
		
		
		// if j key pressed
		if (Input.GetKey("j")) {
			Shoot();
			//return;
		}
		
		// if k key pressed
		if (Input.GetKey("k")) {
			ChTrans(ChState.normal);
		}
	}
		
	void Move(float speed, FaceDirection dir){
		if (jumping) {
			return;
		}
		Vector3 force = gameObject.transform.position;
		faceDir = dir;
		isMoving = true;
		if (faceDir == FaceDirection.right) {
		    gameObject.transform.position = new Vector3(force.x, force.y,force.z + speed*Time.deltaTime);
		} else {
			gameObject.transform.position = new Vector3(force.x, force.y,force.z - speed*Time.deltaTime);
		}
	}
	
	void Turn(FaceDirection dir){
		Vector3 v = transform.position;
		if (dir == FaceDirection.left) {
    		spawnObject.transform.position = new Vector3 (v.x, v.y+0.1f, v.z - 0.8f); 
			spawnObject.transform.eulerAngles = new Vector3(0, 180, 0);
			//gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
			gameObject.renderer.material = faceLeft;
		} else {
			spawnObject.transform.position = new Vector3 (v.x, v.y+0.1f, v.z + 0.8f);
			spawnObject.transform.eulerAngles = new Vector3 (0, 0, 0);
			//gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
			gameObject.renderer.material = faceRight;
		}
		faceDir = dir;
	}
		
	void Jump(float height){
		audio.PlayOneShot(jumpSE);
		Vector3 pos = gameObject.transform.position;
		gameObject.transform.position = new Vector3(pos.x, pos.y + jumpHeight*Time.deltaTime, pos.z);
		jumpBoostTime = Time.time;
		jumping = true;
	}
	
	void Shoot(){
		if (playerGetsHit && Time.time <= (playerFlash + playerFlashTime)) {
			return;
		}
		GameObject bullet = null;
		switch (chState) {
		case ChState.normal:
			bullet = eatingObject;
			if (jumping) {return;}break;
		case ChState.grass:
			bullet = bulletObject;break;
		case ChState.rock:
			bullet = null;break;
		case ChState.mushroom:
			bullet = gasObject;break;
		default:
			break;
		}
		if ((lastShoot <= Time.time - shootingRate)) {
			if (bullet != null) {
				
            		Instantiate(bullet, spawnObject.transform.position, spawnObject.transform.rotation);
				
			} else if (rockAttack == false && jumping == false){
				rockAttack = true;
			}
	    	lastShoot = Time.time;
		}
		
	}
	
	public void ChTrans(ChState state){
		switch (state) {
		case ChState.grass:
			//transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			chState = ChState.grass;
			faceLeft = grassLeft;
			faceRight = grassRight;
			break;
		case ChState.rock:
			//transform.localScale = new Vector3(0.001f, 1, 1);
			chState = ChState.rock;
			faceLeft = rockLeft;
			faceRight = rockRight;
			break;
		case ChState.normal:
			//transform.localScale = new Vector3(0.001f, 0.8f, 0.8f);
			chState = ChState.normal;
			faceLeft = normalLeft;
			faceRight = normalRight;
			break;
		case ChState.mushroom:
			chState = ChState.mushroom;
			faceLeft = mushroomLeft;
			faceRight = mushroomRight;
			break;
		default:break;
		}
		Turn(faceDir);
	}
	
	bool JumpReachTop(){
		return Time.time - jumpBoostTime >= jumpTimeOut;
	}
	
	public FaceDirection Facing() {
		return faceDir;
	}
	
	public bool PlayerRockAttack() {
		return rockAttack;
	}
	
	public bool IsEating() {
		return eating;
	}
	
	public ChState PlayerState() {
		return chState;
	}
	
	public void PlayerGetHit() {
		audio.PlayOneShot(hurtSE);
		playerGetsHit = true;
		playerFlash = Time.time;
		playerFlashStart = 0;
	}
	
	public bool PlayerIsHit() {
		return playerGetsHit;
	}
}
