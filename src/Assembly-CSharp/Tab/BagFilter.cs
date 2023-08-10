using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tab;

public class BagFilter : MonoBehaviour
{
	public Animator FilterAnimator;

	public Animator LianZiAnimator;

	public List<UIInvFilterBtn> BigFilterBtnList;

	public List<UIInvFilterBtn> FilterBtnList;

	public int BigTypeIndex;

	public int SmallTypeIndex;

	public int BigTypeMax = 4;

	public bool CanSort = true;

	public int SmallTypeMax = 12;

	public UIInvFilterBtn CurSelectBigBtn;

	private void Awake()
	{
		StopAllAn();
		CanSort = true;
	}

	public void ResetData()
	{
		BigTypeIndex = 0;
		CloseSmallSelect();
		foreach (UIInvFilterBtn bigFilterBtn in BigFilterBtnList)
		{
			((Component)bigFilterBtn).gameObject.SetActive(false);
		}
		CurSelectBigBtn = null;
	}

	public void AddBigTypeBtn(UnityAction call, string Name)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		if (BigTypeIndex >= BigTypeMax)
		{
			Debug.LogError((object)"已经超过最大上限");
			return;
		}
		BigFilterBtnList[BigTypeIndex].ClearListener();
		UIInvFilterBtn BigFilterBtn = BigFilterBtnList[BigTypeIndex];
		BigFilterBtn.DeafaultName = Name;
		BigFilterBtn.AddClickEvent((UnityAction)delegate
		{
			if ((Object)(object)CurSelectBigBtn != (Object)null)
			{
				CloseSmallSelect();
				if ((Object)(object)CurSelectBigBtn == (Object)(object)BigFilterBtn)
				{
					StopAllAn();
					PlayHideAn();
					CurSelectBigBtn = null;
					return;
				}
			}
			StopAllAn();
			PlayShowAn();
			CurSelectBigBtn = BigFilterBtn;
			call.Invoke();
			BigFilterBtn.State = UIInvFilterBtn.BtnState.Normal;
		});
		BigFilterBtnList[BigTypeIndex].ShowText.SetText(Name);
		((Component)BigFilterBtnList[BigTypeIndex]).gameObject.SetActive(true);
		BigTypeIndex++;
	}

	public void AddSmallTypeBtn(UnityAction call, string Name)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		if (SmallTypeIndex >= SmallTypeMax)
		{
			Debug.LogError((object)"已经超过最大上限");
			return;
		}
		UIInvFilterBtn smallTypeBtn = FilterBtnList[SmallTypeIndex];
		smallTypeBtn.ClearListener();
		smallTypeBtn.AddClickEvent((UnityAction)delegate
		{
			call.Invoke();
			if (smallTypeBtn.ShowText.text == "全部")
			{
				CurSelectBigBtn.ShowText.SetText(CurSelectBigBtn.DeafaultName);
			}
			else
			{
				CurSelectBigBtn.ShowText.SetText(smallTypeBtn.ShowText.text);
			}
			StopAllAn();
			PlayHideAn();
			CurSelectBigBtn = null;
		});
		if (CurSelectBigBtn.ShowText.text == Name)
		{
			smallTypeBtn.State = UIInvFilterBtn.BtnState.Active;
		}
		else
		{
			smallTypeBtn.State = UIInvFilterBtn.BtnState.Normal;
		}
		smallTypeBtn.ShowText.SetText(Name);
		((Component)smallTypeBtn).gameObject.SetActive(true);
		SmallTypeIndex++;
	}

	public void PlayShowAn()
	{
		((Behaviour)LianZiAnimator).enabled = true;
		((Behaviour)FilterAnimator).enabled = true;
		FilterAnimator.Play("Show");
		LianZiAnimator.Play("Show");
	}

	public void PlayHideAn()
	{
		((Behaviour)LianZiAnimator).enabled = true;
		((Behaviour)FilterAnimator).enabled = true;
		FilterAnimator.Play("Hide");
		LianZiAnimator.Play("Hide");
	}

	public void StopAllAn()
	{
		((Behaviour)FilterAnimator).enabled = false;
	}

	public void CloseSmallSelect()
	{
		foreach (UIInvFilterBtn filterBtn in FilterBtnList)
		{
			((Component)filterBtn).gameObject.SetActive(false);
			filterBtn.State = UIInvFilterBtn.BtnState.Normal;
		}
		SmallTypeIndex = 0;
	}

	public void Sort(UnityAction action = null)
	{
		if (CanSort)
		{
			CanSort = false;
			PlayShowAn();
			PlayerEx.Player.SortItem();
			if (action != null)
			{
				action.Invoke();
			}
			((MonoBehaviour)this).Invoke("SortEnd", 0.5f);
		}
	}

	public void SortEnd()
	{
		PlayHideAn();
		CanSort = true;
	}
}
