using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine.Events;

// Token: 0x0200044C RID: 1100
public class VersionMag
{
	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000EDD28 File Offset: 0x000EBF28
	public static VersionMag Inst
	{
		get
		{
			if (VersionMag._inst == null)
			{
				VersionMag._inst = new VersionMag();
			}
			return VersionMag._inst;
		}
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x000EDD40 File Offset: 0x000EBF40
	private VersionMag()
	{
		this.VersionDict = new Dictionary<int, UnityAction>();
		this.VersionDict.Add(4, new UnityAction(this.Version4));
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x000EDD6C File Offset: 0x000EBF6C
	public void UpdateVersion(int nowVersion, int oldVersion)
	{
		if (oldVersion >= nowVersion)
		{
			return;
		}
		int num = oldVersion + 1;
		if (num < 4)
		{
			num = 4;
		}
		for (int i = num; i <= nowVersion; i++)
		{
			if (this.VersionDict.ContainsKey(i))
			{
				this.VersionDict[i].Invoke();
			}
		}
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x000EDDB4 File Offset: 0x000EBFB4
	public void Version4()
	{
		Avatar player = Tools.instance.getPlayer();
		VersionJsonData4 versionJsonData = VersionJsonData4.DataDict[(int)player.level];
		player._HP_Max += versionJsonData.XueLiang;
		player.HP = player.HP_Max;
		player._shengShi += versionJsonData.ShenShi;
		player._dunSu += versionJsonData.DunSu;
	}

	// Token: 0x04001C07 RID: 7175
	public Dictionary<int, UnityAction> VersionDict;

	// Token: 0x04001C08 RID: 7176
	private static VersionMag _inst;
}
