using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Upgrade Class */

[Serializable]
public class UpgradeBuilding {
	public float ObtainGoldSeconds;
	public float CostGold;
}

[Serializable]
public class UpgradeLand {
	public int MaxPeople;
	public int SpawnPeopleSeconds;
	public float CostGold;
}

[Serializable]
public class UpgradeFishing {
	public int FishType;
	public float CostGold;
}

[Serializable]
public class UpgradeTech {
	public int ActiveState;
	public float CostGold;
}

/* Upgrade Class */

/* Weather Class */

[Serializable]
public class WeatherRain {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public float IncreasePeoplePercent;
}

[Serializable]
public class WeatherSnow {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public float IncreasePeoplePercent;
}

/* Weather Class */

/* Disaster Class */

[Serializable]
public class DisasterLevel {
	public int BoundaryLevel;
	public float DurationDays;
}

[Serializable]
public class DisasterDrought {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public int[] DamageLevel;
}

[Serializable]
public class DisasterFlood {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public int[] DamageLevel;
}

[Serializable]
public class DisasterTyphoon {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public int[] DamageLevel;
}

[Serializable]
public class DisasterHeavySnow {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public int[] DamageLevel;
}

/* Disaster Class */

public class GameSystemCtrl : MonoBehaviour {

	[Header("- Time Settings -")]
	public float DaySeconds = 600f;
	public float StartDaySecond = 0f;
	[Range(0, 100)]
	public float DaySpeed = 1f;

	private bool dayRun;
	private int daySurvives;
	private float dayProgress;

	private DateTime dayDateTime;

	[Header("- Upgrade Settings -")]
	public UpgradeBuilding[] UpgradeBuildingData;
	private int upgradeBuildingLevel;
	public UpgradeLand[] UpgradeLandData;
	private int upgradeLandLevel;
	public UpgradeFishing[] UpgradeFishingData;
	private int upgradeFishingLevel;
	public UpgradeTech[] UpgradeTechData;
	private int upgradeTechLevel;

	[Header("- Weather Settings -")]
	public WeatherRain WeatherRainData;
	private bool weatherRainState;
	public WeatherSnow WeatherSnowData;
	private bool weatherSnowState;

	[Header("- Disaster Settings -")]
	public DisasterLevel[] DisasterLevelData;
	public DisasterDrought DisasterDroughtData;
	public DisasterFlood DisasterFloodData;
	public DisasterTyphoon DisasterTyphoonData;
	public DisasterHeavySnow DisasterHeavySnowData;

	[Header("- Gold Settings -")]
	public float StartGold = 0;
	private float currentGold;

	[Header("- People Settings -")]
	public int StartPeople = 0;
	private int currentPeople;
	private float currentPeopleLeftSeconds;

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

		weatherRainState = false;
		weatherSnowState = false;

		currentGold = StartGold;

		currentPeople = StartPeople;
		currentPeopleLeftSeconds = 0f;

		CurrentBuildingScaler.SetScale(upgradeBuildingLevel);
	}
	
	void Update () {
		RunTime();
	}

	void SecondUpdate () {
		currentGold += UpgradeBuildingData[upgradeBuildingLevel-1].ObtainGoldSeconds;
		if(currentPeopleLeftSeconds > 0) {
			currentPeopleLeftSeconds -= 1;
		} else if(currentPeople+1 <= getCurrentMaxPeople()) {
			currentPeople++;
			if(currentPeople+1 <= getCurrentMaxPeople())
				currentPeopleLeftSeconds = getSpawnPeopleSeconds();
		}
	}

	void DayUpdate () {
		switch(getSeason(dayDateTime.Month)){
		case "spring":
			weatherRainState = UnityEngine.Random.Range(0f, 1f) <= WeatherRainData.PercentageSpring;
			weatherSnowState = UnityEngine.Random.Range(0f, 1f) <= WeatherSnowData.PercentageSpring;
		break;
		case "summer":
			weatherRainState = UnityEngine.Random.Range(0f, 1f) <= WeatherRainData.PercentageSummer;
			weatherSnowState = UnityEngine.Random.Range(0f, 1f) <= WeatherSnowData.PercentageSummer;
		break;
		case "fall":
			weatherRainState = UnityEngine.Random.Range(0f, 1f) <= WeatherRainData.PercentageFall;
			weatherSnowState = UnityEngine.Random.Range(0f, 1f) <= WeatherSnowData.PercentageFall;
		break;
		case "winter":
			weatherRainState = UnityEngine.Random.Range(0f, 1f) <= WeatherRainData.PercentageWinter;
			weatherSnowState = UnityEngine.Random.Range(0f, 1f) <= WeatherSnowData.PercentageWinter;
		break;
		}
	}
	
	void MonthUpdate () {

	}

	void YearUpdate () {

	}

	/* Event Functions */

	/* Get Functions */

	public bool getDayRun() {
		return dayRun;
	}

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
		return UpgradeBuildingData.Length <= upgradeBuildingLevel? -1f : UpgradeBuildingData[upgradeBuildingLevel].CostGold;
	}

	public int getUpgradeLandLevel() {
		return upgradeLandLevel;
	}
	
	public float getUpgradeLandCost() {
		return UpgradeLandData.Length <= upgradeLandLevel? -1f : UpgradeLandData[upgradeLandLevel].CostGold;
	}

	public int getUpgradeFishingLevel() {
		return upgradeFishingLevel;
	}
	
	public float getUpgradeFishingCost() {
		return UpgradeFishingData.Length <= upgradeFishingLevel? -1f : UpgradeFishingData[upgradeFishingLevel].CostGold;
	}

	public int getUpgradeTechLevel() {
		return upgradeTechLevel;
	}
	
	public float getUpgradeTechCost() {
		return UpgradeTechData.Length <= upgradeTechLevel? -1f : UpgradeTechData[upgradeTechLevel].CostGold;
	}

	public bool getWeatherRainState() {
		return weatherRainState;
	}

	public bool getWeatherSnowState() {
		return weatherSnowState;
	}

	public float getCurrentGold() {
		return currentGold;
	}

	public int getCurrentPeople() {
		return currentPeople;
	}

	public int getCurrentMaxPeople() {
		return UpgradeLandData.Length < upgradeLandLevel? -1 : UpgradeLandData[upgradeLandLevel-1].MaxPeople;
	}

	public float getCurrentPeopleSeconds() {
		return currentPeopleLeftSeconds;
	}

	public float getSpawnPeopleSeconds() {
		float per = 1f;
		if(weatherRainState) per += WeatherRainData.IncreasePeoplePercent;
		if(weatherSnowState) per += WeatherSnowData.IncreasePeoplePercent;
		
		return UpgradeLandData.Length < upgradeLandLevel? -1f : UpgradeLandData[upgradeLandLevel-1].SpawnPeopleSeconds * per;
	}

	/* Get Functions */

	/* Logic Functions */

	public void GameEnd() {
		Application.Quit();
	}

	public void setUpgradeBuildingLevel() {
		if(UpgradeBuildingData.Length <= upgradeBuildingLevel) return;
		if(UpgradeBuildingData[upgradeBuildingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeBuildingData[upgradeBuildingLevel].CostGold;
		upgradeBuildingLevel++;
	}
	
	public void setUpgradeLandLevel() {
		if(UpgradeLandData.Length <= upgradeLandLevel) return;
		if(UpgradeLandData[upgradeLandLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeLandData[upgradeLandLevel].CostGold;
		upgradeLandLevel++;
	}
	
	public void setUpgradeFishingLevel() {
		if(UpgradeFishingData.Length <= upgradeFishingLevel) return;
		if(UpgradeFishingData[upgradeFishingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeFishingData[upgradeFishingLevel].CostGold;
		upgradeFishingLevel++;
	}
	
	public void setUpgradeTechLevel() {
		if(UpgradeTechData.Length <= upgradeTechLevel) return;
		if(UpgradeTechData[upgradeTechLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeTechData[upgradeTechLevel].CostGold;
		upgradeTechLevel++;
	}

	public void toggleDayRun() {
		dayRun = !dayRun;
	}

	public void toggleDayRun(bool run) {
		dayRun = run;
	}

	private void RunTime() {
		if(!dayRun) return;

		float prevDayProgress = dayProgress;
		DateTime prevDayDateTime = dayDateTime;

		dayProgress += Time.deltaTime * DaySpeed;
		dayDateTime = dayDateTime.AddDays(Time.deltaTime*DaySpeed/DaySeconds);

		if(((int)prevDayProgress) != ((int)dayProgress)) SecondUpdate();
		if(prevDayDateTime.Day != dayDateTime.Day) DayUpdate();
		if(prevDayDateTime.Month != dayDateTime.Month) MonthUpdate();
		if(prevDayDateTime.Year != dayDateTime.Year) YearUpdate();

		if(dayProgress >= DaySeconds){
			dayProgress -= DaySeconds;
			daySurvives++;
		}
	}

	private string getSeason(int month) {
		if(month < 0 || month > 12){
			Debug.LogError("Error: month is over 0, less 13");
			return "error";
		}
		string season = "spring";
		switch(month){
		case 3:
		case 4:
		case 5:
			season = "spring";
		break;
		case 6:
		case 7:
		case 8:
			season = "summer";
		break;
		case 9:
		case 10:
		case 11:
			season = "fall";
		break;
		case 12:
		case 1:
		case 2:
			season = "winter";
		break;
		}
		return season;
	}

	/* Logic Functions */

}
