using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C7B RID: 3195
	public class NomelTaskMag
	{
		// Token: 0x06005825 RID: 22565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

		// Token: 0x06005826 RID: 22566 RVA: 0x002490D1 File Offset: 0x002472D1
		public NomelTaskMag(Avatar _avatar)
		{
			this.avatar = _avatar;
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x002490E0 File Offset: 0x002472E0
		public void restAllTaskType()
		{
			foreach (NTaskAllType ntaskAllType in NTaskAllType.DataList)
			{
				try
				{
					this.randomTask(ntaskAllType.Id, false);
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
				}
			}
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x00249150 File Offset: 0x00247350
		public void ResetAllStaticNTask()
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.StaticNTaksTime)
			{
				int type = (int)keyValuePair.Value["fenzu"];
				List<JToken> list = Tools.FindAllJTokens(jsonData.instance.StaticNTaks, (JToken aa) => (int)aa["fenzu"] == type);
				if (list.Count == 0)
				{
					Debug.LogError(string.Format("固定时间获得任务表没有fenzu {0}", type));
				}
				else
				{
					bool flag = true;
					if (this.avatar.StaticNTaskTime.ContainsKey("StaticTime" + type))
					{
						DateTime startTime = DateTime.Parse((string)this.avatar.StaticNTaskTime["StaticTime" + type]);
						DateTime endTime = startTime.AddMonths((int)this.avatar.StaticNTaskTime["CD" + type]);
						if (Tools.instance.IsInTime(this.avatar.worldTimeMag.getNowTime(), startTime, endTime, 0))
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.avatar.StaticNTaskTime["StaticTime" + type] = this.avatar.worldTimeMag.nowTime;
						this.avatar.StaticNTaskTime["CD" + type] = Tools.getRandomInt((int)keyValuePair.Value["CD"][0], (int)keyValuePair.Value["CD"][1]);
						int taskID = (int)list[jsonData.GetRandom() % list.Count]["TypeId"];
						if (!this.IsNTaskStart(taskID))
						{
							this.StartNTask(taskID, 1);
						}
					}
				}
			}
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x002493B0 File Offset: 0x002475B0
		public void DeDaiSetWhereNode(int taskID, bool digui = false)
		{
			if (digui)
			{
				NomelTaskMag.DeDaiSetWhereNodeCount++;
			}
			else
			{
				NomelTaskMag.DeDaiSetWhereNodeCount = 0;
			}
			if (NomelTaskMag.DeDaiSetWhereNodeCount > 1000)
			{
				Debug.LogError(string.Format("DeDaiSetWhereNode({0})递归超过1000次，强制终止", taskID));
				return;
			}
			List<JSONObject> nowNTask = this.GetNowNTask();
			string b = "";
			if (!this.avatar.NomelTaskJson[taskID.ToString()].HasField("TaskWhereChild"))
			{
				return;
			}
			foreach (JSONObject jsonobject in this.avatar.NomelTaskJson[taskID.ToString()]["TaskWhereChild"].list)
			{
				if (NTaskSuiJI.DataDict.ContainsKey(jsonobject.I))
				{
					b = NTaskSuiJI.DataDict[jsonobject.I].StrValue;
				}
			}
			foreach (JSONObject jsonobject2 in nowNTask)
			{
				int i = jsonobject2["id"].I;
				List<JSONObject> ntaskXiangXiList = this.avatar.nomelTaskMag.GetNTaskXiangXiList(i);
				int num = 0;
				foreach (JSONObject jsonobject3 in ntaskXiangXiList)
				{
					int whereChilidID = this.getWhereChilidID(i, num);
					if (NTaskSuiJI.DataDict.ContainsKey(whereChilidID) && NTaskSuiJI.DataDict[whereChilidID].StrValue == b)
					{
						this.randomTask(taskID, true);
						this.DeDaiSetWhereNode(taskID, true);
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x00249590 File Offset: 0x00247790
		public void StartNTask(int taskID, int type = 1)
		{
			this.avatar.NomelTaskFlag.SetField(taskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
			this.avatar.NomelTaskJson[taskID.ToString()].SetField("IsStart", true);
			this.avatar.NomelTaskJson[taskID.ToString()].SetField("StartTime", this.avatar.worldTimeMag.nowTime);
			this.avatar.NomelTaskJson[taskID.ToString()].SetField("IsFirstStart", true);
			if (!NTaskAllType.DataDict[taskID].seid.Contains(1) && type == 1)
			{
				UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
			}
			try
			{
				foreach (JSONObject jsonobject in this.avatar.NomelTaskJson[taskID.ToString()]["TaskWhereChild"].list)
				{
					if (NTaskSuiJI.DataDict.ContainsKey(jsonobject.I))
					{
						string strValue = NTaskSuiJI.DataDict[jsonobject.I].StrValue;
						JToken jtoken = this.avatar.RandomFuBenList[strValue];
						if (jtoken != null && ((JObject)jtoken).ContainsKey("ShouldReset"))
						{
							jtoken["ShouldReset"] = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			this.avatar.nomelTaskMag.RefreshGetNowNTaskData();
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x00249740 File Offset: 0x00247940
		public void TimeOutEndTask(int taskID)
		{
			this.setNTaskEnd(taskID);
			NTaskAllType ntaskAllType = NTaskAllType.DataDict[taskID];
			if (!ntaskAllType.seid.Contains(1))
			{
				UIPopTip.Inst.Pop("一条委托任务已失效", PopTipIconType.任务进度);
			}
			NTaskXiangXi ntaskXiangXiData = this.GetNTaskXiangXiData(taskID);
			if (ntaskAllType.shili >= 0 && ntaskXiangXiData.ShiLIAdd > 0)
			{
				this.avatar.SetMenPaiHaoGandu(ntaskAllType.shili, -ntaskXiangXiData.ShiLIReduce);
			}
			if (ntaskAllType.GeRen > 0 && ntaskXiangXiData.GeRenAdd > 0)
			{
				this.avatar.setAvatarHaoGandu(ntaskAllType.GeRen, -ntaskXiangXiData.GeRenReduce);
			}
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x002497DC File Offset: 0x002479DC
		public void setNTaskEnd(int taskID)
		{
			this.avatar.NomelTaskJson[taskID.ToString()].SetField("IsEnd", true);
			this.avatar.NomelTaskJson[taskID.ToString()].SetField("IsStart", false);
			this.avatar.nomelTaskMag.RefreshGetNowNTaskData();
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x0024983D File Offset: 0x00247A3D
		public void EndNTask(int taskID)
		{
			this.issueReward(taskID);
			this.setNTaskEnd(taskID);
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x00249850 File Offset: 0x00247A50
		public void getReward(int taskID, ref int money, ref int menpaihuobi)
		{
			List<JSONObject> list = this.avatar.NomelTaskJson[taskID.ToString()]["TaskChild"].list;
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(taskID);
			this.GetNTaskXiangXiData(taskID);
			JSONObject ntaskXiangXiJson = this.GetNTaskXiangXiJson(taskID);
			int num = 0;
			foreach (JSONObject jsonobject in list)
			{
				float n = ntaskXiangXiList[num]["num"].n;
				float n2 = ntaskXiangXiJson["shouYiLu"].n;
				NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[list[num].I];
				JSONObject jsonobject2 = jsonData.instance.NTaskSuiJI[list[num].I.ToString()];
				money += (int)((float)ntaskSuiJI.jiaZhi * n * n2);
				menpaihuobi += (int)(jsonobject2["huobi"].n * n * n2);
				string _talkID = ntaskXiangXiList[num]["talkID"].str;
				JSONObject jsonobject3 = jsonData.instance.NTaskSuiJI.list.Find((JSONObject _aa) => _aa["Str"].str == _talkID);
				if (_talkID != "0" && jsonobject3 != null)
				{
					money += (int)(jsonobject3["jiaZhi"].n * n2);
					menpaihuobi += (int)(jsonobject3["huobi"].n * n2);
				}
				num++;
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x00249A1C File Offset: 0x00247C1C
		public int GetTaskMoney(int taskID)
		{
			int num = 0;
			List<JSONObject> list = this.avatar.NomelTaskJson[taskID.ToString()]["TaskChild"].list;
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(taskID);
			JSONObject ntaskXiangXiJson = this.GetNTaskXiangXiJson(taskID);
			int num2 = 0;
			foreach (JSONObject jsonobject in list)
			{
				float n = ntaskXiangXiList[num2]["num"].n;
				float n2 = ntaskXiangXiJson["shouYiLu"].n;
				num += (int)((float)NTaskSuiJI.DataDict[list[num2].I].jiaZhi * n * n2);
				num2++;
			}
			return num;
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x00249AFC File Offset: 0x00247CFC
		public void issueReward(int taskID)
		{
			int num = 0;
			int num2 = 0;
			this.getReward(taskID, ref num, ref num2);
			NTaskAllType ntaskAllType = NTaskAllType.DataDict[taskID];
			int menpaihuobi = ntaskAllType.menpaihuobi;
			JSONObject jsonobject = this.GetNTaskXiangXiList(taskID).Find((JSONObject aa) => aa["type"].I == 6);
			if (menpaihuobi == 0 && num > 0 && !ntaskAllType.seid.Contains(4) && jsonobject == null)
			{
				this.avatar.AddMoney(num);
			}
			if (menpaihuobi > 0 && num2 > 0)
			{
				this.avatar.addItem(menpaihuobi, num2, Tools.CreateItemSeid(menpaihuobi), true);
			}
			this.EndNTaskSeid(taskID);
			NTaskXiangXi ntaskXiangXiData = this.GetNTaskXiangXiData(taskID);
			if (ntaskAllType.shili >= 0 && ntaskXiangXiData.ShiLIAdd > 0)
			{
				this.avatar.SetMenPaiHaoGandu(ntaskAllType.shili, ntaskXiangXiData.ShiLIAdd);
			}
			if (ntaskAllType.GeRen > 0 && ntaskXiangXiData.GeRenAdd > 0)
			{
				this.avatar.setAvatarHaoGandu(ntaskAllType.GeRen, ntaskXiangXiData.GeRenAdd);
			}
			PlayTutorial.CheckChuTaXianTu2(taskID);
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x00249C05 File Offset: 0x00247E05
		public bool IsNTaskStart(int taskID)
		{
			return this.avatar.NomelTaskJson[taskID.ToString()]["IsStart"].b;
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x00249C30 File Offset: 0x00247E30
		public bool ISNTaskTimeOut(int taskID)
		{
			if (this.IsNTaskStart(taskID))
			{
				NTaskXiangXi ntaskXiangXiData = this.GetNTaskXiangXiData(taskID);
				DateTime startTime = DateTime.Parse(this.avatar.NomelTaskJson[taskID.ToString()]["StartTime"].str);
				DateTime endTime = startTime.AddMonths(ntaskXiangXiData.shiXian);
				if (Tools.instance.IsInTime(this.avatar.worldTimeMag.getNowTime(), startTime, endTime, 0))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x00249CAC File Offset: 0x00247EAC
		public bool IsNTaskOutCD(int taskID)
		{
			if (!this.ISNTaskTimeOut(taskID))
			{
				return false;
			}
			if (this.IsNTaskStart(taskID))
			{
				NTaskAllType ntaskAllType = NTaskAllType.DataDict[taskID];
				DateTime startTime = DateTime.Parse(this.avatar.NomelTaskJson[taskID.ToString()]["RandomTime"].str);
				DateTime endTime = startTime.AddMonths(ntaskAllType.CD);
				if (Tools.instance.IsInTime(this.avatar.worldTimeMag.getNowTime(), startTime, endTime, 0))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x00249D38 File Offset: 0x00247F38
		public void autoSetNtask()
		{
			foreach (JSONObject jsonobject in this.GetNowNTask())
			{
				if (this.IsNTaskStart(jsonobject["id"].I) && this.ISNTaskTimeOut(jsonobject["id"].I))
				{
					this.TimeOutEndTask(jsonobject["id"].I);
				}
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x00249DCC File Offset: 0x00247FCC
		public void RefreshGetNowNTaskData()
		{
			JSONObject nomelTaskJson = this.avatar.NomelTaskJson;
			List<JSONObject> list = new List<JSONObject>();
			try
			{
				int count = nomelTaskJson.keys.Count;
				for (int i = 0; i < count; i++)
				{
					JSONObject jsonobject = nomelTaskJson.list[i];
					if (jsonobject["IsStart"].b)
					{
						list.Add(jsonobject);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("任务触发异常，异常信息:\n" + ex.Message + "\n" + ex.StackTrace);
			}
			this._NowNTaskData = list;
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x00249E70 File Offset: 0x00248070
		public List<JSONObject> GetNowNTask()
		{
			if (this._NowNTaskData == null)
			{
				this.RefreshGetNowNTaskData();
			}
			return this._NowNTaskData;
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x00249E88 File Offset: 0x00248088
		public bool HasNTask(int TaskID)
		{
			return this.avatar.NomelTaskJson.list.Find((JSONObject aa) => aa["IsStart"].b && aa["id"].I == TaskID) != null;
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x00249EC8 File Offset: 0x002480C8
		public JSONObject GetNTaskXiangXiJson(int TaskID)
		{
			int i = this.avatar.NomelTaskJson[TaskID.ToString()]["TaskID"].I;
			return jsonData.instance.NTaskXiangXi[i.ToString()];
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x00249F14 File Offset: 0x00248114
		public NTaskXiangXi GetNTaskXiangXiData(int TaskID)
		{
			JSONObject jsonobject = this.avatar.NomelTaskJson[TaskID.ToString()];
			if (jsonobject != null)
			{
				int i = jsonobject["TaskID"].I;
				return NTaskXiangXi.DataDict[i];
			}
			Debug.LogError(string.Format("Player.NomelTaskJson不存在{0}", TaskID));
			return null;
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x00249F70 File Offset: 0x00248170
		public bool IsCanReSetCD(int TaskID)
		{
			if (!this.avatar.NomelTaskJson.HasField(TaskID.ToString()))
			{
				return true;
			}
			if (!this.IsNTaskStart(TaskID))
			{
				return true;
			}
			NTaskXiangXi ntaskXiangXiData = this.avatar.nomelTaskMag.GetNTaskXiangXiData(TaskID);
			DateTime endTime = Tools.GetEndTime(this.avatar.NomelTaskJson[TaskID.ToString()]["StartTime"].str, 0, ntaskXiangXiData.shiXian, 0);
			if (endTime < this.avatar.worldTimeMag.getNowTime())
			{
				this.TimeOutEndTask(TaskID);
				return true;
			}
			return !(Tools.getShengYuShiJian(this.avatar.worldTimeMag.getNowTime(), endTime) > new DateTime(1, 1, 1));
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0024A032 File Offset: 0x00248232
		public IEnumerator TimeOutTask(int TaskID)
		{
			yield return new WaitForSeconds(1f);
			this.TimeOutEndTask(TaskID);
			yield break;
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0024A048 File Offset: 0x00248248
		public void randomTask(int TaskID, bool Reset = false)
		{
			JSONObject AllTypeJson = jsonData.instance.NTaskAllType[TaskID.ToString()];
			if (!Reset)
			{
				if (this.avatar.NomelTaskJson.HasField(TaskID.ToString()) && this.avatar.NomelTaskJson[TaskID.ToString()].HasField("RandomTime"))
				{
					DateTime startTime = DateTime.Parse(this.avatar.NomelTaskJson[TaskID.ToString()]["RandomTime"].str);
					DateTime endTime = startTime.AddMonths(AllTypeJson["CD"].I);
					if (Tools.instance.IsInTime(this.avatar.worldTimeMag.getNowTime(), startTime, endTime, 0))
					{
						return;
					}
				}
				if (!this.IsCanReSetCD(TaskID))
				{
					return;
				}
			}
			int menpai = AllTypeJson["shili"].I;
			List<JSONObject> list = jsonData.instance.NTaskXiangXi.list.FindAll(delegate(JSONObject aa)
			{
				bool flag = AllTypeJson["XiangXiID"].HasItem(aa["Type"].I);
				bool flag2 = aa["menpaihaogan"].list.Count < 1 || Tools.IsInNum(this.avatar.MenPaiHaoGanDu.HasField(menpai.ToString()) ? this.avatar.MenPaiHaoGanDu[menpai.ToString()].I : 0, aa["menpaihaogan"][0].I, aa["menpaihaogan"][1].I);
				bool flag3 = aa["Level"].list.Count < 1 || Tools.IsInNum((int)this.avatar.level, aa["Level"][0].I, aa["Level"][1].I);
				return flag && flag2 && flag3;
			});
			JSONObject randomListByPercent = Tools.instance.getRandomListByPercent(list, "percent");
			if (randomListByPercent != null)
			{
				JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
				jsonobject.SetField("id", TaskID);
				jsonobject.SetField("TaskID", randomListByPercent["id"].I);
				jsonobject.SetField("RandomTime", this.avatar.worldTimeMag.nowTime);
				jsonobject.SetField("IsStart", false);
				string str = randomListByPercent["TaskZiXiang"].Str;
				JSONObject taskJson = this.getTaskJson(str);
				JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
				JSONObject jsonobject3 = new JSONObject(JSONObject.Type.ARRAY);
				JSONObject jsonobject4 = new JSONObject(JSONObject.Type.ARRAY);
				JSONObject jsonobject5 = new JSONObject(JSONObject.Type.ARRAY);
				using (List<JSONObject>.Enumerator enumerator = taskJson.list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JSONObject __aa = enumerator.Current;
						JSONObject randomList = Tools.getRandomList(jsonData.instance.NTaskSuiJI.list.FindAll((JSONObject cc) => cc["Str"].str.Replace(".0", "") == __aa["TaskID"].str));
						if (randomList == null)
						{
							Debug.LogError("错误任务");
						}
						jsonobject2.Add(randomList["id"].I);
						if (randomList["type"].Count > 0)
						{
							jsonobject4.Add(randomList["type"][jsonData.GetRandom() % randomList["type"].Count].I);
						}
						if (randomList["shuxing"].Count > 0)
						{
							jsonobject5.Add(randomList["shuxing"][jsonData.GetRandom() % randomList["shuxing"].Count].I);
						}
						List<JSONObject> list2 = jsonData.instance.NTaskSuiJI.list.FindAll((JSONObject cc) => cc["Str"].str == __aa["Place"].str);
						if (list2.Count > 0)
						{
							JSONObject randomList2 = Tools.getRandomList(list2);
							jsonobject3.Add(randomList2["id"].I);
						}
						else
						{
							jsonobject3.Add(-1);
						}
					}
				}
				jsonobject.SetField("TaskChild", jsonobject2);
				jsonobject.SetField("TaskWhereChild", jsonobject3);
				jsonobject.SetField("TaskWhereChildType", jsonobject4);
				jsonobject.SetField("TaskWhereChildShuXin", jsonobject5);
				this.avatar.NomelTaskJson[TaskID.ToString()] = jsonobject;
			}
			if (AllTypeJson["seid"].HasItem(3))
			{
				List<int> list3 = new List<int>();
				foreach (JSONObject jsonobject6 in this.getTaskChildList(TaskID).list)
				{
					int item = (int)jsonData.instance.NTaskSuiJI[jsonobject6.I.ToString()]["Value"].n;
					if (list3.Contains(item))
					{
						this.randomTask(TaskID, true);
						break;
					}
					list3.Add(item);
				}
			}
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x0024A4C4 File Offset: 0x002486C4
		public JSONObject getTaskJson(string taskInfo)
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			try
			{
				foreach (string text in taskInfo.Split(new char[]
				{
					'|'
				}))
				{
					if (!(text == ""))
					{
						JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
						string[] array2 = text.Split(new char[]
						{
							'#'
						});
						jsonobject2.SetField("type", int.Parse(array2[0]));
						jsonobject2.SetField("desc", array2[1]);
						jsonobject2.SetField("num", int.Parse(array2[2]));
						jsonobject2.SetField("TaskID", array2[3]);
						jsonobject2.SetField("talkID", array2[4]);
						jsonobject2.SetField("Place", array2[5]);
						jsonobject.Add(jsonobject2);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("初始化任务失败检测任务信息:" + taskInfo);
				Debug.LogError(ex);
			}
			return jsonobject;
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x00004095 File Offset: 0x00002295
		public void AutoGetNTaskDo(int TaskID)
		{
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x0024A5CC File Offset: 0x002487CC
		public int nowChildNTask(int TaskID)
		{
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(TaskID);
			int num = 0;
			foreach (JSONObject json in ntaskXiangXiList)
			{
				if (!this.XiangXiTaskIsEnd(json, TaskID, num))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x0024A634 File Offset: 0x00248834
		public void EndNTaskSeid(int TaskID)
		{
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(TaskID);
			int num = 0;
			foreach (JSONObject json in ntaskXiangXiList)
			{
				this.realizedNTaskFinish(json, TaskID, num);
				num++;
			}
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0024A690 File Offset: 0x00248890
		public bool AllXiangXiTaskIsEnd(int TaskID)
		{
			return this.nowChildNTask(TaskID) == -1;
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0024A69C File Offset: 0x0024889C
		public bool IsNTaskCanFinish(int TaskID)
		{
			if (!this.IsNTaskStart(TaskID))
			{
				return false;
			}
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(TaskID);
			int num = 0;
			bool result = true;
			foreach (JSONObject json in ntaskXiangXiList)
			{
				if (!this.XiangXiTaskIsEnd(json, TaskID, num))
				{
					return false;
				}
				num++;
			}
			return result;
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0024A710 File Offset: 0x00248910
		public bool XiangXiTaskIsEnd(JSONObject json, int TaskID, int index)
		{
			int i = json["type"].I;
			return (bool)base.GetType().GetMethod("NTaskSeid" + i).Invoke(this, new object[]
			{
				json,
				TaskID,
				index
			});
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x0024A770 File Offset: 0x00248970
		public void realizedNTaskFinish(JSONObject json, int TaskID, int index)
		{
			int i = json["type"].I;
			base.GetType().GetMethod("EndNTaskSeid" + i).Invoke(this, new object[]
			{
				json,
				TaskID,
				index
			});
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x0024A7CC File Offset: 0x002489CC
		public List<JSONObject> GetNTaskXiangXiList(int TaskID)
		{
			string taskZiXiang = this.GetNTaskXiangXiData(TaskID).TaskZiXiang;
			return this.getTaskJson(taskZiXiang).list;
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x0024A7F2 File Offset: 0x002489F2
		public JSONObject GetXiangXi(int TaskID, int index)
		{
			return this.GetNTaskXiangXiList(TaskID)[index];
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x00004095 File Offset: 0x00002295
		public void GetNtaskZhuiZong()
		{
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0024A804 File Offset: 0x00248A04
		public void AutoNTaskSetKillAvatar(int AvatarID)
		{
			foreach (JSONObject jsonobject in this.GetNowNTask())
			{
				int i = jsonobject["id"].I;
				int num = GlobalValue.Get(402, "NomelTaskMag.AutoNTaskSetKillAvatar 委托任务ID临时变量");
				if (i == num)
				{
					if (!this.avatar.NomelTaskFlag.HasField(i.ToString()))
					{
						this.avatar.NomelTaskFlag[i.ToString()] = new JSONObject(JSONObject.Type.OBJECT);
					}
					if (!this.avatar.NomelTaskFlag[i.ToString()].HasField("killAvatar"))
					{
						this.avatar.NomelTaskFlag[i.ToString()]["killAvatar"] = new JSONObject(JSONObject.Type.ARRAY);
					}
					foreach (JSONObject jsonobject2 in this.getTaskChildList(i).list)
					{
						if (jsonData.instance.NTaskSuiJI[jsonobject2.I.ToString()]["Value"].I == AvatarID)
						{
							this.avatar.NomelTaskFlag[i.ToString()]["killAvatar"].Add(AvatarID);
						}
					}
				}
			}
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x0024A9B0 File Offset: 0x00248BB0
		public void setTalkIndex(int TaskID, int index)
		{
			if (!this.avatar.NomelTaskFlag.HasField(TaskID.ToString()))
			{
				this.avatar.NomelTaskFlag[TaskID.ToString()] = new JSONObject(JSONObject.Type.OBJECT);
			}
			if (!this.avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex"))
			{
				this.avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"] = new JSONObject(JSONObject.Type.ARRAY);
			}
			this.avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].Add(index);
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x0024AA64 File Offset: 0x00248C64
		public void AutoSetLuJin()
		{
			foreach (JSONObject jsonobject in this.GetNowNTask())
			{
			}
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0024AAB0 File Offset: 0x00248CB0
		public int AutoThreeSceneHasNTask()
		{
			List<JSONObject> nowNTask = this.GetNowNTask();
			List<int> list = new List<int>();
			foreach (JSONObject jsonobject in nowNTask)
			{
				if (this.IsNTaskZiXiangInLuJin(jsonobject["id"].I, null) != null)
				{
					list.Add(jsonobject["id"].I);
				}
			}
			if (list.Count <= 0)
			{
				return -1;
			}
			if (list.Count > 1)
			{
				return this.GetYouXianTask(list);
			}
			return list[0];
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0024AB54 File Offset: 0x00248D54
		public int AutoAllMapPlaceHasNTask(List<int> flag)
		{
			List<JSONObject> nowNTask = this.GetNowNTask();
			List<int> list = new List<int>();
			foreach (JSONObject jsonobject in nowNTask)
			{
				if (this.IsNTaskZiXiangInLuJin(jsonobject["id"].I, flag) != null)
				{
					list.Add(jsonobject["id"].I);
				}
			}
			if (list.Count <= 0)
			{
				return -1;
			}
			if (list.Count > 1)
			{
				return this.GetYouXianTask(list);
			}
			return list[0];
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x0024ABF8 File Offset: 0x00248DF8
		public int GetYouXianTask(List<int> taskList)
		{
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			dictionary.Add(1, new List<int>());
			foreach (RenWuDaLeiYouXianJi renWuDaLeiYouXianJi in RenWuDaLeiYouXianJi.DataList)
			{
				dictionary.Add(renWuDaLeiYouXianJi.Id, new List<int>());
			}
			foreach (int num in taskList)
			{
				bool flag = false;
				foreach (RenWuDaLeiYouXianJi renWuDaLeiYouXianJi2 in RenWuDaLeiYouXianJi.DataList)
				{
					for (int i = 0; i < renWuDaLeiYouXianJi2.QuJian.Count; i += 2)
					{
						if (num >= renWuDaLeiYouXianJi2.QuJian[i] && num <= renWuDaLeiYouXianJi2.QuJian[i + 1])
						{
							dictionary[renWuDaLeiYouXianJi2.Id].Add(num);
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
					dictionary[1].Add(num);
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

		// Token: 0x0600584E RID: 22606 RVA: 0x0024AD90 File Offset: 0x00248F90
		public JSONObject IsNTaskZiXiangInLuJin(int TaskID, List<int> flag)
		{
			int num = this.nowChildNTask(TaskID);
			if (num == -1)
			{
				return null;
			}
			int num2 = -1;
			try
			{
				num2 = this.getWhereChilidID(TaskID, num);
			}
			catch
			{
				Debug.Log("临时trycatch命中");
			}
			if (num2 == -1)
			{
				return null;
			}
			string str = jsonData.instance.NTaskSuiJI[num2.ToString()]["StrValue"].str;
			string screenName = Tools.getScreenName();
			if (str.Contains("F"))
			{
				string[] array = str.Split(new char[]
				{
					','
				});
				if (screenName == array[0])
				{
				}
			}
			else
			{
				if (str.Contains("S") && screenName == str)
				{
					return this.GetXiangXi(TaskID, num);
				}
				if (screenName == "AllMaps" && flag != null && string.Concat(flag[0]) == str)
				{
					return this.GetXiangXi(TaskID, num);
				}
			}
			return null;
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x0024AE88 File Offset: 0x00249088
		public JSONObject getTaskChildList(int TaskID)
		{
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"];
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x0024AEAC File Offset: 0x002490AC
		public JSONObject getWhereTaskChildList(int TaskID)
		{
			if (!this.avatar.NomelTaskJson[TaskID.ToString()].HasField("TaskWhereChild"))
			{
				return null;
			}
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChild"];
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x0024AF00 File Offset: 0x00249100
		public JSONObject getWhereTaskChildTypeList(int TaskID)
		{
			if (!this.avatar.NomelTaskJson[TaskID.ToString()].HasField("TaskWhereChildType"))
			{
				return null;
			}
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChildType"];
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x0024AF53 File Offset: 0x00249153
		public JSONObject getWhereTaskChildShuxingList(int TaskID)
		{
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChildShuXin"];
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x0024AF76 File Offset: 0x00249176
		public int getChilidID(int TaskID, int index)
		{
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"][index].I;
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x0024AFA4 File Offset: 0x002491A4
		public JSONObject GetNowChildIDSuiJiJson(int TaskID)
		{
			int index = this.avatar.nomelTaskMag.nowChildNTask(TaskID);
			int chilidID = this.avatar.nomelTaskMag.getChilidID(TaskID, index);
			return jsonData.instance.NTaskSuiJI[chilidID.ToString()];
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x0024AFEC File Offset: 0x002491EC
		public int getWhereChilidID(int TaskID, int index)
		{
			return this.avatar.NomelTaskJson[TaskID.ToString()]["TaskWhereChild"][index].I;
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0024B01C File Offset: 0x0024921C
		public bool NTaskSeid1(JSONObject json, int TaskID, int index)
		{
			int i = json["num"].I;
			int chilidID = this.getChilidID(TaskID, index);
			int i2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
			foreach (ITEM_INFO item_INFO in this.avatar.itemList.values)
			{
				if (item_INFO.itemId == i2 && (ulong)item_INFO.itemCount >= (ulong)((long)i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x0024B0D4 File Offset: 0x002492D4
		public void EndNTaskSeid1(JSONObject json, int TaskID, int index)
		{
			int i = json["num"].I;
			int chilidID = this.getChilidID(TaskID, index);
			int i2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
			this.avatar.removeItem(i2, i);
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0024B130 File Offset: 0x00249330
		public bool NTaskSeid2(JSONObject json, int TaskID, int index)
		{
			int chilidID = this.getChilidID(TaskID, index);
			int i = jsonData.instance.NTaskSuiJI[chilidID.ToString()]["Value"].I;
			return this.avatar.NomelTaskFlag.HasField(TaskID.ToString()) && this.avatar.NomelTaskFlag[TaskID.ToString()].HasField("killAvatar") && this.avatar.NomelTaskFlag[TaskID.ToString()]["killAvatar"].HasItem(i);
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0024B1D4 File Offset: 0x002493D4
		public void EndNTaskSeid2(JSONObject json, int TaskID, int index)
		{
			this.avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0024B1F4 File Offset: 0x002493F4
		public bool NTaskSeid4(JSONObject json, int TaskID, int index)
		{
			return this.avatar.NomelTaskFlag.HasField(TaskID.ToString()) && this.avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex") && this.avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].HasItem(index);
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0024B1D4 File Offset: 0x002493D4
		public void EndNTaskSeid4(JSONObject json, int TaskID, int index)
		{
			this.avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0024B26C File Offset: 0x0024946C
		public bool NTaskSeid5(JSONObject json, int TaskID, int index)
		{
			return this.avatar.NomelTaskFlag.HasField(TaskID.ToString()) && this.avatar.NomelTaskFlag[TaskID.ToString()].HasField("talkIndex") && this.avatar.NomelTaskFlag[TaskID.ToString()]["talkIndex"].HasItem(index);
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0024B1D4 File Offset: 0x002493D4
		public void EndNTaskSeid5(JSONObject json, int TaskID, int index)
		{
			this.avatar.NomelTaskFlag.SetField(TaskID.ToString(), new JSONObject(JSONObject.Type.OBJECT));
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool NTaskSeid6(JSONObject json, int TaskID, int index)
		{
			return true;
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0024B2E4 File Offset: 0x002494E4
		public void EndNTaskSeid6(JSONObject json, int TaskID, int _index)
		{
			List<JSONObject> list = this.avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"].list;
			int taskSeid6AddItemNum = this.GetTaskSeid6AddItemNum(TaskID, _index);
			int itemID = (int)jsonData.instance.NTaskSuiJI[list[_index].I.ToString()]["Value"].n;
			this.avatar.addItem(itemID, taskSeid6AddItemNum, Tools.CreateItemSeid(itemID), true);
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0024B36C File Offset: 0x0024956C
		public int GetTaskSeid6AddItemNum(int TaskID, int _index)
		{
			List<JSONObject> list = this.avatar.NomelTaskJson[TaskID.ToString()]["TaskChild"].list;
			List<JSONObject> ntaskXiangXiList = this.GetNTaskXiangXiList(TaskID);
			JSONObject ntaskXiangXiJson = this.GetNTaskXiangXiJson(TaskID);
			int num = 0;
			int num2 = 0;
			foreach (JSONObject jsonobject in list)
			{
				if (num2 != _index)
				{
					float n = ntaskXiangXiList[num2]["num"].n;
					float n2 = ntaskXiangXiJson["shouYiLu"].n;
					num += (int)((float)NTaskSuiJI.DataDict[list[num2].I].jiaZhi * n * n2);
					num2++;
				}
			}
			int jiaZhi = NTaskSuiJI.DataDict[list[_index].I].jiaZhi;
			return (int)Math.Ceiling((double)((float)num / (float)jiaZhi));
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool NTaskSeid7(JSONObject json, int TaskID, int index)
		{
			return true;
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x00004095 File Offset: 0x00002295
		public void EndNTaskSeid7(JSONObject json, int TaskID, int _index)
		{
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool NTaskSeid10(JSONObject json, int TaskID, int index)
		{
			return false;
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x00004095 File Offset: 0x00002295
		public void EndNTaskSeid10(JSONObject json, int TaskID, int _index)
		{
		}

		// Token: 0x040051FC RID: 20988
		private Avatar avatar;

		// Token: 0x040051FD RID: 20989
		private static int DeDaiSetWhereNodeCount;

		// Token: 0x040051FE RID: 20990
		private List<JSONObject> _NowNTaskData;
	}
}
