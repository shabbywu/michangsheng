using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class MainUIDF : MonoBehaviour, IESCClose
{
	// Token: 0x06001F7A RID: 8058 RVA: 0x0010EC04 File Offset: 0x0010CE04
	public void Init()
	{
		if (!this.isInit)
		{
			for (int i = 0; i < this.maxNum; i++)
			{
				MainUIDFCell component = Object.Instantiate<GameObject>(this.doufaobj, this.doufaoList).GetComponent<MainUIDFCell>();
				if (i == this.maxNum - 1)
				{
					string desc = "剧情模式通关后解锁";
					component.Init(this.startIndex, 15, true, desc);
				}
				else
				{
					int level = this.targetLevelList[i] - 1;
					int num = this.targetLevelList[i];
					bool isLock = this.targetLevelList[i] > MainUIMag.inst.maxLevel;
					string desc = "剧情模式达到" + jsonData.instance.LevelUpDataJsonData[num.ToString()]["Name"].Str + "后解锁";
					component.Init(this.startIndex, level, isLock, desc);
				}
				this.startIndex++;
			}
			this.isInit = true;
		}
		else
		{
			base.gameObject.SetActive(true);
		}
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x0001694F File Offset: 0x00014B4F
	public void Close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x00019FE9 File Offset: 0x000181E9
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001AEA RID: 6890
	public List<int> targetLevelList;

	// Token: 0x04001AEB RID: 6891
	public int maxNum;

	// Token: 0x04001AEC RID: 6892
	public int startIndex;

	// Token: 0x04001AED RID: 6893
	[SerializeField]
	private GameObject doufaobj;

	// Token: 0x04001AEE RID: 6894
	[SerializeField]
	private Transform doufaoList;

	// Token: 0x04001AEF RID: 6895
	private bool isInit;
}
