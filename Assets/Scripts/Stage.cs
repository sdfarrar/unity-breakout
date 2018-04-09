using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	private Bounds bounds;

	void Start () {
		ComputeBounds();
	}

	public Bounds GetBounds(){
		return bounds;
	}

	private void ComputeBounds(){
		bounds = new Bounds();
	}
}
