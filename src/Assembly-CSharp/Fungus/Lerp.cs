using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF7 RID: 3575
	[CommandInfo("Math", "Lerp", "Linearly Interpolate from A to B", 0)]
	[AddComponentMenu("")]
	public class Lerp : Command
	{
		// Token: 0x06006527 RID: 25895 RVA: 0x00282078 File Offset: 0x00280278
		public override void OnEnter()
		{
			switch (this.mode)
			{
			case Lerp.Mode.Lerp:
				this.outValue.Value = Mathf.Lerp(this.a.Value, this.b.Value, this.percentage.Value);
				break;
			case Lerp.Mode.LerpUnclamped:
				this.outValue.Value = Mathf.LerpUnclamped(this.a.Value, this.b.Value, this.percentage.Value);
				break;
			case Lerp.Mode.LerpAngle:
				this.outValue.Value = Mathf.LerpAngle(this.a.Value, this.b.Value, this.percentage.Value);
				break;
			}
			this.Continue();
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x00282140 File Offset: 0x00280340
		public override string GetSummary()
		{
			return string.Concat(new string[]
			{
				this.mode.ToString(),
				" [",
				this.a.Value.ToString(),
				"-",
				this.b.Value.ToString(),
				"]"
			});
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x002821B0 File Offset: 0x002803B0
		public override bool HasReference(Variable variable)
		{
			return this.a.floatRef == variable || this.b.floatRef == variable || this.percentage.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040056F7 RID: 22263
		[SerializeField]
		protected Lerp.Mode mode;

		// Token: 0x040056F8 RID: 22264
		[SerializeField]
		protected FloatData a = new FloatData(0f);

		// Token: 0x040056F9 RID: 22265
		[SerializeField]
		protected FloatData b = new FloatData(1f);

		// Token: 0x040056FA RID: 22266
		[SerializeField]
		protected FloatData percentage;

		// Token: 0x040056FB RID: 22267
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x020016BA RID: 5818
		public enum Mode
		{
			// Token: 0x04007373 RID: 29555
			Lerp,
			// Token: 0x04007374 RID: 29556
			LerpUnclamped,
			// Token: 0x04007375 RID: 29557
			LerpAngle
		}
	}
}
