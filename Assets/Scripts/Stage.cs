using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	private Transform left;
	private BoxCollider2D leftBox;
	private Transform right;
	private BoxCollider2D rightBox;

	void Start () {
		left = transform.Find("Left");	
		leftBox = left.GetComponent<BoxCollider2D>();
		right = transform.Find("Right");
		rightBox = right.GetComponent<BoxCollider2D>();
		ComputeBounds();
	}

	public float GetLeftBound(){
		return left.transform.position.x + leftBox.size.x*0.5f;
	}

	public float GetRightBound(){
		return right.transform.position.x - rightBox.size.x*0.5f;
	}

	private void ComputeBounds(){
		float leftX = left.transform.position.x;
		float rightX = right.transform.position.x;
		float sizeX = (Mathf.Abs(leftX) + Mathf.Abs(rightX))/2f;
	}
}
