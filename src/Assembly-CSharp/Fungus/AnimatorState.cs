using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA1 RID: 3745
	[EventHandlerInfo("MonoBehaviour", "Animator", "The block will execute when the desired OnAnimator* message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class AnimatorState : EventHandler
	{
		// Token: 0x06006A10 RID: 27152 RVA: 0x0029295A File Offset: 0x00290B5A
		private void OnAnimatorIK(int layer)
		{
			if ((this.FireOn & AnimatorState.AnimatorMessageFlags.OnAnimatorIK) != (AnimatorState.AnimatorMessageFlags)0 && (this.IKLayer == layer || this.IKLayer < 0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x0029297F File Offset: 0x00290B7F
		private void OnAnimatorMove()
		{
			if ((this.FireOn & AnimatorState.AnimatorMessageFlags.OnAnimatorMove) != (AnimatorState.AnimatorMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059D9 RID: 23001
		[Tooltip("Which of the OnAnimator messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected AnimatorState.AnimatorMessageFlags FireOn = AnimatorState.AnimatorMessageFlags.OnAnimatorMove;

		// Token: 0x040059DA RID: 23002
		[Tooltip("IK layer to trigger on. Negative is all.")]
		[SerializeField]
		protected int IKLayer = 1;

		// Token: 0x020016F3 RID: 5875
		[Flags]
		public enum AnimatorMessageFlags
		{
			// Token: 0x04007476 RID: 29814
			OnAnimatorIK = 1,
			// Token: 0x04007477 RID: 29815
			OnAnimatorMove = 2
		}
	}
}
