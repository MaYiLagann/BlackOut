using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeBuilding {
	public float ObtainGoldSeconds;
	public float CostGold;
	public float CostTime;
}

[Serializable]
public class UpgradeLand {
	public int MaxPeople;
	public int SpawnPeopleSeconds;
	public float CostGold;
	public float CostTime;
}

[Serializable]
public class UpgradeFishing {
	public int FishType;
	public float CostGold;
	public float CostTime;
}

[Serializable]
public class UpgradeTech {
	public int ActiveState;
	public float CostGold;
	public float CostTime;
}

public class GameSystemCtrl : MonoBehaviour {

	[Header("- Time Settings -")]
	public float DaySeconds = 600f;
	public float StartDaySecond = 0f;

	private bool dayRun;
	private int daySurvives;
	private float dayProgress;

	private DateTime dayDateTime;

	[Header("- Upgrade Settings -")]
	public UpgradeBuilding[] UpgradeBuildingData;
	private int upgradeBuildingLevel;
	private bool upgradeBuildingState;
	private float upgradeBuildingLeftSeconds;
	public UpgradeLand[] UpgradeLandData;
	private int upgradeLandLevel;
	private bool upgradeLandState;
	private float upgradeLandLeftSeconds;
	public UpgradeFishing[] UpgradeFishingData;
	private int upgradeFishingLevel;
	private bool upgradeFishingState;
	private float upgradeFishingLeftSeconds;
	public UpgradeTech[] UpgradeTechData;
	private int upgradeTechLevel;
	private bool upgradeTechState;
	private float upgradeTechLeftSeconds;

	[Header("- Gold Settings -")]
	public float StartGold = 0;
	private float currentGold;

	[Header("- People Settings -")]
	public int StartPeople = 0;
	private int currentPeople;

	[Header("- Require Components -")]
	public BuildingScaler CurrentBuildingScaler;

	/* Event Functions */

	void Awake () {
		dayRun = true;
		daySurvives = 1;
		dayProgress = StartDaySecond;
		dayDateTime = new DateTime();
		dayDateTime = dayDateTime.AddDays(StartDaySecond/DaySeconds);
		
		upgradeBuildingLevel = 1;
		upgradeLandLevel = 1;
		upgradeFishingLevel = 1;
		upgradeTechLevel = 1;

		upgradeBuildingState = false;
		upgradeLandState = false;
		upgradeFishingState = false;
		upgradeTechState = false;

		upgradeBuildingLeftSeconds = 0f;
		upgradeLandLeftSeconds = 0f;
		upgradeFishingLeftSeconds = 0f;
		upgradeTechLeftSeconds = 0f;

		currentGold = StartGold;

		currentPeople = StartPeople;

		CurrentBuildingScaler.SetScale(upgradeBuildingLevel);
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

	public DateTime getDayDateTime() {
		return dayDateTime;
	}

	public int getUpgradeBuildingLevel() {
		return upgradeBuildingLevel;
	}

	public float getUpgradeBuildingCost() {
		return UpgradeBuildingData.Length < upgradeBuildingLevel? -1f : UpgradeBuildingData[upgradeBuildingLevel].CostGold;
	}

	public bool getUpgradeBuildingState() {
		return upgradeBuildingState;
	}

	public float getUpgradeBuildingLeftSeconds() {
		return upgradeBuildingLeftSeconds;
	}

	public int getUpgradeLandLevel() {
		return upgradeLandLevel;
	}
	
	public float getUpgradeLandCost() {
		return UpgradeLandData.Length < upgradeLandLevel? -1f : UpgradeLandData[upgradeLandLevel].CostGold;
	}

	public bool getUpgradeLandState() {
		return upgradeLandState;
	}

	public float getUpgradeLandLeftSeconds() {
		return upgradeLandLeftSeconds;
	}

	public int getUpgradeFishingLevel() {
		return upgradeFishingLevel;
	}
	
	public float getUpgradeFishingCost() {
		return UpgradeFishingData.Length < upgradeFishingLevel? -1f : UpgradeFishingData[upgradeFishingLevel].CostGold;
	}

	public bool getUpgradeFishingState() {
		return upgradeFishingState;
	}

	public float getUpgradeFishingLeftSeconds() {
		return upgradeFishingLeftSeconds;
	}

	public int getUpgradeTechLevel() {
		return upgradeTechLevel;
	}
	
	public float getUpgradeTechCost() {
		return UpgradeTechData.Length < upgradeTechLevel? -1f : UpgradeTechData[upgradeTechLevel].CostGold;
	}

	public bool getUpgradeTechState() {
		return upgradeTechState;
	}

	public float getUpgradeTechLeftSeconds() {
		return upgradeTechLeftSeconds;
	}

	public float getCurrentGold(){
		return currentGold;
	}

	/* Get Functions */

	/* Logic Functions */

	public void setUpgradeBuildingLevel() {
		if(upgradeBuildingState) return;
		if(UpgradeBuildingData.Length < upgradeBuildingLevel) return;
		if(UpgradeBuildingData[upgradeBuildingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeBuildingData[upgradeBuildingLevel].CostGold;

		upgradeBuildingState = true;
		upgradeBuildingLeftSeconds = UpgradeBuildingData[upgradeBuildingLevel].CostTime;
	}
	
	public void setUpgradeLandLevel() {
		if(upgradeLandState) return;
		if(UpgradeLandData.Length < upgradeLandLevel) return;
		if(UpgradeLandData[upgradeLandLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeLandData[upgradeLandLevel].CostGold;
		
		upgradeLandState = true;
		upgradeLandLeftSeconds = UpgradeLandData[upgradeLandLevel].CostTime;
	}
	
	public void setUpgradeFishingLevel() {
		if(upgradeFishingState) return;
		if(UpgradeFishingData.Length < upgradeFishingLevel) return;
		if(UpgradeFishingData[upgradeFishingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeFishingData[upgradeFishingLevel].CostGold;
		
		upgradeFishingState = true;
		upgradeFishingLeftSeconds = UpgradeFishingData[upgradeFishingLevel].CostTime;
	}
	
	public void setUpgradeTechLevel() {
		if(upgradeTechState) return;
		if(UpgradeTechData.Length < upgradeTechLevel) return;
		if(UpgradeTechData[upgradeTechLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeTechData[upgradeTechLevel].CostGold;
		
		upgradeTechState = true;
		upgradeTechLeftSeconds = UpgradeTechData[upgradeTechLevel].CostTime;
	}

	public void toggleDayRun() {
		dayRun = !dayRun;
	}

	public void toggleDayRun(bool run) {
		dayRun = run;
	}

	private void RunTime() {
		if(!dayRun) return;

		float prev_dayp = dayProgress;
		dayProgress += Time.deltaTime;
		dayDateTime = dayDateTime.AddDays(Time.deltaTime/DaySeconds);
		if(((int)prev_dayp) != ((int)dayProgress)) SecondUpdate();

		if(dayProgress >= DaySeconds){
			dayProgress -= DaySeconds;
			daySurvives++;
		}

		if(upgradeBuildingState) {
			upgradeBuildingLeftSeconds -= Time.deltaTime;
			if(upgradeBuildingLeftSeconds < 0){
				upgradeBuildingState = false;
				upgradeBuildingLevel++;
				CurrentBuildingScaler.SetScale(upgradeBuildingLevel);
			}
		}
		if(upgradeLandState) {
			upgradeLandLeftSeconds -= Time.deltaTime;
			if(upgradeLandLeftSeconds < 0){
				upgradeLandState = false;
				upgradeLandLevel++;
			}
		}
		if(upgradeFishingState) {
			upgradeFishingLeftSeconds -= Time.deltaTime;
			if(upgradeFishingLeftSeconds < 0){
				upgradeFishingState = false;
				upgradeFishingLevel++;
			}
		}
		if(upgradeTechState) {
			upgradeTechLeftSeconds -= Time.deltaTime;
			if(upgradeTechLeftSeconds < 0){
				upgradeTechState = false;
				upgradeTechLevel++;
			}
		}
	}

	/* Logic Functions */

}
