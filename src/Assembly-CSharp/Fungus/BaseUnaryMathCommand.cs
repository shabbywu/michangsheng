using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF1 RID: 3569
	[AddComponentMenu("")]
	public abstract class BaseUnaryMathCommand : Command
	{
		// Token: 0x06006513 RID: 25875 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x00281C00 File Offset: 0x0027FE00
		public override string GetSummary()
		{
			return "in: " + ((this.inValue.floatRef != null) ? this.inValue.floatRef.Key : this.inValue.Value.ToString()) + ", out: " + ((this.outValue.floatRef != null) ? this.outValue.floatRef.Key : this.outValue.Value.ToString());
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x00281C8C File Offset: 0x0027FE8C
		public override bool HasReference(Variable variable)
		{
			return variable == this.inValue.floatRef || variable == this.outValue.floatRef;
		}

		// Token: 0x040056EA RID: 22250
		[Tooltip("Value to be passed in to the function.")]
		[SerializeField]
		protected FloatData inValue;

		// Token: 0x040056EB RID: 22251
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected FloatData outValue;
	}
}
