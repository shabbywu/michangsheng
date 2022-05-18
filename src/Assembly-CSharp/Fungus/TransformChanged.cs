using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001330 RID: 4912
	[EventHandlerInfo("MonoBehaviour", "Transform", "The block will execute when the desired OnTransform related message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class TransformChanged : EventHandler
	{
		// Token: 0x06007770 RID: 30576 RVA: 0x0005170B File Offset: 0x0004F90B
		private void OnTransformChildrenChanged()
		{
			if ((this.FireOn & TransformChanged.TransformMessageFlags.OnTransformChildrenChanged) != (TransformChanged.TransformMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x0005171E File Offset: 0x0004F91E
		private void OnTransformParentChanged()
		{
			if ((this.FireOn & TransformChanged.TransformMessageFlags.OnTransformParentChanged) != (TransformChanged.TransformMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0400681B RID: 26651
		[Tooltip("Which of the OnTransformChanged messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected TransformChanged.TransformMessageFlags FireOn = TransformChanged.TransformMessageFlags.OnTransformChildrenChanged | TransformChanged.TransformMessageFlags.OnTransformParentChanged;

		// Token: 0x02001331 RID: 4913
		[Flags]
		public enum TransformMessageFlags
		{
			// Token: 0x0400681D RID: 26653
			OnTransformChildrenChanged = 1,
			// Token: 0x0400681E RID: 26654
			OnTransformParentChanged = 2
		}
	}
}
