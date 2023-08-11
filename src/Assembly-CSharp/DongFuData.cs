using System.Collections.Generic;

public class DongFuData
{
	public int ID;

	public bool HasDongFu;

	public string DongFuName;

	public int LingYanLevel;

	public int JuLingZhenLevel;

	public List<int> AreaUnlock = new List<int>();

	public List<int> Decorate = new List<int>();

	public int CuiShengLingLi;

	public List<LingTianData> LingTian = new List<LingTianData>();

	public DongFuData(int id)
	{
		ID = id;
	}

	public void Load()
	{
		JSONObject jSONObject = PlayerEx.Player.DongFuData[$"DongFu{ID}"];
		if (jSONObject == null)
		{
			HasDongFu = false;
			return;
		}
		HasDongFu = true;
		DongFuName = jSONObject["DongFuName"].Str;
		LingYanLevel = jSONObject["LingYanLevel"].I;
		JuLingZhenLevel = jSONObject["JuLingZhenLevel"].I;
		AreaUnlock.Clear();
		for (int i = 0; i < int.MaxValue && jSONObject.HasField($"Area{i}Unlock"); i++)
		{
			AreaUnlock.Add(jSONObject[$"Area{i}Unlock"].I);
		}
		Decorate.Clear();
		for (int j = 0; j < int.MaxValue && jSONObject.HasField($"Decorate{j}"); j++)
		{
			AreaUnlock.Add(jSONObject[$"Decorate{j}"].I);
		}
		CuiShengLingLi = jSONObject["LingTian"]["CuiShengLingLi"].I;
		LingTian.Clear();
		for (int k = 0; k < DongFuManager.LingTianCount; k++)
		{
			LingTianData lingTianData = new LingTianData();
			lingTianData.ID = jSONObject["LingTian"][$"Slot{k}"]["ID"].I;
			lingTianData.LingLi = jSONObject["LingTian"][$"Slot{k}"]["LingLi"].I;
			LingTian.Add(lingTianData);
		}
	}

	public void Save()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("DongFuName", DongFuName);
		jSONObject.SetField("LingYanLevel", LingYanLevel);
		jSONObject.SetField("JuLingZhenLevel", JuLingZhenLevel);
		for (int i = 0; i < AreaUnlock.Count; i++)
		{
			jSONObject.SetField($"Area{i}Unlock", AreaUnlock[i]);
		}
		for (int j = 0; j < Decorate.Count; j++)
		{
			jSONObject.SetField($"Decorate{j}", Decorate[j]);
		}
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject2.SetField("CuiShengLingLi", CuiShengLingLi);
		for (int k = 0; k < LingTian.Count; k++)
		{
			JSONObject jSONObject3 = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject3.SetField("ID", LingTian[k].ID);
			jSONObject3.SetField("LingLi", LingTian[k].LingLi);
			jSONObject2.SetField($"Slot{k}", jSONObject3);
		}
		jSONObject.SetField("LingTian", jSONObject2);
		PlayerEx.Player.DongFuData.SetField($"DongFu{ID}", jSONObject);
	}
}
