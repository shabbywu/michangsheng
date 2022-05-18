using System;
using System.Globalization;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200120D RID: 4621
	[CommandInfo("Variable", "From String", "Attempts to parse a string into a given fungus variable type, such as integer or float", 0)]
	[AddComponentMenu("")]
	public class FromString : Command
	{
		// Token: 0x0600710E RID: 28942 RVA: 0x002A4314 File Offset: 0x002A2514
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

		// Token: 0x0600710F RID: 28943 RVA: 0x002A43D4 File Offset: 0x002A25D4
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

		// Token: 0x06007110 RID: 28944 RVA: 0x0004CCFA File Offset: 0x0004AEFA
		public override bool HasReference(Variable variable)
		{
			return variable == this.sourceString || variable == this.outValue;
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04006366 RID: 25446
		[Tooltip("Source of string data to parse into another variables value")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable sourceString;

		// Token: 0x04006367 RID: 25447
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
