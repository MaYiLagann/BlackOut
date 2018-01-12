using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeBuilding {
	public float ObtainGoldSeconds;
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

	[Header("- Gold Settings -")]
	public float StartGold = 0;

	private float currentGold;

	/* Event Functions */

	void Awake () {
		dayRun = true;
		daySurvives = 1;
		dayProgress = 0f;
		upgradeBuildingLevel = 1;
		upgradeLandLevel = 1;
		upgradeFishingLevel = 1;
		upgradeTechLevel = 1;
		currentGold = StartGold;
	}
	
	void Update () {
		RunTime();
	}

	void SecondUpdate () {
		currentGold += UpgradeBuildingData[upgradeBuildingLevel-1].ObtainGoldSeconds;
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

	public float getCurrentGold(){
		return currentGold;
	}

	/* Get Functions */

	/* Logic Functions */

	public void setUpgradeBuildingLevel() {
		if(UpgradeBuildingData.Length < upgradeBuildingLevel+1) return;
		if(UpgradeBuildingData[upgradeBuildingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeBuildingData[upgradeBuildingLevel].CostGold;
		upgradeBuildingLevel++;
	}
	
	public void setUpgradeLandLevel() {
		if(UpgradeLandData.Length < upgradeLandLevel+1) return;
		if(UpgradeLandData[upgradeLandLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeLandData[upgradeLandLevel].CostGold;
		upgradeLandLevel++;
	}
	
	public void setUpgradeFishingLevel() {
		if(UpgradeFishingData.Length < upgradeFishingLevel+1) return;
		if(UpgradeFishingData[upgradeFishingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeFishingData[upgradeFishingLevel].CostGold;
		upgradeFishingLevel++;
	}
	
	public void setUpgradeTechLevel() {
		if(UpgradeTechData.Length < upgradeTechLevel+1) return;
		if(UpgradeTechData[upgradeTechLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeTechData[upgradeTechLevel].CostGold;
		upgradeTechLevel++;
	}

	private void RunTime() {
		if(!dayRun) return;

		float prev_dayp = dayProgress;
		dayProgress += Time.deltaTime;
		if(((int)prev_dayp) != ((int)dayProgress)) SecondUpdate();

		if(dayProgress >= DaySeconds){
			dayProgress -= DaySeconds;
			daySurvives++;
		}
	}

	/* Logic Functions */

}
