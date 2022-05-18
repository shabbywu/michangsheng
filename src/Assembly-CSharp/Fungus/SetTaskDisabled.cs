using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141F RID: 5151
	[CommandInfo("YSNew/Set", "SetTaskDisabled", "将传闻置灰", 0)]
	[AddComponentMenu("")]
	public class SetTaskDisabled : Command
	{
		// Token: 0x06007CCA RID: 31946 RVA: 0x002C5694 File Offset: 0x002C3894
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (!player.taskMag._TaskData["Task"].HasField(this.TaskID.ToString()))
			{
				this.Continue();
				return;
			}
			player.taskMag._TaskData["Task"][this.TaskID.ToString()].SetField("disableTask", true);
			if (player.taskMag.isNowTask(this.TaskID))
			{
				player.taskMag.setNowTask(0);
			}
			string name = TaskJsonData.DataDict[this.TaskID].Name;
			UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 已达成", PopTipIconType.任务完成);
			this.Continue();
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AA7 RID: 27303
		[Tooltip("需要置灰的任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
