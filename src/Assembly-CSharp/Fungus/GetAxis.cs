using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DD8 RID: 3544
	[CommandInfo("Input", "GetAxis", "Store Input.GetAxis in a variable", 0)]
	[AddComponentMenu("")]
	public class GetAxis : Command
	{
		// Token: 0x060064A2 RID: 25762 RVA: 0x0027F8DC File Offset: 0x0027DADC
		public override void OnEnter()
		{
			if (this.axisRaw)
			{
				this.outValue.Value = Input.GetAxisRaw(this.axisName.Value);
			}
			else
			{
				this.outValue.Value = Input.GetAxis(this.axisName.Value);
			}
			this.Continue();
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x0027F92F File Offset: 0x0027DB2F
		public override string GetSummary()
		{
			return this.axisName + (this.axisRaw ? " Raw" : "");
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x0027F955 File Offset: 0x0027DB55
		public override bool HasReference(Variable variable)
		{
			return this.axisName.stringRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x04005672 RID: 22130
		[SerializeField]
		protected StringData axisName;

		// Token: 0x04005673 RID: 22131
		[Tooltip("If true, calls GetAxisRaw instead of GetAxis")]
		[SerializeField]
		protected bool axisRaw;

		// Token: 0x04005674 RID: 22132
		[Tooltip("Float to store the value of the GetAxis")]
		[SerializeField]
		protected FloatData outValue;
	}
}
