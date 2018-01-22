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
	[Range(0f, 1f)]
	public float DecreaseDamage;
	public float CostGold;
}

/* Upgrade Class */

/* Weather Class */

[Serializable]
public class WeatherData {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;
	public float IncreasePeoplePercent;

	[HideInInspector]
	public bool State;

	public WeatherData() {
		State = false;
	}

	/* Logic Function */

	public bool setActive(string season) {
		return setActive(season, () => {});
	}

	public bool setActive(string season, Action function) {
		bool result = false;

		switch(season) {
		case "spring":
			result = PercentageSpring != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageSpring;
		break;
		case "summer":
			result = PercentageSummer != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageSummer;
		break;
		case "fall":
			result = PercentageFall != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageFall;
		break;
		case "winter":
			result = PercentageWinter != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageWinter;
		break;
		}

		if(result)
			function();

		return result;
	}

	/* Logic Function */
}

/* Weather Class */

/* Disaster Class */

[Serializable]
public class DisasterLevelData {
	public int BoundaryLevel;
	public float DurationDays;
}

[Serializable]
public class DisasterData {
	[Range(0, 1)]
	public float PercentageSpring;
	[Range(0, 1)]
	public float PercentageSummer;
	[Range(0, 1)]
	public float PercentageFall;
	[Range(0, 1)]
	public float PercentageWinter;

	public int[] DamageLevel;
	[HideInInspector]
	public float DamageCount;

	public Action DayUpdate = () => {};
	private float LeftDays;

	public DisasterData() {
		LeftDays = 0;
	}
	
	/* Get Functions */

	public float getLeftDays() {
		return LeftDays;
	}
	
	/* Get Functions */

	/* Logic Functions */

	public bool setActive(string season) {
		return setActive(season, () => {});
	}

	public bool setActive(string season, Action function) {

		bool result = false;

		switch(season) {
		case "spring":
			result = PercentageSpring != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageSpring;
		break;
		case "summer":
			result = PercentageSummer != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageSummer;
		break;
		case "fall":
			result = PercentageFall != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageFall;
		break;
		case "winter":
			result = PercentageWinter != 0f && UnityEngine.Random.Range(0f, 1f) <= PercentageWinter;
		break;
		}

		if(result)
			function();

		return result;
	}

	public void setLeftDays(float sec) {
		if(sec < 0f)
			sec = 0f;
		LeftDays = sec;
	}

	public void subLeftDays(float sec) {
		if(LeftDays <= 0f) return;

		float prev = LeftDays;
		LeftDays -= sec;
		if((int)prev != (int)LeftDays) DayUpdate();
	}

	public void setDamage(ref int people, int level, float day, float percent = 1f) {
		DamageCount +=  DamageLevel[level-1] / day * percent;
		if(DamageCount >= 1f) {
			float dmg = Mathf.Floor(DamageCount);
			DamageCount -= dmg;
			people -= (int)dmg;
		}
	}
	
	/* Logic Functions */
}

/* Disaster Class */

/* Fish Class */

[Serializable]
public class FishData {
	public Sprite Image;
	public string Name;
	public string Size;
	public int ActiveLevel;
	public int MaxHealth;
	public float PriceGold;
	[Range(0f, 1f)]
	public float Percentage;
}

/* Fish Class */

public class GameSystemCtrl : MonoBehaviour {

	[Header("- Time Settings -")]
	[Range(0, 100)]
	public float GameSpeed = 1f;
	public float DaySeconds = 600f;
	public float StartDaySecond = 0f;

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
	public WeatherData WeatherRainData;
	public WeatherData WeatherSnowData;

	[Header("- Disaster Settings -")]
	public DisasterLevelData[] DisasterLevelData;
	private int currentDisasterLevel;
	public DisasterData DisasterDroughtData;
	public DisasterData DisasterFloodData;
	public DisasterData DisasterTyphoonData;
	public DisasterData DisasterHeavySnowData;

	[Header("- Fish Settings -")]
	public FishData[] FishTypeData;

	[Header("- Gold Settings -")]
	public float StartGold = 0;
	private float currentGold;

	[Header("- People Settings -")]
	public int StartPeople = 0;
	private int currentPeople;
	private float currentPeopleLeftSeconds;

	[Header("- Require Components -")]
	public BuildingScaler CurrentBuildingScaler;

	/* Events */

	public event EventHandler onSecondUpdate;
	public event EventHandler onDayUpdate;
	public event EventHandler onMonthUpdate;
	public event EventHandler onYearUpdate;
	public event EventHandler onDayToggle;
	public event EventHandler onUpgradeUpdate;

	/* Events */

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

		WeatherRainData.State = false;
		WeatherSnowData.State = false;

		currentDisasterLevel = 1;
		DisasterDroughtData.DayUpdate = () => { DisasterDroughtData.setDamage(ref currentPeople, currentDisasterLevel, DisasterLevelData[currentDisasterLevel-1].DurationDays, 1f - UpgradeTechData[upgradeTechLevel-1].DecreaseDamage); };
		DisasterFloodData.DayUpdate = () => { DisasterFloodData.setDamage(ref currentPeople, currentDisasterLevel, DisasterLevelData[currentDisasterLevel-1].DurationDays, 1f - UpgradeTechData[upgradeTechLevel-1].DecreaseDamage); };
		DisasterTyphoonData.DayUpdate = () => { DisasterTyphoonData.setDamage(ref currentPeople, currentDisasterLevel, DisasterLevelData[currentDisasterLevel-1].DurationDays, 1f - UpgradeTechData[upgradeTechLevel-1].DecreaseDamage); };
		DisasterHeavySnowData.DayUpdate = () => { DisasterHeavySnowData.setDamage(ref currentPeople, currentDisasterLevel, DisasterLevelData[currentDisasterLevel-1].DurationDays, 1f - UpgradeTechData[upgradeTechLevel-1].DecreaseDamage); };

		currentGold = StartGold;

		currentPeople = StartPeople;
		currentPeopleLeftSeconds = getSpawnPeopleSeconds();
	}
	
	void Update () {
		RunTime();
	}

	void SecondUpdate () {

		currentGold += UpgradeBuildingData[upgradeBuildingLevel-1].ObtainGoldSeconds;
		if(currentPeopleLeftSeconds > 0f && currentPeople < getCurrentMaxPeople()) {
			currentPeopleLeftSeconds -= 1f;
		} else {
			if(currentPeople < getCurrentMaxPeople())
				currentPeople++;
			currentPeopleLeftSeconds = getSpawnPeopleSeconds();
		}

		if(onSecondUpdate != null)
			onSecondUpdate(this, EventArgs.Empty);
	}

	void DayUpdate () {

		string season = getSeason(dayDateTime.Month);

		WeatherRainData.State = WeatherRainData.setActive(season);
		WeatherSnowData.State = WeatherSnowData.setActive(season);

		DisasterDroughtData.subLeftDays(1f);
		DisasterFloodData.subLeftDays(1f);
		DisasterTyphoonData.subLeftDays(1f);
		DisasterHeavySnowData.subLeftDays(1f);

		if(onDayUpdate != null)
			onDayUpdate(this, EventArgs.Empty);
	}
	
	void MonthUpdate () {

		bool newSeason;
		string season = getSeason(dayDateTime.Month, out newSeason);

		if(newSeason) {
			DisasterDroughtData.setActive(season, () => {
				DisasterData data = DisasterDroughtData;
				if(data.DamageLevel[currentDisasterLevel-1] == 0)
					return;
				data.setLeftDays(DisasterLevelData[currentDisasterLevel-1].DurationDays);
			});
			DisasterFloodData.setActive(season, () => {
				DisasterData data = DisasterFloodData;
				if(data.DamageLevel[currentDisasterLevel-1] == 0)
					return;
				data.setLeftDays(DisasterLevelData[currentDisasterLevel-1].DurationDays);
			});
			DisasterTyphoonData.setActive(season, () => {
				DisasterData data = DisasterTyphoonData;
				if(data.DamageLevel[currentDisasterLevel-1] == 0)
					return;
				data.setLeftDays(DisasterLevelData[currentDisasterLevel-1].DurationDays);
			});
			DisasterHeavySnowData.setActive(season, () => {
				DisasterData data = DisasterHeavySnowData;
				if(data.DamageLevel[currentDisasterLevel-1] == 0)
					return;
				data.setLeftDays(DisasterLevelData[currentDisasterLevel-1].DurationDays);
			});
		}

		if(onMonthUpdate != null)
			onMonthUpdate(this, EventArgs.Empty);
	}

	void YearUpdate () {
		if(onYearUpdate != null)
			onYearUpdate(this, EventArgs.Empty);
	}

	/* Event Functions */

	/* Get Functions: Day */

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
	
	/* Get Functions: Day */

	/* Get Functions: Upgrade */

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
	
	/* Get Functions: Upgrade */
	
	/* Get Functions: Gold */

	public float getCurrentGold() {
		return currentGold;
	}

	/* Get Functions: Gold */

	/* Get Functions: People */

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
		if(WeatherRainData.State) per += WeatherRainData.IncreasePeoplePercent;
		if(WeatherSnowData.State) per += WeatherSnowData.IncreasePeoplePercent;
		
		return UpgradeLandData.Length < upgradeLandLevel? -1f : UpgradeLandData[upgradeLandLevel-1].SpawnPeopleSeconds * per;
	}
	
	/* Get Functions: People */
	
	/* Get Functions: Disaster */

	public int getCurrentDisasterLevel() {
		return currentDisasterLevel;
	}

	/* Get Functions: Disaster */

	/* Logic Functions */

	public void GameEnd() {
		Application.Quit();
	}

	public void setUpgradeBuildingLevel() {
		if(UpgradeBuildingData.Length <= upgradeBuildingLevel) return;
		if(UpgradeBuildingData[upgradeBuildingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeBuildingData[upgradeBuildingLevel].CostGold;
		upgradeBuildingLevel++;
		setDisasterLevel();
		
		if(onUpgradeUpdate != null)
			onUpgradeUpdate(this, EventArgs.Empty);
	}
	
	public void setUpgradeLandLevel() {
		if(UpgradeLandData.Length <= upgradeLandLevel) return;
		if(UpgradeLandData[upgradeLandLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeLandData[upgradeLandLevel].CostGold;
		upgradeLandLevel++;
		setDisasterLevel();
		
		if(onUpgradeUpdate != null)
			onUpgradeUpdate(this, EventArgs.Empty);
	}
	
	public void setUpgradeFishingLevel() {
		if(UpgradeFishingData.Length <= upgradeFishingLevel) return;
		if(UpgradeFishingData[upgradeFishingLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeFishingData[upgradeFishingLevel].CostGold;
		upgradeFishingLevel++;
		setDisasterLevel();
		
		if(onUpgradeUpdate != null)
			onUpgradeUpdate(this, EventArgs.Empty);
	}
	
	public void setUpgradeTechLevel() {
		if(UpgradeTechData.Length <= upgradeTechLevel) return;
		if(UpgradeTechData[upgradeTechLevel].CostGold > currentGold) return;
		
		currentGold -= UpgradeTechData[upgradeTechLevel].CostGold;
		upgradeTechLevel++;
		setDisasterLevel();
		
		if(onUpgradeUpdate != null)
			onUpgradeUpdate(this, EventArgs.Empty);
	}

	public void toggleDayRun() {
		toggleDayRun(!dayRun);
	}

	public void toggleDayRun(bool run) {
		dayRun = run;

		if(onDayToggle != null)
			onDayToggle(this, EventArgs.Empty);
	}

	private void RunTime() {
		if(!dayRun) return;

		float prevDayProgress = dayProgress;
		DateTime prevDayDateTime = dayDateTime;

		dayProgress += Time.deltaTime * GameSpeed;
		dayDateTime = dayDateTime.AddDays(Time.deltaTime * GameSpeed / DaySeconds);

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
		bool temp;
		return getSeason(month, out temp);
	}

	private string getSeason(int month, out bool newSeason) {
		newSeason = month==3 || month==6 || month==9 || month==12;
		if(month < 0 || month > 12){
			Debug.LogError("Month is over 0, less 13");
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

	private void setDisasterLevel() {
		int level = upgradeBuildingLevel + upgradeLandLevel + upgradeFishingLevel + upgradeTechLevel;
		int i = 0;
		while(i<DisasterLevelData.Length && level > DisasterLevelData[i].BoundaryLevel) i++;
		currentDisasterLevel = i + 1;
	}

	/* Logic Functions */

}
