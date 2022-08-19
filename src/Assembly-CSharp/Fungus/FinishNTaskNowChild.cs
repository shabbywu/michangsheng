using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F6E RID: 3950
	[CommandInfo("YSTask", "FinishNTaskNowChild", "完成一个杂闻任务", 0)]
	[AddComponentMenu("")]
	public class FinishNTaskNowChild : Command
	{
		// Token: 0x06006EF0 RID: 28400 RVA: 0x002A5DD0 File Offset: 0x002A3FD0
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int index = player.nomelTaskMag.nowChildNTask(this.NTaskID.Value);
			player.nomelTaskMag.setTalkIndex(this.NTaskID.Value, index);
			this.Continue();
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BD9 RID: 23513
		[Tooltip("需要完成子项的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
