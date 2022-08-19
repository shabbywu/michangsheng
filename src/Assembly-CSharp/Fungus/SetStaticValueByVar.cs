using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F65 RID: 3941
	[CommandInfo("YSNew/Set", "SetStaticValueByVar", "设置全局变量", 0)]
	[AddComponentMenu("")]
	public class SetStaticValueByVar : Command
	{
		// Token: 0x06006ECF RID: 28367 RVA: 0x002A57AB File Offset: 0x002A39AB
		public override void OnEnter()
		{
			GlobalValue.Set(this.StaticValueID.Value, this.value.Value, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BCD RID: 23501
		[Tooltip("全局变量的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable StaticValueID;

		// Token: 0x04005BCE RID: 23502
		[Tooltip("全局变量的值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public IntegerVariable value;
	}
}
