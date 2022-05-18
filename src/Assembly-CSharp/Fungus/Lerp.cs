using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200123D RID: 4669
	[CommandInfo("Math", "Lerp", "Linearly Interpolate from A to B", 0)]
	[AddComponentMenu("")]
	public class Lerp : Command
	{
		// Token: 0x060071B5 RID: 29109 RVA: 0x002A66A0 File Offset: 0x002A48A0
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

		// Token: 0x060071B6 RID: 29110 RVA: 0x002A6768 File Offset: 0x002A4968
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

		// Token: 0x060071B7 RID: 29111 RVA: 0x002A67D8 File Offset: 0x002A49D8
		public override bool HasReference(Variable variable)
		{
			return this.a.floatRef == variable || this.b.floatRef == variable || this.percentage.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0400640E RID: 25614
		[SerializeField]
		protected Lerp.Mode mode;

		// Token: 0x0400640F RID: 25615
		[SerializeField]
		protected FloatData a = new FloatData(0f);

		// Token: 0x04006410 RID: 25616
		[SerializeField]
		protected FloatData b = new FloatData(1f);

		// Token: 0x04006411 RID: 25617
		[SerializeField]
		protected FloatData percentage;

		// Token: 0x04006412 RID: 25618
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x0200123E RID: 4670
		public enum Mode
		{
			// Token: 0x04006414 RID: 25620
			Lerp,
			// Token: 0x04006415 RID: 25621
			LerpUnclamped,
			// Token: 0x04006416 RID: 25622
			LerpAngle
		}
	}
}
