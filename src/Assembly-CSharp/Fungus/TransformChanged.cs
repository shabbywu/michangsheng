using System;
using UnityEngine;

namespace Fungus;

[EventHandlerInfo("MonoBehaviour", "Transform", "The block will execute when the desired OnTransform related message for the monobehaviour is received.")]
[AddComponentMenu("")]
public class TransformChanged : EventHandler
{
	[Flags]
	public enum TransformMessageFlags
	{
		OnTransformChildrenChanged = 1,
		OnTransformParentChanged = 2
	}

	[Tooltip("Which of the OnTransformChanged messages to trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected TransformMessageFlags FireOn = TransformMessageFlags.OnTransformChildrenChanged | TransformMessageFlags.OnTransformParentChanged;

	private void OnTransformChildrenChanged()
	{
		if ((FireOn & TransformMessageFlags.OnTransformChildrenChanged) != 0)
		{
			ExecuteBlock();
		}
	}

	private void OnTransformParentChanged()
	{
		if ((FireOn & TransformMessageFlags.OnTransformParentChanged) != 0)
		{
			ExecuteBlock();
		}
	}
}
