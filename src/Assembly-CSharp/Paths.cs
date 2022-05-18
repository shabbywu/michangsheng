using System;
using System.IO;
using UnityEngine;

// Token: 0x020005FC RID: 1532
public class Paths
{
	// Token: 0x0600265E RID: 9822 RVA: 0x0012E4F0 File Offset: 0x0012C6F0
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

	// Token: 0x040020C0 RID: 8384
	private const string BasePrefab = "Prefab/";

	// Token: 0x040020C1 RID: 8385
	public const string TAB_PANEL = "Prefab/TabPanel";

	// Token: 0x040020C2 RID: 8386
	public const string Bag = "Bag/";

	// Token: 0x040020C3 RID: 8387
	public const string PASSIVESKILLICON = "StaticSkill Icon/";

	// Token: 0x040020C4 RID: 8388
	public const string ActiveSKILLICON = "Skill Icon/";
}
