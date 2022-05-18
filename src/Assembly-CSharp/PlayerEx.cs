using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x020002CF RID: 719
public static class PlayerEx
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x060015AE RID: 5550 RVA: 0x00013851 File Offset: 0x00011A51
	public static Avatar Player
	{
		get
		{
			if (Tools.instance != null)
			{
				return Tools.instance.getPlayer();
			}
			return null;
		}
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x0001386C File Offset: 0x00011A6C
	public static bool IsTheather(int npcid)
	{
		return PlayerEx.Player.TeatherId.HasItem(npcid);
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x0001387E File Offset: 0x00011A7E
	public static bool IsDaoLv(int npcid)
	{
		return PlayerEx.Player.DaoLvId.HasItem(npcid);
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000C4590 File Offset: 0x000C2790
	public static string GetDaoLvNickName(int npcid)
	{
		string result = PlayerEx.Player.lastName;
		if (PlayerEx.Player.DaoLvChengHu.HasField(npcid.ToString()))
		{
			result = PlayerEx.Player.DaoLvChengHu[npcid.ToString()].Str;
		}
		return result;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00013890 File Offset: 0x00011A90
	public static bool IsBrother(int npcid)
	{
		return PlayerEx.Player.Brother.HasItem(npcid);
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000138A2 File Offset: 0x00011AA2
	public static bool IsTuDi(int npcid)
	{
		return PlayerEx.Player.TuDiId.HasItem(npcid);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000138B4 File Offset: 0x00011AB4
	public static bool IsDaTing(int npcid)
	{
		return PlayerEx.Player.DaTingId.HasItem(npcid);
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000138C6 File Offset: 0x00011AC6
	public static void AddDaTingNPC(int npcid)
	{
		if (!PlayerEx.IsDaTing(npcid))
		{
			PlayerEx.Player.DaTingId.Add(npcid);
		}
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000C45E0 File Offset: 0x000C27E0
	public static void AddRelationship(int npcid, bool teather, bool daolv, bool brother, bool tudi)
	{
		if (teather && !PlayerEx.IsTheather(npcid))
		{
			PlayerEx.Player.TeatherId.Add(npcid);
		}
		if (daolv && !PlayerEx.IsDaoLv(npcid))
		{
			PlayerEx.Player.DaoLvId.Add(npcid);
		}
		if (brother && !PlayerEx.IsBrother(npcid))
		{
			PlayerEx.Player.Brother.Add(npcid);
		}
		if (tudi && !PlayerEx.IsTuDi(npcid))
		{
			PlayerEx.Player.TuDiId.Add(npcid);
		}
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000C465C File Offset: 0x000C285C
	public static void SubRelationship(int npcid, bool teather, bool daolv, bool brother, bool tudi)
	{
		if (teather && PlayerEx.IsTheather(npcid))
		{
			List<int> list = PlayerEx.Player.TeatherId.ToList();
			list.Remove(npcid);
			PlayerEx.Player.TeatherId = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int val in list)
			{
				PlayerEx.Player.TeatherId.Add(val);
			}
		}
		if (daolv && PlayerEx.IsDaoLv(npcid))
		{
			List<int> list2 = PlayerEx.Player.DaoLvId.ToList();
			list2.Remove(npcid);
			PlayerEx.Player.DaoLvId = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int val2 in list2)
			{
				PlayerEx.Player.DaoLvId.Add(val2);
			}
		}
		if (brother && PlayerEx.IsBrother(npcid))
		{
			List<int> list3 = PlayerEx.Player.Brother.ToList();
			list3.Remove(npcid);
			PlayerEx.Player.Brother = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int val3 in list3)
			{
				PlayerEx.Player.Brother.Add(val3);
			}
		}
		if (tudi && PlayerEx.IsTuDi(npcid))
		{
			List<int> list4 = PlayerEx.Player.TuDiId.ToList();
			list4.Remove(npcid);
			PlayerEx.Player.TuDiId = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int val4 in list4)
			{
				PlayerEx.Player.TuDiId.Add(val4);
			}
		}
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000C484C File Offset: 0x000C2A4C
	private static void InitShengWang()
	{
		if (!PlayerEx.isShengWangInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShengWangLevelData.list)
			{
				PlayerEx._ShengWangLevelDict.Add(jsonobject["id"].I, jsonobject["ShengWangQuJian"].list[0].I);
			}
			PlayerEx.isShengWangInited = true;
		}
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000C48E4 File Offset: 0x000C2AE4
	public static int CalcShengWangLevel(int shengwang)
	{
		int result = 1;
		int num = 1;
		while (num <= 7 && shengwang >= PlayerEx._ShengWangLevelDict[num])
		{
			result = num;
			num++;
		}
		return result;
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000C4910 File Offset: 0x000C2B10
	private static float CalcShengWangProcess(int shengwang)
	{
		float result = 0f;
		float num = (float)shengwang;
		if (shengwang >= -99999 && shengwang < -500)
		{
			return 1f;
		}
		if (shengwang >= -499 && shengwang < -50)
		{
			result = 0.66f + (-num - 50f) / 449f / 3f;
		}
		else if (shengwang >= -49 && shengwang < -10)
		{
			result = 0.33f + (-num - 10f) / 39f / 3f;
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

	// Token: 0x060015BB RID: 5563 RVA: 0x000138E0 File Offset: 0x00011AE0
	public static int GetNingZhouShengWang()
	{
		return PlayerEx.GetShengWang(0);
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000138E8 File Offset: 0x00011AE8
	public static int GetSeaShengWang()
	{
		return PlayerEx.GetShengWang(19);
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000C4A20 File Offset: 0x000C2C20
	public static int GetShengWang(int id)
	{
		PlayerEx.InitShengWang();
		if (PlayerEx.Player == null)
		{
			return 0;
		}
		if (!PlayerEx.Player.MenPaiHaoGanDu.HasField(id.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.MenPaiHaoGanDu[id.ToString()].I;
	}

	// Token: 0x060015BE RID: 5566 RVA: 0x000C4A70 File Offset: 0x000C2C70
	public static int GetMenPaiShengWang()
	{
		PlayerEx.InitShengWang();
		if (PlayerEx.Player.menPai == 0)
		{
			return 0;
		}
		if (!PlayerEx.Player.MenPaiHaoGanDu.HasField(PlayerEx.Player.menPai.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.MenPaiHaoGanDu[PlayerEx.Player.menPai.ToString()].I;
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x000138F1 File Offset: 0x00011AF1
	public static void AddNingZhouShengWang(int add)
	{
		PlayerEx.AddShengWang(0, add, false);
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x000138FB File Offset: 0x00011AFB
	public static void AddSeaShengWang(int add)
	{
		PlayerEx.AddShengWang(19, add, false);
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000C4AD8 File Offset: 0x000C2CD8
	public static void AddShengWang(int id, int add, bool show = false)
	{
		int val = (PlayerEx.Player.MenPaiHaoGanDu.HasField(id.ToString()) ? PlayerEx.Player.MenPaiHaoGanDu[id.ToString()].I : 0) + add;
		PlayerEx.Player.MenPaiHaoGanDu.SetField(id.ToString(), val);
		if (show)
		{
			if (add > 0)
			{
				UIPopTip.Inst.Pop(string.Format("声望增加了{0}", add), PopTipIconType.上箭头);
				return;
			}
			if (add < 0)
			{
				UIPopTip.Inst.Pop(string.Format("声望减少了{0}", Mathf.Abs(add)), PopTipIconType.下箭头);
			}
		}
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000C4B7C File Offset: 0x000C2D7C
	public static void AddMenPaiShengWang(int add)
	{
		int val = (PlayerEx.Player.MenPaiHaoGanDu.HasField(PlayerEx.Player.menPai.ToString()) ? PlayerEx.Player.MenPaiHaoGanDu[PlayerEx.Player.menPai.ToString()].I : 0) + add;
		PlayerEx.Player.MenPaiHaoGanDu.SetField(PlayerEx.Player.menPai.ToString(), val);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x00013906 File Offset: 0x00011B06
	public static int GetNingZhouShengWangLevel()
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetNingZhouShengWang());
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x00013912 File Offset: 0x00011B12
	public static int GetSeaShengWangLevel()
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetSeaShengWang());
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x0001391E File Offset: 0x00011B1E
	public static int GetShengWangLevel(int id)
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetShengWang(id));
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x0001392B File Offset: 0x00011B2B
	public static float GetNingZhouShengWangProcess()
	{
		return PlayerEx.CalcShengWangProcess(PlayerEx.GetNingZhouShengWang());
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x00013937 File Offset: 0x00011B37
	public static float GetSeaShengWangProcess()
	{
		return PlayerEx.CalcShengWangProcess(PlayerEx.GetSeaShengWang());
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x00013943 File Offset: 0x00011B43
	public static bool IsNingZhouMaxShengWangLevel()
	{
		return PlayerEx.GetNingZhouShengWangLevel() == 7;
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x0001394D File Offset: 0x00011B4D
	public static bool IsSeaMaxShengWangLevel()
	{
		return PlayerEx.GetSeaShengWangLevel() == 7;
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x000C4BF4 File Offset: 0x000C2DF4
	public static int CalcXuanShang(int shengwang, int pingfen, out int shangjin, out string desc)
	{
		int num = 0;
		shangjin = 0;
		foreach (JSONObject jsonobject in jsonData.instance.ShengWangShangJinData.list)
		{
			if (shengwang > jsonobject["ShengWang"].I)
			{
				break;
			}
			shangjin = jsonobject["ShiJiShangJin"].I;
		}
		JSONObject shangJinPingFenData = jsonData.instance.ShangJinPingFenData;
		foreach (JSONObject jsonobject2 in shangJinPingFenData.list)
		{
			if (shangjin >= pingfen + jsonobject2["PingFen"].I)
			{
				num = jsonobject2["ShaShouLv"].I;
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
		else if (shangjin >= pingfen + shangJinPingFenData[PlayerEx.Player.level.ToString()]["PingFen"].I)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["1"]["Info"].Str;
			desc = desc.Replace("{jingjie}", num.ToLevelName());
		}
		else if (shangjin < pingfen + shangJinPingFenData[PlayerEx.Player.level.ToString()]["PingFen"].I)
		{
			desc = jsonData.instance.XuanShangMiaoShuData["2"]["Info"].Str;
			num = 0;
		}
		return num;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000C4E24 File Offset: 0x000C3024
	public static int GetXuanShangLevel(int id)
	{
		int shengWang = PlayerEx.GetShengWang(id);
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(id);
		int num;
		string text;
		return PlayerEx.CalcXuanShang(shengWang, shangJinPingFen, out num, out text);
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000C4E48 File Offset: 0x000C3048
	public static int GetShangJin(int id)
	{
		int shengWang = PlayerEx.GetShengWang(id);
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(id);
		int result;
		string text;
		PlayerEx.CalcXuanShang(shengWang, shangJinPingFen, out result, out text);
		return result;
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000C4E70 File Offset: 0x000C3070
	public static void AddShangJinPingFen(int id, int add)
	{
		int val = (PlayerEx.Player.ShangJinPingFen.HasField(id.ToString()) ? PlayerEx.Player.ShangJinPingFen[id.ToString()].I : 0) + add;
		PlayerEx.Player.ShangJinPingFen.SetField(id.ToString(), val);
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x00013957 File Offset: 0x00011B57
	public static int GetShangJinPingFen(int id)
	{
		if (!PlayerEx.Player.ShangJinPingFen.HasField(id.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.ShangJinPingFen[id.ToString()].I;
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x0001398E File Offset: 0x00011B8E
	public static void SetShiLiChengHaoLevel(int id, int level)
	{
		PlayerEx.Player.ShiLiChengHaoLevel.SetField(id.ToString(), level);
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000139A7 File Offset: 0x00011BA7
	public static int GetShiLiChengHaoLevel(int id)
	{
		if (!PlayerEx.Player.ShiLiChengHaoLevel.HasField(id.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.ShiLiChengHaoLevel[id.ToString()].I;
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x000C4ED0 File Offset: 0x000C30D0
	public static string GetMenPaiChengHao()
	{
		int chenghaoLevel = 0;
		if (PlayerEx.Player.menPai == 0)
		{
			chenghaoLevel = 0;
		}
		else
		{
			chenghaoLevel = PlayerEx.GetShiLiChengHaoLevel(1);
		}
		string result = "无";
		JSONObject jsonobject = jsonData.instance.ShiLiShenFenData.list.Find((JSONObject d) => d["ShiLi"].I == 1 && d["ShenFen"].I == chenghaoLevel);
		if (jsonobject != null)
		{
			result = jsonobject["Name"].Str;
		}
		return result;
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x000C4F48 File Offset: 0x000C3148
	public static void StudyShuangXiuSkill(int skillID)
	{
		if (!PlayerEx.Player.ShuangXiuData.HasField("HasSkillList"))
		{
			PlayerEx.Player.ShuangXiuData.SetField("HasSkillList", new JSONObject(JSONObject.Type.ARRAY));
		}
		if (!PlayerEx.HasShuangXiuSkill(skillID))
		{
			PlayerEx.Player.ShuangXiuData["HasSkillList"].Add(skillID);
		}
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x000C4FA8 File Offset: 0x000C31A8
	public static bool HasShuangXiuSkill(int skillID)
	{
		if (!PlayerEx.Player.ShuangXiuData.HasField("HasSkillList"))
		{
			PlayerEx.Player.ShuangXiuData.SetField("HasSkillList", new JSONObject(JSONObject.Type.ARRAY));
		}
		return PlayerEx.Player.ShuangXiuData["HasSkillList"].ToList().Contains(skillID);
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x000C5004 File Offset: 0x000C3204
	public static void DoShuangXiu(int skillID, UINPCData npc)
	{
		if (skillID == 1)
		{
			if (PlayerEx.Player.ShuangXiuData.HasField("JingYuan"))
			{
				PlayerEx.Player.ShuangXiuData.RemoveField("JingYuan");
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
			if (ziZhi <= 20)
			{
				val = 1;
			}
			else if (ziZhi >= 21 && ziZhi <= 40)
			{
				val = 2;
			}
			else if (ziZhi >= 41 && ziZhi <= 60)
			{
				val = 3;
			}
			else if (ziZhi >= 51 && ziZhi <= 80)
			{
				val = 4;
			}
			else if (ziZhi >= 81 && ziZhi <= 100)
			{
				val = 5;
			}
			else
			{
				val = 6;
			}
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
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("Skill", skillID);
		jsonobject.SetField("PinJie", val);
		jsonobject.SetField("Count", num);
		jsonobject.SetField("Reward", num / jiazhi);
		PlayerEx.Player.ShuangXiuData.SetField("JingYuan", jsonobject);
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000139DE File Offset: 0x00011BDE
	public static int GetSeaTanSuoDu(int seaID)
	{
		if (PlayerEx.Player.SeaTanSuoDu.HasField(seaID.ToString()))
		{
			return PlayerEx.Player.SeaTanSuoDu[seaID.ToString()].I;
		}
		return 0;
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x000C51DC File Offset: 0x000C33DC
	public static void AddSeaTanSuoDu(int seaID, int value)
	{
		if (PlayerEx.Player.SeaTanSuoDu.HasField(seaID.ToString()))
		{
			int i = PlayerEx.Player.SeaTanSuoDu[seaID.ToString()].I;
			PlayerEx.Player.SeaTanSuoDu.SetField(seaID.ToString(), i + value);
			SeaHaiYuTanSuo seaHaiYuTanSuo = SeaHaiYuTanSuo.DataDict[seaID];
			int value2 = seaHaiYuTanSuo.Value;
			if (i + value >= 100)
			{
				GlobalValue.Set(value2, 1, string.Format("PlayerEx.AddSeaTanSuoDu({0}, {1}) 海域探索度超过100，解锁了海域隐藏点", seaID, value));
				EndlessSeaMag.AddSeeIsland(seaHaiYuTanSuo.ZuoBiao);
			}
			else
			{
				GlobalValue.Set(value2, 0, string.Format("PlayerEx.AddSeaTanSuoDu({0}, {1}) 海域探索度未超过100，无法解锁隐藏点", seaID, value));
			}
		}
		else
		{
			PlayerEx.Player.SeaTanSuoDu.SetField(seaID.ToString(), value);
		}
		string eventName = SceneNameJsonData.DataDict[string.Format("Sea{0}", seaID)].EventName;
		UIPopTip.Inst.Pop(string.Format("对{0}的探索度提升了{1}", eventName, value), PopTipIconType.上箭头);
		if (UISeaTanSuoPanel.Inst != null)
		{
			UISeaTanSuoPanel.Inst.RefreshUI();
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x000C5308 File Offset: 0x000C3508
	public static void PostLoadGame()
	{
		try
		{
			JArray jarray = (JArray)PlayerEx.Player.EndlessSeaAvatarSeeIsland["Island"];
			List<int> list = new List<int>();
			foreach (JToken jtoken in jarray)
			{
				if (!list.Contains((int)jtoken))
				{
					list.Add((int)jtoken);
				}
			}
			PlayerEx.Player.EndlessSeaAvatarSeeIsland["Island"] = new JArray();
			foreach (int sea in list)
			{
				EndlessSeaMag.AddSeeIsland(sea);
			}
			List<string> keys = PlayerEx.Player.FuBen.keys;
			for (int i = 0; i < keys.Count; i++)
			{
				List<int> list2 = PlayerEx.Player.FuBen[keys[i]]["ExploredNode"].ToList();
				List<int> list3 = new List<int>();
				foreach (int item in list2)
				{
					if (!list3.Contains(item))
					{
						list3.Add(item);
					}
				}
				PlayerEx.Player.FuBen[keys[i]]["ExploredNode"].Clear();
				foreach (int val in list3)
				{
					PlayerEx.Player.FuBen[keys[i]]["ExploredNode"].Add(val);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("尝试修复老档失败，错误:" + ex.Message + "\n" + ex.StackTrace);
		}
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000C5570 File Offset: 0x000C3770
	public static bool IsLingWuBook(int itemid)
	{
		if (itemid > 100000)
		{
			itemid -= 100000;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemid];
		foreach (int num in itemJsonData.seid)
		{
			if (num == 1)
			{
				if (ItemsSeidJsonData1.DataDict.ContainsKey(itemid))
				{
					int id = ItemsSeidJsonData1.DataDict[itemid].value1;
					if (PlayerEx.Player.hasSkillList.Find((SkillItem s) => s.itemId == id) != null)
					{
						return true;
					}
				}
			}
			else if (num == 2)
			{
				if (ItemsSeidJsonData2.DataDict.ContainsKey(itemid))
				{
					int id = ItemsSeidJsonData2.DataDict[itemid].value1;
					if (PlayerEx.Player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id) != null)
					{
						return true;
					}
				}
			}
			else if (num == 13 && ItemsSeidJsonData13.DataDict.ContainsKey(itemid))
			{
				int value = ItemsSeidJsonData13.DataDict[itemid].value1;
				if (Tools.instance.getPlayer().ISStudyDanFan(value))
				{
					return true;
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

	// Token: 0x060015D9 RID: 5593 RVA: 0x000C5710 File Offset: 0x000C3910
	public static void CheckChuHai()
	{
		if (SceneEx.NowSceneName.StartsWith("Sea") && PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(1))
		{
			PlayerEx.Player.OnceShow.Add(1);
			UITutorialSeaMove.Inst.Show();
		}
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x00013A15 File Offset: 0x00011C15
	public static void CheckLianDan()
	{
		if (PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(2))
		{
			PlayerEx.Player.OnceShow.Add(2);
			TuJianManager.Inst.OnHyperlink("2_506_3");
		}
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x00013A4F File Offset: 0x00011C4F
	public static void CheckLianQi()
	{
		if (PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(3))
		{
			PlayerEx.Player.OnceShow.Add(3);
			TuJianManager.Inst.OnHyperlink("2_506_4");
		}
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00013A89 File Offset: 0x00011C89
	public static void SetDaoLvChengHu(int npcid, string chenghu)
	{
		if (PlayerEx.Player != null)
		{
			PlayerEx.Player.DaoLvChengHu.SetField(npcid.ToString(), chenghu);
		}
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00013AA9 File Offset: 0x00011CA9
	public static void AddHuaShenStartXianXing(int xianxing)
	{
		PlayerEx.Player.HuaShenStartXianXing = new JSONObject(PlayerEx.Player.HuaShenStartXianXing.I + xianxing);
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x000C5764 File Offset: 0x000C3964
	public static bool HasSkill(int skillID)
	{
		return PlayerEx.Player.hasSkillList.Find((SkillItem aa) => aa.itemId == skillID) != null;
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x000C57A0 File Offset: 0x000C39A0
	public static bool HasStaticSkill(int skillID)
	{
		return PlayerEx.Player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skillID) != null;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x000C57DC File Offset: 0x000C39DC
	public static void RecordShengPing(string shengPingID, Dictionary<string, string> args = null)
	{
		if (!ShengPing.DataDict.ContainsKey(shengPingID))
		{
			Debug.LogError("记录生平出错，找不到ID为 " + shengPingID + " 的配表数据");
			return;
		}
		ShengPingData shengPingData = new ShengPingData();
		shengPingData.time = PlayerEx.Player.worldTimeMag.getNowTime();
		shengPingData.args = args;
		ShengPing shengPing = ShengPing.DataDict[shengPingID];
		JSONObject obj = shengPingData.ToJson();
		if (shengPing.IsChongfu == 1)
		{
			if (!PlayerEx.Player.ShengPingRecord.HasField(shengPingID))
			{
				PlayerEx.Player.ShengPingRecord.SetField(shengPingID, new JSONObject(JSONObject.Type.ARRAY));
			}
			PlayerEx.Player.ShengPingRecord[shengPingID].Add(obj);
			return;
		}
		if (PlayerEx.Player.ShengPingRecord.HasField(shengPingID))
		{
			Debug.LogError("记录生平出错，ID为 " + shengPingID + " 的生平不允许重复记录");
			return;
		}
		PlayerEx.Player.ShengPingRecord.SetField(shengPingID, obj);
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x000C58C4 File Offset: 0x000C3AC4
	public static List<ShengPingData> GetShengPingList()
	{
		List<ShengPingData> list = new List<ShengPingData>();
		foreach (string text in PlayerEx.Player.ShengPingRecord.keys)
		{
			if (ShengPing.DataDict.ContainsKey(text))
			{
				JSONObject jsonobject = PlayerEx.Player.ShengPingRecord[text];
				ShengPing shengPing = ShengPing.DataDict[text];
				if (shengPing.IsChongfu == 1)
				{
					using (List<JSONObject>.Enumerator enumerator2 = jsonobject.list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							JSONObject json = enumerator2.Current;
							list.Add(new ShengPingData(json)
							{
								ID = text,
								priority = shengPing.priority
							});
						}
						continue;
					}
				}
				list.Add(new ShengPingData(jsonobject)
				{
					ID = text,
					priority = shengPing.priority
				});
			}
			else
			{
				Debug.LogError("获取生平出错，找不到ID为 " + text + " 的配表数据");
			}
		}
		list.Sort();
		return list;
	}

	// Token: 0x040011B6 RID: 4534
	private static Dictionary<int, int> _ShengWangLevelDict = new Dictionary<int, int>();

	// Token: 0x040011B7 RID: 4535
	private static bool isShengWangInited;
}
