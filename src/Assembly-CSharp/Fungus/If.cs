using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001212 RID: 4626
	[CommandInfo("Flow", "If", "If the test expression is true, execute the following command block.", 0)]
	[AddComponentMenu("")]
	public class If : VariableCondition
	{
		// Token: 0x06007122 RID: 28962 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
