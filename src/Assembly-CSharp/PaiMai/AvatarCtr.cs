using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai;

public class AvatarCtr : MonoBehaviour
{
	public List<PaiMaiAvatar> AvatarList;

	private int index;

	public void AllAvatarThinkItem()
	{
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			avatar.ThinKCurShop();
		}
	}

	public void AvatarStart()
	{
		index = 0;
		((MonoBehaviour)this).Invoke("AvatarAddPrice", 0.75f);
	}

	private void AvatarAddPrice()
	{
		if (index >= AvatarList.Count)
		{
			((MonoBehaviour)this).Invoke("EndRound", 0.5f);
			return;
		}
		if (AvatarList[index].State == PaiMaiAvatar.StateType.放弃)
		{
			SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = AvatarList[index];
			index++;
			AvatarAddPrice();
			return;
		}
		SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = AvatarList[index];
		SingletonMono<PaiMaiUiMag>.Instance.AddPrice();
		index++;
		if (index >= AvatarList.Count)
		{
			((MonoBehaviour)this).Invoke("EndRound", 0.5f);
		}
		else
		{
			((MonoBehaviour)this).Invoke("AvatarAddPrice", 0.75f);
		}
	}

	public void AvatarSayWord()
	{
		int num = 0;
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			if (avatar.State == PaiMaiAvatar.StateType.势在必得)
			{
				num = SingletonMono<PaiMaiUiMag>.Instance.WordDict[(int)avatar.State * 10][Tools.instance.GetRandomInt(0, SingletonMono<PaiMaiUiMag>.Instance.WordDict[(int)avatar.State * 10].Count - 1)];
				avatar.UiCtr.SayWord(PaiMaiDuiHuaBiao.DataDict[num].Text);
			}
		}
	}

	public bool IsAllGiveUp()
	{
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			if (avatar.State != PaiMaiAvatar.StateType.放弃)
			{
				return false;
			}
		}
		return true;
	}

	public void AddTagetStateMaxPrice(PaiMaiAvatar curAvatar, PaiMaiAvatar.StateType stateType, int lv, float precent)
	{
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			if ((stateType == PaiMaiAvatar.StateType.所有状态 || avatar.State == stateType) && (curAvatar.IsPlayer || curAvatar != avatar) && Tools.instance.GetRandomInt(0, 100) <= lv)
			{
				avatar.AddMaxMoney(precent);
			}
		}
	}

	public void SetCanSelect(CeLueType ceLueType)
	{
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			if (avatar.State != PaiMaiAvatar.StateType.放弃)
			{
				avatar.CanSelect = true;
			}
		}
	}

	public void StopSelect()
	{
		foreach (PaiMaiAvatar avatar in AvatarList)
		{
			avatar.CanSelect = false;
		}
	}

	private void EndRound()
	{
		SingletonMono<PaiMaiUiMag>.Instance.EndRound();
	}
}
