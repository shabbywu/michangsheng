using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F71 RID: 3953
	[CommandInfo("YSTask", "IsNTaskCanFinish", "判断是否做完所有子项任务", 0)]
	[AddComponentMenu("")]
	public class IsNTaskCanFinish : Command
	{
		// Token: 0x06006EFB RID: 28411 RVA: 0x002A5FBC File Offset: 0x002A41BC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.IsStart.Value = player.nomelTaskMag.AllXiangXiTaskIsEnd(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BDE RID: 23518
		[Tooltip("需要判断的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BDF RID: 23519
		[Tooltip("将判断后的值保存到一个变量中")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStart;
	}
}
