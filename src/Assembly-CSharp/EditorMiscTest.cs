using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DebuggingEssentials;
using Fungus;
using JSONClass;
using KBEngine;
using Newtonsoft.Json;
using script.ExchangeMeeting.UI.Interface;
using Tab;
using UnityEngine;
using YSGame.EquipRandom;
using YSGame.Fight;
using YSGame.TianJiDaBi;
using YSGame.TuJian;

// Token: 0x0200017B RID: 379
[ConsoleAlias("test")]
public class EditorMiscTest
{
	// Token: 0x06000FC7 RID: 4039 RVA: 0x0005E5CE File Offset: 0x0005C7CE
	[ConsoleCommand("", "时间增加1天", 1)]
	public static void AddTime1Day()
	{
		PlayerEx.Player.AddTime(1, 0, 0);
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0005E5DD File Offset: 0x0005C7DD
	[ConsoleCommand("", "时间增加1月", 1)]
	public static void AddTime1Month()
	{
		PlayerEx.Player.AddTime(0, 1, 0);
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0005E5EC File Offset: 0x0005C7EC
	[ConsoleCommand("", "时间增加1年", 1)]
	public static void AddTime1Year()
	{
		PlayerEx.Player.AddTime(0, 0, 1);
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x0005E5FB File Offset: 0x0005C7FB
	[ConsoleCommand("", "时间增加10年", 1)]
	public static void AddTime10Year()
	{
		PlayerEx.Player.AddTime(0, 0, 10);
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x0005E60B File Offset: 0x0005C80B
	[ConsoleCommand("", "时间增加990年", 1)]
	public static void AddTime990Year()
	{
		PlayerEx.Player.AddTime(0, 0, 990);
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0005E61E File Offset: 0x0005C81E
	// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0005E625 File Offset: 0x0005C825
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

	// Token: 0x06000FCE RID: 4046 RVA: 0x0005E62D File Offset: 0x0005C82D
	public void TestUInputBox()
	{
		UInputBox.Show("为洞府命名", delegate(string s)
		{
			Debug.Log("将洞府命名为" + s);
		});
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x0005E658 File Offset: 0x0005C858
	public void OpenLingTianPanel()
	{
		UIDongFu.Inst.ShowLingTianPanel();
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x0005E664 File Offset: 0x0005C864
	public void GetDongFu(int dongFuID = 1, int level = 1)
	{
		DongFuManager.CreateDongFu(dongFuID, level);
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x0005E66D File Offset: 0x0005C86D
	public void SetJuLingZhenLevel(int dongFuID = 1, int level = 1)
	{
		PlayerEx.Player.DongFuData[string.Format("DongFu{0}", dongFuID)].SetField("JuLingZhenLevel", level);
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x0005E699 File Offset: 0x0005C899
	public void LoadDongFu(int dongFuID = 1)
	{
		DongFuManager.LoadDongFuScene(dongFuID);
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
	public void CreateTestEquip(bool givePlayer, int count = 100)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int itemID = 0;
		JSONObject jsonobject = null;
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		for (int i = 0; i < count; i++)
		{
			RandomEquip.CreateRandomEquip(ref itemID, ref jsonobject, -1, -1, -1, -1, -1, null);
			if (givePlayer)
			{
				PlayerEx.Player.addItem(itemID, jsonobject, 1);
				stringBuilder.Append(jsonobject["Name"].str + " ");
			}
		}
		stopwatch.Stop();
		Debug.Log(string.Format("生成{0}个随机装备完成，用时{1}ms", count, stopwatch.ElapsedMilliseconds));
		Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x0005E748 File Offset: 0x0005C948
	public void CreateTestEquip100ToPlayer()
	{
		this.CreateTestEquip(true, 100);
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x0005E753 File Offset: 0x0005C953
	public void CreateTestEquip3000ToPlayer()
	{
		this.CreateTestEquip(true, 3000);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x0005E761 File Offset: 0x0005C961
	public void CreateTestEquip10000()
	{
		this.CreateTestEquip(false, 10000);
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x0005E76F File Offset: 0x0005C96F
	public void ForeachRandomEquipName()
	{
		RandomEquip.TestLogAllBetterRandomEquipName();
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0005E778 File Offset: 0x0005C978
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

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0005E7F4 File Offset: 0x0005C9F4
	// (set) Token: 0x06000FDA RID: 4058 RVA: 0x0005E809 File Offset: 0x0005CA09
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

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0005E81D File Offset: 0x0005CA1D
	// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0005E832 File Offset: 0x0005CA32
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

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0005E846 File Offset: 0x0005CA46
	// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0005E85C File Offset: 0x0005CA5C
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

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0005E870 File Offset: 0x0005CA70
	// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x0005E885 File Offset: 0x0005CA85
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

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0005E899 File Offset: 0x0005CA99
	// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0005E8AE File Offset: 0x0005CAAE
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

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0005E8C2 File Offset: 0x0005CAC2
	[ConsoleCommand("", "提升境界", 1)]
	public static void AddLevel()
	{
		PlayerEx.Player.exp = 0UL;
		PlayerEx.Player.levelUp();
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0005E8DC File Offset: 0x0005CADC
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

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0005E984 File Offset: 0x0005CB84
	[ConsoleCommand("", "学习御剑飞行", 1)]
	public static void StudyFly()
	{
		LearningStaticSkill.Study(804);
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0005E990 File Offset: 0x0005CB90
	[ConsoleCommand("", "学习功法(功法ID)", 1)]
	public static void StudyGongFa(int gongFaID)
	{
		LearningStaticSkill.Study(gongFaID);
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0005E998 File Offset: 0x0005CB98
	[ConsoleCommand("", "学习神通(神通ID)", 1)]
	public static void StudyShenTong(int shenTongID)
	{
		LearningSkill.Study(shenTongID);
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0005E9A0 File Offset: 0x0005CBA0
	[ConsoleCommand("", "增加神识(增加量)", 1)]
	public static void AddShenShi(int shenShi = 20)
	{
		PlayerEx.Player.addShenShi(shenShi);
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0005E9AD File Offset: 0x0005CBAD
	[ConsoleCommand("", "增加遁速(增加量)", 1)]
	public static void AddDunSu(int dunSu = 20)
	{
		PlayerEx.Player._dunSu += dunSu;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0005E9C1 File Offset: 0x0005CBC1
	[ConsoleCommand("", "增加灵感(增加量)", 1)]
	public static void AddLingGan(int lingGan = 20)
	{
		PlayerEx.Player.AddLingGan(lingGan);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0005E9CE File Offset: 0x0005CBCE
	[ConsoleCommand("", "增加灵石(增加量)", 1)]
	public static void AddMoney(int money = 10000)
	{
		PlayerEx.Player.AddMoney(money);
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0005E9DB File Offset: 0x0005CBDB
	[ConsoleCommand("", "增加宁州声望(增加量)", 1)]
	public static void AddNingZhouShengWang(int shengWang = 50)
	{
		PlayerEx.AddNingZhouShengWang(shengWang);
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0005E9E3 File Offset: 0x0005CBE3
	[ConsoleCommand("", "增加海上声望(增加量)", 1)]
	public static void AddSeaShengWang(int shengWang = 50)
	{
		PlayerEx.AddSeaShengWang(shengWang);
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0005E9EB File Offset: 0x0005CBEB
	[ConsoleCommand("", "增加声望(势力ID, 增加量)", 1)]
	public static void AddShengWang(int id, int shengWang = 50)
	{
		PlayerEx.AddShengWang(id, shengWang, false);
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0005E9F8 File Offset: 0x0005CBF8
	[ConsoleCommand("", "移除物品(物品ID)", 1)]
	public static void RemoveItem(int id)
	{
		int removeItemNum = PlayerEx.Player.getRemoveItemNum(id);
		PlayerEx.Player.removeItem(id, removeItemNum);
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0005EA1D File Offset: 0x0005CC1D
	[ConsoleCommand("", "加载场景(场景名称)", 1)]
	public static void LoadScene(string sceneName = "S1")
	{
		Tools.instance.loadMapScenes(sceneName, true);
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0005EA2B File Offset: 0x0005CC2B
	[ConsoleCommand("", "加载副本(场景名称, 副本内位置)", 1)]
	public static void LoadFuBen(string sceneName = "F1", int fubenPos = 1)
	{
		SceneEx.LoadFuBen(sceneName, fubenPos);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0005EA34 File Offset: 0x0005CC34
	[ConsoleCommand("", "增加海域探索度(海域ID, 增加量)", 1)]
	public static void AddSeaTanSuoDu(int seaID = 9, int tanSuoDu = 10)
	{
		PlayerEx.AddSeaTanSuoDu(seaID, tanSuoDu);
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0005EA3D File Offset: 0x0005CC3D
	[ConsoleCommand("", "获取当前战斗UI状态", 1)]
	public static void GetNowUIFightState()
	{
		Debug.Log(UIFightPanel.Inst.UIFightState);
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0005EA54 File Offset: 0x0005CC54
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

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0005EAA0 File Offset: 0x0005CCA0
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

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0005EAF5 File Offset: 0x0005CCF5
	[ConsoleCommand("", "获得强强技能[仅在战斗中]", 1)]
	public static void AddSuperSkill()
	{
		PlayerEx.Player.FightAddSkill(10000, 0, 12);
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0005EB09 File Offset: 0x0005CD09
	[ConsoleCommand("", "获得技能[仅在战斗中]", 1)]
	public static void FightAddSkill(int skillid)
	{
		if (skillid > 0)
		{
			PlayerEx.Player.FightAddSkill(skillid, 0, 12);
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0005EB1D File Offset: 0x0005CD1D
	[ConsoleCommand("", "战斗中给玩家加buff(buffID, 层数)", 1)]
	public static void AddBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.spell.addBuff(buffID, count);
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0005EB31 File Offset: 0x0005CD31
	[ConsoleCommand("", "战斗中给敌人加buff(buffID, 层数)", 1)]
	public static void AddDiRenBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.OtherAvatar.spell.addBuff(buffID, count);
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0005EB4A File Offset: 0x0005CD4A
	[ConsoleCommand("", "战斗中给敌人加血量上限(增加量)", 1)]
	public static void AddDiRenHP(int count = 10000)
	{
		PlayerEx.Player.OtherAvatar.HP_Max += count;
		PlayerEx.Player.OtherAvatar.HP = PlayerEx.Player.OtherAvatar.HP_Max;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0005EB84 File Offset: 0x0005CD84
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

	// Token: 0x06000FFC RID: 4092 RVA: 0x0005EBC4 File Offset: 0x0005CDC4
	public void OpenHuaShen()
	{
		GlobalValue.SetTalk(0, 605, "RuntimeTestCode.OpenHuaShen");
		UIHuaShenRuDaoSelect.Inst.Show();
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x0005EBE0 File Offset: 0x0005CDE0
	public void TestDiMo(int 敌人ID = 5040)
	{
		Tools.instance.startFight(敌人ID);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0005EBED File Offset: 0x0005CDED
	public void NPCAddGongXian(int npcid = 0, int gongxian = 10)
	{
		if (npcid == 0)
		{
			NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(UINPCJiaoHu.Inst.NowJiaoHuNPC.ID, gongxian);
			return;
		}
		NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(npcid, gongxian);
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0005EC23 File Offset: 0x0005CE23
	public void TianJiDaBiRollNPC(int rollCount = 48)
	{
		TianJiDaBiManager.RollDaBiPlayer(rollCount);
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0005EC2C File Offset: 0x0005CE2C
	public void SimDaBi48()
	{
		TianJiDaBiManager.OnTimeSimDaBi();
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0005EC33 File Offset: 0x0005CE33
	public void LogLastDaBi()
	{
		Debug.Log(JsonConvert.SerializeObject(PlayerEx.Player.StreamData.TianJiDaBiSaveData.LastMatch));
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0005EC53 File Offset: 0x0005CE53
	public void StartDaBi(bool playerJoin = true)
	{
		TianJiDaBiManager.CmdTianJiDaBiStart(playerJoin, null);
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0005EC5C File Offset: 0x0005CE5C
	public void OpenSaiChangUI()
	{
		UITianJiDaBiSaiChang.ShowNormal();
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0005EC63 File Offset: 0x0005CE63
	public void RecordPlayerFight(bool playerWin = true)
	{
		CmdTianJiDaBiRecordPlayer.Do(playerWin);
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0005EC6B File Offset: 0x0005CE6B
	public void NewRound()
	{
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		nowMatch.NewRound();
		nowMatch.AfterRound();
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0005EC7D File Offset: 0x0005CE7D
	public void LogMatchPlayers()
	{
		TianJiDaBiManager.GetNowMatch().LogPlayerRecord();
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0005EC89 File Offset: 0x0005CE89
	public void OpenTianJiRank()
	{
		UITianJiDaBiRankPanel.Show(null);
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06001008 RID: 4104 RVA: 0x0005EC91 File Offset: 0x0005CE91
	// (set) Token: 0x06001009 RID: 4105 RVA: 0x0005EC98 File Offset: 0x0005CE98
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

	// Token: 0x0600100A RID: 4106 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
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
		stringBuilder.AppendLine("=====StaticValue=====");
		for (int i = 0; i < num; i++)
		{
			if (player.StaticValue.Value[i] > 0)
			{
				stringBuilder.AppendLine(string.Format("{0}:{1}", i, player.StaticValue.Value[i]));
			}
		}
		stringBuilder.AppendLine("=====Talk=====");
		int num2 = player.StaticValue.talk.Length;
		for (int j = 0; j < num2; j++)
		{
			stringBuilder.AppendLine(string.Format("{0}:{1}", j, player.StaticValue.talk[j]));
		}
		Debug.Log(string.Format("全局变量情况:\n{0}", stringBuilder));
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0005ED94 File Offset: 0x0005CF94
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

	// Token: 0x0600100C RID: 4108 RVA: 0x0005EF3C File Offset: 0x0005D13C
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

	// Token: 0x0600100D RID: 4109 RVA: 0x0005F09C File Offset: 0x0005D29C
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

	// Token: 0x0600100E RID: 4110 RVA: 0x0005F164 File Offset: 0x0005D364
	[ConsoleCommand("", "检查指定全局变量(全局变量ID)", 1)]
	public static void CheckStaticValue(int id)
	{
		int num = GlobalValue.Get(id, "RuntimeTestCode.CheckStaticValue");
		Debug.Log(string.Format("全局变量{0}的值为:{1}", id, num));
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0005F198 File Offset: 0x0005D398
	[ConsoleCommand("", "设置指定全局变量(全局变量ID, 值)", 1)]
	public static void SetStaticValue(int id, int value)
	{
		GlobalValue.Set(id, value, "RuntimeTestCode.SetStaticValue");
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0005F1A6 File Offset: 0x0005D3A6
	[ConsoleCommand("", "设置指定全局变量talk(ID, 值)", 1)]
	public static void SetStaticValueTalk(int id, int value)
	{
		GlobalValue.SetTalk(id, value, "RuntimeTestCode.SetStaticValueTalk");
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0005F1B4 File Offset: 0x0005D3B4
	public void RefreshPlayerAvatar()
	{
		this.PlayerAvatar = PlayerEx.Player;
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0005F1C1 File Offset: 0x0005D3C1
	public void WeiLaiTest(int npcId)
	{
		IExchangeUIMag.Open();
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
	public void WeiLaiTest2(int id)
	{
		foreach (RandomExchangeData randomExchangeData in RandomExchangeData.DataList)
		{
			if (this.HasRepeat(randomExchangeData.YiWuFlag))
			{
				Debug.LogError("YiWuFlag重复 key为：" + randomExchangeData.id);
			}
			if (this.HasRepeat(randomExchangeData.YiWuItem))
			{
				Debug.LogError("YiWuItem重复 key为：" + randomExchangeData.id);
			}
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x0005F264 File Offset: 0x0005D464
	private bool HasRepeat(List<int> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			for (int j = i + 1; j < list.Count; j++)
			{
				if (list[i] == list[j])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0005F2A8 File Offset: 0x0005D4A8
	public void CheckCanClick()
	{
		Tools.instance.canClick(true, true);
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0005F2B7 File Offset: 0x0005D4B7
	public void CheckWuDaoError()
	{
		UINPCData.CheckWuDaoError();
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0005F2C0 File Offset: 0x0005D4C0
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

	// Token: 0x06001018 RID: 4120 RVA: 0x0005F32B File Offset: 0x0005D52B
	public void TuJianHylink(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0005F338 File Offset: 0x0005D538
	public void StartTianJieCD()
	{
		TianJieManager.StartTianJieCD();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0005F33F File Offset: 0x0005D53F
	public void TianJieJiaSu(int year)
	{
		TianJieManager.TianJieJiaSu(year);
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0005F348 File Offset: 0x0005D548
	public void SenPopTip(string msg = "消息", int count = 3, PopTipIconType iconType = PopTipIconType.叹号)
	{
		for (int i = 0; i < count; i++)
		{
			UIPopTip.Inst.Pop(msg, iconType);
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0005F36D File Offset: 0x0005D56D
	public void SenPopTipOld(string msg = "消息")
	{
		UI_ErrorHint._instance.errorShow(msg, 0);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0005F37B File Offset: 0x0005D57B
	public void OpenMap(MapArea area, UIMapState state)
	{
		UIMapPanel.Inst.OpenMap(area, state);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0005F389 File Offset: 0x0005D589
	public void OpenMapHighlight(int highlight = 1)
	{
		UIMapPanel.Inst.OpenHighlight(highlight);
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0005F396 File Offset: 0x0005D596
	public void OpenXiuChuan()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0005F39D File Offset: 0x0005D59D
	public void ChangeTitle(string title)
	{
		GameWindowTitle.Inst.SetTitle(title);
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0005F3AA File Offset: 0x0005D5AA
	public void UnlockShenXianDouFa(int index)
	{
		Avatar.UnlockShenXianDouFa(index);
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0005F3B2 File Offset: 0x0005D5B2
	public void OpenMiniShop(int itemID = 5001, int price = 100, int maxSellCount = 10000)
	{
		UIMiniShop.Show(itemID, price, maxSellCount, null);
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0005F3BD File Offset: 0x0005D5BD
	public void LogGanYingLuaScript()
	{
		Debug.Log((RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor).checkSucessScript);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0005F3D8 File Offset: 0x0005D5D8
	public void SetGanYingLuaScript(string lua)
	{
		TianJieMiShuLingWuFightEventProcessor tianJieMiShuLingWuFightEventProcessor = RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor;
		tianJieMiShuLingWuFightEventProcessor.checkSucessScript = lua;
		Debug.Log("设置了" + tianJieMiShuLingWuFightEventProcessor.checkSucessScript);
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0005F414 File Offset: 0x0005D614
	public void StartTianJieGanYing(WuDaoType wuDaoType = WuDaoType.剑)
	{
		CmdSetHuaShenLingYuSkill.Do(wuDaoType);
		Avatar player = PlayerEx.Player;
		int i = player.HuaShenLingYuSkill.I;
		int i2 = player.HuaShenWuDao.I;
		Debug.Log(string.Format("当前化神悟道:{0} 当前化神领域技能ID:{1}", (WuDaoType)i2, i));
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0005F460 File Offset: 0x0005D660
	public void StartTianJieGanYing(string MiShuID = "盾")
	{
		TianJieMiShuLingWuFightEventProcessor.MiShuID = MiShuID;
		StartFight.Do(10010, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.天劫秘术领悟, 0, 0, 0, 0, "战斗3", false, "", new List<StarttFightAddBuff>(), new List<StarttFightAddBuff>());
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0005F49A File Offset: 0x0005D69A
	public void OpenDuJieZhunBei(bool isDuJie)
	{
		UIDuJieZhunBei.OpenPanel(isDuJie);
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
	public void StartDuJieFight()
	{
		StartFight.Do(9999, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.FeiSheng, 0, 0, 0, 0, "战斗3", false, "", new List<StarttFightAddBuff>(), new List<StarttFightAddBuff>());
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0005F4D8 File Offset: 0x0005D6D8
	public void SetDuJieLeiJie(EditorMiscTest.TestTianJieType type)
	{
		for (int i = 0; i < TianJieManager.Inst.LeiJieList.Count; i++)
		{
			TianJieManager.Inst.LeiJieList[i] = type.ToString();
		}
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0005F51C File Offset: 0x0005D71C
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

	// Token: 0x0600102B RID: 4139 RVA: 0x0005F598 File Offset: 0x0005D798
	public void OpenJianLingPanel()
	{
		UIJianLingPanel.OpenPanel();
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0005F59F File Offset: 0x0005D79F
	public void OpenJianLingPanel2()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingPanel"), NewUICanvas.Inst.Canvas.transform).transform.SetAsLastSibling();
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x0600102D RID: 4141 RVA: 0x0005F5C9 File Offset: 0x0005D7C9
	// (set) Token: 0x0600102E RID: 4142 RVA: 0x0005F5DE File Offset: 0x0005D7DE
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

	// Token: 0x0600102F RID: 4143 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
	public void SaveNewSave(int slot = 1)
	{
		if (PanelMamager.inst.UISceneGameObject != null && AuToSLMgr.Inst.CanSave())
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			if (jsonData.instance.saveState == 1)
			{
				UIPopTip.Inst.Pop("存档未完成,请稍等", PopTipIconType.叹号);
				return;
			}
			YSNewSaveSystem.SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), slot, null, false);
		}
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0005F667 File Offset: 0x0005D867
	public void LoadNewSave(int slot = 1)
	{
		YSNewSaveSystem.LoadSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), slot, -1);
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0005F67A File Offset: 0x0005D87A
	public void ZipNewSave(int slot = 0)
	{
		YSZip.ZipFile(Paths.GetNewSavePath(), string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), slot));
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x0005F69C File Offset: 0x0005D89C
	public void UnZipNewSave(int slot = 0)
	{
		YSZip.UnZipFile(string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), slot), Paths.GetNewSavePath() ?? "");
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0005F6C7 File Offset: 0x0005D8C7
	public void UploadSave(int slot = 0)
	{
		YSNewSaveSystem.UploadCloudSaveData(slot);
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0005F6CF File Offset: 0x0005D8CF
	public void DownloadSave(int slot = 0)
	{
		YSNewSaveSystem.DownloadCloudSave(slot);
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0005F6D7 File Offset: 0x0005D8D7
	public void OpenSaveFolder()
	{
		Process.Start(Paths.GetNewSavePath());
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0005F6E4 File Offset: 0x0005D8E4
	public void OpenCloudSaveFolder()
	{
		Process.Start(Paths.GetCloudSavePath());
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0005F6F1 File Offset: 0x0005D8F1
	public void LogCloudFiles()
	{
		YSNewSaveSystem.LogCloudFiles();
	}

	// Token: 0x04000BC6 RID: 3014
	public Avatar PlayerAvatar;

	// Token: 0x0200129A RID: 4762
	public enum TestTianJieType
	{
		// Token: 0x04006616 RID: 26134
		天雷劫,
		// Token: 0x04006617 RID: 26135
		阴阳劫,
		// Token: 0x04006618 RID: 26136
		风火劫,
		// Token: 0x04006619 RID: 26137
		心魔劫,
		// Token: 0x0400661A RID: 26138
		罡雷劫,
		// Token: 0x0400661B RID: 26139
		造化劫,
		// Token: 0x0400661C RID: 26140
		混元劫,
		// Token: 0x0400661D RID: 26141
		五行劫,
		// Token: 0x0400661E RID: 26142
		乾天劫,
		// Token: 0x0400661F RID: 26143
		生死劫,
		// Token: 0x04006620 RID: 26144
		天地劫,
		// Token: 0x04006621 RID: 26145
		灭世劫
	}
}
