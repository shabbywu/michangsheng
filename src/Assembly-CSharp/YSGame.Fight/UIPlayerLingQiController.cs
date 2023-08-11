using System.Collections.Generic;
using UnityEngine;

namespace YSGame.Fight;

public class UIPlayerLingQiController : MonoBehaviour
{
	public List<UIFightLingQiPlayerSlot> SlotList;

	public Sprite CountBGSmall;

	public Sprite CountBGBig;

	public GameObject MoBG;

	private float refreshCD;

	private void Update()
	{
	}

	public void ResetPlayerLingQiCount()
	{
		for (int i = 0; i < 6; i++)
		{
			int num = PlayerEx.Player.cardMag[i];
			if (SlotList[i].LingQiCount != num)
			{
				SlotList[i].LingQiCount = num;
			}
		}
	}

	public int GetPlayerLingQiSum()
	{
		ResetPlayerLingQiCount();
		int num = 0;
		for (int i = 0; i < 6; i++)
		{
			num += PlayerEx.Player.cardMag[i];
		}
		return num;
	}

	public UIFightLingQiPlayerSlot GetTargetLingQiSlot(LingQiType lingQiType)
	{
		return SlotList[(int)lingQiType];
	}

	public void RefreshLingQiCount(bool show)
	{
		refreshCD = 1f;
		if (show)
		{
			for (int i = 0; i < 6; i++)
			{
				SlotList[i].LingQiCountText.text = SlotList[i].LingQiCount.ToString();
				SlotList[i].SetLingQiCountShow(show: true);
			}
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				SlotList[j].SetLingQiCountShow(show: false);
			}
		}
		Dictionary<LingQiType, int> nowCacheLingQi = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQi();
		for (int k = 0; k < 6; k++)
		{
			if (UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
			{
				if (nowCacheLingQi.ContainsKey(SlotList[k].LingQiType))
				{
					SlotList[k].HighlightObj.SetActive(true);
				}
				else
				{
					SlotList[k].HighlightObj.SetActive(false);
				}
			}
			else
			{
				SlotList[k].HighlightObj.SetActive(false);
			}
		}
	}
}
