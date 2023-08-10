using JSONClass;
using KBEngine;
using UnityEngine;

public static class DongFuManager
{
	public static int LingTianCount = 9;

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

	public static void LoadDongFuScene(int dongFuID)
	{
		NowDongFuID = dongFuID;
		Tools.instance.loadMapScenes("S101");
	}

	public static JSONObject GetNowDongFuJson()
	{
		return PlayerEx.Player.DongFuData[$"DongFu{NowDongFuID}"];
	}

	public static void SetDongFuName(int dongFuID, string name)
	{
		PlayerEx.Player.DongFuData[$"DongFu{dongFuID}"].SetField("DongFuName", name);
	}

	public static string GetDongFuName(int dongFuID)
	{
		string result = "洞府";
		Avatar player = PlayerEx.Player;
		if (player.DongFuData.HasField($"DongFu{dongFuID}") && player.DongFuData[$"DongFu{dongFuID}"].HasField("DongFuName"))
		{
			result = player.DongFuData[$"DongFu{dongFuID}"]["DongFuName"].Str;
		}
		return result;
	}

	public static void CreateDongFu(int dongFuID, int level)
	{
		Avatar player = PlayerEx.Player;
		if (player.DongFuData.HasField($"DongFu{dongFuID}"))
		{
			player.DongFuData[$"DongFu{dongFuID}"].SetField("LingYanLevel", level);
		}
		else
		{
			player.DongFuData.SetField($"DongFu{dongFuID}", new JSONObject(JSONObject.Type.OBJECT));
			player.DongFuData[$"DongFu{dongFuID}"].SetField("DongFuName", "洞府");
			player.DongFuData[$"DongFu{dongFuID}"].SetField("LingYanLevel", level);
			player.DongFuData[$"DongFu{dongFuID}"].SetField("JuLingZhenLevel", 1);
			player.DongFuData[$"DongFu{dongFuID}"].SetField("Area0Unlock", 0);
			player.DongFuData[$"DongFu{dongFuID}"].SetField("Area1Unlock", 0);
			player.DongFuData[$"DongFu{dongFuID}"].SetField("Area2Unlock", 0);
			player.DongFuData[$"DongFu{dongFuID}"].SetField("LingTian", new JSONObject(JSONObject.Type.OBJECT));
			for (int i = 0; i < LingTianCount; i++)
			{
				JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
				jSONObject.SetField("ID", 0);
				jSONObject.SetField("LingLi", 0);
				player.DongFuData[$"DongFu{dongFuID}"]["LingTian"].SetField($"Slot{i}", jSONObject);
			}
			player.DongFuData[$"DongFu{dongFuID}"]["LingTian"].SetField("CuiShengLingLi", 0);
		}
		Debug.Log((object)$"洞府:\n{player.DongFuData}");
	}

	public static void RefreshDongFuShow()
	{
		if ((Object)(object)DongFuScene.Inst != (Object)null)
		{
			DongFuScene.Inst.RefreshShow();
		}
		if ((Object)(object)UILingTianPanel.Inst != (Object)null)
		{
			UILingTianPanel.Inst.RefreshUI();
		}
	}

	public static void ZhongZhi(int dongFuID, int slot, int id)
	{
		DongFuData dongFuData = new DongFuData(dongFuID);
		dongFuData.Load();
		dongFuData.LingTian[slot].ID = id;
		dongFuData.LingTian[slot].LingLi = 0;
		dongFuData.Save();
		RefreshDongFuShow();
	}

	public static void ShouHuo(int dongFuID, int slot)
	{
		DongFuData dongFuData = new DongFuData(dongFuID);
		dongFuData.Load();
		Avatar player = PlayerEx.Player;
		int iD = dongFuData.LingTian[slot].ID;
		if (iD == 0)
		{
			Debug.LogError((object)"灵田收获异常，不能收获id为0的草药");
		}
		else
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[iD];
			int num = dongFuData.LingTian[slot].LingLi / itemJsonData.price;
			player.addItem(iD, num + 1, Tools.CreateItemSeid(iD));
			dongFuData.LingTian[slot].ID = 0;
			dongFuData.LingTian[slot].LingLi = 0;
			dongFuData.Save();
		}
		UIDongFu.Inst.InitData();
		RefreshDongFuShow();
	}

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
		foreach (string key in player.DongFuData.keys)
		{
			int num = 0;
			for (int i = 0; i < LingTianCount; i++)
			{
				if (player.DongFuData[key]["LingTian"][$"Slot{i}"]["ID"].I > 0)
				{
					num++;
				}
			}
			if (num <= 0)
			{
				continue;
			}
			int i2 = player.DongFuData[key]["LingYanLevel"].I;
			int i3 = player.DongFuData[key]["JuLingZhenLevel"].I;
			int lingtiansudu = DFLingYanLevel.DataDict[i2].lingtiansudu;
			int lingtiansudu2 = DFZhenYanLevel.DataDict[i3].lingtiansudu;
			int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i3].lingtiancuishengsudu;
			int i4 = player.DongFuData[key]["LingTian"]["CuiShengLingLi"].I;
			int num2 = Mathf.Min(i4, lingtiancuishengsudu * month);
			player.DongFuData[key]["LingTian"].SetField("CuiShengLingLi", i4 - num2);
			int num3 = lingtiansudu * month + lingtiansudu2 * month + num2;
			int num4 = num3 / num;
			int num5 = num3 % num;
			for (int j = 0; j < LingTianCount; j++)
			{
				int i5 = player.DongFuData[key]["LingTian"][$"Slot{j}"]["ID"].I;
				int i6 = player.DongFuData[key]["LingTian"][$"Slot{j}"]["LingLi"].I;
				if (i5 > 0)
				{
					if (num5 > 0)
					{
						player.DongFuData[key]["LingTian"][$"Slot{j}"].SetField("LingLi", i6 + num4 + 1);
						num5--;
					}
					else
					{
						player.DongFuData[key]["LingTian"][$"Slot{j}"].SetField("LingLi", i6 + num4);
					}
				}
			}
		}
		RefreshDongFuShow();
	}

	public static void UnlockArea(int dongFuID, DongFuArea area)
	{
		PlayerEx.Player.DongFuData[$"DongFu{dongFuID}"].SetField($"Area{(int)area}Unlock", 1);
	}

	public static bool IsAreaUnlocked(int dongFuID, DongFuArea area)
	{
		Avatar player = PlayerEx.Player;
		string text = $"Area{(int)area}Unlock";
		if (player.DongFuData[$"DongFu{dongFuID}"].HasField(text) && player.DongFuData[$"DongFu{dongFuID}"][text].I == 1)
		{
			return true;
		}
		return false;
	}

	public static int GetPlayerDongFuCount()
	{
		return PlayerEx.Player.DongFuData.Count;
	}

	public static bool PlayerHasDongFu(int dongFuID)
	{
		if (PlayerEx.Player.DongFuData.HasField($"DongFu{dongFuID}"))
		{
			return true;
		}
		return false;
	}
}
