using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141D RID: 5149
	[CommandInfo("YSNew/Set", "SetTaskIndex", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetTask : Command
	{
		// Token: 0x06007CC2 RID: 31938 RVA: 0x002C5584 File Offset: 0x002C3784
		public override void OnEnter()
		{
			Tools.instance.getPlayer().taskMag.setTaskIndex(this.TaskID, this.TaskIndex);
			string name = TaskJsonData.DataDict[this.TaskID].Name;
			UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 进度已更新", PopTipIconType.任务进度);
			this.Continue();
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AA4 RID: 27300
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04006AA5 RID: 27301
		[Tooltip("任务的变量值")]
		[SerializeField]
		protected int TaskIndex;
	}
}
