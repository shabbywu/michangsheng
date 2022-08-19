using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F74 RID: 3956
	[CommandInfo("YSTask", "NTaskAbandon", "放弃任务", 0)]
	[AddComponentMenu("")]
	public class NTaskAbandon : Command
	{
		// Token: 0x06006F07 RID: 28423 RVA: 0x002A616B File Offset: 0x002A436B
		public override void OnEnter()
		{
			Tools.instance.getPlayer().nomelTaskMag.TimeOutEndTask(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BE4 RID: 23524
		[Tooltip("需要放弃的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
