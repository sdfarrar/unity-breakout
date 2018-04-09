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
}
