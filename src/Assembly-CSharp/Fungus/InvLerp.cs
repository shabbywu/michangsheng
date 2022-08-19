using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF6 RID: 3574
	[CommandInfo("Math", "InvLerp", "Calculates the inverse lerp, the percentage a value is between two others.", 0)]
	[AddComponentMenu("")]
	public class InvLerp : Command
	{
		// Token: 0x06006522 RID: 25890 RVA: 0x00281F00 File Offset: 0x00280100
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

		// Token: 0x06006523 RID: 25891 RVA: 0x00281F88 File Offset: 0x00280188
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

		// Token: 0x06006524 RID: 25892 RVA: 0x00282010 File Offset: 0x00280210
		public override bool HasReference(Variable variable)
		{
			return this.a.floatRef == variable || this.b.floatRef == variable || this.value.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040056F2 RID: 22258
		[Tooltip("Clamp percentage to 0-1?")]
		[SerializeField]
		protected bool clampResult = true;

		// Token: 0x040056F3 RID: 22259
		[SerializeField]
		protected FloatData a;

		// Token: 0x040056F4 RID: 22260
		[SerializeField]
		protected FloatData b;

		// Token: 0x040056F5 RID: 22261
		[SerializeField]
		protected FloatData value;

		// Token: 0x040056F6 RID: 22262
		[SerializeField]
		protected FloatData outValue;
	}
}
