using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001429 RID: 5161
	[CommandInfo("YSTask", "IsNTaskCanFinish", "判断是否做完所有子项任务", 0)]
	[AddComponentMenu("")]
	public class IsNTaskCanFinish : Command
	{
		// Token: 0x06007CED RID: 31981 RVA: 0x002C5D00 File Offset: 0x002C3F00
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.IsStart.Value = player.nomelTaskMag.AllXiangXiTaskIsEnd(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AB4 RID: 27316
		[Tooltip("需要判断的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006AB5 RID: 27317
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
