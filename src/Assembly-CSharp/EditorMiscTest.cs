using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DebuggingEssentials;
using Fungus;
using JSONClass;
using KBEngine;
using Newtonsoft.Json;
using Tab;
using UnityEngine;
using YSGame.EquipRandom;
using YSGame.Fight;
using YSGame.TianJiDaBi;
using YSGame.TuJian;
using script.ExchangeMeeting.UI.Interface;

[ConsoleAlias("test")]
public class EditorMiscTest
{
	public enum TestTianJieType
	{
		天雷劫,
		阴阳劫,
		风火劫,
		心魔劫,
		罡雷劫,
		造化劫,
		混元劫,
		五行劫,
		乾天劫,
		生死劫,
		天地劫,
		灭世劫
	}

	public Avatar PlayerAvatar;

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

	public string PlayerLingGen
	{
		get
		{
			if (PlayerEx.Player == null)
			{
				return "无数据";
			}
			string text = "";
			foreach (int item in PlayerEx.Player.LingGeng)
			{
				text += $"{item} ";
			}
			return text;
		}
	}

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

	public ulong PlayerExp
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.exp;
			}
			return 0uL;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.exp = value;
			}
		}
	}

	public uint PlayerWuXing
	{
		get
		{
			if (PlayerEx.Player != null)
			{
				return PlayerEx.Player.wuXin;
			}
			return 0u;
		}
		set
		{
			if (PlayerEx.Player != null)
			{
				PlayerEx.Player.wuXin = value;
			}
		}
	}

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

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddTime1Day()
	{
		PlayerEx.Player.AddTime(1);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddTime1Month()
	{
		PlayerEx.Player.AddTime(0, 1);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddTime1Year()
	{
		PlayerEx.Player.AddTime(0, 0, 1);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddTime10Year()
	{
		PlayerEx.Player.AddTime(0, 0, 10);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddTime990Year()
	{
		PlayerEx.Player.AddTime(0, 0, 990);
	}

	public void TestUInputBox()
	{
		UInputBox.Show("为洞府命名", delegate(string s)
		{
			Debug.Log((object)("将洞府命名为" + s));
		});
	}

	public void OpenLingTianPanel()
	{
		UIDongFu.Inst.ShowLingTianPanel();
	}

	public void GetDongFu(int dongFuID = 1, int level = 1)
	{
		DongFuManager.CreateDongFu(dongFuID, level);
	}

	public void SetJuLingZhenLevel(int dongFuID = 1, int level = 1)
	{
		PlayerEx.Player.DongFuData[$"DongFu{dongFuID}"].SetField("JuLingZhenLevel", level);
	}

	public void LoadDongFu(int dongFuID = 1)
	{
		DongFuManager.LoadDongFuScene(dongFuID);
	}

	public void CreateTestEquip(bool givePlayer, int count = 100)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int ItemID = 0;
		JSONObject ItemJson = null;
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		for (int i = 0; i < count; i++)
		{
			RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson);
			if (givePlayer)
			{
				PlayerEx.Player.addItem(ItemID, ItemJson);
				stringBuilder.Append(ItemJson["Name"].str + " ");
			}
		}
		stopwatch.Stop();
		Debug.Log((object)$"生成{count}个随机装备完成，用时{stopwatch.ElapsedMilliseconds}ms");
		Debug.Log((object)stringBuilder.ToString());
	}

	public void CreateTestEquip100ToPlayer()
	{
		CreateTestEquip(givePlayer: true);
	}

	public void CreateTestEquip3000ToPlayer()
	{
		CreateTestEquip(givePlayer: true, 3000);
	}

	public void CreateTestEquip10000()
	{
		CreateTestEquip(givePlayer: false, 10000);
	}

	public void ForeachRandomEquipName()
	{
		RandomEquip.TestLogAllBetterRandomEquipName();
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddLevel()
	{
		PlayerEx.Player.exp = 0uL;
		PlayerEx.Player.levelUp();
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddWuDao()
	{
		foreach (JSONObject item in jsonData.instance.WuDaoAllTypeJson.list)
		{
			JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject.SetField("ex", 150000);
			jSONObject.SetField("study", new JSONObject(JSONObject.Type.ARRAY));
			PlayerEx.Player.WuDaoJson.SetField(item["id"].I.ToString(), jSONObject);
		}
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void StudyFly()
	{
		LearningStaticSkill.Study(804);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void StudyGongFa(int gongFaID)
	{
		LearningStaticSkill.Study(gongFaID);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void StudyShenTong(int shenTongID)
	{
		LearningSkill.Study(shenTongID);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddShenShi(int shenShi = 20)
	{
		PlayerEx.Player.addShenShi(shenShi);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddDunSu(int dunSu = 20)
	{
		PlayerEx.Player._dunSu += dunSu;
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddLingGan(int lingGan = 20)
	{
		PlayerEx.Player.AddLingGan(lingGan);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddMoney(int money = 10000)
	{
		PlayerEx.Player.AddMoney(money);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddNingZhouShengWang(int shengWang = 50)
	{
		PlayerEx.AddNingZhouShengWang(shengWang);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddSeaShengWang(int shengWang = 50)
	{
		PlayerEx.AddSeaShengWang(shengWang);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddShengWang(int id, int shengWang = 50)
	{
		PlayerEx.AddShengWang(id, shengWang);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void RemoveItem(int id)
	{
		int removeItemNum = PlayerEx.Player.getRemoveItemNum(id);
		PlayerEx.Player.removeItem(id, removeItemNum);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void LoadScene(string sceneName = "S1")
	{
		Tools.instance.loadMapScenes(sceneName);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void LoadFuBen(string sceneName = "F1", int fubenPos = 1)
	{
		SceneEx.LoadFuBen(sceneName, fubenPos);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddSeaTanSuoDu(int seaID = 9, int tanSuoDu = 10)
	{
		PlayerEx.AddSeaTanSuoDu(seaID, tanSuoDu);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void GetNowUIFightState()
	{
		Debug.Log((object)UIFightPanel.Inst.UIFightState);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void GetLogicLingQiCount()
	{
		string text = "玩家逻辑灵气数量:";
		for (int i = 0; i < 6; i++)
		{
			text += $"{PlayerEx.Player.cardMag[i]} ";
		}
		Debug.Log((object)text);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void GetUILingQiCount()
	{
		string text = "玩家UI灵气数量:";
		for (int i = 0; i < 6; i++)
		{
			text += $"{UIFightPanel.Inst.PlayerLingQiController.SlotList[i].LingQiCount} ";
		}
		Debug.Log((object)text);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddSuperSkill()
	{
		PlayerEx.Player.FightAddSkill(10000, 0, 12);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void FightAddSkill(int skillid)
	{
		if (skillid > 0)
		{
			PlayerEx.Player.FightAddSkill(skillid, 0, 12);
		}
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.spell.addBuff(buffID, count);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddDiRenBuff(int buffID = 1, int count = 1)
	{
		PlayerEx.Player.OtherAvatar.spell.addBuff(buffID, count);
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void AddDiRenHP(int count = 10000)
	{
		PlayerEx.Player.OtherAvatar.HP_Max += count;
		PlayerEx.Player.OtherAvatar.HP = PlayerEx.Player.OtherAvatar.HP_Max;
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
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

	public void OpenHuaShen()
	{
		GlobalValue.SetTalk(0, 605, "RuntimeTestCode.OpenHuaShen");
		UIHuaShenRuDaoSelect.Inst.Show();
	}

	public void TestDiMo(int 敌人ID = 5040)
	{
		Tools.instance.startFight(敌人ID);
	}

	public void NPCAddGongXian(int npcid = 0, int gongxian = 10)
	{
		if (npcid == 0)
		{
			NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(UINPCJiaoHu.Inst.NowJiaoHuNPC.ID, gongxian);
		}
		else
		{
			NpcJieSuanManager.inst.npcSetField.NpcAddGongXian(npcid, gongxian);
		}
	}

	public void TianJiDaBiRollNPC(int rollCount = 48)
	{
		TianJiDaBiManager.RollDaBiPlayer(rollCount);
	}

	public void SimDaBi48()
	{
		TianJiDaBiManager.OnTimeSimDaBi();
	}

	public void LogLastDaBi()
	{
		Debug.Log((object)JsonConvert.SerializeObject((object)PlayerEx.Player.StreamData.TianJiDaBiSaveData.LastMatch));
	}

	public void StartDaBi(bool playerJoin = true)
	{
		TianJiDaBiManager.CmdTianJiDaBiStart(playerJoin);
	}

	public void OpenSaiChangUI()
	{
		UITianJiDaBiSaiChang.ShowNormal();
	}

	public void RecordPlayerFight(bool playerWin = true)
	{
		CmdTianJiDaBiRecordPlayer.Do(playerWin);
	}

	public void NewRound()
	{
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		nowMatch.NewRound();
		nowMatch.AfterRound();
	}

	public void LogMatchPlayers()
	{
		TianJiDaBiManager.GetNowMatch().LogPlayerRecord();
	}

	public void OpenTianJiRank()
	{
		UITianJiDaBiRankPanel.Show();
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void ScanStaticValue()
	{
		StringBuilder stringBuilder = new StringBuilder();
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.Log((object)"必须先开始游戏才能使用此功能");
			return;
		}
		int num = player.StaticValue.Value.Length;
		stringBuilder.AppendLine("=====StaticValue=====");
		for (int i = 0; i < num; i++)
		{
			if (player.StaticValue.Value[i] > 0)
			{
				stringBuilder.AppendLine($"{i}:{player.StaticValue.Value[i]}");
			}
		}
		stringBuilder.AppendLine("=====Talk=====");
		int num2 = player.StaticValue.talk.Length;
		for (int j = 0; j < num2; j++)
		{
			stringBuilder.AppendLine($"{j}:{player.StaticValue.talk[j]}");
		}
		Debug.Log((object)$"全局变量情况:\n{stringBuilder}");
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void ScanSceneStaticValue()
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		Flowchart[] array2 = array;
		foreach (Flowchart flowchart in array2)
		{
			SetStaticValue[] components = ((Component)flowchart).GetComponents<SetStaticValue>();
			SetStaticValueByVar[] components2 = ((Component)flowchart).GetComponents<SetStaticValueByVar>();
			GetStaticValue[] components3 = ((Component)flowchart).GetComponents<GetStaticValue>();
			num += components.Length;
			num += components2.Length;
			SetStaticValue[] array3 = components;
			foreach (SetStaticValue setStaticValue in array3)
			{
				Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{setStaticValue.ParentBlock.BlockName}拥有ID为{setStaticValue.StaticValueID}的SetStaticValue,Value:{setStaticValue.value}");
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
			SetStaticValueByVar[] array4 = components2;
			foreach (SetStaticValueByVar setStaticValueByVar in array4)
			{
				Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{setStaticValueByVar.ParentBlock.BlockName}拥有ID为{setStaticValueByVar.StaticValueID.Value}的SetStaticValueByVar");
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
			GetStaticValue[] array5 = components3;
			foreach (GetStaticValue getStaticValue in array5)
			{
				Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{getStaticValue.ParentBlock.BlockName}拥有ID为{getStaticValue.StaticValueID}的GetStaticValue");
				if (!list.Contains(flowchart))
				{
					list.Add(flowchart);
				}
			}
		}
		if (num == 0)
		{
			Debug.Log((object)"此场景中没有全局变量的操作");
		}
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void CheckSetStaticValue(int StaticValueID = 1)
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		Flowchart[] array2 = array;
		foreach (Flowchart flowchart in array2)
		{
			SetStaticValue[] components = ((Component)flowchart).GetComponents<SetStaticValue>();
			SetStaticValueByVar[] components2 = ((Component)flowchart).GetComponents<SetStaticValueByVar>();
			num += components.Length;
			num += components2.Length;
			SetStaticValue[] array3 = components;
			foreach (SetStaticValue setStaticValue in array3)
			{
				if (setStaticValue.StaticValueID == StaticValueID)
				{
					Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{setStaticValue.ParentBlock.BlockName}拥有ID为{StaticValueID}的SetStaticValue,Value:{setStaticValue.value}");
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
			SetStaticValueByVar[] array4 = components2;
			foreach (SetStaticValueByVar setStaticValueByVar in array4)
			{
				if (setStaticValueByVar.StaticValueID.Value == StaticValueID)
				{
					Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{setStaticValueByVar.ParentBlock.BlockName}拥有ID为{StaticValueID}的SetStaticValueByVar");
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.Log((object)$"{num}个SetStaticValue中没有找到StaticValueID为{StaticValueID}的指令");
		}
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void CheckGetStaticValue(int StaticValueID = 1)
	{
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		List<Flowchart> list = new List<Flowchart>();
		int num = 0;
		Flowchart[] array2 = array;
		foreach (Flowchart flowchart in array2)
		{
			GetStaticValue[] components = ((Component)flowchart).GetComponents<GetStaticValue>();
			num += components.Length;
			GetStaticValue[] array3 = components;
			foreach (GetStaticValue getStaticValue in array3)
			{
				if (getStaticValue.StaticValueID == StaticValueID)
				{
					Debug.Log((object)$"Chart:{flowchart.GetParentName()},Block:{getStaticValue.ParentBlock.BlockName}拥有ID为{StaticValueID}的GetStaticValue");
					if (!list.Contains(flowchart))
					{
						list.Add(flowchart);
					}
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.Log((object)$"{num}个GetStaticValue中没有找到StaticValueID为{StaticValueID}的指令");
		}
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void CheckStaticValue(int id)
	{
		int num = GlobalValue.Get(id, "RuntimeTestCode.CheckStaticValue");
		Debug.Log((object)$"全局变量{id}的值为:{num}");
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void SetStaticValue(int id, int value)
	{
		GlobalValue.Set(id, value, "RuntimeTestCode.SetStaticValue");
	}

	[ConsoleCommand(/*Could not decode attribute arguments.*/)]
	public static void SetStaticValueTalk(int id, int value)
	{
		GlobalValue.SetTalk(id, value, "RuntimeTestCode.SetStaticValueTalk");
	}

	public void RefreshPlayerAvatar()
	{
		PlayerAvatar = PlayerEx.Player;
	}

	public void WeiLaiTest(int npcId)
	{
		IExchangeUIMag.Open();
	}

	public void WeiLaiTest2(int id)
	{
		foreach (RandomExchangeData data in RandomExchangeData.DataList)
		{
			if (HasRepeat(data.YiWuFlag))
			{
				Debug.LogError((object)("YiWuFlag重复 key为：" + data.id));
			}
			if (HasRepeat(data.YiWuItem))
			{
				Debug.LogError((object)("YiWuItem重复 key为：" + data.id));
			}
		}
	}

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

	public void CheckCanClick()
	{
		Tools.instance.canClick(show: true);
	}

	public void CheckWuDaoError()
	{
		UINPCData.CheckWuDaoError();
	}

	public void CreateLuanLiuMap()
	{
		if (PlayerEx.Player == null)
		{
			Debug.LogError((object)"要生成乱流，请先载入存档");
			return;
		}
		Debug.Log((object)"开始生成乱流，请耐心等待...");
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		PlayerEx.Player.seaNodeMag.CreateLuanLiuMap();
		stopwatch.Stop();
		Debug.Log((object)$"生成完毕，共耗时{(float)stopwatch.ElapsedMilliseconds / 1000f}s");
	}

	public void TuJianHylink(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}

	public void StartTianJieCD()
	{
		TianJieManager.StartTianJieCD();
	}

	public void TianJieJiaSu(int year)
	{
		TianJieManager.TianJieJiaSu(year);
	}

	public void SenPopTip(string msg = "消息", int count = 3, PopTipIconType iconType = PopTipIconType.叹号)
	{
		for (int i = 0; i < count; i++)
		{
			UIPopTip.Inst.Pop(msg, iconType);
		}
	}

	public void SenPopTipOld(string msg = "消息")
	{
		UI_ErrorHint._instance.errorShow(msg);
	}

	public void OpenMap(MapArea area, UIMapState state)
	{
		UIMapPanel.Inst.OpenMap(area, state);
	}

	public void OpenMapHighlight(int highlight = 1)
	{
		UIMapPanel.Inst.OpenHighlight(highlight);
	}

	public void OpenXiuChuan()
	{
		UIXiuChuanPanel.OpenDefaultXiuChuan();
	}

	public void ChangeTitle(string title)
	{
		GameWindowTitle.Inst.SetTitle(title);
	}

	public void UnlockShenXianDouFa(int index)
	{
		Avatar.UnlockShenXianDouFa(index);
	}

	public void OpenMiniShop(int itemID = 5001, int price = 100, int maxSellCount = 10000)
	{
		UIMiniShop.Show(itemID, price, maxSellCount);
	}

	public void LogGanYingLuaScript()
	{
		Debug.Log((object)(RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor).checkSucessScript);
	}

	public void SetGanYingLuaScript(string lua)
	{
		TianJieMiShuLingWuFightEventProcessor tianJieMiShuLingWuFightEventProcessor = RoundManager.instance.PlayerFightEventProcessor as TianJieMiShuLingWuFightEventProcessor;
		tianJieMiShuLingWuFightEventProcessor.checkSucessScript = lua;
		Debug.Log((object)("设置了" + tianJieMiShuLingWuFightEventProcessor.checkSucessScript));
	}

	public void StartTianJieGanYing(WuDaoType wuDaoType = WuDaoType.剑)
	{
		CmdSetHuaShenLingYuSkill.Do(wuDaoType);
		Avatar player = PlayerEx.Player;
		int i = player.HuaShenLingYuSkill.I;
		int i2 = player.HuaShenWuDao.I;
		Debug.Log((object)$"当前化神悟道:{(WuDaoType)i2} 当前化神领域技能ID:{i}");
	}

	public void StartTianJieGanYing(string MiShuID = "盾")
	{
		TianJieMiShuLingWuFightEventProcessor.MiShuID = MiShuID;
		StartFight.Do(10010, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.天劫秘术领悟, 0, 0, 0, 0, "战斗3", SeaRemoveNPCFlag: false, "", new List<StarttFightAddBuff>(), new List<StarttFightAddBuff>());
	}

	public void OpenDuJieZhunBei(bool isDuJie)
	{
		UIDuJieZhunBei.OpenPanel(isDuJie);
	}

	public void StartDuJieFight()
	{
		StartFight.Do(9999, 1, StartFight.MonstarType.Normal, StartFight.FightEnumType.FeiSheng, 0, 0, 0, 0, "战斗3", SeaRemoveNPCFlag: false, "", new List<StarttFightAddBuff>(), new List<StarttFightAddBuff>());
	}

	public void SetDuJieLeiJie(TestTianJieType type)
	{
		for (int i = 0; i < TianJieManager.Inst.LeiJieList.Count; i++)
		{
			TianJieManager.Inst.LeiJieList[i] = type.ToString();
		}
	}

	public void LogShengPing()
	{
		List<ShengPingData> shengPingList = PlayerEx.GetShengPingList();
		string text = $"生平信息:共{shengPingList.Count}条\n";
		foreach (ShengPingData item in shengPingList)
		{
			text += $"{item}\n";
		}
		Debug.Log((object)text);
	}

	public void OpenJianLingPanel()
	{
		UIJianLingPanel.OpenPanel();
	}

	public void OpenJianLingPanel2()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingPanel"), ((Component)NewUICanvas.Inst.Canvas).transform).transform.SetAsLastSibling();
	}

	public void SaveNewSave(int slot = 1)
	{
		if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null && AuToSLMgr.Inst.CanSave())
		{
			if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			if (jsonData.instance.saveState == 1)
			{
				UIPopTip.Inst.Pop("存档未完成,请稍等");
			}
			else
			{
				YSNewSaveSystem.SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), slot);
			}
		}
	}

	public void LoadNewSave(int slot = 1)
	{
		YSNewSaveSystem.LoadSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), slot);
	}

	public void ZipNewSave(int slot = 0)
	{
		YSZip.ZipFile(Paths.GetNewSavePath(), $"{Paths.GetCloudSavePath()}/CloudSave_{slot}.zip");
	}

	public void UnZipNewSave(int slot = 0)
	{
		YSZip.UnZipFile($"{Paths.GetCloudSavePath()}/CloudSave_{slot}.zip", Paths.GetNewSavePath() ?? "");
	}

	public void UploadSave(int slot = 0)
	{
		YSNewSaveSystem.UploadCloudSaveData(slot);
	}

	public void DownloadSave(int slot = 0)
	{
		YSNewSaveSystem.DownloadCloudSave(slot);
	}

	public void OpenSaveFolder()
	{
		Process.Start(Paths.GetNewSavePath());
	}

	public void OpenCloudSaveFolder()
	{
		Process.Start(Paths.GetCloudSavePath());
	}

	public void LogCloudFiles()
	{
		YSNewSaveSystem.LogCloudFiles();
	}
}
