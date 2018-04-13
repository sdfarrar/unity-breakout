using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Brick", menuName="Brick")]
public class BrickTemplate : ScriptableObject {

	public int health = 1;
	public bool indestructible = false;
	public Color color;

}
