using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CC RID: 5068
	[CommandInfo("YS", "CheckExp", "检测经验数量", 0)]
	[AddComponentMenu("")]
	public class CheckExp : Command
	{
		// Token: 0x06007B90 RID: 31632 RVA: 0x002C3E88 File Offset: 0x002C2088
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.exp;
			this.Continue();
		}

		// Token: 0x06007B91 RID: 31633 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B92 RID: 31634 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A0F RID: 27151
		[Tooltip("获取到的修为值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
