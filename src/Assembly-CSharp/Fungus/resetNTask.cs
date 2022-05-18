using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001423 RID: 5155
	[CommandInfo("YS", "resetNTask", "重置任务", 0)]
	[AddComponentMenu("")]
	public class resetNTask : Command
	{
		// Token: 0x06007CD7 RID: 31959 RVA: 0x000548E7 File Offset: 0x00052AE7
		public override void OnEnter()
		{
			resetNTask.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x000548FF File Offset: 0x00052AFF
		public static void Do(int _NTaskID)
		{
			PlayerEx.Player.nomelTaskMag.randomTask(_NTaskID, true);
		}

		// Token: 0x04006AAC RID: 27308
		[Tooltip("任务的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
