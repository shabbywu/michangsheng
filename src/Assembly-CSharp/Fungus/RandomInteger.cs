using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001268 RID: 4712
	[CommandInfo("Variable", "Random Integer", "Sets an integer variable to a random value in the defined range.", 0)]
	[AddComponentMenu("")]
	public class RandomInteger : Command
	{
		// Token: 0x06007269 RID: 29289 RVA: 0x0004DDEE File Offset: 0x0004BFEE
		public override void OnEnter()
		{
			if (this.variable != null)
			{
				this.variable.Value = Random.Range(this.minValue.Value, this.maxValue.Value);
			}
			this.Continue();
		}

		// Token: 0x0600726A RID: 29290 RVA: 0x0004DE2A File Offset: 0x0004C02A
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			return this.variable.Key;
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x0004DE4B File Offset: 0x0004C04B
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.minValue.integerRef == variable || this.maxValue.integerRef == variable;
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04006497 RID: 25751
		[Tooltip("The variable whos value will be set")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable variable;

		// Token: 0x04006498 RID: 25752
		[Tooltip("Minimum value for random range")]
		[SerializeField]
		protected IntegerData minValue;

		// Token: 0x04006499 RID: 25753
		[Tooltip("Maximum value for random range")]
		[SerializeField]
		protected IntegerData maxValue;
	}
}
