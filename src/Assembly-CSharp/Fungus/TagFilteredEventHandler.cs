using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200132F RID: 4911
	[AddComponentMenu("")]
	public abstract class TagFilteredEventHandler : EventHandler
	{
		// Token: 0x0600776E RID: 30574 RVA: 0x000516E2 File Offset: 0x0004F8E2
		protected void ProcessTagFilter(string tagOnOther)
		{
			if (this.tagFilter.Length == 0)
			{
				this.ExecuteBlock();
				return;
			}
			if (Array.IndexOf<string>(this.tagFilter, tagOnOther) != -1)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0400681A RID: 26650
		[Tooltip("Only fire the event if one of the tags match. Empty means any will fire.")]
		[SerializeField]
		protected string[] tagFilter;
	}
}
