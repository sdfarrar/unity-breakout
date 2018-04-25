using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector2 launchSpeed = new Vector2(1.5f, 1.5f);
	public Vector3 velocity;
	public Paddle paddle;
	public float maxSpeed = 3f;

	private Rigidbody2D rb;
	private bool launched;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		velocity = rb.velocity;
	}

	public void Launch(Vector2 angle){
		if(launched){ return; }

		rb.AddForce(launchSpeed*angle);
		transform.parent = null;

		launched = true;
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag!="Killzone"){ return; }
		if(paddle!=null){
			paddle.ResetBall();
			launched = false;
		}else{
			Destroy(this.gameObject); // another shouldnt happen zone
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		switch(collision.gameObject.tag){
			case "Brick": collision.gameObject.GetComponent<Brick>().Damage(); break;
			case "Player": AudioManager.Instance.Play("PaddleHit"); break;
			case "Wall": AudioManager.Instance.Play("WallHit"); break;
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if(Mathf.Abs(rb.velocity.x) - 0.001f <= 0f){
			rb.AddForce(new Vector3(Random.Range(-1.5f, 1.5f), 0f, 0f));
		}
		if(Mathf.Abs(rb.velocity.y) - 0.001f <= 0f){
			rb.AddForce(new Vector3(0f, Random.Range(-1.5f, 1.5f), 0f));
		}

		Vector2 newVelocity = rb.velocity * 1.05f;
		Debug.Log("vel: " + newVelocity.sqrMagnitude + " | max: " + maxSpeed*maxSpeed);
		if(newVelocity.sqrMagnitude < maxSpeed*maxSpeed){
			rb.velocity *= 1.05f;
		}
		
	}
}
