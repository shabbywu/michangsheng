using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E26 RID: 3622
	[CommandInfo("Variable", "Save Variable", "Save an Boolean, Integer, Float or String variable to persistent storage using a string key. The value can be loaded again later using the Load Variable command. You can also use the Set Save Profile command to manage separate save profiles for multiple players.", 0)]
	[AddComponentMenu("")]
	public class SaveVariable : Command
	{
		// Token: 0x06006613 RID: 26131 RVA: 0x00285050 File Offset: 0x00283250
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
					PlayerPrefs.SetInt(text, booleanVariable.Value ? 1 : 0);
				}
			}
			else if (type == typeof(IntegerVariable))
			{
				IntegerVariable integerVariable = this.variable as IntegerVariable;
				if (integerVariable != null)
				{
					PlayerPrefs.SetInt(text, integerVariable.Value);
				}
			}
			else if (type == typeof(FloatVariable))
			{
				FloatVariable floatVariable = this.variable as FloatVariable;
				if (floatVariable != null)
				{
					PlayerPrefs.SetFloat(text, floatVariable.Value);
				}
			}
			else if (type == typeof(StringVariable))
			{
				StringVariable stringVariable = this.variable as StringVariable;
				if (stringVariable != null)
				{
					PlayerPrefs.SetString(text, stringVariable.Value);
				}
			}
			this.Continue();
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x002851A0 File Offset: 0x002833A0
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
			return this.variable.Key + " into '" + this.key + "'";
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x002851F4 File Offset: 0x002833F4
		public override bool HasReference(Variable in_variable)
		{
			return this.variable == in_variable || base.HasReference(in_variable);
		}

		// Token: 0x0400578D RID: 22413
		[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}")]
		[SerializeField]
		protected string key = "";

		// Token: 0x0400578E RID: 22414
		[Tooltip("Variable to read the value from. Only Boolean, Integer, Float and String are supported.")]
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
