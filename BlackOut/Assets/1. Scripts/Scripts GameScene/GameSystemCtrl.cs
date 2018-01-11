using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeBuilding {
	public float ObtainGoldSeconds;
	public float ObtailGoldDay;
	public float CostGold;
	public float CostTime;
}

[System.Serializable]
public class UpgradeLand {
	public int MaxPeople;
	public int SpawnPeopleSeconds;
	public float CostGold;
	public float CostTime;
}

[System.Serializable]
public class UpgradeFishing {
	public int FishType;
	public float CostGold;
	public float CostTime;
}

[System.Serializable]
public class UpgradeTech {
	public int ActiveState;
	public float CostGold;
	public float CostTime;
}

public class GameSystemCtrl : MonoBehaviour {

	[Header("- Time Settings -")]
	public float DaySeconds = 600f;

	private bool dayRun;
	private int daySurvives;
	private float dayProgress;

	[Header("- Upgrade Settings -")]
	public UpgradeBuilding[] UpgradeBuildingData;
	public UpgradeLand[] UpgradeLandData;
	public UpgradeFishing[] UpgradeFishingData;
	public UpgradeTech[] UpgradeTechData;

	private int upgradeBuildingLevel;
	private int upgradeLandLevel;
	private int upgradeFishingLevel;
	private int upgradeTechLevel;

	/* Event Functions */

	void Awake () {
		dayRun = true;
		daySurvives = 1;
		dayProgress = 0f;
		upgradeBuildingLevel = 1;
		upgradeLandLevel = 1;
		upgradeFishingLevel = 1;
		upgradeTechLevel = 1;
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

	public int getUpgradeBuildingLevel() {
		return upgradeBuildingLevel;
	}
	public int getUpgradeLandLevel() {
		return upgradeLandLevel;
	}
	public int getUpgradeFishingLevel() {
		return upgradeFishingLevel;
	}
	public int getUpgradeTechLevel() {
		return upgradeTechLevel;
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
