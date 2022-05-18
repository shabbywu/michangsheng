using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142E RID: 5166
	[CommandInfo("YSTask", "StartNTask", "开始一个任务", 0)]
	[AddComponentMenu("")]
	public class StartNTask : Command
	{
		// Token: 0x06007D01 RID: 32001 RVA: 0x00054992 File Offset: 0x00052B92
		public override void OnEnter()
		{
			StartNTask.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x002C60C0 File Offset: 0x002C42C0
		public static void Do(int _NTaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			if (!player.nomelTaskMag.IsNTaskStart(_NTaskID))
			{
				player.nomelTaskMag.StartNTask(_NTaskID, 1);
			}
		}

		// Token: 0x04006ABD RID: 27325
		[Tooltip("需要开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
