using System;
using System.Collections.Generic;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class RandomFuBenMag
{
	private Avatar avatar;

	public RandomFuBenMag(Avatar _avatar)
	{
		avatar = _avatar;
	}

	public void AutoSetRandomFuBen()
	{
		foreach (KeyValuePair<string, JToken> randomMap in jsonData.instance.RandomMapList)
		{
			SetRandomFuBenShouldReset(randomMap.Value);
		}
	}

	public void SetRandomFuBenShouldReset(JToken json)
	{
		int fuBenId = (int)json[(object)"id"];
		InitFuBenJson(fuBenId);
		DateTime startTime = DateTime.Parse((string)avatar.RandomFuBenList[fuBenId.ToString()][(object)"startTime"]);
		DateTime nowTime = avatar.worldTimeMag.getNowTime();
		if (!(bool)avatar.RandomFuBenList[fuBenId.ToString()][(object)"ShouldReset"])
		{
			int value = (int)json[(object)"resetYear"];
			if (!Tools.instance.IsInTime(nowTime, startTime, startTime.AddYears(value)))
			{
				avatar.RandomFuBenList[fuBenId.ToString()][(object)"ShouldReset"] = JToken.op_Implicit(true);
			}
		}
	}

	public void OutRandomFuBen()
	{
		Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastFuBenScence);
		avatar.lastFuBenScence = "";
		avatar.NowFuBen = "";
	}

	public void GetInRandomFuBen(int id, int specialType = -1)
	{
		if (specialType > 0)
		{
			foreach (KeyValuePair<string, JToken> randomMap in jsonData.instance.RandomMapList)
			{
				if ((int)randomMap.Value[(object)"type"][(object)0] == specialType)
				{
					id = 9901;
					break;
				}
			}
		}
		LoadRandomFuBen(id, specialType);
		avatar.zulinContorl.kezhanLastScence = Tools.getScreenName();
		avatar.lastFuBenScence = Tools.getScreenName();
		avatar.NowFuBen = "FRandomBase";
		avatar.NowRandomFuBenID = id;
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Tools.instance.Save(@int, 0);
		Tools.instance.loadMapScenes("FRandomBase");
	}

	private void LoadRandomFuBen(int id, int specialType)
	{
		foreach (KeyValuePair<string, JToken> randomMap in jsonData.instance.RandomMapList)
		{
			if ((int)randomMap.Value[(object)"id"] == id)
			{
				LoadRandomFuBen(randomMap.Value, specialType);
				break;
			}
		}
	}

	private void LoadRandomFuBen(JToken json, int specialType)
	{
		int fuBenId = (int)json[(object)"id"];
		InitFuBenJson(fuBenId);
		if (CanRandom(json))
		{
			try
			{
				CreateRandomFuBen(json, specialType);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
			ResetAvatarFuBenInfo(json);
		}
	}

	public void ResetAvatarFuBenInfo(JToken json)
	{
		int num = (int)json[(object)"id"];
		avatar.RandomFuBenList[num.ToString()][(object)"startTime"] = JToken.op_Implicit(avatar.worldTimeMag.nowTime);
		avatar.RandomFuBenList[num.ToString()][(object)"ShouldReset"] = JToken.op_Implicit(false);
	}

	private void CreateRandomFuBen(JToken json, int specialType = -1)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		int num = (int)json[(object)"id"];
		JToken FuBenJson = avatar.RandomFuBenList[num.ToString()];
		if (specialType > 0)
		{
			FuBenJson[(object)"type"] = JToken.op_Implicit(specialType);
		}
		else
		{
			FuBenJson[(object)"type"] = JToken.op_Implicit(getFuBentype(json));
		}
		FuBenJson[(object)"ShuXin"] = JToken.op_Implicit(getFuBenShuXin(json));
		if (((JContainer)(JArray)json[(object)"nandu"]).Count > 0)
		{
			FuBenJson[(object)"NamDu"] = JToken.op_Implicit(Tools.getRandomInt((int)json[(object)"nandu"][(object)0], (int)json[(object)"nandu"][(object)0]));
		}
		else
		{
			FuBenJson[(object)"NamDu"] = JToken.op_Implicit(1);
		}
		JToken val = jsonData.instance.RandomMapType[((int)FuBenJson[(object)"type"]).ToString()];
		FuBenJson[(object)"high"] = JToken.op_Implicit(Tools.getRandomInt((int)val[(object)"high"][(object)0], (int)val[(object)"high"][(object)1]));
		FuBenJson[(object)"wide"] = JToken.op_Implicit(Tools.getRandomInt((int)val[(object)"wide"][(object)0], (int)val[(object)"wide"][(object)1]));
		string text = (string)Tools.RandomGetToken(Tools.FindAllJTokens((JToken)(object)jsonData.instance.RandomMapFirstName, (JToken aa) => (int)aa[(object)"shuxin"] == (int)FuBenJson[(object)"ShuXin"]))[(object)"name"] + (string)val[(object)"LastName"];
		FuBenJson[(object)"Name"] = JToken.op_Implicit(text);
		FuBenJson[(object)"UUID"] = JToken.op_Implicit(Tools.getUUID());
		CreateRoadNode(FuBenJson, json);
	}

	public void CreateRoadNode(JToken FuBenJson, JToken mapJson)
	{
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		int high = (int)FuBenJson[(object)"high"];
		int wide = (int)FuBenJson[(object)"wide"];
		FuBenMap fuBenMap = new FuBenMap(high, wide);
		fuBenMap.CreateAllNode(avatar, FuBenJson, mapJson);
		for (int i = 0; i < fuBenMap.Wide; i++)
		{
			string text = "";
			for (int j = 0; j < fuBenMap.High; j++)
			{
				text += fuBenMap.map[i, j];
			}
			Debug.Log((object)text);
		}
		int shuxing = (int)FuBenJson[(object)"ShuXin"];
		int type = (int)FuBenJson[(object)"type"];
		int namdu = (int)FuBenJson[(object)"NamDu"];
		int weizhi = (int)mapJson[(object)"weizhi"];
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)jsonData.instance.RandomMapEventList, delegate(JToken aa)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			if ((Tools.HasItems(aa[(object)"shuxing"], shuxing) || ((JContainer)(JArray)aa[(object)"shuxing"]).Count == 0) && (Tools.HasItems(aa[(object)"type"], type) || ((JContainer)(JArray)aa[(object)"type"]).Count == 0) && (Tools.HasItems(aa[(object)"weizhi"], weizhi) || ((JContainer)(JArray)aa[(object)"weizhi"]).Count == 0) && (int)aa[(object)"nandu"][(object)0] <= namdu && (int)aa[(object)"nandu"][(object)1] >= namdu)
			{
				if (((JContainer)(JArray)aa[(object)"EventValue"]).Count <= 0)
				{
					return true;
				}
				if (Avatar.ManZuValue((int)aa[(object)"EventValue"][(object)0], (int)aa[(object)"EventValue"][(object)1], (string)aa[(object)"fuhao"]))
				{
					return true;
				}
			}
			return false;
		});
		List<JToken> list2 = list.FindAll((JToken aa) => (int)aa[(object)"EventType"] == 1);
		List<JToken> list3 = list.FindAll((JToken aa) => (int)aa[(object)"EventType"] == 2);
		if (list3.Count == 0 || list2.Count == 0)
		{
			Debug.LogError((object)("能够随机到的奖励或随机事件为空  属性:" + shuxing + " 类型:" + type));
		}
		List<int> list4 = new List<int>();
		List<int> list5 = new List<int>();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Entrance, list4, list5);
		List<int> list6 = new List<int>();
		List<int> list7 = new List<int>();
		FuBenJson[(object)"Award"] = (JToken)new JArray();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Award, list6, list7);
		AddFuBenJsonNode(fuBenMap, list2, FuBenJson, "Award", list6, list7);
		List<int> list8 = new List<int>();
		List<int> list9 = new List<int>();
		FuBenJson[(object)"Event"] = (JToken)new JArray();
		fuBenMap.getAllMapIndex(FuBenMap.NodeType.Event, list8, list9);
		AddFuBenJsonNode(fuBenMap, list3, FuBenJson, "Event", list8, list9);
		int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { (int)mapJson[(object)"id"] });
		if (num != -1)
		{
			int index = jsonData.GetRandom() % list6.Count;
			new JObject();
			JSONObject jSONObject = avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int> { (int)mapJson[(object)"id"] });
			FuBenJson[(object)"TaskTalkID"] = JToken.op_Implicit(int.Parse(jSONObject["talkID"].str));
			FuBenJson[(object)"TaskIndex"] = JToken.op_Implicit(fuBenMap.mapIndex[list6[index], list7[index]]);
		}
		FuBenJson[(object)"Map"] = (JToken)new JArray();
		for (int k = 0; k < fuBenMap.Wide; k++)
		{
			((JArray)FuBenJson[(object)"Map"]).Add((JToken)new JArray());
			for (int l = 0; l < fuBenMap.High; l++)
			{
				((JArray)FuBenJson[(object)"Map"][(object)k]).Add(JToken.op_Implicit(fuBenMap.map[k, l]));
			}
		}
		avatar.fubenContorl[(string)FuBenJson[(object)"UUID"]].setFirstIndex(fuBenMap.mapIndex[list4[0], list5[0]]);
	}

	public void AddFuBenJsonNode(FuBenMap map, List<JToken> EventRandomJson, JToken FuBenJson, string type, List<int> listEventX, List<int> listEventY)
	{
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		int num = 0;
		for (int i = 0; i < listEventX.Count; i++)
		{
			if (num >= 1000)
			{
				break;
			}
			num++;
			if (EventRandomJson.Count <= 0)
			{
				Debug.LogError((object)"副本随机事件数量不足");
				break;
			}
			JToken randomListByPercent = Tools.instance.getRandomListByPercent(EventRandomJson, "percent");
			if ((int)randomListByPercent[(object)"fenzu"] != 0)
			{
				List<JToken> list2 = new List<JToken>();
				foreach (JToken item in EventRandomJson)
				{
					if ((int)item[(object)"fenzu"] == (int)randomListByPercent[(object)"fenzu"])
					{
						list2.Add(item);
					}
				}
				foreach (JToken item2 in list2)
				{
					EventRandomJson.Remove(item2);
				}
			}
			if ((int)randomListByPercent[(object)"duoci"] == 0)
			{
				EventRandomJson.Remove(randomListByPercent);
			}
			list.Add((int)randomListByPercent[(object)"id"]);
			JObject val = new JObject();
			val["ID"] = JToken.op_Implicit((int)randomListByPercent[(object)"id"]);
			val["Index"] = JToken.op_Implicit(map.mapIndex[listEventX[i], listEventY[i]]);
			((JArray)FuBenJson[(object)type]).Add((JToken)(object)val);
		}
	}

	public int getFuBenShuXin(JToken json)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { (int)json[(object)"id"] });
		if (num != -1)
		{
			using List<JSONObject>.Enumerator enumerator = avatar.nomelTaskMag.getWhereTaskChildShuxingList(num).list.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return enumerator.Current.I;
			}
		}
		if (((JContainer)(JArray)json[(object)"shuxingPercent"]).Count <= 0)
		{
			return 1;
		}
		int randomByJToken = Tools.GetRandomByJToken(json[(object)"shuxingPercent"]);
		return (int)json[(object)"shuxing"][(object)randomByJToken];
	}

	public int getFuBentype(JToken json)
	{
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { (int)json[(object)"id"] });
		if (num != -1)
		{
			using List<JSONObject>.Enumerator enumerator = avatar.nomelTaskMag.getWhereTaskChildTypeList(num).list.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return enumerator.Current.I;
			}
		}
		if ((Object)(object)EndlessSeaMag.Inst != (Object)null)
		{
			int seaIslandIndex = avatar.seaNodeMag.GetSeaIslandIndex((int)json[(object)"id"]);
			int realIndex = EndlessSeaMag.GetRealIndex((int)json[(object)"id"], seaIslandIndex);
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
						if (avatar.seaNodeMag.GetIndexFengBaoLv(index, EndlessSeaMag.MapWide) > 0)
						{
							num2++;
						}
					}
				}
			}
			foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
			{
				if (monstar.NowMapIndex == realIndex)
				{
					return 2;
				}
			}
			if (num2 >= 20)
			{
				return 3;
			}
			return 1;
		}
		if (((JContainer)(JArray)json[(object)"type"]).Count <= 0)
		{
			return 1;
		}
		return (int)Tools.RandomGetArrayToken(json[(object)"type"]);
	}

	public void InitFuBenJson(int FuBenId)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		if (!avatar.RandomFuBenList.ContainsKey(FuBenId.ToString()))
		{
			avatar.RandomFuBenList[FuBenId.ToString()] = (JToken)new JObject();
			avatar.RandomFuBenList[FuBenId.ToString()][(object)"startTime"] = JToken.op_Implicit("1-1-1");
			avatar.RandomFuBenList[FuBenId.ToString()][(object)"ShouldReset"] = JToken.op_Implicit(true);
		}
	}

	public bool CanRandom(JToken json)
	{
		int num = (int)json[(object)"id"];
		if (!(bool)avatar.RandomFuBenList[num.ToString()][(object)"ShouldReset"])
		{
			if (avatar.RandomFuBenList[num.ToString()][(object)"type"] == null)
			{
				return true;
			}
			return false;
		}
		return true;
	}
}
