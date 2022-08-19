using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E1C RID: 3612
	[CommandInfo("Variable", "Read Text File", "Reads in a text file and stores the contents in a string variable", 0)]
	public class ReadTextFile : Command
	{
		// Token: 0x060065E0 RID: 26080 RVA: 0x002844D4 File Offset: 0x002826D4
		public override void OnEnter()
		{
			if (this.textFile == null || this.stringVariable == null)
			{
				this.Continue();
				return;
			}
			this.stringVariable.Value = this.textFile.text;
			this.Continue();
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x00284520 File Offset: 0x00282720
		public override string GetSummary()
		{
			if (this.stringVariable == null)
			{
				return "Error: Variable not selected";
			}
			if (this.textFile == null)
			{
				return "Error: Text file not selected";
			}
			return this.stringVariable.Key;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00284555 File Offset: 0x00282755
		public override bool HasReference(Variable variable)
		{
			return variable == this.stringVariable;
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x04005765 RID: 22373
		[Tooltip("Text file to read into the string variable")]
		[SerializeField]
		protected TextAsset textFile;

		// Token: 0x04005766 RID: 22374
		[Tooltip("String variable to store the tex file contents in")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable stringVariable;
	}
}
