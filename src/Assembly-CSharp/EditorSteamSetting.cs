using System;
using System.IO;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class EditorSteamSetting
{
	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0005E53F File Offset: 0x0005C73F
	public bool 编辑器中启用Steam
	{
		get
		{
			return EditorSteamSetting.EditorEnableSteam;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0005E546 File Offset: 0x0005C746
	public static string SettingPath
	{
		get
		{
			return Application.persistentDataPath + "/../EditorSteam.txt";
		}
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0005E557 File Offset: 0x0005C757
	public EditorSteamSetting()
	{
		EditorSteamSetting.Refresh();
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x0005E564 File Offset: 0x0005C764
	public static void Refresh()
	{
		bool editorEnableSteam = false;
		if (new FileInfo(EditorSteamSetting.SettingPath).Exists && File.ReadAllText(EditorSteamSetting.SettingPath) == "true")
		{
			editorEnableSteam = true;
		}
		EditorSteamSetting.EditorEnableSteam = editorEnableSteam;
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x0005E5A2 File Offset: 0x0005C7A2
	public void 启用()
	{
		File.WriteAllText(EditorSteamSetting.SettingPath, "true");
		EditorSteamSetting.Refresh();
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0005E5B8 File Offset: 0x0005C7B8
	public void 关闭()
	{
		File.WriteAllText(EditorSteamSetting.SettingPath, "false");
		EditorSteamSetting.Refresh();
	}

	// Token: 0x04000BC5 RID: 3013
	public static bool EditorEnableSteam;
}
