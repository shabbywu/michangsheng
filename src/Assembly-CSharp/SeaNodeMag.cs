using System;
using System.Collections.Generic;
using System.IO;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SeaNodeMag
{
	private Avatar avatar;

	public SeaNodeMag(Avatar _avatar)
	{
		avatar = _avatar;
	}

	public void INITSEA()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		CreatSeaIsland();
		SetHaiYuAnQuAnDengJi();
		SetLuanLiuLv();
		if (!avatar.EndlessSeaAvatarSeeIsland.ContainsKey("Island"))
		{
			avatar.EndlessSeaAvatarSeeIsland["Island"] = (JToken)new JArray();
		}
	}

	public JToken FindSeaMonsetar(string monstaruuid, int seaID)
	{
		foreach (JToken item in (IEnumerable<JToken>)avatar.EndlessSeaRandomNode[seaID.ToString()][(object)"Monstar"])
		{
			if (monstaruuid == (string)item[(object)"uuid"])
			{
				return item;
			}
		}
		return null;
	}

	public void RemoveSeaMonstar(string monstaruuid, int seaID)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		JToken val = avatar.EndlessSeaRandomNode[seaID.ToString()][(object)"Monstar"];
		JToken val2 = FindSeaMonsetar(monstaruuid, seaID);
		if (val2 != null)
		{
			((JArray)val).Remove(val2);
		}
	}

	public void RemoveSeaMonstar(string monstaruuid)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<string, JToken> item in avatar.EndlessSeaRandomNode)
		{
			foreach (JToken item2 in (IEnumerable<JToken>)item.Value[(object)"Monstar"])
			{
				if ((string)item2[(object)"uuid"] == monstaruuid)
				{
					((JArray)item.Value[(object)"Monstar"]).Remove(item2);
					break;
				}
			}
		}
	}

	public void SetSeaMonstarIndex(string monstaruuid, int seaID, int index)
	{
		JToken val = FindSeaMonsetar(monstaruuid, seaID);
		if (val != null)
		{
			val[(object)"index"] = JToken.op_Implicit(index);
		}
	}

	public int GetSeaIslandIndex(int seaid)
	{
		return (int)avatar.EndlessSea["AllIaLand"][(object)(seaid - 1)];
	}

	public void CreatSeaIsland()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		if (!avatar.EndlessSea.ContainsKey("AllIaLand"))
		{
			JArray val = new JArray();
			avatar.EndlessSea["AllIaLand"] = (JToken)(object)val;
		}
		FuBenMap fuBenMap = new FuBenMap(7, 7);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		foreach (SeaHaiYuJiZhiShuaXin data in SeaHaiYuJiZhiShuaXin.DataList)
		{
			foreach (int item in data.WeiZhi)
			{
				list2.Add(item);
			}
		}
		foreach (KeyValuePair<string, JToken> item2 in jsonData.instance.SeaStaticIsland)
		{
			int allMapIndex = (int)item2.Value[(object)"IsLandIndex"];
			int inSeaID = GetInSeaID(allMapIndex, EndlessSeaMag.MapWide);
			list.Add(inSeaID);
		}
		for (int i = ((JContainer)(JArray)avatar.EndlessSea["AllIaLand"]).Count; i < ((JContainer)jsonData.instance.EndlessSeaType).Count; i++)
		{
			if (list.Contains(i + 1))
			{
				((JArray)avatar.EndlessSea["AllIaLand"]).Add(JToken.op_Implicit(-1));
				continue;
			}
			bool flag;
			do
			{
				int num = jsonData.GetRandom() % 5 + 1;
				int num2 = jsonData.GetRandom() % 5 + 1;
				int realIndex = EndlessSeaMag.GetRealIndex(i + 1, fuBenMap.mapIndex[num, num2]);
				flag = list2.Contains(realIndex);
				if (!flag)
				{
					((JArray)avatar.EndlessSea["AllIaLand"]).Add(JToken.op_Implicit(fuBenMap.mapIndex[num, num2]));
				}
			}
			while (flag);
		}
	}

	private void CreateLuanLiuID()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		if (!avatar.EndlessSea.ContainsKey("LuanLiuId"))
		{
			JArray val = new JArray();
			avatar.EndlessSea["LuanLiuId"] = (JToken)(object)val;
		}
		if (!avatar.EndlessSea.ContainsKey("LuanLiuLV"))
		{
			JArray val2 = new JArray();
			avatar.EndlessSea["LuanLiuLV"] = (JToken)(object)val2;
		}
		for (int i = ((JContainer)(JArray)avatar.EndlessSea["LuanLiuId"]).Count; i < ((JContainer)jsonData.instance.EndlessSeaType).Count; i++)
		{
			((JArray)avatar.EndlessSea["LuanLiuId"]).Add(JToken.op_Implicit(jsonData.GetRandom() % 1000));
		}
		for (int j = ((JContainer)(JArray)avatar.EndlessSea["LuanLiuLV"]).Count; j < ((JContainer)jsonData.instance.EndlessSeaType).Count; j++)
		{
			((JArray)avatar.EndlessSea["LuanLiuLV"]).Add(JToken.op_Implicit(0));
		}
	}

	public void SetLuanLiuLv()
	{
		DateTime nowTime = avatar.worldTimeMag.getNowTime();
		if (avatar.EndlessSea.ContainsKey("LuanLiuResetTime"))
		{
			if (new List<int> { 5, 6, 7, 8, 11, 12, 1, 2 }.Contains(nowTime.Month))
			{
				return;
			}
			DateTime startTime = DateTime.Parse((string)avatar.EndlessSea["LuanLiuResetTime"]);
			DateTime endTime = startTime.AddDays(15.0);
			if (Tools.instance.IsInTime(nowTime, startTime, endTime))
			{
				return;
			}
		}
		CreateLuanLiuID();
		for (int i = 0; i < ((JContainer)jsonData.instance.EndlessSeaType).Count; i++)
		{
			avatar.EndlessSea["LuanLiuLV"][(object)i] = JToken.op_Implicit(1 + (int)avatar.EndlessSea["SafeLv"][(object)i]);
			avatar.EndlessSea["LuanLiuId"][(object)i] = JToken.op_Implicit(1 + (int)avatar.EndlessSea["LuanLiuId"][(object)i]);
		}
		avatar.EndlessSea["LuanLiuResetTime"] = JToken.op_Implicit(avatar.worldTimeMag.nowTime);
	}

	public int GetSeaIDLV(int seaID)
	{
		return (int)avatar.EndlessSea["SafeLv"][(object)(seaID - 1)];
	}

	public JToken GetFengBaoIndexList(int _seaid)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		int num = (int)((JArray)avatar.EndlessSea["SafeLv"])[_seaid - 1];
		JToken val = jsonData.instance.EndlessSeaLuanLiuRandom[num.ToString()];
		int num2 = (int)avatar.EndlessSea["LuanLiuId"][(object)(_seaid - 1)] % ((JContainer)(JArray)val).Count;
		return val[(object)num2];
	}

	public int GetIndexFengBaoLv(int AllMapIndex, int wide)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		int inSeaID = GetInSeaID(AllMapIndex, wide);
		int num = (int)((JArray)avatar.EndlessSea["SafeLv"])[inSeaID - 1];
		JToken val = jsonData.instance.EndlessSeaLuanLiuRandomMap[num.ToString()];
		int num2 = (int)avatar.EndlessSea["LuanLiuId"][(object)(inSeaID - 1)] % ((JContainer)(JArray)val).Count;
		int num3 = FuBenMap.getIndexX(AllMapIndex, wide) % 7;
		int num4 = FuBenMap.getIndexY(AllMapIndex, wide) % 7;
		return (int)val[(object)num2][(object)num4][(object)num3];
	}

	public int GetInSeaID(int AllMapIndex, int wide)
	{
		int indexX = FuBenMap.getIndexX(AllMapIndex, wide);
		return FuBenMap.getIndex(y: FuBenMap.getIndexY(AllMapIndex, wide) / 7, x: indexX / 7, wide: wide / 7);
	}

	public void CreateLuanLiuMap()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Expected O, but got Unknown
		JObject val = new JObject();
		JObject val2 = new JObject();
		JObject val3 = new JObject();
		for (int i = 1; i <= ((JContainer)jsonData.instance.EndlessSeaSafeLvData).Count; i++)
		{
			JArray val4 = new JArray();
			JArray val5 = new JArray();
			FuBenMap fuBenMap = new FuBenMap(7, 7);
			FuBenMap baseMapIndex = new FuBenMap(7, 7);
			JToken val6 = jsonData.instance.EndlessSeaSafeLvData[i.ToString()];
			int index = 1;
			CreatMapLuanLiuNode(fuBenMap, 4, (int)val6[(object)"value4"], baseMapIndex, ref index);
			CreatMapLuanLiuNode(fuBenMap, 3, (int)val6[(object)"value3"], baseMapIndex, ref index);
			CreatMapLuanLiuNode(fuBenMap, 2, (int)val6[(object)"value2"], baseMapIndex, ref index);
			CreatMapLuanLiuNode(fuBenMap, 1, (int)val6[(object)"value1"], baseMapIndex, ref index);
			for (int j = 0; j < 400; j++)
			{
				MoveNodePostion(fuBenMap, baseMapIndex);
				SaveNodePostion(fuBenMap, val4, baseMapIndex);
				JArray val7 = new JArray();
				foreach (List<int> item in fuBenMap.ToListList())
				{
					JArray val8 = new JArray();
					((JContainer)val8).Add((object)item);
					val7.Add((JToken)(object)val8);
				}
				val5.Add((JToken)(object)val7);
			}
			val2.Add(i.ToString(), (JToken)(object)val5);
			val.Add(i.ToString(), (JToken)(object)val4);
		}
		foreach (KeyValuePair<string, JToken> item2 in val)
		{
			JArray val9 = new JArray();
			foreach (JToken item3 in (IEnumerable<JToken>)item2.Value)
			{
				JArray val10 = new JArray();
				FuBenMap fuBenMap2 = new FuBenMap(7, 7);
				foreach (JToken item4 in (IEnumerable<JToken>)item3)
				{
					int indexX = FuBenMap.getIndexX((int)item4[(object)"index"], 7);
					int indexY = FuBenMap.getIndexY((int)item4[(object)"index"], 7);
					int num = (int)item4[(object)"lv"];
					JToken obj = jsonData.instance.EndlessSeaLuanLIuXinZhuang[num.ToString()];
					fuBenMap2.map[indexX, indexY] = num;
					List<int> list = new List<int>();
					List<int> list2 = new List<int>();
					int num2 = 0;
					foreach (JToken item5 in (IEnumerable<JToken>)obj[(object)"xinzhuangX"])
					{
						if (num2 % 2 == 0)
						{
							list.Add((int)item5);
						}
						else
						{
							list2.Add((int)item5);
						}
						num2++;
					}
					int num3 = 0;
					foreach (int item6 in list)
					{
						_ = item6;
						int num4 = Mathf.Clamp(indexX + list[num3], 0, 6);
						int num5 = Mathf.Clamp(indexY + list2[num3], 0, 6);
						if (fuBenMap2.map[num4, num5] < num)
						{
							fuBenMap2.map[num4, num5] = num;
						}
						num3++;
					}
				}
				foreach (List<int> item7 in fuBenMap2.ToListList())
				{
					JArray val11 = new JArray();
					((JContainer)val11).Add((object)item7);
					val10.Add((JToken)(object)val11);
				}
				val9.Add((JToken)(object)val10);
			}
			val3.Add(item2.Key, (JToken)(object)val9);
		}
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuMap.json", ((object)val).ToString());
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuJMap.json", ((object)val2).ToString());
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuRMap.json", ((object)val3).ToString());
	}

	public void MoveNodePostion(FuBenMap baseMap, FuBenMap BaseMapIndex)
	{
		for (int i = 0; i < 7; i++)
		{
			int num = 0;
			for (int j = 0; j < 7; j++)
			{
				if (baseMap.map[i, j] == 0)
				{
					continue;
				}
				int num2 = getWide(baseMap.map[i, j]) / 2;
				int num3 = getHigh(baseMap.map[i, j]) / 2;
				int num4 = 6 - num2;
				int num5 = num2;
				int num6 = 6 - num3;
				int num7 = num3;
				int num8 = jsonData.GetRandom() % 2;
				int num9 = ((num8 == 0) ? 1 : 0);
				int num10 = jsonData.GetRandom() % 2 + 1;
				int num11 = jsonData.GetRandom() % 2 + 1;
				int num12 = Mathf.Clamp((int)((float)i + Mathf.Pow(-1f, (float)num10) * (float)num8), num5, num4);
				int num13 = Mathf.Clamp((int)((float)j + Mathf.Pow(-1f, (float)num11) * (float)num9), num7, num6);
				if (num12 == i && num13 == j && num < 10)
				{
					j--;
					num++;
					continue;
				}
				if (num >= 10)
				{
					Debug.Log((object)"移动乱流时失败循环次数超过10次");
				}
				num = 0;
				RelizedMoveNode(baseMap, i, j, num12, num13, BaseMapIndex);
			}
		}
	}

	public bool RelizedMoveNode(FuBenMap baseMap, int startX, int startY, int endX, int endY, FuBenMap BaseMapIndex)
	{
		if (baseMap.map[endX, endY] == 0)
		{
			baseMap.map[endX, endY] = baseMap.map[startX, startY];
			baseMap.map[startX, startY] = 0;
			BaseMapIndex.map[endX, endY] = BaseMapIndex.map[startX, startY];
			BaseMapIndex.map[startX, startY] = 0;
			return true;
		}
		return false;
	}

	public int getWide(int type)
	{
		return (int)jsonData.instance.EndlessSeaLuanLIuXinZhuang[type.ToString()][(object)"wide"];
	}

	public int getHigh(int type)
	{
		return (int)jsonData.instance.EndlessSeaLuanLIuXinZhuang[type.ToString()][(object)"high"];
	}

	public void SaveNodePostion(FuBenMap baseMap, JArray lvJson, FuBenMap baseMapIndex)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		JArray val = new JArray();
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				if (baseMap.map[i, j] != 0)
				{
					JObject val2 = new JObject();
					val2["index"] = JToken.op_Implicit(baseMap.mapIndex[i, j]);
					val2["lv"] = JToken.op_Implicit(baseMap.map[i, j]);
					val2["id"] = JToken.op_Implicit(baseMapIndex.map[i, j]);
					val.Add((JToken)(object)val2);
				}
			}
		}
		lvJson.Add((JToken)(object)val);
	}

	public void CreatMapLuanLiuNode(FuBenMap baseMap, int Lv, int num, FuBenMap baseMapIndex, ref int index)
	{
		for (int i = 0; i < num; i++)
		{
			int num2 = getWide(Lv) / 2;
			int num3 = getHigh(Lv) / 2;
			int num4 = num2 + jsonData.GetRandom() % (7 - num2 * 2);
			int num5 = num3 + jsonData.GetRandom() % (7 - num3 * 2);
			if (baseMap.map[num4, num5] == 0)
			{
				baseMapIndex.map[num4, num5] = index;
				baseMap.map[num4, num5] = Lv;
				index++;
			}
			else
			{
				i--;
			}
		}
	}

	public void SetHaiYuAnQuAnDengJi()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		if (avatar.EndlessSea.ContainsKey("SafeResetTime"))
		{
			DateTime startTime = DateTime.Parse((string)avatar.EndlessSea["SafeResetTime"]);
			DateTime endTime = startTime.AddYears(10);
			if (Tools.instance.IsInTime(avatar.worldTimeMag.getNowTime(), startTime, endTime))
			{
				return;
			}
		}
		JArray val = new JArray();
		for (int i = 1; i <= ((JContainer)jsonData.instance.EndlessSeaType).Count; i++)
		{
			int num = (int)jsonData.instance.EndlessSeaType[i.ToString()][(object)"weixianLv"];
			JToken val2 = jsonData.instance.EndlessSeaData[num.ToString()];
			val.Add(JToken.op_Implicit(Tools.getRandomInt((int)val2[(object)"AnQuanLv"][(object)0], (int)val2[(object)"AnQuanLv"][(object)1])));
		}
		avatar.EndlessSea["SafeLv"] = (JToken)(object)val;
		avatar.EndlessSea["SafeResetTime"] = JToken.op_Implicit(avatar.worldTimeMag.nowTime);
	}
}
