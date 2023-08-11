using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetTaskIndexFinish", "完成指定任务的某条进度", 0)]
[AddComponentMenu("")]
public class SetTaskIndexFinish : Command
{
	[Tooltip("任务的ID")]
	[SerializeField]
	protected int TaskID;

	[Tooltip("任务的变量值")]
	[SerializeField]
	protected int TaskIndex;

	public override void OnEnter()
	{
		Do(TaskID, TaskIndex);
		Continue();
	}

	public static void Do(int TaskID, int TaskIndex)
	{
		Avatar player = Tools.instance.getPlayer();
		if (!player.taskMag._TaskData["Task"].HasField(TaskID.ToString()))
		{
			return;
		}
		if (!player.taskMag._TaskData["Task"][TaskID.ToString()].HasField("finishIndex"))
		{
			player.taskMag._TaskData["Task"][TaskID.ToString()].AddField("finishIndex", new JSONObject(JSONObject.Type.ARRAY));
		}
		bool flag = false;
		JSONObject jSONObject = player.taskMag._TaskData["Task"][TaskID.ToString()]["finishIndex"];
		for (int i = 0; i < jSONObject.Count; i++)
		{
			if (jSONObject[i].I == TaskIndex)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return;
		}
		player.taskMag._TaskData["Task"][TaskID.ToString()]["finishIndex"].Add(TaskIndex);
		string name = TaskJsonData.DataDict[TaskID].Name;
		UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 进度已更新", PopTipIconType.任务进度);
		List<JSONObject> list = jsonData.instance.TaskInfoJsonData.list.FindAll((JSONObject aa) => aa["TaskID"].I == TaskID);
		if (player.taskMag._TaskData["Task"][TaskID.ToString()]["finishIndex"].Count >= list.Count)
		{
			player.taskMag._TaskData["Task"][TaskID.ToString()].SetField("disableTask", val: true);
			if (player.taskMag.isNowTask(TaskID))
			{
				player.taskMag.setNowTask(0);
			}
			player.StreamData.TaskMag.CheckHasOut();
			UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 已完成", PopTipIconType.任务完成);
		}
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
