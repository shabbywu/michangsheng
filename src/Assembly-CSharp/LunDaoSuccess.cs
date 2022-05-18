using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000481 RID: 1153
public class LunDaoSuccess : MonoBehaviour
{
	// Token: 0x06001ED0 RID: 7888 RVA: 0x001098CC File Offset: 0x00107ACC
	public void Init()
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		base.gameObject.SetActive(true);
		Avatar player = Tools.instance.getPlayer();
		int npcStateId = LunDaoManager.inst.npcController.npcStateId;
		foreach (int num in LunDaoManager.inst.getWuDaoExp.Keys)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.siXuCell.gameObject, this.siXuCellParent);
			gameObject.GetComponent<Image>().sprite = this.siXuList[num - 1];
			gameObject.SetActive(true);
		}
		this.AddPlayerLunDaoSiXu();
		int wuDaoZhi = this.GetWuDaoZhi();
		if (npcStateId != 4)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoZhi(LunDaoManager.inst.npcId, wuDaoZhi);
			this.AddNpcWuDaoExp();
		}
		player.ReduceLingGan(jsonData.instance.LunDaoStateData[player.LunDaoState.ToString()]["LingGanXiaoHao"].I);
		this.addWuDaoZhiText.text = wuDaoZhi.ToString();
		int wuDaoZhiLevel = player.WuDaoZhiLevel;
		int startWuDaoZhi = player.WuDaoZhi;
		int wuDaoZhi2 = 0;
		player.WuDaoZhi += wuDaoZhi;
		player.WuDaoZhiLevel = this.GetPlayerLevel(wuDaoZhiLevel, player.WuDaoZhi, ref wuDaoZhi2);
		int curMax = jsonData.instance.WuDaoZhiData[wuDaoZhiLevel.ToString()]["LevelUpExp"].I;
		this.curJinDu.fillAmount = (float)startWuDaoZhi / (float)curMax;
		if (player.WuDaoZhiLevel - wuDaoZhiLevel > 0)
		{
			this.addWuDaoDian.text = string.Format("悟道点+{0}({1}/{2})", player.WuDaoZhiLevel - wuDaoZhiLevel, player.WuDaoZhiLevel, WuDaoZhiData.DataList.Count - 1);
			player._WuDaoDian += player.WuDaoZhiLevel - wuDaoZhiLevel;
			this.addWuDaoDian.gameObject.SetActive(true);
			float time = 0.5f / (float)(player.WuDaoZhiLevel - wuDaoZhiLevel);
			this.MoreSlider(startWuDaoZhi, player.WuDaoZhi, wuDaoZhiLevel, time, false);
		}
		else if (startWuDaoZhi > curMax)
		{
			this.jinDu.text = string.Format("({0}/极限)", startWuDaoZhi);
		}
		else
		{
			float num2 = (float)player.WuDaoZhi / (float)curMax;
			DOTweenModuleUI.DOFillAmount(this.curJinDu, num2, 0.5f);
			DOTween.To(() => startWuDaoZhi, delegate(int x)
			{
				startWuDaoZhi = x;
				this.jinDu.text = string.Format("({0}/{1})", startWuDaoZhi, curMax);
			}, player.WuDaoZhi, 0.5f);
		}
		player.WuDaoZhi = wuDaoZhi2;
		if (NpcJieSuanManager.inst.lunDaoNpcList.Contains(LunDaoManager.inst.npcId))
		{
			NpcJieSuanManager.inst.lunDaoNpcList.Remove(LunDaoManager.inst.npcId);
		}
		NpcJieSuanManager.inst.npcNoteBook.NoteLunDaoSuccess(LunDaoManager.inst.npcId, player.name);
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00109C08 File Offset: 0x00107E08
	private int GetWuDaoZhi()
	{
		int num = LunDaoManager.inst.getWuDaoZhi;
		float f = jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoZhi"].f;
		float f2 = jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoZhi"].f;
		float num2 = (f + f2) / 100f;
		num += (int)((float)num * num2);
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00109CA0 File Offset: 0x00107EA0
	private int GetPlayerLevel(int curLevel, int curExp, ref int endExp)
	{
		int i = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		int num = curLevel;
		while (curExp >= i)
		{
			num++;
			if (!jsonData.instance.WuDaoZhiData.HasField(num.ToString()))
			{
				num--;
				break;
			}
			curExp -= i;
			i = jsonData.instance.WuDaoZhiData[num.ToString()]["LevelUpExp"].I;
		}
		endExp = curExp;
		return num;
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00109D30 File Offset: 0x00107F30
	private void MoreSlider(int curExp, int endExp, int curLevel, float time, bool flag = false)
	{
		int nextExp = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
		if (flag)
		{
			TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.curJinDu.fillAmount, delegate(float x)
			{
				this.curJinDu.fillAmount = x;
			}, (float)endExp / (float)nextExp, time));
			TweenExtensions.Play<TweenerCore<int, int, NoOptions>>(DOTween.To(() => curExp, delegate(int x)
			{
				curExp = x;
				this.jinDu.text = string.Format("({0}/{1})", curExp, nextExp);
			}, endExp, time));
			return;
		}
		TweenExtensions.Play<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.curJinDu.fillAmount, delegate(float x)
		{
			this.curJinDu.fillAmount = x;
		}, 1f, time));
		TweenExtensions.Play<TweenerCore<int, int, NoOptions>>(TweenSettingsExtensions.OnComplete<TweenerCore<int, int, NoOptions>>(DOTween.To(() => curExp, delegate(int x)
		{
			curExp = x;
			this.jinDu.text = string.Format("({0}/{1})", curExp, nextExp);
		}, nextExp, time), delegate()
		{
			if (flag)
			{
				return;
			}
			endExp -= nextExp;
			int curLevel2 = curLevel;
			curLevel = curLevel2 + 1;
			this.curJinDu.fillAmount = 0f;
			if (!jsonData.instance.WuDaoZhiData.HasField(curLevel.ToString()))
			{
				this.jinDu.text = string.Format("({0}/极限)", curExp);
				return;
			}
			nextExp = jsonData.instance.WuDaoZhiData[curLevel.ToString()]["LevelUpExp"].I;
			if (endExp > nextExp)
			{
				this.MoreSlider(0, endExp, curLevel, time, false);
				return;
			}
			this.MoreSlider(0, endExp, curLevel, time, true);
		}));
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x00109E7C File Offset: 0x0010807C
	private void AddPlayerLunDaoSiXu()
	{
		Avatar player = Tools.instance.getPlayer();
		Dictionary<int, List<int>> getWuDaoExp = LunDaoManager.inst.getWuDaoExp;
		int num = 0;
		int num2 = 0;
		DateTime nowTime = player.worldTimeMag.getNowTime();
		string text = string.Format("{0}年{1}月{2}日", nowTime.Year, nowTime.Month, nowTime.Day);
		string npcName = LunDaoManager.inst.npcController.GetNpcName();
		float i = (float)jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoExp"].I;
		float num3 = (float)jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoExp"].I;
		float num4 = (i + num3) / 100f;
		foreach (int num5 in getWuDaoExp.Keys)
		{
			num = 0;
			num2 = 0;
			int num6 = 0;
			foreach (int num7 in getWuDaoExp[num5])
			{
				num2 += num7;
				num += jsonData.instance.LunDaoShouYiData[num7.ToString()]["WuDaoExp"].I;
				num6++;
			}
			if (num6 == 2)
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
				string str = jsonData.instance.WuDaoAllTypeJson[num5.ToString()]["name1"].Str;
				int num8 = num / jsonData.instance.LunDaoSiXuData[num2.ToString()]["SiXvXiaoLv"].I;
				int num9 = LingGanTimeMaxData.DataDict[(int)player.level].MaxTime;
				int lunDaoState = player.GetLunDaoState();
				if (lunDaoState == 1)
				{
					num9 *= 4;
				}
				else if (lunDaoState == 2)
				{
					num9 *= 2;
				}
				if (num8 > num9)
				{
					num8 = num9;
				}
				player.wuDaoMag.AddLingGuang("对" + str + "的感悟", num5, num8, 1825, string.Concat(new string[]
				{
					"在",
					text,
					"你与",
					npcName,
					"论道时，对",
					str,
					"产生了灵光一现的感悟"
				}), jsonData.instance.LunDaoSiXuData[num2.ToString()]["PinJie"].I, true);
			}
		}
		if (num4 > -1f)
		{
			UIPopTip.Inst.Pop("获得新的感悟", PopTipIconType.感悟);
		}
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x0010A19C File Offset: 0x0010839C
	private void AddNpcWuDaoExp()
	{
		int npcId = LunDaoManager.inst.npcId;
		Dictionary<int, List<int>> getWuDaoExp = LunDaoManager.inst.getWuDaoExp;
		foreach (int num in getWuDaoExp.Keys)
		{
			int num2 = 0;
			float i = (float)jsonData.instance.LunDaoStateData[LunDaoManager.inst.npcController.npcStateId.ToString()]["WuDaoExp"].I;
			float num3 = (float)jsonData.instance.LunDaoStateData[LunDaoManager.inst.playerController.playerStateId.ToString()]["WuDaoExp"].I;
			float num4 = (i + num3) / 100f;
			foreach (int num5 in getWuDaoExp[num])
			{
				num2 += jsonData.instance.LunDaoShouYiData[num5.ToString()]["WuDaoExp"].I;
			}
			num2 += (int)((float)num2 * num4);
			if (num2 >= 0)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcWuDaoExp(npcId, num, num2);
			}
		}
	}

	// Token: 0x04001A3C RID: 6716
	[SerializeField]
	private Image siXuCell;

	// Token: 0x04001A3D RID: 6717
	[SerializeField]
	private Transform siXuCellParent;

	// Token: 0x04001A3E RID: 6718
	public List<Sprite> siXuList;

	// Token: 0x04001A3F RID: 6719
	[SerializeField]
	private Image curJinDu;

	// Token: 0x04001A40 RID: 6720
	[SerializeField]
	private Text jinDu;

	// Token: 0x04001A41 RID: 6721
	[SerializeField]
	private Text addWuDaoZhiText;

	// Token: 0x04001A42 RID: 6722
	[SerializeField]
	private Text addWuDaoDian;
}
