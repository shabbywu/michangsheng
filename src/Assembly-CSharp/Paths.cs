using System.IO;
using UnityEngine;

public class Paths
{
	private const string BasePrefab = "Prefab/";

	public const string TAB_PANEL = "Prefab/TabPanel";

	public const string Bag = "Bag/";

	public const string PASSIVESKILLICON = "StaticSkill Icon/";

	public const string ActiveSKILLICON = "Skill Icon/";

	public static string GetSavePath()
	{
		string text = "";
		text = ((!clientApp.IsTestVersion) ? (Application.persistentDataPath ?? "") : (Application.persistentDataPath + "test"));
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		return text;
	}

	public static string GetNewSavePath()
	{
		string text = "";
		text = ((!clientApp.IsTestVersion) ? (clientApp.dataPath + "/../MCSSave") : (clientApp.dataPath + "/../MCSSave_TestBranch"));
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		return text;
	}

	public static string GetCloudSavePath()
	{
		return Application.dataPath + "/../MCSCloudSave";
	}
}
