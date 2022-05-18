using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001246 RID: 4678
	[CommandInfo("Math", "Round", "Command to execute and store the result of a Round", 0)]
	[AddComponentMenu("")]
	public class Round : BaseUnaryMathCommand
	{
		// Token: 0x060071CE RID: 29134 RVA: 0x002A6C20 File Offset: 0x002A4E20
		public override void OnEnter()
		{
			switch (this.function)
			{
			case Round.Mode.Round:
				this.outValue.Value = Mathf.Round(this.inValue.Value);
				break;
			case Round.Mode.Floor:
				this.outValue.Value = Mathf.Floor(this.inValue.Value);
				break;
			case Round.Mode.Ceil:
				this.outValue.Value = Mathf.Ceil(this.inValue.Value);
				break;
			}
			this.Continue();
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x0004D60F File Offset: 0x0004B80F
		public override string GetSummary()
		{
			return this.function.ToString() + " " + base.GetSummary();
		}

		// Token: 0x0400642B RID: 25643
		[Tooltip("Mode; Round (closest), floor(smaller) or ceil(bigger).")]
		[SerializeField]
		protected Round.Mode function;

		// Token: 0x02001247 RID: 4679
		public enum Mode
		{
			// Token: 0x0400642D RID: 25645
			Round,
			// Token: 0x0400642E RID: 25646
			Floor,
			// Token: 0x0400642F RID: 25647
			Ceil
		}
	}
}
