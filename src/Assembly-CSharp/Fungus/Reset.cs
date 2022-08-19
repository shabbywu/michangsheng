using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E1D RID: 3613
	[CommandInfo("Variable", "Reset", "Resets the state of all commands and variables in the Flowchart.", 0)]
	[AddComponentMenu("")]
	public class Reset : Command
	{
		// Token: 0x060065E5 RID: 26085 RVA: 0x00284563 File Offset: 0x00282763
		public override void OnEnter()
		{
			this.GetFlowchart().Reset(this.resetCommands, this.resetVariables);
			this.Continue();
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005767 RID: 22375
		[Tooltip("Reset state of all commands in the script")]
		[SerializeField]
		protected bool resetCommands = true;

		// Token: 0x04005768 RID: 22376
		[Tooltip("Reset variables back to their default values")]
		[SerializeField]
		protected bool resetVariables = true;
	}
}
