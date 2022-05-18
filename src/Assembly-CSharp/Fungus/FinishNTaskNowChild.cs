using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001426 RID: 5158
	[CommandInfo("YSTask", "FinishNTaskNowChild", "完成一个杂闻任务", 0)]
	[AddComponentMenu("")]
	public class FinishNTaskNowChild : Command
	{
		// Token: 0x06007CE2 RID: 31970 RVA: 0x002C5B14 File Offset: 0x002C3D14
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int index = player.nomelTaskMag.nowChildNTask(this.NTaskID.Value);
			player.nomelTaskMag.setTalkIndex(this.NTaskID.Value, index);
			this.Continue();
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AAF RID: 27311
		[Tooltip("需要完成子项的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
