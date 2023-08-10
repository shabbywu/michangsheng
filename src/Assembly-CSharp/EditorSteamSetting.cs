using System.IO;
using UnityEngine;

public class EditorSteamSetting
{
	public static bool EditorEnableSteam;

	public bool 编辑器中启用Steam => EditorEnableSteam;

	public static string SettingPath => Application.persistentDataPath + "/../EditorSteam.txt";

	public EditorSteamSetting()
	{
		Refresh();
	}

	public static void Refresh()
	{
		bool editorEnableSteam = false;
		if (new FileInfo(SettingPath).Exists && File.ReadAllText(SettingPath) == "true")
		{
			editorEnableSteam = true;
		}
		EditorEnableSteam = editorEnableSteam;
	}

	public void 启用()
	{
		File.WriteAllText(SettingPath, "true");
		Refresh();
	}

	public void 关闭()
	{
		File.WriteAllText(SettingPath, "false");
		Refresh();
	}
}
