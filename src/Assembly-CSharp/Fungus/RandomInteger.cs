using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E1B RID: 3611
	[CommandInfo("Variable", "Random Integer", "Sets an integer variable to a random value in the defined range.", 0)]
	[AddComponentMenu("")]
	public class RandomInteger : Command
	{
		// Token: 0x060065DB RID: 26075 RVA: 0x0028443E File Offset: 0x0028263E
		public override void OnEnter()
		{
			if (this.variable != null)
			{
				this.variable.Value = Random.Range(this.minValue.Value, this.maxValue.Value);
			}
			this.Continue();
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x0028447A File Offset: 0x0028267A
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			return this.variable.Key;
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x0028449B File Offset: 0x0028269B
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.minValue.integerRef == variable || this.maxValue.integerRef == variable;
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04005762 RID: 22370
		[Tooltip("The variable whos value will be set")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable variable;

		// Token: 0x04005763 RID: 22371
		[Tooltip("Minimum value for random range")]
		[SerializeField]
		protected IntegerData minValue;

		// Token: 0x04005764 RID: 22372
		[Tooltip("Maximum value for random range")]
		[SerializeField]
		protected IntegerData maxValue;
	}
}
