using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector2 launchSpeed = new Vector2(1.5f, 1.5f);
	public Vector3 velocity;

	private Rigidbody2D rb;
	private bool launched;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
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
}
