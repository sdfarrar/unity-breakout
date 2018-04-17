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

	private LevelData levelData;

	private string map = "##11 1111 1111 1111\n"
						+"   111  111  111";

	void Start () {
		levelData.gameManager = GameObject.FindObjectOfType<GameManager>();

		//map = "##";//simple map
		//map = "#123";//multiple brick types
		Brick.SetBrickManager(this);

		levelData.bricksParent = GameObject.Find("Bricks");

		if(levelData.bricksParent==null){
			levelData.bricksParent = new GameObject();
			levelData.bricksParent.name = "Bricks";
		}

		GenerateLevel();

		levelData.bricksParent.transform.position = new Vector2(-8.5f, 4.5f);
	}
	

	public bool HasBricksLeft(){
		return levelData.bricks.Count>0;
	}

	public void DestroyBrick(Brick brick){
		if(levelData.bricks.Remove(brick.gameObject)){
			Destroy(brick.gameObject);
		}
	}

	// Callback for bricks with easing
	public void BrickInPosition(){
		levelData.bricksReady++;
		if(IsLevelReady()){
			levelData.gameManager.OnLevelLoaded();
		}
	}

	private void GenerateLevel(){
		List<GameObject> bricks = new List<GameObject>();

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
					levelData.indestructibleCount++;
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
		levelData.bricks = bricks;
	}

	private Brick InstantiateBrick(Vector3 position, Quaternion rotation, BrickTemplate template){
		GameObject go = Instantiate(brickPrefab, levelData.bricksParent.transform.position, rotation, levelData.bricksParent.transform);
		Brick brick = go.GetComponent<Brick>();
		brick.transform.position = position;
		brick.indestructible = template.indestructible;
		brick.health = template.health;
		brick.SetColor(template.color);
		return brick;
	}

	private GameObject InstantiateBrickWithEasing(Vector3 position, Quaternion rotation, BrickTemplate template){
		Vector3 startPosition = position + spawnPositionOffset;
		Brick brick = InstantiateBrick(startPosition, rotation, template);
		EasePosition easing = brick.gameObject.AddComponent<EasePosition>();
		easing.endPosition = position;
		easing.duration = Random.Range(0.25f, 0.75f);
		easing.OnEasingFinished.AddListener(brick.IsReady);
		return brick.gameObject;
	}

	private bool IsLevelReady(){
		return (levelData.bricksReady-levelData.indestructibleCount)>=levelData.bricks.Count;
	}


	private struct LevelData {
		public GameObject bricksParent;
		public List<GameObject> bricks;
		public GameManager gameManager;
		public int bricksReady;
		public int indestructibleCount;

		LevelData(GameObject bricksParent, List<GameObject> bricks, GameManager gameManager){
			this.bricksParent = bricksParent;
			this.bricks = bricks;
			this.gameManager = gameManager;
			this.bricksReady = this.indestructibleCount = 0;
		}

	}
}
