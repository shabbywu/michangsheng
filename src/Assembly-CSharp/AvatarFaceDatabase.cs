using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class AvatarFaceDatabase : MonoBehaviour
{
	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06002036 RID: 8246 RVA: 0x000E2569 File Offset: 0x000E0769
	public List<faceInfoList> AllList
	{
		get
		{
			if (this.ListType == 1)
			{
				return this.AllList1;
			}
			return this.AllList2;
		}
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x000E2584 File Offset: 0x000E0784
	public faceInfoList getFaceInfo(string aa)
	{
		foreach (faceInfoList faceInfoList in this.AllList)
		{
			if (faceInfoList.Name == aa)
			{
				return faceInfoList;
			}
		}
		return null;
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x000E25E8 File Offset: 0x000E07E8
	public faceInfoDataBaseList getBaseInfo(string str)
	{
		foreach (faceInfoList faceInfoList in this.AllList)
		{
			foreach (faceInfoDataBaseList faceInfoDataBaseList in faceInfoList.faceList)
			{
				if (faceInfoDataBaseList.Name == str)
				{
					return faceInfoDataBaseList;
				}
			}
		}
		return null;
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x000E2684 File Offset: 0x000E0884
	private void Awake()
	{
		if (AvatarFaceDatabase.inst != null)
		{
			Object.Destroy(AvatarFaceDatabase.inst.gameObject);
		}
		AvatarFaceDatabase.inst = this;
		Object.DontDestroyOnLoad(AvatarFaceDatabase.inst.gameObject);
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x000E26B8 File Offset: 0x000E08B8
	public void Init()
	{
		string str = File.ReadAllText(File.ReadAllText("C://FacePath//Path.txt") + "//Effect/json/d_str.py.xinXiangSuiji.json");
		this.SuiJiTouXiangGeShuJsonData = new JSONObject(str, -2, false, false);
		this.AllList.Clear();
		using (List<JSONObject>.Enumerator enumerator = this.SuiJiTouXiangGeShuJsonData.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject aa = enumerator.Current;
				string str2 = aa["StrID"].str;
				if (!(aa["Type"].str == "") && aa["SuiJiSex" + this.ListType].list.Count > 0 && this.AllList.Find((faceInfoList __a) => __a.Name == Tools.Code64(aa["Type"].str)) == null)
				{
					faceInfoList faceInfoList = new faceInfoList();
					faceInfoList.Name = Tools.Code64(aa["Type"].str);
					faceInfoList.faceList = new List<faceInfoDataBaseList>();
					foreach (JSONObject jsonobject in this.SuiJiTouXiangGeShuJsonData.list.FindAll((JSONObject _i) => _i["Type"].str == aa["Type"].str && _i["SuiJiSex" + this.ListType].list.Count > 0))
					{
						faceInfoDataBaseList faceInfoDataBaseList = new faceInfoDataBaseList();
						faceInfoDataBaseList.Name = Tools.Code64(jsonobject["ChildType"].str);
						faceInfoDataBaseList.JsonInfoName = jsonobject["StrID"].str;
						faceInfoList.faceList.Add(faceInfoDataBaseList);
					}
					this.AllList.Add(faceInfoList);
				}
			}
		}
		foreach (faceInfoList faceInfoList2 in this.AllList)
		{
			foreach (faceInfoDataBaseList cc in faceInfoList2.faceList)
			{
				this.addInfo(cc);
			}
		}
		this.ListType = 2;
		using (List<JSONObject>.Enumerator enumerator = this.SuiJiTouXiangGeShuJsonData.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject aa = enumerator.Current;
				string str3 = aa["StrID"].str;
				if (!(aa["Type"].str == "") && aa["SuiJiSex" + this.ListType].list.Count > 0 && this.AllList.Find((faceInfoList __a) => __a.Name == Tools.Code64(aa["Type"].str)) == null)
				{
					faceInfoList faceInfoList3 = new faceInfoList();
					faceInfoList3.Name = Tools.Code64(aa["Type"].str);
					faceInfoList3.faceList = new List<faceInfoDataBaseList>();
					foreach (JSONObject jsonobject2 in this.SuiJiTouXiangGeShuJsonData.list.FindAll((JSONObject _i) => _i["Type"].str == aa["Type"].str && _i["SuiJiSex" + this.ListType].list.Count > 0))
					{
						faceInfoDataBaseList faceInfoDataBaseList2 = new faceInfoDataBaseList();
						faceInfoDataBaseList2.Name = Tools.Code64(jsonobject2["ChildType"].str);
						faceInfoDataBaseList2.JsonInfoName = jsonobject2["StrID"].str;
						faceInfoList3.faceList.Add(faceInfoDataBaseList2);
					}
					this.AllList.Add(faceInfoList3);
				}
			}
		}
		foreach (faceInfoList faceInfoList4 in this.AllList)
		{
			foreach (faceInfoDataBaseList cc2 in faceInfoList4.faceList)
			{
				this.addInfo(cc2);
			}
		}
		this.ListType = 1;
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x000E2BE8 File Offset: 0x000E0DE8
	public void addInfo(faceInfoDataBaseList cc)
	{
		string jsonInfoName = cc.JsonInfoName;
		if (jsonInfoName == "")
		{
			return;
		}
		this.getSuijiList(jsonInfoName, "SuiJiSex" + this.ListType);
		List<int> suijiList = this.getSuijiList(jsonInfoName, "Sex" + this.ListType);
		foreach (int num in suijiList)
		{
			bool flag = true;
			foreach (faceInfoDataBase faceInfoDataBase in cc.faceList)
			{
				if (num == faceInfoDataBase.FaceID)
				{
					flag = false;
				}
			}
			if (suijiList.Contains(num) && flag)
			{
				faceInfoDataBase faceInfoDataBase2 = new faceInfoDataBase();
				faceInfoDataBase2.FaceID = num;
				string str = this.SuiJiTouXiangGeShuJsonData[cc.JsonInfoName]["ImageName"].str;
				if (this.ListType == 1 && (str == "hair_" || str == "hair_0" || str == "hair_00"))
				{
					faceInfoDataBase2.Image2 = Resources.Load<Sprite>("Ui Icon/createFace/images" + this.ListType + "/face");
					faceInfoDataBase2.Image3 = Resources.Load<Sprite>(string.Concat(new object[]
					{
						"Ui Icon/createFace/images",
						this.ListType,
						"/",
						str,
						num
					}) ?? "");
					if (str == "hair_" && num >= 20)
					{
						faceInfoDataBase2.Image1 = Resources.Load<Sprite>(string.Concat(new object[]
						{
							"Ui Icon/createFace/images",
							this.ListType,
							"/shawl_hair_",
							num
						}) ?? "");
					}
				}
				else if (this.ListType == 2 && str == "hair_")
				{
					faceInfoDataBase2.Image3 = Resources.Load<Sprite>(string.Concat(new object[]
					{
						"Ui Icon/createFace/images",
						this.ListType,
						"/",
						str,
						num
					}) ?? "");
				}
				else
				{
					int num2 = num;
					if ("hairColorR" == cc.JsonInfoName || cc.JsonInfoName == "yanzhuColor" || cc.JsonInfoName == "tezhengColor" || cc.JsonInfoName == "eyebrowColor" || cc.JsonInfoName == "mouthColor")
					{
						num2++;
					}
					faceInfoDataBase2.Image1 = Resources.Load<Sprite>(string.Concat(new object[]
					{
						"Ui Icon/createFace/images",
						this.ListType,
						"/",
						str,
						num2
					}) ?? "");
				}
				if (!(faceInfoDataBase2.Image1 == null) || !(faceInfoDataBase2.Image2 == null) || !(faceInfoDataBase2.Image3 == null))
				{
					cc.faceList.Add(faceInfoDataBase2);
				}
			}
		}
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x000E2F90 File Offset: 0x000E1190
	public List<int> getSuijiList(string name, string sex)
	{
		List<JSONObject> list = this.SuiJiTouXiangGeShuJsonData[name][sex].list;
		List<int> list2 = new List<int>();
		for (int i = 0; i < list.Count / 2; i++)
		{
			for (int j = (int)list[i * 2].n; j <= (int)list[i * 2 + 1].n; j++)
			{
				list2.Add(j);
			}
		}
		return list2;
	}

	// Token: 0x04001A29 RID: 6697
	public List<faceInfoList> AllList1;

	// Token: 0x04001A2A RID: 6698
	public List<faceInfoList> AllList2;

	// Token: 0x04001A2B RID: 6699
	public static AvatarFaceDatabase inst;

	// Token: 0x04001A2C RID: 6700
	public int ListType = 1;

	// Token: 0x04001A2D RID: 6701
	public JSONObject SuiJiTouXiangGeShuJsonData;
}
