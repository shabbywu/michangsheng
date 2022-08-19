using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F64 RID: 3940
	[CommandInfo("YSNew/Set", "SetStaticValue", "设置全局变量", 0)]
	[AddComponentMenu("")]
	public class SetStaticValue : Command
	{
		// Token: 0x06006ECC RID: 28364 RVA: 0x002A578C File Offset: 0x002A398C
		public override void OnEnter()
		{
			GlobalValue.Set(this.StaticValueID, this.value, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BCB RID: 23499
		[Tooltip("全局变量的ID")]
		[SerializeField]
		public int StaticValueID;

		// Token: 0x04005BCC RID: 23500
		[Tooltip("全局变量的值")]
		[SerializeField]
		public int value;
	}
}
