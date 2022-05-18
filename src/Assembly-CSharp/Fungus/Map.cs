using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001241 RID: 4673
	[CommandInfo("Math", "Map", "Map a value that exists in 1 range of numbers to another.", 0)]
	[AddComponentMenu("")]
	public class Map : Command
	{
		// Token: 0x060071BD RID: 29117 RVA: 0x002A6890 File Offset: 0x002A4A90
		public override void OnEnter()
		{
			float num = (this.value.Value - this.initialRangeLower.Value) / (this.initialRangeUpper.Value - this.initialRangeLower.Value) * (this.newRangeUpper.Value - this.newRangeLower.Value);
			num += this.newRangeLower.Value;
			this.outValue.Value = num;
			this.Continue();
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x002A6908 File Offset: 0x002A4B08
		public override string GetSummary()
		{
			return string.Concat(new string[]
			{
				"Map [",
				this.initialRangeLower.Value.ToString(),
				"-",
				this.initialRangeUpper.Value.ToString(),
				"] to [",
				this.newRangeLower.Value.ToString(),
				"-",
				this.newRangeUpper.Value.ToString(),
				"]"
			});
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x002A69A4 File Offset: 0x002A4BA4
		public override bool HasReference(Variable variable)
		{
			return this.initialRangeLower.floatRef == variable || this.initialRangeUpper.floatRef == variable || this.value.floatRef == variable || this.newRangeLower.floatRef == variable || this.newRangeUpper.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0400641B RID: 25627
		[SerializeField]
		protected FloatData initialRangeLower = new FloatData(0f);

		// Token: 0x0400641C RID: 25628
		[SerializeField]
		protected FloatData initialRangeUpper = new FloatData(1f);

		// Token: 0x0400641D RID: 25629
		[SerializeField]
		protected FloatData value;

		// Token: 0x0400641E RID: 25630
		[SerializeField]
		protected FloatData newRangeLower = new FloatData(0f);

		// Token: 0x0400641F RID: 25631
		[SerializeField]
		protected FloatData newRangeUpper = new FloatData(1f);

		// Token: 0x04006420 RID: 25632
		[SerializeField]
		protected FloatData outValue;
	}
}
