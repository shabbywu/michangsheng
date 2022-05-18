using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D1 RID: 5073
	[CommandInfo("YS", "CheckShaQi", "检测杀气数量", 0)]
	[AddComponentMenu("")]
	public class CheckShaQi : Command
	{
		// Token: 0x06007BA4 RID: 31652 RVA: 0x002C4024 File Offset: 0x002C2224
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.shaQi;
			this.Continue();
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A17 RID: 27159
		[Tooltip("获取到的杀气值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
