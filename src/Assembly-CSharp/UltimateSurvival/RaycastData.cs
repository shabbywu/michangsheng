using UnityEngine;

namespace UltimateSurvival;

public class RaycastData
{
	public bool ObjectIsInteractable { get; private set; }

	public GameObject GameObject { get; private set; }

	public InteractableObject InteractableObject { get; private set; }

	public RaycastHit HitInfo { get; private set; }

	public static implicit operator bool(RaycastData raycastData)
	{
		return raycastData != null;
	}

	public RaycastData(RaycastHit hitInfo)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		GameObject = ((Component)((RaycastHit)(ref hitInfo)).collider).gameObject;
		InteractableObject = ((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<InteractableObject>();
		if (Object.op_Implicit((Object)(object)InteractableObject) && !((Behaviour)InteractableObject).enabled)
		{
			InteractableObject = null;
		}
		ObjectIsInteractable = (Object)(object)InteractableObject != (Object)null;
		HitInfo = hitInfo;
	}
}
