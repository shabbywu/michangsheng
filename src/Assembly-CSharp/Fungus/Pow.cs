using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001245 RID: 4677
	[CommandInfo("Math", "Pow", "Raise a value to the power of another.", 0)]
	[AddComponentMenu("")]
	public class Pow : Command
	{
		// Token: 0x060071C9 RID: 29129 RVA: 0x0004D5A6 File Offset: 0x0004B7A6
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Pow(this.baseValue.Value, this.exponentValue.Value);
			this.Continue();
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x002A6BA0 File Offset: 0x002A4DA0
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

		// Token: 0x060071CB RID: 29131 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x0004D5D4 File Offset: 0x0004B7D4
		public override bool HasReference(Variable variable)
		{
			return this.baseValue.floatRef == variable || this.exponentValue.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x04006428 RID: 25640
		[SerializeField]
		protected FloatData baseValue;

		// Token: 0x04006429 RID: 25641
		[SerializeField]
		protected FloatData exponentValue;

		// Token: 0x0400642A RID: 25642
		[Tooltip("Where the result of the function is stored.")]
		[SerializeField]
		protected FloatData outValue;
	}
}
