using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	private Transform left;
	private BoxCollider2D leftBox;
	private Transform right;
	private BoxCollider2D rightBox;

	private Bounds bounds;

	void Start () {
		left = transform.Find("Left");	
		leftBox = left.GetComponent<BoxCollider2D>();
		right = transform.Find("Right");
		rightBox = right.GetComponent<BoxCollider2D>();
		ComputeBounds();
	}

	public Bounds GetBounds(){
		return bounds;
	}

	public float GetLeftBound(){
		return left.transform.position.x + leftBox.size.x*0.5f;
	}

	public float GetRightBound(){
		return right.transform.position.x - rightBox.size.x*0.5f;
	}

	private void ComputeBounds(){
		Debug.Log("parent.position " + transform.position);
		Debug.Log("left.position " + left.transform.position);
		Debug.Log("right.position " + right.transform.position);
		float leftX = left.transform.position.x;
		float rightX = right.transform.position.x;
		float sizeX = (Mathf.Abs(leftX) + Mathf.Abs(rightX))/2f;
		bounds = new Bounds(transform.position, new Vector3(sizeX, 0, 0));
		Debug.Log("bounds: " + bounds);
	}
}
