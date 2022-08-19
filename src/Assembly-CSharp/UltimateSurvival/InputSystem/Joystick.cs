using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000635 RID: 1589
	public class Joystick : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler, IDragHandler
	{
		// Token: 0x06003265 RID: 12901 RVA: 0x001654D8 File Offset: 0x001636D8
		private void Start()
		{
			this.m_ParentCanvas = base.GetComponentInParent<Canvas>();
			this.m_StartPosition = base.transform.position;
			this.m_InitialMovementRange = this.m_MovementRange;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x00165504 File Offset: 0x00163704
		public void OnDrag(PointerEventData data)
		{
			this.m_MovementRange = this.m_InitialMovementRange * this.m_ParentCanvas.scaleFactor;
			this.m_CurrentPosition.x = (float)((int)(data.position.x - this.m_StartPosition.x));
			this.m_CurrentPosition.y = (float)((int)(data.position.y - this.m_StartPosition.y));
			this.m_CurrentPosition = Vector3.ClampMagnitude(this.m_CurrentPosition, this.m_MovementRange);
			base.transform.position = this.m_StartPosition + this.m_CurrentPosition;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x001655A4 File Offset: 0x001637A4
		public void OnPointerUp(PointerEventData data)
		{
			base.transform.position = this.m_StartPosition;
			this.m_CurrentPosition = Vector3.zero;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x00004095 File Offset: 0x00002295
		public void OnPointerDown(PointerEventData data)
		{
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x001655C2 File Offset: 0x001637C2
		public float GetHorizontalInput()
		{
			return this.m_CurrentPosition.x / this.m_MovementRange;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x001655D6 File Offset: 0x001637D6
		public float GetVerticalInput()
		{
			return this.m_CurrentPosition.y / this.m_MovementRange;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x001655EA File Offset: 0x001637EA
		public float GetNormalizedMagnitude()
		{
			return this.m_CurrentPosition.magnitude / this.m_MovementRange;
		}

		// Token: 0x04002CEF RID: 11503
		[SerializeField]
		[Tooltip("How far this can be moved(in pixels).")]
		private float m_MovementRange = 48f;

		// Token: 0x04002CF0 RID: 11504
		private Canvas m_ParentCanvas;

		// Token: 0x04002CF1 RID: 11505
		private Vector3 m_StartPosition;

		// Token: 0x04002CF2 RID: 11506
		private Vector3 m_CurrentPosition;

		// Token: 0x04002CF3 RID: 11507
		private float m_InitialMovementRange;
	}
}
