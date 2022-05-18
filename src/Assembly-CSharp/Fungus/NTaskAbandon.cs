using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142C RID: 5164
	[CommandInfo("YSTask", "NTaskAbandon", "放弃任务", 0)]
	[AddComponentMenu("")]
	public class NTaskAbandon : Command
	{
		// Token: 0x06007CF9 RID: 31993 RVA: 0x00054948 File Offset: 0x00052B48
		public override void OnEnter()
		{
			Tools.instance.getPlayer().nomelTaskMag.TimeOutEndTask(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006ABA RID: 27322
		[Tooltip("需要放弃的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
