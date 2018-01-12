using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScaler : MonoBehaviour {
	
	public Vector3[] ScaleLevel;

	/* Logic Function */

	public void SetScale(int level){
		if(ScaleLevel.Length < level){
			gameObject.transform.localScale = ScaleLevel[ScaleLevel.Length-1];
			return;
		}
		gameObject.transform.localScale = ScaleLevel[level-1];
	}

	/* Logic Function */
}
