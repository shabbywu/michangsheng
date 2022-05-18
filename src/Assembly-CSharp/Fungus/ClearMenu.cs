using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011F2 RID: 4594
	[CommandInfo("Narrative", "Clear Menu", "Clears the options from a menu dialogue", 0)]
	public class ClearMenu : Command
	{
		// Token: 0x06007078 RID: 28792 RVA: 0x0004C668 File Offset: 0x0004A868
		public override void OnEnter()
		{
			this.menuDialog.Clear();
			this.Continue();
		}

		// Token: 0x06007079 RID: 28793 RVA: 0x0004C67B File Offset: 0x0004A87B
		public override string GetSummary()
		{
			if (this.menuDialog == null)
			{
				return "Error: No menu dialog object selected";
			}
			return this.menuDialog.name;
		}

		// Token: 0x0600707A RID: 28794 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x0400630B RID: 25355
		[Tooltip("Menu Dialog to clear the options on")]
		[SerializeField]
		protected MenuDialog menuDialog;
	}
}
