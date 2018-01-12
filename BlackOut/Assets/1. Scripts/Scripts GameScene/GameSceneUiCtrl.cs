using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Canvas))]
public class GameSceneUiCtrl : MonoBehaviour {

	[Header("- Main Settings -")]
	public Text MainTimeText;
	public Text MainCurrentGoldText;
	public GameObject MainUpgradeBuildingStatePanel;
	public Text MainUpagradeBuildingSecondsText;
	public Text MainUpagradeLandSecondsText;
	public Text MainUpagradeFishingSecondsText;
	public Text MainUpagradeTechSecondsText;
	public GameObject MainUpgradeLandStatePanel;
	public GameObject MainUpgradeFishingStatePanel;
	public GameObject MainUpgradeTechStatePanel;

	[Header("- Upgrade Settings -")]
	public Text UpgradeCurrentGoldText;
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
	public Text DevCurrentDayProgressText;
	public Text DevCurrentDaySurvivesText;
	public Text DevCurrentUpgradeBuildingLevelText;
	public Text DevCurrentUpgradeLandLevelText;
	public Text DevCurrentUpgradeFishingLevelText;
	public Text DevCurrentUpgradeTechLevelText;
	public Text DevCurrentUpgradeBuildingSecondsText;
	public Text DevCurrentUpgradeLandSecondsText;
	public Text DevCurrentUpgradeFishingSecondsText;
	public Text DevCurrentUpgradeTechSecondsText;
	public Text DevCurrentGoldText;

	[Header("- Day Night Cycle Settings -")]
	public Animator DayNightCycleAnimator;

	[Header("- Require Components -")]
	public GameSystemCtrl CurrentGameSystemCtrl;

	/* Event Functions */

	void Awake() {
		InitDayNightCycle();
	}

	void FixedUpdate() {
		switch(gameObject.GetComponent<Animator>().GetInteger("State")){
			case 0:
				SetMainUiValues();
			break;
			case 1:
				SetDevUiValues();
			break;
			case 2:
				SetUpgradeUiValues();
			break;
		}
	}
	
	/* Event Functions */

	/* Logic Functions */

	public void SetAnim(int state) {
		Animator anim = gameObject.GetComponent<Animator>();
		anim.SetInteger("State", state);
		anim.SetTrigger("Move");
	}

	private void SetMainUiValues() {
		System.DateTime date = CurrentGameSystemCtrl.getDayDateTime();
		MainTimeText.text = date.Month + "월 " + date.Day + "일 " + date.Hour + "시 " + date.Minute + "분";
		MainCurrentGoldText.text = CurrentGameSystemCtrl.getCurrentGold().ToString("F0");
		MainUpgradeBuildingStatePanel.SetActive(CurrentGameSystemCtrl.getUpgradeBuildingState());
		MainUpgradeLandStatePanel.SetActive(CurrentGameSystemCtrl.getUpgradeLandState());
		MainUpgradeFishingStatePanel.SetActive(CurrentGameSystemCtrl.getUpgradeFishingState());
		MainUpgradeTechStatePanel.SetActive(CurrentGameSystemCtrl.getUpgradeTechState());
		MainUpagradeBuildingSecondsText.text = "건물: " + Mathf.Ceil(CurrentGameSystemCtrl.getUpgradeBuildingLeftSeconds()) + "s";
		MainUpagradeLandSecondsText.text = "토지: " + Mathf.Ceil(CurrentGameSystemCtrl.getUpgradeLandLeftSeconds()) + "s";
		MainUpagradeFishingSecondsText.text = "낚시: " + Mathf.Ceil(CurrentGameSystemCtrl.getUpgradeFishingLeftSeconds()) + "s";
		MainUpagradeTechSecondsText.text = "기술: " + Mathf.Ceil(CurrentGameSystemCtrl.getUpgradeTechLeftSeconds()) + "s";
	}

	private void SetDevUiValues() {
		DevCurrentDayProgressText.text = "하루 진행도: " + (CurrentGameSystemCtrl.getDayProgress() / CurrentGameSystemCtrl.DaySeconds * 100).ToString("F2") + "%";
		DevCurrentDaySurvivesText.text = "진행된 일수: " + CurrentGameSystemCtrl.getDaySurvives() + "일";
		DevCurrentUpgradeBuildingLevelText.text = "건물 레벨: " + CurrentGameSystemCtrl.getUpgradeBuildingLevel();
		DevCurrentUpgradeLandLevelText.text = "토지 레벨: " + CurrentGameSystemCtrl.getUpgradeLandLevel();
		DevCurrentUpgradeFishingLevelText.text = "낚시 레벨: " + CurrentGameSystemCtrl.getUpgradeFishingLevel();
		DevCurrentUpgradeTechLevelText.text = "기술 레벨: " + CurrentGameSystemCtrl.getUpgradeTechLevel();
		DevCurrentGoldText.text = "현재 돈: " + CurrentGameSystemCtrl.getCurrentGold();
		DevCurrentUpgradeBuildingSecondsText.text = "건물 업그레이드 시간: " + CurrentGameSystemCtrl.getUpgradeBuildingLeftSeconds().ToString("F2") + "s";
		DevCurrentUpgradeLandSecondsText.text = "토지 업그레이드 시간: " + CurrentGameSystemCtrl.getUpgradeLandLeftSeconds().ToString("F2") + "s";
		DevCurrentUpgradeFishingSecondsText.text = "낚시 업그레이드 시간: " + CurrentGameSystemCtrl.getUpgradeFishingLeftSeconds().ToString("F2") + "s";
		DevCurrentUpgradeTechSecondsText.text = "기술 업그레이드 시간: " + CurrentGameSystemCtrl.getUpgradeTechLeftSeconds().ToString("F2") + "s";
	}

	private void SetUpgradeUiValues() {
		UpgradeCurrentGoldText.text = CurrentGameSystemCtrl.getCurrentGold().ToString("F0");
		UpgradeCurrentBuildingLevelText.text = CurrentGameSystemCtrl.getUpgradeBuildingLevel().ToString();
		UpgradeCurrentBuildingCostText.text = CurrentGameSystemCtrl.getUpgradeBuildingCost().ToString();
		UpgradeCurrentLandLevelText.text = CurrentGameSystemCtrl.getUpgradeLandLevel().ToString();
		UpgradeCurrentLandCostText.text = CurrentGameSystemCtrl.getUpgradeLandCost().ToString();
		UpgradeCurrentFishingLevelText.text = CurrentGameSystemCtrl.getUpgradeFishingLevel().ToString();
		UpgradeCurrentFishingCostText.text = CurrentGameSystemCtrl.getUpgradeFishingCost().ToString();
		UpgradeCurrentTechLevelText.text = CurrentGameSystemCtrl.getUpgradeTechLevel().ToString();
		UpgradeCurrentTechCostText.text = CurrentGameSystemCtrl.getUpgradeTechCost().ToString();
		UpgradeBuildingButton.interactable = !CurrentGameSystemCtrl.getUpgradeBuildingState() && CurrentGameSystemCtrl.getUpgradeBuildingCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeLandButton.interactable = !CurrentGameSystemCtrl.getUpgradeLandState() && CurrentGameSystemCtrl.getUpgradeLandCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeFishingButton.interactable = !CurrentGameSystemCtrl.getUpgradeFishingState() && CurrentGameSystemCtrl.getUpgradeFishingCost() < CurrentGameSystemCtrl.getCurrentGold();
		UpgradeTechButton.interactable = !CurrentGameSystemCtrl.getUpgradeTechState() && CurrentGameSystemCtrl.getUpgradeTechCost() < CurrentGameSystemCtrl.getCurrentGold();
	}

	private void InitDayNightCycle() {
		DayNightCycleAnimator.speed = 1/CurrentGameSystemCtrl.DaySeconds;
		float pr = CurrentGameSystemCtrl.getDayProgress()/CurrentGameSystemCtrl.DaySeconds;
		if(pr>1)
			pr-=Mathf.Floor(pr);
		DayNightCycleAnimator.Play("SpringDayNightCycle", 0, pr);
	}

	/* Logic Functions */
}
