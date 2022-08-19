using System;
using System.Collections.Generic;

// Token: 0x02000204 RID: 516
public class DongFuData
{
	// Token: 0x060014C1 RID: 5313 RVA: 0x00084F0E File Offset: 0x0008310E
	public DongFuData(int id)
	{
		this.ID = id;
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x00084F40 File Offset: 0x00083140
	public void Load()
	{
		JSONObject jsonobject = PlayerEx.Player.DongFuData[string.Format("DongFu{0}", this.ID)];
		if (jsonobject == null)
		{
			this.HasDongFu = false;
			return;
		}
		this.HasDongFu = true;
		this.DongFuName = jsonobject["DongFuName"].Str;
		this.LingYanLevel = jsonobject["LingYanLevel"].I;
		this.JuLingZhenLevel = jsonobject["JuLingZhenLevel"].I;
		this.AreaUnlock.Clear();
		int num = 0;
		while (num < 2147483647 && jsonobject.HasField(string.Format("Area{0}Unlock", num)))
		{
			this.AreaUnlock.Add(jsonobject[string.Format("Area{0}Unlock", num)].I);
			num++;
		}
		this.Decorate.Clear();
		int num2 = 0;
		while (num2 < 2147483647 && jsonobject.HasField(string.Format("Decorate{0}", num2)))
		{
			this.AreaUnlock.Add(jsonobject[string.Format("Decorate{0}", num2)].I);
			num2++;
		}
		this.CuiShengLingLi = jsonobject["LingTian"]["CuiShengLingLi"].I;
		this.LingTian.Clear();
		for (int i = 0; i < DongFuManager.LingTianCount; i++)
		{
			LingTianData lingTianData = new LingTianData();
			lingTianData.ID = jsonobject["LingTian"][string.Format("Slot{0}", i)]["ID"].I;
			lingTianData.LingLi = jsonobject["LingTian"][string.Format("Slot{0}", i)]["LingLi"].I;
			this.LingTian.Add(lingTianData);
		}
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x0008513C File Offset: 0x0008333C
	public void Save()
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("DongFuName", this.DongFuName);
		jsonobject.SetField("LingYanLevel", this.LingYanLevel);
		jsonobject.SetField("JuLingZhenLevel", this.JuLingZhenLevel);
		for (int i = 0; i < this.AreaUnlock.Count; i++)
		{
			jsonobject.SetField(string.Format("Area{0}Unlock", i), this.AreaUnlock[i]);
		}
		for (int j = 0; j < this.Decorate.Count; j++)
		{
			jsonobject.SetField(string.Format("Decorate{0}", j), this.Decorate[j]);
		}
		JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject2.SetField("CuiShengLingLi", this.CuiShengLingLi);
		for (int k = 0; k < this.LingTian.Count; k++)
		{
			JSONObject jsonobject3 = new JSONObject(JSONObject.Type.OBJECT);
			jsonobject3.SetField("ID", this.LingTian[k].ID);
			jsonobject3.SetField("LingLi", this.LingTian[k].LingLi);
			jsonobject2.SetField(string.Format("Slot{0}", k), jsonobject3);
		}
		jsonobject.SetField("LingTian", jsonobject2);
		PlayerEx.Player.DongFuData.SetField(string.Format("DongFu{0}", this.ID), jsonobject);
	}

	// Token: 0x04000F7F RID: 3967
	public int ID;

	// Token: 0x04000F80 RID: 3968
	public bool HasDongFu;

	// Token: 0x04000F81 RID: 3969
	public string DongFuName;

	// Token: 0x04000F82 RID: 3970
	public int LingYanLevel;

	// Token: 0x04000F83 RID: 3971
	public int JuLingZhenLevel;

	// Token: 0x04000F84 RID: 3972
	public List<int> AreaUnlock = new List<int>();

	// Token: 0x04000F85 RID: 3973
	public List<int> Decorate = new List<int>();

	// Token: 0x04000F86 RID: 3974
	public int CuiShengLingLi;

	// Token: 0x04000F87 RID: 3975
	public List<LingTianData> LingTian = new List<LingTianData>();
}
