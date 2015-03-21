using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Vector3 patrolstartPoint;
		public Vector3 patrolEndPoint;
		private Vector3 currentPosition;
		private Vector3 lastPosition;
	
	private enum State {
		patrol,
		idle,
		attack,
		die,}
	private State state;
	// Use this for initialization
	void Start () {
		state = State.idle;
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		switch (state){
		case State.patrol: Patrol();break;
		}
	}
	void SetState(State currentState){
		state = currentState;
	}
	
	void Patrol(){
		//if currentPos 在 lastPos 左边 and currentPos 在 goalPos 右边{
		//MoveTo(patrolEndPoint);
	    //} else {
		// MoveTo(patrolStartPoint}
		// RecordPostion();
	}
	
	public Vector3 GetCurrentPostion(){
		currentPosition = this.transform.position;
	  return currentPosition;
	}
	
	public void MoveTo(Vector3 goal){
	}
	
    void RecordPostion(){
		lastPosition = currentPosition;
	}
}
