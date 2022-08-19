using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E04 RID: 3588
	[CommandInfo("Narrative", "Menu Shuffle", "Shuffle the order of the items in a Fungus Menu", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MenuShuffle : Command
	{
		// Token: 0x0600655E RID: 25950 RVA: 0x00282E1C File Offset: 0x0028101C
		public override void OnEnter()
		{
			MenuDialog menuDialog = MenuDialog.GetMenuDialog();
			if (this.shuffleMode == MenuShuffle.Mode.Every || this.seed == -1)
			{
				this.seed = Random.Range(0, 1000000);
			}
			if (menuDialog != null)
			{
				menuDialog.Shuffle(new Random(this.seed));
			}
			this.Continue();
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x00282E71 File Offset: 0x00281071
		public override string GetSummary()
		{
			return this.shuffleMode.ToString();
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x0400571A RID: 22298
		[SerializeField]
		[Tooltip("Determines if the order is shuffled everytime this command is it (Every) or if it is consistent when returned to but random (Once)")]
		protected MenuShuffle.Mode shuffleMode = MenuShuffle.Mode.Once;

		// Token: 0x0400571B RID: 22299
		private int seed = -1;

		// Token: 0x020016C0 RID: 5824
		public enum Mode
		{
			// Token: 0x0400738E RID: 29582
			Every,
			// Token: 0x0400738F RID: 29583
			Once
		}
	}
}
