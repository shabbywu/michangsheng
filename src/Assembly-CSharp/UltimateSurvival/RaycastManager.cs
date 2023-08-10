using KBEngine;
using UnityEngine;

namespace UltimateSurvival;

public class RaycastManager : PlayerBehaviour
{
	[SerializeField]
	private Camera m_WorldCamera;

	[SerializeField]
	[Tooltip("The maximum distance at which you can interact with objects.")]
	private float m_RayLength = 1.5f;

	[SerializeField]
	[Tooltip("The distance at which an object is considered 'too close'.")]
	private float m_TooCloseThreeshold = 1f;

	[SerializeField]
	private LayerMask m_LayerMask;

	private void Start()
	{
		base.Player.InteractOnce.SetTryer(Try_InteractOnce);
		base.Player.Sleep.AddStartListener(delegate
		{
			base.Player.RaycastData.Set(null);
		});
	}

	private bool Try_InteractOnce()
	{
		RaycastData raycastData = base.Player.RaycastData.Get();
		if ((bool)raycastData && raycastData.ObjectIsInteractable)
		{
			raycastData.InteractableObject.OnInteract(base.Player);
		}
		return true;
	}

	private void Update()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		if (KBEngineApp.app.player() == null || (KBEngineApp.app != null && KBEngineApp.app.player().renderObj == null))
		{
			return;
		}
		GameObject val = (GameObject)KBEngineApp.app.player().renderObj;
		Ray val2 = new Ray(val.transform.position + new Vector3(0f, 0f, 0f), val.transform.forward);
		RaycastData raycastData = base.Player.RaycastData.Get();
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(val2, ref hitInfo, 6f) && (Object)(object)((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<GameEntity>() != (Object)null && ((Component)((RaycastHit)(ref hitInfo)).collider).GetComponent<GameEntity>().entity_name == "建筑")
		{
			RaycastData raycastData2 = new RaycastData(hitInfo);
			base.Player.RaycastData.Set(raycastData2);
			if ((bool)raycastData && raycastData2.ObjectIsInteractable && (Object)(object)raycastData2.InteractableObject != (Object)(object)raycastData.InteractableObject)
			{
				raycastData2.InteractableObject.OnRaycastStart(base.Player);
			}
			else if (raycastData2.ObjectIsInteractable)
			{
				raycastData2.InteractableObject.OnRaycastUpdate(base.Player);
			}
		}
		else
		{
			if ((bool)raycastData && raycastData.ObjectIsInteractable)
			{
				raycastData.InteractableObject.OnRaycastEnd(base.Player);
			}
			if (raycastData != null)
			{
				base.Player.RaycastData.Set(null);
			}
		}
		Debug.DrawLine(val.transform.position, ((RaycastHit)(ref hitInfo)).point, Color.red, 2f);
		int num;
		if ((bool)base.Player.RaycastData.Get())
		{
			RaycastHit hitInfo2 = base.Player.RaycastData.Get().HitInfo;
			num = ((((RaycastHit)(ref hitInfo2)).distance < m_TooCloseThreeshold) ? 1 : 0);
		}
		else
		{
			num = 0;
		}
		bool value = (byte)num != 0;
		base.Player.IsCloseToAnObject.Set(value);
	}
}
