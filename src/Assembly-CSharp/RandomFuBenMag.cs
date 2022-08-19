using System;
using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020003BB RID: 955
public class RandomFuBenMag
{
	// Token: 0x06001F1A RID: 7962 RVA: 0x000D9D01 File Offset: 0x000D7F01
	public RandomFuBenMag(Avatar _avatar)
	{
		this.avatar = _avatar;
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x000D9D10 File Offset: 0x000D7F10
	public void AutoSetRandomFuBen()
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.RandomMapList)
		{
			this.SetRandomFuBenShouldReset(keyValuePair.Value);
		}
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x000D9D68 File Offset: 0x000D7F68
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

	// Token: 0x06001F1D RID: 7965 RVA: 0x000D9E41 File Offset: 0x000D8041
	public void OutRandomFuBen()
	{
		Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence, true);
		this.avatar.lastFuBenScence = "";
		this.avatar.NowFuBen = "";
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x000D9E80 File Offset: 0x000D8080
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

	// Token: 0x06001F1F RID: 7967 RVA: 0x000D9F6C File Offset: 0x000D816C
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

	// Token: 0x06001F20 RID: 7968 RVA: 0x000D9FE0 File Offset: 0x000D81E0
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

	// Token: 0x06001F21 RID: 7969 RVA: 0x000DA038 File Offset: 0x000D8238
	public void ResetAvatarFuBenInfo(JToken json)
	{
		int num = (int)json["id"];
		this.avatar.RandomFuBenList[num.ToString()]["startTime"] = this.avatar.worldTimeMag.nowTime;
		this.avatar.RandomFuBenList[num.ToString()]["ShouldReset"] = false;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x000DA0B4 File Offset: 0x000D82B4
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

	// Token: 0x06001F23 RID: 7971 RVA: 0x000DA31C File Offset: 0x000D851C
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

	// Token: 0x06001F24 RID: 7972 RVA: 0x000DA6EC File Offset: 0x000D88EC
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

	// Token: 0x06001F25 RID: 7973 RVA: 0x000DA8A8 File Offset: 0x000D8AA8
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

	// Token: 0x06001F26 RID: 7974 RVA: 0x000DA984 File Offset: 0x000D8B84
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

	// Token: 0x06001F27 RID: 7975 RVA: 0x000DABAC File Offset: 0x000D8DAC
	public void InitFuBenJson(int FuBenId)
	{
		if (!this.avatar.RandomFuBenList.ContainsKey(FuBenId.ToString()))
		{
			this.avatar.RandomFuBenList[FuBenId.ToString()] = new JObject();
			this.avatar.RandomFuBenList[FuBenId.ToString()]["startTime"] = "1-1-1";
			this.avatar.RandomFuBenList[FuBenId.ToString()]["ShouldReset"] = true;
		}
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x000DAC40 File Offset: 0x000D8E40
	public bool CanRandom(JToken json)
	{
		int num = (int)json["id"];
		return (bool)this.avatar.RandomFuBenList[num.ToString()]["ShouldReset"] || this.avatar.RandomFuBenList[num.ToString()]["type"] == null;
	}

	// Token: 0x0400195D RID: 6493
	private Avatar avatar;
}
