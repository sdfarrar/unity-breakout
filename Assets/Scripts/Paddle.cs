﻿using UnityEngine;

public class Paddle : MonoBehaviour {

	public Transform stage;
	public GameObject ballPrefab;
	public float speed = 5f;

	private Animator animator;

	private float leftBound;
	private float rightBound;
	private BoxCollider2D box;
	private Ball ball;


	private void Start() {
		animator = GetComponentInChildren<Animator>();
		box = GetComponent<BoxCollider2D>();
		ball = GetComponentInChildren<Ball>();
		InitializeBall(true);

		Stage s = stage.GetComponent<Stage>();
		leftBound = s.GetLeftBound();
		rightBound = s.GetRightBound();
	}
	
	private void Update() {
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
		transform.position += input * Time.deltaTime * speed;

		float halfWidth = box.size.x*transform.localScale.x*0.5f;


		if((transform.position.x - halfWidth) < leftBound){
			transform.position = new Vector3(leftBound+halfWidth, transform.position.y, transform.position.z);
		}
		if((transform.position.x + halfWidth) > rightBound){
			transform.position = new Vector3(rightBound-halfWidth, transform.position.y, transform.position.z);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			ball.Launch(GetLaunchVector());
		}
	}

	public void ResetBall(){
		// Check lives, if 0, end game
		InitializeBall(false);
	}

	private void InitializeBall(bool instantiateBall){
		Vector3 ballPosition = transform.position + Vector3.up*0.335f;
		if(instantiateBall){
			ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity, transform).GetComponent<Ball>();
			ball.paddle = this;
		}else{
			ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			ball.transform.parent = transform;
			ball.transform.position = ballPosition;
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

	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject==ball.gameObject){
			animator.Play("bouncy", -1, 0);
		}

	}
}
