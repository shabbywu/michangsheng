using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141E RID: 5150
	[CommandInfo("YSNew/Set", "SetTaskCompelet", "指定任务完成", 0)]
	[AddComponentMenu("")]
	public class SetTaskCompelet : Command
	{
		// Token: 0x06007CC5 RID: 31941 RVA: 0x000548A0 File Offset: 0x00052AA0
		public override void OnEnter()
		{
			SetTaskCompelet.Do(this.TaskID);
			this.Continue();
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x002C55E8 File Offset: 0x002C37E8
		public static void Do(int TaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.taskMag._TaskData["Task"].HasField(TaskID.ToString()))
			{
				player.taskMag._TaskData["Task"][string.Concat(TaskID)].SetField("isComplete", true);
				player.taskMag._TaskData["Task"][string.Concat(TaskID)].SetField("disableTask", true);
				player.StreamData.TaskMag.CheckHasOut();
			}
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AA6 RID: 27302
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
