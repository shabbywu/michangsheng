using UnityEngine;
using UnityEngine.EventSystems;

namespace UltimateSurvival.InputSystem;

public class Joystick : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler, IDragHandler
{
	[SerializeField]
	[Tooltip("How far this can be moved(in pixels).")]
	private float m_MovementRange = 48f;

	private Canvas m_ParentCanvas;

	private Vector3 m_StartPosition;

	private Vector3 m_CurrentPosition;

	private float m_InitialMovementRange;

	private void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		m_ParentCanvas = ((Component)this).GetComponentInParent<Canvas>();
		m_StartPosition = ((Component)this).transform.position;
		m_InitialMovementRange = m_MovementRange;
	}

	public void OnDrag(PointerEventData data)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		m_MovementRange = m_InitialMovementRange * m_ParentCanvas.scaleFactor;
		m_CurrentPosition.x = (int)(data.position.x - m_StartPosition.x);
		m_CurrentPosition.y = (int)(data.position.y - m_StartPosition.y);
		m_CurrentPosition = Vector3.ClampMagnitude(m_CurrentPosition, m_MovementRange);
		((Component)this).transform.position = m_StartPosition + m_CurrentPosition;
	}

	public void OnPointerUp(PointerEventData data)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.position = m_StartPosition;
		m_CurrentPosition = Vector3.zero;
	}

	public void OnPointerDown(PointerEventData data)
	{
	}

	public float GetHorizontalInput()
	{
		return m_CurrentPosition.x / m_MovementRange;
	}

	public float GetVerticalInput()
	{
		return m_CurrentPosition.y / m_MovementRange;
	}

	public float GetNormalizedMagnitude()
	{
		return ((Vector3)(ref m_CurrentPosition)).magnitude / m_MovementRange;
	}
}
