using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DEE RID: 3566
	[CommandInfo("Flow", "Lua Else If", "Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.", 0)]
	[AddComponentMenu("")]
	public class LuaElseIf : LuaCondition
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600650A RID: 25866 RVA: 0x00024C5F File Offset: 0x00022E5F
		protected override bool IsElseIf
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
