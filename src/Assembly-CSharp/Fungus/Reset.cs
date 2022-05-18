using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200126A RID: 4714
	[CommandInfo("Variable", "Reset", "Resets the state of all commands and variables in the Flowchart.", 0)]
	[AddComponentMenu("")]
	public class Reset : Command
	{
		// Token: 0x06007273 RID: 29299 RVA: 0x0004DEC4 File Offset: 0x0004C0C4
		public override void OnEnter()
		{
			this.GetFlowchart().Reset(this.resetCommands, this.resetVariables);
			this.Continue();
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0400649C RID: 25756
		[Tooltip("Reset state of all commands in the script")]
		[SerializeField]
		protected bool resetCommands = true;

		// Token: 0x0400649D RID: 25757
		[Tooltip("Reset variables back to their default values")]
		[SerializeField]
		protected bool resetVariables = true;
	}
}
