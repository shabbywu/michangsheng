using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using PaiMai;

// Token: 0x020003C3 RID: 963
[Serializable]
public class EmailDataMag
{
	// Token: 0x06001A92 RID: 6802 RVA: 0x000169D8 File Offset: 0x00014BD8
	public bool IsFriend(int npcId)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		return this.cyNpcList.Contains(npcId);
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000EA7E0 File Offset: 0x000E89E0
	public EmailDataMag()
	{
		this.hasReadEmailDictionary = new Dictionary<string, List<EmailData>>();
		this.newEmailDictionary = new Dictionary<string, List<EmailData>>();
		this.cyNpcList = new List<int>();
		this.Init();
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000EA830 File Offset: 0x000E8A30
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

	// Token: 0x06001A95 RID: 6805 RVA: 0x000EA90C File Offset: 0x000E8B0C
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

	// Token: 0x06001A96 RID: 6806 RVA: 0x00016A0F File Offset: 0x00014C0F
	public void InitNewJson(Dictionary<string, List<EmailData>> dict)
	{
		this.newEmailDictionary = dict;
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x000EABF4 File Offset: 0x000E8DF4
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

	// Token: 0x06001A98 RID: 6808 RVA: 0x000EAEDC File Offset: 0x000E90DC
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

	// Token: 0x06001A99 RID: 6809 RVA: 0x000EAF44 File Offset: 0x000E9144
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

	// Token: 0x06001A9A RID: 6810 RVA: 0x000EB2E4 File Offset: 0x000E94E4
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

	// Token: 0x06001A9B RID: 6811 RVA: 0x000EB684 File Offset: 0x000E9884
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

	// Token: 0x06001A9C RID: 6812 RVA: 0x000EB770 File Offset: 0x000E9970
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

	// Token: 0x06001A9D RID: 6813 RVA: 0x000EB844 File Offset: 0x000E9A44
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

	// Token: 0x06001A9E RID: 6814 RVA: 0x000EB91C File Offset: 0x000E9B1C
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

	// Token: 0x06001A9F RID: 6815 RVA: 0x000EB9F4 File Offset: 0x000E9BF4
	public void SendToPlayerNoPd(int npcId, int contentId, int contentNum, string npcName, string sendTime)
	{
		this.SendToPlayer(npcId, contentId, contentNum, 999, 0, 0, 120, 0, sendTime).npcName = npcName;
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x000EBA20 File Offset: 0x000E9C20
	public void AuToSendToPlayer(int npcId, int actionId, int answerType, string sendTime, object obj = null)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		CyNpcAnswerData cyNpcAnswerData = null;
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
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

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000EBB5C File Offset: 0x000E9D5C
	public void SendToNpc(int questionId, int npcId, string sendTime, object obj = null)
	{
		EmailData emailData = new EmailData(npcId, questionId, true, sendTime);
		this.QuestionActionDict[CyPlayeQuestionData.DataDict[questionId].SendAction](emailData, obj);
		this.AddNewEmail(npcId.ToString(), emailData);
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["ActionId"].I;
		this.AuToSendToPlayer(npcId, i, questionId, sendTime, obj);
		this.NewToHasRead(npcId.ToString());
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000EBBE4 File Offset: 0x000E9DE4
	public void OldToPlayer(int npcId, int oldId, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		Tools.instance.getPlayer().AddFriend(npcId);
		EmailData data = new EmailData(npcId, true, oldId, sendTime);
		this.AddNewEmail(npcId.ToString(), data);
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000EBC40 File Offset: 0x000E9E40
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

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000EBCEC File Offset: 0x000E9EEC
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

	// Token: 0x06001AA5 RID: 6821 RVA: 0x000EBD94 File Offset: 0x000E9F94
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

	// Token: 0x06001AA6 RID: 6822 RVA: 0x000042DD File Offset: 0x000024DD
	private void DoNothing(EmailData email, object obj)
	{
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000EBF84 File Offset: 0x000EA184
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

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000EBFF8 File Offset: 0x000EA1F8
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

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00016A18 File Offset: 0x00014C18
	private void YaoQingNpcToDongFu(EmailData emailData, object obj)
	{
		emailData.DongFuId = (int)obj;
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x00016A26 File Offset: 0x00014C26
	public void QuestionPaiMai(EmailData emailData, object obj)
	{
		jsonData.instance.AvatarJsonData[emailData.npcId.ToString()].SetField("ActionId", 1000);
	}

	// Token: 0x04001604 RID: 5636
	public Dictionary<string, List<EmailData>> hasReadEmailDictionary;

	// Token: 0x04001605 RID: 5637
	public Dictionary<string, List<EmailData>> newEmailDictionary;

	// Token: 0x04001606 RID: 5638
	public List<int> TagNpcList;

	// Token: 0x04001607 RID: 5639
	public List<int> cyNpcList;

	// Token: 0x04001608 RID: 5640
	public List<int> HasReceiveList;

	// Token: 0x04001609 RID: 5641
	public bool IsStopAll;

	// Token: 0x0400160A RID: 5642
	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> AnswerActionDict = new Dictionary<int, Action<EmailData, object>>();

	// Token: 0x0400160B RID: 5643
	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> QuestionActionDict = new Dictionary<int, Action<EmailData, object>>();
}
