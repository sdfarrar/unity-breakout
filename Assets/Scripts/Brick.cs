﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Brick : MonoBehaviour {

	public int health = 1;
	public bool indestructible = false;

	private SpriteRenderer spriteRenderer;
	private bool updateColor = false;

	private static BrickManager manager;

	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update(){
	}

	public void Damage(){
		if(indestructible){ return; }
		if(--health>0){ return; }

		if(Brick.manager==null){ Destroy(this.gameObject); return; } // this is bad
		Brick.manager.DestroyBrick(this);
	}

	public void SetColor(Color c){
		spriteRenderer.color = c;
	}

	public static void SetBrickManager(BrickManager manager){
		Brick.manager = manager;
	}

}
