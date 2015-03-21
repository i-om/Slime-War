using UnityEngine;
using System.Collections;

public class Shoot_Gun : MonoBehaviour {
	private float lastFired;
	public GameObject bullet;
	public GameObject cannon;
	
	

	// Use this for initialization
	void Start () {
		lastFired = Time.time + 4;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= lastFired){
			lastFired = Time.time + 3;
	
		Instantiate(bullet,cannon.transform.position, cannon.transform.rotation);
			
	}
}
}