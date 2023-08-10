using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using GUIPackage;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using Tab;
using UnityEngine;
using UnityEngine.Events;
using YSGame.TuJian;

public static class PlayerEx
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Predicate<UINPCWuDaoData> _003C_003E9__41_0;

		public static Predicate<UINPCWuDaoData> _003C_003E9__41_1;

		public static UnityAction _003C_003E9__59_0;

		internal bool _003CDoShuangXiu_003Eb__41_0(UINPCWuDaoData w)
		{
			return w.ID == 6;
		}

		internal bool _003CDoShuangXiu_003Eb__41_1(UINPCWuDaoData w)
		{
			return w.ID == 7;
		}

		internal void _003CDeleteErrorItem_003Eb__59_0()
		{
			for (int num = Player.itemList.values.Count - 1; num >= 0; num--)
			{
				if (ErrorItemIDList.Contains(Player.itemList.values[num].itemId))
				{
					Player.itemList.values.RemoveAt(num);
				}
			}
			ErrorItemIDList.Clear();
			if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
		}
	}

	private static Dictionary<int, int> _ShengWangLevelDict = new Dictionary<int, int>();

	private static bool isShengWangInited;

	public static List<int> ErrorItemIDList = new List<int>();

	public static Avatar Player
	{
		get
		{
			if ((Object)(object)Tools.instance != (Object)null)
			{
				return Tools.instance.getPlayer();
			}
			return null;
		}
	}

	public static bool IsTheather(int npcid)
	{
		return Player.TeatherId.HasItem(npcid);
	}

	public static bool IsDaoLv(int npcid)
	{
		return Player.DaoLvId.HasItem(npcid);
	}

	public static string GetDaoLvNickName(int npcid)
	{
		string result = Player.lastName;
		if (Player.DaoLvChengHu.HasField(npcid.ToString()))
		{
			result = Player.DaoLvChengHu[npcid.ToString()].Str;
		}
		return result;
	}

	public static bool IsBrother(int npcid)
	{
		return Player.Brother.HasItem(npcid);
	}

	public static bool IsTuDi(int npcid)
	{
		return Player.TuDiId.HasItem(npcid);
	}

	public static bool IsDaTing(int npcid)
	{
		return Player.DaTingId.HasItem(npcid);
	}

	public static void AddDaTingNPC(int npcid)
	{
		if (!IsDaTing(npcid))
		{
			Player.DaTingId.Add(npcid);
		}
	}

	public static void AddRelationship(int npcid, bool teather, bool daolv, bool brother, bool tudi)
	{
		if (teather && !IsTheather(npcid))
		{
			Player.TeatherId.Add(npcid);
		}
		if (daolv && !IsDaoLv(npcid))
		{
			Player.DaoLvId.Add(npcid);
		}
		if (brother && !IsBrother(npcid))
		{
			Player.Brother.Add(npcid);
		}
		if (tudi && !IsTuDi(npcid))
		{
			Player.TuDiId.Add(npcid);
		}
	}

	public static void SubRelationship(int npcid, bool teather, bool daolv, bool brother, bool tudi)
	{
		if (teather && IsTheather(npcid))
		{
			List<int> list = Player.TeatherId.ToList();
			list.Remove(npcid);
			Player.TeatherId = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int item in list)
			{
				Player.TeatherId.Add(item);
			}
		}
		if (daolv && IsDaoLv(npcid))
		{
			List<int> list2 = Player.DaoLvId.ToList();
			list2.Remove(npcid);
			Player.DaoLvId = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int item2 in list2)
			{
				Player.DaoLvId.Add(item2);
			}
		}
		if (brother && IsBrother(npcid))
		{
			List<int> list3 = Player.Brother.ToList();
			list3.Remove(npcid);
			Player.Brother = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int item3 in list3)
			{
				Player.Brother.Add(item3);
			}
		}
		if (!tudi || !IsTuDi(npcid))
		{
			return;
		}
		List<int> list4 = Player.TuDiId.ToList();
		list4.Remove(npcid);
		Player.TuDiId = new JSONObject(JSONObject.Type.ARRAY);
		foreach (int item4 in list4)
		{
			Player.TuDiId.Add(item4);
		}
	}

	private static void InitShengWang()
	{
		if (isShengWangInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.ShengWangLevelData.list)
		{
			_ShengWangLevelDict.Add(item["id"].I, item["ShengWangQuJian"].list[0].I);
		}
		isShengWangInited = true;
	}

	public static int CalcShengWangLevel(int shengwang)
	{
		int result = 1;
		for (int i = 1; i <= 7 && shengwang >= _ShengWangLevelDict[i]; i++)
		{
			result = i;
		}
		return result;
	}

	private static float CalcShengWangProcess(int shengwang)
	{
		float result = 0f;
		float num = shengwang;
		if (shengwang >= -99999 && shengwang < -500)
		{
			return 1f;
		}
		if (shengwang >= -499 && shengwang < -50)
		{
			result = 0.66f + (0f - num - 50f) / 449f / 3f;
		}
		else if (shengwang >= -49 && shengwang < -10)
		{
			result = 0.33f + (0f - num - 10f) / 39f / 3f;
		}
		else if (shengwang >= -9 && shengwang < 49)
		{
			result = (num - -9f) / 58f / 3f;
		}
		else if (shengwang >= 50 && shengwang < 499)
		{
			result = 0.33f + (num - 50f) / 449f / 3f;
		}
		else if (shengwang >= 500 && shengwang < 999)
		{
			result = 0.66f + (num - 500f) / 499f / 3f;
		}
		else if (shengwang >= 1000 && shengwang < 99999)
		{
			return 1f;
		}
		return result;
	}

	public static int GetNingZhouShengWang()
	{
		return GetShengWang(0);
	}

	public static int GetSeaShengWang()
	{
		return GetShengWang(19);
	}

	public static int GetShengWang(int id)
	{
		InitShengWang();
		if (Player == null)
		{
			return 0;
		}
		if (!Player.MenPaiHaoGanDu.HasField(id.ToString()))
		{
			return 0;
		}
		return Player.MenPaiHaoGanDu[id.ToString()].I;
	}

	public static int GetMenPaiShengWang()
	{
		InitShengWang();
		if (Player.menPai == 0)
		{
			return 0;
		}
		if (!Player.MenPaiHaoGanDu.HasField(Player.menPai.ToString()))
		{
			return 0;
		}
		return Player.MenPaiHaoGanDu[Player.menPai.ToString()].I;
	}

	public static void AddNingZhouShengWang(int add)
	{
		AddShengWang(0, add);
	}

	public static void AddSeaShengWang(int add)
	{
		AddShengWang(19, add);
	}

	public static void AddShengWang(int id, int add, bool show = false)
	{
		int val = (Player.MenPaiHaoGanDu.HasField(id.ToString()) ? Player.MenPaiHaoGanDu[id.ToString()].I : 0) + add;
		Player.MenPaiHaoGanDu.SetField(id.ToString(), val);
		if (show)
		{
			if (add > 0)
			{
				UIPopTip.Inst.Pop($"声望增加了{add}", PopTipIconType.上箭头);
			}
			else if (add < 0)
			{
				UIPopTip.Inst.Pop($"声望减少了{Mathf.Abs(add)}", PopTipIconType.下箭头);
			}
		}
	}

	public static void AddMenPaiShengWang(int add)
	{
		int val = (Player.MenPaiHaoGanDu.HasField(Player.menPai.ToString()) ? Player.MenPaiHaoGanDu[Player.menPai.ToString()].I : 0) + add;
		Player.MenPaiHaoGanDu.SetField(Player.menPai.ToString(), val);
	}

	public static int GetNingZhouShengWangLevel()
	{
		return CalcShengWangLevel(GetNingZhouShengWang());
	}

	public static int GetSeaShengWangLevel()
	{
		return CalcShengWangLevel(GetSeaShengWang());
	}

	public static int GetShengWangLevel(int id)
	{
		return CalcShengWangLevel(GetShengWang(id));
	}

	public static float GetNingZhouShengWangProcess()
	{
		return CalcShengWangProcess(GetNingZhouShengWang());
	}

	public static float GetSeaShengWangProcess()
	{
		return CalcShengWangProcess(GetSeaShengWang());
	}

	public static bool IsNingZhouMaxShengWangLevel()
	{
		return GetNingZhouShengWangLevel() == 7;
	}

	public static bool IsSeaMaxShengWangLevel()
	{
		return GetSeaShengWangLevel() == 7;
	}

	public static int CalcXuanShang(int shengwang, int pingfen, out int shangjin, out string desc)
	{
		int num = 0;
		shangjin = 0;
		foreach (JSONObject item in jsonData.instance.ShengWangShangJinData.list)
		{
			if (shengwang <= item["ShengWang"].I)
			{
				shangjin = item["ShiJiShangJin"].I;
				continue;
			}
			break;
		}
		JSONObject shangJinPingFenData = jsonData.instance.ShangJinPingFenData;
		foreach (JSONObject item2 in shangJinPingFenData.list)
		{
			if (shangjin >= pingfen + item2["PingFen"].I)
			{
				num = item2["ShaShouLv"].I;
			}
		}
		desc = "";
		if (shangjin == 0)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["3"]["Info"].Str;
		}
		else if (pingfen >= jsonData.instance.ShangJinPingFenData["15"]["PingFen"].I)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["4"]["Info"].Str;
			num = 0;
		}
		else if (shangjin >= pingfen + shangJinPingFenData[Player.level.ToString()]["PingFen"].I)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["1"]["Info"].Str;
			desc = desc.Replace("{jingjie}", num.ToLevelName());
		}
		else if (shangjin < pingfen + shangJinPingFenData[Player.level.ToString()]["PingFen"].I)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["2"]["Info"].Str;
			num = 0;
		}
		return num;
	}

	public static int GetXuanShangLevel(int id)
	{
		int shengWang = GetShengWang(id);
		int shangJinPingFen = GetShangJinPingFen(id);
		int shangjin;
		string desc;
		return CalcXuanShang(shengWang, shangJinPingFen, out shangjin, out desc);
	}

	public static int GetShangJin(int id)
	{
		int shengWang = GetShengWang(id);
		int shangJinPingFen = GetShangJinPingFen(id);
		CalcXuanShang(shengWang, shangJinPingFen, out var shangjin, out var _);
		return shangjin;
	}

	public static void AddShangJinPingFen(int id, int add)
	{
		int val = (Player.ShangJinPingFen.HasField(id.ToString()) ? Player.ShangJinPingFen[id.ToString()].I : 0) + add;
		Player.ShangJinPingFen.SetField(id.ToString(), val);
	}

	public static int GetShangJinPingFen(int id)
	{
		if (!Player.ShangJinPingFen.HasField(id.ToString()))
		{
			return 0;
		}
		return Player.ShangJinPingFen[id.ToString()].I;
	}

	public static void SetShiLiChengHaoLevel(int id, int level)
	{
		Player.ShiLiChengHaoLevel.SetField(id.ToString(), level);
	}

	public static int GetShiLiChengHaoLevel(int id)
	{
		if (!Player.ShiLiChengHaoLevel.HasField(id.ToString()))
		{
			return 0;
		}
		return Player.ShiLiChengHaoLevel[id.ToString()].I;
	}

	public static string GetMenPaiChengHao()
	{
		int chenghaoLevel = 0;
		if (Player.menPai == 0)
		{
			chenghaoLevel = 0;
		}
		else
		{
			chenghaoLevel = GetShiLiChengHaoLevel(1);
		}
		string result = "无";
		JSONObject jSONObject = jsonData.instance.ShiLiShenFenData.list.Find((JSONObject d) => d["ShiLi"].I == 1 && d["ShenFen"].I == chenghaoLevel);
		if (jSONObject != null)
		{
			result = jSONObject["Name"].Str;
		}
		return result;
	}

	public static void StudyShuangXiuSkill(int skillID)
	{
		if (!Player.ShuangXiuData.HasField("HasSkillList"))
		{
			Player.ShuangXiuData.SetField("HasSkillList", new JSONObject(JSONObject.Type.ARRAY));
		}
		if (!HasShuangXiuSkill(skillID))
		{
			Player.ShuangXiuData["HasSkillList"].Add(skillID);
		}
	}

	public static bool HasShuangXiuSkill(int skillID)
	{
		if (!Player.ShuangXiuData.HasField("HasSkillList"))
		{
			Player.ShuangXiuData.SetField("HasSkillList", new JSONObject(JSONObject.Type.ARRAY));
		}
		return Player.ShuangXiuData["HasSkillList"].ToList().Contains(skillID);
	}

	public static void DoShuangXiu(int skillID, UINPCData npc)
	{
		if (skillID == 1)
		{
			if (Player.ShuangXiuData.HasField("JingYuan"))
			{
				Player.ShuangXiuData.RemoveField("JingYuan");
			}
			return;
		}
		ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[skillID];
		int jiazhi = ShuangXiuJingYuanJiaZhi.DataDict[shuangXiuMiShu.ningliantype].jiazhi;
		int val = 0;
		if (shuangXiuMiShu.pinjietype == 1)
		{
			val = npc.BigLevel;
		}
		else if (shuangXiuMiShu.pinjietype == 2)
		{
			int ziZhi = npc.ZiZhi;
			val = ((ziZhi <= 20) ? 1 : ((ziZhi >= 21 && ziZhi <= 40) ? 2 : ((ziZhi >= 41 && ziZhi <= 60) ? 3 : ((ziZhi >= 51 && ziZhi <= 80) ? 4 : ((ziZhi < 81 || ziZhi > 100) ? 6 : 5)))));
		}
		else if (shuangXiuMiShu.pinjietype == 3)
		{
			val = npc.WuDao.Find((UINPCWuDaoData w) => w.ID == 6).Level + 1;
		}
		else if (shuangXiuMiShu.pinjietype == 4)
		{
			val = npc.WuDao.Find((UINPCWuDaoData w) => w.ID == 7).Level + 1;
		}
		int num = jiazhi * shuangXiuMiShu.jingyuanbeilv;
		if (shuangXiuMiShu.jingyuantype == 1)
		{
			num *= ShuangXiuJingJieBeiLv.DataDict[npc.Level].BeiLv;
		}
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("Skill", skillID);
		jSONObject.SetField("PinJie", val);
		jSONObject.SetField("Count", num);
		jSONObject.SetField("Reward", num / jiazhi);
		Player.ShuangXiuData.SetField("JingYuan", jSONObject);
	}

	public static int GetSeaTanSuoDu(int seaID)
	{
		if (Player.SeaTanSuoDu.HasField(seaID.ToString()))
		{
			return Player.SeaTanSuoDu[seaID.ToString()].I;
		}
		return 0;
	}

	public static void AddSeaTanSuoDu(int seaID, int value)
	{
		if (Player.SeaTanSuoDu.HasField(seaID.ToString()))
		{
			int i = Player.SeaTanSuoDu[seaID.ToString()].I;
			Player.SeaTanSuoDu.SetField(seaID.ToString(), i + value);
			SeaHaiYuTanSuo seaHaiYuTanSuo = SeaHaiYuTanSuo.DataDict[seaID];
			int value2 = seaHaiYuTanSuo.Value;
			if (i + value >= 100)
			{
				GlobalValue.Set(value2, 1, $"PlayerEx.AddSeaTanSuoDu({seaID}, {value}) 海域探索度超过100，解锁了海域隐藏点");
				EndlessSeaMag.AddSeeIsland(seaHaiYuTanSuo.ZuoBiao);
			}
			else
			{
				GlobalValue.Set(value2, 0, $"PlayerEx.AddSeaTanSuoDu({seaID}, {value}) 海域探索度未超过100，无法解锁隐藏点");
			}
		}
		else
		{
			Player.SeaTanSuoDu.SetField(seaID.ToString(), value);
		}
		string eventName = SceneNameJsonData.DataDict[$"Sea{seaID}"].EventName;
		UIPopTip.Inst.Pop($"对{eventName}的探索度提升了{value}", PopTipIconType.上箭头);
		if ((Object)(object)UISeaTanSuoPanel.Inst != (Object)null)
		{
			UISeaTanSuoPanel.Inst.RefreshUI();
		}
	}

	public static void PostLoadGame()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		try
		{
			JArray val = (JArray)Player.EndlessSeaAvatarSeeIsland["Island"];
			List<int> list = new List<int>();
			foreach (JToken item in val)
			{
				if (!list.Contains((int)item))
				{
					list.Add((int)item);
				}
			}
			Player.EndlessSeaAvatarSeeIsland["Island"] = (JToken)new JArray();
			foreach (int item2 in list)
			{
				EndlessSeaMag.AddSeeIsland(item2);
			}
			List<string> keys = Player.FuBen.keys;
			for (int i = 0; i < keys.Count; i++)
			{
				List<int> list2 = Player.FuBen[keys[i]]["ExploredNode"].ToList();
				List<int> list3 = new List<int>();
				foreach (int item3 in list2)
				{
					if (!list3.Contains(item3))
					{
						list3.Add(item3);
					}
				}
				Player.FuBen[keys[i]]["ExploredNode"].Clear();
				foreach (int item4 in list3)
				{
					Player.FuBen[keys[i]]["ExploredNode"].Add(item4);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("尝试修复老档失败，错误:" + ex.Message + "\n" + ex.StackTrace));
		}
	}

	public static bool IsLingWuBook(int itemid)
	{
		if (itemid > jsonData.QingJiaoItemIDSegment)
		{
			itemid -= jsonData.QingJiaoItemIDSegment;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemid];
		using (List<int>.Enumerator enumerator = itemJsonData.seid.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
				case 1:
					if (ItemsSeidJsonData1.DataDict.ContainsKey(itemid))
					{
						int id = ItemsSeidJsonData1.DataDict[itemid].value1;
						if (Player.hasSkillList.Find((SkillItem s) => s.itemId == id) != null)
						{
							return true;
						}
					}
					break;
				case 2:
					if (ItemsSeidJsonData2.DataDict.ContainsKey(itemid))
					{
						int id2 = ItemsSeidJsonData2.DataDict[itemid].value1;
						if (Player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id2) != null)
						{
							return true;
						}
					}
					break;
				case 13:
					if (ItemsSeidJsonData13.DataDict.ContainsKey(itemid))
					{
						int value = ItemsSeidJsonData13.DataDict[itemid].value1;
						if (Tools.instance.getPlayer().ISStudyDanFan(value))
						{
							return true;
						}
					}
					break;
				}
			}
		}
		if (itemJsonData.type == 13)
		{
			int itemCanUseNum = item.GetItemCanUseNum(itemid);
			if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, itemid.ToString()) >= itemCanUseNum)
			{
				return true;
			}
		}
		return false;
	}

	public static void CheckChuHai()
	{
		if (SceneEx.NowSceneName.StartsWith("Sea") && Player != null && !Player.OnceShow.HasItem(1))
		{
			Player.OnceShow.Add(1);
			UITutorialSeaMove.Inst.Show();
		}
	}

	public static void CheckLianDan()
	{
		if (Player != null && !Player.OnceShow.HasItem(2))
		{
			Player.OnceShow.Add(2);
			TuJianManager.Inst.OnHyperlink("2_506_3");
		}
	}

	public static void CheckLianQi()
	{
		if (Player != null && !Player.OnceShow.HasItem(3))
		{
			Player.OnceShow.Add(3);
			TuJianManager.Inst.OnHyperlink("2_506_4");
		}
	}

	public static void SetDaoLvChengHu(int npcid, string chenghu)
	{
		if (Player != null)
		{
			Player.DaoLvChengHu.SetField(npcid.ToString(), chenghu);
		}
	}

	public static void AddHuaShenStartXianXing(int xianxing)
	{
		Player.HuaShenStartXianXing = new JSONObject(Player.HuaShenStartXianXing.I + xianxing);
	}

	public static bool HasSkill(int skillID)
	{
		if (Player.hasSkillList.Find((SkillItem aa) => (aa.itemId == skillID) ? true : false) == null)
		{
			return false;
		}
		return true;
	}

	public static bool HasStaticSkill(int skillID)
	{
		if (Player.hasStaticSkillList.Find((SkillItem aa) => (aa.itemId == skillID) ? true : false) == null)
		{
			return false;
		}
		return true;
	}

	public static void RecordShengPing(string shengPingID, Dictionary<string, string> args = null)
	{
		if (ShengPing.DataDict.ContainsKey(shengPingID))
		{
			ShengPingData shengPingData = new ShengPingData();
			shengPingData.time = Player.worldTimeMag.getNowTime();
			shengPingData.args = args;
			ShengPing shengPing = ShengPing.DataDict[shengPingID];
			JSONObject obj = shengPingData.ToJson();
			if (shengPing.IsChongfu == 1)
			{
				if (!Player.ShengPingRecord.HasField(shengPingID))
				{
					Player.ShengPingRecord.SetField(shengPingID, new JSONObject(JSONObject.Type.ARRAY));
				}
				Player.ShengPingRecord[shengPingID].Add(obj);
			}
			else if (!Player.ShengPingRecord.HasField(shengPingID))
			{
				Player.ShengPingRecord.SetField(shengPingID, obj);
			}
		}
		else
		{
			Debug.LogError((object)("记录生平出错，找不到ID为 " + shengPingID + " 的配表数据"));
		}
	}

	public static List<ShengPingData> GetShengPingList()
	{
		List<ShengPingData> list = new List<ShengPingData>();
		foreach (string key in Player.ShengPingRecord.keys)
		{
			if (ShengPing.DataDict.ContainsKey(key))
			{
				JSONObject jSONObject = Player.ShengPingRecord[key];
				ShengPing shengPing = ShengPing.DataDict[key];
				if (shengPing.IsChongfu == 1)
				{
					foreach (JSONObject item in jSONObject.list)
					{
						ShengPingData shengPingData = new ShengPingData(item);
						shengPingData.ID = key;
						shengPingData.priority = shengPing.priority;
						list.Add(shengPingData);
					}
				}
				else
				{
					ShengPingData shengPingData2 = new ShengPingData(jSONObject);
					shengPingData2.ID = key;
					shengPingData2.priority = shengPing.priority;
					list.Add(shengPingData2);
				}
			}
			else
			{
				Debug.LogError((object)("获取生平出错，找不到ID为 " + key + " 的配表数据"));
			}
		}
		list.Sort();
		return list;
	}

	public static bool HasTianFu(int tianFuID)
	{
		foreach (int item in Player.SelectTianFuID.ToList())
		{
			if (item == tianFuID)
			{
				return true;
			}
		}
		return false;
	}

	public static 游戏难度 GetGameDifficulty()
	{
		foreach (int item in Player.SelectTianFuID.ToList())
		{
			if (item <= 5)
			{
				return (游戏难度)item;
			}
		}
		return 游戏难度.未知;
	}

	public static void AddErrorItemID(int id)
	{
		if (!ErrorItemIDList.Contains(id))
		{
			ErrorItemIDList.Add(id);
		}
	}

	public static void DeleteErrorItem()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		if (Player == null || ErrorItemIDList.Count == 0)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("即将从背包删除以下异常的物品：");
		foreach (int errorItemID in ErrorItemIDList)
		{
			stringBuilder.AppendLine(errorItemID.ToString());
		}
		string text = stringBuilder.ToString();
		object obj = _003C_003Ec._003C_003E9__59_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				for (int num = Player.itemList.values.Count - 1; num >= 0; num--)
				{
					if (ErrorItemIDList.Contains(Player.itemList.values[num].itemId))
					{
						Player.itemList.values.RemoveAt(num);
					}
				}
				ErrorItemIDList.Clear();
				if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
				{
					SingletonMono<TabUIMag>.Instance.TryEscClose();
				}
			};
			_003C_003Ec._003C_003E9__59_0 = val;
			obj = (object)val;
		}
		UBigCheckBox.Show(text, (UnityAction)obj);
	}
}
