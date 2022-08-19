using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F69 RID: 3945
	[CommandInfo("YSNew/Set", "SetTaskFaild", "指定任务失败", 0)]
	[AddComponentMenu("")]
	public class SetTaskFaild : Command
	{
		// Token: 0x06006EDD RID: 28381 RVA: 0x002A59C4 File Offset: 0x002A3BC4
		public override void OnEnter()
		{
			if (!Tools.instance.getPlayer().taskMag._TaskData["Task"].HasField(this.TaskID.ToString()))
			{
				Tools.instance.getPlayer().taskMag._TaskData["Task"][string.Concat(this.TaskID)].SetField("disableTask", true);
			}
			this.Continue();
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BD3 RID: 23507
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;
	}
}
