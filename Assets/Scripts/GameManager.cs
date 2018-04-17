using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject brickManager;

	private Paddle _paddle;
	private BrickManager _brickManager;

	void Start () {
		_paddle = player.GetComponent<Paddle>();
		_paddle.hasControl = false;
		_brickManager = brickManager.GetComponent<BrickManager>();
	}
	
	void LateUpdate () {
		if(!_brickManager.HasBricksLeft()){
			Debug.Log("No bricks left");
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; // TODO remove when we load new levels
#else
			Application.Quit();
#endif
		}
	}

	public void OnLevelLoaded(){
		_paddle.hasControl = true;
	}
}
