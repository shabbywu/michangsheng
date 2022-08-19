using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200044D RID: 1101
public static class WorkshopTool
{
	// Token: 0x1700028C RID: 652
	// (get) Token: 0x060022C6 RID: 8902 RVA: 0x000EDE22 File Offset: 0x000EC022
	public static string WorkshopRootPath
	{
		get
		{
			if (string.IsNullOrEmpty(WorkshopTool.workshopRootPath))
			{
				WorkshopTool.workshopRootPath = Application.dataPath + "/../../../workshop/content/1189490";
			}
			return WorkshopTool.workshopRootPath;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060022C7 RID: 8903 RVA: 0x000EDE49 File Offset: 0x000EC049
	public static string DisableModPath
	{
		get
		{
			if (string.IsNullOrEmpty(WorkshopTool.disableModPath))
			{
				WorkshopTool.disableModPath = Application.dataPath + "/../DontLoadModsList.txt";
			}
			return WorkshopTool.disableModPath;
		}
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x000EDE70 File Offset: 0x000EC070
	public static DirectoryInfo GetModDirectory(string workshopID)
	{
		return new DirectoryInfo(WorkshopTool.WorkshopRootPath + "/" + workshopID);
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x000EDE88 File Offset: 0x000EC088
	public static List<DirectoryInfo> GetAllModDirectory()
	{
		List<DirectoryInfo> list = new List<DirectoryInfo>();
		DirectoryInfo directoryInfo = new DirectoryInfo(WorkshopTool.WorkshopRootPath);
		if (directoryInfo.Exists)
		{
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				FileInfo[] files = directoryInfo2.GetFiles();
				for (int j = 0; j < files.Length; j++)
				{
					if (files[j].Name == "Mod.bin")
					{
						list.Add(directoryInfo2);
						break;
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x000EDF08 File Offset: 0x000EC108
	public static List<DirectoryInfo> GetAllModChildDirectoryByName(string directoryName)
	{
		List<DirectoryInfo> list = new List<DirectoryInfo>();
		foreach (DirectoryInfo directoryInfo in WorkshopTool.GetAllModDirectory())
		{
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				if (directoryInfo2.Name == directoryName)
				{
					list.Add(directoryInfo2);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x000EDF8C File Offset: 0x000EC18C
	public static bool CheckModIsDisable(string workshopID)
	{
		if (WorkshopTool.disableMods == null)
		{
			WorkshopTool.InitDisableMods();
		}
		return WorkshopTool.disableMods.Contains(workshopID);
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x000EDFA8 File Offset: 0x000EC1A8
	private static void InitDisableMods()
	{
		WorkshopTool.disableMods = new List<string>();
		if (File.Exists(WorkshopTool.DisableModPath))
		{
			foreach (string text in File.ReadAllLines(WorkshopTool.DisableModPath))
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					WorkshopTool.disableMods.Add(text);
				}
			}
		}
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000EDFFB File Offset: 0x000EC1FB
	public static void OpenMod(string workshopID)
	{
		if (WorkshopTool.disableMods == null)
		{
			WorkshopTool.InitDisableMods();
		}
		if (!WorkshopTool.disableMods.Contains(workshopID))
		{
			return;
		}
		WorkshopTool.disableMods.Remove(workshopID);
		File.WriteAllLines(WorkshopTool.disableModPath, WorkshopTool.disableMods);
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x000EE032 File Offset: 0x000EC232
	public static void CloseMod(string workshopID)
	{
		if (WorkshopTool.disableMods == null)
		{
			WorkshopTool.InitDisableMods();
		}
		if (WorkshopTool.disableMods.Contains(workshopID))
		{
			return;
		}
		WorkshopTool.disableMods.Add(workshopID);
		File.WriteAllLines(WorkshopTool.disableModPath, WorkshopTool.disableMods);
	}

	// Token: 0x04001C09 RID: 7177
	private static string workshopRootPath;

	// Token: 0x04001C0A RID: 7178
	private static string disableModPath;

	// Token: 0x04001C0B RID: 7179
	private static List<string> disableMods;
}
