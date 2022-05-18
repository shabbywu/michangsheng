using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000927 RID: 2343
	public class Joystick : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler, IDragHandler
	{
		// Token: 0x06003B9F RID: 15263 RVA: 0x0002B1C1 File Offset: 0x000293C1
		private void Start()
		{
			this.m_ParentCanvas = base.GetComponentInParent<Canvas>();
			this.m_StartPosition = base.transform.position;
			this.m_InitialMovementRange = this.m_MovementRange;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x001AEAFC File Offset: 0x001ACCFC
		public void OnDrag(PointerEventData data)
		{
			this.m_MovementRange = this.m_InitialMovementRange * this.m_ParentCanvas.scaleFactor;
			this.m_CurrentPosition.x = (float)((int)(data.position.x - this.m_StartPosition.x));
			this.m_CurrentPosition.y = (float)((int)(data.position.y - this.m_StartPosition.y));
			this.m_CurrentPosition = Vector3.ClampMagnitude(this.m_CurrentPosition, this.m_MovementRange);
			base.transform.position = this.m_StartPosition + this.m_CurrentPosition;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x0002B1EC File Offset: 0x000293EC
		public void OnPointerUp(PointerEventData data)
		{
			base.transform.position = this.m_StartPosition;
			this.m_CurrentPosition = Vector3.zero;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000042DD File Offset: 0x000024DD
		public void OnPointerDown(PointerEventData data)
		{
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0002B20A File Offset: 0x0002940A
		public float GetHorizontalInput()
		{
			return this.m_CurrentPosition.x / this.m_MovementRange;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x0002B21E File Offset: 0x0002941E
		public float GetVerticalInput()
		{
			return this.m_CurrentPosition.y / this.m_MovementRange;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x0002B232 File Offset: 0x00029432
		public float GetNormalizedMagnitude()
		{
			return this.m_CurrentPosition.magnitude / this.m_MovementRange;
		}

		// Token: 0x04003640 RID: 13888
		[SerializeField]
		[Tooltip("How far this can be moved(in pixels).")]
		private float m_MovementRange = 48f;

		// Token: 0x04003641 RID: 13889
		private Canvas m_ParentCanvas;

		// Token: 0x04003642 RID: 13890
		private Vector3 m_StartPosition;

		// Token: 0x04003643 RID: 13891
		private Vector3 m_CurrentPosition;

		// Token: 0x04003644 RID: 13892
		private float m_InitialMovementRange;
	}
}
