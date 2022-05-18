using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008D7 RID: 2263
	public class RaycastManager : PlayerBehaviour
	{
		// Token: 0x06003A37 RID: 14903 RVA: 0x0002A44A File Offset: 0x0002864A
		private void Start()
		{
			base.Player.InteractOnce.SetTryer(new TryerDelegate(this.Try_InteractOnce));
			base.Player.Sleep.AddStartListener(delegate
			{
				base.Player.RaycastData.Set(null);
			});
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x001A7964 File Offset: 0x001A5B64
		private bool Try_InteractOnce()
		{
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (raycastData && raycastData.ObjectIsInteractable)
			{
				raycastData.InteractableObject.OnInteract(base.Player);
			}
			return true;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x001A79A4 File Offset: 0x001A5BA4
		private void Update()
		{
			if (KBEngineApp.app.player() == null || (KBEngineApp.app != null && KBEngineApp.app.player().renderObj == null))
			{
				return;
			}
			GameObject gameObject = (GameObject)KBEngineApp.app.player().renderObj;
			Ray ray = new Ray(gameObject.transform.position + new Vector3(0f, 0f, 0f), gameObject.transform.forward);
			RaycastData raycastData = base.Player.RaycastData.Get();
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, ref hitInfo, 6f) && hitInfo.collider.GetComponent<GameEntity>() != null && hitInfo.collider.GetComponent<GameEntity>().entity_name == "建筑")
			{
				RaycastData raycastData2 = new RaycastData(hitInfo);
				base.Player.RaycastData.Set(raycastData2);
				if (raycastData && raycastData2.ObjectIsInteractable && raycastData2.InteractableObject != raycastData.InteractableObject)
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
				if (raycastData && raycastData.ObjectIsInteractable)
				{
					raycastData.InteractableObject.OnRaycastEnd(base.Player);
				}
				if (raycastData != null)
				{
					base.Player.RaycastData.Set(null);
				}
			}
			Debug.DrawLine(gameObject.transform.position, hitInfo.point, Color.red, 2f);
			bool value = base.Player.RaycastData.Get() && base.Player.RaycastData.Get().HitInfo.distance < this.m_TooCloseThreeshold;
			base.Player.IsCloseToAnObject.Set(value);
		}

		// Token: 0x04003446 RID: 13382
		[SerializeField]
		private Camera m_WorldCamera;

		// Token: 0x04003447 RID: 13383
		[SerializeField]
		[Tooltip("The maximum distance at which you can interact with objects.")]
		private float m_RayLength = 1.5f;

		// Token: 0x04003448 RID: 13384
		[SerializeField]
		[Tooltip("The distance at which an object is considered 'too close'.")]
		private float m_TooCloseThreeshold = 1f;

		// Token: 0x04003449 RID: 13385
		[SerializeField]
		private LayerMask m_LayerMask;
	}
}
