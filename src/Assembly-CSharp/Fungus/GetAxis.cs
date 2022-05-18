using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001213 RID: 4627
	[CommandInfo("Input", "GetAxis", "Store Input.GetAxis in a variable", 0)]
	[AddComponentMenu("")]
	public class GetAxis : Command
	{
		// Token: 0x06007124 RID: 28964 RVA: 0x002A4524 File Offset: 0x002A2724
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

		// Token: 0x06007125 RID: 28965 RVA: 0x0004CDED File Offset: 0x0004AFED
		public override string GetSummary()
		{
			return this.axisName + (this.axisRaw ? " Raw" : "");
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x0004CE13 File Offset: 0x0004B013
		public override bool HasReference(Variable variable)
		{
			return this.axisName.stringRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x04006372 RID: 25458
		[SerializeField]
		protected StringData axisName;

		// Token: 0x04006373 RID: 25459
		[Tooltip("If true, calls GetAxisRaw instead of GetAxis")]
		[SerializeField]
		protected bool axisRaw;

		// Token: 0x04006374 RID: 25460
		[Tooltip("Float to store the value of the GetAxis")]
		[SerializeField]
		protected FloatData outValue;
	}
}
