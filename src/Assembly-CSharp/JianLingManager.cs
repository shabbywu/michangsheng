using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class JianLingManager
{
	// Token: 0x06001C44 RID: 7236 RVA: 0x00017A08 File Offset: 0x00015C08
	public JianLingManager(Avatar avatar)
	{
		this.avatar = avatar;
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x000FAED8 File Offset: 0x000F90D8
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

	// Token: 0x06001C46 RID: 7238 RVA: 0x00017A17 File Offset: 0x00015C17
	public void AddExJiYiHuiFuDu(int value)
	{
		this.avatar.JianLingExJiYiHuiFuDu += value;
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x000FAF90 File Offset: 0x000F9190
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
		this.avatar.JianLingUnlockedXianSuo.SetField(ID, true);
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x000FB024 File Offset: 0x000F9224
	public bool IsXianSuoUnlocked(string ID)
	{
		if (!JianLingXianSuo.DataDict.ContainsKey(ID))
		{
			Debug.LogError("检查是否解锁剑灵线索出现错误，id为" + ID + "的线索不再配表中，已返回否");
		}
		return this.avatar.JianLingUnlockedXianSuo.HasField(ID) && this.avatar.JianLingUnlockedXianSuo[ID].b;
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x00017A2C File Offset: 0x00015C2C
	public void UnlockZhenXiang(string ID)
	{
		if (JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			this.avatar.JianLingUnlockedZhenXiang.SetField(ID, true);
			return;
		}
		Debug.LogError("解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已忽略此线索");
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x000FB080 File Offset: 0x000F9280
	public bool IsZhenXiangUnlocked(string ID)
	{
		if (!JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			Debug.LogError("检查是否解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已返回否");
		}
		return this.avatar.JianLingUnlockedZhenXiang.HasField(ID) && this.avatar.JianLingUnlockedZhenXiang[ID].b;
	}

	// Token: 0x04001842 RID: 6210
	private Avatar avatar;
}
