using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200131A RID: 4890
	[EventHandlerInfo("Scene", "Flowchart Enabled", "The block will execute when the Flowchart game object is enabled.")]
	[AddComponentMenu("")]
	public class FlowchartEnabled : EventHandler
	{
		// Token: 0x06007733 RID: 30515 RVA: 0x000513A6 File Offset: 0x0004F5A6
		protected virtual void OnEnable()
		{
			base.Invoke("DoEvent", 0f);
		}

		// Token: 0x06007734 RID: 30516 RVA: 0x00050FED File Offset: 0x0004F1ED
		protected virtual void DoEvent()
		{
			this.ExecuteBlock();
		}
	}
}
