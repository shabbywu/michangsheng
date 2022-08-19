using System;
using System.IO;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class Paths
{
	// Token: 0x0600229F RID: 8863 RVA: 0x000ED714 File Offset: 0x000EB914
	public static string GetSavePath()
	{
		string text;
		if (clientApp.IsTestVersion)
		{
			text = Application.persistentDataPath + "test";
		}
		else
		{
			text = (Application.persistentDataPath ?? "");
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		return text;
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x000ED768 File Offset: 0x000EB968
	public static string GetNewSavePath()
	{
		string text;
		if (clientApp.IsTestVersion)
		{
			text = clientApp.dataPath + "/../MCSSave_TestBranch";
		}
		else
		{
			text = clientApp.dataPath + "/../MCSSave";
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		return text;
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x000ED7BA File Offset: 0x000EB9BA
	public static string GetCloudSavePath()
	{
		return Application.dataPath + "/../MCSCloudSave";
	}

	// Token: 0x04001BF4 RID: 7156
	private const string BasePrefab = "Prefab/";

	// Token: 0x04001BF5 RID: 7157
	public const string TAB_PANEL = "Prefab/TabPanel";

	// Token: 0x04001BF6 RID: 7158
	public const string Bag = "Bag/";

	// Token: 0x04001BF7 RID: 7159
	public const string PASSIVESKILLICON = "StaticSkill Icon/";

	// Token: 0x04001BF8 RID: 7160
	public const string ActiveSKILLICON = "Skill Icon/";
}
