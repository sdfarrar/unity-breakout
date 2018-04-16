using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;

	private Paddle paddle;

	// Use this for initialization
	void Start () {
		paddle = player.GetComponent<Paddle>();
		paddle.hasControl = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnLevelLoaded(){
		paddle.hasControl = true;
	}
}
