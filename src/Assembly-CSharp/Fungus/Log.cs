using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200123F RID: 4671
	[CommandInfo("Math", "Log", "Command to execute and store the result of a Log", 0)]
	[AddComponentMenu("")]
	public class Log : BaseUnaryMathCommand
	{
		// Token: 0x060071BA RID: 29114 RVA: 0x002A6834 File Offset: 0x002A4A34
		public override void OnEnter()
		{
			Log.Mode mode = this.mode;
			if (mode != Log.Mode.Base10)
			{
				if (mode == Log.Mode.Natural)
				{
					this.outValue.Value = Mathf.Log(this.inValue.Value);
				}
			}
			else
			{
				this.outValue.Value = Mathf.Log10(this.inValue.Value);
			}
			this.Continue();
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x0004D51A File Offset: 0x0004B71A
		public override string GetSummary()
		{
			return this.mode.ToString() + " " + base.GetSummary();
		}

		// Token: 0x04006417 RID: 25623
		[Tooltip("Which log to use, natural or base 10")]
		[SerializeField]
		protected Log.Mode mode = Log.Mode.Natural;

		// Token: 0x02001240 RID: 4672
		public enum Mode
		{
			// Token: 0x04006419 RID: 25625
			Base10,
			// Token: 0x0400641A RID: 25626
			Natural
		}
	}
}
