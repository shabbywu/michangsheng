using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012A6 RID: 4774
	[CommandInfo("Variable", "To String", "Stores the result of a ToString on given variable in a string.", 0)]
	[AddComponentMenu("")]
	public class ToString : Command
	{
		// Token: 0x0600739E RID: 29598 RVA: 0x0004EE42 File Offset: 0x0004D042
		public override void OnEnter()
		{
			if (this.variable != null && this.outValue != null)
			{
				this.outValue.Value = this.variable.ToString();
			}
			this.Continue();
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x002AB510 File Offset: 0x002A9710
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			if (this.outValue == null)
			{
				return "Error: outValue not set";
			}
			return this.outValue.Key + " = " + this.variable.Key + ".ToString";
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x0004EE7C File Offset: 0x0004D07C
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.outValue == variable;
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0400656D RID: 25965
		[Tooltip("Target variable to get String of.")]
		[VariableProperty(new Type[]
		{

		})]
		[SerializeField]
		protected Variable variable;

		// Token: 0x0400656E RID: 25966
		[Tooltip("Variable to store the result of ToString")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable outValue;
	}
}
