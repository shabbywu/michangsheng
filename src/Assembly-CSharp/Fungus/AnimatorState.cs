using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001320 RID: 4896
	[EventHandlerInfo("MonoBehaviour", "Animator", "The block will execute when the desired OnAnimator* message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class AnimatorState : EventHandler
	{
		// Token: 0x06007746 RID: 30534 RVA: 0x00051432 File Offset: 0x0004F632
		private void OnAnimatorIK(int layer)
		{
			if ((this.FireOn & AnimatorState.AnimatorMessageFlags.OnAnimatorIK) != (AnimatorState.AnimatorMessageFlags)0 && (this.IKLayer == layer || this.IKLayer < 0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x00051457 File Offset: 0x0004F657
		private void OnAnimatorMove()
		{
			if ((this.FireOn & AnimatorState.AnimatorMessageFlags.OnAnimatorMove) != (AnimatorState.AnimatorMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040067F3 RID: 26611
		[Tooltip("Which of the OnAnimator messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected AnimatorState.AnimatorMessageFlags FireOn = AnimatorState.AnimatorMessageFlags.OnAnimatorMove;

		// Token: 0x040067F4 RID: 26612
		[Tooltip("IK layer to trigger on. Negative is all.")]
		[SerializeField]
		protected int IKLayer = 1;

		// Token: 0x02001321 RID: 4897
		[Flags]
		public enum AnimatorMessageFlags
		{
			// Token: 0x040067F6 RID: 26614
			OnAnimatorIK = 1,
			// Token: 0x040067F7 RID: 26615
			OnAnimatorMove = 2
		}
	}
}
