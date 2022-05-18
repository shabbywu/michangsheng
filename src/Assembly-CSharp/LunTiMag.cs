using System;
using System.Collections.Generic;

// Token: 0x02000475 RID: 1141
public class LunTiMag
{
	// Token: 0x06001E92 RID: 7826 RVA: 0x0001958C File Offset: 0x0001778C
	public LunTiMag()
	{
		this.curLunDianList = new List<LunDaoQiu>();
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x001086BC File Offset: 0x001068BC
	public void CreateLunTi(List<int> lunTiList, int npcId)
	{
		this.targetLunTiDictionary = new Dictionary<int, List<int>>();
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"];
		foreach (int num in lunTiList)
		{
			int num2 = jsonobject[num.ToString()]["level"].I + 1;
			int num3 = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(num) + 1;
			if (num2 == num3)
			{
				this.targetLunTiDictionary.Add(num, new List<int>
				{
					num2 + 1
				});
			}
			else if (num2 > num3)
			{
				this.targetLunTiDictionary.Add(num, new List<int>
				{
					num2,
					num3
				});
			}
			else
			{
				this.targetLunTiDictionary.Add(num, new List<int>
				{
					num3,
					num2
				});
			}
		}
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x001087DC File Offset: 0x001069DC
	public bool CheckCanHeCheng(ref int minIndex, ref int bigIndex)
	{
		if (this.curLunDianList.Count < 1)
		{
			return false;
		}
		for (int i = 0; i < this.curLunDianList.Count; i++)
		{
			if (!this.curLunDianList[i].isNull)
			{
				for (int j = this.curLunDianList.Count - 1; j > i; j--)
				{
					if (this.curLunDianList[i].wudaoId == this.curLunDianList[j].wudaoId && this.curLunDianList[i].level == this.curLunDianList[j].level)
					{
						minIndex = i;
						bigIndex = j;
						return true;
					}
				}
			}
		}
		minIndex = -1;
		bigIndex = -1;
		return false;
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x00108894 File Offset: 0x00106A94
	private bool CheckIsTargetLunTi(ref int wuDaoId)
	{
		foreach (int num in this.targetLunTiDictionary.Keys)
		{
			int num2 = 0;
			foreach (int num3 in this.targetLunTiDictionary[num])
			{
				foreach (LunDaoQiu lunDaoQiu in this.curLunDianList)
				{
					if (!lunDaoQiu.isNull && lunDaoQiu.wudaoId == num && lunDaoQiu.level == num3)
					{
						num2++;
						break;
					}
				}
			}
			if (num2 == this.targetLunTiDictionary[num].Count)
			{
				wuDaoId = num;
				return true;
			}
		}
		wuDaoId = -1;
		return false;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x001089B4 File Offset: 0x00106BB4
	public bool CheckIsTargetLunTi()
	{
		int num = -1;
		return this.CheckIsTargetLunTi(ref num);
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x001089CC File Offset: 0x00106BCC
	public void CompleteLunTi()
	{
		int num = 0;
		if (this.CheckIsTargetLunTi(ref num))
		{
			LunDaoManager.inst.hasCompleteLunTi.Add(num);
			LunDaoManager.inst.AddWuDaoExp(num);
			int num2 = 0;
			foreach (int num3 in this.targetLunTiDictionary[num])
			{
				num2 += jsonData.instance.WuDaoZhiJiaCheng[num3.ToString()]["JiaCheng"].I;
			}
			LunDaoManager.inst.AddWuDaoZhi(num, num2);
			this.targetLunTiDictionary.Remove(num);
			LunDaoManager.inst.lunDaoAmrMag.AddCompleteLunTi(LunDaoManager.inst.lunDaoPanel.lunTiCtrDictionary[num].finshIBg, LunDaoManager.inst.lunDaoPanel.lunTiCtrDictionary[num].finshImage);
		}
		if (this.targetLunTiDictionary.Keys.Count < 1)
		{
			LunDaoManager.inst.gameState = LunDaoManager.GameState.论道结束;
			LunDaoManager.inst.GameOver();
		}
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x00108AF8 File Offset: 0x00106CF8
	public void LunDianHeCheng()
	{
		int index = -1;
		int index2 = -1;
		while (this.CheckCanHeCheng(ref index, ref index2))
		{
			this.curLunDianList[index].LevelUp();
			int i = jsonData.instance.LunDaoShouYiData[this.curLunDianList[index].level.ToString()]["WuDaoZhi"].I;
			LunDaoManager.inst.AddWuDaoZhi(this.curLunDianList[index].wudaoId, i);
			this.curLunDianList[index2].SetNull();
			LunDaoManager.inst.lunDaoAmrMag.AddHeCheng(this.curLunDianList[index].transform);
			this.CompleteLunTi();
		}
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x00108BB8 File Offset: 0x00106DB8
	public int GetNullSlot()
	{
		int result = -1;
		for (int i = 0; i < this.curLunDianList.Count; i++)
		{
			if (this.curLunDianList[i].isNull)
			{
				return i;
			}
		}
		return result;
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x00108BF4 File Offset: 0x00106DF4
	public List<LunDaoCard> GetShengYuLunDian()
	{
		List<LunDaoCard> list = new List<LunDaoCard>();
		foreach (int num in this.targetLunTiDictionary.Keys)
		{
			foreach (int level in this.targetLunTiDictionary[num])
			{
				list.Add(new LunDaoCard(num, level));
			}
		}
		List<LunDaoCard> list2 = new List<LunDaoCard>();
		foreach (LunDaoQiu lunDaoQiu in this.curLunDianList)
		{
			foreach (LunDaoCard lunDaoCard in list)
			{
				if (lunDaoQiu.wudaoId == lunDaoCard.wudaoId && lunDaoQiu.level == lunDaoCard.level)
				{
					list2.Add(lunDaoCard);
				}
			}
		}
		foreach (LunDaoCard item in list2)
		{
			list.Remove(item);
		}
		return list;
	}

	// Token: 0x04001A00 RID: 6656
	public Dictionary<int, List<int>> targetLunTiDictionary;

	// Token: 0x04001A01 RID: 6657
	public List<LunDaoQiu> curLunDianList;
}
