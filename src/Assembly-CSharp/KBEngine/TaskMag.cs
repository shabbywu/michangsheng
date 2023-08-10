namespace KBEngine;

public class TaskMag
{
	private JSONObject taskData = new JSONObject();

	private string exampleJson = "{\"Task\":{\"1\":{\"id\":0,\"NowIndex\":0,\"AllIndex\":0}}}";

	private Avatar avatar;

	public JSONObject _TaskData
	{
		get
		{
			return taskData;
		}
		set
		{
			taskData = value;
		}
	}

	public bool isHasTask(int taskId)
	{
		if (taskData["Task"].HasField(taskId.ToString()))
		{
			return true;
		}
		return false;
	}

	public void addTask(int taskId)
	{
		if (!taskData["Task"].HasField(taskId.ToString()))
		{
			JSONObject jSONObject = new JSONObject();
			jSONObject.AddField("id", taskId);
			jSONObject.AddField("NowIndex", 1);
			jSONObject.AddField("AllIndex", new JSONObject(JSONObject.Type.ARRAY));
			jSONObject.AddField("disableTask", val: false);
			jSONObject.AddField("finishIndex", new JSONObject(JSONObject.Type.ARRAY));
			jSONObject.AddField("curTime", avatar.worldTimeMag.nowTime);
			jSONObject.AddField("continueTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["continueTime"].I);
			jSONObject.AddField("isComplete", val: false);
			if (jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str != "")
			{
				jSONObject.AddField("EndTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str);
			}
			taskData["Task"].AddField(string.Concat(taskId), jSONObject);
			setTaskIndex(taskId, 1);
		}
	}

	public void addTask(string curTime, int taskId)
	{
		if (!taskData["Task"].HasField(taskId.ToString()))
		{
			JSONObject jSONObject = new JSONObject();
			jSONObject.AddField("id", taskId);
			jSONObject.AddField("NowIndex", 1);
			jSONObject.AddField("AllIndex", new JSONObject(JSONObject.Type.ARRAY));
			jSONObject.AddField("disableTask", val: false);
			jSONObject.AddField("finishIndex", new JSONObject(JSONObject.Type.ARRAY));
			jSONObject.AddField("curTime", curTime);
			jSONObject.AddField("continueTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["continueTime"].I);
			jSONObject.AddField("isComplete", val: false);
			if (jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str != "")
			{
				jSONObject.AddField("EndTime", jsonData.instance.TaskJsonData[string.Concat(taskId)]["EndTime"].str);
			}
			taskData["Task"].AddField(string.Concat(taskId), jSONObject);
			setTaskIndex(taskId, 1);
		}
	}

	public void setNowTask(int taskID)
	{
		if (!taskData.HasField("ShowTask"))
		{
			taskData.AddField("ShowTask", 0);
		}
		taskData.SetField("ShowTask", taskID);
	}

	public bool isNowTask(int taskID)
	{
		if (taskData.HasField("ShowTask") && (int)taskData["ShowTask"].n == taskID)
		{
			return true;
		}
		return false;
	}

	public int GetTaskNowIndex(int taskId)
	{
		int result = 0;
		if (taskData["Task"].HasField(taskId.ToString()))
		{
			result = taskData["Task"][taskId.ToString()]["NowIndex"].I;
		}
		return result;
	}

	public void setTaskIndex(int taskId, int index)
	{
		if (!taskData["Task"].HasField(taskId.ToString()))
		{
			return;
		}
		foreach (JSONObject item in taskData["Task"][taskId.ToString()]["AllIndex"].list)
		{
			if (index == (int)item.n)
			{
				return;
			}
		}
		taskData["Task"][taskId.ToString()].SetField("NowIndex", index);
		taskData["Task"][taskId.ToString()]["AllIndex"].Add(index);
		if (getFinallyIndex(taskId) == index)
		{
			taskData["Task"][taskId.ToString()].SetField("disableTask", val: true);
		}
	}

	public TaskMag(Avatar _avatar)
	{
		taskData.AddField("Task", new JSONObject(JSONObject.Type.OBJECT));
		avatar = _avatar;
	}

	public int getFinallyIndex(int TaskID)
	{
		JSONObject taskInfoJsonData = jsonData.instance.TaskInfoJsonData;
		foreach (string key in taskInfoJsonData.keys)
		{
			if (taskInfoJsonData[key]["TaskID"].I == TaskID && taskInfoJsonData[key]["IsFinal"].I == 1)
			{
				return taskInfoJsonData[key]["TaskIndex"].I;
			}
		}
		return 0;
	}

	public void SetChuanWenBlack(int taskId)
	{
		if (isHasTask(taskId))
		{
			taskData["Task"][taskId.ToString()].SetField("isBlack", val: true);
		}
	}
}
