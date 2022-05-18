using System;
using System.Collections.Generic;
using System.Linq;
using CaiJi;
using JSONClass;
using KBEngine;
using script.MenPaiTask;
using UnityEngine;
using YSGame.TianJiDaBi;

// Token: 0x0200032D RID: 813
public class NpcJieSuanManager : MonoBehaviour
{
	// Token: 0x060017FE RID: 6142 RVA: 0x000D1A58 File Offset: 0x000CFC58
	private void Awake()
	{
		if (NpcJieSuanManager.inst != null)
		{
			Object.Destroy(NpcJieSuanManager.inst.gameObject);
		}
		NpcJieSuanManager.inst = this;
		Object.DontDestroyOnLoad(NpcJieSuanManager.inst.gameObject);
		jsonData.instance.init("Effect/json/d_avatar.py.datas", out jsonData.instance.AvatarJsonData);
		this.random = new Random();
		this.InitCyData();
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x000D1AC0 File Offset: 0x000CFCC0
	public void InitCyData()
	{
		this.cyPdAuToList = new List<CyPdData>();
		this.cyPdFungusList = new List<CyPdData>();
		foreach (JSONObject jsonobject in jsonData.instance.CyNpcSendData.list)
		{
			CyPdData cyPdData = new CyPdData();
			cyPdData.id = jsonobject["id"].I;
			cyPdData.npcActionList = jsonobject["NPCXingWei"].ToList();
			cyPdData.npcType = jsonobject["NPCshenfen"].I;
			cyPdData.minLevel = jsonobject["NPCLevel"][0].I;
			cyPdData.maxLevel = jsonobject["NPCLevel"][1].I;
			if (jsonobject["EventValue"].Count == 2)
			{
				cyPdData.staticId = jsonobject["EventValue"][0].I;
				cyPdData.staticId = jsonobject["EventValue"][1].I;
				cyPdData.SetStaticFuHao(jsonobject["fuhao1"].Str);
			}
			if (jsonobject["HaoGanDu"].I > 0)
			{
				cyPdData.needHaoGanDu = jsonobject["HaoGanDu"].I;
				cyPdData.SetHaoGanFuHao(jsonobject["fuhao2"].Str);
			}
			if (jsonobject["StarTime"].Str != "")
			{
				cyPdData.startTime = jsonobject["StarTime"].Str;
				cyPdData.endTime = jsonobject["EndTime"].Str;
			}
			cyPdData.npcState = jsonobject["ZhuangTaiInfo"].I;
			cyPdData.isOnly = (jsonobject["IsOnly"].I == 1);
			cyPdData.cyType = jsonobject["XiaoXiType"].I;
			cyPdData.baseRate = jsonobject["Rate"].I;
			cyPdData.actionId = jsonobject["XingWeiType"].I;
			cyPdData.qingFen = jsonobject["QingFen"].I;
			if (cyPdData.actionId != 0)
			{
				cyPdData.itemPrice = jsonobject["ItemJiaGe"].I;
			}
			cyPdData.outTime = jsonobject["GuoQiShiJian"].I;
			cyPdData.addHaoGan = jsonobject["HaoGanDuChange"].I;
			cyPdData.talkId = jsonobject["DuiBaiType"].I;
			if (cyPdData.actionId != 0)
			{
				int num = 0;
				while (num + 1 < jsonobject["RandomItemID"].Count)
				{
					if (this.cyDictionary.ContainsKey(cyPdData.id))
					{
						List<int> item = new List<int>
						{
							jsonobject["RandomItemID"][num].I,
							jsonobject["RandomItemID"][num + 1].I
						};
						this.cyDictionary[cyPdData.id].Add(item);
					}
					else
					{
						List<int> item2 = new List<int>
						{
							jsonobject["RandomItemID"][num].I,
							jsonobject["RandomItemID"][num + 1].I
						};
						this.cyDictionary.Add(cyPdData.id, new List<List<int>>
						{
							item2
						});
					}
					num += 2;
				}
			}
			if (jsonobject["IsChuFa"].I == 1)
			{
				this.cyPdFungusList.Add(cyPdData);
			}
			else if (jsonobject["IsChuFa"].I == 0)
			{
				this.cyPdAuToList.Add(cyPdData);
			}
		}
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000D1ECC File Offset: 0x000D00CC
	private void Start()
	{
		this.npcFight = new NpCFight();
		this.npcXiuLian = new NPCXiuLian();
		this.npcTuPo = new NPCTuPo();
		this.npcTianJiGe = new NpcTianJiGe();
		this.npcShouJi = new NPCShouJi();
		this.npcUseItem = new NPCUseItem();
		this.npcLiLian = new NPCLiLian();
		this.npcFuYe = new NPCFuYe();
		this.npcNoteBook = new NPCNoteBook();
		this.npcStatus = new NPCStatus();
		this.npcMap = new NPCMap();
		this.npcDeath = new NPCDeath();
		this.npcChengHao = new NPCChengHao();
		this.npcSetField = new NpcSetField();
		this.npcTeShu = new NPCTeShu();
		this.npcSpeedJieSuan = new NPCSpeedJieSuan();
		this.initNpcAction();
		this.EquipNameList.Add("equipWeaponPianHao");
		this.EquipNameList.Add("equipClothingPianHao");
		this.EquipNameList.Add("equipRingPianHao");
		this.EquipNameList.Add("equipWeapon2PianHao");
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x00015128 File Offset: 0x00013328
	public DateTime GetNowTime()
	{
		return DateTime.Parse(this.JieSuanTime);
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000D1FD0 File Offset: 0x000D01D0
	public void initNpcAction()
	{
		this.ActionDictionary.Add(1, new Action<int>(this.DoNothing));
		this.ActionDictionary.Add(2, new Action<int>(this.npcXiuLian.NpcBiGuan));
		this.ActionDictionary.Add(3, new Action<int>(this.npcShouJi.NpcCaiYao));
		this.ActionDictionary.Add(4, new Action<int>(this.npcShouJi.NpcCaiKuang));
		this.ActionDictionary.Add(5, new Action<int>(this.npcFuYe.NpcLianDan));
		this.ActionDictionary.Add(6, new Action<int>(this.npcFuYe.NpcLianQi));
		this.ActionDictionary.Add(7, new Action<int>(this.npcXiuLian.NpcXiuLianShenTong));
		this.ActionDictionary.Add(8, new Action<int>(this.npcShouJi.NpcBuyMiJi));
		this.ActionDictionary.Add(9, new Action<int>(this.npcShouJi.NpcBuyFaBao));
		this.ActionDictionary.Add(10, new Action<int>(this.npcXiuLian.NpcLunDao));
		this.ActionDictionary.Add(11, new Action<int>(this.npcShouJi.NpcBuyDanYao));
		this.ActionDictionary.Add(30, new Action<int>(this.npcLiLian.NPCNingZhouKillYaoShou));
		this.ActionDictionary.Add(31, new Action<int>(this.npcLiLian.NPCDoZhuChengTask));
		this.ActionDictionary.Add(33, new Action<int>(this.npcLiLian.NPCNingZhouYouLi));
		this.ActionDictionary.Add(34, new Action<int>(this.npcTeShu.NpcToJieSha));
		this.ActionDictionary.Add(35, new Action<int>(this.npcLiLian.NPCDoMenPaiTask));
		this.ActionDictionary.Add(36, new Action<int>(this.npcShouJi.NpcShouJiTuPoItem));
		this.ActionDictionary.Add(37, new Action<int>(this.npcShouJi.NpcSpeedDeath));
		this.NextActionDictionary.Add(36, new Action<int>(this.npcShouJi.NextNpcShouJiTuPoItem));
		this.ActionDictionary.Add(50, new Action<int>(this.npcTuPo.NpcBigTuPo));
		this.ActionDictionary.Add(41, new Action<int>(this.npcLiLian.NPCHaiShangKillYaoShou));
		this.ActionDictionary.Add(42, new Action<int>(this.npcLiLian.NpcHaiShangYouLi));
		this.ActionDictionary.Add(43, new Action<int>(this.npcTeShu.NpcSuiXingDaoJinHuo));
		this.ActionDictionary.Add(44, new Action<int>(this.npcTeShu.NpcToGangKou));
		this.ActionDictionary.Add(45, new Action<int>(this.npcFuYe.NpcCreateZhenQi));
		this.ActionDictionary.Add(51, new Action<int>(this.npcTeShu.NpcToDongShiGuShiFang));
		this.ActionDictionary.Add(52, new Action<int>(this.npcTeShu.NpcToTianXingShiFang));
		this.ActionDictionary.Add(53, new Action<int>(this.npcTeShu.NpcToHaiShangShiFang));
		this.NextActionDictionary.Add(51, new Action<int>(this.npcTeShu.NextNpcShiFang));
		this.NextActionDictionary.Add(52, new Action<int>(this.npcTeShu.NextNpcShiFang));
		this.NextActionDictionary.Add(53, new Action<int>(this.npcTeShu.NextNpcShiFang));
		this.ActionDictionary.Add(54, new Action<int>(this.npcTeShu.NpcToDongShiGuPaiMai));
		this.ActionDictionary.Add(55, new Action<int>(this.npcTeShu.NpcToTianJiGePaiMai));
		this.ActionDictionary.Add(56, new Action<int>(this.npcTeShu.NpcToHaiShangPaiMai));
		this.ActionDictionary.Add(57, new Action<int>(this.npcTeShu.NpcToNanYaChengPaiMai));
		this.ActionDictionary.Add(100, new Action<int>(this.npcXiuLian.NpcBiGuan));
		this.ActionDictionary.Add(101, new Action<int>(this.npcTeShu.NpcToGuangChang));
		this.ActionDictionary.Add(102, new Action<int>(this.npcTeShu.NpcZhangLaoToDaDian));
		this.ActionDictionary.Add(103, new Action<int>(this.npcTeShu.NpcZhangMenToDaDian));
		this.ActionDictionary.Add(111, new Action<int>(this.npcTianJiGe.TianJiGePaoShang));
		this.ActionDictionary.Add(112, new Action<int>(this.npcTianJiGe.TianJiGeJinHuo));
		this.ActionDictionary.Add(113, new Action<int>(this.npcTeShu.NpcFriendToDongFu));
		this.ActionDictionary.Add(114, new Action<int>(this.npcTeShu.NpcDaoLuToDongFu));
		this.ActionDictionary.Add(115, new Action<int>(this.npcTeShu.NpcToSuiXingShop));
		this.ActionDictionary.Add(116, new Action<int>(this.npcTeShu.NpcToSuiXingFaZhan));
		this.ActionDictionary.Add(121, new Action<int>(this.npcShouJi.NpcToLingHe1));
		this.ActionDictionary.Add(122, new Action<int>(this.npcShouJi.NpcToLingHe2));
		this.ActionDictionary.Add(123, new Action<int>(this.npcShouJi.NpcToLingHe3));
		this.ActionDictionary.Add(124, new Action<int>(this.npcShouJi.NpcToLingHe4));
		this.ActionDictionary.Add(125, new Action<int>(this.npcShouJi.NpcToLingHe5));
		this.ActionDictionary.Add(126, new Action<int>(this.npcShouJi.NpcToLingHe6));
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x000D25CC File Offset: 0x000D07CC
	public void NpcJieSuan(int times, bool isCanChanger = true)
	{
		if (this.IsNoJieSuan)
		{
			this.IsNoJieSuan = false;
			return;
		}
		this.isCanJieSuan = false;
		DateTime tempTime = DateTime.Parse(this.JieSuanTime).AddMonths(times);
		int actionTimes = 0;
		if (isCanChanger)
		{
			actionTimes = SystemConfig.Inst.GetNpcActionTimes();
		}
		Loom.RunAsync(delegate
		{
			try
			{
				if (!GameVersion.inst.realTest)
				{
					if (actionTimes != 2)
					{
						if (times > 120)
						{
							int num = 12;
							if (actionTimes == 1)
							{
								num *= 2;
							}
							int i = times - num;
							int num2 = i / num;
							num--;
							while (i >= num2)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num2);
								this.JieSuanTimes += num2;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num2).ToString();
								i -= num2;
								if (num > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num--;
								}
							}
							if (i > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(i);
								this.JieSuanTimes += i;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(i).ToString();
							}
							for (int j = 0; j < num; j++)
							{
								this.RandomNpcAction();
								this.JieSuanTimes++;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							}
							this.RandomNpcAction();
							this.JieSuanTimes++;
							this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							goto IL_77C;
						}
						if (times > 12 && times <= 120)
						{
							int num3 = 6;
							if (actionTimes == 1)
							{
								num3 *= 2;
							}
							int num4 = times - num3;
							int num5 = num4 / num3;
							num3--;
							while (num5 > 0 && num4 >= num5)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num5);
								this.JieSuanTimes += num5;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num5).ToString();
								num4 -= num5;
								if (num3 > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num3--;
								}
							}
							if (num4 > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num4);
								this.JieSuanTimes += num4;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num4).ToString();
							}
							for (int k = 0; k < num3; k++)
							{
								this.RandomNpcAction();
								this.JieSuanTimes++;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							}
							this.RandomNpcAction();
							this.JieSuanTimes++;
							this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							goto IL_77C;
						}
						if (times > 6 && times <= 12)
						{
							int num6 = times / 2;
							if (actionTimes == 1)
							{
								num6 *= 2;
								num6--;
							}
							int num7 = times - num6;
							int num8 = num7 / num6;
							num6--;
							while (num8 > 0 && num7 >= num8)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num8);
								this.JieSuanTimes += num8;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num8).ToString();
								num7 -= num8;
								if (num6 > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num6--;
								}
							}
							if (num7 > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num7);
								this.JieSuanTimes += num7;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num7).ToString();
							}
							for (int l = 0; l < num6; l++)
							{
								this.RandomNpcAction();
								this.JieSuanTimes++;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							}
							this.RandomNpcAction();
							this.JieSuanTimes++;
							this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
							goto IL_77C;
						}
						while (times > 0)
						{
							int times2 = times;
							times = times2 - 1;
							this.RandomNpcAction();
							this.JieSuanTimes++;
							this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
						}
						goto IL_77C;
					}
				}
				while (times > 0)
				{
					int times2 = times;
					times = times2 - 1;
					this.RandomNpcAction();
					this.JieSuanTimes++;
					this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
				}
				IL_77C:
				while (DateTime.Parse(this.JieSuanTime) >= DateTime.Parse(Tools.instance.getPlayer().NextCreateTime))
				{
					Tools.instance.getPlayer().NextCreateTime = DateTime.Parse(Tools.instance.getPlayer().NextCreateTime).AddYears(20).ToString();
					FactoryManager.inst.npcFactory.AuToCreateNpcs();
				}
				for (int m = 0; m < this.afterDeathList.Count; m++)
				{
					this.npcDeath.SetNpcDeath(this.afterDeathList[m][0], this.afterDeathList[m][1], this.afterDeathList[m][2], false);
				}
				TianJiDaBiManager.OnAddTime();
			}
			catch (Exception ex)
			{
				if (tempTime > DateTime.Parse(this.JieSuanTime))
				{
					this.JieSuanTime = tempTime.ToString();
				}
				Debug.LogError("结算出错");
				Debug.LogException(ex);
				this.FixNpcData();
			}
			finally
			{
				this.afterDeathList = new List<List<int>>();
				try
				{
					Tools.instance.getPlayer().setMonstarDeath();
				}
				catch (Exception ex2)
				{
					Debug.LogError(ex2);
					Debug.LogError("设置Npc死亡出错");
				}
				this.CurJieSuanNpcTaskList = new List<int>();
				this.isUpDateNpcList = true;
				this.isCanJieSuan = true;
				this.JieSuanAnimation = true;
				Loom.QueueOnMainThread(delegate(object obj)
				{
					MessageMag.Instance.Send("MSG_Npc_JieSuan_COMPLETE", null);
					if (PlayerEx.Player.TianJie != null && PlayerEx.Player.TianJie.HasField("ShowTianJieCD"))
					{
						TianJieManager.OnAddTime();
					}
				}, null);
			}
		});
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000D2650 File Offset: 0x000D0850
	public void FixNpcData()
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		foreach (string text in avatarJsonData.keys)
		{
			if (int.Parse(text) >= 20000)
			{
				if (!jsonData.instance.AvatarRandomJsonData.HasField(text))
				{
					if (avatarJsonData[text]["isImportant"].b)
					{
						jsonData.instance.AvatarRandomJsonData.SetField(avatarJsonData[text]["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[avatarJsonData[text]["BindingNpcID"].I.ToString()]);
					}
					else
					{
						JSONObject jsonobject = jsonData.instance.randomAvatarFace(avatarJsonData[text], jsonData.instance.AvatarRandomJsonData.HasField(string.Concat((int)avatarJsonData[text]["id"].n)) ? jsonData.instance.AvatarRandomJsonData[((int)avatarJsonData[text]["id"].n).ToString()] : null);
						jsonData.instance.AvatarRandomJsonData.SetField(string.Concat((int)avatarJsonData[text]["id"].n), jsonobject.Clone());
					}
				}
				if (!jsonData.instance.AvatarBackpackJsonData.HasField(text))
				{
					FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, avatarJsonData[text]["id"].I, avatarJsonData[text]);
				}
			}
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000D284C File Offset: 0x000D0A4C
	public void RandomNpcAction()
	{
		if (this.PaiMaiNpcDictionary.Count > 0)
		{
			this.npcTeShu.NextNpcPaiMai();
			this.PaiMaiNpcDictionary = new Dictionary<int, List<int>>();
		}
		if (this.lunDaoNpcList.Count >= 2)
		{
			this.npcXiuLian.NextNpcLunDao();
		}
		else
		{
			this.lunDaoNpcList = new List<int>();
		}
		this.npcTeShu.NextJieSha();
		Avatar player = Tools.instance.getPlayer();
		this.npcMap.RestartMap();
		if (this.NpcActionQuanZhongDictionary.Count < 1)
		{
			foreach (string text in jsonData.instance.NPCActionDate.keys)
			{
				this.NpcActionQuanZhongDictionary.Add(int.Parse(text), jsonData.instance.NPCActionDate[text]["QuanZhong"].I);
			}
		}
		new Dictionary<int, int>();
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		List<string> keys = jsonData.instance.AvatarJsonData.keys;
		for (int i = 0; i < keys.Count; i++)
		{
			string text2 = keys[i];
			int num = int.Parse(text2);
			if (num >= 20000 && !avatarJsonData[text2].HasField("IsFly"))
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>(this.NpcActionQuanZhongDictionary);
				dictionary = this.getFinallyNpcActionQuanZhongDictionary(avatarJsonData[text2], dictionary);
				int randomActionID = this.getRandomActionID(dictionary);
				if (this.ActionDictionary.ContainsKey(randomActionID) && !this.npcDeath.NpcYiWaiPanDing(randomActionID, num) && this.npcSetField.AddNpcAge(num, 1))
				{
					this.npcStatus.ReduceStatusTime(num, 1);
					int i2 = avatarJsonData[num.ToString()]["ActionId"].I;
					if (this.NextActionDictionary.ContainsKey(i2))
					{
						this.NextActionDictionary[i2](num);
					}
					int i3 = avatarJsonData[num.ToString()]["Status"]["StatusId"].I;
					if (avatarJsonData[num.ToString()]["Status"]["StatusId"].I == 20)
					{
						this.npcTeShu.NpcFriendToDongFu(num);
						avatarJsonData[num.ToString()].SetField("ActionId", 113);
					}
					else if (avatarJsonData[num.ToString()]["Status"]["StatusId"].I == 21)
					{
						this.npcTeShu.NpcDaoLuToDongFu(num);
						avatarJsonData[num.ToString()].SetField("ActionId", 114);
					}
					else
					{
						if (avatarJsonData[num.ToString()]["isImportant"].b && avatarJsonData[num.ToString()].HasField("BindingNpcID"))
						{
							if (!this.ImprotantNpcActionPanDing(num))
							{
								avatarJsonData[num.ToString()].SetField("ActionId", randomActionID);
								this.ActionDictionary[randomActionID](num);
								avatarJsonData[num.ToString()].SetField("IsNeedHelp", this.IsNeedHelp());
							}
						}
						else
						{
							avatarJsonData[num.ToString()].SetField("ActionId", randomActionID);
							this.ActionDictionary[randomActionID](num);
							avatarJsonData[num.ToString()].SetField("IsNeedHelp", this.IsNeedHelp());
						}
						this.SendMessage(num);
						this.SendCy(num);
					}
					this.GuDingAddExp(num, 1f);
				}
			}
		}
		if (!player.emailDateMag.IsStopAll)
		{
			if (this.lateEmailList.Count > 0)
			{
				foreach (EmailData emailData in this.lateEmailList)
				{
					player.emailDateMag.AddNewEmail(emailData.npcId.ToString(), emailData);
				}
				this.lateEmailList = new List<EmailData>();
			}
			if (this.lateEmailDict.Keys.Count > 0)
			{
				List<int> list = new List<int>();
				foreach (int num2 in this.lateEmailDict.Keys)
				{
					EmailData emailData2 = this.lateEmailDict[num2];
					if (emailData2.RandomTask != null)
					{
						DateTime dateTime = DateTime.Parse(emailData2.sendTime);
						if (this.GetNowTime() < dateTime)
						{
							continue;
						}
						RandomTask randomTask = emailData2.RandomTask;
						if (randomTask.TaskId != 0)
						{
							player.StreamData.TaskMag.AddTask(randomTask.TaskId, randomTask.TaskType, randomTask.CyId, emailData2.npcId, randomTask.TaskValue, dateTime);
							if (randomTask.TaskType == 1 && randomTask.LockActionId > 0)
							{
								this.getNpcData(emailData2.npcId).SetField("ActionId", randomTask.LockActionId);
								this.getNpcData(emailData2.npcId).SetField("LockAction", randomTask.LockActionId);
							}
						}
						if (randomTask.TaskValue != 0)
						{
							GlobalValue.Set(randomTask.TaskValue, emailData2.npcId, "NpcJieSuanManager.RandomNpcAction 传音符相关全局变量A");
						}
						if (randomTask.StaticId.Count > 0)
						{
							for (int j = 0; j < randomTask.StaticId.Count; j++)
							{
								GlobalValue.Set(randomTask.StaticId[j], randomTask.StaticValue[j], "NpcJieSuanManager.RandomNpcAction 传音符相关全局变量B");
							}
						}
					}
					list.Add(num2);
					player.emailDateMag.AddNewEmail(emailData2.npcId.ToString(), emailData2);
				}
				foreach (int key in list)
				{
					this.lateEmailDict.Remove(key);
				}
			}
		}
		Tools.instance.getPlayer().StreamData.TaskMag.CheckHasOut();
		this.CheckMenPaiTask();
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000D2F18 File Offset: 0x000D1118
	public void CheckMenPaiTask()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.menPai <= 0)
		{
			return;
		}
		int zhangMenId = this.GetZhangMenId((int)player.menPai);
		Dictionary<int, MenPaiFengLuBiao> dataDict = MenPaiFengLuBiao.DataDict;
		if (player.chengHao >= 7 && player.chengHao <= 10 && PlayerEx.GetMenPaiShengWang() < dataDict[player.chengHao].MenKan)
		{
			player.chengHao = 6;
			PlayerEx.SetShiLiChengHaoLevel((int)player.menPai, 6);
			player.AddFriend(zhangMenId);
			player.emailDateMag.AuToSendToPlayer(zhangMenId, 996, 996, NpcJieSuanManager.inst.JieSuanTime, null);
			return;
		}
		MenPaiTaskMag menPaiTaskMag = player.StreamData.MenPaiTaskMag;
		if (menPaiTaskMag.CheckNeedSend())
		{
			player.AddFriend(zhangMenId);
			menPaiTaskMag.SendTask(zhangMenId);
		}
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x000D2FD8 File Offset: 0x000D11D8
	private int GetZhangMenId(int shili)
	{
		int num = Tools.instance.getPlayer().GetZhangMenChengHaoId(shili);
		if (Tools.instance.getPlayer().menPai == 6)
		{
			num--;
		}
		foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
		{
			int i = jsonobject["id"].I;
			if (i >= 20000 && jsonobject["ChengHaoID"].I == num)
			{
				return i;
			}
		}
		Debug.LogError(string.Format("不存在当前势力的掌门，势力Id{0}", shili));
		return -1;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x000D30A0 File Offset: 0x000D12A0
	public Dictionary<int, int> getFinallyNpcActionQuanZhongDictionary(JSONObject npcDate, Dictionary<int, int> dictionary)
	{
		int i = npcDate["NPCTag"].I;
		NPCTagDate npctagDate = NPCTagDate.DataDict[i];
		for (int j = 0; j < npctagDate.Change.Count; j++)
		{
			if (dictionary.ContainsKey(npctagDate.Change[j]))
			{
				int key = npctagDate.Change[j];
				dictionary[key] += npctagDate.ChangeTo[j];
				if (dictionary[npctagDate.Change[j]] < 0)
				{
					dictionary[npctagDate.Change[j]] = 0;
				}
			}
		}
		NPCChengHaoData npcchengHaoData = NPCChengHaoData.DataDict[npcDate["ChengHaoID"].I];
		if (npcchengHaoData.ChengHao != npcDate["Title"].Str)
		{
			foreach (NPCChengHaoData npcchengHaoData2 in NPCChengHaoData.DataList)
			{
				if (npcchengHaoData2.ChengHao == npcDate["Title"].Str)
				{
					npcDate.SetField("ChengHaoID", npcchengHaoData2.id);
					break;
				}
			}
		}
		for (int k = 0; k < npcchengHaoData.Change.Count; k++)
		{
			int key = npcchengHaoData.Change[k];
			dictionary[key] += npcchengHaoData.ChangeTo[k];
			if (dictionary[npcchengHaoData.Change[k]] < 0)
			{
				dictionary[npcchengHaoData.Change[k]] = 0;
			}
		}
		List<int> list = new List<int>();
		foreach (int item in dictionary.Keys)
		{
			list.Add(item);
		}
		for (int l = 0; l < list.Count; l++)
		{
			int num = list[l];
			int i2 = jsonData.instance.NPCActionDate[num.ToString()]["PanDing"].I;
			if (i2 > 0)
			{
				switch (i2)
				{
				case 1:
					if (this.npcTuPo.IsCanBigTuPo(npcDate["id"].I))
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 2:
				{
					int month = this.GetNowTime().Month;
					if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && this.GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && month >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][0].I && month <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				}
				case 3:
				case 4:
					if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && this.GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 5:
				case 6:
				case 7:
				case 8:
					if (this.PaiMaiIsOpen(jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiTime"].I) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["LingShi"].I && npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 9:
					if (this.IsCanChangeEquip(npcDate) != 0)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 10:
					if (!this.IsCanLianDan(npcDate["id"].I))
					{
						dictionary[num] = 0;
						goto IL_C78;
					}
					goto IL_C78;
				case 11:
					if (!this.IsCanLianQi(npcDate["id"].I))
					{
						dictionary[num] = 0;
						goto IL_C78;
					}
					goto IL_C78;
				case 12:
					if (this.npcStatus.IsInTargetStatus(npcDate["id"].I, 2) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCTuPuoDate[npcDate["Level"].I.ToString()]["LingShiPanDuan"].I && !npcDate["isImportant"].b)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 13:
					if (this.npcStatus.IsInTargetStatus(npcDate["id"].I, 2))
					{
						dictionary[1] = 0;
						goto IL_C78;
					}
					goto IL_C78;
				case 14:
					if (this.GetNpcShengYuTime(npcDate["id"].I) < 10)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_C78;
					}
					goto IL_C78;
				case 15:
					if (npcDate["Level"].I == 1)
					{
						dictionary[num] = 0;
						goto IL_C78;
					}
					goto IL_C78;
				case 16:
					goto IL_C78;
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
					if (NPCActionPanDingDate.DataDict[i2].JingJie[0] > npcDate["Level"].I || NPCActionPanDingDate.DataDict[i2].JingJie[1] < npcDate["Level"].I || npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I != NPCActionPanDingDate.DataDict[i2].PaiMaiType)
					{
						goto IL_C78;
					}
					if (!this.npcMap.fuBenNPCDictionary.ContainsKey("F" + 26))
					{
						int key = num;
						dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
						goto IL_C78;
					}
					using (List<int>.Enumerator enumerator3 = NPCActionPanDingDate.DataDict[i2].LingHeDianWei.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int num2 = enumerator3.Current;
							if (!this.npcMap.fuBenNPCDictionary["F" + 26].Keys.Contains(num2) && (!(LingHeCaiJiUIMag.inst != null) || num2 != LingHeCaiJiUIMag.inst.nowMapIndex))
							{
								int key = num;
								dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
								break;
							}
						}
						goto IL_C78;
					}
					break;
				case 23:
					break;
				default:
					goto IL_C78;
				}
				if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I)
				{
					int key = num;
					dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
				}
			}
			IL_C78:;
		}
		return dictionary;
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x000D3D64 File Offset: 0x000D1F64
	public bool PaiMaiIsOpen(int paimaiHangID)
	{
		string str = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["StarTime"].str;
		string str2 = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["EndTime"].str;
		int i = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["circulation"].I;
		return Tools.instance.IsInTime(DateTime.Parse(this.JieSuanTime), DateTime.Parse(str), DateTime.Parse(str2), i);
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000D3E08 File Offset: 0x000D2008
	public int IsCanChangeEquip(JSONObject npcDate)
	{
		int i = jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I;
		int num = (int)jsonData.instance.LianQiWuQiQuality[npcDate["Level"].I.ToString()]["price"];
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

	// Token: 0x0600180B RID: 6155 RVA: 0x000D4024 File Offset: 0x000D2224
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

	// Token: 0x0600180C RID: 6156 RVA: 0x000D413C File Offset: 0x000D233C
	public int GetNpcBeiBaoAllItemSum(int npcID)
	{
		int num = 0;
		int index = NpcJieSuanManager.inst.getNpcBigLevel(npcID) - 1;
		NPCTuPuoDate npctuPuoDate = NPCTuPuoDate.DataList[index];
		List<int> list = new List<int>();
		foreach (int item in npctuPuoDate.ShouJiItem)
		{
			list.Add(item);
		}
		foreach (int item2 in npctuPuoDate.TuPoItem)
		{
			list.Add(item2);
		}
		foreach (JSONObject jsonobject in jsonData.instance.AvatarBackpackJsonData[string.Concat(npcID)]["Backpack"].list)
		{
			if (!list.Contains(jsonobject["ItemID"].I) && jsonobject["Num"].I > 0)
			{
				num += jsonobject["Num"].I;
			}
		}
		return num;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000D429C File Offset: 0x000D249C
	public Dictionary<int, int> GetNpcBaiBaoItemSum(int npcId, List<int> itemList)
	{
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)]["Backpack"].list;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		new List<int>();
		foreach (JSONObject jsonobject in list)
		{
			if (itemList.Contains(jsonobject["ItemID"].I))
			{
				if (dictionary.ContainsKey(jsonobject["ItemID"].I))
				{
					Dictionary<int, int> dictionary2 = dictionary;
					int i = jsonobject["ItemID"].I;
					dictionary2[i] += jsonobject["Num"].I;
				}
				else
				{
					dictionary.Add(jsonobject["ItemID"].I, jsonobject["Num"].I);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x00015135 File Offset: 0x00013335
	public int GetEquipLevel(int quality, int shangXia)
	{
		return quality * 3 - (3 - shangXia);
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x0001513E File Offset: 0x0001333E
	public int getRandomInt(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000D43AC File Offset: 0x000D25AC
	public int getRandomActionID(Dictionary<int, int> dictionary)
	{
		int num = 0;
		foreach (int key in dictionary.Keys)
		{
			num += dictionary[key];
		}
		int randomInt = this.getRandomInt(1, num);
		int result = -1;
		int num2 = 0;
		foreach (int num3 in dictionary.Keys)
		{
			num2 += dictionary[num3];
			if (num2 >= randomInt)
			{
				result = num3;
				break;
			}
		}
		return result;
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000D4468 File Offset: 0x000D2668
	public JSONObject AddItemToNpcBackpack(int npcId, int itemID, int num, JSONObject seid = null, bool isPaiMai = false)
	{
		JSONObject jsonobject = jsonData.instance.setAvatarBackpack(Tools.getUUID(), itemID, num, 1, 100, 1, (seid == null) ? Tools.CreateItemSeid(itemID) : seid, 0);
		if (isPaiMai && jsonobject["Seid"] != null)
		{
			jsonobject["Seid"].SetField("isPaiMai", true);
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"].Add(jsonobject);
		return jsonobject;
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x000D44E8 File Offset: 0x000D26E8
	public void RemoveNpcItem(int npcId, int itemId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		JSONObject jsonobject2 = new JSONObject();
		foreach (JSONObject jsonobject3 in jsonobject.list)
		{
			if (jsonobject3["ItemID"].I == itemId)
			{
				jsonobject2 = jsonobject3;
				break;
			}
		}
		int num = jsonobject2["Num"].I - 1;
		if (num <= 0)
		{
			jsonobject.list.Remove(jsonobject2);
			return;
		}
		jsonobject2.SetField("Num", num);
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000D45A8 File Offset: 0x000D27A8
	private void RemoveItemByUid(int npcId, string uid)
	{
		JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		JSONObject jsonobject2 = new JSONObject();
		foreach (JSONObject jsonobject3 in jsonobject.list)
		{
			if (jsonobject3["UUID"].Str == uid)
			{
				jsonobject2 = jsonobject3;
				break;
			}
		}
		int num = jsonobject2["Num"].I - 1;
		if (num <= 0)
		{
			jsonobject.list.Remove(jsonobject2);
			return;
		}
		jsonobject2.SetField("Num", num);
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x000D466C File Offset: 0x000D286C
	private void RemoveItemById(int npcId, int itemId, int count)
	{
		JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		List<JSONObject> list = new List<JSONObject>();
		int num = 0;
		foreach (JSONObject jsonobject2 in jsonobject.list)
		{
			if (jsonobject2["ItemID"].I == itemId)
			{
				list.Add(jsonobject2);
				num += jsonobject2["Num"].I;
			}
		}
		num -= count;
		if (num > 0)
		{
			list[0].SetField("Num", num);
			list.RemoveAt(0);
		}
		if (list.Count > 0)
		{
			foreach (JSONObject item in list)
			{
				jsonobject.list.Remove(item);
			}
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x0001514F File Offset: 0x0001334F
	public void RemoveItem(int npcId, int itemId, int count, string uid)
	{
		if (count == 1)
		{
			this.RemoveItemByUid(npcId, uid);
			return;
		}
		this.RemoveItemById(npcId, itemId, count);
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000D4784 File Offset: 0x000D2984
	public void SortNpcPack(int npcId)
	{
		if (!this.isCanJieSuan)
		{
			Debug.Log("正在结算中，不能整理");
			return;
		}
		JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		if (jsonobject.Count < 1)
		{
			return;
		}
		Dictionary<int, JSONObject> dictionary = new Dictionary<int, JSONObject>();
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject jsonobject2 in jsonobject.list)
		{
			if (!jsonobject2.HasField("ItemID"))
			{
				Debug.LogError("整理NPC背包出错");
				Debug.LogError(string.Format("npcId为：{0},物品数据没有ItemID", npcId));
				return;
			}
			int i = jsonobject2["ItemID"].I;
			if (i < 1)
			{
				Debug.LogError("整理NPC背包出错");
				Debug.LogError(string.Format("npcId为：{0},itemId小于1", npcId));
				return;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[i];
			if (itemJsonData.maxNum > 1)
			{
				if (dictionary.ContainsKey(itemJsonData.id))
				{
					dictionary[itemJsonData.id].SetField("Num", dictionary[itemJsonData.id]["Num"].I + jsonobject2["Num"].I);
				}
				else
				{
					dictionary.Add(itemJsonData.id, jsonobject2.Copy());
				}
			}
			else
			{
				list.Add(jsonobject2.Copy());
			}
		}
		jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"] = new JSONObject();
		jsonobject = jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["Backpack"];
		foreach (JSONObject obj in list)
		{
			jsonobject.Add(obj);
		}
		foreach (int key in dictionary.Keys)
		{
			jsonobject.Add(dictionary[key]);
		}
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000D4A18 File Offset: 0x000D2C18
	public void AddNpcEquip(int npcId, int equipType, bool isLianQi = false)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		string index = this.EquipNameList[equipType - 1];
		int i = jsonobject["Level"].I;
		int i2 = jsonobject[index][this.getRandomInt(0, jsonobject[index].Count - 1)].I;
		int itemID = 0;
		int i3 = jsonData.instance.NpcLevelShouYiDate[i.ToString()]["fabao"].I;
		JSONObject jsonobject2 = new JSONObject();
		RandomNPCEquip.CreateLoveEquip(ref itemID, ref jsonobject2, i2, null, i3);
		JSONObject item = this.AddItemToNpcBackpack(npcId, itemID, 1, jsonobject2, false);
		if (isLianQi)
		{
			this.npcNoteBook.NoteLianQi(npcId, i3, (equipType == 4) ? 1 : equipType, jsonobject2["Name"].str);
			int i4 = jsonData.instance.LianQiJieSuanBiao[i3.ToString()]["exp"].I;
			this.npcSetField.AddNpcWuDaoExp(npcId, 22, i4);
		}
		this.npcUseItem.UseItem(npcId, item, false);
		int num = (int)jsonData.instance.LianQiWuQiQuality[jsonobject["Level"].I.ToString()]["price"];
		this.npcSetField.AddNpcMoney(npcId, -num);
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x000D4B8C File Offset: 0x000D2D8C
	public void UpdateNpcWuDao(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		int i = jsonobject["wudaoType"].I;
		int i2 = jsonobject["Level"].I;
		JSONObject npcwuDaoJson = jsonData.instance.NPCWuDaoJson;
		for (int j = 0; j < npcwuDaoJson.Count; j++)
		{
			if (npcwuDaoJson[j]["Type"].I == i && npcwuDaoJson[j]["lv"].I == i2)
			{
				for (int k = 0; k < npcwuDaoJson[j]["wudaoID"].Count; k++)
				{
					int i3 = npcwuDaoJson[j]["wudaoID"][k].I;
					if (!jsonobject["wuDaoSkillList"].ToList().Contains(i3))
					{
						try
						{
							jsonobject["wuDaoSkillList"].Add(i3);
						}
						catch (Exception ex)
						{
							Debug.LogError(ex);
						}
					}
				}
				for (int l = 1; l <= 12; l++)
				{
					int num = (l <= 10) ? l : (l + 10);
					if (jsonobject["wuDaoJson"][num.ToString()]["level"].I < npcwuDaoJson[j]["value" + l].I)
					{
						jsonobject["wuDaoJson"][num.ToString()].SetField("level", npcwuDaoJson[j]["value" + l].I);
						int num2 = jsonobject["wuDaoJson"][num.ToString()]["level"].I - 1;
						int val = 0;
						if (num2 != 0)
						{
							val = jsonData.instance.WuDaoJinJieJson[num2.ToString()]["Max"].I;
						}
						jsonobject["wuDaoJson"][num.ToString()].SetField("exp", val);
					}
				}
				return;
			}
		}
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x000D4DF4 File Offset: 0x000D2FF4
	public bool IsCanLianDan(int npcId)
	{
		bool result;
		try
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["21"]["level"].I;
			if (jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I >= jsonData.instance.WuDaoJinJieJson[i.ToString()]["LianDan"].I)
			{
				result = true;
			}
			else
			{
				result = false;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			Debug.LogError(ex.StackTrace);
			result = false;
		}
		return result;
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000D4EBC File Offset: 0x000D30BC
	public bool IsCanLianQi(int npcId)
	{
		bool result;
		try
		{
			int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["wuDaoJson"]["22"]["level"].I;
			if (i == 0)
			{
				result = false;
			}
			else if (jsonData.instance.AvatarBackpackJsonData[npcId.ToString()]["money"].I >= jsonData.instance.WuDaoJinJieJson[i.ToString()]["LianQi"].I)
			{
				result = true;
			}
			else
			{
				result = false;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			Debug.LogError(ex.StackTrace);
			result = false;
		}
		return result;
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000D4F88 File Offset: 0x000D3188
	public int getNpcBigLevel(int npcId)
	{
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["Level"].I;
		int num = i / 3;
		if (i % 3 == 0)
		{
			num--;
		}
		return num + 1;
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x00015168 File Offset: 0x00013368
	public void DoNothing(int npcId)
	{
		this.npcTeShu.NpcAddDoSomething(npcId);
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x00015176 File Offset: 0x00013376
	public JSONObject getNpcData(int npcId)
	{
		return jsonData.instance.AvatarJsonData[npcId.ToString()];
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x0001518E File Offset: 0x0001338E
	public bool IsInScope(int cur, int min, int max)
	{
		return cur >= min && cur <= max;
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000D4FCC File Offset: 0x000D31CC
	public bool ImprotantNpcActionPanDing(int npcId)
	{
		JSONObject npcData = this.getNpcData(npcId);
		JSONObject npcImprotantPanDingData = jsonData.instance.NpcImprotantPanDingData;
		if (npcData["isImportant"].b && npcData.HasField("BindingNpcID"))
		{
			int i = npcData["BindingNpcID"].I;
			foreach (JSONObject jsonobject in npcImprotantPanDingData.list)
			{
				if (jsonobject["NPC"].I == i)
				{
					string str = jsonobject["StartTime"].str;
					string str2 = jsonobject["EndTime"].str;
					if (Tools.instance.IsInTime(DateTime.Parse(this.JieSuanTime), DateTime.Parse(str), DateTime.Parse(str2), 0))
					{
						if (jsonobject["EventValue"].Count <= 0)
						{
							npcData.SetField("ActionId", jsonobject["XingWei"].I);
							return true;
						}
						string str3 = jsonobject["fuhao"].str;
						int num = GlobalValue.Get(jsonobject["EventValue"][0].I, string.Format("NpcJieSuanManager.ImprotantNpcActionPanDing({0})", npcId));
						if (str3 == "=")
						{
							if (num == jsonobject["EventValue"][1].I)
							{
								npcData.SetField("ActionId", jsonobject["XingWei"].I);
								return true;
							}
						}
						else if (str3 == "<")
						{
							if (num < jsonobject["EventValue"][1].I)
							{
								npcData.SetField("ActionId", jsonobject["XingWei"].I);
								return true;
							}
						}
						else if (str3 == ">" && num > jsonobject["EventValue"][1].I)
						{
							npcData.SetField("ActionId", jsonobject["XingWei"].I);
							return true;
						}
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000D5254 File Offset: 0x000D3454
	public int GetNpcShengYuTime(int npcId)
	{
		JSONObject npcData = this.getNpcData(npcId);
		int num = npcData["age"].I / 12;
		return npcData["shouYuan"].I - num;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000D5290 File Offset: 0x000D3490
	public void GuDingAddExp(int npcId, float times = 1f)
	{
		JSONObject npcData = this.getNpcData(npcId);
		npcData.SetField("isTanChaUnlock", false);
		if (npcData["isImportant"].b)
		{
			int npcBigLevel = this.getNpcBigLevel(npcId);
			if (npcBigLevel == 1 && npcData.HasField("LianQiAddSpeed"))
			{
				this.npcSetField.AddNpcExp(npcId, (int)((float)npcData["LianQiAddSpeed"].I * times));
			}
			else if (npcBigLevel == 2 && npcData.HasField("ZhuJiAddSpeed"))
			{
				this.npcSetField.AddNpcExp(npcId, (int)((float)npcData["ZhuJiAddSpeed"].I * times));
			}
			else if (npcBigLevel == 3 && npcData.HasField("JinDanAddSpeed"))
			{
				this.npcSetField.AddNpcExp(npcId, (int)((float)npcData["JinDanAddSpeed"].I * times));
			}
			else if (npcBigLevel == 4 && npcData.HasField("HuaShengTime") && npcData.HasField("YuanYingAddSpeed"))
			{
				this.npcSetField.AddNpcExp(npcId, (int)((float)npcData["YuanYingAddSpeed"].I * times));
			}
		}
		int num = npcData["xiuLianSpeed"].I;
		if (npcData.HasField("JinDanData"))
		{
			float num2 = npcData["JinDanData"]["JinDanAddSpeed"].f / 100f;
			num += (int)(num2 * (float)num);
		}
		this.npcSetField.AddNpcExp(npcId, (int)((float)num * times));
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000D5400 File Offset: 0x000D3600
	public List<int> GetPaiMaiListByPaiMaiId(int paiMaiId)
	{
		List<int> list = new List<int>();
		string changJing = PaiMaiBiao.DataDict[paiMaiId].ChangJing;
		if (this.npcMap.threeSenceNPCDictionary.ContainsKey(changJing))
		{
			list.AddRange(this.npcMap.threeSenceNPCDictionary[changJing]);
		}
		if (this.PaiMaiNpcDictionary.ContainsKey(paiMaiId))
		{
			this.PaiMaiNpcDictionary[paiMaiId] = new List<int>();
		}
		return list;
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000D5470 File Offset: 0x000D3670
	public void CheckImportantEvent(string nowTime)
	{
		DateTime t = DateTime.Parse(nowTime);
		Tools.instance.getPlayer();
		foreach (JSONObject jsonobject in jsonData.instance.NpcImprotantEventData.list)
		{
			if (this.ImportantNpcBangDingDictionary.ContainsKey(jsonobject["ImportantNPC"].I))
			{
				int npcId = this.ImportantNpcBangDingDictionary[jsonobject["ImportantNPC"].I];
				JSONObject npcData = this.getNpcData(npcId);
				DateTime t2 = DateTime.Parse(jsonobject["Time"].str);
				if (t >= t2)
				{
					if (jsonobject["EventLv"].Count > 0)
					{
						bool flag = false;
						int num = GlobalValue.Get(jsonobject["EventLv"][0].I, "NpcJieSuanManager.CheckImportantEvent(" + nowTime + ")");
						if (jsonobject["fuhao"].str == "=")
						{
							if (num == jsonobject["EventLv"][1].I)
							{
								flag = true;
							}
						}
						else if (jsonobject["fuhao"].Str == ">")
						{
							if (num > jsonobject["EventLv"][1].I)
							{
								flag = true;
							}
						}
						else if (jsonobject["fuhao"].Str == "<" && num < jsonobject["EventLv"][1].I)
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
						using (List<JSONObject>.Enumerator enumerator2 = npcData["NoteBook"]["101"].list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current["gudingshijian"].I == jsonobject["id"].I)
								{
									flag2 = false;
								}
							}
						}
						if (flag2)
						{
							this.npcNoteBook.NoteImprotantEvent(npcId, jsonobject["id"].I, jsonobject["Time"].Str);
						}
					}
					else
					{
						this.npcNoteBook.NoteImprotantEvent(npcId, jsonobject["id"].I, jsonobject["Time"].Str);
					}
				}
			}
		}
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x0001519B File Offset: 0x0001339B
	public bool IsNeedHelp()
	{
		return this.getRandomInt(0, 100) <= 30;
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000D574C File Offset: 0x000D394C
	public bool IsDeath(int npcId)
	{
		return this.npcDeath.npcDeathJson.HasField(npcId.ToString()) || (this.npcDeath.npcDeathJson.HasField("deathImportantList") && this.npcDeath.npcDeathJson["deathImportantList"].ToList().Contains(npcId));
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000151AD File Offset: 0x000133AD
	public bool IsFly(int npcId)
	{
		return this.getNpcData(npcId).HasField("IsFly");
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000D57B0 File Offset: 0x000D39B0
	public void SendMessage(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = this.getNpcData(npcId);
		int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
		if (player.emailDateMag.cyNpcList.Contains(npcId))
		{
			foreach (CyPdData cyPdData in this.cyPdAuToList)
			{
				if ((!cyPdData.isOnly || !npcData.HasField("CyList") || !npcData["CyList"].ToList().Contains(cyPdData.cyType)) && cyPdData.npcActionList.Contains(npcData["ActionId"].I) && (cyPdData.npcType == 0 || cyPdData.npcType == npcData["Type"].I) && npcData["Level"].I >= cyPdData.minLevel && npcData["Level"].I <= cyPdData.maxLevel && (cyPdData.staticFuHao <= 0 || cyPdData.StaticValuePd()) && (cyPdData.needHaoGanDu <= 0 || cyPdData.HaoGanPd(i)) && cyPdData.IsinTime() && (cyPdData.npcState == 0 || cyPdData.npcState == npcData["Status"]["StatusId"].I))
				{
					if (cyPdData.actionId == 1)
					{
						if (this.getRandomInt(0, 100) > cyPdData.baseRate)
						{
							continue;
						}
					}
					else if (this.getRandomInt(0, 100) > cyPdData.baseRate)
					{
						continue;
					}
					if (cyPdData.qingFen != 1 || npcData.TryGetField("QingFen").I >= cyPdData.itemPrice)
					{
						int randomInt = this.getRandomInt(1, 3);
						int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
						if (cyPdData.actionId != 0)
						{
							List<int> item = cyPdData.GetItem();
							player.emailDateMag.SendToPlayer(npcId, duiBaiId, randomInt, cyPdData.actionId, item[0], item[1], cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
							if (cyPdData.qingFen == 1)
							{
								NPCEx.AddQingFen(npcData["id"].I, -jsonData.instance.ItemJsonData[item[0].ToString()]["price"].I * item[1], false);
								Debug.Log(string.Format("{0}的情分减少{1}", npcData["id"].I, jsonData.instance.ItemJsonData[item[0].ToString()]["price"].I * item[1]));
							}
						}
						else
						{
							player.emailDateMag.SendToPlayer(npcId, duiBaiId, randomInt, cyPdData.actionId, 0, 0, cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
						}
						if (cyPdData.isOnly)
						{
							if (npcData.HasField("CyList"))
							{
								npcData["CyList"].Add(cyPdData.cyType);
							}
							else
							{
								JSONObject arr = JSONObject.arr;
								arr.Add(cyPdData.cyType);
								npcData.SetField("CyList", arr);
							}
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000D5BA0 File Offset: 0x000D3DA0
	public void SendFungusCyFu(int cytype)
	{
		if (!this.isCanJieSuan)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		foreach (CyPdData cyPdData in this.cyPdFungusList)
		{
			if (cyPdData.cyType == cytype)
			{
				foreach (int num in player.emailDateMag.cyNpcList)
				{
					if (num >= 20000 && !this.IsDeath(num))
					{
						JSONObject npcData = this.getNpcData(num);
						int i = jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
						if ((!cyPdData.isOnly || !npcData.HasField("CyList") || !npcData["CyList"].ToList().Contains(cyPdData.cyType)) && cyPdData.npcActionList.Contains(npcData["ActionId"].I) && (cyPdData.npcType == 0 || cyPdData.npcType == npcData["Type"].I) && npcData["Level"].I >= cyPdData.minLevel && npcData["Level"].I <= cyPdData.maxLevel && (cyPdData.staticFuHao <= 0 || cyPdData.StaticValuePd()) && (cyPdData.needHaoGanDu <= 0 || cyPdData.HaoGanPd(i)) && cyPdData.IsinTime() && (cyPdData.npcState == 0 || cyPdData.npcState == npcData["Status"]["StatusId"].I) && this.getRandomInt(0, 100) <= cyPdData.GetRate(i))
						{
							int randomInt = this.getRandomInt(1, 3);
							int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
							if (cyPdData.actionId != 0)
							{
								List<int> item = cyPdData.GetItem();
								player.emailDateMag.SendToPlayerLate(num, duiBaiId, randomInt, cyPdData.actionId, item[0], item[1], cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
							}
							else
							{
								player.emailDateMag.SendToPlayerLate(num, duiBaiId, randomInt, cyPdData.actionId, 0, 0, cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
							}
							if (cyPdData.isOnly)
							{
								if (npcData.HasField("CyList"))
								{
									npcData["CyList"].Add(cyPdData.cyType);
								}
								else
								{
									JSONObject arr = JSONObject.arr;
									arr.Add(cyPdData.cyType);
									npcData.SetField("CyList", arr);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x000D5EE8 File Offset: 0x000D40E8
	public void SendFungusCyByNpcId(int cytype, int npcId)
	{
		if (!this.isCanJieSuan)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		foreach (CyPdData cyPdData in this.cyPdFungusList)
		{
			if (cyPdData.cyType == cytype)
			{
				JSONObject npcData = this.getNpcData(npcId);
				int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
				if ((!cyPdData.isOnly || !npcData.HasField("CyList") || !npcData["CyList"].ToList().Contains(cyPdData.cyType)) && cyPdData.npcActionList.Contains(npcData["ActionId"].I) && (cyPdData.npcType == 0 || cyPdData.npcType == npcData["Type"].I) && npcData["Level"].I >= cyPdData.minLevel && npcData["Level"].I <= cyPdData.maxLevel && (cyPdData.staticFuHao <= 0 || cyPdData.StaticValuePd()) && (cyPdData.needHaoGanDu <= 0 || cyPdData.HaoGanPd(i)) && cyPdData.IsinTime() && (cyPdData.npcState == 0 || cyPdData.npcState == npcData["Status"]["StatusId"].I) && this.getRandomInt(0, 100) <= cyPdData.GetRate(i))
				{
					int randomInt = this.getRandomInt(1, 3);
					int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
					if (cyPdData.actionId != 0)
					{
						List<int> item = cyPdData.GetItem();
						player.emailDateMag.SendToPlayerLate(npcId, duiBaiId, randomInt, cyPdData.actionId, item[0], item[1], cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
					}
					else
					{
						player.emailDateMag.SendToPlayerLate(npcId, duiBaiId, randomInt, cyPdData.actionId, 0, 0, cyPdData.outTime, cyPdData.addHaoGan, this.JieSuanTime);
					}
					if (cyPdData.isOnly)
					{
						if (npcData.HasField("CyList"))
						{
							npcData["CyList"].Add(cyPdData.cyType);
						}
						else
						{
							JSONObject arr = JSONObject.arr;
							arr.Add(cyPdData.cyType);
							npcData.SetField("CyList", arr);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000D61A8 File Offset: 0x000D43A8
	public void SendCy(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = this.getNpcData(npcId);
		DateTime nowTime = this.GetNowTime();
		if (!player.emailDateMag.cyNpcList.Contains(npcId))
		{
			return;
		}
		foreach (CyRandomTaskData cyRandomTaskData in CyRandomTaskData.DataList)
		{
			if (this.CurJieSuanNpcTaskList.Contains(npcId))
			{
				break;
			}
			DateTime t;
			DateTime t2;
			if (cyRandomTaskData.Type != 3 && (cyRandomTaskData.IsZhongYaoNPC != 0 || !npcData.HasField("isImportant") || !npcData["isImportant"].b) && (cyRandomTaskData.NPCLiuPai.Count <= 0 || cyRandomTaskData.NPCLiuPai.Contains(npcData.TryGetField("LiuPai").I)) && !this.lateEmailDict.ContainsKey(cyRandomTaskData.id) && !player.StreamData.TaskMag.HasTaskNpcList.Contains(npcId) && (cyRandomTaskData.IsOnly != 1 || !player.emailDateMag.HasReceiveList.Contains(cyRandomTaskData.id)) && (!DateTime.TryParse(cyRandomTaskData.StarTime, out t) || !DateTime.TryParse(cyRandomTaskData.EndTime, out t2) || (!(nowTime < t) && !(nowTime > t2))) && (cyRandomTaskData.Level.Count <= 0 || ((int)player.level >= cyRandomTaskData.Level[0] && (int)player.level <= cyRandomTaskData.Level[1])) && (cyRandomTaskData.NPCLevel.Count <= 0 || (npcData["Level"].I >= cyRandomTaskData.NPCLevel[0] && npcData["Level"].I <= cyRandomTaskData.NPCLevel[1])) && (cyRandomTaskData.NPCXingGe.Count <= 0 || cyRandomTaskData.NPCXingGe.Contains(npcData["XingGe"].I)) && (cyRandomTaskData.NPCType.Count <= 0 || cyRandomTaskData.NPCType.Contains(npcData["Type"].I)) && (cyRandomTaskData.NPCTag.Count <= 0 || cyRandomTaskData.NPCTag.Contains(npcData["NPCTag"].I)) && (cyRandomTaskData.NPCXingWei.Count <= 0 || cyRandomTaskData.NPCXingWei.Contains(npcData["ActionId"].I)) && (cyRandomTaskData.NPCXingWei.Count <= 0 || cyRandomTaskData.NPCXingWei.Contains(npcData["ActionId"].I)))
			{
				if (cyRandomTaskData.NPCGuanXi.Count > 0)
				{
					bool flag = false;
					foreach (int num in cyRandomTaskData.NPCGuanXi)
					{
						if (flag)
						{
							break;
						}
						switch (num)
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
				if (cyRandomTaskData.NPCGuanXiNot.Count > 0)
				{
					bool flag2 = false;
					foreach (int num2 in cyRandomTaskData.NPCGuanXiNot)
					{
						if (flag2)
						{
							break;
						}
						switch (num2)
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
				if (cyRandomTaskData.HaoGanDu.Count > 0)
				{
					int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
					if (i < cyRandomTaskData.HaoGanDu[0] || i > cyRandomTaskData.HaoGanDu[1])
					{
						continue;
					}
				}
				if (cyRandomTaskData.WuDaoType.Count > 0)
				{
					bool flag3 = true;
					for (int j = 0; j < cyRandomTaskData.WuDaoType.Count; j++)
					{
						if (npcData["wuDaoJson"][cyRandomTaskData.WuDaoType[j].ToString()]["level"].I < cyRandomTaskData.WuDaoLevel[j])
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
				if (cyRandomTaskData.EventValue.Count > 0)
				{
					bool flag4 = true;
					for (int k = 0; k < cyRandomTaskData.EventValue.Count; k++)
					{
						int num3 = cyRandomTaskData.fuhao[k];
						int id = cyRandomTaskData.EventValue[k];
						int num4 = cyRandomTaskData.EventValueNum[k];
						int num5 = GlobalValue.Get(id, string.Format("NpcJieSuanManager.SendCy({0}) 第三代传音符变量判定", npcId));
						switch (num3)
						{
						case 1:
							if (num5 != num4)
							{
								flag4 = false;
							}
							break;
						case 2:
							if (num5 <= num4)
							{
								flag4 = false;
							}
							break;
						case 3:
							if (num5 >= num4)
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
				if (cyRandomTaskData.TaskType == 1 && !this.CurJieSuanNpcTaskList.Contains(npcId))
				{
					this.CurJieSuanNpcTaskList.Add(npcId);
				}
				int randomInt = this.getRandomInt(1, 3);
				int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyRandomTaskData.info);
				DateTime dateTime = this.GetNowTime().AddDays((double)Tools.instance.GetRandomInt(cyRandomTaskData.DelayTime[0], cyRandomTaskData.DelayTime[0]));
				if (cyRandomTaskData.Type == 1)
				{
					dateTime = DateTime.Parse(cyRandomTaskData.StarTime).AddDays((double)Tools.instance.GetRandomInt(cyRandomTaskData.DelayTime[0], cyRandomTaskData.DelayTime[0]));
				}
				RandomTask randomTask = new RandomTask(cyRandomTaskData.id, cyRandomTaskData.TaskID, cyRandomTaskData.TaskType, cyRandomTaskData.Taskvalue, cyRandomTaskData.NPCxingdong, cyRandomTaskData.valueID, cyRandomTaskData.value);
				player.emailDateMag.RandomTaskSendToPlayer(randomTask, npcId, duiBaiId, randomInt, cyRandomTaskData.XingWeiType, cyRandomTaskData.ItemID, cyRandomTaskData.ItemNum, dateTime.ToString());
			}
		}
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000D68E8 File Offset: 0x000D4AE8
	public void SendFungusCy(int cyId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		CyRandomTaskData cyRandomTaskData = CyRandomTaskData.DataDict[cyId];
		JSONObject jsonobject = null;
		DateTime nowTime = this.GetNowTime();
		foreach (int num in player.emailDateMag.cyNpcList)
		{
			if (num >= 20000)
			{
				jsonobject = this.getNpcData(num);
				DateTime t;
				DateTime t2;
				if (jsonobject != null && cyRandomTaskData.Type == 3 && (cyRandomTaskData.IsOnly != 1 || !player.emailDateMag.HasReceiveList.Contains(cyRandomTaskData.id)) && (cyRandomTaskData.IsZhongYaoNPC != 0 || !jsonobject.HasField("isImportant") || !jsonobject["isImportant"].b) && (cyRandomTaskData.NPCLiuPai.Count <= 0 || cyRandomTaskData.NPCLiuPai.Contains(jsonobject.TryGetField("LiuPai").I)) && !this.lateEmailDict.ContainsKey(cyRandomTaskData.id) && !player.StreamData.TaskMag.HasTaskNpcList.Contains(num) && (!DateTime.TryParse(cyRandomTaskData.StarTime, out t) || !DateTime.TryParse(cyRandomTaskData.EndTime, out t2) || (!(nowTime < t) && !(nowTime > t2))) && (cyRandomTaskData.Level.Count <= 0 || ((int)player.level >= cyRandomTaskData.Level[0] && (int)player.level <= cyRandomTaskData.Level[1])) && (cyRandomTaskData.NPCLevel.Count <= 0 || (jsonobject["Level"].I >= cyRandomTaskData.NPCLevel[0] && jsonobject["Level"].I <= cyRandomTaskData.NPCLevel[1])) && (cyRandomTaskData.NPCXingGe.Count <= 0 || cyRandomTaskData.NPCXingGe.Contains(jsonobject["XingGe"].I)) && (cyRandomTaskData.NPCType.Count <= 0 || cyRandomTaskData.NPCType.Contains(jsonobject["Type"].I)) && (cyRandomTaskData.NPCTag.Count <= 0 || cyRandomTaskData.NPCTag.Contains(jsonobject["NPCTag"].I)) && (cyRandomTaskData.NPCXingWei.Count <= 0 || cyRandomTaskData.NPCXingWei.Contains(jsonobject["ActionId"].I)) && (cyRandomTaskData.NPCXingWei.Count <= 0 || cyRandomTaskData.NPCXingWei.Contains(jsonobject["ActionId"].I)))
				{
					if (cyRandomTaskData.NPCGuanXi.Count > 0)
					{
						bool flag = false;
						foreach (int num2 in cyRandomTaskData.NPCGuanXi)
						{
							if (flag)
							{
								break;
							}
							switch (num2)
							{
							case 1:
								if (PlayerEx.IsTheather(num))
								{
									flag = true;
								}
								break;
							case 2:
								if (PlayerEx.IsDaoLv(num))
								{
									flag = true;
								}
								break;
							case 3:
								if (PlayerEx.IsBrother(num))
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
						int i = jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
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
							if (jsonobject["wuDaoJson"][cyRandomTaskData.WuDaoType[j].ToString()]["level"].I < cyRandomTaskData.WuDaoLevel[j])
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
						bool flag3 = true;
						for (int k = 0; k < cyRandomTaskData.EventValue.Count; k++)
						{
							int num3 = cyRandomTaskData.fuhao[k];
							int id = cyRandomTaskData.EventValue[k];
							int num4 = cyRandomTaskData.EventValueNum[k];
							int num5 = GlobalValue.Get(id, string.Format("NpcJieSuanManager.SendFungusCy({0}) 第三代传音符fungus发送 变量判定", cyId));
							switch (num3)
							{
							case 1:
								if (num5 != num4)
								{
									flag3 = false;
								}
								break;
							case 2:
								if (num5 <= num4)
								{
									flag3 = false;
								}
								break;
							case 3:
								if (num5 >= num4)
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
					int randomInt = this.getRandomInt(1, 3);
					int duiBaiId = this.GetDuiBaiId(jsonobject["XingGe"].I, cyRandomTaskData.info);
					DateTime dateTime = this.GetNowTime().AddDays((double)Tools.instance.GetRandomInt(cyRandomTaskData.DelayTime[0], cyRandomTaskData.DelayTime[0]));
					RandomTask randomTask = new RandomTask(cyRandomTaskData.id, cyRandomTaskData.TaskID, cyRandomTaskData.TaskType, cyRandomTaskData.Taskvalue, cyRandomTaskData.NPCxingdong, cyRandomTaskData.valueID, cyRandomTaskData.value);
					player.emailDateMag.RandomTaskSendToPlayer(randomTask, num, duiBaiId, randomInt, cyRandomTaskData.XingWeiType, cyRandomTaskData.ItemID, cyRandomTaskData.ItemNum, dateTime.ToString());
				}
			}
		}
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000D6EEC File Offset: 0x000D50EC
	public List<int> GetJieShaNpcList(int index)
	{
		List<int> list = new List<int>();
		if (this.npcMap.bigMapNPCDictionary.ContainsKey(index) && this.npcMap.bigMapNPCDictionary[index].Count > 0)
		{
			foreach (int num in this.npcMap.bigMapNPCDictionary[index])
			{
				if (this.getNpcBigLevel(num) == Tools.instance.getPlayer().getLevelType() && jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I < 50 && this.getNpcData(num)["ActionId"].I == 34)
				{
					list.Add(num);
				}
			}
		}
		return list;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000D6FE0 File Offset: 0x000D51E0
	public List<int> GetXunLuoNpcList(string fubenName, int index)
	{
		List<int> list = new List<int>();
		Avatar player = Tools.instance.getPlayer();
		if (this.npcMap.fuBenNPCDictionary.ContainsKey(fubenName) && this.npcMap.fuBenNPCDictionary[fubenName].ContainsKey(index))
		{
			foreach (int num in this.npcMap.fuBenNPCDictionary[fubenName][index])
			{
				JSONObject npcData = this.getNpcData(num);
				if (npcData["MenPai"].I != (int)player.menPai && player.shengShi <= npcData["shengShi"].I)
				{
					list.Add(num);
				}
			}
		}
		return list;
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x000D70C4 File Offset: 0x000D52C4
	public int GetDuiBaiId(int XingGe, int type)
	{
		int result = 0;
		foreach (JSONObject jsonobject in jsonData.instance.CyNpcDuiBaiData.list)
		{
			if (jsonobject["XingGe"].I == XingGe && jsonobject["Type"].I == type)
			{
				result = jsonobject["id"].I;
			}
		}
		return result;
	}

	// Token: 0x04001339 RID: 4921
	public static NpcJieSuanManager inst;

	// Token: 0x0400133A RID: 4922
	public NpCFight npcFight;

	// Token: 0x0400133B RID: 4923
	private Random random;

	// Token: 0x0400133C RID: 4924
	public NPCXiuLian npcXiuLian;

	// Token: 0x0400133D RID: 4925
	public NPCTuPo npcTuPo;

	// Token: 0x0400133E RID: 4926
	public NpcSetField npcSetField;

	// Token: 0x0400133F RID: 4927
	public NPCShouJi npcShouJi;

	// Token: 0x04001340 RID: 4928
	public NPCFuYe npcFuYe;

	// Token: 0x04001341 RID: 4929
	public NPCUseItem npcUseItem;

	// Token: 0x04001342 RID: 4930
	public NPCLiLian npcLiLian;

	// Token: 0x04001343 RID: 4931
	public NPCStatus npcStatus;

	// Token: 0x04001344 RID: 4932
	public NpcTianJiGe npcTianJiGe;

	// Token: 0x04001345 RID: 4933
	public NPCTeShu npcTeShu;

	// Token: 0x04001346 RID: 4934
	public NPCNoteBook npcNoteBook;

	// Token: 0x04001347 RID: 4935
	public NPCMap npcMap;

	// Token: 0x04001348 RID: 4936
	public NPCSpeedJieSuan npcSpeedJieSuan;

	// Token: 0x04001349 RID: 4937
	public NPCDeath npcDeath;

	// Token: 0x0400134A RID: 4938
	public NPCChengHao npcChengHao;

	// Token: 0x0400134B RID: 4939
	public Dictionary<int, List<List<int>>> cyDictionary = new Dictionary<int, List<List<int>>>();

	// Token: 0x0400134C RID: 4940
	public List<CyPdData> cyPdAuToList;

	// Token: 0x0400134D RID: 4941
	public List<CyPdData> cyPdFungusList;

	// Token: 0x0400134E RID: 4942
	public Dictionary<int, Action<int>> ActionDictionary = new Dictionary<int, Action<int>>();

	// Token: 0x0400134F RID: 4943
	public Dictionary<int, Action<int>> NextActionDictionary = new Dictionary<int, Action<int>>();

	// Token: 0x04001350 RID: 4944
	public List<int> CurJieSuanNpcTaskList = new List<int>();

	// Token: 0x04001351 RID: 4945
	public Dictionary<int, int> ImportantNpcBangDingDictionary = new Dictionary<int, int>();

	// Token: 0x04001352 RID: 4946
	private Dictionary<int, int> NpcActionQuanZhongDictionary = new Dictionary<int, int>();

	// Token: 0x04001353 RID: 4947
	public Dictionary<int, List<int>> PaiMaiNpcDictionary = new Dictionary<int, List<int>>();

	// Token: 0x04001354 RID: 4948
	public List<int> allBigMapNpcList = new List<int>();

	// Token: 0x04001355 RID: 4949
	public List<int> JieShaNpcList = new List<int>();

	// Token: 0x04001356 RID: 4950
	public List<EmailData> lateEmailList = new List<EmailData>();

	// Token: 0x04001357 RID: 4951
	public Dictionary<int, EmailData> lateEmailDict = new Dictionary<int, EmailData>();

	// Token: 0x04001358 RID: 4952
	public List<int> lunDaoNpcList = new List<int>();

	// Token: 0x04001359 RID: 4953
	public List<List<int>> afterDeathList = new List<List<int>>();

	// Token: 0x0400135A RID: 4954
	[HideInInspector]
	public List<string> EquipNameList = new List<string>();

	// Token: 0x0400135B RID: 4955
	public bool isUpDateNpcList;

	// Token: 0x0400135C RID: 4956
	public bool isCanJieSuan = true;

	// Token: 0x0400135D RID: 4957
	public bool JieSuanAnimation;

	// Token: 0x0400135E RID: 4958
	public int JieSuanTimes;

	// Token: 0x0400135F RID: 4959
	public string JieSuanTime = "0001-1-1";

	// Token: 0x04001360 RID: 4960
	public bool IsNoJieSuan;
}
