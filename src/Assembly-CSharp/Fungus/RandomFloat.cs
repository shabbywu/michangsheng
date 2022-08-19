using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E1A RID: 3610
	[CommandInfo("Variable", "Random Float", "Sets an float variable to a random value in the defined range.", 0)]
	[AddComponentMenu("")]
	public class RandomFloat : Command
	{
		// Token: 0x060065D6 RID: 26070 RVA: 0x002843AB File Offset: 0x002825AB
		public override void OnEnter()
		{
			if (this.variable != null)
			{
				this.variable.Value = Random.Range(this.minValue.Value, this.maxValue.Value);
			}
			this.Continue();
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x002843E7 File Offset: 0x002825E7
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			return this.variable.Key;
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x00284408 File Offset: 0x00282608
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.minValue.floatRef == variable || this.maxValue.floatRef == variable;
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0400575F RID: 22367
		[Tooltip("The variable whos value will be set")]
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable)
		})]
		[SerializeField]
		protected FloatVariable variable;

		// Token: 0x04005760 RID: 22368
		[Tooltip("Minimum value for random range")]
		[SerializeField]
		protected FloatData minValue;

		// Token: 0x04005761 RID: 22369
		[Tooltip("Maximum value for random range")]
		[SerializeField]
		protected FloatData maxValue;
	}
}
