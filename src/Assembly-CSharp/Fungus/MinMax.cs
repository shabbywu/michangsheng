using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001242 RID: 4674
	[CommandInfo("Math", "MinMax", "Command to store the min or max of 2 values", 0)]
	[AddComponentMenu("")]
	public class MinMax : Command
	{
		// Token: 0x060071C2 RID: 29122 RVA: 0x002A6A78 File Offset: 0x002A4C78
		public override void OnEnter()
		{
			MinMax.Function function = this.function;
			if (function != MinMax.Function.Min)
			{
				if (function == MinMax.Function.Max)
				{
					this.outValue.Value = Mathf.Max(this.inLHSValue.Value, this.inRHSValue.Value);
				}
			}
			else
			{
				this.outValue.Value = Mathf.Min(this.inLHSValue.Value, this.inRHSValue.Value);
			}
			this.Continue();
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x002A6AEC File Offset: 0x002A4CEC
		public override string GetSummary()
		{
			return string.Concat(new string[]
			{
				this.function.ToString(),
				" out: ",
				(this.outValue.floatRef != null) ? this.outValue.floatRef.Key : this.outValue.Value.ToString(),
				" [",
				this.inLHSValue.Value.ToString(),
				" - ",
				this.inRHSValue.Value.ToString(),
				"]"
			});
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x0004D54C File Offset: 0x0004B74C
		public override bool HasReference(Variable variable)
		{
			return this.inLHSValue.floatRef == variable || this.inRHSValue.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006421 RID: 25633
		[Tooltip("Min Or Max")]
		[SerializeField]
		protected MinMax.Function function;

		// Token: 0x04006422 RID: 25634
		[SerializeField]
		protected FloatData inLHSValue;

		// Token: 0x04006423 RID: 25635
		[SerializeField]
		protected FloatData inRHSValue;

		// Token: 0x04006424 RID: 25636
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x02001243 RID: 4675
		public enum Function
		{
			// Token: 0x04006426 RID: 25638
			Min,
			// Token: 0x04006427 RID: 25639
			Max
		}
	}
}
