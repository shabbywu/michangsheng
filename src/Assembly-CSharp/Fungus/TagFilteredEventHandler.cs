using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAA RID: 3754
	[AddComponentMenu("")]
	public abstract class TagFilteredEventHandler : EventHandler
	{
		// Token: 0x06006A38 RID: 27192 RVA: 0x00292C0A File Offset: 0x00290E0A
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

		// Token: 0x040059E0 RID: 23008
		[Tooltip("Only fire the event if one of the tags match. Empty means any will fire.")]
		[SerializeField]
		protected string[] tagFilter;
	}
}
