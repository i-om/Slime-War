using UnityEngine;
using System.Collections;

public class ScorePup : MonoBehaviour {
	
	const float killTime = 0.5f;
	private float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startTime + killTime){
			Destroy(gameObject);
		} else {
			transform.position = new Vector3(transform.position.x, transform.position.y+0.5f*Time.deltaTime, transform.position.z);
		}
	}
}
