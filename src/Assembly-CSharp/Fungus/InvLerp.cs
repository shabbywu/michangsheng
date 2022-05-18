using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200123C RID: 4668
	[CommandInfo("Math", "InvLerp", "Calculates the inverse lerp, the percentage a value is between two others.", 0)]
	[AddComponentMenu("")]
	public class InvLerp : Command
	{
		// Token: 0x060071B0 RID: 29104 RVA: 0x002A6534 File Offset: 0x002A4734
		public override void OnEnter()
		{
			if (this.clampResult)
			{
				this.outValue.Value = Mathf.InverseLerp(this.a.Value, this.b.Value, this.value.Value);
			}
			else
			{
				this.outValue.Value = (this.value.Value - this.a.Value) / (this.b.Value - this.a.Value);
			}
			this.Continue();
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x002A65BC File Offset: 0x002A47BC
		public override string GetSummary()
		{
			if (this.outValue.floatRef == null)
			{
				return "Error: no out value selected";
			}
			return string.Concat(new string[]
			{
				this.outValue.floatRef.Key,
				" = [",
				this.a.Value.ToString(),
				"-",
				this.b.Value.ToString(),
				"]"
			});
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x002A6644 File Offset: 0x002A4844
		public override bool HasReference(Variable variable)
		{
			return this.a.floatRef == variable || this.b.floatRef == variable || this.value.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006409 RID: 25609
		[Tooltip("Clamp percentage to 0-1?")]
		[SerializeField]
		protected bool clampResult = true;

		// Token: 0x0400640A RID: 25610
		[SerializeField]
		protected FloatData a;

		// Token: 0x0400640B RID: 25611
		[SerializeField]
		protected FloatData b;

		// Token: 0x0400640C RID: 25612
		[SerializeField]
		protected FloatData value;

		// Token: 0x0400640D RID: 25613
		[SerializeField]
		protected FloatData outValue;
	}
}
