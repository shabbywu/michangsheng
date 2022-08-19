using System;
using System.Collections.Generic;

// Token: 0x02000313 RID: 787
public class LunTiMag
{
	// Token: 0x06001B61 RID: 7009 RVA: 0x000C318F File Offset: 0x000C138F
	public LunTiMag()
	{
		this.curLunDianList = new List<LunDaoQiu>();
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x000C31A4 File Offset: 0x000C13A4
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

	// Token: 0x06001B63 RID: 7011 RVA: 0x000C32C4 File Offset: 0x000C14C4
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

	// Token: 0x06001B64 RID: 7012 RVA: 0x000C337C File Offset: 0x000C157C
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

	// Token: 0x06001B65 RID: 7013 RVA: 0x000C349C File Offset: 0x000C169C
	public bool CheckIsTargetLunTi()
	{
		int num = -1;
		return this.CheckIsTargetLunTi(ref num);
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000C34B4 File Offset: 0x000C16B4
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

	// Token: 0x06001B67 RID: 7015 RVA: 0x000C35E0 File Offset: 0x000C17E0
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

	// Token: 0x06001B68 RID: 7016 RVA: 0x000C36A0 File Offset: 0x000C18A0
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

	// Token: 0x06001B69 RID: 7017 RVA: 0x000C36DC File Offset: 0x000C18DC
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

	// Token: 0x040015E6 RID: 5606
	public Dictionary<int, List<int>> targetLunTiDictionary;

	// Token: 0x040015E7 RID: 5607
	public List<LunDaoQiu> curLunDianList;
}
