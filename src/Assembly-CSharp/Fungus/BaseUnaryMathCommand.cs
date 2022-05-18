using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001236 RID: 4662
	[AddComponentMenu("")]
	public abstract class BaseUnaryMathCommand : Command
	{
		// Token: 0x060071A1 RID: 29089 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x002A62CC File Offset: 0x002A44CC
		public override string GetSummary()
		{
			return "in: " + ((this.inValue.floatRef != null) ? this.inValue.floatRef.Key : this.inValue.Value.ToString()) + ", out: " + ((this.outValue.floatRef != null) ? this.outValue.floatRef.Key : this.outValue.Value.ToString());
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x0004D448 File Offset: 0x0004B648
		public override bool HasReference(Variable variable)
		{
			return variable == this.inValue.floatRef || variable == this.outValue.floatRef;
		}

		// Token: 0x040063FD RID: 25597
		[Tooltip("Value to be passed in to the function.")]
		[SerializeField]
		protected FloatData inValue;

		// Token: 0x040063FE RID: 25598
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected FloatData outValue;
	}
}
