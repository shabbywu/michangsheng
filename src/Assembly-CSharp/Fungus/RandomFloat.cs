using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001267 RID: 4711
	[CommandInfo("Variable", "Random Float", "Sets an float variable to a random value in the defined range.", 0)]
	[AddComponentMenu("")]
	public class RandomFloat : Command
	{
		// Token: 0x06007264 RID: 29284 RVA: 0x0004DD5B File Offset: 0x0004BF5B
		public override void OnEnter()
		{
			if (this.variable != null)
			{
				this.variable.Value = Random.Range(this.minValue.Value, this.maxValue.Value);
			}
			this.Continue();
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x0004DD97 File Offset: 0x0004BF97
		public override string GetSummary()
		{
			if (this.variable == null)
			{
				return "Error: Variable not selected";
			}
			return this.variable.Key;
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x0004DDB8 File Offset: 0x0004BFB8
		public override bool HasReference(Variable variable)
		{
			return variable == this.variable || this.minValue.floatRef == variable || this.maxValue.floatRef == variable;
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04006494 RID: 25748
		[Tooltip("The variable whos value will be set")]
		[VariableProperty(new Type[]
		{
			typeof(FloatVariable)
		})]
		[SerializeField]
		protected FloatVariable variable;

		// Token: 0x04006495 RID: 25749
		[Tooltip("Minimum value for random range")]
		[SerializeField]
		protected FloatData minValue;

		// Token: 0x04006496 RID: 25750
		[Tooltip("Maximum value for random range")]
		[SerializeField]
		protected FloatData maxValue;
	}
}
