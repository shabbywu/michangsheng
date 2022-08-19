using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DBC RID: 3516
	[CommandInfo("Narrative", "Clear Menu", "Clears the options from a menu dialogue", 0)]
	public class ClearMenu : Command
	{
		// Token: 0x0600640B RID: 25611 RVA: 0x0027D4F2 File Offset: 0x0027B6F2
		public override void OnEnter()
		{
			this.menuDialog.Clear();
			this.Continue();
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x0027D505 File Offset: 0x0027B705
		public override string GetSummary()
		{
			if (this.menuDialog == null)
			{
				return "Error: No menu dialog object selected";
			}
			return this.menuDialog.name;
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005616 RID: 22038
		[Tooltip("Menu Dialog to clear the options on")]
		[SerializeField]
		protected MenuDialog menuDialog;
	}
}
