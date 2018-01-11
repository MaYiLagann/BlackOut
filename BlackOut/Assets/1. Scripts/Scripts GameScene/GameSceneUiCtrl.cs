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

	[Header("- Require Components -")]
	public GameSystemCtrl CurrentGameSystemCtrl;

	/* Event Functions */

	void Update() {
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
		CurrentDayProgressText.text = "하루 진행도: " + (CurrentGameSystemCtrl.getDayProgress() / CurrentGameSystemCtrl.DaySeconds * 100).ToString("F2") + "%";
		CurrentDaySurvivesText.text = "진행된 일수: " + CurrentGameSystemCtrl.getDaySurvives() + "일";
	}

	/* Logic Functions */
}
