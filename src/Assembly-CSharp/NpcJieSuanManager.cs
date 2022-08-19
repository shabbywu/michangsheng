using System;
using System.Collections.Generic;
using System.Linq;
using CaiJi;
using JSONClass;
using KBEngine;
using script.MenPaiTask;
using UnityEngine;
using YSGame.TianJiDaBi;

// Token: 0x02000215 RID: 533
public class NpcJieSuanManager : MonoBehaviour
{
	// Token: 0x0600154D RID: 5453 RVA: 0x00089544 File Offset: 0x00087744
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

	// Token: 0x0600154E RID: 5454 RVA: 0x000895AC File Offset: 0x000877AC
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

	// Token: 0x0600154F RID: 5455 RVA: 0x000899B8 File Offset: 0x00087BB8
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

	// Token: 0x06001550 RID: 5456 RVA: 0x00089ABB File Offset: 0x00087CBB
	public DateTime GetNowTime()
	{
		return DateTime.Parse(this.JieSuanTime);
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x00089AC8 File Offset: 0x00087CC8
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

	// Token: 0x06001552 RID: 5458 RVA: 0x0008A0C4 File Offset: 0x000882C4
	public void NpcJieSuan(int times, bool isCanChanger = true)
	{
		int num = 0;
		foreach (int num2 in PlayerEx.Player.emailDateMag.cyNpcList)
		{
			if (!NPCEx.IsDeath(num))
			{
				num++;
			}
		}
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
							int num3 = 12;
							if (actionTimes == 1)
							{
								num3 *= 2;
							}
							int i = times - num3;
							int num4 = i / num3;
							num3--;
							while (i >= num4)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num4);
								this.JieSuanTimes += num4;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num4).ToString();
								i -= num4;
								if (num3 > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num3--;
								}
							}
							if (i > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(i);
								this.JieSuanTimes += i;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(i).ToString();
							}
							for (int j = 0; j < num3; j++)
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
								this.npcSpeedJieSuan.DoSpeedJieSuan(num7);
								this.JieSuanTimes += num7;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num7).ToString();
								num6 -= num7;
								if (num5 > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num5--;
								}
							}
							if (num6 > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num6);
								this.JieSuanTimes += num6;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num6).ToString();
							}
							for (int k = 0; k < num5; k++)
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
								this.npcSpeedJieSuan.DoSpeedJieSuan(num10);
								this.JieSuanTimes += num10;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num10).ToString();
								num9 -= num10;
								if (num8 > 0)
								{
									this.RandomNpcAction();
									this.JieSuanTimes++;
									this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(1).ToString();
									num8--;
								}
							}
							if (num9 > 0)
							{
								this.npcSpeedJieSuan.DoSpeedJieSuan(num9);
								this.JieSuanTimes += num9;
								this.JieSuanTime = DateTime.Parse(this.JieSuanTime).AddMonths(num9).ToString();
							}
							for (int l = 0; l < num8; l++)
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

	// Token: 0x06001553 RID: 5459 RVA: 0x0008A1A0 File Offset: 0x000883A0
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
						JSONObject jsonobject = jsonData.instance.randomAvatarFace(avatarJsonData[text], null);
						jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(avatarJsonData[text]["id"].I), jsonobject.Copy());
					}
				}
				if (!jsonData.instance.AvatarBackpackJsonData.HasField(text))
				{
					FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, avatarJsonData[text]["id"].I, avatarJsonData[text]);
				}
			}
		}
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0008A334 File Offset: 0x00088534
	public void PaiMaiAction()
	{
		if (this.PaiMaiNpcDictionary.Count > 0)
		{
			this.npcTeShu.NextNpcPaiMai();
			this.PaiMaiNpcDictionary = new Dictionary<int, List<int>>();
		}
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x0008A35A File Offset: 0x0008855A
	public void LunDaoAction()
	{
		if (this.lunDaoNpcList.Count >= 2)
		{
			this.npcXiuLian.NextNpcLunDao();
			return;
		}
		this.lunDaoNpcList = new List<int>();
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x0008A384 File Offset: 0x00088584
	public void RandomNpcAction()
	{
		this.PaiMaiAction();
		this.LunDaoAction();
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
		List<int> list = new List<int>();
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
					else if (!Tools.instance.getPlayer().ElderTaskMag.GetExecutingTaskNpcIdList().Contains(num))
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
						if (randomActionID == 35 && !list.Contains(num))
						{
							list.Add(num);
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
				List<int> list2 = new List<int>();
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
								this.GetNpcData(emailData2.npcId).SetField("ActionId", randomTask.LockActionId);
								this.GetNpcData(emailData2.npcId).SetField("LockAction", randomTask.LockActionId);
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
					list2.Add(num2);
					player.emailDateMag.AddNewEmail(emailData2.npcId.ToString(), emailData2);
				}
				foreach (int key in list2)
				{
					this.lateEmailDict.Remove(key);
				}
			}
		}
		Tools.instance.getPlayer().StreamData.TaskMag.CheckHasOut();
		this.CheckMenPaiTask();
		Tools.instance.getPlayer().ElderTaskMag.UpdateTaskProcess.CheckHasExecutingTask();
		foreach (int npcId in list)
		{
			Tools.instance.getPlayer().ElderTaskMag.AddCanAccpetNpcIdList(npcId);
		}
		Tools.instance.getPlayer().ElderTaskMag.AllotTask.GetCanAccpetNpcList();
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x0008AAF0 File Offset: 0x00088CF0
	public void CheckMenPaiTask()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.menPai <= 0)
		{
			return;
		}
		int zhangMenId = this.GetZhangMenId((int)player.menPai);
		Dictionary<int, MenPaiFengLuBiao> dataDict = MenPaiFengLuBiao.DataDict;
		if (player.chengHao >= 6 && player.chengHao <= 9 && PlayerEx.GetMenPaiShengWang() < dataDict[player.chengHao].MenKan)
		{
			player.chengHao = 5;
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

	// Token: 0x06001558 RID: 5464 RVA: 0x0008ABB0 File Offset: 0x00088DB0
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

	// Token: 0x06001559 RID: 5465 RVA: 0x0008AC78 File Offset: 0x00088E78
	public Dictionary<int, int> getFinallyNpcActionQuanZhongDictionary(JSONObject npcDate, Dictionary<int, int> dictionary)
	{
		int i = npcDate["NPCTag"].I;
		NPCTagDate npctagDate = NPCTagDate.DataDict[i];
		for (int j = 0; j < npctagDate.Change.Count; j++)
		{
			if (dictionary.ContainsKey(npctagDate.Change[j]))
			{
				int key;
				if (npctagDate.Change[j] == 35 && Tools.instance.getPlayer().ElderTaskMag.GetWaitAcceptTaskList().Count > 0)
				{
					key = npctagDate.Change[j];
					dictionary[key] += npctagDate.ChangeTo[j];
				}
				key = npctagDate.Change[j];
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
						goto IL_CDD;
					}
					goto IL_CDD;
				case 2:
				{
					int month = this.GetNowTime().Month;
					if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && this.GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && month >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][0].I && month <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["YueFen"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				}
				case 3:
				case 4:
					if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && this.GetNpcBeiBaoAllItemSum(npcDate["id"].I) >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["BeiBao"].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 5:
				case 6:
				case 7:
				case 8:
					if (this.PaiMaiIsOpen(jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiTime"].I) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["LingShi"].I && npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I && npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I == jsonData.instance.NPCActionPanDingDate[i2.ToString()]["PaiMaiType"].I)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 9:
					if (this.IsCanChangeEquip(npcDate) != 0)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 10:
					if (!this.IsCanLianDan(npcDate["id"].I))
					{
						dictionary[num] = 0;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 11:
					if (!this.IsCanLianQi(npcDate["id"].I))
					{
						dictionary[num] = 0;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 12:
					if (this.npcStatus.IsInTargetStatus(npcDate["id"].I, 2) && jsonData.instance.AvatarBackpackJsonData[npcDate["id"].I.ToString()]["money"].I >= jsonData.instance.NPCTuPuoDate[npcDate["Level"].I.ToString()]["LingShiPanDuan"].I && !npcDate["isImportant"].b)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 13:
					if (this.npcStatus.IsInTargetStatus(npcDate["id"].I, 2))
					{
						dictionary[1] = 0;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 14:
					if (this.GetNpcShengYuTime(npcDate["id"].I) < 10)
					{
						int key = num;
						dictionary[key] += jsonData.instance.NPCActionPanDingDate[i2.ToString()]["ChangeTo"].I;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 15:
					if (npcDate["Level"].I == 1)
					{
						dictionary[num] = 0;
						goto IL_CDD;
					}
					goto IL_CDD;
				case 16:
					goto IL_CDD;
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
					if (NPCActionPanDingDate.DataDict[i2].JingJie[0] > npcDate["Level"].I || NPCActionPanDingDate.DataDict[i2].JingJie[1] < npcDate["Level"].I || npcDate["paimaifenzu"][this.getRandomInt(0, npcDate["paimaifenzu"].Count - 1)].I != NPCActionPanDingDate.DataDict[i2].PaiMaiType)
					{
						goto IL_CDD;
					}
					if (!this.npcMap.fuBenNPCDictionary.ContainsKey("F" + 26))
					{
						int key = num;
						dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
						goto IL_CDD;
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
						goto IL_CDD;
					}
					break;
				case 23:
					break;
				default:
					goto IL_CDD;
				}
				if (npcDate["Level"].I >= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][0].I && npcDate["Level"].I <= jsonData.instance.NPCActionPanDingDate[i2.ToString()]["JingJie"][1].I)
				{
					int key = num;
					dictionary[key] += NPCActionPanDingDate.DataDict[i2].ChangeTo;
				}
			}
			IL_CDD:;
		}
		return dictionary;
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0008B9A0 File Offset: 0x00089BA0
	public bool PaiMaiIsOpen(int paimaiHangID)
	{
		string str = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["StarTime"].str;
		string str2 = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["EndTime"].str;
		int i = jsonData.instance.PaiMaiBiao[paimaiHangID.ToString()]["circulation"].I;
		return Tools.instance.IsInTime(DateTime.Parse(this.JieSuanTime), DateTime.Parse(str), DateTime.Parse(str2), i);
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x0008BA44 File Offset: 0x00089C44
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

	// Token: 0x0600155C RID: 5468 RVA: 0x0008BC60 File Offset: 0x00089E60
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

	// Token: 0x0600155D RID: 5469 RVA: 0x0008BD78 File Offset: 0x00089F78
	public int GetNpcBeiBaoAllItemSum(int npcID)
	{
		int num = 0;
		int index = NpcJieSuanManager.inst.GetNpcBigLevel(npcID) - 1;
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

	// Token: 0x0600155E RID: 5470 RVA: 0x0008BED8 File Offset: 0x0008A0D8
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

	// Token: 0x0600155F RID: 5471 RVA: 0x0008BFE8 File Offset: 0x0008A1E8
	public int GetEquipLevel(int quality, int shangXia)
	{
		return quality * 3 - (3 - shangXia);
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0008BFF1 File Offset: 0x0008A1F1
	public int getRandomInt(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0008C004 File Offset: 0x0008A204
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

	// Token: 0x06001562 RID: 5474 RVA: 0x0008C0C0 File Offset: 0x0008A2C0
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

	// Token: 0x06001563 RID: 5475 RVA: 0x0008C140 File Offset: 0x0008A340
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

	// Token: 0x06001564 RID: 5476 RVA: 0x0008C200 File Offset: 0x0008A400
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

	// Token: 0x06001565 RID: 5477 RVA: 0x0008C2C4 File Offset: 0x0008A4C4
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

	// Token: 0x06001566 RID: 5478 RVA: 0x0008C3DC File Offset: 0x0008A5DC
	public void RemoveItem(int npcId, int itemId, int count, string uid)
	{
		if (count == 1)
		{
			this.RemoveItemByUid(npcId, uid);
			return;
		}
		this.RemoveItemById(npcId, itemId, count);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x0008C3F8 File Offset: 0x0008A5F8
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

	// Token: 0x06001568 RID: 5480 RVA: 0x0008C68C File Offset: 0x0008A88C
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

	// Token: 0x06001569 RID: 5481 RVA: 0x0008C800 File Offset: 0x0008AA00
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

	// Token: 0x0600156A RID: 5482 RVA: 0x0008CA68 File Offset: 0x0008AC68
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

	// Token: 0x0600156B RID: 5483 RVA: 0x0008CB30 File Offset: 0x0008AD30
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

	// Token: 0x0600156C RID: 5484 RVA: 0x0008CBFC File Offset: 0x0008ADFC
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

	// Token: 0x0600156D RID: 5485 RVA: 0x0008CC3F File Offset: 0x0008AE3F
	public void DoNothing(int npcId)
	{
		this.npcTeShu.NpcAddDoSomething(npcId);
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x0008CC4D File Offset: 0x0008AE4D
	public JSONObject GetNpcData(int npcId)
	{
		if (jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
		{
			return jsonData.instance.AvatarJsonData[npcId.ToString()];
		}
		return null;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x0008CC7F File Offset: 0x0008AE7F
	public bool IsInScope(int cur, int min, int max)
	{
		return cur >= min && cur <= max;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x0008CC8C File Offset: 0x0008AE8C
	public bool ImprotantNpcActionPanDing(int npcId)
	{
		JSONObject npcData = this.GetNpcData(npcId);
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

	// Token: 0x06001571 RID: 5489 RVA: 0x0008CF14 File Offset: 0x0008B114
	public int GetNpcShengYuTime(int npcId)
	{
		JSONObject npcData = this.GetNpcData(npcId);
		int num = npcData["age"].I / 12;
		return npcData["shouYuan"].I - num;
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x0008CF50 File Offset: 0x0008B150
	public void GuDingAddExp(int npcId, float times = 1f)
	{
		JSONObject npcData = this.GetNpcData(npcId);
		npcData.SetField("isTanChaUnlock", false);
		if (npcData["isImportant"].b)
		{
			int npcBigLevel = this.GetNpcBigLevel(npcId);
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

	// Token: 0x06001573 RID: 5491 RVA: 0x0008D0C0 File Offset: 0x0008B2C0
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

	// Token: 0x06001574 RID: 5492 RVA: 0x0008D130 File Offset: 0x0008B330
	public void CheckImportantEvent(string nowTime)
	{
		DateTime t = DateTime.Parse(nowTime);
		Tools.instance.getPlayer();
		foreach (JSONObject jsonobject in jsonData.instance.NpcImprotantEventData.list)
		{
			if (this.ImportantNpcBangDingDictionary.ContainsKey(jsonobject["ImportantNPC"].I))
			{
				int npcId = this.ImportantNpcBangDingDictionary[jsonobject["ImportantNPC"].I];
				JSONObject npcData = this.GetNpcData(npcId);
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

	// Token: 0x06001575 RID: 5493 RVA: 0x0008D40C File Offset: 0x0008B60C
	public bool IsNeedHelp()
	{
		return this.getRandomInt(0, 100) <= 30;
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x0008D420 File Offset: 0x0008B620
	public bool IsDeath(int npcId)
	{
		return this.npcDeath.npcDeathJson.HasField(npcId.ToString()) || (this.npcDeath.npcDeathJson.HasField("deathImportantList") && this.npcDeath.npcDeathJson["deathImportantList"].ToList().Contains(npcId));
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x0008D482 File Offset: 0x0008B682
	public bool IsFly(int npcId)
	{
		return jsonData.instance.AvatarJsonData.HasField(npcId.ToString()) && this.GetNpcData(npcId).HasField("IsFly");
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x0008D4B0 File Offset: 0x0008B6B0
	public void SendMessage(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = this.GetNpcData(npcId);
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
					else
					{
						int num = player.AliveFriendCount - 3;
						if (num <= 0)
						{
							num = 1;
						}
						if (this.getRandomInt(0, 100) > cyPdData.baseRate / num)
						{
							continue;
						}
					}
					if (cyPdData.qingFen != 1 || npcData.TryGetField("QingFen").I >= cyPdData.itemPrice)
					{
						int randomInt = this.getRandomInt(1, 3);
						int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
						if (cyPdData.actionId != 0)
						{
							List<int> item = cyPdData.GetItem(npcId);
							if (item.Count < 1)
							{
								continue;
							}
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

	// Token: 0x06001579 RID: 5497 RVA: 0x0008D8C4 File Offset: 0x0008BAC4
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
						JSONObject npcData = this.GetNpcData(num);
						int i = jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
						if ((!cyPdData.isOnly || !npcData.HasField("CyList") || !npcData["CyList"].ToList().Contains(cyPdData.cyType)) && cyPdData.npcActionList.Contains(npcData["ActionId"].I) && (cyPdData.npcType == 0 || cyPdData.npcType == npcData["Type"].I) && npcData["Level"].I >= cyPdData.minLevel && npcData["Level"].I <= cyPdData.maxLevel && (cyPdData.staticFuHao <= 0 || cyPdData.StaticValuePd()) && (cyPdData.needHaoGanDu <= 0 || cyPdData.HaoGanPd(i)) && cyPdData.IsinTime() && (cyPdData.npcState == 0 || cyPdData.npcState == npcData["Status"]["StatusId"].I) && this.getRandomInt(0, 100) <= cyPdData.GetRate(i))
						{
							int randomInt = this.getRandomInt(1, 3);
							int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
							if (cyPdData.actionId != 0)
							{
								List<int> item = cyPdData.GetItem(num);
								if (item.Count < 1)
								{
									continue;
								}
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

	// Token: 0x0600157A RID: 5498 RVA: 0x0008DC1C File Offset: 0x0008BE1C
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
				JSONObject npcData = this.GetNpcData(npcId);
				int i = jsonData.instance.AvatarRandomJsonData[npcId.ToString()]["HaoGanDu"].I;
				if ((!cyPdData.isOnly || !npcData.HasField("CyList") || !npcData["CyList"].ToList().Contains(cyPdData.cyType)) && cyPdData.npcActionList.Contains(npcData["ActionId"].I) && (cyPdData.npcType == 0 || cyPdData.npcType == npcData["Type"].I) && npcData["Level"].I >= cyPdData.minLevel && npcData["Level"].I <= cyPdData.maxLevel && (cyPdData.staticFuHao <= 0 || cyPdData.StaticValuePd()) && (cyPdData.needHaoGanDu <= 0 || cyPdData.HaoGanPd(i)) && cyPdData.IsinTime() && (cyPdData.npcState == 0 || cyPdData.npcState == npcData["Status"]["StatusId"].I) && this.getRandomInt(0, 100) <= cyPdData.GetRate(i))
				{
					int randomInt = this.getRandomInt(1, 3);
					int duiBaiId = this.GetDuiBaiId(npcData["XingGe"].I, cyPdData.talkId);
					if (cyPdData.actionId != 0)
					{
						List<int> item = cyPdData.GetItem(npcId);
						if (item.Count < 1)
						{
							continue;
						}
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

	// Token: 0x0600157B RID: 5499 RVA: 0x0008DEEC File Offset: 0x0008C0EC
	public void SendCy(int npcId)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.emailDateMag.IsStopAll)
		{
			return;
		}
		JSONObject npcData = this.GetNpcData(npcId);
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

	// Token: 0x0600157C RID: 5500 RVA: 0x0008E62C File Offset: 0x0008C82C
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
				jsonobject = this.GetNpcData(num);
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

	// Token: 0x0600157D RID: 5501 RVA: 0x0008EC30 File Offset: 0x0008CE30
	public List<int> GetJieShaNpcList(int index)
	{
		List<int> list = new List<int>();
		if (this.npcMap.bigMapNPCDictionary.ContainsKey(index) && this.npcMap.bigMapNPCDictionary[index].Count > 0)
		{
			foreach (int num in this.npcMap.bigMapNPCDictionary[index])
			{
				if (this.GetNpcBigLevel(num) == Tools.instance.getPlayer().getLevelType() && jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I < 50 && this.GetNpcData(num)["ActionId"].I == 34)
				{
					list.Add(num);
				}
			}
		}
		return list;
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0008ED24 File Offset: 0x0008CF24
	public List<int> GetXunLuoNpcList(string fubenName, int index)
	{
		List<int> list = new List<int>();
		Avatar player = Tools.instance.getPlayer();
		if (this.npcMap.fuBenNPCDictionary.ContainsKey(fubenName) && this.npcMap.fuBenNPCDictionary[fubenName].ContainsKey(index))
		{
			foreach (int num in this.npcMap.fuBenNPCDictionary[fubenName][index])
			{
				JSONObject npcData = this.GetNpcData(num);
				if (npcData["MenPai"].I != (int)player.menPai && player.shengShi <= npcData["shengShi"].I)
				{
					list.Add(num);
				}
			}
		}
		return list;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x0008EE08 File Offset: 0x0008D008
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

	// Token: 0x04000FE9 RID: 4073
	public static NpcJieSuanManager inst;

	// Token: 0x04000FEA RID: 4074
	public NpCFight npcFight;

	// Token: 0x04000FEB RID: 4075
	private Random random;

	// Token: 0x04000FEC RID: 4076
	public NPCXiuLian npcXiuLian;

	// Token: 0x04000FED RID: 4077
	public NPCTuPo npcTuPo;

	// Token: 0x04000FEE RID: 4078
	public NpcSetField npcSetField;

	// Token: 0x04000FEF RID: 4079
	public NPCShouJi npcShouJi;

	// Token: 0x04000FF0 RID: 4080
	public NPCFuYe npcFuYe;

	// Token: 0x04000FF1 RID: 4081
	public NPCUseItem npcUseItem;

	// Token: 0x04000FF2 RID: 4082
	public NPCLiLian npcLiLian;

	// Token: 0x04000FF3 RID: 4083
	public NPCStatus npcStatus;

	// Token: 0x04000FF4 RID: 4084
	public NpcTianJiGe npcTianJiGe;

	// Token: 0x04000FF5 RID: 4085
	public NPCTeShu npcTeShu;

	// Token: 0x04000FF6 RID: 4086
	public NPCNoteBook npcNoteBook;

	// Token: 0x04000FF7 RID: 4087
	public NPCMap npcMap;

	// Token: 0x04000FF8 RID: 4088
	public NPCSpeedJieSuan npcSpeedJieSuan;

	// Token: 0x04000FF9 RID: 4089
	public NPCDeath npcDeath;

	// Token: 0x04000FFA RID: 4090
	public NPCChengHao npcChengHao;

	// Token: 0x04000FFB RID: 4091
	public Dictionary<int, List<List<int>>> cyDictionary = new Dictionary<int, List<List<int>>>();

	// Token: 0x04000FFC RID: 4092
	public List<CyPdData> cyPdAuToList;

	// Token: 0x04000FFD RID: 4093
	public List<CyPdData> cyPdFungusList;

	// Token: 0x04000FFE RID: 4094
	public Dictionary<int, Action<int>> ActionDictionary = new Dictionary<int, Action<int>>();

	// Token: 0x04000FFF RID: 4095
	public Dictionary<int, Action<int>> NextActionDictionary = new Dictionary<int, Action<int>>();

	// Token: 0x04001000 RID: 4096
	public List<int> CurJieSuanNpcTaskList = new List<int>();

	// Token: 0x04001001 RID: 4097
	public Dictionary<int, int> ImportantNpcBangDingDictionary = new Dictionary<int, int>();

	// Token: 0x04001002 RID: 4098
	private Dictionary<int, int> NpcActionQuanZhongDictionary = new Dictionary<int, int>();

	// Token: 0x04001003 RID: 4099
	public Dictionary<int, List<int>> PaiMaiNpcDictionary = new Dictionary<int, List<int>>();

	// Token: 0x04001004 RID: 4100
	public List<int> allBigMapNpcList = new List<int>();

	// Token: 0x04001005 RID: 4101
	public List<int> JieShaNpcList = new List<int>();

	// Token: 0x04001006 RID: 4102
	public List<EmailData> lateEmailList = new List<EmailData>();

	// Token: 0x04001007 RID: 4103
	public Dictionary<int, EmailData> lateEmailDict = new Dictionary<int, EmailData>();

	// Token: 0x04001008 RID: 4104
	public List<int> lunDaoNpcList = new List<int>();

	// Token: 0x04001009 RID: 4105
	public List<List<int>> afterDeathList = new List<List<int>>();

	// Token: 0x0400100A RID: 4106
	[HideInInspector]
	public List<string> EquipNameList = new List<string>();

	// Token: 0x0400100B RID: 4107
	public bool isUpDateNpcList;

	// Token: 0x0400100C RID: 4108
	public bool isCanJieSuan = true;

	// Token: 0x0400100D RID: 4109
	public bool JieSuanAnimation;

	// Token: 0x0400100E RID: 4110
	public int JieSuanTimes;

	// Token: 0x0400100F RID: 4111
	public string JieSuanTime = "0001-1-1";

	// Token: 0x04001010 RID: 4112
	public bool IsNoJieSuan;
}
