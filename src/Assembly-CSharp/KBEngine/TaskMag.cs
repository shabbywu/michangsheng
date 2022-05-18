using System;

namespace KBEngine
{
	// Token: 0x02001031 RID: 4145
	public class TaskMag
	{
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x000446C3 File Offset: 0x000428C3
		// (set) Token: 0x06006324 RID: 25380 RVA: 0x000446BA File Offset: 0x000428BA
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

		// Token: 0x06006326 RID: 25382 RVA: 0x000446CB File Offset: 0x000428CB
		public bool isHasTask(int taskId)
		{
			return this.taskData["Task"].HasField(taskId.ToString());
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x0027A0EC File Offset: 0x002782EC
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

		// Token: 0x06006328 RID: 25384 RVA: 0x0027A254 File Offset: 0x00278454
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

		// Token: 0x06006329 RID: 25385 RVA: 0x000446EE File Offset: 0x000428EE
		public void setNowTask(int taskID)
		{
			if (!this.taskData.HasField("ShowTask"))
			{
				this.taskData.AddField("ShowTask", 0);
			}
			this.taskData.SetField("ShowTask", taskID);
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00044724 File Offset: 0x00042924
		public bool isNowTask(int taskID)
		{
			return this.taskData.HasField("ShowTask") && (int)this.taskData["ShowTask"].n == taskID;
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x0027A3AC File Offset: 0x002785AC
		public int GetTaskNowIndex(int taskId)
		{
			int result = 0;
			if (this.taskData["Task"].HasField(taskId.ToString()))
			{
				result = this.taskData["Task"][taskId.ToString()]["NowIndex"].I;
			}
			return result;
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x0027A408 File Offset: 0x00278608
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

		// Token: 0x0600632D RID: 25389 RVA: 0x00044754 File Offset: 0x00042954
		public TaskMag(Avatar _avatar)
		{
			this.taskData.AddField("Task", new JSONObject(JSONObject.Type.OBJECT));
			this.avatar = _avatar;
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0027A52C File Offset: 0x0027872C
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

		// Token: 0x0600632F RID: 25391 RVA: 0x0004478F File Offset: 0x0004298F
		public void SetChuanWenBlack(int taskId)
		{
			if (!this.isHasTask(taskId))
			{
				return;
			}
			this.taskData["Task"][taskId.ToString()].SetField("isBlack", true);
		}

		// Token: 0x04005D13 RID: 23827
		private JSONObject taskData = new JSONObject();

		// Token: 0x04005D14 RID: 23828
		private string exampleJson = "{\"Task\":{\"1\":{\"id\":0,\"NowIndex\":0,\"AllIndex\":0}}}";

		// Token: 0x04005D15 RID: 23829
		private Avatar avatar;
	}
}
