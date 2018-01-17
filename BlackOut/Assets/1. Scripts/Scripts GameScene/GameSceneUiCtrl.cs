using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Canvas))]
public class GameSceneUiCtrl : MonoBehaviour {

	[Header("- Static Settings -")]
	public Text StaticTimeText;
	public Text StaticCurrentGoldText;

	[Header("- Main Settings -")]
	public Text MainCurrentPeopleText;
	public Text MainCurrentPeopleSpawnSecondsText;
	public GameObject MainCurrentPeopleSpawnSecondsPanel;

	[Header("- Upgrade Settings -")]
	public Text UpgradeCurrentBuildingLevelText;
	public Text UpgradeCurrentBuildingCostText;
	public Text UpgradeCurrentLandLevelText;
	public Text UpgradeCurrentLandCostText;
	public Text UpgradeCurrentFishingLevelText;
	public Text UpgradeCurrentFishingCostText;
	public Text UpgradeCurrentTechLevelText;
	public Text UpgradeCurrentTechCostText;
	public Button UpgradeBuildingButton;
	public Button UpgradeLandButton;
	public Button UpgradeFishingButton;
	public Button UpgradeTechButton;

	[Header("- Dev Settings -")]
	public Text DevCurrentVersionText;
	public Text DevCurrentDayProgressText;
	public Text DevCurrentDaySurvivesText;
	public Text DevCurrentUpgradeBuildingLevelText;
	public Text DevCurrentUpgradeLandLevelText;
	public Text DevCurrentUpgradeFishingLevelText;
	public Text DevCurrentUpgradeTechLevelText;
	public Text DevCurrentGoldText;
	public Text DevCurrentPeopleText;
	public Text DevCurrentPeopleSecondsText;
	public Text DevCurrentRainStateText;
	public Text DevCurrentSnowStateText;
	public Text DevCurrentDisasterLevelText;
	public Text DevCurrentDisasterDroughtDaysText;
	public Text DevCurrentDisasterFloodDaysText;
	public Text DevCurrentDisasterTyphoonDaysText;
	public Text DevCurrentDisasterHeavySnowDaysText;

	[Header("- Foreground Settings -")]
	public GameObject WeatherRainEffect;
	public GameObject WeatherSnowEffect;
	public GameObject DisasterDroughtEffect;
	public GameObject DisasterFloodEffect;
	public GameObject DisasterTyphoonEffect;
	public GameObject DisasterHeavySnowEffect;
	public Animator DayNightCycleAnimator;

	[Header("- Require Components -")]
	public GameSystemCtrl CurrentGameSystemCtrl;

	/* Event Functions */

	void Awake() {
		InitDayNightCycle();
	}

	void FixedUpdate() {

		SetStaticUiValues();
		switch(gameObject.GetComponent<Animator>().GetInteger("State")){
			case -1:
				if(Input.GetKeyDown(KeyCode.Escape)) {
					SetAnim(0);
					CurrentGameSystemCtrl.toggleDayRun(true);
				}
			break;
			case 0:
				SetMainUiValues();
				if(Input.GetKeyDown(KeyCode.Escape)) {
					SetAnim(-1);
					CurrentGameSystemCtrl.toggleDayRun(false);
				}
			break;
			case 1:
				SetDevUiValues();
				if(Input.GetKeyDown(KeyCode.Escape)) {
					SetAnim(0);
				}
			break;
			case 2:
				SetUpgradeUiValues();
				if(Input.GetKeyDown(KeyCode.Escape)) {
					SetAnim(0);
					CurrentGameSystemCtrl.toggleDayRun(true);
				}
			break;
		}

		DayNightCycleAnimator.speed = CurrentGameSystemCtrl.getDayRun()? 1/CurrentGameSystemCtrl.DaySeconds*CurrentGameSystemCtrl.DaySpeed : 0;
	}
	
	/* Event Functions */

	/* Logic Functions */

	public void SetAnim(int state) {
		Animator anim = gameObject.GetComponent<Animator>();
		anim.SetInteger("State", state);
		anim.SetTrigger("Move");
	}

	private void SetStaticUiValues() {
		System.DateTime date = CurrentGameSystemCtrl.getDayDateTime();
		StaticTimeText.text = date.Month + "월 " + date.Day + "일 ";
		StaticCurrentGoldText.text = CurrentGameSystemCtrl.getCurrentGold().ToString("F0");

		WeatherRainEffect.SetActive(CurrentGameSystemCtrl.WeatherRainData.State);
		WeatherSnowEffect.SetActive(CurrentGameSystemCtrl.WeatherSnowData.State);
		DisasterDroughtEffect.SetActive(CurrentGameSystemCtrl.DisasterDroughtData.getLeftDays() > 0);
		DisasterFloodEffect.SetActive(CurrentGameSystemCtrl.DisasterFloodData.getLeftDays() > 0);
		DisasterTyphoonEffect.SetActive(CurrentGameSystemCtrl.DisasterTyphoonData.getLeftDays() > 0);
		DisasterHeavySnowEffect.SetActive(CurrentGameSystemCtrl.DisasterHeavySnowData.getLeftDays() > 0);
	}

	private void SetMainUiValues() {
		MainCurrentPeopleText.text = CurrentGameSystemCtrl.getCurrentPeople() + "/" + CurrentGameSystemCtrl.getCurrentMaxPeople() + " 명";
		MainCurrentPeopleSpawnSecondsPanel.SetActive(CurrentGameSystemCtrl.getCurrentPeopleSeconds() > 0f && CurrentGameSystemCtrl.getCurrentPeople() < CurrentGameSystemCtrl.getCurrentMaxPeople());
		MainCurrentPeopleSpawnSecondsText.text = CurrentGameSystemCtrl.getCurrentPeopleSeconds().ToString("F0") + "s";
	}

	private void SetDevUiValues() {
		DevCurrentVersionText.text = "BlackOut 버전: " + Application.version;
		DevCurrentDayProgressText.text = "하루 진행도: " + (CurrentGameSystemCtrl.getDayProgress() / CurrentGameSystemCtrl.DaySeconds * 100).ToString("F2") + "%";
		DevCurrentDaySurvivesText.text = "진행된 일수: " + CurrentGameSystemCtrl.getDaySurvives() + "일";
		DevCurrentUpgradeBuildingLevelText.text = "건물 레벨: " + CurrentGameSystemCtrl.getUpgradeBuildingLevel();
		DevCurrentUpgradeLandLevelText.text = "토지 레벨: " + CurrentGameSystemCtrl.getUpgradeLandLevel();
		DevCurrentUpgradeFishingLevelText.text = "낚시 레벨: " + CurrentGameSystemCtrl.getUpgradeFishingLevel();
		DevCurrentUpgradeTechLevelText.text = "기술 레벨: " + CurrentGameSystemCtrl.getUpgradeTechLevel();
		DevCurrentGoldText.text = "현재 돈: " + CurrentGameSystemCtrl.getCurrentGold() + "G";
		DevCurrentPeopleText.text = "현재 인구: " + CurrentGameSystemCtrl.getCurrentPeople() + "/" + CurrentGameSystemCtrl.getCurrentMaxPeople() + " 명";
		DevCurrentPeopleSecondsText.text = "인구 증가 시간: " + CurrentGameSystemCtrl.getCurrentPeopleSeconds().ToString("F0") + "s";
		DevCurrentRainStateText.text = "비 상태: " + (CurrentGameSystemCtrl.WeatherRainData.State? "On" : "Off");
		DevCurrentSnowStateText.text = "눈 상태: " + (CurrentGameSystemCtrl.WeatherSnowData.State? "On" : "Off");
		DevCurrentDisasterLevelText.text = "재해 레벨: " + CurrentGameSystemCtrl.getCurrentDisasterLevel();
		DevCurrentDisasterDroughtDaysText.text = "가뭄 시간: " + CurrentGameSystemCtrl.DisasterDroughtData.getLeftDays().ToString("F0") + "일";
		DevCurrentDisasterFloodDaysText.text = "홍수 시간: " + CurrentGameSystemCtrl.DisasterFloodData.getLeftDays().ToString("F0") + "일";
		DevCurrentDisasterTyphoonDaysText.text = "태풍 시간: " + CurrentGameSystemCtrl.DisasterTyphoonData.getLeftDays().ToString("F0") + "일";
		DevCurrentDisasterHeavySnowDaysText.text = "폭설 시간: " + CurrentGameSystemCtrl.DisasterHeavySnowData.getLeftDays().ToString("F0") + "일";
	}

	private void SetUpgradeUiValues() {
		UpgradeCurrentBuildingLevelText.text = CurrentGameSystemCtrl.getUpgradeBuildingLevel().ToString();
		UpgradeCurrentBuildingCostText.text = CurrentGameSystemCtrl.getUpgradeBuildingCost().ToString();
		UpgradeCurrentLandLevelText.text = CurrentGameSystemCtrl.getUpgradeLandLevel().ToString();
		UpgradeCurrentLandCostText.text = CurrentGameSystemCtrl.getUpgradeLandCost().ToString();
		UpgradeCurrentFishingLevelText.text = CurrentGameSystemCtrl.getUpgradeFishingLevel().ToString();
		UpgradeCurrentFishingCostText.text = CurrentGameSystemCtrl.getUpgradeFishingCost().ToString();
		UpgradeCurrentTechLevelText.text = CurrentGameSystemCtrl.getUpgradeTechLevel().ToString();
		UpgradeCurrentTechCostText.text = CurrentGameSystemCtrl.getUpgradeTechCost().ToString();
		UpgradeBuildingButton.interactable = CurrentGameSystemCtrl.getUpgradeBuildingCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeLandButton.interactable = CurrentGameSystemCtrl.getUpgradeLandCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeFishingButton.interactable = CurrentGameSystemCtrl.getUpgradeFishingCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeTechButton.interactable = CurrentGameSystemCtrl.getUpgradeTechCost() < CurrentGameSystemCtrl.getCurrentGold();
	}

	private void InitDayNightCycle() {
		float pr = CurrentGameSystemCtrl.getDayProgress()/CurrentGameSystemCtrl.DaySeconds;
		if(pr>1)
			pr-=Mathf.Floor(pr);
		DayNightCycleAnimator.Play("SpringDayNightCycle", 0, pr);
	}

	/* Logic Functions */
}
