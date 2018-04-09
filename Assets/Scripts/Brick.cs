using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Brick : MonoBehaviour {

	public int health = 1;

	public void Damage(){
		health--;
		if(health<=0){
			Destroy(this.gameObject);
		}
	}

}
