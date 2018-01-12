using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Canvas))]
public class GameSceneUiCtrl : MonoBehaviour {

	[Header("- Main Settings -")]
	public Text CurrentGoldText;

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

	[Header("- Dev Settings -")]
	public Text DevCurrentDayProgressText;
	public Text DevCurrentDaySurvivesText;
	public Text DevCurrentUpgradeBuildingLevelText;
	public Text DevCurrentUpgradeLandLevelText;
	public Text DevCurrentUpgradeFishingLevelText;
	public Text DevCurrentUpgradeTechLevelText;
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
			case -1:
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
		CurrentGoldText.text = CurrentGameSystemCtrl.getCurrentGold().ToString("F0");
	}

	private void SetDevUiValues() {
		DevCurrentDayProgressText.text = "하루 진행도: " + (CurrentGameSystemCtrl.getDayProgress() / CurrentGameSystemCtrl.DaySeconds * 100).ToString("F2") + "%";
		DevCurrentDaySurvivesText.text = "진행된 일수: " + CurrentGameSystemCtrl.getDaySurvives() + "일";
		DevCurrentUpgradeBuildingLevelText.text = "건물 레벨: " + CurrentGameSystemCtrl.getUpgradeBuildingLevel();
		DevCurrentUpgradeLandLevelText.text = "토지 레벨: " + CurrentGameSystemCtrl.getUpgradeLandLevel();
		DevCurrentUpgradeFishingLevelText.text = "낚시 레벨: " + CurrentGameSystemCtrl.getUpgradeFishingLevel();
		DevCurrentUpgradeTechLevelText.text = "기술 레벨: " + CurrentGameSystemCtrl.getUpgradeTechLevel();
		DevCurrentGoldText.text = "현재 돈: " + CurrentGameSystemCtrl.getCurrentGold();
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
	}

	private void InitDayNightCycle() {
		DayNightCycleAnimator.speed = 1/CurrentGameSystemCtrl.DaySeconds;
	}

	/* Logic Functions */
}
