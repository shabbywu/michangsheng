using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class MainUIDF : MonoBehaviour, IESCClose
{
	// Token: 0x06001C28 RID: 7208 RVA: 0x000C987F File Offset: 0x000C7A7F
	public void Init()
	{
		if (!this.isInit)
		{
			ESCCloseManager.Inst.RegisterClose(this);
			this.isInit = true;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x000C98A8 File Offset: 0x000C7AA8
	public void RefreshSaveSlot()
	{
		for (int i = this.cellList.Count - 1; i >= 0; i--)
		{
			Object.Destroy(this.cellList[i]);
		}
		this.cellList.Clear();
		this.startIndex = 100;
		for (int j = 0; j < this.maxNum; j++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.doufaobj, this.doufaoList);
			this.cellList.Add(gameObject);
			MainUIDFCell component = gameObject.GetComponent<MainUIDFCell>();
			if (j == this.maxNum - 1)
			{
				string desc = "剧情模式通关后解锁";
				component.Init(this.startIndex, 15, true, desc);
			}
			else
			{
				int level = this.targetLevelList[j] - 1;
				int num = this.targetLevelList[j];
				bool isLock = this.targetLevelList[j] > MainUIMag.inst.maxLevel;
				string desc = "剧情模式达到" + jsonData.instance.LevelUpDataJsonData[num.ToString()]["Name"].Str + "后解锁";
				component.Init(this.startIndex, level, isLock, desc);
			}
			this.startIndex++;
		}
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x000A3540 File Offset: 0x000A1740
	public void Close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x000C99E8 File Offset: 0x000C7BE8
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x000C99F1 File Offset: 0x000C7BF1
	public void ClearDFSave()
	{
		USelectBox.Show("确定清空神仙斗法存档吗？之后可以重新生成。", delegate
		{
			this.startIndex = 100;
			for (int i = 0; i < this.maxNum; i++)
			{
				YSNewSaveSystem.DeleteSave(i + this.startIndex);
			}
			this.RefreshSaveSlot();
			UIPopTip.Inst.Pop("已清除", PopTipIconType.叹号);
		}, null);
	}

	// Token: 0x040016B4 RID: 5812
	public List<int> targetLevelList;

	// Token: 0x040016B5 RID: 5813
	public int maxNum;

	// Token: 0x040016B6 RID: 5814
	private int startIndex = 100;

	// Token: 0x040016B7 RID: 5815
	[SerializeField]
	private GameObject doufaobj;

	// Token: 0x040016B8 RID: 5816
	[SerializeField]
	private Transform doufaoList;

	// Token: 0x040016B9 RID: 5817
	private bool isInit;

	// Token: 0x040016BA RID: 5818
	private List<GameObject> cellList = new List<GameObject>();
}
