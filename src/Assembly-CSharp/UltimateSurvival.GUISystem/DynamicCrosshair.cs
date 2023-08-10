using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class DynamicCrosshair : MonoBehaviour
{
	[SerializeField]
	[Clamp(0f, 256f)]
	private float m_Distance = 32f;

	[Header("Crosshair Parts")]
	[SerializeField]
	private Image m_Left;

	[SerializeField]
	private Image m_Right;

	[SerializeField]
	private Image m_Down;

	[SerializeField]
	private Image m_Up;

	public float Distance => m_Distance;

	public void SetActive(bool active)
	{
		Image left = m_Left;
		Image right = m_Right;
		Image down = m_Down;
		bool flag2 = (((Behaviour)m_Up).enabled = active);
		bool flag4 = (((Behaviour)down).enabled = flag2);
		bool enabled = (((Behaviour)right).enabled = flag4);
		((Behaviour)left).enabled = enabled;
	}

	public void SetDistance(float distance)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)m_Left).rectTransform.anchoredPosition = new Vector2(0f - distance, 0f);
		((Graphic)m_Right).rectTransform.anchoredPosition = new Vector2(distance, 0f);
		((Graphic)m_Down).rectTransform.anchoredPosition = new Vector2(0f, 0f - distance);
		((Graphic)m_Up).rectTransform.anchoredPosition = new Vector2(0f, distance);
		m_Distance = distance;
	}

	public void SetColor(Color color)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Image left = m_Left;
		Image right = m_Right;
		Image down = m_Down;
		Color val2 = (((Graphic)m_Up).color = color);
		Color val4 = (((Graphic)down).color = val2);
		Color color2 = (((Graphic)right).color = val4);
		((Graphic)left).color = color2;
	}

	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			SetDistance(m_Distance);
		}
	}
}
