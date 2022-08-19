using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E55 RID: 3669
	[CommandInfo("Variable", "To String", "Stores the result of a ToString on given variable in a string.", 0)]
	[AddComponentMenu("")]
	public class ToString : Command
	{
		// Token: 0x06006710 RID: 26384 RVA: 0x0028884C File Offset: 0x00286A4C
		public override void OnEnter()
		{
			if (this.variable != null && this.outValue != null)
			{
				this.outValue.Value = this.variable.ToString();
			}
			this.Continue();
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x00288888 File Offset: 0x00286A88
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

		// Token: 0x06006712 RID: 26386 RVA: 0x002888E2 File Offset: 0x00286AE2
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.outValue == variable;
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04005829 RID: 22569
		[Tooltip("Target variable to get String of.")]
		[VariableProperty(new Type[]
		{

		})]
		[SerializeField]
		protected Variable variable;

		// Token: 0x0400582A RID: 22570
		[Tooltip("Variable to store the result of ToString")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable outValue;
	}
}
