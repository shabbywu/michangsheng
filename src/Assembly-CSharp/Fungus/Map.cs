using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF9 RID: 3577
	[CommandInfo("Math", "Map", "Map a value that exists in 1 range of numbers to another.", 0)]
	[AddComponentMenu("")]
	public class Map : Command
	{
		// Token: 0x0600652F RID: 25903 RVA: 0x002822C4 File Offset: 0x002804C4
		public override void OnEnter()
		{
			float num = (this.value.Value - this.initialRangeLower.Value) / (this.initialRangeUpper.Value - this.initialRangeLower.Value) * (this.newRangeUpper.Value - this.newRangeLower.Value);
			num += this.newRangeLower.Value;
			this.outValue.Value = num;
			this.Continue();
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0028233C File Offset: 0x0028053C
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

		// Token: 0x06006531 RID: 25905 RVA: 0x002823D8 File Offset: 0x002805D8
		public override bool HasReference(Variable variable)
		{
			return this.initialRangeLower.floatRef == variable || this.initialRangeUpper.floatRef == variable || this.value.floatRef == variable || this.newRangeLower.floatRef == variable || this.newRangeUpper.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040056FD RID: 22269
		[SerializeField]
		protected FloatData initialRangeLower = new FloatData(0f);

		// Token: 0x040056FE RID: 22270
		[SerializeField]
		protected FloatData initialRangeUpper = new FloatData(1f);

		// Token: 0x040056FF RID: 22271
		[SerializeField]
		protected FloatData value;

		// Token: 0x04005700 RID: 22272
		[SerializeField]
		protected FloatData newRangeLower = new FloatData(0f);

		// Token: 0x04005701 RID: 22273
		[SerializeField]
		protected FloatData newRangeUpper = new FloatData(1f);

		// Token: 0x04005702 RID: 22274
		[SerializeField]
		protected FloatData outValue;
	}
}
