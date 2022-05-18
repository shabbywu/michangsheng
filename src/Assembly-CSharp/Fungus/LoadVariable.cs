using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200122E RID: 4654
	[CommandInfo("Variable", "Load Variable", "Loads a saved value and stores it in a Boolean, Integer, Float or String variable. If the key is not found then the variable is not modified.", 0)]
	[AddComponentMenu("")]
	public class LoadVariable : Command
	{
		// Token: 0x06007180 RID: 29056 RVA: 0x002A5C5C File Offset: 0x002A3E5C
		public override void OnEnter()
		{
			if (this.key == "" || this.variable == null)
			{
				this.Continue();
				return;
			}
			Flowchart flowchart = this.GetFlowchart();
			string text = SetSaveProfile.SaveProfile + "_" + flowchart.SubstituteVariables(this.key);
			Type type = this.variable.GetType();
			if (type == typeof(BooleanVariable))
			{
				BooleanVariable booleanVariable = this.variable as BooleanVariable;
				if (booleanVariable != null)
				{
					booleanVariable.Value = (PlayerPrefs.GetInt(text) == 1);
				}
			}
			else if (type == typeof(IntegerVariable))
			{
				IntegerVariable integerVariable = this.variable as IntegerVariable;
				if (integerVariable != null)
				{
					integerVariable.Value = PlayerPrefs.GetInt(text);
				}
			}
			else if (type == typeof(FloatVariable))
			{
				FloatVariable floatVariable = this.variable as FloatVariable;
				if (floatVariable != null)
				{
					floatVariable.Value = PlayerPrefs.GetFloat(text);
				}
			}
			else if (type == typeof(StringVariable))
			{
				StringVariable stringVariable = this.variable as StringVariable;
				if (stringVariable != null)
				{
					stringVariable.Value = PlayerPrefs.GetString(text);
				}
			}
			this.Continue();
		}

		// Token: 0x06007181 RID: 29057 RVA: 0x002A5DAC File Offset: 0x002A3FAC
		public override string GetSummary()
		{
			if (this.key.Length == 0)
			{
				return "Error: No stored value key selected";
			}
			if (this.variable == null)
			{
				return "Error: No variable selected";
			}
			return "'" + this.key + "' into " + this.variable.Key;
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x0004D2F6 File Offset: 0x0004B4F6
		public override bool HasReference(Variable in_variable)
		{
			return this.variable == in_variable || base.HasReference(in_variable);
		}

		// Token: 0x040063EA RID: 25578
		[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}\"")]
		[SerializeField]
		protected string key = "";

		// Token: 0x040063EB RID: 25579
		[Tooltip("Variable to store the value in.")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable),
			typeof(IntegerVariable),
			typeof(FloatVariable),
			typeof(StringVariable)
		})]
		[SerializeField]
		protected Variable variable;
	}
}
