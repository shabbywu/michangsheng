using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAE RID: 3758
	[EventHandlerInfo("MonoBehaviour", "Update", "The block will execute every chosen Update, or FixedUpdate or LateUpdate.")]
	[AddComponentMenu("")]
	public class UpdateTick : EventHandler
	{
		// Token: 0x06006A45 RID: 27205 RVA: 0x00292C95 File Offset: 0x00290E95
		private void Update()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.Update) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x00292CA8 File Offset: 0x00290EA8
		private void FixedUpdate()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.FixedUpdate) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x00292CBB File Offset: 0x00290EBB
		private void LateUpdate()
		{
			if ((this.FireOn & UpdateTick.UpdateMessageFlags.LateUpdate) != (UpdateTick.UpdateMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059E2 RID: 23010
		[Tooltip("Which of the Update messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected UpdateTick.UpdateMessageFlags FireOn = UpdateTick.UpdateMessageFlags.Update;

		// Token: 0x020016FA RID: 5882
		[Flags]
		public enum UpdateMessageFlags
		{
			// Token: 0x04007499 RID: 29849
			Update = 1,
			// Token: 0x0400749A RID: 29850
			FixedUpdate = 2,
			// Token: 0x0400749B RID: 29851
			LateUpdate = 4
		}
	}
}
