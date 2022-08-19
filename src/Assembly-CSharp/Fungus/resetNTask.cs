using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F6B RID: 3947
	[CommandInfo("YS", "resetNTask", "重置任务", 0)]
	[AddComponentMenu("")]
	public class resetNTask : Command
	{
		// Token: 0x06006EE5 RID: 28389 RVA: 0x002A5CB4 File Offset: 0x002A3EB4
		public override void OnEnter()
		{
			resetNTask.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x002A5CCC File Offset: 0x002A3ECC
		public static void Do(int _NTaskID)
		{
			PlayerEx.Player.nomelTaskMag.randomTask(_NTaskID, true);
		}

		// Token: 0x04005BD6 RID: 23510
		[Tooltip("任务的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
