using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Brick : MonoBehaviour {

	public int health = 1;

	private static BrickManager manager;

	public void Damage(){
		health--;
		if(health<=0){
			if(Brick.manager==null){ Destroy(this.gameObject); return; } // this is bad
			Brick.manager.DestroyBrick(this);
		}
	}

	public static void SetBrickManager(BrickManager manager){
		Brick.manager = manager;
	}

}
