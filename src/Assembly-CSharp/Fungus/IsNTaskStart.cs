using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142B RID: 5163
	[CommandInfo("YSTask", "IsNTaskStart", "判断任务是否开始", 0)]
	[AddComponentMenu("")]
	public class IsNTaskStart : Command
	{
		// Token: 0x06007CF5 RID: 31989 RVA: 0x002C5DF4 File Offset: 0x002C3FF4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (player.NomelTaskJson.HasField(this.NTaskID.Value.ToString()) && player.NomelTaskJson[this.NTaskID.Value.ToString()].HasField("IsStart"))
			{
				this.IsStart.Value = player.nomelTaskMag.IsNTaskStart(this.NTaskID.Value);
			}
			else
			{
				this.IsStart.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AB8 RID: 27320
		[Tooltip("需要判断是否开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006AB9 RID: 27321
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
