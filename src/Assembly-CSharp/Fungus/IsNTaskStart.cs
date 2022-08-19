using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F73 RID: 3955
	[CommandInfo("YSTask", "IsNTaskStart", "判断任务是否开始", 0)]
	[AddComponentMenu("")]
	public class IsNTaskStart : Command
	{
		// Token: 0x06006F03 RID: 28419 RVA: 0x002A60D4 File Offset: 0x002A42D4
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

		// Token: 0x06006F04 RID: 28420 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BE2 RID: 23522
		[Tooltip("需要判断是否开始的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BE3 RID: 23523
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
