using System;
using KBEngine;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005FD RID: 1533
	public class RaycastManager : PlayerBehaviour
	{
		// Token: 0x0600313A RID: 12602 RVA: 0x0015E40D File Offset: 0x0015C60D
		private void Start()
		{
			base.Player.InteractOnce.SetTryer(new TryerDelegate(this.Try_InteractOnce));
			base.Player.Sleep.AddStartListener(delegate
			{
				base.Player.RaycastData.Set(null);
			});
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0015E448 File Offset: 0x0015C648
		private bool Try_InteractOnce()
		{
			RaycastData raycastData = base.Player.RaycastData.Get();
			if (raycastData && raycastData.ObjectIsInteractable)
			{
				raycastData.InteractableObject.OnInteract(base.Player);
			}
			return true;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0015E488 File Offset: 0x0015C688
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

		// Token: 0x04002B59 RID: 11097
		[SerializeField]
		private Camera m_WorldCamera;

		// Token: 0x04002B5A RID: 11098
		[SerializeField]
		[Tooltip("The maximum distance at which you can interact with objects.")]
		private float m_RayLength = 1.5f;

		// Token: 0x04002B5B RID: 11099
		[SerializeField]
		[Tooltip("The distance at which an object is considered 'too close'.")]
		private float m_TooCloseThreeshold = 1f;

		// Token: 0x04002B5C RID: 11100
		[SerializeField]
		private LayerMask m_LayerMask;
	}
}
