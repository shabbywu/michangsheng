using System;

namespace KBEngine
{
	// Token: 0x02000C7E RID: 3198
	public class TaskMag
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06005886 RID: 22662 RVA: 0x0024E3B1 File Offset: 0x0024C5B1
		// (set) Token: 0x06005885 RID: 22661 RVA: 0x0024E3A8 File Offset: 0x0024C5A8
		public JSONObject _TaskData
		{
			get
			{
				return this.taskData;
			}
			set
			{
				this.taskData = value;
			}
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x0024E3B9 File Offset: 0x0024C5B9
		public bool isHasTask(int taskId)
		{
			return this.taskData["Task"].HasField(taskId.ToString());
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x0024E3DC File Offset: 0x0024C5DC
		public void addTask(int taskId)
		{
			if (this.taskData["Task"].HasField(taskId.ToString()))
			{
				return;
			}
			JSONObject jsonobject = new JSONObject();
			jsonobject.AddField("id", taskId);
			jsonobject.AddField("NowIndex", 1);
			jsonobject.AddField("AllIndex", new JSONObject(JSONObject.Type.ARRAY));
			jsonobject.AddField("disableTask", false);
			jsonobject.AddField("finishIndex", new JSONObject(JSONObject.Type.ARRAY));
			jsonobject.AddField("curTime", this.avatar.worldTimeMag.nowTime);
			jsonobject.AddField("continueTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["continueTime"].I);
			jsonobject.AddField("isComplete", false);
			if (jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str != "")
			{
				jsonobject.AddField("EndTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str);
			}
			this.taskData["Task"].AddField(string.Concat(taskId), jsonobject);
			this.setTaskIndex(taskId, 1);
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x0024E544 File Offset: 0x0024C744
		public void addTask(string curTime, int taskId)
		{
			if (this.taskData["Task"].HasField(taskId.ToString()))
			{
				return;
			}
			JSONObject jsonobject = new JSONObject();
			jsonobject.AddField("id", taskId);
			jsonobject.AddField("NowIndex", 1);
			jsonobject.AddField("AllIndex", new JSONObject(JSONObject.Type.ARRAY));
			jsonobject.AddField("disableTask", false);
			jsonobject.AddField("finishIndex", new JSONObject(JSONObject.Type.ARRAY));
			jsonobject.AddField("curTime", curTime);
			jsonobject.AddField("continueTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["continueTime"].I);
			jsonobject.AddField("isComplete", false);
			if (jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str != "")
			{
				jsonobject.AddField("EndTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str);
			}
			this.taskData["Task"].AddField(string.Concat(taskId), jsonobject);
			this.setTaskIndex(taskId, 1);
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x0024E69A File Offset: 0x0024C89A
		public void setNowTask(int taskID)
		{
			if (!this.taskData.HasField("ShowTask"))
			{
				this.taskData.AddField("ShowTask", 0);
			}
			this.taskData.SetField("ShowTask", taskID);
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x0024E6D0 File Offset: 0x0024C8D0
		public bool isNowTask(int taskID)
		{
			return this.taskData.HasField("ShowTask") && (int)this.taskData["ShowTask"].n == taskID;
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0024E700 File Offset: 0x0024C900
		public int GetTaskNowIndex(int taskId)
		{
			int result = 0;
			if (this.taskData["Task"].HasField(taskId.ToString()))
			{
				result = this.taskData["Task"][taskId.ToString()]["NowIndex"].I;
			}
			return result;
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0024E75C File Offset: 0x0024C95C
		public void setTaskIndex(int taskId, int index)
		{
			if (!this.taskData["Task"].HasField(taskId.ToString()))
			{
				return;
			}
			foreach (JSONObject jsonobject in this.taskData["Task"][taskId.ToString()]["AllIndex"].list)
			{
				if (index == (int)jsonobject.n)
				{
					return;
				}
			}
			this.taskData["Task"][taskId.ToString()].SetField("NowIndex", index);
			this.taskData["Task"][taskId.ToString()]["AllIndex"].Add(index);
			if (this.getFinallyIndex(taskId) == index)
			{
				this.taskData["Task"][taskId.ToString()].SetField("disableTask", true);
			}
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0024E880 File Offset: 0x0024CA80
		public TaskMag(Avatar _avatar)
		{
			this.taskData.AddField("Task", new JSONObject(JSONObject.Type.OBJECT));
			this.avatar = _avatar;
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x0024E8BC File Offset: 0x0024CABC
		public int getFinallyIndex(int TaskID)
		{
			JSONObject taskInfoJsonData = jsonData.instance.TaskInfoJsonData;
			foreach (string index in taskInfoJsonData.keys)
			{
				if (taskInfoJsonData[index]["TaskID"].I == TaskID && taskInfoJsonData[index]["IsFinal"].I == 1)
				{
					return taskInfoJsonData[index]["TaskIndex"].I;
				}
			}
			return 0;
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x0024E964 File Offset: 0x0024CB64
		public void SetChuanWenBlack(int taskId)
		{
			if (!this.isHasTask(taskId))
			{
				return;
			}
			this.taskData["Task"][taskId.ToString()].SetField("isBlack", true);
		}

		// Token: 0x04005204 RID: 20996
		private JSONObject taskData = new JSONObject();

		// Token: 0x04005205 RID: 20997
		private string exampleJson = "{\"Task\":{\"1\":{\"id\":0,\"NowIndex\":0,\"AllIndex\":0}}}";

		// Token: 0x04005206 RID: 20998
		private Avatar avatar;
	}
}
