using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasePosition : MonoBehaviour {

	private Vector3 startPosition = Vector3.zero;
	public Vector3 endPosition = Vector3.zero;
	public float duration = 1f;

	public UnityEngine.Events.UnityEvent OnEasingFinished = new UnityEngine.Events.UnityEvent();

	private float elapsed = 0f;
	private bool done = false;

	private void Start(){
		startPosition = transform.localPosition;
	}

	void Update () {
		elapsed += Time.deltaTime;
		float newY = Berp(elapsed/duration);
		transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

		if(transform.localPosition.y==endPosition.y){
			this.enabled = false;//disable this script when we're done with our easing
			OnEasingFinished.Invoke();
		}

	}

	/**
	 * "Bouncy" Lerp
	 */
	private float Berp(float value){
		value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return startPosition.y + (endPosition.y - startPosition.y) * value;
	}

}
