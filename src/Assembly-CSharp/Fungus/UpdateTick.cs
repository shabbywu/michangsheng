using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001334 RID: 4916
	[EventHandlerInfo("MonoBehaviour", "Update", "The block will execute every chosen Update, or FixedUpdate or LateUpdate.")]
	[AddComponentMenu("")]
	public class UpdateTick : EventHandler
	{
		// Token: 0x0600777B RID: 30587 RVA: 0x0005176D File Offset: 0x0004F96D
		private void Update()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.Update) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x00051780 File Offset: 0x0004F980
		private void FixedUpdate()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.FixedUpdate) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x00051793 File Offset: 0x0004F993
		private void LateUpdate()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.LateUpdate) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0400681F RID: 26655
		[Tooltip("Which of the Update messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected UpdateTick.UpdateMessageFlags FireOn = UpdateTick.UpdateMessageFlags.Update;

		// Token: 0x02001335 RID: 4917
		[Flags]
		public enum UpdateMessageFlags
		{
			// Token: 0x04006821 RID: 26657
			Update = 1,
			// Token: 0x04006822 RID: 26658
			FixedUpdate = 2,
			// Token: 0x04006823 RID: 26659
			LateUpdate = 4
		}
	}
}
