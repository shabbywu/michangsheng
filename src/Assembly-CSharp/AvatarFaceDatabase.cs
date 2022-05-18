using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000582 RID: 1410
[ExecuteInEditMode]
public class AvatarFaceDatabase : MonoBehaviour
{
	// Token: 0x060023B6 RID: 9142 RVA: 0x0001CD2A File Offset: 0x0001AF2A
	private void Awake()
	{
		AvatarFaceDatabase.inst = this;
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x060023B7 RID: 9143 RVA: 0x0001CD32 File Offset: 0x0001AF32
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

	// Token: 0x060023B8 RID: 9144 RVA: 0x00124D88 File Offset: 0x00122F88
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

	// Token: 0x060023B9 RID: 9145 RVA: 0x00124DEC File Offset: 0x00122FEC
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

	// Token: 0x060023BA RID: 9146 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x00124E88 File Offset: 0x00123088
	public void init()
	{
		this.autoSetImage = false;
		TextAsset textAsset = (TextAsset)Resources.Load("Effect/json/d_str.py.xinXiangSuiji");
		this.SuiJiTouXiangGeShuJsonData = new JSONObject(textAsset.text, -2, false, false);
		this.AllList.Clear();
		using (List<JSONObject>.Enumerator enumerator = this.SuiJiTouXiangGeShuJsonData.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject aa = enumerator.Current;
				string str = aa["StrID"].str;
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
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x00125138 File Offset: 0x00123338
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

	// Token: 0x060023BE RID: 9150 RVA: 0x001254E0 File Offset: 0x001236E0
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

	// Token: 0x04001EBB RID: 7867
	public List<faceInfoList> AllList1;

	// Token: 0x04001EBC RID: 7868
	public List<faceInfoList> AllList2;

	// Token: 0x04001EBD RID: 7869
	public static AvatarFaceDatabase inst;

	// Token: 0x04001EBE RID: 7870
	public int ListType = 1;

	// Token: 0x04001EBF RID: 7871
	public bool autoSetImage = true;

	// Token: 0x04001EC0 RID: 7872
	public JSONObject SuiJiTouXiangGeShuJsonData;
}
