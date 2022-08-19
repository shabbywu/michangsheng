using System;
using System.Collections.Generic;
using System.IO;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class SeaNodeMag
{
	// Token: 0x06001F29 RID: 7977 RVA: 0x000DACAE File Offset: 0x000D8EAE
	public SeaNodeMag(Avatar _avatar)
	{
		this.avatar = _avatar;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x000DACC0 File Offset: 0x000D8EC0
	public void INITSEA()
	{
		this.CreatSeaIsland();
		this.SetHaiYuAnQuAnDengJi();
		this.SetLuanLiuLv();
		if (!this.avatar.EndlessSeaAvatarSeeIsland.ContainsKey("Island"))
		{
			this.avatar.EndlessSeaAvatarSeeIsland["Island"] = new JArray();
		}
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x000DAD10 File Offset: 0x000D8F10
	public JToken FindSeaMonsetar(string monstaruuid, int seaID)
	{
		foreach (JToken jtoken in this.avatar.EndlessSeaRandomNode[seaID.ToString()]["Monstar"])
		{
			if (monstaruuid == (string)jtoken["uuid"])
			{
				return jtoken;
			}
		}
		return null;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x000DAD90 File Offset: 0x000D8F90
	public void RemoveSeaMonstar(string monstaruuid, int seaID)
	{
		JToken jtoken = this.avatar.EndlessSeaRandomNode[seaID.ToString()]["Monstar"];
		JToken jtoken2 = this.FindSeaMonsetar(monstaruuid, seaID);
		if (jtoken2 != null)
		{
			((JArray)jtoken).Remove(jtoken2);
		}
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x000DADD8 File Offset: 0x000D8FD8
	public void RemoveSeaMonstar(string monstaruuid)
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in this.avatar.EndlessSeaRandomNode)
		{
			foreach (JToken jtoken in keyValuePair.Value["Monstar"])
			{
				if ((string)jtoken["uuid"] == monstaruuid)
				{
					((JArray)keyValuePair.Value["Monstar"]).Remove(jtoken);
					break;
				}
			}
		}
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x000DAE9C File Offset: 0x000D909C
	public void SetSeaMonstarIndex(string monstaruuid, int seaID, int index)
	{
		JToken jtoken = this.FindSeaMonsetar(monstaruuid, seaID);
		if (jtoken != null)
		{
			jtoken["index"] = index;
		}
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x000DAEC6 File Offset: 0x000D90C6
	public int GetSeaIslandIndex(int seaid)
	{
		return (int)this.avatar.EndlessSea["AllIaLand"][seaid - 1];
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x000DAEF0 File Offset: 0x000D90F0
	public void CreatSeaIsland()
	{
		if (!this.avatar.EndlessSea.ContainsKey("AllIaLand"))
		{
			JArray jarray = new JArray();
			this.avatar.EndlessSea["AllIaLand"] = jarray;
		}
		FuBenMap fuBenMap = new FuBenMap(7, 7);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		foreach (SeaHaiYuJiZhiShuaXin seaHaiYuJiZhiShuaXin in SeaHaiYuJiZhiShuaXin.DataList)
		{
			foreach (int item in seaHaiYuJiZhiShuaXin.WeiZhi)
			{
				list2.Add(item);
			}
		}
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.SeaStaticIsland)
		{
			int allMapIndex = (int)keyValuePair.Value["IsLandIndex"];
			int inSeaID = this.GetInSeaID(allMapIndex, EndlessSeaMag.MapWide);
			list.Add(inSeaID);
		}
		for (int i = ((JArray)this.avatar.EndlessSea["AllIaLand"]).Count; i < jsonData.instance.EndlessSeaType.Count; i++)
		{
			if (list.Contains(i + 1))
			{
				((JArray)this.avatar.EndlessSea["AllIaLand"]).Add(-1);
			}
			else
			{
				bool flag;
				do
				{
					int num = jsonData.GetRandom() % 5 + 1;
					int num2 = jsonData.GetRandom() % 5 + 1;
					int realIndex = EndlessSeaMag.GetRealIndex(i + 1, fuBenMap.mapIndex[num, num2]);
					flag = list2.Contains(realIndex);
					if (!flag)
					{
						((JArray)this.avatar.EndlessSea["AllIaLand"]).Add(fuBenMap.mapIndex[num, num2]);
					}
				}
				while (flag);
			}
		}
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x000DB120 File Offset: 0x000D9320
	private void CreateLuanLiuID()
	{
		if (!this.avatar.EndlessSea.ContainsKey("LuanLiuId"))
		{
			JArray jarray = new JArray();
			this.avatar.EndlessSea["LuanLiuId"] = jarray;
		}
		if (!this.avatar.EndlessSea.ContainsKey("LuanLiuLV"))
		{
			JArray jarray2 = new JArray();
			this.avatar.EndlessSea["LuanLiuLV"] = jarray2;
		}
		for (int i = ((JArray)this.avatar.EndlessSea["LuanLiuId"]).Count; i < jsonData.instance.EndlessSeaType.Count; i++)
		{
			((JArray)this.avatar.EndlessSea["LuanLiuId"]).Add(jsonData.GetRandom() % 1000);
		}
		for (int j = ((JArray)this.avatar.EndlessSea["LuanLiuLV"]).Count; j < jsonData.instance.EndlessSeaType.Count; j++)
		{
			((JArray)this.avatar.EndlessSea["LuanLiuLV"]).Add(0);
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x000DB258 File Offset: 0x000D9458
	public void SetLuanLiuLv()
	{
		DateTime nowTime = this.avatar.worldTimeMag.getNowTime();
		if (this.avatar.EndlessSea.ContainsKey("LuanLiuResetTime"))
		{
			if (new List<int>
			{
				5,
				6,
				7,
				8,
				11,
				12,
				1,
				2
			}.Contains(nowTime.Month))
			{
				return;
			}
			DateTime startTime = DateTime.Parse((string)this.avatar.EndlessSea["LuanLiuResetTime"]);
			DateTime endTime = startTime.AddDays(15.0);
			if (Tools.instance.IsInTime(nowTime, startTime, endTime, 0))
			{
				return;
			}
		}
		this.CreateLuanLiuID();
		for (int i = 0; i < jsonData.instance.EndlessSeaType.Count; i++)
		{
			this.avatar.EndlessSea["LuanLiuLV"][i] = 1 + (int)this.avatar.EndlessSea["SafeLv"][i];
			this.avatar.EndlessSea["LuanLiuId"][i] = 1 + (int)this.avatar.EndlessSea["LuanLiuId"][i];
		}
		this.avatar.EndlessSea["LuanLiuResetTime"] = this.avatar.worldTimeMag.nowTime;
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x000DB408 File Offset: 0x000D9608
	public int GetSeaIDLV(int seaID)
	{
		return (int)this.avatar.EndlessSea["SafeLv"][seaID - 1];
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x000DB434 File Offset: 0x000D9634
	public JToken GetFengBaoIndexList(int _seaid)
	{
		int num = (int)((JArray)this.avatar.EndlessSea["SafeLv"])[_seaid - 1];
		JToken jtoken = jsonData.instance.EndlessSeaLuanLiuRandom[num.ToString()];
		int num2 = (int)this.avatar.EndlessSea["LuanLiuId"][_seaid - 1] % ((JArray)jtoken).Count;
		return jtoken[num2];
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000DB4C0 File Offset: 0x000D96C0
	public int GetIndexFengBaoLv(int AllMapIndex, int wide)
	{
		int inSeaID = this.GetInSeaID(AllMapIndex, wide);
		int num = (int)((JArray)this.avatar.EndlessSea["SafeLv"])[inSeaID - 1];
		JToken jtoken = jsonData.instance.EndlessSeaLuanLiuRandomMap[num.ToString()];
		int num2 = (int)this.avatar.EndlessSea["LuanLiuId"][inSeaID - 1] % ((JArray)jtoken).Count;
		int num3 = FuBenMap.getIndexX(AllMapIndex, wide) % 7;
		int num4 = FuBenMap.getIndexY(AllMapIndex, wide) % 7;
		return (int)jtoken[num2][num4][num3];
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x000DB588 File Offset: 0x000D9788
	public int GetInSeaID(int AllMapIndex, int wide)
	{
		int indexX = FuBenMap.getIndexX(AllMapIndex, wide);
		int indexY = FuBenMap.getIndexY(AllMapIndex, wide);
		return FuBenMap.getIndex(indexX / 7, indexY / 7, wide / 7);
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x000DB5B4 File Offset: 0x000D97B4
	public void CreateLuanLiuMap()
	{
		JObject jobject = new JObject();
		JObject jobject2 = new JObject();
		JObject jobject3 = new JObject();
		for (int i = 1; i <= jsonData.instance.EndlessSeaSafeLvData.Count; i++)
		{
			JArray jarray = new JArray();
			JArray jarray2 = new JArray();
			FuBenMap fuBenMap = new FuBenMap(7, 7);
			FuBenMap baseMapIndex = new FuBenMap(7, 7);
			JToken jtoken = jsonData.instance.EndlessSeaSafeLvData[i.ToString()];
			int num = 1;
			this.CreatMapLuanLiuNode(fuBenMap, 4, (int)jtoken["value4"], baseMapIndex, ref num);
			this.CreatMapLuanLiuNode(fuBenMap, 3, (int)jtoken["value3"], baseMapIndex, ref num);
			this.CreatMapLuanLiuNode(fuBenMap, 2, (int)jtoken["value2"], baseMapIndex, ref num);
			this.CreatMapLuanLiuNode(fuBenMap, 1, (int)jtoken["value1"], baseMapIndex, ref num);
			for (int j = 0; j < 400; j++)
			{
				this.MoveNodePostion(fuBenMap, baseMapIndex);
				this.SaveNodePostion(fuBenMap, jarray, baseMapIndex);
				JArray jarray3 = new JArray();
				foreach (List<int> list in fuBenMap.ToListList())
				{
					JArray jarray4 = new JArray();
					jarray4.Add(list);
					jarray3.Add(jarray4);
				}
				jarray2.Add(jarray3);
			}
			jobject2.Add(i.ToString(), jarray2);
			jobject.Add(i.ToString(), jarray);
		}
		foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
		{
			JArray jarray5 = new JArray();
			foreach (IEnumerable<JToken> enumerable in keyValuePair.Value)
			{
				JArray jarray6 = new JArray();
				FuBenMap fuBenMap2 = new FuBenMap(7, 7);
				foreach (JToken jtoken2 in enumerable)
				{
					int indexX = FuBenMap.getIndexX((int)jtoken2["index"], 7);
					int indexY = FuBenMap.getIndexY((int)jtoken2["index"], 7);
					int num2 = (int)jtoken2["lv"];
					JToken jtoken3 = jsonData.instance.EndlessSeaLuanLIuXinZhuang[num2.ToString()];
					fuBenMap2.map[indexX, indexY] = num2;
					List<int> list2 = new List<int>();
					List<int> list3 = new List<int>();
					int num3 = 0;
					foreach (JToken jtoken4 in jtoken3["xinzhuangX"])
					{
						if (num3 % 2 == 0)
						{
							list2.Add((int)jtoken4);
						}
						else
						{
							list3.Add((int)jtoken4);
						}
						num3++;
					}
					int num4 = 0;
					foreach (int num5 in list2)
					{
						int num6 = Mathf.Clamp(indexX + list2[num4], 0, 6);
						int num7 = Mathf.Clamp(indexY + list3[num4], 0, 6);
						if (fuBenMap2.map[num6, num7] < num2)
						{
							fuBenMap2.map[num6, num7] = num2;
						}
						num4++;
					}
				}
				foreach (List<int> list4 in fuBenMap2.ToListList())
				{
					JArray jarray7 = new JArray();
					jarray7.Add(list4);
					jarray6.Add(jarray7);
				}
				jarray5.Add(jarray6);
			}
			jobject3.Add(keyValuePair.Key, jarray5);
		}
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuMap.json", jobject.ToString());
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuJMap.json", jobject2.ToString());
		File.WriteAllText("C:\\michangsheng1res\\Res\\Effect\\json\\LuanLiuRMap.json", jobject3.ToString());
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x000DBA8C File Offset: 0x000D9C8C
	public void MoveNodePostion(FuBenMap baseMap, FuBenMap BaseMapIndex)
	{
		for (int i = 0; i < 7; i++)
		{
			int num = 0;
			for (int j = 0; j < 7; j++)
			{
				if (baseMap.map[i, j] != 0)
				{
					int num2 = this.getWide(baseMap.map[i, j]) / 2;
					int num3 = this.getHigh(baseMap.map[i, j]) / 2;
					int num4 = 6 - num2;
					int num5 = num2;
					int num6 = 6 - num3;
					int num7 = num3;
					int num8 = jsonData.GetRandom() % 2;
					int num9 = (num8 == 0) ? 1 : 0;
					int num10 = jsonData.GetRandom() % 2 + 1;
					int num11 = jsonData.GetRandom() % 2 + 1;
					int num12 = Mathf.Clamp((int)((float)i + Mathf.Pow(-1f, (float)num10) * (float)num8), num5, num4);
					int num13 = Mathf.Clamp((int)((float)j + Mathf.Pow(-1f, (float)num11) * (float)num9), num7, num6);
					if (num12 == i && num13 == j && num < 10)
					{
						j--;
						num++;
					}
					else
					{
						if (num >= 10)
						{
							Debug.Log("移动乱流时失败循环次数超过10次");
						}
						num = 0;
						this.RelizedMoveNode(baseMap, i, j, num12, num13, BaseMapIndex);
					}
				}
			}
		}
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x000DBBB4 File Offset: 0x000D9DB4
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

	// Token: 0x06001F3A RID: 7994 RVA: 0x000DBC2C File Offset: 0x000D9E2C
	public int getWide(int type)
	{
		return (int)jsonData.instance.EndlessSeaLuanLIuXinZhuang[type.ToString()]["wide"];
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x000DBC53 File Offset: 0x000D9E53
	public int getHigh(int type)
	{
		return (int)jsonData.instance.EndlessSeaLuanLIuXinZhuang[type.ToString()]["high"];
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x000DBC7C File Offset: 0x000D9E7C
	public void SaveNodePostion(FuBenMap baseMap, JArray lvJson, FuBenMap baseMapIndex)
	{
		JArray jarray = new JArray();
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				if (baseMap.map[i, j] != 0)
				{
					JObject jobject = new JObject();
					jobject["index"] = baseMap.mapIndex[i, j];
					jobject["lv"] = baseMap.map[i, j];
					jobject["id"] = baseMapIndex.map[i, j];
					jarray.Add(jobject);
				}
			}
		}
		lvJson.Add(jarray);
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x000DBD28 File Offset: 0x000D9F28
	public void CreatMapLuanLiuNode(FuBenMap baseMap, int Lv, int num, FuBenMap baseMapIndex, ref int index)
	{
		for (int i = 0; i < num; i++)
		{
			int num2 = this.getWide(Lv) / 2;
			int num3 = this.getHigh(Lv) / 2;
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

	// Token: 0x06001F3E RID: 7998 RVA: 0x000DBDB4 File Offset: 0x000D9FB4
	public void SetHaiYuAnQuAnDengJi()
	{
		if (this.avatar.EndlessSea.ContainsKey("SafeResetTime"))
		{
			DateTime startTime = DateTime.Parse((string)this.avatar.EndlessSea["SafeResetTime"]);
			DateTime endTime = startTime.AddYears(10);
			if (Tools.instance.IsInTime(this.avatar.worldTimeMag.getNowTime(), startTime, endTime, 0))
			{
				return;
			}
		}
		JArray jarray = new JArray();
		for (int i = 1; i <= jsonData.instance.EndlessSeaType.Count; i++)
		{
			int num = (int)jsonData.instance.EndlessSeaType[i.ToString()]["weixianLv"];
			JToken jtoken = jsonData.instance.EndlessSeaData[num.ToString()];
			jarray.Add(Tools.getRandomInt((int)jtoken["AnQuanLv"][0], (int)jtoken["AnQuanLv"][1]));
		}
		this.avatar.EndlessSea["SafeLv"] = jarray;
		this.avatar.EndlessSea["SafeResetTime"] = this.avatar.worldTimeMag.nowTime;
	}

	// Token: 0x0400195E RID: 6494
	private Avatar avatar;
}
