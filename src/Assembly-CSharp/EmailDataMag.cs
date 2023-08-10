using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using PaiMai;
using UnityEngine;

[Serializable]
public class EmailDataMag
{
	public Dictionary<string, List<EmailData>> hasReadEmailDictionary;

	public Dictionary<string, List<EmailData>> newEmailDictionary;

	public List<int> TagNpcList;

	public List<int> cyNpcList;

	public List<int> HasReceiveList;

	public bool IsStopAll;

	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> AnswerActionDict = new Dictionary<int, Action<EmailData, object>>();

	[NonSerialized]
	public Dictionary<int, Action<EmailData, object>> QuestionActionDict = new Dictionary<int, Action<EmailData, object>>();

	public bool IsFriend(int npcId)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		if (cyNpcList.Contains(npcId))
		{
			return true;
		}
		return false;
	}

	public EmailDataMag()
	{
		hasReadEmailDictionary = new Dictionary<string, List<EmailData>>();
		newEmailDictionary = new Dictionary<string, List<EmailData>>();
		cyNpcList = new List<int>();
		Init();
	}

	public void Init()
	{
		AnswerActionDict = new Dictionary<int, Action<EmailData, object>>();
		AnswerActionDict.Add(0, DoNothing);
		AnswerActionDict.Add(1, NpcToDongFu);
		AnswerActionDict.Add(2, NpcAnswerPaiMaiInfo);
		QuestionActionDict = new Dictionary<int, Action<EmailData, object>>();
		QuestionActionDict.Add(0, DoNothing);
		QuestionActionDict.Add(1, YaoQingNpcToDongFu);
		QuestionActionDict.Add(2, QuestionPaiMai);
		if (HasReceiveList == null)
		{
			HasReceiveList = new List<int>();
		}
		if (TagNpcList == null)
		{
			TagNpcList = new List<int>();
		}
	}

	public void InitNewJson(JSONObject newJson)
	{
		newEmailDictionary = new Dictionary<string, List<EmailData>>();
		foreach (string key in newJson.keys)
		{
			foreach (JSONObject item in newJson[key].list)
			{
				EmailData emailData = new EmailData(item["npcId"].I, item["isOut"].b, item["isComplete"].b, new List<int>
				{
					item["content"][0].I,
					item["content"][1].I
				}, item["actionId"].I, new List<int>
				{
					item["item"][0].I,
					item["item"][1].I
				}, item["outTime"].I, item["addHaoGanDu"].I, item["sendTime"].Str);
				emailData.isOld = item["isOld"].b;
				emailData.isPlayer = item["isPlayer"].b;
				emailData.oldId = item["oldId"].I;
				emailData.isAnswer = item["isAnswer"].b;
				emailData.isPangBai = item["isPangBai"].b;
				emailData.answerId = item["answerId"].I;
				emailData.sceneName = item["sceneName"].Str;
				emailData.daoYaoStr = item["daoYaoStr"].Str;
				emailData.questionId = item["questionId"].I;
				emailData.xiaoYaoStr = item["xiaoYaoStr"].Str;
				emailData.npcName = item.TryGetField("npcName").Str;
				if (newEmailDictionary.ContainsKey(key))
				{
					newEmailDictionary[key].Add(emailData);
					continue;
				}
				newEmailDictionary.Add(key, new List<EmailData> { emailData });
			}
		}
	}

	public void InitNewJson(Dictionary<string, List<EmailData>> dict)
	{
		newEmailDictionary = dict;
	}

	public void InitHasReadJson(JSONObject hasReadJson)
	{
		hasReadEmailDictionary = new Dictionary<string, List<EmailData>>();
		foreach (string key in hasReadJson.keys)
		{
			foreach (JSONObject item in hasReadJson[key].list)
			{
				EmailData emailData = new EmailData(item["npcId"].I, item["isOut"].b, item["isComplete"].b, new List<int>
				{
					item["content"][0].I,
					item["content"][1].I
				}, item["actionId"].I, new List<int>
				{
					item["item"][0].I,
					item["item"][1].I
				}, item["outTime"].I, item["addHaoGanDu"].I, item["sendTime"].Str);
				emailData.isOld = item["isOld"].b;
				emailData.isPlayer = item["isPlayer"].b;
				emailData.oldId = item["oldId"].I;
				emailData.isAnswer = item["isAnswer"].b;
				emailData.isPangBai = item["isPangBai"].b;
				emailData.answerId = item["answerId"].I;
				emailData.questionId = item["questionId"].I;
				emailData.sceneName = item["sceneName"].Str;
				emailData.daoYaoStr = item["daoYaoStr"].Str;
				emailData.xiaoYaoStr = item["xiaoYaoStr"].Str;
				emailData.npcName = item.TryGetField("npcName").Str;
				if (hasReadEmailDictionary.ContainsKey(key))
				{
					hasReadEmailDictionary[key].Add(emailData);
					continue;
				}
				hasReadEmailDictionary.Add(key, new List<EmailData> { emailData });
			}
		}
	}

	public JSONObject CyNpcListToJson()
	{
		JSONObject jSONObject = new JSONObject();
		foreach (int cyNpc in cyNpcList)
		{
			jSONObject.Add(cyNpc);
		}
		if (jSONObject.Count < 1)
		{
			return null;
		}
		return jSONObject;
	}

	public JSONObject NewToJson()
	{
		JSONObject jSONObject = new JSONObject();
		foreach (string key in newEmailDictionary.Keys)
		{
			foreach (EmailData item in newEmailDictionary[key])
			{
				JSONObject jSONObject2 = new JSONObject();
				JSONObject arr = JSONObject.arr;
				if (item.content != null && item.content.Count == 2)
				{
					arr.Add(item.content[0]);
					arr.Add(item.content[1]);
				}
				else
				{
					arr.Add(0);
					arr.Add(0);
				}
				JSONObject arr2 = JSONObject.arr;
				if (item.contentKey != null && item.contentKey.Count > 0)
				{
					foreach (int item2 in item.contentKey)
					{
						arr2.Add(item2);
					}
				}
				else
				{
					arr2.Add(-1);
				}
				JSONObject arr3 = JSONObject.arr;
				if (item.item != null && item.item.Count == 2)
				{
					arr3.Add(item.item[0]);
					arr3.Add(item.item[1]);
				}
				else
				{
					arr3.Add(0);
					arr3.Add(0);
				}
				jSONObject2.SetField("npcId", item.npcId);
				jSONObject2.SetField("isOut", item.isOut);
				jSONObject2.SetField("isPlayer", item.isPlayer);
				jSONObject2.SetField("isOld", item.isOld);
				jSONObject2.SetField("oldId", item.oldId);
				jSONObject2.SetField("questionId", item.questionId);
				jSONObject2.SetField("isComplete", item.isComplete);
				jSONObject2.SetField("content", arr);
				jSONObject2.SetField("actionId", item.actionId);
				jSONObject2.SetField("isAnswer", item.isAnswer);
				jSONObject2.SetField("isPangBai", item.isPangBai);
				jSONObject2.SetField("answerId", item.answerId);
				jSONObject2.SetField("item", arr3);
				jSONObject2.SetField("outTime", item.outTime);
				jSONObject2.SetField("addHaoGanDu", item.addHaoGanDu);
				jSONObject2.SetField("sendTime", item.sendTime);
				jSONObject2.SetField("sceneName", item.sceneName);
				jSONObject2.SetField("daoYaoStr", item.daoYaoStr);
				jSONObject2.SetField("xiaoYaoStr", item.xiaoYaoStr);
				jSONObject2.SetField("npcName", item.npcName);
				if (jSONObject.HasField(key))
				{
					jSONObject[key].Add(jSONObject2);
					continue;
				}
				JSONObject arr4 = JSONObject.arr;
				arr4.Add(jSONObject2);
				jSONObject.SetField(key, arr4);
			}
		}
		if (jSONObject.keys == null)
		{
			return null;
		}
		return jSONObject;
	}

	public JSONObject HasReadToJson()
	{
		JSONObject jSONObject = new JSONObject();
		foreach (string key in hasReadEmailDictionary.Keys)
		{
			foreach (EmailData item in hasReadEmailDictionary[key])
			{
				JSONObject jSONObject2 = new JSONObject();
				JSONObject arr = JSONObject.arr;
				if (item.content != null && item.content.Count == 2)
				{
					arr.Add(item.content[0]);
					arr.Add(item.content[1]);
				}
				else
				{
					arr.Add(0);
					arr.Add(0);
				}
				JSONObject arr2 = JSONObject.arr;
				if (item.contentKey != null && item.contentKey.Count > 0)
				{
					foreach (int item2 in item.contentKey)
					{
						arr2.Add(item2);
					}
				}
				else
				{
					arr2.Add(-1);
				}
				JSONObject arr3 = JSONObject.arr;
				if (item.item != null && item.item.Count == 2)
				{
					arr3.Add(item.item[0]);
					arr3.Add(item.item[1]);
				}
				else
				{
					arr3.Add(0);
					arr3.Add(0);
				}
				jSONObject2.SetField("npcId", item.npcId);
				jSONObject2.SetField("isOut", item.isOut);
				jSONObject2.SetField("isPlayer", item.isPlayer);
				jSONObject2.SetField("isOld", item.isOld);
				jSONObject2.SetField("oldId", item.oldId);
				jSONObject2.SetField("questionId", item.questionId);
				jSONObject2.SetField("isAnswer", item.isAnswer);
				jSONObject2.SetField("isPangBai", item.isPangBai);
				jSONObject2.SetField("answerId", item.answerId);
				jSONObject2.SetField("isComplete", item.isComplete);
				jSONObject2.SetField("content", arr);
				jSONObject2.SetField("actionId", item.actionId);
				jSONObject2.SetField("item", arr3);
				jSONObject2.SetField("outTime", item.outTime);
				jSONObject2.SetField("addHaoGanDu", item.addHaoGanDu);
				jSONObject2.SetField("sendTime", item.sendTime);
				jSONObject2.SetField("sceneName", item.sceneName);
				jSONObject2.SetField("daoYaoStr", item.daoYaoStr);
				jSONObject2.SetField("xiaoYaoStr", item.xiaoYaoStr);
				jSONObject2.SetField("npcName", item.npcName);
				if (jSONObject.HasField(key))
				{
					jSONObject[key].Add(jSONObject2);
					continue;
				}
				JSONObject arr4 = JSONObject.arr;
				arr4.Add(jSONObject2);
				jSONObject.SetField(key, arr4);
			}
		}
		if (jSONObject.keys == null)
		{
			return null;
		}
		return jSONObject;
	}

	public void RandomTaskSendToPlayer(RandomTask randomTask, int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, isOut: false, isComplete: false, new List<int> { contentId, contentNum }, actionId, new List<int> { itemId, itemNum }, 500000, 0, sendTime);
		emailData.RandomTask = randomTask;
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][$"dir{contentNum}"].Str;
		GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		NpcJieSuanManager.inst.lateEmailDict.Add(emailData.RandomTask.CyId, emailData);
	}

	public void TaskFailSendToPlayer(int npcId, int contentId, int contentNum, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, isOut: false, isComplete: false, new List<int> { contentId, contentNum }, 0, new List<int> { 0, 0 }, 500000, 0, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][$"dir{contentNum}"].Str;
		GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		AddNewEmail(npcId.ToString(), emailData);
	}

	public EmailData SendToPlayer(int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, int outTime, int addHaoGanDu, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, isOut: false, isComplete: false, new List<int> { contentId, contentNum }, actionId, new List<int> { itemId, itemNum }, outTime, addHaoGanDu, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][$"dir{contentNum}"].Str;
		GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		AddNewEmail(npcId.ToString(), emailData);
		return emailData;
	}

	public EmailData SendToPlayerLate(int npcId, int contentId, int contentNum, int actionId, int itemId, int itemNum, int outTime, int addHaoGanDu, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		EmailData emailData = new EmailData(npcId, isOut: false, isComplete: false, new List<int> { contentId, contentNum }, actionId, new List<int> { itemId, itemNum }, outTime, addHaoGanDu, sendTime);
		string str = jsonData.instance.CyNpcDuiBaiData[contentId.ToString()][$"dir{contentNum}"].Str;
		GetEmailContentKey(str, emailData);
		if (str.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		NpcJieSuanManager.inst.lateEmailList.Add(emailData);
		return emailData;
	}

	public void SendToPlayerNoPd(int npcId, int contentId, int contentNum, string npcName, string sendTime)
	{
		SendToPlayer(npcId, contentId, contentNum, 999, 0, 0, 120, 0, sendTime).npcName = npcName;
	}

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
		foreach (CyNpcAnswerData data in CyNpcAnswerData.DataList)
		{
			if (data.NPCActionID == actionId && data.AnswerType == answerType)
			{
				cyNpcAnswerData = data;
				break;
			}
		}
		bool isPangBai = cyNpcAnswerData.IsPangBai == 1;
		EmailData emailData = new EmailData(npcId, cyNpcAnswerData.id, isAnswer: true, isPangBai, sendTime);
		AnswerActionDict[cyNpcAnswerData.AnswerAction](emailData, obj);
		string duiHua = cyNpcAnswerData.DuiHua;
		emailData.contentKey = new List<int>();
		GetEmailContentKey(duiHua, emailData);
		if (duiHua.Contains("{DiDian}"))
		{
			emailData.sceneName = NpcJieSuanManager.inst.npcMap.GetNpcSceneName(npcId);
		}
		AddNewEmail(npcId.ToString(), emailData);
	}

	public void SendToNpc(int questionId, int npcId, string sendTime, object obj = null)
	{
		EmailData emailData = new EmailData(npcId, questionId, isPlayer: true, sendTime);
		QuestionActionDict[CyPlayeQuestionData.DataDict[questionId].SendAction](emailData, obj);
		AddNewEmail(npcId.ToString(), emailData);
		int i = jsonData.instance.AvatarJsonData[npcId.ToString()]["ActionId"].I;
		AuToSendToPlayer(npcId, i, questionId, sendTime, obj);
		NewToHasRead(npcId.ToString());
	}

	public void OldToPlayer(int npcId, int oldId, string sendTime)
	{
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
		{
			npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
		}
		Tools.instance.getPlayer().AddFriend(npcId);
		EmailData emailData = new EmailData(npcId, isOld: true, oldId, sendTime);
		JSONObject jSONObject = Tools.instance.getPlayer().NewChuanYingList[emailData.oldId.ToString()];
		int i = jsonData.instance.ChuanYingFuBiao[emailData.oldId.ToString()]["SPvalueID"].I;
		if (jsonData.instance.ChuanYingFuBiao[emailData.oldId.ToString()]["SPvalueID"].I > 0)
		{
			Tools.instance.getPlayer().StaticValue.Value[i] = Tools.instance.getPlayer().worldTimeMag.getNowTime().Year;
		}
		GetEmailContentKey(jSONObject["info"].Str, emailData);
		AddNewEmail(npcId.ToString(), emailData);
	}

	public void OldToPlayerByExchange(int npcId, int oldId, string sendTime)
	{
		Tools.instance.getPlayer().AddFriend(npcId);
		AddNewEmail(npcId.ToString(), new EmailData(npcId, isOld: true, oldId, sendTime));
	}

	public void AddNewEmail(string npcId, EmailData data)
	{
		if (newEmailDictionary.ContainsKey(npcId))
		{
			newEmailDictionary[npcId].Add(data);
		}
		else
		{
			newEmailDictionary.Add(npcId, new List<EmailData> { data });
		}
		if (data.RandomTask != null)
		{
			if (HasReceiveList == null)
			{
				HasReceiveList = new List<int>();
			}
			HasReceiveList.Add(data.RandomTask.CyId);
		}
		if (!((Object)(object)CyUIMag.inst == (Object)null))
		{
			return;
		}
		Loom.RunAsync(delegate
		{
			Loom.QueueOnMainThread(delegate
			{
				UIPopTip.Inst.Pop("收到新的传音符", PopTipIconType.传音符);
				UIHeadPanel.Inst.ChuanYinFuPoint.SetActive(true);
			}, null);
		});
	}

	public void NewToHasRead(string npcId)
	{
		if (!newEmailDictionary.ContainsKey(npcId))
		{
			return;
		}
		foreach (EmailData item in newEmailDictionary[npcId])
		{
			if (hasReadEmailDictionary.ContainsKey(npcId))
			{
				hasReadEmailDictionary[npcId].Add(item);
				continue;
			}
			hasReadEmailDictionary.Add(npcId, new List<EmailData> { item });
		}
		newEmailDictionary.Remove(npcId);
	}

	public void GetEmailContentKey(string msg, EmailData emailData)
	{
		int npcId = emailData.npcId;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		num = ((npcId >= 20000) ? jSONObject["MenPai"].I : (-1));
		if (msg.Contains("{daoyou}"))
		{
			if (num != 0)
			{
				if (num == player.menPai)
				{
					if (jSONObject["Level"].I >= player.level)
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
			}
		}
		else
		{
			if (!msg.Contains("{xiaoyou}"))
			{
				return;
			}
			if (num != 0)
			{
				if (num == player.menPai)
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

	private void DoNothing(EmailData email, object obj)
	{
	}

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

	private void YaoQingNpcToDongFu(EmailData emailData, object obj)
	{
		emailData.DongFuId = (int)obj;
	}

	public void QuestionPaiMai(EmailData emailData, object obj)
	{
		jsonData.instance.AvatarJsonData[emailData.npcId.ToString()].SetField("ActionId", 1000);
	}
}
