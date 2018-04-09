using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

	private GameObject bricksParent;

	private string map = "####  ####  ####  ####";

	// Use this for initialization
	void Start () {
		bricksParent = GameObject.Find("Bricks");
		if(bricksParent==null){
			bricksParent = new GameObject();
			bricksParent.name = "Bricks";
		}

		GenerateLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateLevel(){
		// iterate over string | # = brick
	}

}
