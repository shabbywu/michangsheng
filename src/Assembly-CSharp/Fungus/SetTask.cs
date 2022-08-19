using System;
using JSONClass;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F66 RID: 3942
	[CommandInfo("YSNew/Set", "SetTaskIndex", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetTask : Command
	{
		// Token: 0x06006ED2 RID: 28370 RVA: 0x002A57D4 File Offset: 0x002A39D4
		public override void OnEnter()
		{
			Tools.instance.getPlayer().taskMag.setTaskIndex(this.TaskID, this.TaskIndex);
			string name = TaskJsonData.DataDict[this.TaskID].Name;
			UIPopTip.Inst.Pop("<color=#FF0000> " + name + " </color> 进度已更新", PopTipIconType.任务进度);
			this.Continue();
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BCF RID: 23503
		[Tooltip("任务的ID")]
		[SerializeField]
		protected int TaskID;

		// Token: 0x04005BD0 RID: 23504
		[Tooltip("任务的变量值")]
		[SerializeField]
		protected int TaskIndex;
	}
}
