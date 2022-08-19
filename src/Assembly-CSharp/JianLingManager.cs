using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class JianLingManager
{
	// Token: 0x0600193C RID: 6460 RVA: 0x000B4F63 File Offset: 0x000B3163
	public JianLingManager(Avatar avatar)
	{
		this.avatar = avatar;
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000B4F74 File Offset: 0x000B3174
	public int GetJiYiHuiFuDu()
	{
		int num = 0;
		foreach (string text in this.avatar.JianLingUnlockedXianSuo.keys)
		{
			if (this.avatar.JianLingUnlockedXianSuo[text].b)
			{
				if (JianLingXianSuo.DataDict.ContainsKey(text))
				{
					JianLingXianSuo jianLingXianSuo = JianLingXianSuo.DataDict[text];
					num += jianLingXianSuo.JiYi;
				}
				else
				{
					Debug.LogError("计算剑灵记忆恢复度出现错误，id为" + text + "的线索不再配表中，已忽略此线索");
				}
			}
		}
		num += this.avatar.JianLingExJiYiHuiFuDu;
		return num;
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x000B502C File Offset: 0x000B322C
	public void AddExJiYiHuiFuDu(int value)
	{
		this.avatar.JianLingExJiYiHuiFuDu += value;
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000B5044 File Offset: 0x000B3244
	public void UnlockXianSuo(string ID)
	{
		if (!JianLingXianSuo.DataDict.ContainsKey(ID))
		{
			Debug.LogError("解锁剑灵线索出现错误，id为" + ID + "的线索不再配表中，已忽略此线索");
			return;
		}
		if (this.avatar.JianLingUnlockedXianSuo.HasField(ID) && this.avatar.JianLingUnlockedXianSuo[ID].b)
		{
			return;
		}
		if (JianLingXianSuo.DataDict[ID].JiYi > 0)
		{
			UIPopTip.Inst.Pop("魏无极似乎想起了什么", PopTipIconType.叹号);
		}
		else
		{
			UIPopTip.Inst.Pop("获得一条新的线索", PopTipIconType.叹号);
		}
		this.avatar.JianLingUnlockedXianSuo.SetField(ID, true);
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000B50E8 File Offset: 0x000B32E8
	public bool IsXianSuoUnlocked(string ID)
	{
		if (!JianLingXianSuo.DataDict.ContainsKey(ID))
		{
			Debug.LogError("检查是否解锁剑灵线索出现错误，id为" + ID + "的线索不再配表中，已返回否");
		}
		return this.avatar.JianLingUnlockedXianSuo.HasField(ID) && this.avatar.JianLingUnlockedXianSuo[ID].b;
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x000B5144 File Offset: 0x000B3344
	public void UnlockZhenXiang(string ID)
	{
		if (JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			this.avatar.JianLingUnlockedZhenXiang.SetField(ID, true);
			UIPopTip.Inst.Pop("【" + ID + "】真相已解锁", PopTipIconType.叹号);
			return;
		}
		Debug.LogError("解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已忽略此线索");
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x000B51A4 File Offset: 0x000B33A4
	public bool IsZhenXiangUnlocked(string ID)
	{
		if (!JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			Debug.LogError("检查是否解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已返回否");
		}
		return this.avatar.JianLingUnlockedZhenXiang.HasField(ID) && this.avatar.JianLingUnlockedZhenXiang[ID].b;
	}

	// Token: 0x04001474 RID: 5236
	private Avatar avatar;
}
