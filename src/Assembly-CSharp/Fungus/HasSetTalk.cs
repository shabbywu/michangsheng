using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F82 RID: 3970
	[CommandInfo("YSTools", "是否有SetTalk", "是否有SetTalk", 0)]
	[AddComponentMenu("")]
	public class HasSetTalk : Command
	{
		// Token: 0x06006F3D RID: 28477 RVA: 0x002A69FC File Offset: 0x002A4BFC
		public override void OnEnter()
		{
			if (GlobalValue.GetTalk(0, "HasSetTalk") > 0)
			{
				this.result.Value = true;
			}
			else
			{
				this.result.Value = false;
			}
			this.Continue();
		}

		// Token: 0x04005BFA RID: 23546
		[Tooltip("结果")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable result;
	}
}
