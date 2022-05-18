using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141C RID: 5148
	[CommandInfo("YSNew/Set", "SetStaticValueByVar", "设置全局变量", 0)]
	[AddComponentMenu("")]
	public class SetStaticValueByVar : Command
	{
		// Token: 0x06007CBF RID: 31935 RVA: 0x00054877 File Offset: 0x00052A77
		public override void OnEnter()
		{
			GlobalValue.Set(this.StaticValueID.Value, this.value.Value, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AA2 RID: 27298
		[Tooltip("全局变量的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable StaticValueID;

		// Token: 0x04006AA3 RID: 27299
		[Tooltip("全局变量的值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable value;
	}
}
