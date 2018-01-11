using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemCtrl : MonoBehaviour {

	[Header("- Time Settings -")]
	public float DaySeconds = 600f;

	private bool dayRun;
	private int daySurvives;
	private float dayProgress;

	// [Header("- Upgrade Settings -")]
	// public UpgradeBuilding[] MaxBuildingUpgrade;

	/* Event Functions */

	void Awake () {
		dayRun = true;
		daySurvives = 1;
		dayProgress = 0f;
	}
	
	void Update () {
		RunTime();
	}

	/* Event Functions */

	/* Get Functions */

	public float getDayProgress() {
		return dayProgress;
	}

	public int getDaySurvives() {
		return daySurvives;
	}

	/* Get Functions */

	/* Logic Functions */

	private void RunTime() {
		if(!dayRun) return;
		dayProgress += Time.deltaTime;
		if(dayProgress >= DaySeconds){
			dayProgress -= DaySeconds;
			daySurvives++;
		}
	}

	/* Logic Functions */

}

// [SerializeField]
// public struct UpgradeBuilding : MonoBehaviour {
// 	float ObtainGoldSecond;
// 	float ObtailGoldDay;
// 	float CostGold;
// 	float CostTime;
// }