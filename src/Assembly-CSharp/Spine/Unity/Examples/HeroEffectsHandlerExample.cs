using System;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE7 RID: 2791
	public class HeroEffectsHandlerExample : MonoBehaviour
	{
		// Token: 0x06004E08 RID: 19976 RVA: 0x00215180 File Offset: 0x00213380
		public void Awake()
		{
			if (this.eventSource == null)
			{
				return;
			}
			this.eventSource.OnLand += new UnityAction(this.OnLand.Invoke);
			this.eventSource.OnJump += new UnityAction(this.OnJump.Invoke);
			this.eventSource.OnHardLand += new UnityAction(this.OnHardLand.Invoke);
		}

		// Token: 0x04004D66 RID: 19814
		public BasicPlatformerController eventSource;

		// Token: 0x04004D67 RID: 19815
		public UnityEvent OnJump;

		// Token: 0x04004D68 RID: 19816
		public UnityEvent OnLand;

		// Token: 0x04004D69 RID: 19817
		public UnityEvent OnHardLand;
	}
}
