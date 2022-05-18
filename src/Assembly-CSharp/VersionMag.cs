using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine.Events;

// Token: 0x02000606 RID: 1542
public class VersionMag
{
	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06002682 RID: 9858 RVA: 0x0001EB7B File Offset: 0x0001CD7B
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

	// Token: 0x06002683 RID: 9859 RVA: 0x0001EB93 File Offset: 0x0001CD93
	private VersionMag()
	{
		this.VersionDict = new Dictionary<int, UnityAction>();
		this.VersionDict.Add(4, new UnityAction(this.Version4));
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x0012E870 File Offset: 0x0012CA70
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

	// Token: 0x06002685 RID: 9861 RVA: 0x0012E8B8 File Offset: 0x0012CAB8
	public void Version4()
	{
		Avatar player = Tools.instance.getPlayer();
		VersionJsonData4 versionJsonData = VersionJsonData4.DataDict[(int)player.level];
		player._HP_Max += versionJsonData.XueLiang;
		player.HP = player.HP_Max;
		player._shengShi += versionJsonData.ShenShi;
		player._dunSu += versionJsonData.DunSu;
	}

	// Token: 0x040020DA RID: 8410
	public Dictionary<int, UnityAction> VersionDict;

	// Token: 0x040020DB RID: 8411
	private static VersionMag _inst;
}
