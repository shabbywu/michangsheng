using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F67 RID: 3943
	[CommandInfo("YSNew/Set", "SetTaskCompelet", "指定任务完成", 0)]
	[AddComponentMenu("")]
	public class SetTaskCompelet : Command
	{
		// Token: 0x06006ED5 RID: 28373 RVA: 0x002A5838 File Offset: 0x002A3A38
		public override void OnEnter()
		{
			SetTaskCompelet.Do(this.TaskID);
			this.Continue();
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x002A584C File Offset: 0x002A3A4C
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

		// Token: 0x06006ED7 RID: 28375 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006ED8 RID: 28376 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BD1 RID: 23505
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
