using UnityEngine;
using System.Collections;

public class Variables : MonoBehaviour {
	private int playerLives = 3;
	private int score = 0;
	private CharacterControl.ChState playerState = CharacterControl.ChState.normal;
	private int playerHealth = 10;
	private int levelPass = 0;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		SetPlayerTrans(CharacterControl.ChState.normal);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int GetPlayerLives() {
		return playerLives;
	}
	
	public void SetPlayerLives(int num) {
		playerLives = num;
	}
	
	public void PlayerGainLife(int num) {
		playerLives += num;
	}
	
	public void PlayerLoseLife() {
		PlayerGainLife(-1);
	}
	
	public void GainScore(int num) {
		score += num;
	}
	
	public int GetScore() {
		return score;
	}
	
	public void SetPlayerTrans(CharacterControl.ChState state) {
		playerState = state;
	}
	
	public CharacterControl.ChState GetPlayerState() {
		return playerState;
	}
	
	public void SetPlayerHP(int num) {
		playerHealth = num;
	}
	
	public int GetPlayerHP() {
		return playerHealth;
	}
	
	public void PassLevel(int level) {
		levelPass = level;
	}
	
	public int GetLevelPass() {
		return levelPass;
	}
}
