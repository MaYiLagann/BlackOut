using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Canvas))]
public class GameSceneUiCtrl : MonoBehaviour {

	[Header("- Dev Settings -")]
	public Text CurrentDayProgressText;
	public Text CurrentDaySurvivesText;
	public Text CurrentUpgradeBuildingLevelText;
	public Text CurrentUpgradeLandLevelText;
	public Text CurrentUpgradeFishingLevelText;
	public Text CurrentUpgradeTechLevelText;
	public Text CurrentGoldText;

	[Header("- Day Night Cycle Settings -")]
	public Animator DayNightCycleAnimator;

	[Header("- Require Components -")]
	public GameSystemCtrl CurrentGameSystemCtrl;

	/* Event Functions */

	void Awake() {
		SetDayNightCycle();
	}

	void FixedUpdate() {
		SetUiValues();
	}
	
	/* Event Functions */

	/* Logic Functions */

	public void SetAnim(int state) {
		Animator anim = gameObject.GetComponent<Animator>();
		anim.SetInteger("State", state);
		anim.SetTrigger("Move");
	}

	private void SetUiValues() {
		// Develop Only
		CurrentDayProgressText.text = "하루 진행도: " + (CurrentGameSystemCtrl.getDayProgress() / CurrentGameSystemCtrl.DaySeconds * 100).ToString("F2") + "%";
		CurrentDaySurvivesText.text = "진행된 일수: " + CurrentGameSystemCtrl.getDaySurvives() + "일";
		CurrentUpgradeBuildingLevelText.text = "건물 레벨: " + CurrentGameSystemCtrl.getUpgradeBuildingLevel();
		CurrentUpgradeLandLevelText.text = "토지 레벨: " + CurrentGameSystemCtrl.getUpgradeLandLevel();
		CurrentUpgradeFishingLevelText.text = "낚시 레벨: " + CurrentGameSystemCtrl.getUpgradeFishingLevel();
		CurrentUpgradeTechLevelText.text = "기술 레벨: " + CurrentGameSystemCtrl.getUpgradeTechLevel();
		CurrentGoldText.text = "현재 돈: " + CurrentGameSystemCtrl.getCurrentGold();
	}

	private void SetDayNightCycle() {
		DayNightCycleAnimator.speed = 1/CurrentGameSystemCtrl.DaySeconds;
	}

	/* Logic Functions */
}
