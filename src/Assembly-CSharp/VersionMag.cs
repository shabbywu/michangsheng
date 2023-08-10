using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine.Events;

public class VersionMag
{
	public Dictionary<int, UnityAction> VersionDict;

	private static VersionMag _inst;

	public static VersionMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new VersionMag();
			}
			return _inst;
		}
	}

	private VersionMag()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		VersionDict = new Dictionary<int, UnityAction>();
		VersionDict.Add(4, new UnityAction(Version4));
	}

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
			if (VersionDict.ContainsKey(i))
			{
				VersionDict[i].Invoke();
			}
		}
	}

	public void Version4()
	{
		Avatar player = Tools.instance.getPlayer();
		VersionJsonData4 versionJsonData = VersionJsonData4.DataDict[player.level];
		player._HP_Max += versionJsonData.XueLiang;
		player.HP = player.HP_Max;
		player._shengShi += versionJsonData.ShenShi;
		player._dunSu += versionJsonData.DunSu;
	}
}
