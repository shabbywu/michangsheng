using System;
using System.Collections.Generic;
using System.Linq;
using CaiJi;
using JSONClass;
using KBEngine;
using UnityEngine;
using YSGame.TianJiDaBi;
using script.MenPaiTask;

public class NpcJieSuanManager : MonoBehaviour
{
	public static NpcJieSuanManager inst;

	public NpCFight npcFight;

	private Random random;

	public NPCXiuLian npcXiuLian;

	public NPCTuPo npcTuPo;

	public NpcSetField npcSetField;

	public NPCShouJi npcShouJi;

	public NPCFuYe npcFuYe;

	public NPCUseItem npcUseItem;

	public NPCLiLian npcLiLian;

	public NPCStatus npcStatus;

	public NpcTianJiGe npcTianJiGe;

	public NPCTeShu npcTeShu;

	public NPCNoteBook npcNoteBook;

	public NPCMap npcMap;

	public NPCSpeedJieSuan npcSpeedJieSuan;

	public NPCDeath npcDeath;

	public NPCChengHao npcChengHao;

	public Dictionary<int, List<List<int>>> cyDictionary = new Dictionary<int, List<List<int>>>();

	public List<CyPdData> cyPdAuToList;

	public List<CyPdData> cyPdFungusList;

	public Dictionary<int, Action<int>> ActionDictionary = new Dictionary<int, Action<int>>();

	public Dictionary<int, Action<int>> NextActionDictionary = new Dictionary<int, Action<int>>();

	public List<int> CurJieSuanNpcTaskList = new List<int>();

	public Dictionary<int, int> ImportantNpcBangDingDictionary = new Dictionary<int, int>();

	private Dictionary<int, int> NpcActionQuanZhongDictionary = new Dictionary<int, int>();

	public Dictionary<int, List<int>> PaiMaiNpcDictionary = new Dictionary<int, List<int>>();

	public List<int> allBigMapNpcList = new List<int>();

	public List<int> JieShaNpcList = new List<int>();

	public List<EmailData> lateEmailList = new List<EmailData>();

	public Dictionary<int, EmailData> lateEmailDict = new Dictionary<int, EmailData>();

	public List<int> lunDaoNpcList = new List<int>();

	public List<List<int>> afterDeathList = new List<List<int>>();

	[HideInInspector]
	public List<string> EquipNameList = new List<string>();

	public bool isUpDateNpcList;

	public bool isCanJieSuan = true;

	public bool JieSuanAnimation;

	public int JieSuanTimes;

	public string JieSuanTime = "0001-1-1";

	public bool IsNoJieSuan;

	private void Awake()
	{
		if ((Object)(object)inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)inst).gameObject);
		}
		inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)inst).gameObject);
		jsonData.instance.init("Effect/json/d_avatar.py.datas", out jsonData.instance.AvatarJsonData);
		random = new Random();
		InitCyData();
	}

	public void InitCyData()
	{
		cyPdAuToList = new List<CyPdData>();
		cyPdFungusList = new List<CyPdData>();
		foreach (JSONObject item3 in jsonData.instance.CyNpcSendData.list)
		{
			CyPdData cyPdData = new CyPdData();
			cyPdData.id = item3["id"].I;
			cyPdData.npcActionList = item3["NPCXingWei"].ToList();
			cyPdData.npcType = item3["NPCshenfen"].I;
			cyPdData.minLevel = item3["NPCLevel"][0].I;
			cyPdData.maxLevel = item3["NPCLevel"][1].I;
			if (item3["EventValue"].Count == 2)
			{
				cyPdData.staticId = item3["EventValue"][0].I;
				cyPdData.staticId = item3["EventValue"][1].I;
				cyPdData.SetStaticFuHao(item3["fuhao1"].Str);
			}
			if (item3["HaoGanDu"].I > 0)
			{
				cyPdData.needHaoGanDu = item3["HaoGanDu"].I;
				cyPdData.SetHaoGanFuHao(item3["fuhao2"].Str);
			}
			if (item3["StarTime"].Str != "")
			{
				cyPdData.startTime = item3["StarTime"].Str;
				cyPdData.endTime = item3["EndTime"].Str;
			}
			cyPdData.npcState = item3["ZhuangTaiInfo"].I;
			cyPdData.isOnly = item3["IsOnly"].I == 1;
			cyPdData.cyType = item3["XiaoXiType"].I;
			cyPdData.baseRate = item3["Rate"].I;
			cyPdData.actionId = item3["XingWeiType"].I;
			cyPdData.qingFen = item3["QingFen"].I;
			if (cyPdData.actionId != 0)
			{
				cyPdData.itemPrice = item3["ItemJiaGe"].I;
			}
			cyPdData.outTime = item3["GuoQiShiJian"].I;
			cyPdData.addHaoGan = item3["HaoGanDuChange"].I;
			cyPdData.talkId = item3["DuiBaiType"].I;
			if (cyPdData.actionId != 0)
			{
				for (int i = 0; i + 1 < item3["RandomItemID"].Count; i += 2)
				{
					if (cyDictionary.ContainsKey(cyPdData.id))
					{
						List<int> item = new List<int>
						{
							item3["RandomItemID"][i].I,
							item3["RandomItemID"][i + 1].I
						};
						cyDictionary[cyPdData.id].Add(item);
					}
					else
					{
						List<int> item2 = new List<int>
						{
							item3["RandomItemID"][i].I,
							item3["RandomItemID"][i + 1].I
						};
						cyDictionary.Add(cyPdData.id, new List<List<int>> { item2 });
					}
				}
			}
			if (item3["IsChuFa"].I == 1)
			{
				cyPdFungusList.Add(cyPdData);
			}
			else if (item3["IsChuFa"].I == 0)
			{
				cyPdAuToList.Add(cyPdData);
			}
		}
	}

	private void Start()
	{
		npcFight = new NpCFight();
		npcXiuLian = new NPCXiuLian();
		npcTuPo = new NPCTuPo();
		npcTianJiGe = new NpcTianJiGe();
		npcShouJi = new NPCShouJi();
		npcUseItem = new NPCUseItem();
		npcLiLian = new NPCLiLian();
		npcFuYe = new NPCFuYe();
		npcNoteBook = new NPCNoteBook();
		npcStatus = new NPCStatus();
		npcMap = new NPCMap();
		npcDeath = new NPCDeath();
		npcChengHao = new NPCChengHao();
		npcSetField = new NpcSetField();
		npcTeShu = new NPCTeShu();
		npcSpeedJieSuan = new NPCSpeedJieSuan();
		initNpcAction();
		EquipNameList.Add("equipWeaponPianHao");
		EquipNameList.Add("equipClothingPianHao");
		EquipNameList.Add("equipRingPianHao");
		EquipNameList.Add("equipWeapon2PianHao");
	}

	public DateTime GetNowTime()
	{
		return DateTime.Parse(JieSuanTime);
	}

	public void initNpcAction()
	{
		ActionDictionary.Add(1, DoNothing);
		ActionDictionary.Add(2, npcXiuLian.NpcBiGuan);
		ActionDictionary.Add(3, npcShouJi.NpcCaiYao);
		ActionDictionary.Add(4, npcShouJi.NpcCaiKuang);
		ActionDictionary.Add(5, npcFuYe.NpcLianDan);
		ActionDictionary.Add(6, npcFuYe.NpcLianQi);
		ActionDictionary.Add(7, npcXiuLian.NpcXiuLianShenTong);
		ActionDictionary.Add(8, npcShouJi.NpcBuyMiJi);
		ActionDictionary.Add(9, npcShouJi.NpcBuyFaBao);
		ActionDictionary.Add(10, npcXiuLian.NpcLunDao);
		ActionDictionary.Add(11, npcShouJi.NpcBuyDanYao);
		ActionDictionary.Add(30, npcLiLian.NPCNingZhouKillYaoShou);
		ActionDictionary.Add(31, npcLiLian.NPCDoZhuChengTask);
		ActionDictionary.Add(33, npcLiLian.NPCNingZhouYouLi);
		ActionDictionary.Add(34, npcTeShu.NpcToJieSha);
		ActionDictionary.Add(35, npcLiLian.NPCDoMenPaiTask);
		ActionDictionary.Add(36, npcShouJi.NpcShouJiTuPoItem);
		ActionDictionary.Add(37, npcShouJi.NpcSpeedDeath);
		NextActionDictionary.Add(36, npcShouJi.NextNpcShouJiTuPoItem);
		ActionDictionary.Add(50, npcTuPo.NpcBigTuPo);
		ActionDictionary.Add(41, npcLiLian.NPCHaiShangKillYaoShou);
		ActionDictionary.Add(42, npcLiLian.NpcHaiShangYouLi);
		ActionDictionary.Add(43, npcTeShu.NpcSuiXingDaoJinHuo);
		ActionDictionary.Add(44, npcTeShu.NpcToGangKou);
		ActionDictionary.Add(45, npcFuYe.NpcCreateZhenQi);
		ActionDictionary.Add(51, npcTeShu.NpcToDongShiGuShiFang);
		ActionDictionary.Add(52, npcTeShu.NpcToTianXingShiFang);
		ActionDictionary.Add(53, npcTeShu.NpcToHaiShangShiFang);
		NextActionDictionary.Add(51, npcTeShu.NextNpcShiFang);
		NextActionDictionary.Add(52, npcTeShu.NextNpcShiFang);
		NextActionDictionary.Add(53, npcTeShu.NextNpcShiFang);
		ActionDictionary.Add(54, npcTeShu.NpcToDongShiGuPaiMai);
		ActionDictionary.Add(55, npcTeShu.NpcToTianJiGePaiMai);
		ActionDictionary.Add(56, npcTeShu.NpcToHaiShangPaiMai);
		ActionDictionary.Add(57, npcTeShu.NpcToNanYaChengPaiMai);
		ActionDictionary.Add(100, npcXiuLian.NpcBiGuan);
		ActionDictionary.Add(101, npcTeShu.NpcToGuangChang);
		ActionDictionary.Add(102, npcTeShu.NpcZhangLaoToDaDian);
		ActionDictionary.Add(103, npcTeShu.NpcZhangMenToDaDian);
		ActionDictionary.Add(111, npcTianJiGe.TianJiGePaoShang);
		ActionDictionary.Add(112, npcTianJiGe.TianJiGeJinHuo);
		ActionDictionary.Add(113, npcTeShu.NpcFriendToDongFu);
		ActionDictionary.Add(114, npcTeShu.NpcDaoLuToDongFu);
		ActionDictionary.Add(115, npcTeShu.NpcToSuiXingShop);
		ActionDictionary.Add(116, npcTeShu.NpcToSuiXingFaZhan);
		ActionDictionary.Add(121, npcShouJi.NpcToLingHe1);
		ActionDictionary.Add(122, npcShouJi.NpcToLingHe2);
		ActionDictionary.Add(123, npcShouJi.NpcToLingHe3);
		ActionDictionary.Add(124, npcShouJi.NpcToLingHe4);
		ActionDictionary.Add(125, npcShouJi.NpcToLingHe5);
		ActionDictionary.Add(126, npcShouJi.NpcToLingHe6);
	}

	public void NpcJieSuan(int times, bool isCanChanger = true)
	{
		int num = 0;
		foreach (int cyNpc in PlayerEx.Player.emailDateMag.cyNpcList)
		{
			_ = cyNpc;
			if (!NPCEx.IsDeath(num))
			{
				num++;
			}
		}
		if (IsNoJieSuan)
		{
			IsNoJieSuan = false;
			return;
		}
		isCanJieSuan = false;
		DateTime tempTime = DateTime.Parse(JieSuanTime).AddMonths(times);
		int actionTimes = 0;
		if (isCanChanger)
		{
			actionTimes = SystemConfig.Inst.GetNpcActionTimes();
		}
		Loom.RunAsync(delegate
		{
			try
			{
				if (GameVersion.inst.realTest || actionTimes == 2)
				{
					while (times > 0)
					{
						times--;
						RandomNpcAction();
						JieSuanTimes++;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
					}
				}
				else if (times > 120)
				{
					int num2 = 12;
					if (actionTimes == 1)
					{
						num2 *= 2;
					}
					int num3 = times - num2;
					int num4 = num3 / num2;
					num2--;
					while (num3 >= num4)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num4);
						JieSuanTimes += num4;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num4).ToString();
						num3 -= num4;
						if (num2 > 0)
						{
							RandomNpcAction();
							JieSuanTimes++;
							JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
							num2--;
						}
					}
					if (num3 > 0)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num3);
						JieSuanTimes += num3;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num3).ToString();
					}
					for (int i = 0; i < num2; i++)
					{
						RandomNpcAction();
						JieSuanTimes++;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
					}
					RandomNpcAction();
					JieSuanTimes++;
					JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
				}
				else if (times > 12 && times <= 120)
				{
					int num5 = 6;
					if (actionTimes == 1)
					{
						num5 *= 2;
					}
					int num6 = times - num5;
					int num7 = num6 / num5;
					num5--;
					while (num7 > 0 && num6 >= num7)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num7);
						JieSuanTimes += num7;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num7).ToString();
						num6 -= num7;
						if (num5 > 0)
						{
							RandomNpcAction();
							JieSuanTimes++;
							JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
							num5--;
						}
					}
					if (num6 > 0)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num6);
						JieSuanTimes += num6;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num6).ToString();
					}
					for (int j = 0; j < num5; j++)
					{
						RandomNpcAction();
						JieSuanTimes++;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
					}
					RandomNpcAction();
					JieSuanTimes++;
					JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
				}
				else if (times > 6 && times <= 12)
				{
					int num8 = times / 2;
					if (actionTimes == 1)
					{
						num8 *= 2;
						num8--;
					}
					int num9 = times - num8;
					int num10 = num9 / num8;
					num8--;
					while (num10 > 0 && num9 >= num10)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num10);
						JieSuanTimes += num10;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num10).ToString();
						num9 -= num10;
						if (num8 > 0)
						{
							RandomNpcAction();
							JieSuanTimes++;
							JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
							num8--;
						}
					}
					if (num9 > 0)
					{
						npcSpeedJieSuan.DoSpeedJieSuan(num9);
						JieSuanTimes += num9;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(num9).ToString();
					}
					for (int k = 0; k < num8; k++)
					{
						RandomNpcAction();
						JieSuanTimes++;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
					}
					RandomNpcAction();
					JieSuanTimes++;
					JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
				}
				else
				{
					while (times > 0)
					{
						times--;
						RandomNpcAction();
						JieSuanTimes++;
						JieSuanTime = DateTime.Parse(JieSuanTime).AddMonths(1).ToString();
					}
				}
				while (DateTime.Parse(JieSuanTime) >= DateTime.Parse(Tools.instance.getPlayer().NextCreateTime))
				{
					Tools.instance.getPlayer().NextCreateTime = DateTime.Parse(Tools.instance.getPlayer().NextCreateTime).AddYears(20).ToString();
					FactoryManager.inst.npcFactory.AuToCreateNpcs();
				}
				for (int l = 0; l < afterDeathList.Count; l++)
				{
					npcDeath.SetNpcDeath(afterDeathList[l][0], afterDeathList[l][1], afterDeathList[l][2]);
				}
				TianJiDaBiManager.OnAddTime();
			}
			catch (Exception ex)
			{
				if (tempTime > DateTime.Parse(JieSuanTime))
				{
					JieSuanTime = tempTime.ToString();
				}
				Debug.LogError((object)"结算出错");
				Debug.LogException(ex);
				FixNpcData();
			}
			finally
			{
				afterDeathList = new List<List<int>>();
				try
				{
					Tools.instance.getPlayer().setMonstarDeath();
				}
				catch (Exception ex2)
				{
					Debug.LogError((object)ex2);
					Debug.LogError((object)"设置Npc死亡出错");
				}
				CurJieSuanNpcTaskList = new List<int>();
				isUpDateNpcList = true;
				isCanJieSuan = true;
				JieSuanAnimation = true;
				Loom.QueueOnMainThread(delegate
				{
					MessageMag.Instance.Send("MSG_Npc_JieSuan_COMPLETE");
					if (PlayerEx.Player.TianJie != null && PlayerEx.Player.TianJie.HasField("ShowTianJieCD"))
					{
						TianJieManager.OnAddTime();
					}
				}, null);
			}
		});
	}

	public void FixNpcData()
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		foreach (string key in avatarJsonData.keys)
		{
			if (int.Parse(key) < 20000)
			{
				continue;
			}
			if (!jsonData.instance.AvatarRandomJsonData.HasField(key))
			{
				if (avatarJsonData[key]["isImportant"].b)
				{
					jsonData.instance.AvatarRandomJsonData.SetField(avatarJsonData[key]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[avatarJsonData[key]["BindingNpcID"].I.ToString()]);
				}
				else
				{
					JSONObject jSONObject = jsonData.instance.randomAvatarFace(avatarJsonData[key]);
					jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(avatarJsonData[key]["id"].I), jSONObject.Copy());
				}
			}
			if (!jsonData.instance.AvatarBackpackJsonData.HasField(key))
			{
				FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, avatarJsonData[key]["id"].I, avatarJsonData[key]);
			}
		}
	}

	public void PaiMaiAction()
	{
		if (PaiMaiNpcDictionary.Count > 0)
		{
			npcTeShu.NextNpcPaiMai();
			PaiMaiNpcDictionary = new Dictionary<int, List<int>>();
		}
	}

	public void LunDaoAction()
	{
		if (lunDaoNpcList.Count >= 2)
		{
			npcXiuLian.NextNpcLunDao();
		}
		else
		{
			lunDaoNpcList = new List<int>();
		}
	}

	public void RandomNpcAction()
	{
		PaiMaiAction();
		LunDaoAction();
		npcTeShu.NextJieSha();
		Avatar player = Tools.instance.getPlayer();
		npcMap.RestartMap();
		if (NpcActionQuanZhongDictionary.Count < 1)
		{
			foreach (string key in jsonData.instance.NPCActionDate.keys)
			{
				NpcActionQuanZhongDictionary.Add(int.Parse(key), jsonData.instance.NPCActionDate[key]["QuanZhong"].I);
			}
		}
		List<int> list = new List<int>();
		new Dictionary<int, int>();
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		List<string> keys = jsonData.instance.AvatarJsonData.keys;
		int num = 0;
		for (int i = 0; i < keys.Count; i++)
		{
			string text = keys[i];
			int num2 = int.Parse(text);
			if (num2 < 20000 || avatarJsonData[text].HasField("IsFly"))
			{
				continue;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>(NpcActionQuanZhongDictionary);
			dictionary = getFinallyNpcActionQuanZhongDictionary(avatarJsonData[text], dictionary);
			num = getRandomActionID(dictionary);
			if (!ActionDictionary.ContainsKey(num) || npcDeath.NpcYiWaiPanDing(num, num2) || !npcSetField.AddNpcAge(num2, 1))
			{
				continue;
			}
			npcStatus.ReduceStatusTime(num2, 1);
			int i2 = avatarJsonData[num2.ToString()]["ActionId"].I;
			if (NextActionDictionary.ContainsKey(i2))
			{
				NextActionDictionary[i2](num2);
			}
			_ = avatarJsonData[num2.ToString()]["Status"]["StatusId"].I;
			if (avatarJsonData[num2.ToString()]["Status"]["StatusId"].I == 20)
			{
				npcTeShu.NpcFriendToDongFu(num2);
				avatarJsonData[num2.ToString()].SetField("ActionId", 113);
				num = 113;
			}
			else if (avatarJsonData[num2.ToString()]["Status"]["StatusId"].I == 21)
			{
				npcTeShu.NpcDaoLuToDongFu(num2);
				num = 114;
				avatarJsonData[num2.ToString()].SetField("ActionId", 114);
			}
			else if (Tools.instance.getPlayer().ElderTaskMag.GetExecutingTaskNpcIdList().Contains(num2))
			{
				num = 35;
			}
			else
			{
				if (avatarJsonData[num2.ToString()]["isImportant"].b && avatarJsonData[num2.ToString()].HasField("BindingNpcID"))
				{
					if (!ImprotantNpcActionPanDing(num2))
					{
						avatarJsonData[num2.ToString()].SetField("ActionId", num);
						ActionDictionary[num](num2);
						avatarJsonData[num2.ToString()].SetField("IsNeedHelp", IsNeedHelp());
					}
				}
				else
				{
					avatarJsonData[num2.ToString()].SetField("ActionId", num);
					ActionDictionary[num](num2);
					avatarJsonData[num2.ToString()].SetField("IsNeedHelp", IsNeedHelp());
				}
				if (num == 35 && !list.Contains(num2))
				{
					list.Add(num2);
				}
				SendMessage(num2);
				SendCy(num2);
			}
			GuDingAddExp(num2);
		}
		if (!player.emailDateMag.IsStopAll)
		{
			if (lateEmailList.Count > 0)
			{
				foreach (EmailData lateEmail in lateEmailList)
				{
					player.emailDateMag.AddNewEmail(lateEmail.npcId.ToString(), lateEmail);
				}
				lateEmailList = new List<EmailData>();
			}
			if (lateEmailDict.Keys.Count > 0)
			{
				List<int> list2 = new List<int>();
				foreach (int key2 in lateEmailDict.Keys)
				{
					EmailData emailData = lateEmailDict[key2];
					if (emailData.RandomTask != null)
					{
						DateTime dateTime = DateTime.Parse(emailData.sendTime);
						if (GetNowTime() < dateTime)
						{
							continue;
						}
						RandomTask randomTask = emailData.RandomTask;
						if (randomTask.TaskId != 0)
						{
							player.StreamData.TaskMag.AddTask(randomTask.TaskId, randomTask.TaskType, randomTask.CyId, emailData.npcId, randomTask.TaskValue, dateTime);
							if (randomTask.TaskType == 1 && randomTask.LockActionId > 0)
							{
								GetNpcData(emailData.npcId).SetField("ActionId", randomTask.LockActionId);
								GetNpcData(emailData.npcId).SetField("LockAction", randomTask.LockActionId);
							}
						}
						if (randomTask.TaskValue != 0)
						{
							GlobalValue.Set(randomTask.TaskValue, emailData.npcId, "NpcJieSuanManager.RandomNpcAction 传音符相关全局变量A");
						}
						if (randomTask.StaticId.Count > 0)
						{
							for (int j = 0; j < randomTask.StaticId.Count; j++)
							{
								GlobalValue.Set(randomTask.StaticId[j], randomTask.StaticValue[j], "NpcJieSuanManager.RandomNpcAction 传音符相关全局变量B");
							}
						}
					}
					list2.Add(key2);
					player.emailDateMag.AddNewEmail(emailData.npcId.ToString(), emailData);
				}
				foreach (int item in list2)
				{
					lateEmailDict.Remove(item);
				}
			}
		}
		Tools.instance.getPlayer().StreamData.TaskMag.CheckHasOut();
		CheckMenPaiTask();
		Tools.instance.getPlayer().ElderTaskMag.UpdateTaskProcess.CheckHasExecutingTask();
		foreach (int item2 in list)
		{
			Tools.instance.getPlayer().ElderTaskMag.AddCanAccpetNpcIdList(item2);
		}
		Tools.instance.getPlayer().ElderTaskMag.AllotTask.GetCanAccpetNpcList();
	}

	public void CheckMenPaiTask()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.menPai <= 0)
		{
			return;
		}
		int zhangMenId = GetZhangMenId(player.menPai);
		Dictionary<int, MenPaiFengLuBiao> dataDict = MenPaiFengLuBiao.DataDict;
		if (player.chengHao >= 6 && player.chengHao <= 9 && PlayerEx.GetMenPaiShengWang() < dataDict[player.chengHao].MenKan)
		{
			player.chengHao = 5;
			PlayerEx.SetShiLiChengHaoLevel(player.menPai, 6);
			player.AddFriend(zhangMenId);
			player.emailDateMag.AuToSendToPlayer(zhangMenId, 996, 996, inst.JieSuanTime);
			return;
		}
		MenPaiTaskMag menPaiTaskMag = player.StreamData.MenPaiTaskMag;
		if (menPaiTaskMag.CheckNeedSend())
		{
			player.AddFriend(zhangMenId);
			menPaiTaskMag.SendTask(zhangMenId);
		}
	}

	private int GetZhangMenId(int shili)
	{
		int num = Tools.instance.getPlayer().GetZhangMenChengHaoId(shili);
		if (Tools.instance.getPlayer().menPai == 6)
		{
			num--;
		}
		int num2 = 0;
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			num2 = item["id"].I;
			if (num2 >= 20000 && item["ChengHaoID"].I == num)
			{
				return num2;
			}
		}
		Debug.LogError((object)$"不存在当前势力的掌门，势力Id{shili}");
		return -1;
	}

	public Dictionary<int, int> getFinallyNpcActionQuanZhongDictionary(JSONObject npcDate, Dictionary<int, int> dictionary)
	{
		int i = npcDate["NPCTag"].I;
		NPCTagDate nPCTagDate = NPCTagDate.DataDict[i];
		for (int j = 0; j < nPCTagDate.Change.Count; j++)
		{
			if (dictionary.ContainsKey(nPCTagDate.Change[j]))
			{
				if (nPCTagDate.Change[j] == 35 && Tools.instance.getPlayer().ElderTaskMag.GetWaitAcceptTaskList().Count > 0)
				{
					dictionary[nPCTagDate.Change[j]] += nPCTagDate.ChangeTo[j];
				}
				dictionary[nPCTagDate.Change[j]] += nPCTagDate.ChangeTo[j];
				if (dictionary[nPCTagDate.Change[j]] < 0)
				{
					dictionary[nPCTagDate.Change[j]] = 0;
				}
			}
		}
		NPCChengHaoData nPCChengHaoData = NPCChengHaoData.DataDict[npcDate["ChengHaoID"].I];
		if (nPCChengHaoData.ChengHao != npcDate["Title"].Str)
		{
			foreach (NPCChengHaoData data in NPCChengHaoData.DataList)
			{
				if (data.ChengHao == npcDate["Title"].Str)
				{
					npcDate.SetField("ChengHaoID", data.id);
					break;
				}
			}
		}
		for (int k = 0; k < nPCChengHaoData.Change.Count; k++)
		{
			dictionary[nPCChengHaoData.Change[k]] += nPCChengHaoData.ChangeTo[k];
			if (dictionary[nPCChengHaoData.Change[k]] < 0)
			{
				dictionary[nPCChengHaoData.Change[k]] = 0;
			}
		}
		List<int> list = new List<int>();
		foreach (int key2 in dictionary.Keys)
		{
			list.Add(key2);
		}
		for (int l = 0; l < list.Count; l++)
		{
			int key = list[l];
			int i2 = jsonData.instance.NPCActionDate[key.ToString()]["PanDing"].I;
			switch (i2)
			{
			case 1:
				if (npcTuPo.IsCanBigTuPo(npcDate["id"].I))
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 2:
			{
				int month = GetNowTime().Month;
				if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && month >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][0].I && month <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][1].I && npcDate["paimaifenzu"][getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			}
			case 3:
			case 4:
				if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 5:
			case 6:
			case 7:
			case 8:
				if (PaiMaiIsOpen(jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiTime"].I) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["LingShi"].I && npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 9:
				if (IsCanChangeEquip(npcDate) != 0)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 10:
				if (!IsCanLianDan(npcDate["id"].I))
				{
					dictionary[key] = 0;
				}
				break;
			case 11:
				if (!IsCanLianQi(npcDate["id"].I))
				{
					dictionary[key] = 0;
				}
				break;
			case 12:
				if (npcStatus.IsInTargetStatus(npcDate["id"].I, 2) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCTuPuoDate[npcDate["Level"].I.ToString()]["LingShiPanDuan"].I && !npcDate["isImportant"].b)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 13:
				if (npcStatus.IsInTargetStatus(npcDate["id"].I, 2))
				{
					dictionary[1] = 0;
				}
				break;
			case 14:
				if (GetNpcShengYuTime(npcDate["id"].I) < 10)
				{
					dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
				}
				break;
			case 15:
				if (npcDate["Level"].I == 1)
				{
					dictionary[key] = 0;
				}
				break;
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
				if (NPCActionPanDingDate.DataDict[i2].JingJie[0] > npcDate["Level"].I || NPCActionPanDingDate.DataDict[i2].JingJie[1] < npcDate["Level"].I || npcDate["paimaifenzu"][getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I != NPCActionPanDingDate.DataDict[i2].PaiMaiType)
				{
					break;
				}
				if (!npcMap.fuBenNPCDictionary.ContainsKey("F" + 26))
				{
					dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
					break;
				}
				foreach (int item in NPCActionPanDingDate.DataDict[i2].LingHeDianWei)
				{
					if (!npcMap.fuBenNPCDictionary["F" + 26].Keys.Contains(item) && (!((Object)(object)LingHeCaiJiUIMag.inst != (Object)null) || item != LingHeCaiJiUIMag.inst.nowMapIndex))
					{
						dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
						break;
					}
				}
				break;
			case 23:
				if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I)
				{
					dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
				}
				break;
			}
		}
		return dictionary;
	}

	public bool PaiMaiIsOpen(int paimaiHangID)
	{
		string str = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["StarTime"].str;
		string str2 = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["EndTime"].str;
		int i = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["circulation"].I;
		return Tools.instance.IsInTime(DateTime.Parse(JieSuanTime), DateTime.Parse(str), DateTime.Parse(str2), i);
	}

	public int IsCanChangeEquip(JSONObject npcDate)
	{
		int i = jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I;
		int num = (int)jsonData.instance.LianQiWuQiQuality[npcDate["Level"].I.ToString()][(object)"price"];
		int i2 = npcDate["Level"].I;
		int i3 = jsonData.instance.NpcLevelShouYiDate[i2.ToString()]["fabao"].I;
		if (i < num)
		{
			return 0;
		}
		if (!npcDate["equipList"].HasField("Weapon1"))
		{
			return 1;
		}
		if (npcDate["wuDaoSkillList"].ToList().Contains(2231))
		{
			if (!npcDate["equipList"].HasField("Weapon2"))
			{
				return 4;
			}
			int i4 = npcDate["equipList"]["Weapon1"]["quality"].I;
			int i5 = npcDate["equipList"]["Weapon2"]["quality"].I;
			if (i3 > i4)
			{
				return 1;
			}
			if (i3 > i5)
			{
				return 4;
			}
		}
		else
		{
			int i6 = npcDate["equipList"]["Weapon1"]["quality"].I;
			if (i3 > i6)
			{
				return 1;
			}
		}
		if (!npcDate["equipList"].HasField("Clothing"))
		{
			return 2;
		}
		int i7 = npcDate["equipList"]["Clothing"]["quality"].I;
		if (i3 > i7)
		{
			return 2;
		}
		if (i2 >= 7)
		{
			if (!npcDate["equipList"].HasField("Ring"))
			{
				return 3;
			}
			int i8 = npcDate["equipList"]["Ring"]["quality"].I;
			if (i3 > i8)
			{
				return 3;
			}
		}
		return 0;
	}

	public int GetChangeEquipWeapon(JSONObject npcDate)
	{
		int i = npcDate["Level"].I;
		int i2 = jsonData.instance.NpcLevelShouYiDate[i.ToString()]["fabao"].I;
		if (!npcDate["equipList"].HasField("Weapon1"))
		{
			return 1;
		}
		if (npcDate["wuDaoSkillList"].ToList().Contains(2231))
		{
			if (!npcDate["equipList"].HasField("Weapon2"))
			{
				return 4;
			}
			int i3 = npcDate["equipList"]["Weapon1"]["quality"].I;
			int i4 = npcDate["equipList"]["Weapon2"]["quality"].I;
			if (i2 > i3)
			{
				return 1;
			}
			if (i2 > i4)
			{
				return 4;
			}
		}
		else
		{
			int i5 = npcDate["equipList"]["Weapon1"]["quality"].I;
			if (i2 > i5)
			{
				return 1;
			}
		}
		return 0;
	}

	public int GetNpcBeiBaoAllItemSum(int npcID)
	{
		int num = 0;
		int index = inst.GetNpcBigLevel(npcID) - 1;
		NPCTuPuoDate nPCTuPuoDate = NPCTuPuoDate.DataList[index];
		List<int> list = new List<int>();
		foreach (int item in nPCTuPuoDate.ShouJiItem)
		{
			list.Add(item);
		}
		foreach (int item2 in nPCTuPuoDate.TuPoItem)
		{
			list.Add(item2);
		}
		foreach (JSONObject item3 in jsonData.instance.AvatarBackpackJsonData[string.Concat(npcID)]["Backpack"].list)
		{
			if (!list.Contains(item3["ItemID"].I) && item3["Num"].I > 0)
			{
				num += item3["Num"].I;
			}
		}
		return num;
	}

	public Dictionary<int, int> GetNpcBaiBaoItemSum(int npcId, List<int> itemList)
	{
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)]["Backpack"].list;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		new List<int>();
		foreach (JSONObject item in list)
		{
			if (itemList.Contains(item["ItemID"].I))
			{
				if (dictionary.ContainsKey(item["ItemID"].I))
				{
					dictionary[item["ItemID"].I] += item["Num"].I;
				}
				else
				{
					dictionary.Add(item["ItemID"].I, item["Num"].I);
				}
			}
		}
		return dictionary;
	}

	public int GetEquipLevel(int quality, int shangXia)
	{
		return quality * 3 - (3 - shangXia);
	}

	public int getRandomInt(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	public int getRandomActionID(Dictionary<int, int> dictionary)
	{
		int num = 0;
		foreach (int key in dictionary.Keys)
		{
			num += dictionary[key];
		}
		int randomInt = getRandomInt(1, num);
		int result = -1;
		int num2 = 0;
		foreach (int key2 in dictionary.Keys)
		{
			num2 += dictionary[key2];
			if (num2 >= randomInt)
			{
				result = key2;
				break;
			}
		}
		return result;
	}

	public JSONObject AddItemToNpcBackpack(int npcId, int itemID, int num, JSONObject seid = null, bool isPaiMai = false)
	{
		JSONObject jSONObject = jsonData.instance.setAvatarBackpack(Tools.getUUID(), itemID, num, 1, 100, 1, (seid == null) ? Tools.CreateItemSeid(itemID) : seid);
		if (isPaiMai && jSONObject["Seid"] != null)
		{
			jSONObject["Seid"].SetField("isPaiMai", val: true);
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].Add(jSONObject);
		return jSONObject;
	}

	public void RemoveNpcItem(int npcId, int itemId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		JSONObject jSONObject2 = new JSONObject();
		foreach (JSONObject item in jSONObject.list)
		{
			if (item["ItemID"].I == itemId)
			{
				jSONObject2 = item;
				break;
			}
		}
		int num = jSONObject2["Num"].I - 1;
		if (num <= 0)
		{
			jSONObject.list.Remove(jSONObject2);
		}
		else
		{
			jSONObject2.SetField("Num", num);
		}
	}

	private void RemoveItemByUid(int npcId, string uid)
	{
		JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		JSONObject jSONObject2 = new JSONObject();
		foreach (JSONObject item in jSONObject.list)
		{
			if (item["UUID"].Str == uid)
			{
				jSONObject2 = item;
				break;
			}
		}
		int num = jSONObject2["Num"].I - 1;
		if (num <= 0)
		{
			jSONObject.list.Remove(jSONObject2);
		}
		else
		{
			jSONObject2.SetField("Num", num);
		}
	}

	private void RemoveItemById(int npcId, int itemId, int count)
	{
		JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		List<JSONObject> list = new List<JSONObject>();
		int num = 0;
		foreach (JSONObject item in jSONObject.list)
		{
			if (item["ItemID"].I == itemId)
			{
				list.Add(item);
				num += item["Num"].I;
			}
		}
		num -= count;
		if (num > 0)
		{
			list[0].SetField("Num", num);
			list.RemoveAt(0);
		}
		if (list.Count <= 0)
		{
			return;
		}
		foreach (JSONObject item2 in list)
		{
			jSONObject.list.Remove(item2);
		}
	}

	public void RemoveItem(int npcId, int itemId, int count, string uid)
	{
		if (count == 1)
		{
			RemoveItemByUid(npcId, uid);
		}
		else
		{
			RemoveItemById(npcId, itemId, count);
		}
	}

	public void SortNpcPack(int npcId)
	{
		if (!isCanJieSuan)
		{
			Debug.Log((object)"正在结算中，不能整理");
			return;
		}
		JSONObject jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		if (jSONObject.Count < 1)
		{
			return;
		}
		Dictionary<int, JSONObject> dictionary = new Dictionary<int, JSONObject>();
		List<JSONObject> list = new List<JSONObject>();
		int num = 0;
		foreach (JSONObject item in jSONObject.list)
		{
			if (!item.HasField("ItemID"))
			{
				Debug.LogError((object)"整理NPC背包出错");
				Debug.LogError((object)$"npcId为：{npcId},物品数据没有ItemID");
				return;
			}
			num = item["ItemID"].I;
			if (num < 1)
			{
				Debug.LogError((object)"整理NPC背包出错");
				Debug.LogError((object)$"npcId为：{npcId},itemId小于1");
				return;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[num];
			if (itemJsonData.maxNum > 1)
			{
				if (dictionary.ContainsKey(itemJsonData.id))
				{
					dictionary[itemJsonData.id].SetField("Num", dictionary[itemJsonData.id]["Num"].I + item["Num"].I);
				}
				else
				{
					dictionary.Add(itemJsonData.id, item.Copy());
				}
			}
			else
			{
				list.Add(item.Copy());
			}
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"] = new JSONObject();
		jSONObject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		foreach (JSONObject item2 in list)
		{
			jSONObject.Add(item2);
		}
		foreach (int key in dictionary.Keys)
		{
			jSONObject.Add(dictionary[key]);
		}
	}

	public void AddNpcEquip(int npcId, int equipType, bool isLianQi = false)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		string index = EquipNameList[equipType - 1];
		int i = jSONObject["Level"].I;
		int num = 0;
		num = jSONObject[index][getRandomInt(0, jSONObject[index].Count - 1)].I;
		int ItemID = 0;
		int i2 = jsonData.instance.NpcLevelShouYiDate[i.ToString()]["fabao"].I;
		JSONObject ItemJson = new JSONObject();
		RandomNPCEquip.CreateLoveEquip(ref ItemID, ref ItemJson, num, null, i2);
		JSONObject item = AddItemToNpcBackpack(npcId, ItemID, 1, ItemJson);
		if (isLianQi)
		{
			npcNoteBook.NoteLianQi(npcId, i2, (equipType == 4) ? 1 : equipType, ItemJson["Name"].str);
			int i3 = jsonData.instance.LianQiJieSuanBiao[i2.ToString()]["exp"].I;
			npcSetField.AddNpcWuDaoExp(npcId, 22, i3);
		}
		npcUseItem.UseItem(npcId, item);
		int num2 = (int)jsonData.instance.LianQiWuQiQuality[jSONObject["Level"].I.ToString()][(object)"price"];
		npcSetField.AddNpcMoney(npcId, -num2);
	}

	public void UpdateNpcWuDao(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jSONObject["wudaoType"].I;
		int i2 = jSONObject["Level"].I;
		JSONObject nPCWuDaoJson = jsonData.instance.NPCWuDaoJson;
		for (int j = 0; j < nPCWuDaoJson.Count; j++)
		{
			if (nPCWuDaoJson[j]["Type"].I != i || nPCWuDaoJson[j]["lv"].I != i2)
			{
				continue;
			}
			for (int k = 0; k < nPCWuDaoJson[j]["wudaoID"].Count; k++)
			{
				int i3 = nPCWuDaoJson[j]["wudaoID"][k].I;
				if (!jSONObject["wuDaoSkillList"].ToList().Contains(i3))
				{
					try
					{
						jSONObject["wuDaoSkillList"].Add(i3);
					}
					catch (Exception ex)
					{
						Debug.LogError((object)ex);
					}
				}
			}
			for (int l = 1; l <= 12; l++)
			{
				int num = ((l <= 10) ? l : (l + 10));
				if (jSONObject["wuDaoJson"][num.ToString()]["level"].I < nPCWuDaoJson[j]["value" + l].I)
				{
					jSONObject["wuDaoJson"][num.ToString()].SetField("level", nPCWuDaoJson[j]["value" + l].I);
					int num2 = jSONObject["wuDaoJson"][num.ToString()]["level"].I - 1;
					int val = 0;
					if (num2 != 0)
					{
						val = jsonData.instance.WuDaoJinJieJson[num2.ToString()]["Max"].I;
					}
					jSONObject["wuDaoJson"][num.ToString()].SetField("exp", val);
				}
			}
			break;
		}
	}

	public bool IsCanLianDan(int npcId)
	{
		try
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["21"]["level"].I;
			if (jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I >= jsonData.instance.WuDaoJinJieJson[i.ToString()]["LianDan"].I)
			{
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.Message);
			Debug.LogError((object)ex.StackTrace);
			return false;
		}
	}

	public bool IsCanLianQi(int npcId)
	{
		try
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["22"]["level"].I;
			if (i == 0)
			{
				return false;
			}
			if (jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I >= jsonData.instance.WuDaoJinJieJson[i.ToString()]["LianQi"].I)
			{
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.Message);
			Debug.LogError((object)ex.StackTrace);
			return false;
		}
	}

	public int GetNpcBigLevel(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		int num = i / 3;
		if (i % 3 == 0)
		{
			num--;
		}
		return num + 1;
	}

	public void DoNothing(int npcId)
	{
		npcTeShu.NpcAddDoSomething(npcId);
	}

	public JSONObject GetNpcData(int npcId)
	{
		if (jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
		{
			return jsonData.instance.AvatarJsonData[npcId.ToString()];
		}
		return null;
	}

	public bool IsInScope(int cur, int min, int max)
	{
		if (cur >= min && cur <= max)
		{
			return true;
		}
		return false;
	}

	public bool ImprotantNpcActionPanDing(int npcId)
	{
		JSONObject npcData = GetNpcData(npcId);
		JSONObject npcImprotantPanDingData = jsonData.instance.NpcImprotantPanDingData;
		if (npcData["isImportant"].b && npcData.HasField("BindingNpcID"))
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			int i = npcData["BindingNpcID"].I;
			foreach (JSONObject item in npcImprotantPanDingData.list)
			{
				if (item["NPC"].I != i)
				{
					continue;
				}
				text = item["StartTime"].str;
				text2 = item["EndTime"].str;
				if (!Tools.instance.IsInTime(DateTime.Parse(JieSuanTime), DateTime.Parse(text), DateTime.Parse(text2)))
				{
					continue;
				}
				if (item["EventValue"].Count > 0)
				{
					text3 = item["fuhao"].str;
					int num = GlobalValue.Get(item["EventValue"][0].I, $"NpcJieSuanManager.ImprotantNpcActionPanDing({npcId})");
					switch (text3)
					{
					case "=":
						if (num == item["EventValue"][1].I)
						{
							npcData.SetField("ActionId", item["XingWei"].I);
							return true;
						}
						break;
					case "<":
						if (num < item["EventValue"][1].I)
						{
							npcData.SetField("ActionId", item["XingWei"].I);
							return true;
						}
						break;
					case ">":
						if (num > item["EventValue"][1].I)
						{
							npcData.SetField("ActionId", item["XingWei"].I);
							return true;
						}
						break;
					}
					continue;
				}
				npcData.SetField("ActionId", item["XingWei"].I);
				return true;
			}
		}
		return false;
	}

	public int GetNpcShengYuTime(int npcId)
	{
		JSONObject npcData = GetNpcData(npcId);
		int num = npcData["age"].I / 12;
		return npcData["shouYuan"].I - num;
	}

	public void GuDingAddExp(int npcId, float times = 1f)
	{
		JSONObject npcData = GetNpcData(npcId);
		npcData.SetField("isTanChaUnlock", val: false);
		if (npcData["isImportant"].b)
		{
			int npcBigLevel = GetNpcBigLevel(npcId);
			if (npcBigLevel == 1 && npcData.HasField("LianQiAddSpeed"))
			{
				npcSetField.AddNpcExp(npcId, (int)((float)npcData["LianQiAddSpeed"].I * times));
			}
			else if (npcBigLevel == 2 && npcData.HasField("ZhuJiAddSpeed"))
			{
				npcSetField.AddNpcExp(npcId, (int)((float)npcData["ZhuJiAddSpeed"].I * times));
			}
			else if (npcBigLevel == 3 && npcData.HasField("JinDanAddSpeed"))
			{
				npcSetField.AddNpcExp(npcId, (int)((float)npcData["JinDanAddSpeed"].I * times));
			}
			else if (npcBigLevel == 4 && npcData.HasField("HuaShengTime") && npcData.HasField("YuanYingAddSpeed"))
			{
				npcSetField.AddNpcExp(npcId, (int)((float)npcData["YuanYingAddSpeed"].I * times));
			}
		}
		int num = npcData["xiuLianSpeed"].I;
		if (npcData.HasField("JinDanData"))
		{
			float num2 = npcData["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		npcSetField.AddNpcExp(npcId, (int)((float)num * times));
	}

	public List<int> GetPaiMaiListByPaiMaiId(int paiMaiId)
	{
		List<int> list = new List<int>();
		string changJing = PaiMaiBiao.DataDict[paiMaiId].ChangJing;
		if (npcMap.threeSenceNPCDictionary.ContainsKey(changJing))
		{
			list.AddRange(npcMap.threeSenceNPCDictionary[changJing]);
		}
		if (PaiMaiNpcDictionary.ContainsKey(paiMaiId))
		{
			PaiMaiNpcDictionary[paiMaiId] = new List<int>();
		}
		return list;
	}

	public void CheckImportantEvent(string nowTime)
	{
		DateTime dateTime = DateTime.Parse(nowTime);
		Tools.instance.getPlayer();
		foreach (JSONObject item in jsonData.instance.NpcImprotantEventData.list)
		{
			if (!ImportantNpcBangDingDictionary.ContainsKey(item["ImportantNPC"].I))
			{
				continue;
			}
			int npcId = ImportantNpcBangDingDictionary[item["ImportantNPC"].I];
			JSONObject npcData = GetNpcData(npcId);
			DateTime dateTime2 = DateTime.Parse(item["Time"].str);
			if (!(dateTime >= dateTime2))
			{
				continue;
			}
			if (item["EventLv"].Count > 0)
			{
				bool flag = false;
				int num = GlobalValue.Get(item["EventLv"][0].I, "NpcJieSuanManager.CheckImportantEvent(" + nowTime + ")");
				if (item["fuhao"].str == "=")
				{
					if (num == item["EventLv"][1].I)
					{
						flag = true;
					}
				}
				else if (item["fuhao"].Str == ">")
				{
					if (num > item["EventLv"][1].I)
					{
						flag = true;
					}
				}
				else if (item["fuhao"].Str == "<" && num < item["EventLv"][1].I)
				{
					flag = true;
				}
				if (!flag)
				{
					continue;
				}
			}
			if (npcData["NoteBook"].HasField("101"))
			{
				bool flag2 = true;
				foreach (JSONObject item2 in npcData["NoteBook"]["101"].list)
				{
					if (item2["gudingshijian"].I == item["id"].I)
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					npcNoteBook.NoteImprotantEvent(npcId, item["id"].I, item["Time"].Str);
				}
			}
			else
			{
				npcNoteBook.NoteImprotantEvent(npcId, item["id"].I, item["Time"].Str);
			}
		}
	}

	public bool IsNeedHelp()
	{
		if (getRandomInt(0, 100) <= 30)
		{
			return true;
		}
		return false;
	}

	public bool IsDeath(int npcId)
	{
		if (npcDeath.npcDeathJson.HasField(npcId.ToString()) || (npcDeath.npcDeathJson.HasField("deathImportantList") && npcDeath.npcDeathJson["deathImportantList"].ToList().Contains(npcId)))
		{
			return true;
		}
		return false;
	}

	public bool IsFly(int npcId)
	{
		if (!jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
		{
			return false;
		}
		return GetNpcData(npcId).HasField("IsFly");
	}

	public void SendMessage(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = GetNpcData(npcId);
		int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
		if (!player.emailDateMag.cyNpcList.Contains(npcId))
		{
			return;
		}
		foreach (CyPdData cyPdAuTo in cyPdAuToList)
		{
			if ((cyPdAuTo.isOnly && npcData.HasField("CyList") && npcData["CyList"].ToList().Contains(cyPdAuTo.cyType)) || !cyPdAuTo.npcActionList.Contains(npcData["ActionId"].I) || (cyPdAuTo.npcType != 0 && cyPdAuTo.npcType != npcData["Type"].I) || npcData["Level"].I < cyPdAuTo.minLevel || npcData["Level"].I > cyPdAuTo.maxLevel || (cyPdAuTo.staticFuHao > 0 && !cyPdAuTo.StaticValuePd()) || (cyPdAuTo.needHaoGanDu > 0 && !cyPdAuTo.HaoGanPd(i)) || !cyPdAuTo.IsinTime() || (cyPdAuTo.npcState != 0 && cyPdAuTo.npcState != npcData["Status"]["StatusId"].I))
			{
				continue;
			}
			if (cyPdAuTo.actionId == 1)
			{
				if (getRandomInt(0, 100) > cyPdAuTo.baseRate)
				{
					continue;
				}
			}
			else
			{
				int num = player.AliveFriendCount - 3;
				if (num <= 0)
				{
					num = 1;
				}
				if (getRandomInt(0, 100) > cyPdAuTo.baseRate / num)
				{
					continue;
				}
			}
			if (cyPdAuTo.qingFen == 1 && npcData.TryGetField("QingFen").I < cyPdAuTo.itemPrice)
			{
				continue;
			}
			int randomInt = getRandomInt(1, 3);
			int duiBaiId = GetDuiBaiId(npcData["XingGe"].I, cyPdAuTo.talkId);
			if (cyPdAuTo.actionId != 0)
			{
				List<int> item = cyPdAuTo.GetItem(npcId);
				if (item.Count < 1)
				{
					continue;
				}
				player.emailDateMag.SendToPlayer(npcId, duiBaiId, randomInt, cyPdAuTo.actionId, item[0], item[1], cyPdAuTo.outTime, cyPdAuTo.addHaoGan, JieSuanTime);
				if (cyPdAuTo.qingFen == 1)
				{
					NPCEx.AddQingFen(npcData["id"].I, -jsonData.instance.ItemJsonData[item[0].ToString()]["price"].I * item[1]);
					Debug.Log((object)string.Format("{0}的情分减少{1}", npcData["id"].I, jsonData.instance.ItemJsonData[item[0].ToString()]["price"].I * item[1]));
				}
			}
			else
			{
				player.emailDateMag.SendToPlayer(npcId, duiBaiId, randomInt, cyPdAuTo.actionId, 0, 0, cyPdAuTo.outTime, cyPdAuTo.addHaoGan, JieSuanTime);
			}
			if (cyPdAuTo.isOnly)
			{
				if (npcData.HasField("CyList"))
				{
					npcData["CyList"].Add(cyPdAuTo.cyType);
					break;
				}
				JSONObject arr = JSONObject.arr;
				arr.Add(cyPdAuTo.cyType);
				npcData.SetField("CyList", arr);
			}
			break;
		}
	}

	public void SendFungusCyFu(int cytype)
	{
		if (!isCanJieSuan)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		foreach (CyPdData cyPdFungus in cyPdFungusList)
		{
			if (cyPdFungus.cyType != cytype)
			{
				continue;
			}
			foreach (int cyNpc in player.emailDateMag.cyNpcList)
			{
				if (cyNpc < 20000 || IsDeath(cyNpc))
				{
					continue;
				}
				JSONObject npcData = GetNpcData(cyNpc);
				int i = jsonData.instance.AvatarRandomJsonData[cyNpc.ToString()]["HaoGanDu"].I;
				if ((cyPdFungus.isOnly && npcData.HasField("CyList") && npcData["CyList"].ToList().Contains(cyPdFungus.cyType)) || !cyPdFungus.npcActionList.Contains(npcData["ActionId"].I) || (cyPdFungus.npcType != 0 && cyPdFungus.npcType != npcData["Type"].I) || npcData["Level"].I < cyPdFungus.minLevel || npcData["Level"].I > cyPdFungus.maxLevel || (cyPdFungus.staticFuHao > 0 && !cyPdFungus.StaticValuePd()) || (cyPdFungus.needHaoGanDu > 0 && !cyPdFungus.HaoGanPd(i)) || !cyPdFungus.IsinTime() || (cyPdFungus.npcState != 0 && cyPdFungus.npcState != npcData["Status"]["StatusId"].I) || getRandomInt(0, 100) > cyPdFungus.GetRate(i))
				{
					continue;
				}
				int randomInt = getRandomInt(1, 3);
				int duiBaiId = GetDuiBaiId(npcData["XingGe"].I, cyPdFungus.talkId);
				if (cyPdFungus.actionId != 0)
				{
					List<int> item = cyPdFungus.GetItem(cyNpc);
					if (item.Count < 1)
					{
						continue;
					}
					player.emailDateMag.SendToPlayerLate(cyNpc, duiBaiId, randomInt, cyPdFungus.actionId, item[0], item[1], cyPdFungus.outTime, cyPdFungus.addHaoGan, JieSuanTime);
				}
				else
				{
					player.emailDateMag.SendToPlayerLate(cyNpc, duiBaiId, randomInt, cyPdFungus.actionId, 0, 0, cyPdFungus.outTime, cyPdFungus.addHaoGan, JieSuanTime);
				}
				if (cyPdFungus.isOnly)
				{
					if (npcData.HasField("CyList"))
					{
						npcData["CyList"].Add(cyPdFungus.cyType);
						continue;
					}
					JSONObject arr = JSONObject.arr;
					arr.Add(cyPdFungus.cyType);
					npcData.SetField("CyList", arr);
				}
			}
		}
	}

	public void SendFungusCyByNpcId(int cytype, int npcId)
	{
		if (!isCanJieSuan)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		foreach (CyPdData cyPdFungus in cyPdFungusList)
		{
			if (cyPdFungus.cyType != cytype)
			{
				continue;
			}
			JSONObject npcData = GetNpcData(npcId);
			int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
			if ((cyPdFungus.isOnly && npcData.HasField("CyList") && npcData["CyList"].ToList().Contains(cyPdFungus.cyType)) || !cyPdFungus.npcActionList.Contains(npcData["ActionId"].I) || (cyPdFungus.npcType != 0 && cyPdFungus.npcType != npcData["Type"].I) || npcData["Level"].I < cyPdFungus.minLevel || npcData["Level"].I > cyPdFungus.maxLevel || (cyPdFungus.staticFuHao > 0 && !cyPdFungus.StaticValuePd()) || (cyPdFungus.needHaoGanDu > 0 && !cyPdFungus.HaoGanPd(i)) || !cyPdFungus.IsinTime() || (cyPdFungus.npcState != 0 && cyPdFungus.npcState != npcData["Status"]["StatusId"].I) || getRandomInt(0, 100) > cyPdFungus.GetRate(i))
			{
				continue;
			}
			int randomInt = getRandomInt(1, 3);
			int duiBaiId = GetDuiBaiId(npcData["XingGe"].I, cyPdFungus.talkId);
			if (cyPdFungus.actionId != 0)
			{
				List<int> item = cyPdFungus.GetItem(npcId);
				if (item.Count < 1)
				{
					continue;
				}
				player.emailDateMag.SendToPlayerLate(npcId, duiBaiId, randomInt, cyPdFungus.actionId, item[0], item[1], cyPdFungus.outTime, cyPdFungus.addHaoGan, JieSuanTime);
			}
			else
			{
				player.emailDateMag.SendToPlayerLate(npcId, duiBaiId, randomInt, cyPdFungus.actionId, 0, 0, cyPdFungus.outTime, cyPdFungus.addHaoGan, JieSuanTime);
			}
			if (cyPdFungus.isOnly)
			{
				if (npcData.HasField("CyList"))
				{
					npcData["CyList"].Add(cyPdFungus.cyType);
					continue;
				}
				JSONObject arr = JSONObject.arr;
				arr.Add(cyPdFungus.cyType);
				npcData.SetField("CyList", arr);
			}
		}
	}

	public void SendCy(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = GetNpcData(npcId);
		DateTime nowTime = GetNowTime();
		if (!player.emailDateMag.cyNpcList.Contains(npcId))
		{
			return;
		}
		foreach (CyRandomTaskData data in CyRandomTaskData.DataList)
		{
			if (CurJieSuanNpcTaskList.Contains(npcId))
			{
				break;
			}
			if (data.Type == 3 || (data.IsZhongYaoNPC == 0 && npcData.HasField("isImportant") && npcData["isImportant"].b) || (data.NPCLiuPai.Count > 0 && !data.NPCLiuPai.Contains(npcData.TryGetField("LiuPai").I)) || lateEmailDict.ContainsKey(data.id) || player.StreamData.TaskMag.HasTaskNpcList.Contains(npcId) || (data.IsOnly == 1 && player.emailDateMag.HasReceiveList.Contains(data.id)) || (DateTime.TryParse(data.StarTime, out var result) && DateTime.TryParse(data.EndTime, out var result2) && (nowTime < result || nowTime > result2)) || (data.Level.Count > 0 && (player.level < data.Level[0] || player.level > data.Level[1])) || (data.NPCLevel.Count > 0 && (npcData["Level"].I < data.NPCLevel[0] || npcData["Level"].I > data.NPCLevel[1])) || (data.NPCXingGe.Count > 0 && !data.NPCXingGe.Contains(npcData["XingGe"].I)) || (data.NPCType.Count > 0 && !data.NPCType.Contains(npcData["Type"].I)) || (data.NPCTag.Count > 0 && !data.NPCTag.Contains(npcData["NPCTag"].I)) || (data.NPCXingWei.Count > 0 && !data.NPCXingWei.Contains(npcData["ActionId"].I)) || (data.NPCXingWei.Count > 0 && !data.NPCXingWei.Contains(npcData["ActionId"].I)))
			{
				continue;
			}
			if (data.NPCGuanXi.Count > 0)
			{
				bool flag = false;
				foreach (int item in data.NPCGuanXi)
				{
					if (flag)
					{
						break;
					}
					switch (item)
					{
					case 1:
						if (PlayerEx.IsTheather(npcId))
						{
							flag = true;
						}
						break;
					case 2:
						if (PlayerEx.IsDaoLv(npcId))
						{
							flag = true;
						}
						break;
					case 3:
						if (PlayerEx.IsBrother(npcId))
						{
							flag = true;
						}
						break;
					}
				}
				if (!flag)
				{
					continue;
				}
			}
			if (data.NPCGuanXiNot.Count > 0)
			{
				bool flag2 = false;
				foreach (int item2 in data.NPCGuanXiNot)
				{
					if (flag2)
					{
						break;
					}
					switch (item2)
					{
					case 1:
						if (PlayerEx.IsTheather(npcId))
						{
							flag2 = true;
						}
						break;
					case 2:
						if (PlayerEx.IsDaoLv(npcId))
						{
							flag2 = true;
						}
						break;
					case 3:
						if (PlayerEx.IsBrother(npcId))
						{
							flag2 = true;
						}
						break;
					}
				}
				if (flag2)
				{
					continue;
				}
			}
			if (data.HaoGanDu.Count > 0)
			{
				int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
				if (i < data.HaoGanDu[0] || i > data.HaoGanDu[1])
				{
					continue;
				}
			}
			if (data.WuDaoType.Count > 0)
			{
				bool flag3 = true;
				for (int j = 0; j < data.WuDaoType.Count; j++)
				{
					if (npcData["wuDaoJson"][data.WuDaoType[j].ToString()]["level"].I < data.WuDaoLevel[j])
					{
						flag3 = false;
						break;
					}
				}
				if (!flag3)
				{
					continue;
				}
			}
			if (data.EventValue.Count > 0)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				bool flag4 = true;
				for (int k = 0; k < data.EventValue.Count; k++)
				{
					num = data.fuhao[k];
					int id = data.EventValue[k];
					num2 = data.EventValueNum[k];
					num3 = GlobalValue.Get(id, $"NpcJieSuanManager.SendCy({npcId}) 第三代传音符变量判定");
					switch (num)
					{
					case 1:
						if (num3 != num2)
						{
							flag4 = false;
						}
						break;
					case 2:
						if (num3 <= num2)
						{
							flag4 = false;
						}
						break;
					case 3:
						if (num3 >= num2)
						{
							flag4 = false;
						}
						break;
					}
				}
				if (!flag4)
				{
					continue;
				}
			}
			if (data.TaskType == 1 && !CurJieSuanNpcTaskList.Contains(npcId))
			{
				CurJieSuanNpcTaskList.Add(npcId);
			}
			int randomInt = getRandomInt(1, 3);
			int duiBaiId = GetDuiBaiId(npcData["XingGe"].I, data.info);
			DateTime dateTime = GetNowTime().AddDays(Tools.instance.GetRandomInt(data.DelayTime[0], data.DelayTime[0]));
			if (data.Type == 1)
			{
				dateTime = DateTime.Parse(data.StarTime).AddDays(Tools.instance.GetRandomInt(data.DelayTime[0], data.DelayTime[0]));
			}
			RandomTask randomTask = new RandomTask(data.id, data.TaskID, data.TaskType, data.Taskvalue, data.NPCxingdong, data.valueID, data.value);
			player.emailDateMag.RandomTaskSendToPlayer(randomTask, npcId, duiBaiId, randomInt, data.XingWeiType, data.ItemID, data.ItemNum, dateTime.ToString());
		}
	}

	public void SendFungusCy(int cyId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		CyRandomTaskData cyRandomTaskData = CyRandomTaskData.DataDict[cyId];
		JSONObject jSONObject = null;
		DateTime nowTime = GetNowTime();
		foreach (int cyNpc in player.emailDateMag.cyNpcList)
		{
			if (cyNpc < 20000)
			{
				continue;
			}
			jSONObject = GetNpcData(cyNpc);
			if (jSONObject == null || cyRandomTaskData.Type != 3 || (cyRandomTaskData.IsOnly == 1 && player.emailDateMag.HasReceiveList.Contains(cyRandomTaskData.id)) || (cyRandomTaskData.IsZhongYaoNPC == 0 && jSONObject.HasField("isImportant") && jSONObject["isImportant"].b) || (cyRandomTaskData.NPCLiuPai.Count > 0 && !cyRandomTaskData.NPCLiuPai.Contains(jSONObject.TryGetField("LiuPai").I)) || lateEmailDict.ContainsKey(cyRandomTaskData.id) || player.StreamData.TaskMag.HasTaskNpcList.Contains(cyNpc) || (DateTime.TryParse(cyRandomTaskData.StarTime, out var result) && DateTime.TryParse(cyRandomTaskData.EndTime, out var result2) && (nowTime < result || nowTime > result2)) || (cyRandomTaskData.Level.Count > 0 && (player.level < cyRandomTaskData.Level[0] || player.level > cyRandomTaskData.Level[1])) || (cyRandomTaskData.NPCLevel.Count > 0 && (jSONObject["Level"].I < cyRandomTaskData.NPCLevel[0] || jSONObject["Level"].I > cyRandomTaskData.NPCLevel[1])) || (cyRandomTaskData.NPCXingGe.Count > 0 && !cyRandomTaskData.NPCXingGe.Contains(jSONObject["XingGe"].I)) || (cyRandomTaskData.NPCType.Count > 0 && !cyRandomTaskData.NPCType.Contains(jSONObject["Type"].I)) || (cyRandomTaskData.NPCTag.Count > 0 && !cyRandomTaskData.NPCTag.Contains(jSONObject["NPCTag"].I)) || (cyRandomTaskData.NPCXingWei.Count > 0 && !cyRandomTaskData.NPCXingWei.Contains(jSONObject["ActionId"].I)) || (cyRandomTaskData.NPCXingWei.Count > 0 && !cyRandomTaskData.NPCXingWei.Contains(jSONObject["ActionId"].I)))
			{
				continue;
			}
			if (cyRandomTaskData.NPCGuanXi.Count > 0)
			{
				bool flag = false;
				foreach (int item in cyRandomTaskData.NPCGuanXi)
				{
					if (flag)
					{
						break;
					}
					switch (item)
					{
					case 1:
						if (PlayerEx.IsTheather(cyNpc))
						{
							flag = true;
						}
						break;
					case 2:
						if (PlayerEx.IsDaoLv(cyNpc))
						{
							flag = true;
						}
						break;
					case 3:
						if (PlayerEx.IsBrother(cyNpc))
						{
							flag = true;
						}
						break;
					}
				}
				if (!flag)
				{
					continue;
				}
			}
			if (cyRandomTaskData.HaoGanDu.Count > 0)
			{
				int i = jsonData.instance.AvatarRandomJsonData[cyNpc.ToString()]["HaoGanDu"].I;
				if (i < cyRandomTaskData.HaoGanDu[0] || i > cyRandomTaskData.HaoGanDu[1])
				{
					continue;
				}
			}
			if (cyRandomTaskData.WuDaoType.Count > 0)
			{
				bool flag2 = true;
				for (int j = 0; j < cyRandomTaskData.WuDaoType.Count; j++)
				{
					if (jSONObject["wuDaoJson"][cyRandomTaskData.WuDaoType[j].ToString()]["level"].I < cyRandomTaskData.WuDaoLevel[j])
					{
						flag2 = false;
						break;
					}
				}
				if (!flag2)
				{
					continue;
				}
			}
			if (cyRandomTaskData.EventValue.Count > 0)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				bool flag3 = true;
				for (int k = 0; k < cyRandomTaskData.EventValue.Count; k++)
				{
					num = cyRandomTaskData.fuhao[k];
					int id = cyRandomTaskData.EventValue[k];
					num2 = cyRandomTaskData.EventValueNum[k];
					num3 = GlobalValue.Get(id, $"NpcJieSuanManager.SendFungusCy({cyId}) 第三代传音符fungus发送 变量判定");
					switch (num)
					{
					case 1:
						if (num3 != num2)
						{
							flag3 = false;
						}
						break;
					case 2:
						if (num3 <= num2)
						{
							flag3 = false;
						}
						break;
					case 3:
						if (num3 >= num2)
						{
							flag3 = false;
						}
						break;
					}
				}
				if (!flag3)
				{
					continue;
				}
			}
			int randomInt = getRandomInt(1, 3);
			int duiBaiId = GetDuiBaiId(jSONObject["XingGe"].I, cyRandomTaskData.info);
			DateTime dateTime = GetNowTime().AddDays(Tools.instance.GetRandomInt(cyRandomTaskData.DelayTime[0], cyRandomTaskData.DelayTime[0]));
			RandomTask randomTask = new RandomTask(cyRandomTaskData.id, cyRandomTaskData.TaskID, cyRandomTaskData.TaskType, cyRandomTaskData.Taskvalue, cyRandomTaskData.NPCxingdong, cyRandomTaskData.valueID, cyRandomTaskData.value);
			player.emailDateMag.RandomTaskSendToPlayer(randomTask, cyNpc, duiBaiId, randomInt, cyRandomTaskData.XingWeiType, cyRandomTaskData.ItemID, cyRandomTaskData.ItemNum, dateTime.ToString());
		}
	}

	public List<int> GetJieShaNpcList(int index)
	{
		List<int> list = new List<int>();
		if (npcMap.bigMapNPCDictionary.ContainsKey(index) && npcMap.bigMapNPCDictionary[index].Count > 0)
		{
			foreach (int item in npcMap.bigMapNPCDictionary[index])
			{
				if (GetNpcBigLevel(item) == Tools.instance.getPlayer().getLevelType() && jsonData.instance.AvatarRandomJsonData[item.ToString()]["HaoGanDu"].I < 50 && GetNpcData(item)["ActionId"].I == 34)
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	public List<int> GetXunLuoNpcList(string fubenName, int index)
	{
		List<int> list = new List<int>();
		Avatar player = Tools.instance.getPlayer();
		if (npcMap.fuBenNPCDictionary.ContainsKey(fubenName) && npcMap.fuBenNPCDictionary[fubenName].ContainsKey(index))
		{
			foreach (int item in npcMap.fuBenNPCDictionary[fubenName][index])
			{
				JSONObject npcData = GetNpcData(item);
				if (npcData["MenPai"].I != player.menPai && player.shengShi <= npcData["shengShi"].I)
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	public int GetDuiBaiId(int XingGe, int type)
	{
		int result = 0;
		foreach (JSONObject item in jsonData.instance.CyNpcDuiBaiData.list)
		{
			if (item["XingGe"].I == XingGe && item["Type"].I == type)
			{
				result = item["id"].I;
			}
		}
		return result;
	}
}
