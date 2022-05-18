using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001420 RID: 5152
	[CommandInfo("YSNew/Set", "SetTaskFaild", "指定任务失败", 0)]
	[AddComponentMenu("")]
	public class SetTaskFaild : Command
	{
		// Token: 0x06007CCD RID: 31949 RVA: 0x002C5760 File Offset: 0x002C3960
		public override void OnEnter()
		{
			if (!Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(this.TaskID.ToString()))
			{
				Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(this.TaskID)].SetField("disableTask", true);
			}
			this.Continue();
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AA8 RID: 27304
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
