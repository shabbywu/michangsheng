using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001421 RID: 5153
	[CommandInfo("YSNew/Set", "SetTaskIndexFinish", "完成指定任务的某条进度", 0)]
	[AddComponentMenu("")]
	public class SetTaskIndexFinish : Command
	{
		// Token: 0x06007CD1 RID: 31953 RVA: 0x000548B3 File Offset: 0x00052AB3
		public override void OnEnter()
		{
			SetTaskIndexFinish.Do(this.TaskID, this.TaskIndex);
			this.Continue();
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x002C57E4 File Offset: 0x002C39E4
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
			JSONObject jsonobject = player.taskMag._TaskData["Task"][TaskID.ToString()]["finishIndex"];
			for (int i = 0; i < jsonobject.Count; i++)
			{
				if (jsonobject[i].I == TaskIndex)
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
			List<JSONObject> list = jsonData.instance.TaskInfoJsonData.list.FindAll((JSONObject aa) => (int)aa["TaskID"].n == TaskID);
			if (player.taskMag._TaskData["Task"][TaskID.ToString()]["finishIndex"].Count >= list.Count)
			{
				player.taskMag._TaskData["Task"][TaskID.ToString()].SetField("disableTask", true);
				if (player.taskMag.isNowTask(TaskID))
				{
					player.taskMag.setNowTask(0);
				}
				player.StreamData.TaskMag.CheckHasOut();
				UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 已完成", PopTipIconType.任务完成);
			}
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AA9 RID: 27305
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04006AAA RID: 27306
		[Tooltip("任务的变量值")]
		[SerializeField]
		protected int TaskIndex;
	}
}
