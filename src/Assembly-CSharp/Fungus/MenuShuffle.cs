using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001250 RID: 4688
	[CommandInfo("Narrative", "Menu Shuffle", "Shuffle the order of the items in a Fungus Menu", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class MenuShuffle : Command
	{
		// Token: 0x060071EC RID: 29164 RVA: 0x002A71B8 File Offset: 0x002A53B8
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

		// Token: 0x060071ED RID: 29165 RVA: 0x0004D77E File Offset: 0x0004B97E
		public override string GetSummary()
		{
			return this.shuffleMode.ToString();
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x0400644C RID: 25676
		[SerializeField]
		[Tooltip("Determines if the order is shuffled everytime this command is it (Every) or if it is consistent when returned to but random (Once)")]
		protected MenuShuffle.Mode shuffleMode = MenuShuffle.Mode.Once;

		// Token: 0x0400644D RID: 25677
		private int seed = -1;

		// Token: 0x02001251 RID: 4689
		public enum Mode
		{
			// Token: 0x0400644F RID: 25679
			Every,
			// Token: 0x04006450 RID: 25680
			Once
		}
	}
}
