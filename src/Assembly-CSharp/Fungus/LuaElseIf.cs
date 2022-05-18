using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001233 RID: 4659
	[CommandInfo("Flow", "Lua Else If", "Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.", 0)]
	[AddComponentMenu("")]
	public class LuaElseIf : LuaCondition
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06007198 RID: 29080 RVA: 0x0000A093 File Offset: 0x00008293
		protected override bool IsElseIf
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
