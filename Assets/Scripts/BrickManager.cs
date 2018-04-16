using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

	public GameObject brickPrefab;

	public Vector3 spawnPositionOffset = Vector3.zero;

	public BrickTemplate weak;
	public BrickTemplate average;
	public BrickTemplate strong;
	public BrickTemplate indestructible;

	private GameObject bricksParent;
	private List<GameObject> bricks;
	private GameManager gameManager;

	private string map = "#### #### #### ####\n"
						+"   ###  ###  ###";

	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();

		map = "##";//simple map
		map = "#123";//multiple brick types
		Brick.SetBrickManager(this);

		bricksParent = GameObject.Find("Bricks");
		if(bricksParent==null){
			bricksParent = new GameObject();
			bricksParent.name = "Bricks";
		}

		GenerateLevel();
		bricksParent.transform.position = new Vector2(-8.5f, 4.5f);
		gameManager.OnLevelLoaded();
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
			GameObject brick;
			switch(c){
				case '#':
					InstantiateBrickWithEasing(position, Quaternion.identity, indestructible);
				break;
				case '1':
					brick = InstantiateBrickWithEasing(position, Quaternion.identity, weak);
					bricks.Add(brick);
				break;
				case '2':
					brick = InstantiateBrickWithEasing(position, Quaternion.identity, average);
					bricks.Add(brick);
				break;
				case '3':
					brick = InstantiateBrickWithEasing(position, Quaternion.identity, strong);
					bricks.Add(brick);
				break;
			}
			position.x += width;
			if(c=='\n'){
				position = new Vector2(0f, position.y - height);
			}
		}
	}

	private GameObject InstantiateBrick(Vector3 position, Quaternion rotation, BrickTemplate template){
		GameObject go = Instantiate(brickPrefab, bricksParent.transform.position, rotation, bricksParent.transform);
		Brick brick = go.GetComponent<Brick>();
		brick.transform.position = position;
		brick.indestructible = template.indestructible;
		brick.health = template.health;
		brick.SetColor(template.color);
		return go;
	}

	private GameObject InstantiateBrickWithEasing(Vector3 position, Quaternion rotation, BrickTemplate template){
		Vector3 startPosition = position + spawnPositionOffset;
		GameObject go = InstantiateBrick(startPosition, rotation, template);
		EasePosition easing = go.AddComponent<EasePosition>();
		easing.endPosition = position;
		easing.duration = 0.75f;
		return go;
	}

}
