using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using PaiMai;

// Token: 0x02000294 RID: 660
[Serializable]
public class EmailDataMag
{
	// Token: 0x060017B5 RID: 6069 RVA: 0x000A3634 File Offset: 0x000A1834
	public bool IsFriend(int npcId)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		return this.cyNpcList.Contains(npcId);
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x000A366C File Offset: 0x000A186C
	public EmailDataMag()
	{
		this.hasReadEmailDictionary = new Dictionary<string, List<EmailData>>();
		this.newEmailDictionary = new Dictionary<string, List<EmailData>>();
		this.cyNpcList = new List<int>();
		this.Init();
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x000A36BC File Offset: 0x000A18BC
	public void Init()
	{
		this.AnswerActionDict = new Dictionary<int, Action<EmailData, object>>();
		this.AnswerActionDict.Add(0, new Action<EmailData, object>(this.DoNothing));
		this.AnswerActionDict.Add(1, new Action<EmailData, object>(this.NpcToDongFu));
		this.AnswerActionDict.Add(2, new Action<EmailData, object>(this.NpcAnswerPaiMaiInfo));
		this.QuestionActionDict = new Dictionary<int, Action<EmailData, object>>();
		this.QuestionActionDict.Add(0, new Action<EmailData, object>(this.DoNothing));
		this.QuestionActionDict.Add(1, new Action<EmailData, object>(this.YaoQingNpcToDongFu));
		this.QuestionActionDict.Add(2, new Action<EmailData, object>(this.QuestionPaiMai));
		if (this.HasReceiveList == null)
		{
			this.HasReceiveList = new List<int>();
		}
		if (this.TagNpcList == null)
		{
			this.TagNpcList = new List<int>();
		}
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x000A3798 File Offset: 0x000A1998
	public void InitNewJson(JSONObject newJson)
	{
		this.newEmailDictionary = new Dictionary<string, List<EmailData>>();
		foreach (string text in newJson.keys)
		{
			foreach (JSONObject jsonobject in newJson[text].list)
			{
				EmailData emailData = new EmailData(jsonobject["npcId"].I, jsonobject["isOut"].b, jsonobject["isComplete"].b, new List<int>
				{
					jsonobject["content"][0].I,
					jsonobject["content"][1].I
				}, jsonobject["actionId"].I, new List<int>
				{
					jsonobject["item"][0].I,
					jsonobject["item"][1].I
				}, jsonobject["outTime"].I, jsonobject["addHaoGanDu"].I, jsonobject["sendTime"].Str);
				emailData.isOld = jsonobject["isOld"].b;
				emailData.isPlayer = jsonobject["isPlayer"].b;
				emailData.oldId = jsonobject["oldId"].I;
				emailData.isAnswer = jsonobject["isAnswer"].b;
				emailData.isPangBai = jsonobject["isPangBai"].b;
				emailData.answerId = jsonobject["answerId"].I;
				emailData.sceneName = jsonobject["sceneName"].Str;
				emailData.daoYaoStr = jsonobject["daoYaoStr"].Str;
				emailData.questionId = jsonobject["questionId"].I;
				emailData.xiaoYaoStr = jsonobject["xiaoYaoStr"].Str;
				emailData.npcName = jsonobject.TryGetField("npcName").Str;
				if (this.newEmailDictionary.ContainsKey(text))
				{
					this.newEmailDictionary[text].Add(emailData);
				}
				else
				{
					this.newEmailDictionary.Add(text, new List<EmailData>
					{
						emailData
					});
				}
			}
		}
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x000A3A80 File Offset: 0x000A1C80
	public void InitNewJson(Dictionary<string, List<EmailData>> dict)
	{
		this.newEmailDictionary = dict;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000A3A8C File Offset: 0x000A1C8C
	public void InitHasReadJson(JSONObject hasReadJson)
	{
		this.hasReadEmailDictionary = new Dictionary<string, List<EmailData>>();
		foreach (string text in hasReadJson.keys)
		{
			foreach (JSONObject jsonobject in hasReadJson[text].list)
			{
				EmailData emailData = new EmailData(jsonobject["npcId"].I, jsonobject["isOut"].b, jsonobject["isComplete"].b, new List<int>
				{
					jsonobject["content"][0].I,
					jsonobject["content"][1].I
				}, jsonobject["actionId"].I, new List<int>
				{
					jsonobject["item"][0].I,
					jsonobject["item"][1].I
				}, jsonobject["outTime"].I, jsonobject["addHaoGanDu"].I, jsonobject["sendTime"].Str);
				emailData.isOld = jsonobject["isOld"].b;
				emailData.isPlayer = jsonobject["isPlayer"].b;
				emailData.oldId = jsonobject["oldId"].I;
				emailData.isAnswer = jsonobject["isAnswer"].b;
				emailData.isPangBai = jsonobject["isPangBai"].b;
				emailData.answerId = jsonobject["answerId"].I;
				emailData.questionId = jsonobject["questionId"].I;
				emailData.sceneName = jsonobject["sceneName"].Str;
				emailData.daoYaoStr = jsonobject["daoYaoStr"].Str;
				emailData.xiaoYaoStr = jsonobject["xiaoYaoStr"].Str;
				emailData.npcName = jsonobject.TryGetField("npcName").Str;
				if (this.hasReadEmailDictionary.ContainsKey(text))
				{
					this.hasReadEmailDictionary[text].Add(emailData);
				}
				else
				{
					this.hasReadEmailDictionary.Add(text, new List<EmailData>
					{
						emailData
					});
				}
			}
		}
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000A3D74 File Offset: 0x000A1F74
	public JSONObject CyNpcListToJson()
	{
		JSONObject jsonobject = new JSONObject();
		foreach (int val in this.cyNpcList)
		{
			jsonobject.Add(val);
		}
		if (jsonobject.Count < 1)
		{
			return null;
		}
		return jsonobject;
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x000A3DDC File Offset: 0x000A1FDC
	public JSONObject NewToJson()
	{
		JSONObject jsonobject = new JSONObject();
		foreach (string text in this.newEmailDictionary.Keys)
		{
			foreach (EmailData emailData in this.newEmailDictionary[text])
			{
				JSONObject jsonobject2 = new JSONObject();
				JSONObject arr = JSONObject.arr;
				if (emailData.content != null && emailData.content.Count == 2)
				{
					arr.Add(emailData.content[0]);
					arr.Add(emailData.content[1]);
				}
				else
				{
					arr.Add(0);
					arr.Add(0);
				}
				JSONObject arr2 = JSONObject.arr;
				if (emailData.contentKey != null && emailData.contentKey.Count > 0)
				{
					using (List<int>.Enumerator enumerator3 = emailData.contentKey.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int val = enumerator3.Current;
							arr2.Add(val);
						}
						goto IL_106;
					}
					goto IL_FE;
				}
				goto IL_FE;
				IL_106:
				JSONObject arr3 = JSONObject.arr;
				if (emailData.item != null && emailData.item.Count == 2)
				{
					arr3.Add(emailData.item[0]);
					arr3.Add(emailData.item[1]);
				}
				else
				{
					arr3.Add(0);
					arr3.Add(0);
				}
				jsonobject2.SetField("npcId", emailData.npcId);
				jsonobject2.SetField("isOut", emailData.isOut);
				jsonobject2.SetField("isPlayer", emailData.isPlayer);
				jsonobject2.SetField("isOld", emailData.isOld);
				jsonobject2.SetField("oldId", emailData.oldId);
				jsonobject2.SetField("questionId", emailData.questionId);
				jsonobject2.SetField("isComplete", emailData.isComplete);
				jsonobject2.SetField("content", arr);
				jsonobject2.SetField("actionId", emailData.actionId);
				jsonobject2.SetField("isAnswer", emailData.isAnswer);
				jsonobject2.SetField("isPangBai", emailData.isPangBai);
				jsonobject2.SetField("answerId", emailData.answerId);
				jsonobject2.SetField("item", arr3);
				jsonobject2.SetField("outTime", emailData.outTime);
				jsonobject2.SetField("addHaoGanDu", emailData.addHaoGanDu);
				jsonobject2.SetField("sendTime", emailData.sendTime);
				jsonobject2.SetField("sceneName", emailData.sceneName);
				jsonobject2.SetField("daoYaoStr", emailData.daoYaoStr);
				jsonobject2.SetField("xiaoYaoStr", emailData.xiaoYaoStr);
				jsonobject2.SetField("npcName", emailData.npcName);
				if (jsonobject.HasField(text))
				{
					jsonobject[text].Add(jsonobject2);
					continue;
				}
				JSONObject arr4 = JSONObject.arr;
				arr4.Add(jsonobject2);
				jsonobject.SetField(text, arr4);
				continue;
				IL_FE:
				arr2.Add(-1);
				goto IL_106;
			}
		}
		if (jsonobject.keys == null)
		{
			return null;
		}
		return jsonobject;
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000A417C File Offset: 0x000A237C
	public JSONObject HasReadToJson()
	{
		JSONObject jsonobject = new JSONObject();
		foreach (string text in this.hasReadEmailDictionary.Keys)
		{
			foreach (EmailData emailData in this.hasReadEmailDictionary[text])
			{
				JSONObject jsonobject2 = new JSONObject();
				JSONObject arr = JSONObject.arr;
				if (emailData.content != null && emailData.content.Count == 2)
				{
					arr.Add(emailData.content[0]);
					arr.Add(emailData.content[1]);
				}
				else
				{
					arr.Add(0);
					arr.Add(0);
				}
				JSONObject arr2 = JSONObject.arr;
				if (emailData.contentKey != null && emailData.contentKey.Count > 0)
				{
					using (List<int>.Enumerator enumerator3 = emailData.contentKey.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int val = enumerator3.Current;
							arr2.Add(val);
						}
						goto IL_106;
					}
					goto IL_FE;
				}
				goto IL_FE;
				IL_106:
				JSONObject arr3 = JSONObject.arr;
				if (emailData.item != null && emailData.item.Count == 2)
				{
					arr3.Add(emailData.item[0]);
					arr3.Add(emailData.item[1]);
				}
				else
				{
					arr3.Add(0);
					arr3.Add(0);
				}
				jsonobject2.SetField("npcId", emailData.npcId);
				jsonobject2.SetField("isOut", emailData.isOut);
				jsonobject2.SetField("isPlayer", emailData.isPlayer);
				jsonobject2.SetField("isOld", emailData.isOld);
				jsonobject2.SetField("oldId", emailData.oldId);
				jsonobject2.SetField("questionId", emailData.questionId);
				jsonobject2.SetField("isAnswer", emailData.isAnswer);
				jsonobject2.SetField("isPangBai", emailData.isPangBai);
				jsonobject2.SetField("answerId", emailData.answerId);
				jsonobject2.SetField("isComplete", emailData.isComplete);
				jsonobject2.SetField("content", arr);
				jsonobject2.SetField("actionId", emailData.actionId);
				jsonobject2.SetField("item", arr3);
				jsonobject2.SetField("outTime", emailData.outTime);
				jsonobject2.SetField("addHaoGanDu", emailData.addHaoGanDu);
				jsonobject2.SetField("sendTime", emailData.sendTime);
				jsonobject2.SetField("sceneName", emailData.sceneName);
				jsonobject2.SetField("daoYaoStr", emailData.daoYaoStr);
				jsonobject2.SetField("xiaoYaoStr", emailData.xiaoYaoStr);
				jsonobject2.SetField("npcName", emailData.npcName);
				if (jsonobject.HasField(text))
				{
					jsonobject[text].Add(jsonobject2);
					continue;
				}
				JSONObject arr4 = JSONObject.arr;
				arr4.Add(jsonobject2);
				jsonobject.SetField(text, arr4);
				continue;
				IL_FE:
				arr2.Add(-1);
				goto IL_106;
			}
		}
		if (jsonobject.keys == null)
		{
			return null;
		}
		return jsonobject;
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000A451C File Offset: 0x000A271C
	public void RandomTaskSendToPlayer(RandomTask randomTask, int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, false, false, new List<int>
		{
			contentId,
			contentNum
		}, actionId, new List<int>
		{
			itemId,
			itemNum
		}, 500000, 0, sendTime);
		emailData.RandomTask = randomTask;
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][string.Format("dir{0}", contentNum)].Str;
		this.GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		NpcJieSuanManager.inst.lateEmailDict.Add(emailData.RandomTask.CyId, emailData);
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x000A4608 File Offset: 0x000A2808
	public void TaskFailSendToPlayer(int npcId, int contentId, int contentNum, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, false, false, new List<int>
		{
			contentId,
			contentNum
		}, 0, new List<int>
		{
			0,
			0
		}, 500000, 0, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][string.Format("dir{0}", contentNum)].Str;
		this.GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		this.AddNewEmail(npcId.ToString(), emailData);
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x000A46DC File Offset: 0x000A28DC
	public EmailData SendToPlayer(int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, int outTime, int addHaoGanDu, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, false, false, new List<int>
		{
			contentId,
			contentNum
		}, actionId, new List<int>
		{
			itemId,
			itemNum
		}, outTime, addHaoGanDu, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][string.Format("dir{0}", contentNum)].Str;
		this.GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		this.AddNewEmail(npcId.ToString(), emailData);
		return emailData;
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x000A47B4 File Offset: 0x000A29B4
	public EmailData SendToPlayerLate(int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, int outTime, int addHaoGanDu, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, false, false, new List<int>
		{
			contentId,
			contentNum
		}, actionId, new List<int>
		{
			itemId,
			itemNum
		}, outTime, addHaoGanDu, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][string.Format("dir{0}", contentNum)].Str;
		this.GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		NpcJieSuanManager.inst.lateEmailList.Add(emailData);
		return emailData;
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x000A488C File Offset: 0x000A2A8C
	public void SendToPlayerNoPd(int npcId, int contentId, int contentNum, string npcName, string sendTime)
	{
		this.SendToPlayer(npcId, contentId, contentNum, 999, 0, 0, 120, 0, sendTime).npcName = npcName;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000A48B8 File Offset: 0x000A2AB8
	public void AuToSendToPlayer(int npcId, int actionId, int answerType, string sendTime, object obj = null)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		CyNpcAnswerData cyNpcAnswerData = null;
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		if (npcData.HasField("LockAction"))
		{
			actionId = npcData["LockAction"].I;
		}
		foreach (CyNpcAnswerData cyNpcAnswerData2 in CyNpcAnswerData.DataList)
		{
			if (cyNpcAnswerData2.NPCActionID == actionId && cyNpcAnswerData2.AnswerType == answerType)
			{
				cyNpcAnswerData = cyNpcAnswerData2;
				break;
			}
		}
		bool isPangBai = cyNpcAnswerData.IsPangBai == 1;
		EmailData emailData = new EmailData(npcId, cyNpcAnswerData.id, true, isPangBai, sendTime);
		this.AnswerActionDict[cyNpcAnswerData.AnswerAction](emailData, obj);
		string duiHua = cyNpcAnswerData.DuiHua;
		emailData.contentKey = new List<int>();
		this.GetEmailContentKey(duiHua, emailData);
		if (duiHua.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		this.AddNewEmail(npcId.ToString(), emailData);
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000A49F4 File Offset: 0x000A2BF4
	public void SendToNpc(int questionId, int npcId, string sendTime, object obj = null)
	{
		EmailData emailData = new EmailData(npcId, questionId, true, sendTime);
		this.QuestionActionDict[CyPlayeQuestionData.DataDict[questionId].SendAction](emailData, obj);
		this.AddNewEmail(npcId.ToString(), emailData);
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["ActionId"].I;
		this.AuToSendToPlayer(npcId, i, questionId, sendTime, obj);
		this.NewToHasRead(npcId.ToString());
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000A4A7C File Offset: 0x000A2C7C
	public void OldToPlayer(int npcId, int oldId, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		Tools.instance.getPlayer().AddFriend(npcId);
		EmailData emailData = new EmailData(npcId, true, oldId, sendTime);
		JSONObject jsonobject = Tools.instance.getPlayer().NewChuanYingList[emailData.oldId.ToString()];
		int i = jsonData.instance.ChuanYingFuBiao[emailData.oldId.ToString()]["SPvalueID"].I;
		if (jsonData.instance.ChuanYingFuBiao[emailData.oldId.ToString()]["SPvalueID"].I > 0)
		{
			Tools.instance.getPlayer().StaticValue.Value[i] = Tools.instance.getPlayer().worldTimeMag.getNowTime().Year;
		}
		this.GetEmailContentKey(jsonobject["info"].Str, emailData);
		this.AddNewEmail(npcId.ToString(), emailData);
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000A4B94 File Offset: 0x000A2D94
	public void OldToPlayerByExchange(int npcId, int oldId, string sendTime)
	{
		Tools.instance.getPlayer().AddFriend(npcId);
		this.AddNewEmail(npcId.ToString(), new EmailData(npcId, true, oldId, sendTime));
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000A4BBC File Offset: 0x000A2DBC
	public void AddNewEmail(string npcId, EmailData data)
	{
		if (this.newEmailDictionary.ContainsKey(npcId))
		{
			this.newEmailDictionary[npcId].Add(data);
		}
		else
		{
			this.newEmailDictionary.Add(npcId, new List<EmailData>
			{
				data
			});
		}
		if (data.RandomTask != null)
		{
			if (this.HasReceiveList == null)
			{
				this.HasReceiveList = new List<int>();
			}
			this.HasReceiveList.Add(data.RandomTask.CyId);
		}
		if (CyUIMag.inst == null)
		{
			Loom.RunAsync(delegate
			{
				Loom.QueueOnMainThread(delegate(object obj)
				{
					UIPopTip.Inst.Pop("收到新的传音符", PopTipIconType.传音符);
					UIHeadPanel.Inst.ChuanYinFuPoint.SetActive(true);
				}, null);
			});
		}
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x000A4C68 File Offset: 0x000A2E68
	public void NewToHasRead(string npcId)
	{
		if (this.newEmailDictionary.ContainsKey(npcId))
		{
			foreach (EmailData item in this.newEmailDictionary[npcId])
			{
				if (this.hasReadEmailDictionary.ContainsKey(npcId))
				{
					this.hasReadEmailDictionary[npcId].Add(item);
				}
				else
				{
					this.hasReadEmailDictionary.Add(npcId, new List<EmailData>
					{
						item
					});
				}
			}
			this.newEmailDictionary.Remove(npcId);
		}
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000A4D10 File Offset: 0x000A2F10
	public void GetEmailContentKey(string msg, EmailData emailData)
	{
		int npcId = emailData.npcId;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		Avatar player = Tools.instance.getPlayer();
		int num;
		if (npcId < 20000)
		{
			num = -1;
		}
		else
		{
			num = jsonobject["MenPai"].I;
		}
		if (msg.Contains("{daoyou}"))
		{
			if (num != 0)
			{
				if (num == (int)player.menPai)
				{
					if (jsonobject["Level"].I >= (int)player.level)
					{
						if (player.Sex == 1)
						{
							emailData.daoYaoStr = "师兄";
						}
						else
						{
							emailData.daoYaoStr = "师姐";
						}
					}
					else if (player.Sex == 1)
					{
						emailData.daoYaoStr = "师弟";
					}
					else
					{
						emailData.daoYaoStr = "师妹";
					}
				}
				else
				{
					emailData.daoYaoStr = "道友";
				}
			}
			else
			{
				emailData.daoYaoStr = "道友";
			}
			if (PlayerEx.IsBrother(npcId))
			{
				if (player.Sex == 1)
				{
					emailData.daoYaoStr = "兄弟";
				}
				else
				{
					emailData.daoYaoStr = "姑娘";
				}
			}
			else
			{
				emailData.daoYaoStr = "道友";
			}
			if (PlayerEx.IsTuDi(npcId))
			{
				emailData.daoYaoStr = "师傅";
			}
			if (PlayerEx.IsTheather(npcId))
			{
				emailData.daoYaoStr = "{LastName}";
			}
			if (PlayerEx.IsDaoLv(npcId))
			{
				emailData.xiaoYaoStr = "{LastName}";
				return;
			}
		}
		else if (msg.Contains("{xiaoyou}"))
		{
			if (num != 0)
			{
				if (num == (int)player.menPai)
				{
					emailData.xiaoYaoStr = "师侄";
				}
			}
			else
			{
				emailData.xiaoYaoStr = "小友";
			}
			if (PlayerEx.IsBrother(npcId))
			{
				if (player.Sex == 1)
				{
					emailData.xiaoYaoStr = "兄弟";
				}
				else
				{
					emailData.xiaoYaoStr = "姑娘";
				}
			}
			if (PlayerEx.IsTuDi(npcId))
			{
				emailData.xiaoYaoStr = "师傅";
			}
			if (PlayerEx.IsTheather(npcId))
			{
				emailData.xiaoYaoStr = "{LastName}";
			}
			if (PlayerEx.IsDaoLv(npcId))
			{
				emailData.xiaoYaoStr = "{LastName}";
			}
		}
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x00004095 File Offset: 0x00002295
	private void DoNothing(EmailData email, object obj)
	{
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000A4F00 File Offset: 0x000A3100
	private void NpcToDongFu(EmailData email, object obj)
	{
		if (PlayerEx.IsDaoLv(email.npcId))
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(email.npcId, 21);
		}
		else
		{
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(email.npcId, 20);
		}
		jsonData.instance.AvatarJsonData[email.npcId.ToString()].SetField("DongFuId", (int)obj);
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x000A4F74 File Offset: 0x000A3174
	private void NpcAnswerPaiMaiInfo(EmailData email, object obj)
	{
		int num = (int)obj;
		PaiMaiBiao paiMaiBiao = PaiMaiBiao.DataDict[num];
		PaiMaiData shopInfo = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.GetShopInfo(num);
		int value = shopInfo.NextUpdateTime.Year - DateTime.Parse(paiMaiBiao.StarTime).Year;
		DateTime startTime = DateTime.Parse(paiMaiBiao.StarTime).AddYears(value);
		DateTime endTime = DateTime.Parse(paiMaiBiao.EndTime).AddYears(value);
		CyPaiMaiInfo paiMaiInfo = new CyPaiMaiInfo
		{
			StartTime = startTime,
			EndTime = endTime,
			ItemList = new List<int>(shopInfo.ShopList),
			PaiMaiId = num
		};
		email.PaiMaiInfo = paiMaiInfo;
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000A5035 File Offset: 0x000A3235
	private void YaoQingNpcToDongFu(EmailData emailData, object obj)
	{
		emailData.DongFuId = (int)obj;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000A5043 File Offset: 0x000A3243
	public void QuestionPaiMai(EmailData emailData, object obj)
	{
		jsonData.instance.AvatarJsonData[emailData.npcId.ToString()].SetField("ActionId", 1000);
	}

	// Token: 0x0400127E RID: 4734
	public Dictionary<string, List<EmailData>> hasReadEmailDictionary;

	// Token: 0x0400127F RID: 4735
	public Dictionary<string, List<EmailData>> newEmailDictionary;

	// Token: 0x04001280 RID: 4736
	public List<int> TagNpcList;

	// Token: 0x04001281 RID: 4737
	public List<int> cyNpcList;

	// Token: 0x04001282 RID: 4738
	public List<int> HasReceiveList;

	// Token: 0x04001283 RID: 4739
	public bool IsStopAll;

	// Token: 0x04001284 RID: 4740
	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> AnswerActionDict = new Dictionary<int, Action<EmailData, object>>();

	// Token: 0x04001285 RID: 4741
	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> QuestionActionDict = new Dictionary<int, Action<EmailData, object>>();
}
