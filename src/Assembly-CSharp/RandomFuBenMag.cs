using System;
using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200054B RID: 1355
public class RandomFuBenMag
{
	// Token: 0x0600229E RID: 8862 RVA: 0x0001C57C File Offset: 0x0001A77C
	public RandomFuBenMag(Avatar _avatar)
	{
		this.avatar = _avatar;
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x0011CBDC File Offset: 0x0011ADDC
	public void AutoSetRandomFuBen()
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.RandomMapList)
		{
			this.SetRandomFuBenShouldReset(keyValuePair.Value);
		}
	}

	// Token: 0x060022A0 RID: 8864 RVA: 0x0011CC34 File Offset: 0x0011AE34
	public void SetRandomFuBenShouldReset(JToken json)
	{
		int fuBenId = (int)json["id"];
		this.InitFuBenJson(fuBenId);
		DateTime startTime = DateTime.Parse((string)this.avatar.RandomFuBenList[fuBenId.ToString()]["startTime"]);
		DateTime nowTime = this.avatar.worldTimeMag.getNowTime();
		if (!(bool)this.avatar.RandomFuBenList[fuBenId.ToString()]["ShouldReset"])
		{
			int value = (int)json["resetYear"];
			if (!Tools.instance.IsInTime(nowTime, startTime, startTime.AddYears(value), 0))
			{
				this.avatar.RandomFuBenList[fuBenId.ToString()]["ShouldReset"] = true;
			}
		}
	}

	// Token: 0x060022A1 RID: 8865 RVA: 0x0001C58B File Offset: 0x0001A78B
	public void OutRandomFuBen()
	{
		Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence, true);
		this.avatar.lastFuBenScence = "";
		this.avatar.NowFuBen = "";
	}

	// Token: 0x060022A2 RID: 8866 RVA: 0x0011CD10 File Offset: 0x0011AF10
	public void GetInRandomFuBen(int id, int specialType = -1)
	{
		if (specialType > 0)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.RandomMapList)
			{
				if ((int)keyValuePair.Value["type"][0] == specialType)
				{
					id = 9901;
					break;
				}
			}
		}
		this.LoadRandomFuBen(id, specialType);
		this.avatar.zulinContorl.kezhanLastScence = Tools.getScreenName();
		this.avatar.lastFuBenScence = Tools.getScreenName();
		this.avatar.NowFuBen = "FRandomBase";
		this.avatar.NowRandomFuBenID = id;
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Tools.instance.Save(@int, 0, null);
		Tools.instance.loadMapScenes("FRandomBase", true);
	}

	// Token: 0x060022A3 RID: 8867 RVA: 0x0011CDFC File Offset: 0x0011AFFC
	private void LoadRandomFuBen(int id, int specialType)
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.RandomMapList)
		{
			if ((int)keyValuePair.Value["id"] == id)
			{
				this.LoadRandomFuBen(keyValuePair.Value, specialType);
				break;
			}
		}
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x0011CE70 File Offset: 0x0011B070
	private void LoadRandomFuBen(JToken json, int specialType)
	{
		int fuBenId = (int)json["id"];
		this.InitFuBenJson(fuBenId);
		if (!this.CanRandom(json))
		{
			return;
		}
		try
		{
			this.CreateRandomFuBen(json, specialType);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		this.ResetAvatarFuBenInfo(json);
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x0011CEC8 File Offset: 0x0011B0C8
	public void ResetAvatarFuBenInfo(JToken json)
	{
		int num = (int)json["id"];
		this.avatar.RandomFuBenList[num.ToString()]["startTime"] = this.avatar.worldTimeMag.nowTime;
		this.avatar.RandomFuBenList[num.ToString()]["ShouldReset"] = false;
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x0011CF44 File Offset: 0x0011B144
	private void CreateRandomFuBen(JToken json, int specialType = -1)
	{
		int num = (int)json["id"];
		JToken FuBenJson = this.avatar.RandomFuBenList[num.ToString()];
		if (specialType > 0)
		{
			FuBenJson["type"] = specialType;
		}
		else
		{
			FuBenJson["type"] = this.getFuBentype(json);
		}
		FuBenJson["ShuXin"] = this.getFuBenShuXin(json);
		if (((JArray)json["nandu"]).Count > 0)
		{
			FuBenJson["NamDu"] = Tools.getRandomInt((int)json["nandu"][0], (int)json["nandu"][0]);
		}
		else
		{
			FuBenJson["NamDu"] = 1;
		}
		JToken jtoken = jsonData.instance.RandomMapType[((int)FuBenJson["type"]).ToString()];
		FuBenJson["high"] = Tools.getRandomInt((int)jtoken["high"][0], (int)jtoken["high"][1]);
		FuBenJson["wide"] = Tools.getRandomInt((int)jtoken["wide"][0], (int)jtoken["wide"][1]);
		string text = (string)Tools.RandomGetToken<JToken>(Tools.FindAllJTokens(jsonData.instance.RandomMapFirstName, (JToken aa) => (int)aa["shuxin"] == (int)FuBenJson["ShuXin"]))["name"] + (string)jtoken["LastName"];
		FuBenJson["Name"] = text;
		FuBenJson["UUID"] = Tools.getUUID();
		this.CreateRoadNode(FuBenJson, json);
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x0011D1AC File Offset: 0x0011B3AC
	public void CreateRoadNode(JToken FuBenJson, JToken mapJson)
	{
		int high = (int)FuBenJson["high"];
		int wide = (int)FuBenJson["wide"];
		FuBenMap fuBenMap = new FuBenMap(high, wide);
		fuBenMap.CreateAllNode(this.avatar, FuBenJson, mapJson);
		for (int i = 0; i < fuBenMap.Wide; i++)
		{
			string text = "";
			for (int j = 0; j < fuBenMap.High; j++)
			{
				text += fuBenMap.map[i, j];
			}
			Debug.Log(text);
		}
		int shuxing = (int)FuBenJson["ShuXin"];
		int type = (int)FuBenJson["type"];
		int namdu = (int)FuBenJson["NamDu"];
		int weizhi = (int)mapJson["weizhi"];
		List<JToken> list = Tools.FindAllJTokens(jsonData.instance.RandomMapEventList, delegate(JToken aa)
		{
			if ((Tools.HasItems(aa["shuxing"], shuxing) || ((JArray)aa["shuxing"]).Count == 0) && (Tools.HasItems(aa["type"], type) || ((JArray)aa["type"]).Count == 0) && (Tools.HasItems(aa["weizhi"], weizhi) || ((JArray)aa["weizhi"]).Count == 0) && (int)aa["nandu"][0] <= namdu && (int)aa["nandu"][1] >= namdu)
			{
				if (((JArray)aa["EventValue"]).Count <= 0)
				{
					return true;
				}
				if (Avatar.ManZuValue((int)aa["EventValue"][0], (int)aa["EventValue"][1], (string)aa["fuhao"]))
				{
					return true;
				}
			}
			return false;
		});
		List<JToken> list2 = list.FindAll((JToken aa) => (int)aa["EventType"] == 1);
		List<JToken> list3 = list.FindAll((JToken aa) => (int)aa["EventType"] == 2);
		if (list3.Count == 0 || list2.Count == 0)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"能够随机到的奖励或随机事件为空  属性:",
				shuxing,
				" 类型:",
				type
			}));
		}
		List<int> list4 = new List<int>();
		List<int> list5 = new List<int>();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Entrance, list4, list5);
		List<int> list6 = new List<int>();
		List<int> list7 = new List<int>();
		FuBenJson["Award"] = new JArray();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Award, list6, list7);
		this.AddFuBenJsonNode(fuBenMap, list2, FuBenJson, "Award", list6, list7);
		List<int> list8 = new List<int>();
		List<int> list9 = new List<int>();
		FuBenJson["Event"] = new JArray();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Event, list8, list9);
		this.AddFuBenJsonNode(fuBenMap, list3, FuBenJson, "Event", list8, list9);
		int num = this.avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
		{
			(int)mapJson["id"]
		});
		if (num != -1)
		{
			int index = jsonData.GetRandom() % list6.Count;
			new JObject();
			JSONObject jsonobject = this.avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int>
			{
				(int)mapJson["id"]
			});
			FuBenJson["TaskTalkID"] = int.Parse(jsonobject["talkID"].str);
			FuBenJson["TaskIndex"] = fuBenMap.mapIndex[list6[index], list7[index]];
		}
		FuBenJson["Map"] = new JArray();
		for (int k = 0; k < fuBenMap.Wide; k++)
		{
			((JArray)FuBenJson["Map"]).Add(new JArray());
			for (int l = 0; l < fuBenMap.High; l++)
			{
				((JArray)FuBenJson["Map"][k]).Add(fuBenMap.map[k, l]);
			}
		}
		this.avatar.fubenContorl[(string)FuBenJson["UUID"]].setFirstIndex(fuBenMap.mapIndex[list4[0], list5[0]]);
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x0011D57C File Offset: 0x0011B77C
	public void AddFuBenJsonNode(FuBenMap map, List<JToken> EventRandomJson, JToken FuBenJson, string type, List<int> listEventX, List<int> listEventY)
	{
		List<int> list = new List<int>();
		int num = 0;
		int num2 = 0;
		while (num2 < listEventX.Count && num < 1000)
		{
			num++;
			if (EventRandomJson.Count <= 0)
			{
				Debug.LogError("副本随机事件数量不足");
				return;
			}
			JToken randomListByPercent = Tools.instance.getRandomListByPercent(EventRandomJson, "percent");
			if ((int)randomListByPercent["fenzu"] != 0)
			{
				List<JToken> list2 = new List<JToken>();
				foreach (JToken jtoken in EventRandomJson)
				{
					if ((int)jtoken["fenzu"] == (int)randomListByPercent["fenzu"])
					{
						list2.Add(jtoken);
					}
				}
				foreach (JToken item in list2)
				{
					EventRandomJson.Remove(item);
				}
			}
			if ((int)randomListByPercent["duoci"] == 0)
			{
				EventRandomJson.Remove(randomListByPercent);
			}
			list.Add((int)randomListByPercent["id"]);
			JObject jobject = new JObject();
			jobject["ID"] = (int)randomListByPercent["id"];
			jobject["Index"] = map.mapIndex[listEventX[num2], listEventY[num2]];
			((JArray)FuBenJson[type]).Add(jobject);
			num2++;
		}
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x0011D738 File Offset: 0x0011B938
	public int getFuBenShuXin(JToken json)
	{
		int num = this.avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
		{
			(int)json["id"]
		});
		if (num != -1)
		{
			using (List<JSONObject>.Enumerator enumerator = this.avatar.nomelTaskMag.getWhereTaskChildShuxingList(num).list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.I;
				}
			}
		}
		if (((JArray)json["shuxingPercent"]).Count <= 0)
		{
			return 1;
		}
		int randomByJToken = Tools.GetRandomByJToken(json["shuxingPercent"]);
		return (int)json["shuxing"][randomByJToken];
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x0011D814 File Offset: 0x0011BA14
	public int getFuBentype(JToken json)
	{
		int num = this.avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
		{
			(int)json["id"]
		});
		if (num != -1)
		{
			using (List<JSONObject>.Enumerator enumerator = this.avatar.nomelTaskMag.getWhereTaskChildTypeList(num).list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.I;
				}
			}
		}
		if (EndlessSeaMag.Inst != null)
		{
			int seaIslandIndex = this.avatar.seaNodeMag.GetSeaIslandIndex((int)json["id"]);
			int realIndex = EndlessSeaMag.GetRealIndex((int)json["id"], seaIslandIndex);
			int indexX = FuBenMap.getIndexX(realIndex, EndlessSeaMag.MapWide);
			int indexY = FuBenMap.getIndexY(realIndex, EndlessSeaMag.MapWide);
			int num2 = 0;
			new Dictionary<int, int>();
			for (int i = -2; i <= 2; i++)
			{
				for (int j = -2; j <= 2; j++)
				{
					if (indexX + i >= 0 && indexX + i <= EndlessSeaMag.MapWide && indexY + j >= 0 && indexY + j <= 69)
					{
						int x = Mathf.Clamp(indexX + i, 0, EndlessSeaMag.MapWide);
						int y = Mathf.Clamp(indexY + j, 0, 69);
						int index = FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide);
						if (this.avatar.seaNodeMag.GetIndexFengBaoLv(index, EndlessSeaMag.MapWide) > 0)
						{
							num2++;
						}
					}
				}
			}
			using (List<SeaAvatarObjBase>.Enumerator enumerator2 = EndlessSeaMag.Inst.MonstarList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.NowMapIndex == realIndex)
					{
						return 2;
					}
				}
			}
			if (num2 >= 20)
			{
				return 3;
			}
			return 1;
		}
		else
		{
			if (((JArray)json["type"]).Count <= 0)
			{
				return 1;
			}
			return (int)Tools.RandomGetArrayToken(json["type"]);
		}
		int result;
		return result;
	}

	// Token: 0x060022AB RID: 8875 RVA: 0x0011DA3C File Offset: 0x0011BC3C
	public void InitFuBenJson(int FuBenId)
	{
		if (!this.avatar.RandomFuBenList.ContainsKey(FuBenId.ToString()))
		{
			this.avatar.RandomFuBenList[FuBenId.ToString()] = new JObject();
			this.avatar.RandomFuBenList[FuBenId.ToString()]["startTime"] = "1-1-1";
			this.avatar.RandomFuBenList[FuBenId.ToString()]["ShouldReset"] = true;
		}
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x0011DAD0 File Offset: 0x0011BCD0
	public bool CanRandom(JToken json)
	{
		int num = (int)json["id"];
		return (bool)this.avatar.RandomFuBenList[num.ToString()]["ShouldReset"] || this.avatar.RandomFuBenList[num.ToString()]["type"] == null;
	}

	// Token: 0x04001DDB RID: 7643
	private Avatar avatar;
}
