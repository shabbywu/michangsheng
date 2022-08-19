using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAB RID: 3755
	[EventHandlerInfo("MonoBehaviour", "Transform", "The block will execute when the desired OnTransform related message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class TransformChanged : EventHandler
	{
		// Token: 0x06006A3A RID: 27194 RVA: 0x00292C33 File Offset: 0x00290E33
		private void OnTransformChildrenChanged()
		{
			if ((this.FireOn & TransformChanged.TransformMessageFlags.OnTransformChildrenChanged) != (TransformChanged.TransformMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x00292C46 File Offset: 0x00290E46
		private void OnTransformParentChanged()
		{
			if ((this.FireOn & TransformChanged.TransformMessageFlags.OnTransformParentChanged) != (TransformChanged.TransformMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059E1 RID: 23009
		[Tooltip("Which of the OnTransformChanged messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected TransformChanged.TransformMessageFlags FireOn = TransformChanged.TransformMessageFlags.OnTransformChildrenChanged | TransformChanged.TransformMessageFlags.OnTransformParentChanged;

		// Token: 0x020016F9 RID: 5881
		[Flags]
		public enum TransformMessageFlags
		{
			// Token: 0x04007496 RID: 29846
			OnTransformChildrenChanged = 1,
			// Token: 0x04007497 RID: 29847
			OnTransformParentChanged = 2
		}
	}
}
