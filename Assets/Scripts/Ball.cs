using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector2 launchSpeed = new Vector2(1.5f, 1.5f);
	public Vector3 velocity;

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

		Destroy(this.gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag=="Brick"){
			collision.gameObject.GetComponent<Brick>().Damage();
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		if(Mathf.Abs(rb.velocity.x) - 0.001f <= 0f){
			Debug.Log("nudging x");
			rb.AddForce(new Vector3(Random.Range(-1.5f, 1.5f), 0f, 0f));
		}
		if(Mathf.Abs(rb.velocity.y) - 0.001f <= 0f){
			Debug.Log("nudging y");
			rb.AddForce(new Vector3(0f, Random.Range(-1.5f, 1.5f), 0f));
		}
		// TODO add a max speed param and check against it before increasing velocity
		rb.velocity *= 1.1f;
	}
}
