using System;
using System.Globalization;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DD2 RID: 3538
	[CommandInfo("Variable", "From String", "Attempts to parse a string into a given fungus variable type, such as integer or float", 0)]
	[AddComponentMenu("")]
	public class FromString : Command
	{
		// Token: 0x0600648C RID: 25740 RVA: 0x0027F5DC File Offset: 0x0027D7DC
		public override void OnEnter()
		{
			if (this.sourceString != null && this.outValue != null)
			{
				double num = 0.0;
				try
				{
					num = Convert.ToDouble(this.sourceString.Value, CultureInfo.CurrentCulture);
				}
				catch (Exception)
				{
					Debug.LogWarning("Failed to parse as number: " + this.sourceString.Value);
				}
				IntegerVariable integerVariable = this.outValue as IntegerVariable;
				if (integerVariable != null)
				{
					integerVariable.Value = (int)num;
				}
				else
				{
					FloatVariable floatVariable = this.outValue as FloatVariable;
					if (floatVariable != null)
					{
						floatVariable.Value = (float)num;
					}
				}
			}
			this.Continue();
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x0027F69C File Offset: 0x0027D89C
		public override string GetSummary()
		{
			if (this.sourceString == null)
			{
				return "Error: No source string selected";
			}
			if (this.outValue == null)
			{
				return "Error: No type and storage variable selected";
			}
			return this.outValue.Key + ".Parse " + this.sourceString.Key;
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x0027F6F1 File Offset: 0x0027D8F1
		public override bool HasReference(Variable variable)
		{
			return variable == this.sourceString || variable == this.outValue;
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04005666 RID: 22118
		[Tooltip("Source of string data to parse into another variables value")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable sourceString;

		// Token: 0x04005667 RID: 22119
		[Tooltip("The variable type to be parsed and value stored within")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable),
			typeof(FloatVariable)
		})]
		[SerializeField]
		protected Variable outValue;
	}
}
