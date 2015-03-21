using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject character;
	public GameObject StartPosition;
	public SmoothFollow camera;
	public GameObject[] scores = new GameObject[6];
	
	private CharacterControl characterControl;
	private GameObject gameDataObject;
	private Variables gameData;
	private int playerHP;
	private int playerLife;
	
	private bool restart = false;
	private bool gameOver = false;
	private bool gamePause = false;
	
	// Use this for initialization
	void Start () {
		characterControl = character.GetComponent<CharacterControl>();
	    gameDataObject = GameObject.FindGameObjectWithTag("GameData");
		gameData = gameDataObject.GetComponent<Variables>();
		playerHP = 10;
		playerLife = gameData.GetPlayerLives();
		characterControl.ChTrans(gameData.GetPlayerState());
		//DontDestroyOnLoad(gameData);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		Rect lifeBox = new Rect(20, 20, 100, 50);
		Rect hpBox = new Rect(50, 40, 100, 40);
		Rect scoreBox = new Rect(Screen.width - 180, 20, 150, 30);
		string healthText = "Helth: " + playerHP.ToString();
		string lifeText = "Life: " + playerLife.ToString();
		string scoreText = "Score: " + gameData.GetScore().ToString();
		
		GUI.Box(lifeBox, lifeText);
		GUI.Label(hpBox, healthText);
		GUI.Box(scoreBox, scoreText);
		
		if (restart) {
			Rect box = new Rect(Screen.width/2-100, Screen.height/2-50, 200, 100);
			GUI.Box(box, "Life: " + playerLife.ToString());
			if (GUI.Button(new Rect(box.x + 50, box.y + 50, 100, 30), "RESTART")) {
				Application.LoadLevel("LevelMenu");
			}
		}
		
		if (gameOver) {
			Rect box = new Rect(Screen.width/2-100, Screen.height/2-50, 200, 100);
			GUI.Box(box, "GAMEOVER");
			if (GUI.Button(new Rect(box.x + 50, box.y + 50, 100, 30), "QUIT")) {
				Destroy(gameDataObject);
				Application.LoadLevel("Title");
			}
		}
		
	}
	
	// set character state
	void SetCharacterState(CharacterControl.ChState state) {
		gameData.SetPlayerTrans(state);
	}
	
	// Get character state
	public CharacterControl.ChState GetCharacterState() {
		return gameData.GetPlayerState();
	}
	
	// get character facedirection
	public CharacterControl.FaceDirection GetFacing() {
		return characterControl.Facing();
	}
	
	public bool IsGameOver() {
		return gameOver;
	}
	
	public void PlayerGainHP(int hp){
		if (hp > 0) {
			Instantiate(scores[5], characterControl.scoreObject.transform.position, characterControl.scoreObject.transform.rotation);
		}
		
		playerHP += hp;
		if (playerHP > 10) {
			playerHP = 10;
		}
	}
	public void PlayerLoseHP(int hp){
		if (characterControl.PlayerIsHit()){
			return;
		}
		PlayerGainHP(-hp);
		PlayerGetsHit();
		if (playerHP <= 0) {
			PlayerLoseLife();
		}
	}
	
	public void PlayerGainLife(int num){
		if (num > 0) { 
	    	Instantiate(scores[4], characterControl.scoreObject.transform.position, characterControl.scoreObject.transform.rotation);
		}
		playerLife += num;
	}
	
	public void PlayerLoseLife() {
		gameData.PlayerLoseLife();
		playerLife -= 1;
		camera.target = null;
		if (playerLife < 1) {
			gameOver = true;
		} else {
			playerHP = 10;
			restart = true;
			//Application.LoadLevel(Application.loadedLevel);
		}
		
	}
	
	public void PlayerGainScore(int num) {
		GameObject sc;
		switch (num){
		case 0:   return;break;
		case 50:  sc = scores[0];break;
		case 100: sc = scores[1];break;
		case 150: sc = scores[2];break;
		case 2000:sc = scores[3];break;
		default:  sc = scores[0];break;
		}
		Instantiate(sc, characterControl.scoreObject.transform.position, characterControl.scoreObject.transform.rotation);
		gameData.GainScore(num);
	}
	
	public void PlayerAte(CharacterControl.ChState state){
		characterControl.ChTrans(state);
		PlayerGainScore(150);
	}
	
	public bool PlayerEating() {
		return characterControl.IsEating();
	}
	
	public bool PlayerRockAttack() {
		return characterControl.PlayerRockAttack();
	}
	public void PlayerPassLevel(int level){
		gameData.PassLevel(level);
		gameData.SetPlayerLives(playerLife);
		gameData.SetPlayerTrans(characterControl.PlayerState());
	}
	
	public void GamePause() {
		gamePause = true;
	}
	
	public void GameResume() {
		gamePause = false;
	}
	
	public bool GameIsPause() {
		return gamePause;
	}
	
	public void PlayerGetsHit(){
		characterControl.PlayerGetHit();
	}
}
