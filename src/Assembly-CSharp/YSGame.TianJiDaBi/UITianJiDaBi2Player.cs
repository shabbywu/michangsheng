using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi;

public class UITianJiDaBi2Player : MonoBehaviour
{
	public UITianJiDaBiFire LeftFire;

	public UITianJiDaBiFire RightFire;

	public GameObject LeftNameHighlight;

	public GameObject RightNameHiglight;

	public FpBtn LeftNameBtn;

	public FpBtn RightNameBtn;

	public Text LeftName;

	public Text RightName;

	public Text LeftWinFail;

	public Text RightWinFail;

	public Animator JianAnimCtl;

	private Color winColor = new Color(11f / 15f, 0.38431373f, 10f / 51f);

	private Color failColor = new Color(26f / 85f, 0.39607844f, 0.49803922f);

	private bool canClickName = true;

	public void InitData(DaBiPlayer left, DaBiPlayer right, Match match)
	{
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Expected O, but got Unknown
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		LeftName.text = left.Name;
		RightName.text = right.Name;
		LeftFire.NumberText.text = left.BigScore.ToString();
		RightFire.NumberText.text = right.BigScore.ToString();
		LeftWinFail.text = "";
		RightWinFail.text = "";
		((Component)LeftWinFail).gameObject.SetActive(false);
		((Component)RightWinFail).gameObject.SetActive(false);
		LeftFire.SetNormal();
		RightFire.SetNormal();
		JianAnimCtl.Play("UITianJiDaBiJianIdleAnim");
		if (left.IsWanJia)
		{
			((Graphic)LeftName).color = new Color(4f / 51f, 0.39607844f, 0.3529412f);
		}
		if (right.IsWanJia)
		{
			((Graphic)RightName).color = new Color(4f / 51f, 0.39607844f, 0.3529412f);
		}
		((UnityEventBase)LeftNameBtn.mouseEnterEvent).RemoveAllListeners();
		((UnityEventBase)RightNameBtn.mouseEnterEvent).RemoveAllListeners();
		((UnityEventBase)LeftNameBtn.mouseOutEvent).RemoveAllListeners();
		((UnityEventBase)RightNameBtn.mouseOutEvent).RemoveAllListeners();
		((UnityEventBase)LeftNameBtn.mouseUpEvent).RemoveAllListeners();
		((UnityEventBase)RightNameBtn.mouseUpEvent).RemoveAllListeners();
		LeftNameBtn.mouseEnterEvent.AddListener((UnityAction)delegate
		{
			if (canClickName)
			{
				LeftNameHighlight.SetActive(true);
			}
		});
		RightNameBtn.mouseEnterEvent.AddListener((UnityAction)delegate
		{
			if (canClickName)
			{
				RightNameHiglight.SetActive(true);
			}
		});
		LeftNameBtn.mouseOutEvent.AddListener((UnityAction)delegate
		{
			LeftNameHighlight.SetActive(false);
		});
		RightNameBtn.mouseOutEvent.AddListener((UnityAction)delegate
		{
			RightNameHiglight.SetActive(false);
		});
		LeftNameBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (canClickName)
			{
				UITianJiDaBiPlayerInfo.Show(left, match);
			}
		});
		RightNameBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (canClickName)
			{
				UITianJiDaBiPlayerInfo.Show(right, match);
			}
		});
	}

	public void SetWinFail(DaBiPlayer left, DaBiPlayer right)
	{
		FightRecord fightRecord = left.FightRecords[left.FightRecords.Count - 1];
		FightRecord fightRecord2 = right.FightRecords[right.FightRecords.Count - 1];
		SetLeftWinFail(fightRecord.WinID == left.ID);
		SetRightWinFail(fightRecord2.WinID == right.ID);
		JianAnimCtl.Play("UITianJiDaBiJianEndAnim");
	}

	public void SetLeftWinFail(bool win, bool showText = true)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (win)
		{
			LeftWinFail.text = "胜";
			((Graphic)LeftWinFail).color = winColor;
			LeftFire.SetWin();
		}
		else
		{
			LeftWinFail.text = "负";
			((Graphic)LeftWinFail).color = failColor;
			LeftFire.SetFail();
		}
		((Component)LeftWinFail).gameObject.SetActive(showText);
	}

	public void SetRightWinFail(bool win, bool showText = true)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (win)
		{
			RightWinFail.text = "胜";
			((Graphic)RightWinFail).color = winColor;
			RightFire.SetWin();
		}
		else
		{
			RightWinFail.text = "负";
			((Graphic)RightWinFail).color = failColor;
			RightFire.SetFail();
		}
		((Component)RightWinFail).gameObject.SetActive(showText);
	}

	public void PlayFightAnim(Action onAnimEnd)
	{
		((MonoBehaviour)this).StartCoroutine(FightAnimC(onAnimEnd));
	}

	private IEnumerator FightAnimC(Action onAnimEnd)
	{
		canClickName = false;
		float num = Random.Range(0f, 1f);
		yield return (object)new WaitForSeconds(num);
		JianAnimCtl.Play("UITianJiDaBiJianFightAnim");
		LeftFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
		RightFire.FireAnim.Play("UITianJiDaBiFireFightAnim");
		yield return (object)new WaitForSeconds(2f);
		onAnimEnd?.Invoke();
		canClickName = true;
	}
}
