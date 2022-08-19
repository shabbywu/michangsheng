using System;
using System.Collections.Generic;
using System.Text;
using GUIPackage;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using Tab;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x020001C8 RID: 456
public static class PlayerEx
{
	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0007720D File Offset: 0x0007540D
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

	// Token: 0x060012FA RID: 4858 RVA: 0x00077228 File Offset: 0x00075428
	public static bool IsTheather(int npcid)
	{
		return PlayerEx.Player.TeatherId.HasItem(npcid);
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0007723A File Offset: 0x0007543A
	public static bool IsDaoLv(int npcid)
	{
		return PlayerEx.Player.DaoLvId.HasItem(npcid);
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x0007724C File Offset: 0x0007544C
	public static string GetDaoLvNickName(int npcid)
	{
		string result = PlayerEx.Player.lastName;
		if (PlayerEx.Player.DaoLvChengHu.HasField(npcid.ToString()))
		{
			result = PlayerEx.Player.DaoLvChengHu[npcid.ToString()].Str;
		}
		return result;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00077299 File Offset: 0x00075499
	public static bool IsBrother(int npcid)
	{
		return PlayerEx.Player.Brother.HasItem(npcid);
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x000772AB File Offset: 0x000754AB
	public static bool IsTuDi(int npcid)
	{
		return PlayerEx.Player.TuDiId.HasItem(npcid);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x000772BD File Offset: 0x000754BD
	public static bool IsDaTing(int npcid)
	{
		return PlayerEx.Player.DaTingId.HasItem(npcid);
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000772CF File Offset: 0x000754CF
	public static void AddDaTingNPC(int npcid)
	{
		if (!PlayerEx.IsDaTing(npcid))
		{
			PlayerEx.Player.DaTingId.Add(npcid);
		}
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000772EC File Offset: 0x000754EC
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

	// Token: 0x06001302 RID: 4866 RVA: 0x00077368 File Offset: 0x00075568
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

	// Token: 0x06001303 RID: 4867 RVA: 0x00077558 File Offset: 0x00075758
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

	// Token: 0x06001304 RID: 4868 RVA: 0x000775F0 File Offset: 0x000757F0
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

	// Token: 0x06001305 RID: 4869 RVA: 0x0007761C File Offset: 0x0007581C
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

	// Token: 0x06001306 RID: 4870 RVA: 0x0007772B File Offset: 0x0007592B
	public static int GetNingZhouShengWang()
	{
		return PlayerEx.GetShengWang(0);
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x00077733 File Offset: 0x00075933
	public static int GetSeaShengWang()
	{
		return PlayerEx.GetShengWang(19);
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0007773C File Offset: 0x0007593C
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

	// Token: 0x06001309 RID: 4873 RVA: 0x0007778C File Offset: 0x0007598C
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

	// Token: 0x0600130A RID: 4874 RVA: 0x000777F1 File Offset: 0x000759F1
	public static void AddNingZhouShengWang(int add)
	{
		PlayerEx.AddShengWang(0, add, false);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000777FB File Offset: 0x000759FB
	public static void AddSeaShengWang(int add)
	{
		PlayerEx.AddShengWang(19, add, false);
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x00077808 File Offset: 0x00075A08
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

	// Token: 0x0600130D RID: 4877 RVA: 0x000778AC File Offset: 0x00075AAC
	public static void AddMenPaiShengWang(int add)
	{
		int val = (PlayerEx.Player.MenPaiHaoGanDu.HasField(PlayerEx.Player.menPai.ToString()) ? PlayerEx.Player.MenPaiHaoGanDu[PlayerEx.Player.menPai.ToString()].I : 0) + add;
		PlayerEx.Player.MenPaiHaoGanDu.SetField(PlayerEx.Player.menPai.ToString(), val);
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x00077921 File Offset: 0x00075B21
	public static int GetNingZhouShengWangLevel()
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetNingZhouShengWang());
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0007792D File Offset: 0x00075B2D
	public static int GetSeaShengWangLevel()
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetSeaShengWang());
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x00077939 File Offset: 0x00075B39
	public static int GetShengWangLevel(int id)
	{
		return PlayerEx.CalcShengWangLevel(PlayerEx.GetShengWang(id));
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x00077946 File Offset: 0x00075B46
	public static float GetNingZhouShengWangProcess()
	{
		return PlayerEx.CalcShengWangProcess(PlayerEx.GetNingZhouShengWang());
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x00077952 File Offset: 0x00075B52
	public static float GetSeaShengWangProcess()
	{
		return PlayerEx.CalcShengWangProcess(PlayerEx.GetSeaShengWang());
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0007795E File Offset: 0x00075B5E
	public static bool IsNingZhouMaxShengWangLevel()
	{
		return PlayerEx.GetNingZhouShengWangLevel() == 7;
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x00077968 File Offset: 0x00075B68
	public static bool IsSeaMaxShengWangLevel()
	{
		return PlayerEx.GetSeaShengWangLevel() == 7;
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x00077974 File Offset: 0x00075B74
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

	// Token: 0x06001316 RID: 4886 RVA: 0x00077BA4 File Offset: 0x00075DA4
	public static int GetXuanShangLevel(int id)
	{
		int shengWang = PlayerEx.GetShengWang(id);
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(id);
		int num;
		string text;
		return PlayerEx.CalcXuanShang(shengWang, shangJinPingFen, out num, out text);
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00077BC8 File Offset: 0x00075DC8
	public static int GetShangJin(int id)
	{
		int shengWang = PlayerEx.GetShengWang(id);
		int shangJinPingFen = PlayerEx.GetShangJinPingFen(id);
		int result;
		string text;
		PlayerEx.CalcXuanShang(shengWang, shangJinPingFen, out result, out text);
		return result;
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x00077BF0 File Offset: 0x00075DF0
	public static void AddShangJinPingFen(int id, int add)
	{
		int val = (PlayerEx.Player.ShangJinPingFen.HasField(id.ToString()) ? PlayerEx.Player.ShangJinPingFen[id.ToString()].I : 0) + add;
		PlayerEx.Player.ShangJinPingFen.SetField(id.ToString(), val);
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x00077C4D File Offset: 0x00075E4D
	public static int GetShangJinPingFen(int id)
	{
		if (!PlayerEx.Player.ShangJinPingFen.HasField(id.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.ShangJinPingFen[id.ToString()].I;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x00077C84 File Offset: 0x00075E84
	public static void SetShiLiChengHaoLevel(int id, int level)
	{
		PlayerEx.Player.ShiLiChengHaoLevel.SetField(id.ToString(), level);
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00077C9D File Offset: 0x00075E9D
	public static int GetShiLiChengHaoLevel(int id)
	{
		if (!PlayerEx.Player.ShiLiChengHaoLevel.HasField(id.ToString()))
		{
			return 0;
		}
		return PlayerEx.Player.ShiLiChengHaoLevel[id.ToString()].I;
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00077CD4 File Offset: 0x00075ED4
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

	// Token: 0x0600131D RID: 4893 RVA: 0x00077D4C File Offset: 0x00075F4C
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

	// Token: 0x0600131E RID: 4894 RVA: 0x00077DAC File Offset: 0x00075FAC
	public static bool HasShuangXiuSkill(int skillID)
	{
		if (!PlayerEx.Player.ShuangXiuData.HasField("HasSkillList"))
		{
			PlayerEx.Player.ShuangXiuData.SetField("HasSkillList", new JSONObject(JSONObject.Type.ARRAY));
		}
		return PlayerEx.Player.ShuangXiuData["HasSkillList"].ToList().Contains(skillID);
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00077E08 File Offset: 0x00076008
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

	// Token: 0x06001320 RID: 4896 RVA: 0x00077FDE File Offset: 0x000761DE
	public static int GetSeaTanSuoDu(int seaID)
	{
		if (PlayerEx.Player.SeaTanSuoDu.HasField(seaID.ToString()))
		{
			return PlayerEx.Player.SeaTanSuoDu[seaID.ToString()].I;
		}
		return 0;
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00078018 File Offset: 0x00076218
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

	// Token: 0x06001322 RID: 4898 RVA: 0x00078144 File Offset: 0x00076344
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

	// Token: 0x06001323 RID: 4899 RVA: 0x000783AC File Offset: 0x000765AC
	public static bool IsLingWuBook(int itemid)
	{
		if (itemid > jsonData.QingJiaoItemIDSegment)
		{
			itemid -= jsonData.QingJiaoItemIDSegment;
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

	// Token: 0x06001324 RID: 4900 RVA: 0x0007854C File Offset: 0x0007674C
	public static void CheckChuHai()
	{
		if (SceneEx.NowSceneName.StartsWith("Sea") && PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(1))
		{
			PlayerEx.Player.OnceShow.Add(1);
			UITutorialSeaMove.Inst.Show();
		}
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x0007859D File Offset: 0x0007679D
	public static void CheckLianDan()
	{
		if (PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(2))
		{
			PlayerEx.Player.OnceShow.Add(2);
			TuJianManager.Inst.OnHyperlink("2_506_3");
		}
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000785D7 File Offset: 0x000767D7
	public static void CheckLianQi()
	{
		if (PlayerEx.Player != null && !PlayerEx.Player.OnceShow.HasItem(3))
		{
			PlayerEx.Player.OnceShow.Add(3);
			TuJianManager.Inst.OnHyperlink("2_506_4");
		}
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00078611 File Offset: 0x00076811
	public static void SetDaoLvChengHu(int npcid, string chenghu)
	{
		if (PlayerEx.Player != null)
		{
			PlayerEx.Player.DaoLvChengHu.SetField(npcid.ToString(), chenghu);
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x00078631 File Offset: 0x00076831
	public static void AddHuaShenStartXianXing(int xianxing)
	{
		PlayerEx.Player.HuaShenStartXianXing = new JSONObject(PlayerEx.Player.HuaShenStartXianXing.I + xianxing);
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00078654 File Offset: 0x00076854
	public static bool HasSkill(int skillID)
	{
		return PlayerEx.Player.hasSkillList.Find((SkillItem aa) => aa.itemId == skillID) != null;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00078690 File Offset: 0x00076890
	public static bool HasStaticSkill(int skillID)
	{
		return PlayerEx.Player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skillID) != null;
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000786CC File Offset: 0x000768CC
	public static void RecordShengPing(string shengPingID, Dictionary<string, string> args = null)
	{
		if (ShengPing.DataDict.ContainsKey(shengPingID))
		{
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
			if (!PlayerEx.Player.ShengPingRecord.HasField(shengPingID))
			{
				PlayerEx.Player.ShengPingRecord.SetField(shengPingID, obj);
				return;
			}
		}
		else
		{
			Debug.LogError("记录生平出错，找不到ID为 " + shengPingID + " 的配表数据");
		}
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000787A0 File Offset: 0x000769A0
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

	// Token: 0x0600132D RID: 4909 RVA: 0x000788E0 File Offset: 0x00076AE0
	public static bool HasTianFu(int tianFuID)
	{
		using (List<int>.Enumerator enumerator = PlayerEx.Player.SelectTianFuID.ToList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == tianFuID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x00078940 File Offset: 0x00076B40
	public static 游戏难度 GetGameDifficulty()
	{
		foreach (int num in PlayerEx.Player.SelectTianFuID.ToList())
		{
			if (num <= 5)
			{
				return (游戏难度)num;
			}
		}
		return 游戏难度.未知;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000789A0 File Offset: 0x00076BA0
	public static void AddErrorItemID(int id)
	{
		if (!PlayerEx.ErrorItemIDList.Contains(id))
		{
			PlayerEx.ErrorItemIDList.Add(id);
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000789BC File Offset: 0x00076BBC
	public static void DeleteErrorItem()
	{
		if (PlayerEx.Player == null)
		{
			return;
		}
		if (PlayerEx.ErrorItemIDList.Count == 0)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("即将从背包删除以下异常的物品：");
		foreach (int num in PlayerEx.ErrorItemIDList)
		{
			stringBuilder.AppendLine(num.ToString());
		}
		UBigCheckBox.Show(stringBuilder.ToString(), delegate
		{
			for (int i = PlayerEx.Player.itemList.values.Count - 1; i >= 0; i--)
			{
				if (PlayerEx.ErrorItemIDList.Contains(PlayerEx.Player.itemList.values[i].itemId))
				{
					PlayerEx.Player.itemList.values.RemoveAt(i);
				}
			}
			PlayerEx.ErrorItemIDList.Clear();
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
		});
	}

	// Token: 0x04000E75 RID: 3701
	private static Dictionary<int, int> _ShengWangLevelDict = new Dictionary<int, int>();

	// Token: 0x04000E76 RID: 3702
	private static bool isShengWangInited;

	// Token: 0x04000E77 RID: 3703
	public static List<int> ErrorItemIDList = new List<int>();
}
