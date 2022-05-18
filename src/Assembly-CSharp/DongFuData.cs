using System;
using System.Collections.Generic;

// Token: 0x02000319 RID: 793
public class DongFuData
{
	// Token: 0x0600176B RID: 5995 RVA: 0x00014AE2 File Offset: 0x00012CE2
	public DongFuData(int id)
	{
		this.ID = id;
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000CD918 File Offset: 0x000CBB18
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

	// Token: 0x0600176D RID: 5997 RVA: 0x000CDB14 File Offset: 0x000CBD14
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

	// Token: 0x040012C5 RID: 4805
	public int ID;

	// Token: 0x040012C6 RID: 4806
	public bool HasDongFu;

	// Token: 0x040012C7 RID: 4807
	public string DongFuName;

	// Token: 0x040012C8 RID: 4808
	public int LingYanLevel;

	// Token: 0x040012C9 RID: 4809
	public int JuLingZhenLevel;

	// Token: 0x040012CA RID: 4810
	public List<int> AreaUnlock = new List<int>();

	// Token: 0x040012CB RID: 4811
	public List<int> Decorate = new List<int>();

	// Token: 0x040012CC RID: 4812
	public int CuiShengLingLi;

	// Token: 0x040012CD RID: 4813
	public List<LingTianData> LingTian = new List<LingTianData>();
}
