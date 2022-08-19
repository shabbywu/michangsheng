using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000206 RID: 518
public static class DongFuManager
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000852B3 File Offset: 0x000834B3
	// (set) Token: 0x060014C6 RID: 5318 RVA: 0x000852C4 File Offset: 0x000834C4
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

	// Token: 0x060014C7 RID: 5319 RVA: 0x000852D6 File Offset: 0x000834D6
	public static void LoadDongFuScene(int dongFuID)
	{
		DongFuManager.NowDongFuID = dongFuID;
		Tools.instance.loadMapScenes("S101", true);
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000852EE File Offset: 0x000834EE
	public static JSONObject GetNowDongFuJson()
	{
		return PlayerEx.Player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)];
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x00085313 File Offset: 0x00083513
	public static void SetDongFuName(int dongFuID, string name)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("DongFuName", name);
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x00085340 File Offset: 0x00083540
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

	// Token: 0x060014CB RID: 5323 RVA: 0x000853CC File Offset: 0x000835CC
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

	// Token: 0x060014CC RID: 5324 RVA: 0x0008560A File Offset: 0x0008380A
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

	// Token: 0x060014CD RID: 5325 RVA: 0x0008563A File Offset: 0x0008383A
	public static void ZhongZhi(int dongFuID, int slot, int id)
	{
		DongFuData dongFuData = new DongFuData(dongFuID);
		dongFuData.Load();
		dongFuData.LingTian[slot].ID = id;
		dongFuData.LingTian[slot].LingLi = 0;
		dongFuData.Save();
		DongFuManager.RefreshDongFuShow();
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x00085678 File Offset: 0x00083878
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

	// Token: 0x060014CF RID: 5327 RVA: 0x0008572C File Offset: 0x0008392C
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

	// Token: 0x060014D0 RID: 5328 RVA: 0x00085A0C File Offset: 0x00083C0C
	public static void UnlockArea(int dongFuID, DongFuArea area)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField(string.Format("Area{0}Unlock", (int)area), 1);
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00085A44 File Offset: 0x00083C44
	public static bool IsAreaUnlocked(int dongFuID, DongFuArea area)
	{
		Avatar player = PlayerEx.Player;
		string text = string.Format("Area{0}Unlock", (int)area);
		return player.DongFuData[string.Format("DongFu{0}", dongFuID)].HasField(text) && player.DongFuData[string.Format("DongFu{0}", dongFuID)][text].I == 1;
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x00085AB7 File Offset: 0x00083CB7
	public static int GetPlayerDongFuCount()
	{
		return PlayerEx.Player.DongFuData.Count;
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x00085AC8 File Offset: 0x00083CC8
	public static bool PlayerHasDongFu(int dongFuID)
	{
		return PlayerEx.Player.DongFuData.HasField(string.Format("DongFu{0}", dongFuID));
	}

	// Token: 0x04000F8A RID: 3978
	public static int LingTianCount = 9;
}
