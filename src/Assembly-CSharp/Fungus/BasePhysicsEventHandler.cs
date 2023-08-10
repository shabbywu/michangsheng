using System;
using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public abstract class BasePhysicsEventHandler : TagFilteredEventHandler
{
	[Flags]
	public enum PhysicsMessageType
	{
		Enter = 1,
		Stay = 2,
		Exit = 4
	}

	[Tooltip("Which of the 3d physics messages to we trigger on.")]
	[SerializeField]
	[EnumFlag]
	protected PhysicsMessageType FireOn = PhysicsMessageType.Enter;

	protected void ProcessCollider(PhysicsMessageType from, string tagOnOther)
	{
		if ((from & FireOn) != 0)
		{
			ProcessTagFilter(tagOnOther);
		}
	}
}
