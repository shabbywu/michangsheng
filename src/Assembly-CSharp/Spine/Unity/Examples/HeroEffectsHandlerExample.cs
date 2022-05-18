using System;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E35 RID: 3637
	public class HeroEffectsHandlerExample : MonoBehaviour
	{
		// Token: 0x06005784 RID: 22404 RVA: 0x0024525C File Offset: 0x0024345C
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

		// Token: 0x04005772 RID: 22386
		public BasicPlatformerController eventSource;

		// Token: 0x04005773 RID: 22387
		public UnityEvent OnJump;

		// Token: 0x04005774 RID: 22388
		public UnityEvent OnLand;

		// Token: 0x04005775 RID: 22389
		public UnityEvent OnHardLand;
	}
}
