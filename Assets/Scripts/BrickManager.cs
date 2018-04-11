using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

	public GameObject brickPrefab;

	private GameObject bricksParent;
	private List<GameObject> bricks;

	private string map = "#### #### #### ####\n"
						+"   ###  ###  ###";

	void Start () {
		map = "##";//simple map
		Brick.SetBrickManager(this);

		bricksParent = GameObject.Find("Bricks");
		if(bricksParent==null){
			bricksParent = new GameObject();
			bricksParent.name = "Bricks";
		}

		GenerateLevel();
		bricksParent.transform.position = new Vector2(-8.5f, 4.5f);
	}
	
	void LateUpdate () {
		if(bricks.Count<=0){
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; // TODO remove when we load new levels
#else
			Application.Quit();
#endif
		}
	}

	public void DestroyBrick(Brick brick){
		if(bricks.Remove(brick.gameObject)){
			Destroy(brick.gameObject);
		}
	}

	private void GenerateLevel(){
		bricks = new List<GameObject>();

		BoxCollider2D box = brickPrefab.GetComponent<BoxCollider2D>();
		float width = box.size.x * box.transform.localScale.x;
		float height = box.size.y * box.transform.localScale.y;

		Vector2 position = Vector2.zero;
		for(int i=0; i<map.Length; ++i){
			char c = map[i];
			if(c=='#'){
				GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity, bricksParent.transform);
				bricks.Add(brick);
			}
			position.x += width;
			if(c=='\n'){
				position = new Vector2(0f, position.y - height);
			}
		}
	}

}
