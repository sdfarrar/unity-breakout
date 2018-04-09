using UnityEngine;

public class Paddle : MonoBehaviour {

	public Transform stage;
	public GameObject ballPrefab;
	public float speed = 5f;

	private Bounds confines;
	private Ball ball;

	void Start() {
		ball = GetComponentInChildren<Ball>();
		if(ball==null){
			Vector3 ballPosition = transform.position + Vector3.up*0.335f;
			ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity, transform).GetComponent<Ball>();
		}
		Stage s = stage.GetComponent<Stage>();
		confines = s.GetBounds();
	}
	
	void Update() {
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
		transform.position += input * Time.deltaTime * speed;

		if(Input.GetKeyDown(KeyCode.Space)){
			ball.Launch(GetLaunchVector());
		}
	}

	private Vector2 GetLaunchVector(){
		float launchAngle = Vector2.Angle(transform.position, stage.transform.position);
		// Find angle in relation to 90 since we're concerned with the angle relative to the "up" direction
		launchAngle = (transform.position.x > stage.position.x) ? 90f - launchAngle : launchAngle + 90f;

		float radians = launchAngle * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
	}

	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, stage.transform.position);
	}
}
