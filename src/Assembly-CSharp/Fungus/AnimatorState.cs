using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Animator", "The block will execute when the desired OnAnimator* message for the monobehaviour is received.")]
[AddComponentMenu("")]
public class AnimatorState : EventHandler
{
	[Flags]
	public enum AnimatorMessageFlags
	{
		OnAnimatorIK = 1,
		OnAnimatorMove = 2
	}

	[Tooltip("Which of the OnAnimator messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected AnimatorMessageFlags FireOn = AnimatorMessageFlags.OnAnimatorMove;

	[Tooltip("IK layer to trigger on. Negative is all.")]
	[SerializeField]
	protected int IKLayer = 1;

	private void OnAnimatorIK(int layer)
	{
		if ((FireOn & AnimatorMessageFlags.OnAnimatorIK) != 0 && (IKLayer == layer || IKLayer < 0))
		{
			ExecuteBlock();
		}
	}

	private void OnAnimatorMove()
	{
		if ((FireOn & AnimatorMessageFlags.OnAnimatorMove) != 0)
		{
			ExecuteBlock();
		}
	}
}
