using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E9C RID: 3740
	[EventHandlerInfo("Scene", "Flowchart Enabled", "The block will execute when the Flowchart game object is enabled.")]
	[AddComponentMenu("")]
	public class FlowchartEnabled : EventHandler
	{
		// Token: 0x06006A03 RID: 27139 RVA: 0x00292767 File Offset: 0x00290967
		protected virtual void OnEnable()
		{
			base.Invoke("DoEvent", 0f);
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x002921CC File Offset: 0x002903CC
		protected virtual void DoEvent()
		{
			this.ExecuteBlock();
		}
	}
}
