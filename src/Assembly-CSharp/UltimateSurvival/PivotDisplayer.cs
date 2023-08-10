using UnityEngine;

namespace UltimateSurvival;

public class PivotDisplayer : MonoBehaviour
{
	[SerializeField]
	private Color m_Color = Color.red;

	[SerializeField]
	private float m_Radius = 0.06f;

	private void OnDrawGizmosSelected()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Gizmos.color = m_Color;
		Gizmos.DrawSphere(((Component)this).transform.position, m_Radius);
	}
}
