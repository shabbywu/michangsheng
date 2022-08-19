using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFC RID: 3580
	[CommandInfo("Math", "Pow", "Raise a value to the power of another.", 0)]
	[AddComponentMenu("")]
	public class Pow : Command
	{
		// Token: 0x0600653B RID: 25915 RVA: 0x0028262D File Offset: 0x0028082D
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Pow(this.baseValue.Value, this.exponentValue.Value);
			this.Continue();
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x0028265C File Offset: 0x0028085C
		public override string GetSummary()
		{
			if (this.outValue.floatRef == null)
			{
				return "Error: No out value selected";
			}
			return string.Concat(new string[]
			{
				this.outValue.floatRef.Key,
				" = ",
				this.baseValue.Value.ToString(),
				"^",
				this.exponentValue.Value.ToString()
			});
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x002826DC File Offset: 0x002808DC
		public override bool HasReference(Variable variable)
		{
			return this.baseValue.floatRef == variable || this.exponentValue.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x04005707 RID: 22279
		[SerializeField]
		protected FloatData baseValue;

		// Token: 0x04005708 RID: 22280
		[SerializeField]
		protected FloatData exponentValue;

		// Token: 0x04005709 RID: 22281
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected FloatData outValue;
	}
}
