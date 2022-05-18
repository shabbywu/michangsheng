using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001234 RID: 4660
	[CommandInfo("Flow", "Lua If", "If the test expression is true, execute the following command block.", 0)]
	[AddComponentMenu("")]
	public class LuaIf : LuaCondition
	{
		// Token: 0x0600719D RID: 29085 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
