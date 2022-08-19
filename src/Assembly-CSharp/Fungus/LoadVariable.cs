using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DEA RID: 3562
	[CommandInfo("Variable", "Load Variable", "Loads a saved value and stores it in a Boolean, Integer, Float or String variable. If the key is not found then the variable is not modified.", 0)]
	[AddComponentMenu("")]
	public class LoadVariable : Command
	{
		// Token: 0x060064F4 RID: 25844 RVA: 0x00281470 File Offset: 0x0027F670
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

		// Token: 0x060064F5 RID: 25845 RVA: 0x002815C0 File Offset: 0x0027F7C0
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

		// Token: 0x060064F6 RID: 25846 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x00281614 File Offset: 0x0027F814
		public override bool HasReference(Variable in_variable)
		{
			return this.variable == in_variable || base.HasReference(in_variable);
		}

		// Token: 0x040056D9 RID: 22233
		[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}\"")]
		[SerializeField]
		protected string key = "";

		// Token: 0x040056DA RID: 22234
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
