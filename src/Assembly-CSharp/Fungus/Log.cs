using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF8 RID: 3576
	[CommandInfo("Math", "Log", "Command to execute and store the result of a Log", 0)]
	[AddComponentMenu("")]
	public class Log : BaseUnaryMathCommand
	{
		// Token: 0x0600652C RID: 25900 RVA: 0x00282234 File Offset: 0x00280434
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

		// Token: 0x0600652D RID: 25901 RVA: 0x0028228F File Offset: 0x0028048F
		public override string GetSummary()
		{
			return this.mode.ToString() + " " + base.GetSummary();
		}

		// Token: 0x040056FC RID: 22268
		[Tooltip("Which log to use, natural or base 10")]
		[SerializeField]
		protected Log.Mode mode = Log.Mode.Natural;

		// Token: 0x020016BB RID: 5819
		public enum Mode
		{
			// Token: 0x04007377 RID: 29559
			Base10,
			// Token: 0x04007378 RID: 29560
			Natural
		}
	}
}
