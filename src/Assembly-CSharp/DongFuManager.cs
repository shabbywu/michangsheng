using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x0200031B RID: 795
public static class DongFuManager
{
	// Token: 0x17000280 RID: 640
	// (get) Token: 0x0600176F RID: 5999 RVA: 0x00014B12 File Offset: 0x00012D12
	// (set) Token: 0x06001770 RID: 6000 RVA: 0x00014B23 File Offset: 0x00012D23
	public static int NowDongFuID
	{
		get
		{
			return PlayerEx.Player.NowDongFuID.I;
		}
		set
		{
			PlayerEx.Player.NowDongFuID = new JSONObject(value);
		}
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x00014B35 File Offset: 0x00012D35
	public static void LoadDongFuScene(int dongFuID)
	{
		DongFuManager.NowDongFuID = dongFuID;
		Tools.instance.loadMapScenes("S101", true);
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x00014B4D File Offset: 0x00012D4D
	public static JSONObject GetNowDongFuJson()
	{
		return PlayerEx.Player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)];
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x00014B72 File Offset: 0x00012D72
	public static void SetDongFuName(int dongFuID, string name)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("DongFuName", name);
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000CDC8C File Offset: 0x000CBE8C
	public static string GetDongFuName(int dongFuID)
	{
		string result = "洞府";
		Avatar player = PlayerEx.Player;
		if (player.DongFuData.HasField(string.Format("DongFu{0}", dongFuID)) && player.DongFuData[string.Format("DongFu{0}", dongFuID)].HasField("DongFuName"))
		{
			result = player.DongFuData[string.Format("DongFu{0}", dongFuID)]["DongFuName"].Str;
		}
		return result;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000CDD18 File Offset: 0x000CBF18
	public static void CreateDongFu(int dongFuID, int level)
	{
		Avatar player = PlayerEx.Player;
		if (player.DongFuData.HasField(string.Format("DongFu{0}", dongFuID)))
		{
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("LingYanLevel", level);
		}
		else
		{
			player.DongFuData.SetField(string.Format("DongFu{0}", dongFuID), new JSONObject(JSONObject.Type.OBJECT));
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("DongFuName", "洞府");
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("LingYanLevel", level);
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("JuLingZhenLevel", 1);
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("Area0Unlock", 0);
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("Area1Unlock", 0);
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("Area2Unlock", 0);
			player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("LingTian", new JSONObject(JSONObject.Type.OBJECT));
			for (int i = 0; i < DongFuManager.LingTianCount; i++)
			{
				JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
				jsonobject.SetField("ID", 0);
				jsonobject.SetField("LingLi", 0);
				player.DongFuData[string.Format("DongFu{0}", dongFuID)]["LingTian"].SetField(string.Format("Slot{0}", i), jsonobject);
			}
			player.DongFuData[string.Format("DongFu{0}", dongFuID)]["LingTian"].SetField("CuiShengLingLi", 0);
		}
		Debug.Log(string.Format("洞府:\n{0}", player.DongFuData));
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x00014B9E File Offset: 0x00012D9E
	public static void RefreshDongFuShow()
	{
		if (DongFuScene.Inst != null)
		{
			DongFuScene.Inst.RefreshShow();
		}
		if (UILingTianPanel.Inst != null)
		{
			UILingTianPanel.Inst.RefreshUI();
		}
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x00014BCE File Offset: 0x00012DCE
	public static void ZhongZhi(int dongFuID, int slot, int id)
	{
		DongFuData dongFuData = new DongFuData(dongFuID);
		dongFuData.Load();
		dongFuData.LingTian[slot].ID = id;
		dongFuData.LingTian[slot].LingLi = 0;
		dongFuData.Save();
		DongFuManager.RefreshDongFuShow();
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000CDF58 File Offset: 0x000CC158
	public static void ShouHuo(int dongFuID, int slot)
	{
		DongFuData dongFuData = new DongFuData(dongFuID);
		dongFuData.Load();
		Avatar player = PlayerEx.Player;
		int id = dongFuData.LingTian[slot].ID;
		if (id == 0)
		{
			Debug.LogError("灵田收获异常，不能收获id为0的草药");
		}
		else
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
			int num = dongFuData.LingTian[slot].LingLi / itemJsonData.price;
			player.addItem(id, num + 1, Tools.CreateItemSeid(id), false);
			dongFuData.LingTian[slot].ID = 0;
			dongFuData.LingTian[slot].LingLi = 0;
			dongFuData.Save();
		}
		UIDongFu.Inst.InitData();
		DongFuManager.RefreshDongFuShow();
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000CE00C File Offset: 0x000CC20C
	public static void LingTianAddTime(int month)
	{
		if (month < 1)
		{
			return;
		}
		Avatar player = PlayerEx.Player;
		if (player.DongFuData.list.Count == 0)
		{
			return;
		}
		foreach (string index in player.DongFuData.keys)
		{
			int num = 0;
			for (int i = 0; i < DongFuManager.LingTianCount; i++)
			{
				if (player.DongFuData[index]["LingTian"][string.Format("Slot{0}", i)]["ID"].I > 0)
				{
					num++;
				}
			}
			if (num > 0)
			{
				int i2 = player.DongFuData[index]["LingYanLevel"].I;
				int i3 = player.DongFuData[index]["JuLingZhenLevel"].I;
				int lingtiansudu = DFLingYanLevel.DataDict[i2].lingtiansudu;
				int lingtiansudu2 = DFZhenYanLevel.DataDict[i3].lingtiansudu;
				int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i3].lingtiancuishengsudu;
				int i4 = player.DongFuData[index]["LingTian"]["CuiShengLingLi"].I;
				int num2 = Mathf.Min(i4, lingtiancuishengsudu * month);
				player.DongFuData[index]["LingTian"].SetField("CuiShengLingLi", i4 - num2);
				int num3 = lingtiansudu * month + lingtiansudu2 * month + num2;
				int num4 = num3 / num;
				int num5 = num3 % num;
				for (int j = 0; j < DongFuManager.LingTianCount; j++)
				{
					int i5 = player.DongFuData[index]["LingTian"][string.Format("Slot{0}", j)]["ID"].I;
					int i6 = player.DongFuData[index]["LingTian"][string.Format("Slot{0}", j)]["LingLi"].I;
					if (i5 > 0)
					{
						if (num5 > 0)
						{
							player.DongFuData[index]["LingTian"][string.Format("Slot{0}", j)].SetField("LingLi", i6 + num4 + 1);
							num5--;
						}
						else
						{
							player.DongFuData[index]["LingTian"][string.Format("Slot{0}", j)].SetField("LingLi", i6 + num4);
						}
					}
				}
			}
		}
		DongFuManager.RefreshDongFuShow();
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x00014C0A File Offset: 0x00012E0A
	public static void UnlockArea(int dongFuID, DongFuArea area)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField(string.Format("Area{0}Unlock", (int)area), 1);
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000CE2EC File Offset: 0x000CC4EC
	public static bool IsAreaUnlocked(int dongFuID, DongFuArea area)
	{
		Avatar player = PlayerEx.Player;
		string text = string.Format("Area{0}Unlock", (int)area);
		return player.DongFuData[string.Format("DongFu{0}", dongFuID)].HasField(text) && player.DongFuData[string.Format("DongFu{0}", dongFuID)][text].I == 1;
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x00014C41 File Offset: 0x00012E41
	public static int GetPlayerDongFuCount()
	{
		return PlayerEx.Player.DongFuData.Count;
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x00014C52 File Offset: 0x00012E52
	public static bool PlayerHasDongFu(int dongFuID)
	{
		return PlayerEx.Player.DongFuData.HasField(string.Format("DongFu{0}", dongFuID));
	}

	// Token: 0x040012D0 RID: 4816
	public static int LingTianCount = 9;
}
