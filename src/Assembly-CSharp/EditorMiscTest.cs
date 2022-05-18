using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DebuggingEssentials;
using Fungus;
using KBEngine;
using Newtonsoft.Json;
using script.Submit;
using UnityEngine;
using YSGame.EquipRandom;
using YSGame.Fight;
using YSGame.TianJiDaBi;
using YSGame.TuJian;

// Token: 0x02000257 RID: 599
[ConsoleAlias("test")]
public class EditorMiscTest
{
	// Token: 0x06001221 RID: 4641 RVA: 0x00011473 File Offset: 0x0000F673
	[ConsoleCommand("", "时间增加1天", 1)]
	public static void AddTime1Day()
	{
		PlayerEx.Player.AddTime(1, 0, 0);
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x00011482 File Offset: 0x0000F682
	[ConsoleCommand("", "时间增加1月", 1)]
	public static void AddTime1Month()
	{
		PlayerEx.Player.AddTime(0, 1, 0);
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x00011491 File Offset: 0x0000F691
	[ConsoleCommand("", "时间增加1年", 1)]
	public static void AddTime1Year()
	{
		PlayerEx.Player.AddTime(0, 0, 1);
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000114A0 File Offset: 0x0000F6A0
	[ConsoleCommand("", "时间增加10年", 1)]
	public static void AddTime10Year()
	{
		PlayerEx.Player.AddTime(0, 0, 10);
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x000114B0 File Offset: 0x0000F6B0
	[ConsoleCommand("", "时间增加990年", 1)]
	public static void AddTime990Year()
	{
		PlayerEx.Player.AddTime(0, 0, 990);
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06001226 RID: 4646 RVA: 0x000114C3 File Offset: 0x0000F6C3
	// (set) Token: 0x06001227 RID: 4647 RVA: 0x000114CA File Offset: 0x0000F6CA
	public bool ClientAppDebugMode
	{
		get
		{
			return clientApp.debugMode;
		}
		set
		{
			clientApp.debugMode = value;
		}
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000114D2 File Offset: 0x0000F6D2
	public void TestUInputBox()
	{
		UInputBox.Show("为洞府命名", delegate(string s)
		{
			Debug.Log("将洞府命名为" + s);
		});
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000114FD File Offset: 0x0000F6FD
	public void OpenLingTianPanel()
	{
		UIDongFu.Inst.ShowLingTianPanel();
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00011509 File Offset: 0x0000F709
	public void GetDongFu(int dongFuID = 1, int level = 1)
	{
		DongFuManager.CreateDongFu(dongFuID, level);
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00011512 File Offset: 0x0000F712
	public void SetJuLingZhenLevel(int dongFuID = 1, int level = 1)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("JuLingZhenLevel", level);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x0001153E File Offset: 0x0000F73E
	public void LoadDongFu(int dongFuID = 1)
	{
		DongFuManager.LoadDongFuScene(dongFuID);
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000ADF28 File Offset: 0x000AC128
	public void CreateTestEquip(bool givePlayer, int count = 100)
	{
		int itemID = 0;
		JSONObject seid = null;
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		for (int i = 0; i < count; i++)
		{
			RandomEquip.CreateRandomEquip(ref itemID, ref seid, -1, -1, -1, -1, -1, null);
			if (givePlayer)
			{
				PlayerEx.Player.addItem(itemID, seid, 1);
			}
		}
		stopwatch.Stop();
		Debug.Log(string.Format("生成{0}个随机装备完成，用时{1}ms", count, stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x00011546 File Offset: 0x0000F746
	public void CreateTestEquip100ToPlayer()
	{
		this.CreateTestEquip(true, 100);
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00011551 File Offset: 0x0000F751
	public void CreateTestEquip3000ToPlayer()
	{
		this.CreateTestEquip(true, 3000);
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x0001155F File Offset: 0x0000F75F
	public void CreateTestEquip10000()
	{
		this.CreateTestEquip(false, 10000);
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06001231 RID: 4657 RVA: 0x000ADF98 File Offset: 0x000AC198
	public string PlayerLingGen
	{
		get
		{
			if (PlayerEx.Player == null)
			{
				return "无数据";
			}
			string text = "";
			foreach (int num in PlayerEx.Player.LingGeng)
			{
				text += string.Format("{0} ", num);
			}
			return text;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06001232 RID: 4658 RVA: 0x0001156D File Offset: 0x0000F76D
	// (set) Token: 0x06001233 RID: 4659 RVA: 0x00011582 File Offset: 0x0000F782
	public int PlayerHPMax
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player._HP_Max;
			}
			return 0;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player._HP_Max = value;
			}
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06001234 RID: 4660 RVA: 0x00011596 File Offset: 0x0000F796
	// (set) Token: 0x06001235 RID: 4661 RVA: 0x000115AB File Offset: 0x0000F7AB
	public int PlayerHP
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.HP;
			}
			return 0;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.HP = value;
			}
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06001236 RID: 4662 RVA: 0x000115BF File Offset: 0x0000F7BF
	// (set) Token: 0x06001237 RID: 4663 RVA: 0x000115D5 File Offset: 0x0000F7D5
	public ulong PlayerExp
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.exp;
			}
			return 0UL;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.exp = value;
			}
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06001238 RID: 4664 RVA: 0x000115E9 File Offset: 0x0000F7E9
	// (set) Token: 0x06001239 RID: 4665 RVA: 0x000115FE File Offset: 0x0000F7FE
	public uint PlayerWuXing
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.wuXin;
			}
			return 0U;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.wuXin = value;
			}
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x0600123A RID: 4666 RVA: 0x00011612 File Offset: 0x0000F812
	// (set) Token: 0x0600123B RID: 4667 RVA: 0x00011627 File Offset: 0x0000F827
	public int PlayerLingGan
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.LingGan;
			}
			return 0;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.LingGan = value;
			}
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x0600123C RID: 4668 RVA: 0x0001163B File Offset: 0x0000F83B
	// (set) Token: 0x0600123D RID: 4669 RVA: 0x00011655 File Offset: 0x0000F855
	public int PlayerHuaShenWuDao
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.HuaShenWuDao.I;
			}
			return 0;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.HuaShenWuDao.i = (long)value;
			}
			PlayerEx.Player.HuaShenWuDao.n = (float)value;
		}
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x00011680 File Offset: 0x0000F880
	[ConsoleCommand("", "提升境界", 1)]
	public static void AddLevel()
	{
		PlayerEx.Player.exp = 0UL;
		PlayerEx.Player.levelUp();
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000AE014 File Offset: 0x000AC214
	[ConsoleCommand("", "获取全大道", 1)]
	public static void AddWuDao()
	{
		foreach (JSONObject jsonobject in jsonData.instance.WuDaoAllTypeJson.list)
		{
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
			jsonobject2.SetField("ex", 150000);
			jsonobject2.SetField("study", new JSONObject(JSONObject.Type.ARRAY));
			PlayerEx.Player.WuDaoJson.SetField(jsonobject["id"].I.ToString(), jsonobject2);
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00011698 File Offset: 0x0000F898
	[ConsoleCommand("", "学习御剑飞行", 1)]
	public static void StudyFly()
	{
		LearningStaticSkill.Study(804);
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000116A4 File Offset: 0x0000F8A4
	[ConsoleCommand("", "学习功法(功法ID)", 1)]
	public static void StudyGongFa(int gongFaID)
	{
		LearningStaticSkill.Study(gongFaID);
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000116AC File Offset: 0x0000F8AC
	[ConsoleCommand("", "学习神通(神通ID)", 1)]
	public static void StudyShenTong(int shenTongID)
	{
		LearningSkill.Study(shenTongID);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000116B4 File Offset: 0x0000F8B4
	[ConsoleCommand("", "增加神识(增加量)", 1)]
	public static void AddShenShi(int shenShi = 20)
	{
		PlayerEx.Player.addShenShi(shenShi);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000116C1 File Offset: 0x0000F8C1
	[ConsoleCommand("", "增加遁速(增加量)", 1)]
	public static void AddDunSu(int dunSu = 20)
	{
		PlayerEx.Player._dunSu += dunSu;
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x000116D5 File Offset: 0x0000F8D5
	[ConsoleCommand("", "增加灵感(增加量)", 1)]
	public static void AddLingGan(int lingGan = 20)
	{
		PlayerEx.Player.AddLingGan(lingGan);
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x000116E2 File Offset: 0x0000F8E2
	[ConsoleCommand("", "增加灵石(增加量)", 1)]
	public static void AddMoney(int money = 10000)
	{
		PlayerEx.Player.AddMoney(money);
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000116EF File Offset: 0x0000F8EF
	[ConsoleCommand("", "增加宁州声望(增加量)", 1)]
	public static void AddNingZhouShengWang(int shengWang = 50)
	{
		PlayerEx.AddNingZhouShengWang(shengWang);
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000116F7 File Offset: 0x0000F8F7
	[ConsoleCommand("", "增加海上声望(增加量)", 1)]
	public static void AddSeaShengWang(int shengWang = 50)
	{
		PlayerEx.AddSeaShengWang(shengWang);
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x000116FF File Offset: 0x0000F8FF
	[ConsoleCommand("", "增加声望(势力ID, 增加量)", 1)]
	public static void AddShengWang(int id, int shengWang = 50)
	{
		PlayerEx.AddShengWang(id, shengWang, false);
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000AE0BC File Offset: 0x000AC2BC
	[ConsoleCommand("", "移除物品(物品ID)", 1)]
	public static void RemoveItem(int id)
	{
		int removeItemNum = PlayerEx.Player.getRemoveItemNum(id);
		PlayerEx.Player.removeItem(id, removeItemNum);
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00011709 File Offset: 0x0000F909
	[ConsoleCommand("", "加载场景(场景名称)", 1)]
	public static void LoadScene(string sceneName = "S1")
	{
		Tools.instance.loadMapScenes(sceneName, true);
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00011717 File Offset: 0x0000F917
	[ConsoleCommand("", "加载副本(场景名称, 副本内位置)", 1)]
	public static void LoadFuBen(string sceneName = "F1", int fubenPos = 1)
	{
		SceneEx.LoadFuBen(sceneName, fubenPos);
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x00011720 File Offset: 0x0000F920
	[ConsoleCommand("", "增加海域探索度(海域ID, 增加量)", 1)]
	public static void AddSeaTanSuoDu(int seaID = 9, int tanSuoDu = 10)
	{
		PlayerEx.AddSeaTanSuoDu(seaID, tanSuoDu);
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00011729 File Offset: 0x0000F929
	[ConsoleCommand("", "获取当前战斗UI状态", 1)]
	public static void GetNowUIFightState()
	{
		Debug.Log(UIFightPanel.Inst.UIFightState);
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000AE0E4 File Offset: 0x000AC2E4
	[ConsoleCommand("", "获得逻辑灵气数量", 1)]
	public static void GetLogicLingQiCount()
	{
		string text = "玩家逻辑灵气数量:";
		for (int i = 0; i < 6; i++)
		{
			text += string.Format("{0} ", PlayerEx.Player.cardMag[i]);
		}
		Debug.Log(text);
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000AE130 File Offset: 0x000AC330
	[ConsoleCommand("", "获得UI灵气数量", 1)]
	public static void GetUILingQiCount()
	{
		string text = "玩家UI灵气数量:";
		for (int i = 0; i < 6; i++)
		{
			text += string.Format("{0} ", UIFightPanel.Inst.PlayerLingQiController.SlotList[i].LingQiCount);
		}
		Debug.Log(text);
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0001173F File Offset: 0x0000F93F
	[ConsoleCommand("", "获得强强技能[仅在战斗中]", 1)]
	public static void AddSuperSkill()
	{
		PlayerEx.Player.FightAddSkill(10000, 0, 12);
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x00011753 File Offset: 0x0000F953
	[ConsoleCommand("", "获得技能[仅在战斗中]", 1)]
	public static void FightAddSkill(int skillid)
	{
		if (skillid > 0)
		{
			PlayerEx.Player.FightAddSkill(skillid, 0, 12);
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x00011767 File Offset: 0x0000F967
	[ConsoleCommand("", "战斗中给玩家加buff(buffID, 层数)", 1)]
	public static void AddBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.spell.addBuff(buffID, count);
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x0001177B File Offset: 0x0000F97B
	[ConsoleCommand("", "战斗中给敌人加buff(buffID, 层数)", 1)]
	public static void AddDiRenBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.OtherAvatar.spell.addBuff(buffID, count);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00011794 File Offset: 0x0000F994
	[ConsoleCommand("", "战斗中给敌人加血量上限(增加量)", 1)]
	public static void AddDiRenHP(int count = 10000)
	{
		PlayerEx.Player.OtherAvatar.HP_Max += count;
		PlayerEx.Player.OtherAvatar.HP = PlayerEx.Player.OtherAvatar.HP_Max;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000AE188 File Offset: 0x000AC388
	[ConsoleCommand("", "战斗中给玩家加手牌(卡牌类型, 数量) 0金1木2水3火4土5魔", 1)]
	public static void AddCard(LingQiType lingQiType = LingQiType.金, int count = 20)
	{
		if (lingQiType != LingQiType.Count)
		{
			RoundManager.instance.DrawCardCreatSpritAndAddCrystal(PlayerEx.Player, (int)lingQiType, count);
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			RoundManager.instance.DrawCardCreatSpritAndAddCrystal(PlayerEx.Player, i, count);
		}
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x000117CB File Offset: 0x0000F9CB
	public void OpenHuaShen()
	{
		GlobalValue.SetTalk(0, 605, "RuntimeTestCode.OpenHuaShen");
		UIHuaShenRuDaoSelect.Inst.Show();
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000117E7 File Offset: 0x0000F9E7
	public void TestDiMo(int 敌人ID = 5040)
	{
		Tools.instance.startFight(敌人ID);
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
	public void ChangePlayerLiHui(int liHuiID = 10001)
	{
		PlayerEx.Player.Face = new JSONObject(liHuiID);
		if (UIHeadPanel.Inst != null)
		{
			UIHeadPanel.Inst.Face.setFace();
		}
		Debug.Log(string.Format("修改玩家立绘为{0}", liHuiID));
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x000AE218 File Offset: 0x000AC418
	public void ChangeNPCLiHui(int liHuiID = 10001)
	{
		jsonData.instance.AvatarJsonData[UINPCJiaoHu.Inst.NowJiaoHuNPC.ID.ToString()].SetField("face", liHuiID);
		NpcJieSuanManager.inst.isUpDateNpcList = true;
		UINPCJiaoHu.Inst.JiaoHuPop.RefreshUI();
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000117F4 File Offset: 0x0000F9F4
	public void NPCAddGongXian(int npcid = 0, int gongxian = 10)
	{
		if (npcid == 0)
		{
			NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(UINPCJiaoHu.Inst.NowJiaoHuNPC.ID, gongxian);
			return;
		}
		NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(npcid, gongxian);
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x0001182A File Offset: 0x0000FA2A
	public void TianJiDaBiRollNPC(int rollCount = 48)
	{
		TianJiDaBiManager.RollDaBiPlayer(rollCount);
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00011833 File Offset: 0x0000FA33
	public void SimDaBi48()
	{
		TianJiDaBiManager.OnTimeSimDaBi();
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x0001183A File Offset: 0x0000FA3A
	public void LogLastDaBi()
	{
		Debug.Log(JsonConvert.SerializeObject(PlayerEx.Player.StreamData.TianJiDaBiSaveData.LastMatch));
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x0001185A File Offset: 0x0000FA5A
	public void StartDaBi(bool playerJoin = true)
	{
		TianJiDaBiManager.CmdTianJiDaBiStart(playerJoin, null);
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x00011863 File Offset: 0x0000FA63
	public void OpenSaiChangUI()
	{
		UITianJiDaBiSaiChang.ShowNormal();
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x0001186A File Offset: 0x0000FA6A
	public void RecordPlayerFight(bool playerWin = true)
	{
		CmdTianJiDaBiRecordPlayer.Do(playerWin);
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x00011872 File Offset: 0x0000FA72
	public void NewRound()
	{
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		nowMatch.NewRound();
		nowMatch.AfterRound();
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00011884 File Offset: 0x0000FA84
	public void LogMatchPlayers()
	{
		TianJiDaBiManager.GetNowMatch().LogPlayerRecord();
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00011890 File Offset: 0x0000FA90
	public void OpenTianJiRank()
	{
		UITianJiDaBiRankPanel.Show(null);
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06001265 RID: 4709 RVA: 0x00011898 File Offset: 0x0000FA98
	// (set) Token: 0x06001266 RID: 4710 RVA: 0x0001189F File Offset: 0x0000FA9F
	public bool GlobaValueLog
	{
		get
		{
			return GlobalValue.LogSource;
		}
		set
		{
			GlobalValue.LogSource = value;
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000AE270 File Offset: 0x000AC470
	[ConsoleCommand("", "扫描全局变量情况", 1)]
	public static void ScanStaticValue()
	{
		StringBuilder stringBuilder = new StringBuilder();
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.Log("必须先开始游戏才能使用此功能");
			return;
		}
		int num = player.StaticValue.Value.Length;
		for (int i = 0; i < num; i++)
		{
			if (player.StaticValue.Value[i] > 0)
			{
				stringBuilder.AppendLine(string.Format("{0}:{1}", i, player.StaticValue.Value[i]));
			}
		}
		Debug.Log(string.Format("全局变量情况:\n{0}", stringBuilder));
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000AE2FC File Offset: 0x000AC4FC
	[ConsoleCommand("", "扫描场景内fungus全局变量情况", 1)]
	public static void ScanSceneStaticValue()
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		foreach (Flowchart flowchart in array)
		{
			SetStaticValue[] components = flowchart.GetComponents<SetStaticValue>();
			SetStaticValueByVar[] components2 = flowchart.GetComponents<SetStaticValueByVar>();
			GetStaticValue[] components3 = flowchart.GetComponents<GetStaticValue>();
			num += components.Length;
			num += components2.Length;
			foreach (SetStaticValue setStaticValue in components)
			{
				Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的SetStaticValue,Value:{3}", new object[]
				{
					flowchart.GetParentName(),
					setStaticValue.ParentBlock.BlockName,
					setStaticValue.StaticValueID,
					setStaticValue.value
				}));
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
			foreach (SetStaticValueByVar setStaticValueByVar in components2)
			{
				Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的SetStaticValueByVar", flowchart.GetParentName(), setStaticValueByVar.ParentBlock.BlockName, setStaticValueByVar.StaticValueID.Value));
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
			foreach (GetStaticValue getStaticValue in components3)
			{
				Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的GetStaticValue", flowchart.GetParentName(), getStaticValue.ParentBlock.BlockName, getStaticValue.StaticValueID));
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
		}
		if (num == 0)
		{
			Debug.Log("此场景中没有全局变量的操作");
		}
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000AE4A4 File Offset: 0x000AC6A4
	[ConsoleCommand("", "检查场景内SetStaticValue(全局变量ID)", 1)]
	public static void CheckSetStaticValue(int StaticValueID = 1)
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		foreach (Flowchart flowchart in array)
		{
			SetStaticValue[] components = flowchart.GetComponents<SetStaticValue>();
			SetStaticValueByVar[] components2 = flowchart.GetComponents<SetStaticValueByVar>();
			num += components.Length;
			num += components2.Length;
			foreach (SetStaticValue setStaticValue in components)
			{
				if (setStaticValue.StaticValueID == StaticValueID)
				{
					Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的SetStaticValue,Value:{3}", new object[]
					{
						flowchart.GetParentName(),
						setStaticValue.ParentBlock.BlockName,
						StaticValueID,
						setStaticValue.value
					}));
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
			foreach (SetStaticValueByVar setStaticValueByVar in components2)
			{
				if (setStaticValueByVar.StaticValueID.Value == StaticValueID)
				{
					Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的SetStaticValueByVar", flowchart.GetParentName(), setStaticValueByVar.ParentBlock.BlockName, StaticValueID));
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.Log(string.Format("{0}个SetStaticValue中没有找到StaticValueID为{1}的指令", num, StaticValueID));
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000AE604 File Offset: 0x000AC804
	[ConsoleCommand("", "检查场景内GetStaticValue(全局变量ID)", 1)]
	public static void CheckGetStaticValue(int StaticValueID = 1)
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		foreach (Flowchart flowchart in array)
		{
			GetStaticValue[] components = flowchart.GetComponents<GetStaticValue>();
			num += components.Length;
			foreach (GetStaticValue getStaticValue in components)
			{
				if (getStaticValue.StaticValueID == StaticValueID)
				{
					Debug.Log(string.Format("Chart:{0},Block:{1}拥有ID为{2}的GetStaticValue", flowchart.GetParentName(), getStaticValue.ParentBlock.BlockName, StaticValueID));
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.Log(string.Format("{0}个GetStaticValue中没有找到StaticValueID为{1}的指令", num, StaticValueID));
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000AE6CC File Offset: 0x000AC8CC
	[ConsoleCommand("", "检查指定全局变量(全局变量ID)", 1)]
	public static void CheckStaticValue(int id)
	{
		int num = GlobalValue.Get(id, "RuntimeTestCode.CheckStaticValue");
		Debug.Log(string.Format("全局变量{0}的值为:{1}", id, num));
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000118A7 File Offset: 0x0000FAA7
	[ConsoleCommand("", "设置指定全局变量(全局变量ID, 值)", 1)]
	public static void SetStaticValue(int id, int value)
	{
		GlobalValue.Set(id, value, "RuntimeTestCode.SetStaticValue");
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000118B5 File Offset: 0x0000FAB5
	public void RefreshPlayerAvatar()
	{
		this.PlayerAvatar = PlayerEx.Player;
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x000118C2 File Offset: 0x0000FAC2
	public void WeiLaiTest(int npcId)
	{
		NpcJieSuanManager.inst.npcUseItem.autoUseItem(npcId);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000118D4 File Offset: 0x0000FAD4
	public void WeiLaiTest2(int npcId)
	{
		SubmitOpenMag.OpenLianQiSub(502);
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000118E0 File Offset: 0x0000FAE0
	public void CheckCanClick()
	{
		Tools.instance.canClick(true, true);
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000118EF File Offset: 0x0000FAEF
	public void CheckWuDaoError()
	{
		UINPCData.CheckWuDaoError();
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000AE700 File Offset: 0x000AC900
	public void CreateLuanLiuMap()
	{
		if (PlayerEx.Player == null)
		{
			Debug.LogError("要生成乱流，请先载入存档");
			return;
		}
		Debug.Log("开始生成乱流，请耐心等待...");
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		PlayerEx.Player.seaNodeMag.CreateLuanLiuMap();
		stopwatch.Stop();
		Debug.Log(string.Format("生成完毕，共耗时{0}s", (float)stopwatch.ElapsedMilliseconds / 1000f));
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000118F6 File Offset: 0x0000FAF6
	public void TuJianHylink(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x00011903 File Offset: 0x0000FB03
	public void StartTianJieCD()
	{
		TianJieManager.StartTianJieCD();
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x0001190A File Offset: 0x0000FB0A
	public void TianJieJiaSu(int year)
	{
		TianJieManager.TianJieJiaSu(year);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000AE76C File Offset: 0x000AC96C
	public void SenPopTip(string msg = "消息", int count = 3, PopTipIconType iconType = PopTipIconType.叹号)
	{
		for (int i = 0; i < count; i++)
		{
			UIPopTip.Inst.Pop(msg, iconType);
		}
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00011912 File Offset: 0x0000FB12
	public void SenPopTipOld(string msg = "消息")
	{
		UI_ErrorHint._instance.errorShow(msg, 0);
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x00011920 File Offset: 0x0000FB20
	public void OpenMap(MapArea area, UIMapState state)
	{
		UIMapPanel.Inst.OpenMap(area, state);
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x0001192E File Offset: 0x0000FB2E
	public void OpenMapHighlight(int highlight = 1)
	{
		UIMapPanel.Inst.OpenHighlight(highlight);
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x0001193B File Offset: 0x0000FB3B
	public void OpenXiuChuan()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x00011942 File Offset: 0x0000FB42
	public void ChangeTitle(string title)
	{
		GameWindowTitle.Inst.SetTitle(title);
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0001194F File Offset: 0x0000FB4F
	public void UnlockShenXianDouFa(int index)
	{
		Avatar.UnlockShenXianDouFa(index);
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x00011957 File Offset: 0x0000FB57
	public void OpenMiniShop(int itemID = 5001, int price = 100, int maxSellCount = 10000)
	{
		UIMiniShop.Show(itemID, price, maxSellCount, null);
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x00011962 File Offset: 0x0000FB62
	public void LogGanYingLuaScript()
	{
		Debug.Log((RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor).checkSucessScript);
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x000AE794 File Offset: 0x000AC994
	public void SetGanYingLuaScript(string lua)
	{
		TianJieMiShuLingWuFightEventProcessor tianJieMiShuLingWuFightEventProcessor = RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor;
		tianJieMiShuLingWuFightEventProcessor.checkSucessScript = lua;
		Debug.Log("设置了" + tianJieMiShuLingWuFightEventProcessor.checkSucessScript);
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
	public void StartTianJieGanYing(string MiShuID = "盾")
	{
		TianJieMiShuLingWuFightEventProcessor.MiShuID = MiShuID;
		StartFight.Do(10010, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.天劫秘术领悟, 0, 0, 0, 0, "战斗3", false, "", new List<StarttFightAddBuff>(), new List<StarttFightAddBuff>());
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x0001197D File Offset: 0x0000FB7D
	public void OpenDuJieZhunBei(bool isDuJie)
	{
		UIDuJieZhunBei.OpenPanel(isDuJie);
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x000AE80C File Offset: 0x000ACA0C
	public void LogShengPing()
	{
		List<ShengPingData> shengPingList = PlayerEx.GetShengPingList();
		string text = string.Format("生平信息:共{0}条\n", shengPingList.Count);
		foreach (ShengPingData arg in shengPingList)
		{
			text += string.Format("{0}\n", arg);
		}
		Debug.Log(text);
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x00011985 File Offset: 0x0000FB85
	public void OpenJianLingPanel()
	{
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x0001198C File Offset: 0x0000FB8C
	public void OpenJianLingPanel2()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06001285 RID: 4741 RVA: 0x000119B6 File Offset: 0x0000FBB6
	// (set) Token: 0x06001286 RID: 4742 RVA: 0x000119CB File Offset: 0x0000FBCB
	public int LaoYeYeJiYiHuiFuDuEx
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.JianLingExJiYiHuiFuDu;
			}
			return 0;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.JianLingExJiYiHuiFuDu = value;
			}
		}
	}

	// Token: 0x04000E96 RID: 3734
	public Avatar PlayerAvatar;
}
