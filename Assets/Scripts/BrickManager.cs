using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

	public GameObject brickPrefab;

	private GameObject bricksParent;

	private string map = "#### #### #### ####\n"
						+"   ###  ###  ###";

	// Use this for initialization
	void Start () {
		bricksParent = GameObject.Find("Bricks");
		if(bricksParent==null){
			bricksParent = new GameObject();
			bricksParent.name = "Bricks";
		}

		GenerateLevel();

		bricksParent.transform.position = new Vector2(-8.5f, 4.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateLevel(){
		BoxCollider2D box = brickPrefab.GetComponent<BoxCollider2D>();
		float width = box.size.x * box.transform.localScale.x;
		float height = box.size.y * box.transform.localScale.y;

		Vector2 position = Vector2.zero;
		for(int i=0; i<map.Length; ++i){
			char c = map[i];
			if(c=='#'){
				Instantiate(brickPrefab, position, Quaternion.identity, bricksParent.transform); // fallsthrough
			}
			position.x += width;
			if(c=='\n'){
				position = new Vector2(0f, position.y - height);
			}
		}
	}

}
