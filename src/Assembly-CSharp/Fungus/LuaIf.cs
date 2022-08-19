using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DEF RID: 3567
	[CommandInfo("Flow", "Lua If", "If the test expression is true, execute the following command block.", 0)]
	[AddComponentMenu("")]
	public class LuaIf : LuaCondition
	{
		// Token: 0x0600650F RID: 25871 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
