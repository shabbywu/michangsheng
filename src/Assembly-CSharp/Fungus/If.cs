using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DD7 RID: 3543
	[CommandInfo("Flow", "If", "If the test expression is true, execute the following command block.", 0)]
	[AddComponentMenu("")]
	public class If : VariableCondition
	{
		// Token: 0x060064A0 RID: 25760 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
