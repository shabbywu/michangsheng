using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KBEngine;

public class NomelTaskMag
{
	private Avatar avatar;

	private static int DeDaiSetWhereNodeCount;

	private List<JSONObject> _NowNTaskData;

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

	public NomelTaskMag(Avatar _avatar)
	{
		avatar = _avatar;
	}

	public void restAllTaskType()
	{
		foreach (NTaskAllType data in NTaskAllType.DataList)
		{
			try
			{
				randomTask(data.Id);
			}
			catch (Exception ex)
			{
				Debug.Log((object)ex);
			}
		}
	}

	public void ResetAllStaticNTask()
	{
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.StaticNTaksTime)
		{
			int type = (int)item.Value[(object)"fenzu"];
			List<JToken> list = Tools.FindAllJTokens((JToken)(object)jsonData.instance.StaticNTaks, (JToken aa) => (int)aa[(object)"fenzu"] == type);
			if (list.Count == 0)
			{
				Debug.LogError((object)$"固定时间获得任务表没有fenzu {type}");
				continue;
			}
			bool flag = true;
			if (avatar.StaticNTaskTime.ContainsKey("StaticTime" + type))
			{
				DateTime startTime = DateTime.Parse((string)avatar.StaticNTaskTime["StaticTime" + type]);
				DateTime endTime = startTime.AddMonths((int)avatar.StaticNTaskTime["CD" + type]);
				if (Tools.instance.IsInTime(avatar.worldTimeMag.getNowTime(), startTime, endTime))
				{
					flag = false;
				}
			}
			if (flag)
			{
				avatar.StaticNTaskTime["StaticTime" + type] = JToken.op_Implicit(avatar.worldTimeMag.nowTime);
				avatar.StaticNTaskTime["CD" + type] = JToken.op_Implicit(Tools.getRandomInt((int)item.Value[(object)"CD"][(object)0], (int)item.Value[(object)"CD"][(object)1]));
				int taskID = (int)list[jsonData.GetRandom() % list.Count][(object)"TypeId"];
				if (!IsNTaskStart(taskID))
				{
					StartNTask(taskID);
				}
			}
		}
	}

	public void DeDaiSetWhereNode(int taskID, bool digui = false)
	{
		if (digui)
		{
			DeDaiSetWhereNodeCount++;
		}
		else
		{
			DeDaiSetWhereNodeCount = 0;
		}
		if (DeDaiSetWhereNodeCount > 1000)
		{
			Debug.LogError((object)$"DeDaiSetWhereNode({taskID})递归超过1000次，强制终止");
			return;
		}
		List<JSONObject> nowNTask = GetNowNTask();
		string text = "";
		if (!avatar.NomelTaskJson[taskID.ToString()].HasField("TaskWhereChild"))
		{
			return;
		}
		foreach (JSONObject item in avatar.NomelTaskJson[taskID.ToString()]["TaskWhereChild"].list)
		{
			if (NTaskSuiJI.DataDict.ContainsKey(item.I))
			{
				text = NTaskSuiJI.DataDict[item.I].StrValue;
			}
		}
		foreach (JSONObject item2 in nowNTask)
		{
			int i = item2["id"].I;
			List<JSONObject> nTaskXiangXiList = avatar.nomelTaskMag.GetNTaskXiangXiList(i);
			int num = 0;
			foreach (JSONObject item3 in nTaskXiangXiList)
			{
				_ = item3;
				int whereChilidID = getWhereChilidID(i, num);
				if (NTaskSuiJI.DataDict.ContainsKey(whereChilidID) && NTaskSuiJI.DataDict[whereChilidID].StrValue == text)
				{
					randomTask(taskID, Reset: true);
					DeDaiSetWhereNode(taskID, digui: true);
					return;
				}
				num++;
			}
		}
	}

	public void StartNTask(int taskID, int type = 1)
	{
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		avatar.NomelTaskFlag.SetField(taskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
		avatar.NomelTaskJson[taskID.ToString()].SetField("IsStart", val: true);
		avatar.NomelTaskJson[taskID.ToString()].SetField("StartTime", avatar.worldTimeMag.nowTime);
		avatar.NomelTaskJson[taskID.ToString()].SetField("IsFirstStart", val: true);
		if (!NTaskAllType.DataDict[taskID].seid.Contains(1) && type == 1)
		{
			UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
		}
		try
		{
			foreach (JSONObject item in avatar.NomelTaskJson[taskID.ToString()]["TaskWhereChild"].list)
			{
				if (NTaskSuiJI.DataDict.ContainsKey(item.I))
				{
					string strValue = NTaskSuiJI.DataDict[item.I].StrValue;
					JToken val = avatar.RandomFuBenList[strValue];
					if (val != null && ((JObject)val).ContainsKey("ShouldReset"))
					{
						val[(object)"ShouldReset"] = JToken.op_Implicit(true);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		avatar.nomelTaskMag.RefreshGetNowNTaskData();
	}

	public void TimeOutEndTask(int taskID)
	{
		setNTaskEnd(taskID);
		NTaskAllType nTaskAllType = NTaskAllType.DataDict[taskID];
		if (!nTaskAllType.seid.Contains(1))
		{
			UIPopTip.Inst.Pop("一条委托任务已失效", PopTipIconType.任务进度);
		}
		NTaskXiangXi nTaskXiangXiData = GetNTaskXiangXiData(taskID);
		if (nTaskAllType.shili >= 0 && nTaskXiangXiData.ShiLIAdd > 0)
		{
			avatar.SetMenPaiHaoGandu(nTaskAllType.shili, -nTaskXiangXiData.ShiLIReduce);
		}
		if (nTaskAllType.GeRen > 0 && nTaskXiangXiData.GeRenAdd > 0)
		{
			avatar.setAvatarHaoGandu(nTaskAllType.GeRen, -nTaskXiangXiData.GeRenReduce);
		}
	}

	public void setNTaskEnd(int taskID)
	{
		avatar.NomelTaskJson[taskID.ToString()].SetField("IsEnd", val: true);
		avatar.NomelTaskJson[taskID.ToString()].SetField("IsStart", val: false);
		avatar.nomelTaskMag.RefreshGetNowNTaskData();
	}

	public void EndNTask(int taskID)
	{
		issueReward(taskID);
		setNTaskEnd(taskID);
	}

	public void getReward(int taskID, ref int money, ref int menpaihuobi)
	{
		List<JSONObject> list = avatar.NomelTaskJson[taskID.ToString()]["TaskChild"].list;
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(taskID);
		GetNTaskXiangXiData(taskID);
		JSONObject nTaskXiangXiJson = GetNTaskXiangXiJson(taskID);
		int num = 0;
		foreach (JSONObject item in list)
		{
			_ = item;
			float n = nTaskXiangXiList[num]["num"].n;
			float n2 = nTaskXiangXiJson["shouYiLu"].n;
			NTaskSuiJI nTaskSuiJI = NTaskSuiJI.DataDict[list[num].I];
			JSONObject jSONObject = jsonData.instance.NTaskSuiJI[list[num].I.ToString()];
			money += (int)((float)nTaskSuiJI.jiaZhi * n * n2);
			menpaihuobi += (int)(jSONObject["huobi"].n * n * n2);
			string _talkID = nTaskXiangXiList[num]["talkID"].str;
			JSONObject jSONObject2 = jsonData.instance.NTaskSuiJI.list.Find((JSONObject _aa) => _aa["Str"].str == _talkID);
			if (_talkID != "0" && jSONObject2 != null)
			{
				money += (int)(jSONObject2["jiaZhi"].n * n2);
				menpaihuobi += (int)(jSONObject2["huobi"].n * n2);
			}
			num++;
		}
	}

	public int GetTaskMoney(int taskID)
	{
		int num = 0;
		List<JSONObject> list = avatar.NomelTaskJson[taskID.ToString()]["TaskChild"].list;
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(taskID);
		JSONObject nTaskXiangXiJson = GetNTaskXiangXiJson(taskID);
		int num2 = 0;
		foreach (JSONObject item in list)
		{
			_ = item;
			float n = nTaskXiangXiList[num2]["num"].n;
			float n2 = nTaskXiangXiJson["shouYiLu"].n;
			num += (int)((float)NTaskSuiJI.DataDict[list[num2].I].jiaZhi * n * n2);
			num2++;
		}
		return num;
	}

	public void issueReward(int taskID)
	{
		int money = 0;
		int menpaihuobi = 0;
		getReward(taskID, ref money, ref menpaihuobi);
		NTaskAllType nTaskAllType = NTaskAllType.DataDict[taskID];
		int menpaihuobi2 = nTaskAllType.menpaihuobi;
		JSONObject jSONObject = GetNTaskXiangXiList(taskID).Find((JSONObject aa) => aa["type"].I == 6);
		if (menpaihuobi2 == 0 && money > 0 && !nTaskAllType.seid.Contains(4) && jSONObject == null)
		{
			avatar.AddMoney(money);
		}
		if (menpaihuobi2 > 0 && menpaihuobi > 0)
		{
			avatar.addItem(menpaihuobi2, menpaihuobi, Tools.CreateItemSeid(menpaihuobi2), ShowText: true);
		}
		EndNTaskSeid(taskID);
		NTaskXiangXi nTaskXiangXiData = GetNTaskXiangXiData(taskID);
		if (nTaskAllType.shili >= 0 && nTaskXiangXiData.ShiLIAdd > 0)
		{
			avatar.SetMenPaiHaoGandu(nTaskAllType.shili, nTaskXiangXiData.ShiLIAdd);
		}
		if (nTaskAllType.GeRen > 0 && nTaskXiangXiData.GeRenAdd > 0)
		{
			avatar.setAvatarHaoGandu(nTaskAllType.GeRen, nTaskXiangXiData.GeRenAdd);
		}
		PlayTutorial.CheckChuTaXianTu2(taskID);
	}

	public bool IsNTaskStart(int taskID)
	{
		return avatar.NomelTaskJson[taskID.ToString()]["IsStart"].b;
	}

	public bool ISNTaskTimeOut(int taskID)
	{
		if (IsNTaskStart(taskID))
		{
			NTaskXiangXi nTaskXiangXiData = GetNTaskXiangXiData(taskID);
			DateTime startTime = DateTime.Parse(avatar.NomelTaskJson[taskID.ToString()]["StartTime"].str);
			DateTime endTime = startTime.AddMonths(nTaskXiangXiData.shiXian);
			if (Tools.instance.IsInTime(avatar.worldTimeMag.getNowTime(), startTime, endTime))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsNTaskOutCD(int taskID)
	{
		if (!ISNTaskTimeOut(taskID))
		{
			return false;
		}
		if (IsNTaskStart(taskID))
		{
			NTaskAllType nTaskAllType = NTaskAllType.DataDict[taskID];
			DateTime startTime = DateTime.Parse(avatar.NomelTaskJson[taskID.ToString()]["RandomTime"].str);
			DateTime endTime = startTime.AddMonths(nTaskAllType.CD);
			if (Tools.instance.IsInTime(avatar.worldTimeMag.getNowTime(), startTime, endTime))
			{
				return false;
			}
		}
		return true;
	}

	public void autoSetNtask()
	{
		foreach (JSONObject item in GetNowNTask())
		{
			if (IsNTaskStart(item["id"].I) && ISNTaskTimeOut(item["id"].I))
			{
				TimeOutEndTask(item["id"].I);
			}
		}
	}

	public void RefreshGetNowNTaskData()
	{
		JSONObject nomelTaskJson = avatar.NomelTaskJson;
		List<JSONObject> list = new List<JSONObject>();
		int num = 0;
		JSONObject jSONObject = null;
		try
		{
			int count = nomelTaskJson.keys.Count;
			for (num = 0; num < count; num++)
			{
				jSONObject = nomelTaskJson.list[num];
				if (jSONObject["IsStart"].b)
				{
					list.Add(jSONObject);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("任务触发异常，异常信息:\n" + ex.Message + "\n" + ex.StackTrace));
		}
		_NowNTaskData = list;
	}

	public List<JSONObject> GetNowNTask()
	{
		if (_NowNTaskData == null)
		{
			RefreshGetNowNTaskData();
		}
		return _NowNTaskData;
	}

	public bool HasNTask(int TaskID)
	{
		return avatar.NomelTaskJson.list.Find((JSONObject aa) => aa["IsStart"].b && aa["id"].I == TaskID) != null;
	}

	public JSONObject GetNTaskXiangXiJson(int TaskID)
	{
		int i = avatar.NomelTaskJson[TaskID.ToString()]["TaskID"].I;
		return jsonData.instance.NTaskXiangXi[i.ToString()];
	}

	public NTaskXiangXi GetNTaskXiangXiData(int TaskID)
	{
		JSONObject jSONObject = avatar.NomelTaskJson[TaskID.ToString()];
		if (jSONObject != null)
		{
			int i = jSONObject["TaskID"].I;
			return NTaskXiangXi.DataDict[i];
		}
		Debug.LogError((object)$"Player.NomelTaskJson不存在{TaskID}");
		return null;
	}

	public bool IsCanReSetCD(int TaskID)
	{
		if (!avatar.NomelTaskJson.HasField(TaskID.ToString()))
		{
			return true;
		}
		if (!IsNTaskStart(TaskID))
		{
			return true;
		}
		NTaskXiangXi nTaskXiangXiData = avatar.nomelTaskMag.GetNTaskXiangXiData(TaskID);
		DateTime endTime = Tools.GetEndTime(avatar.NomelTaskJson[TaskID.ToString()]["StartTime"].str, 0, nTaskXiangXiData.shiXian);
		if (endTime < avatar.worldTimeMag.getNowTime())
		{
			TimeOutEndTask(TaskID);
			return true;
		}
		if (Tools.getShengYuShiJian(avatar.worldTimeMag.getNowTime(), endTime) > new DateTime(1, 1, 1))
		{
			return false;
		}
		return true;
	}

	public IEnumerator TimeOutTask(int TaskID)
	{
		yield return (object)new WaitForSeconds(1f);
		TimeOutEndTask(TaskID);
	}

	public void randomTask(int TaskID, bool Reset = false)
	{
		JSONObject AllTypeJson = jsonData.instance.NTaskAllType[TaskID.ToString()];
		if (!Reset)
		{
			if (avatar.NomelTaskJson.HasField(TaskID.ToString()) && avatar.NomelTaskJson[TaskID.ToString()].HasField("RandomTime"))
			{
				DateTime startTime = DateTime.Parse(avatar.NomelTaskJson[TaskID.ToString()]["RandomTime"].str);
				DateTime endTime = startTime.AddMonths(AllTypeJson["CD"].I);
				if (Tools.instance.IsInTime(avatar.worldTimeMag.getNowTime(), startTime, endTime))
				{
					return;
				}
			}
			if (!IsCanReSetCD(TaskID))
			{
				return;
			}
		}
		int menpai = AllTypeJson["shili"].I;
		List<JSONObject> list = jsonData.instance.NTaskXiangXi.list.FindAll(delegate(JSONObject aa)
		{
			bool num = AllTypeJson["XiangXiID"].HasItem(aa["Type"].I);
			bool flag = aa["menpaihaogan"].list.Count < 1 || Tools.IsInNum(avatar.MenPaiHaoGanDu.HasField(menpai.ToString()) ? avatar.MenPaiHaoGanDu[menpai.ToString()].I : 0, aa["menpaihaogan"][0].I, aa["menpaihaogan"][1].I);
			bool flag2 = aa["Level"].list.Count < 1 || Tools.IsInNum(avatar.level, aa["Level"][0].I, aa["Level"][1].I);
			return (num && flag && flag2) ? true : false;
		});
		JSONObject randomListByPercent = Tools.instance.getRandomListByPercent(list, "percent");
		if (randomListByPercent != null)
		{
			JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject.SetField("id", TaskID);
			jSONObject.SetField("TaskID", randomListByPercent["id"].I);
			jSONObject.SetField("RandomTime", avatar.worldTimeMag.nowTime);
			jSONObject.SetField("IsStart", val: false);
			string str = randomListByPercent["TaskZiXiang"].Str;
			JSONObject taskJson = getTaskJson(str);
			JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jSONObject3 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jSONObject4 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jSONObject5 = new JSONObject(JSONObject.Type.ARRAY);
			foreach (JSONObject __aa in taskJson.list)
			{
				JSONObject randomList = Tools.getRandomList(jsonData.instance.NTaskSuiJI.list.FindAll((JSONObject cc) => cc["Str"].str.Replace(".0", "") == __aa["TaskID"].str));
				if (randomList == null)
				{
					Debug.LogError((object)"错误任务");
				}
				jSONObject2.Add(randomList["id"].I);
				if (randomList["type"].Count > 0)
				{
					jSONObject4.Add(randomList["type"][jsonData.GetRandom() % randomList["type"].Count].I);
				}
				if (randomList["shuxing"].Count > 0)
				{
					jSONObject5.Add(randomList["shuxing"][jsonData.GetRandom() % randomList["shuxing"].Count].I);
				}
				List<JSONObject> list2 = jsonData.instance.NTaskSuiJI.list.FindAll((JSONObject cc) => cc["Str"].str == __aa["Place"].str);
				if (list2.Count > 0)
				{
					JSONObject randomList2 = Tools.getRandomList(list2);
					jSONObject3.Add(randomList2["id"].I);
				}
				else
				{
					jSONObject3.Add(-1);
				}
			}
			jSONObject.SetField("TaskChild", jSONObject2);
			jSONObject.SetField("TaskWhereChild", jSONObject3);
			jSONObject.SetField("TaskWhereChildType", jSONObject4);
			jSONObject.SetField("TaskWhereChildShuXin", jSONObject5);
			avatar.NomelTaskJson[TaskID.ToString()] = jSONObject;
		}
		if (!AllTypeJson["seid"].HasItem(3))
		{
			return;
		}
		List<int> list3 = new List<int>();
		foreach (JSONObject item2 in getTaskChildList(TaskID).list)
		{
			int item = (int)jsonData.instance.NTaskSuiJI[item2.I.ToString()]["Value"].n;
			if (list3.Contains(item))
			{
				randomTask(TaskID, Reset: true);
				break;
			}
			list3.Add(item);
		}
	}

	public JSONObject getTaskJson(string taskInfo)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		try
		{
			string[] array = taskInfo.Split(new char[1] { '|' });
			foreach (string text in array)
			{
				if (!(text == ""))
				{
					JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
					string[] array2 = text.Split(new char[1] { '#' });
					jSONObject2.SetField("type", int.Parse(array2[0]));
					jSONObject2.SetField("desc", array2[1]);
					jSONObject2.SetField("num", int.Parse(array2[2]));
					jSONObject2.SetField("TaskID", array2[3]);
					jSONObject2.SetField("talkID", array2[4]);
					jSONObject2.SetField("Place", array2[5]);
					jSONObject.Add(jSONObject2);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("初始化任务失败检测任务信息:" + taskInfo));
			Debug.LogError((object)ex);
		}
		return jSONObject;
	}

	public void AutoGetNTaskDo(int TaskID)
	{
	}

	public int nowChildNTask(int TaskID)
	{
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(TaskID);
		int num = 0;
		foreach (JSONObject item in nTaskXiangXiList)
		{
			if (!XiangXiTaskIsEnd(item, TaskID, num))
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	public void EndNTaskSeid(int TaskID)
	{
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(TaskID);
		int num = 0;
		foreach (JSONObject item in nTaskXiangXiList)
		{
			realizedNTaskFinish(item, TaskID, num);
			num++;
		}
	}

	public bool AllXiangXiTaskIsEnd(int TaskID)
	{
		return nowChildNTask(TaskID) == -1;
	}

	public bool IsNTaskCanFinish(int TaskID)
	{
		if (!IsNTaskStart(TaskID))
		{
			return false;
		}
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(TaskID);
		int num = 0;
		bool result = true;
		foreach (JSONObject item in nTaskXiangXiList)
		{
			if (!XiangXiTaskIsEnd(item, TaskID, num))
			{
				return false;
			}
			num++;
		}
		return result;
	}

	public bool XiangXiTaskIsEnd(JSONObject json, int TaskID, int index)
	{
		int i = json["type"].I;
		return (bool)GetType().GetMethod("NTaskSeid" + i).Invoke(this, new object[3] { json, TaskID, index });
	}

	public void realizedNTaskFinish(JSONObject json, int TaskID, int index)
	{
		int i = json["type"].I;
		GetType().GetMethod("EndNTaskSeid" + i).Invoke(this, new object[3] { json, TaskID, index });
	}

	public List<JSONObject> GetNTaskXiangXiList(int TaskID)
	{
		string taskZiXiang = GetNTaskXiangXiData(TaskID).TaskZiXiang;
		return getTaskJson(taskZiXiang).list;
	}

	public JSONObject GetXiangXi(int TaskID, int index)
	{
		return GetNTaskXiangXiList(TaskID)[index];
	}

	public void GetNtaskZhuiZong()
	{
	}

	public void AutoNTaskSetKillAvatar(int AvatarID)
	{
		foreach (JSONObject item in GetNowNTask())
		{
			int i = item["id"].I;
			int num = GlobalValue.Get(402, "NomelTaskMag.AutoNTaskSetKillAvatar 委托任务ID临时变量");
			if (i != num)
			{
				continue;
			}
			if (!avatar.NomelTaskFlag.HasField(i.ToString()))
			{
				avatar.NomelTaskFlag[i.ToString()] = new JSONObject(JSONObject.Type.OBJECT);
			}
			if (!avatar.NomelTaskFlag[i.ToString()].HasField("killAvatar"))
			{
				avatar.NomelTaskFlag[i.ToString()]["killAvatar"] = new JSONObject(JSONObject.Type.ARRAY);
			}
			foreach (JSONObject item2 in getTaskChildList(i).list)
			{
				if (jsonData.instance.NTaskSuiJI[item2.I.ToString()]["Value"].I == AvatarID)
				{
					avatar.NomelTaskFlag[i.ToString()]["killAvatar"].Add(AvatarID);
				}
			}
		}
	}

	public void setTalkIndex(int TaskID, int index)
	{
		if (!avatar.NomelTaskFlag.HasField(TaskID.ToString()))
		{
			avatar.NomelTaskFlag[TaskID.ToString()] = new JSONObject(JSONObject.Type.OBJECT);
		}
		if (!avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex"))
		{
			avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"] = new JSONObject(JSONObject.Type.ARRAY);
		}
		avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].Add(index);
	}

	public void AutoSetLuJin()
	{
		foreach (JSONObject item in GetNowNTask())
		{
			_ = item;
		}
	}

	public int AutoThreeSceneHasNTask()
	{
		List<JSONObject> nowNTask = GetNowNTask();
		List<int> list = new List<int>();
		foreach (JSONObject item in nowNTask)
		{
			if (IsNTaskZiXiangInLuJin(item["id"].I, null) != null)
			{
				list.Add(item["id"].I);
			}
		}
		if (list.Count > 0)
		{
			if (list.Count > 1)
			{
				return GetYouXianTask(list);
			}
			return list[0];
		}
		return -1;
	}

	public int AutoAllMapPlaceHasNTask(List<int> flag)
	{
		List<JSONObject> nowNTask = GetNowNTask();
		List<int> list = new List<int>();
		foreach (JSONObject item in nowNTask)
		{
			if (IsNTaskZiXiangInLuJin(item["id"].I, flag) != null)
			{
				list.Add(item["id"].I);
			}
		}
		if (list.Count > 0)
		{
			if (list.Count > 1)
			{
				return GetYouXianTask(list);
			}
			return list[0];
		}
		return -1;
	}

	public int GetYouXianTask(List<int> taskList)
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		dictionary.Add(1, new List<int>());
		foreach (RenWuDaLeiYouXianJi data in RenWuDaLeiYouXianJi.DataList)
		{
			dictionary.Add(data.Id, new List<int>());
		}
		foreach (int task in taskList)
		{
			bool flag = false;
			foreach (RenWuDaLeiYouXianJi data2 in RenWuDaLeiYouXianJi.DataList)
			{
				for (int i = 0; i < data2.QuJian.Count; i += 2)
				{
					if (task >= data2.QuJian[i] && task <= data2.QuJian[i + 1])
					{
						dictionary[data2.Id].Add(task);
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				dictionary[1].Add(task);
			}
		}
		for (int j = 1; j <= dictionary.Count; j++)
		{
			if (dictionary[j].Count > 0)
			{
				return dictionary[j][0];
			}
		}
		return taskList[0];
	}

	public JSONObject IsNTaskZiXiangInLuJin(int TaskID, List<int> flag)
	{
		int num = nowChildNTask(TaskID);
		if (num == -1)
		{
			return null;
		}
		int num2 = -1;
		try
		{
			num2 = getWhereChilidID(TaskID, num);
		}
		catch
		{
			Debug.Log((object)"临时trycatch命中");
		}
		if (num2 == -1)
		{
			return null;
		}
		string str = jsonData.instance.NTaskSuiJI[num2.ToString()]["StrValue"].str;
		string screenName = Tools.getScreenName();
		if (str.Contains("F"))
		{
			string[] array = str.Split(new char[1] { ',' });
			if (!(screenName == array[0]))
			{
			}
		}
		else
		{
			if (str.Contains("S") && screenName == str)
			{
				return GetXiangXi(TaskID, num);
			}
			if (screenName == "AllMaps" && flag != null && string.Concat(flag[0]) == str)
			{
				return GetXiangXi(TaskID, num);
			}
		}
		return null;
	}

	public JSONObject getTaskChildList(int TaskID)
	{
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"];
	}

	public JSONObject getWhereTaskChildList(int TaskID)
	{
		if (!avatar.NomelTaskJson[TaskID.ToString()].HasField("TaskWhereChild"))
		{
			return null;
		}
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChild"];
	}

	public JSONObject getWhereTaskChildTypeList(int TaskID)
	{
		if (!avatar.NomelTaskJson[TaskID.ToString()].HasField("TaskWhereChildType"))
		{
			return null;
		}
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChildType"];
	}

	public JSONObject getWhereTaskChildShuxingList(int TaskID)
	{
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChildShuXin"];
	}

	public int getChilidID(int TaskID, int index)
	{
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"][index].I;
	}

	public JSONObject GetNowChildIDSuiJiJson(int TaskID)
	{
		int index = avatar.nomelTaskMag.nowChildNTask(TaskID);
		int chilidID = avatar.nomelTaskMag.getChilidID(TaskID, index);
		return jsonData.instance.NTaskSuiJI[chilidID.ToString()];
	}

	public int getWhereChilidID(int TaskID, int index)
	{
		return avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChild"][index].I;
	}

	public bool NTaskSeid1(JSONObject json, int TaskID, int index)
	{
		int i = json["num"].I;
		int chilidID = getChilidID(TaskID, index);
		int i2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
		foreach (ITEM_INFO value in avatar.itemList.values)
		{
			if (value.itemId == i2 && value.itemCount >= i)
			{
				return true;
			}
		}
		return false;
	}

	public void EndNTaskSeid1(JSONObject json, int TaskID, int index)
	{
		int i = json["num"].I;
		int chilidID = getChilidID(TaskID, index);
		int i2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
		avatar.removeItem(i2, i);
	}

	public bool NTaskSeid2(JSONObject json, int TaskID, int index)
	{
		int chilidID = getChilidID(TaskID, index);
		int i = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
		if (avatar.NomelTaskFlag.HasField(TaskID.ToString()) && avatar.NomelTaskFlag[TaskID.ToString()].HasField("killAvatar") && avatar.NomelTaskFlag[TaskID.ToString()]["killAvatar"].HasItem(i))
		{
			return true;
		}
		return false;
	}

	public void EndNTaskSeid2(JSONObject json, int TaskID, int index)
	{
		avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
	}

	public bool NTaskSeid4(JSONObject json, int TaskID, int index)
	{
		if (avatar.NomelTaskFlag.HasField(TaskID.ToString()) && avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex") && avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].HasItem(index))
		{
			return true;
		}
		return false;
	}

	public void EndNTaskSeid4(JSONObject json, int TaskID, int index)
	{
		avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
	}

	public bool NTaskSeid5(JSONObject json, int TaskID, int index)
	{
		if (avatar.NomelTaskFlag.HasField(TaskID.ToString()) && avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex") && avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].HasItem(index))
		{
			return true;
		}
		return false;
	}

	public void EndNTaskSeid5(JSONObject json, int TaskID, int index)
	{
		avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
	}

	public bool NTaskSeid6(JSONObject json, int TaskID, int index)
	{
		return true;
	}

	public void EndNTaskSeid6(JSONObject json, int TaskID, int _index)
	{
		List<JSONObject> list = avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"].list;
		int taskSeid6AddItemNum = GetTaskSeid6AddItemNum(TaskID, _index);
		int itemID = (int)jsonData.instance.NTaskSuiJI[list[_index].I.ToString()]["Value"].n;
		avatar.addItem(itemID, taskSeid6AddItemNum, Tools.CreateItemSeid(itemID), ShowText: true);
	}

	public int GetTaskSeid6AddItemNum(int TaskID, int _index)
	{
		List<JSONObject> list = avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"].list;
		List<JSONObject> nTaskXiangXiList = GetNTaskXiangXiList(TaskID);
		JSONObject nTaskXiangXiJson = GetNTaskXiangXiJson(TaskID);
		int num = 0;
		int num2 = 0;
		foreach (JSONObject item in list)
		{
			_ = item;
			if (num2 != _index)
			{
				float n = nTaskXiangXiList[num2]["num"].n;
				float n2 = nTaskXiangXiJson["shouYiLu"].n;
				num += (int)((float)NTaskSuiJI.DataDict[list[num2].I].jiaZhi * n * n2);
				num2++;
			}
		}
		int jiaZhi = NTaskSuiJI.DataDict[list[_index].I].jiaZhi;
		return (int)Math.Ceiling((float)num / (float)jiaZhi);
	}

	public bool NTaskSeid7(JSONObject json, int TaskID, int index)
	{
		return true;
	}

	public void EndNTaskSeid7(JSONObject json, int TaskID, int _index)
	{
	}

	public bool NTaskSeid10(JSONObject json, int TaskID, int index)
	{
		return false;
	}

	public void EndNTaskSeid10(JSONObject json, int TaskID, int _index)
	{
	}
}
