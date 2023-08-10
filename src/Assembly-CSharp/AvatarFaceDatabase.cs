using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarFaceDatabase : MonoBehaviour
{
	public List<faceInfoList> AllList1;

	public List<faceInfoList> AllList2;

	public static AvatarFaceDatabase inst;

	public int ListType = 1;

	public JSONObject SuiJiTouXiangGeShuJsonData;

	public List<faceInfoList> AllList
	{
		get
		{
			if (ListType == 1)
			{
				return AllList1;
			}
			return AllList2;
		}
	}

	public faceInfoList getFaceInfo(string aa)
	{
		foreach (faceInfoList all in AllList)
		{
			if (all.Name == aa)
			{
				return all;
			}
		}
		return null;
	}

	public faceInfoDataBaseList getBaseInfo(string str)
	{
		foreach (faceInfoList all in AllList)
		{
			foreach (faceInfoDataBaseList face in all.faceList)
			{
				if (face.Name == str)
				{
					return face;
				}
			}
		}
		return null;
	}

	private void Awake()
	{
		if ((Object)(object)inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)inst).gameObject);
		}
		inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)inst).gameObject);
	}

	public void Init()
	{
		string str = File.ReadAllText(File.ReadAllText("C://FacePath//Path.txt") + "//Effect/json/d_str.py.xinXiangSuiji.json");
		SuiJiTouXiangGeShuJsonData = new JSONObject(str);
		AllList.Clear();
		foreach (JSONObject aa in SuiJiTouXiangGeShuJsonData.list)
		{
			_ = aa["StrID"].str;
			if (aa["Type"].str == "" || aa["SuiJiSex" + ListType].list.Count <= 0 || AllList.Find((faceInfoList __a) => __a.Name == Tools.Code64(aa["Type"].str)) != null)
			{
				continue;
			}
			faceInfoList faceInfoList2 = new faceInfoList();
			faceInfoList2.Name = Tools.Code64(aa["Type"].str);
			faceInfoList2.faceList = new List<faceInfoDataBaseList>();
			foreach (JSONObject item in SuiJiTouXiangGeShuJsonData.list.FindAll((JSONObject _i) => _i["Type"].str == aa["Type"].str && _i["SuiJiSex" + ListType].list.Count > 0))
			{
				faceInfoDataBaseList faceInfoDataBaseList2 = new faceInfoDataBaseList();
				faceInfoDataBaseList2.Name = Tools.Code64(item["ChildType"].str);
				faceInfoDataBaseList2.JsonInfoName = item["StrID"].str;
				faceInfoList2.faceList.Add(faceInfoDataBaseList2);
			}
			AllList.Add(faceInfoList2);
		}
		foreach (faceInfoList all in AllList)
		{
			foreach (faceInfoDataBaseList face in all.faceList)
			{
				addInfo(face);
			}
		}
		ListType = 2;
		foreach (JSONObject aa2 in SuiJiTouXiangGeShuJsonData.list)
		{
			_ = aa2["StrID"].str;
			if (aa2["Type"].str == "" || aa2["SuiJiSex" + ListType].list.Count <= 0 || AllList.Find((faceInfoList __a) => __a.Name == Tools.Code64(aa2["Type"].str)) != null)
			{
				continue;
			}
			faceInfoList faceInfoList3 = new faceInfoList();
			faceInfoList3.Name = Tools.Code64(aa2["Type"].str);
			faceInfoList3.faceList = new List<faceInfoDataBaseList>();
			foreach (JSONObject item2 in SuiJiTouXiangGeShuJsonData.list.FindAll((JSONObject _i) => _i["Type"].str == aa2["Type"].str && _i["SuiJiSex" + ListType].list.Count > 0))
			{
				faceInfoDataBaseList faceInfoDataBaseList3 = new faceInfoDataBaseList();
				faceInfoDataBaseList3.Name = Tools.Code64(item2["ChildType"].str);
				faceInfoDataBaseList3.JsonInfoName = item2["StrID"].str;
				faceInfoList3.faceList.Add(faceInfoDataBaseList3);
			}
			AllList.Add(faceInfoList3);
		}
		foreach (faceInfoList all2 in AllList)
		{
			foreach (faceInfoDataBaseList face2 in all2.faceList)
			{
				addInfo(face2);
			}
		}
		ListType = 1;
	}

	public void addInfo(faceInfoDataBaseList cc)
	{
		string jsonInfoName = cc.JsonInfoName;
		if (jsonInfoName == "")
		{
			return;
		}
		getSuijiList(jsonInfoName, "SuiJiSex" + ListType);
		List<int> suijiList = getSuijiList(jsonInfoName, "Sex" + ListType);
		foreach (int item in suijiList)
		{
			bool flag = true;
			foreach (faceInfoDataBase face in cc.faceList)
			{
				if (item == face.FaceID)
				{
					flag = false;
				}
			}
			if (!suijiList.Contains(item) || !flag)
			{
				continue;
			}
			faceInfoDataBase faceInfoDataBase2 = new faceInfoDataBase();
			faceInfoDataBase2.FaceID = item;
			string str = SuiJiTouXiangGeShuJsonData[cc.JsonInfoName]["ImageName"].str;
			if (ListType != 1)
			{
				goto IL_01f8;
			}
			switch (str)
			{
			case "hair_":
			case "hair_0":
			case "hair_00":
				break;
			default:
				goto IL_01f8;
			}
			faceInfoDataBase2.Image2 = Resources.Load<Sprite>("Ui Icon/createFace/images" + ListType + "/face");
			faceInfoDataBase2.Image3 = Resources.Load<Sprite>(("Ui Icon/createFace/images" + ListType + "/" + str + item) ?? "");
			if (str == "hair_" && item >= 20)
			{
				faceInfoDataBase2.Image1 = Resources.Load<Sprite>(("Ui Icon/createFace/images" + ListType + "/shawl_hair_" + item) ?? "");
			}
			goto IL_0310;
			IL_01f8:
			if (ListType == 2 && str == "hair_")
			{
				faceInfoDataBase2.Image3 = Resources.Load<Sprite>(("Ui Icon/createFace/images" + ListType + "/" + str + item) ?? "");
			}
			else
			{
				int num = item;
				if ("hairColorR" == cc.JsonInfoName || cc.JsonInfoName == "yanzhuColor" || cc.JsonInfoName == "tezhengColor" || cc.JsonInfoName == "eyebrowColor" || cc.JsonInfoName == "mouthColor")
				{
					num++;
				}
				faceInfoDataBase2.Image1 = Resources.Load<Sprite>(("Ui Icon/createFace/images" + ListType + "/" + str + num) ?? "");
			}
			goto IL_0310;
			IL_0310:
			if (!((Object)(object)faceInfoDataBase2.Image1 == (Object)null) || !((Object)(object)faceInfoDataBase2.Image2 == (Object)null) || !((Object)(object)faceInfoDataBase2.Image3 == (Object)null))
			{
				cc.faceList.Add(faceInfoDataBase2);
			}
		}
	}

	public List<int> getSuijiList(string name, string sex)
	{
		List<JSONObject> list = SuiJiTouXiangGeShuJsonData[name][sex].list;
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
}
