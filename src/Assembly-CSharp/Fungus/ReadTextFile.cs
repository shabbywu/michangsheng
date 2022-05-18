using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001269 RID: 4713
	[CommandInfo("Variable", "Read Text File", "Reads in a text file and stores the contents in a string variable", 0)]
	public class ReadTextFile : Command
	{
		// Token: 0x0600726E RID: 29294 RVA: 0x002A816C File Offset: 0x002A636C
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

		// Token: 0x0600726F RID: 29295 RVA: 0x0004DE81 File Offset: 0x0004C081
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

		// Token: 0x06007270 RID: 29296 RVA: 0x0004DEB6 File Offset: 0x0004C0B6
		public override bool HasReference(Variable variable)
		{
			return variable == this.stringVariable;
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x0400649A RID: 25754
		[Tooltip("Text file to read into the string variable")]
		[SerializeField]
		protected TextAsset textFile;

		// Token: 0x0400649B RID: 25755
		[Tooltip("String variable to store the tex file contents in")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable stringVariable;
	}
}
