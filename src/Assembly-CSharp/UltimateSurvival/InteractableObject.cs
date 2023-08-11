using UnityEngine;

namespace UltimateSurvival;

public class InteractableObject : MonoBehaviour
{
	public virtual void OnRaycastStart(PlayerEventHandler player)
	{
	}

	public virtual void OnRaycastUpdate(PlayerEventHandler player)
	{
	}

	public virtual void OnRaycastEnd(PlayerEventHandler player)
	{
	}

	public virtual void OnInteract(PlayerEventHandler player)
	{
	}

	public virtual void OnInteractHold(PlayerEventHandler player)
	{
	}
}
