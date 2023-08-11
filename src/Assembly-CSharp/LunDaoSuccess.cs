using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class LunDaoSuccess : MonoBehaviour
{
	[SerializeField]
	private Image siXuCell;

	[SerializeField]
	private Transform siXuCellParent;

	public List<Sprite> siXuList;

	[SerializeField]
	private Image curJinDu;

	[SerializeField]
	private Text jinDu;

	[SerializeField]
	private Text addWuDaoZhiText;

	[SerializeField]
	private Text addWuDaoDian;

	public void Init()
	{
		if (((Component)this).gameObject.activeSelf)
		{
			return;
		}
		((Component)this).gameObject.SetActive(true);
		Avatar player = Tools.instance.getPlayer();
		int npcStateId = LunDaoManager.inst.npcController.npcStateId;
		foreach (int key in LunDaoManager.inst.getWuDaoExp.Keys)
		{
			GameObject obj = Object.Instantiate<GameObject>(((Component)siXuCell).gameObject, siXuCellParent);
			obj.GetComponent<Image>().sprite = siXuList[key - 1];
			obj.SetActive(true);
		}
		AddPlayerLunDaoSiXu();
		int wuDaoZhi = GetWuDaoZhi();
		if (npcStateId != 4)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(LunDaoManager.inst.npcId, wuDaoZhi);
			AddNpcWuDaoExp();
		}
		player.ReduceLingGan(jsonData.instance.LunDaoStateData[player.LunDaoState.ToString()]["LingGanXiaoHao"].I);
		addWuDaoZhiText.text = wuDaoZhi.ToString();
		int wuDaoZhiLevel = player.WuDaoZhiLevel;
		int startWuDaoZhi = player.WuDaoZhi;
		int endExp = 0;
		player.WuDaoZhi += wuDaoZhi;
		player.WuDaoZhiLevel = GetPlayerLevel(wuDaoZhiLevel, player.WuDaoZhi, ref endExp);
		int curMax = jsonData.instance.WuDaoZhiData[wuDaoZhiLevel.ToString()]["LevelUpExp"].I;
		curJinDu.fillAmount = (float)startWuDaoZhi / (float)curMax;
		if (player.WuDaoZhiLevel - wuDaoZhiLevel > 0)
		{
			addWuDaoDian.text = $"悟道点+{player.WuDaoZhiLevel - wuDaoZhiLevel}({player.WuDaoZhiLevel}/{WuDaoZhiData.DataList.Count - 1})";
			player._WuDaoDian += player.WuDaoZhiLevel - wuDaoZhiLevel;
			((Component)addWuDaoDian).gameObject.SetActive(true);
			float time = 0.5f / (float)(player.WuDaoZhiLevel - wuDaoZhiLevel);
			MoreSlider(startWuDaoZhi, player.WuDaoZhi, wuDaoZhiLevel, time);
		}
		else if (startWuDaoZhi > curMax)
		{
			jinDu.text = $"({startWuDaoZhi}/极限)";
		}
		else
		{
			float num = (float)player.WuDaoZhi / (float)curMax;
			DOTweenModuleUI.DOFillAmount(curJinDu, num, 0.5f);
			DOTween.To((DOGetter<int>)(() => startWuDaoZhi), (DOSetter<int>)delegate(int x)
			{
				startWuDaoZhi = x;
				jinDu.text = $"({startWuDaoZhi}/{curMax})";
			}, player.WuDaoZhi, 0.5f);
		}
		player.WuDaoZhi = endExp;
		if (NpcJieSuanManager.inst.lunDaoNpcList.Contains(LunDaoManager.inst.npcId))
		{
			NpcJieSuanManager.inst.lunDaoNpcList.Remove(LunDaoManager.inst.npcId);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(LunDaoManager.inst.npcId, player.name);
	}

	private int GetWuDaoZhi()
	{
		int getWuDaoZhi = LunDaoManager.inst.getWuDaoZhi;
		float f = jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoZhi"].f;
		float f2 = jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoZhi"].f;
		float num = (f + f2) / 100f;
		getWuDaoZhi += (int)((float)getWuDaoZhi * num);
		if (getWuDaoZhi < 0)
		{
			getWuDaoZhi = 0;
		}
		return getWuDaoZhi;
	}

	private int GetPlayerLevel(int curLevel, int curExp, ref int endExp)
	{
		int i = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		int num = curLevel;
		while (curExp >= i)
		{
			num++;
			if (jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				curExp -= i;
				i = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
				continue;
			}
			num--;
			break;
		}
		endExp = curExp;
		return num;
	}

	private void MoreSlider(int curExp, int endExp, int curLevel, float time, bool flag = false)
	{
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		int nextExp = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		if (flag)
		{
			TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTween.To((DOGetter<float>)(() => curJinDu.fillAmount), (DOSetter<float>)delegate(float x)
			{
				curJinDu.fillAmount = x;
			}, (float)endExp / (float)nextExp, time));
			TweenExtensions.Play<TweenerCore<int, int, NoOptions>>(DOTween.To((DOGetter<int>)(() => curExp), (DOSetter<int>)delegate(int x)
			{
				curExp = x;
				jinDu.text = $"({curExp}/{nextExp})";
			}, endExp, time));
			return;
		}
		TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTween.To((DOGetter<float>)(() => curJinDu.fillAmount), (DOSetter<float>)delegate(float x)
		{
			curJinDu.fillAmount = x;
		}, 1f, time));
		TweenExtensions.Play<TweenerCore<int, int, NoOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<int, int, NoOptions>>(DOTween.To((DOGetter<int>)(() => curExp), (DOSetter<int>)delegate(int x)
		{
			curExp = x;
			jinDu.text = $"({curExp}/{nextExp})";
		}, nextExp, time), (TweenCallback)delegate
		{
			if (!flag)
			{
				endExp -= nextExp;
				curLevel++;
				curJinDu.fillAmount = 0f;
				if (jsonData.instance.WuDaoZhiData.HasField(curLevel.ToString()))
				{
					nextExp = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
					if (endExp > nextExp)
					{
						MoreSlider(0, endExp, curLevel, time);
					}
					else
					{
						MoreSlider(0, endExp, curLevel, time, flag: true);
					}
				}
				else
				{
					jinDu.text = $"({curExp}/极限)";
				}
			}
		}));
	}

	private void AddPlayerLunDaoSiXu()
	{
		Avatar player = Tools.instance.getPlayer();
		Dictionary<int, List<int>> getWuDaoExp = LunDaoManager.inst.getWuDaoExp;
		int num = 0;
		int num2 = 0;
		DateTime nowTime = player.worldTimeMag.getNowTime();
		string text = $"{nowTime.Year}年{nowTime.Month}月{nowTime.Day}日";
		string npcName = LunDaoManager.inst.npcController.GetNpcName();
		string text2 = "";
		int i = jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoExp"].I;
		float num3 = jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoExp"].I;
		float num4 = ((float)i + num3) / 100f;
		foreach (int key in getWuDaoExp.Keys)
		{
			num = 0;
			num2 = 0;
			int num5 = 0;
			foreach (int item in getWuDaoExp[key])
			{
				num2 += item;
				num += jsonData.instance.LunDaoShouYiData[item.ToString()]["WuDaoExp"].I;
				num5++;
			}
			if (num5 == 2)
			{
				if (num2 % 2 > 0)
				{
					num2++;
				}
				num2 /= 2;
			}
			else
			{
				num2--;
			}
			num += (int)((float)num * num4);
			if (num > 0)
			{
				text2 = jsonData.instance.WuDaoAllTypeJson[key.ToString()]["name1"].Str;
				int num6 = num / jsonData.instance.LunDaoSiXuData[num2.ToString()]["SiXvXiaoLv"].I;
				int num7 = LingGanTimeMaxData.DataDict[player.level].MaxTime;
				switch (player.GetLunDaoState())
				{
				case 1:
					num7 *= 4;
					break;
				case 2:
					num7 *= 2;
					break;
				}
				if (num6 > num7)
				{
					num6 = num7;
				}
				player.wuDaoMag.AddLingGuang("对" + text2 + "的感悟", key, num6, 1825, "在" + text + "你与" + npcName + "论道时，对" + text2 + "产生了灵光一现的感悟", jsonData.instance.LunDaoSiXuData[num2.ToString()]["PinJie"].I, isLunDao: true);
			}
		}
		if (num4 > -1f)
		{
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
		}
	}

	private void AddNpcWuDaoExp()
	{
		int npcId = LunDaoManager.inst.npcId;
		Dictionary<int, List<int>> getWuDaoExp = LunDaoManager.inst.getWuDaoExp;
		foreach (int key in getWuDaoExp.Keys)
		{
			int num = 0;
			int i = jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoExp"].I;
			float num2 = jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoExp"].I;
			float num3 = ((float)i + num2) / 100f;
			foreach (int item in getWuDaoExp[key])
			{
				num += jsonData.instance.LunDaoShouYiData[item.ToString()]["WuDaoExp"].I;
			}
			num += (int)((float)num * num3);
			if (num >= 0)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, key, num);
			}
		}
	}
}
