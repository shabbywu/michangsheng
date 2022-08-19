using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFD RID: 3581
	[CommandInfo("Math", "Round", "Command to execute and store the result of a Round", 0)]
	[AddComponentMenu("")]
	public class Round : BaseUnaryMathCommand
	{
		// Token: 0x06006540 RID: 25920 RVA: 0x00282718 File Offset: 0x00280918
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

		// Token: 0x06006541 RID: 25921 RVA: 0x0028279B File Offset: 0x0028099B
		public override string GetSummary()
		{
			return this.function.ToString() + " " + base.GetSummary();
		}

		// Token: 0x0400570A RID: 22282
		[Tooltip("Mode; Round (closest), floor(smaller) or ceil(bigger).")]
		[SerializeField]
		protected Round.Mode function;

		// Token: 0x020016BD RID: 5821
		public enum Mode
		{
			// Token: 0x0400737D RID: 29565
			Round,
			// Token: 0x0400737E RID: 29566
			Floor,
			// Token: 0x0400737F RID: 29567
			Ceil
		}
	}
}
