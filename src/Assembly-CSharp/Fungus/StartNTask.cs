using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F76 RID: 3958
	[CommandInfo("YSTask", "StartNTask", "开始一个任务", 0)]
	[AddComponentMenu("")]
	public class StartNTask : Command
	{
		// Token: 0x06006F0F RID: 28431 RVA: 0x002A63EC File Offset: 0x002A45EC
		public override void OnEnter()
		{
			StartNTask.Do(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F11 RID: 28433 RVA: 0x002A6404 File Offset: 0x002A4604
		public static void Do(int _NTaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			if (!player.nomelTaskMag.IsNTaskStart(_NTaskID))
			{
				player.nomelTaskMag.StartNTask(_NTaskID, 1);
			}
		}

		// Token: 0x04005BE7 RID: 23527
		[Tooltip("需要开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;
	}
}
