using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DCA RID: 3530
	[CommandInfo("Flow", "Else If", "Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.", 0)]
	[AddComponentMenu("")]
	public class ElseIf : VariableCondition
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x00024C5F File Offset: 0x00022E5F
		protected override bool IsElseIf
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
