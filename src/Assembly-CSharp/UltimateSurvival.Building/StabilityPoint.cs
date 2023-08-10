using System;
using UnityEngine;

namespace UltimateSurvival.Building;

[Serializable]
public class StabilityPoint
{
	[SerializeField]
	private string m_Name;

	[SerializeField]
	private Vector3 m_Position;

	[SerializeField]
	private Vector3 m_Direction = Vector3.down;

	[SerializeField]
	[Clamp(0f, 10f)]
	private float m_Distance = 0.2f;

	public StabilityPoint GetClone()
	{
		return (StabilityPoint)MemberwiseClone();
	}

	public bool IsStable(BuildingPiece piece, LayerMask mask)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit[] array = Physics.RaycastAll(((Component)piece).transform.position + ((Component)piece).transform.TransformVector(m_Position), ((Component)piece).transform.TransformDirection(m_Direction), m_Distance, LayerMask.op_Implicit(mask), (QueryTriggerInteraction)1);
		for (int i = 0; i < array.Length; i++)
		{
			if (!piece.HasCollider(((RaycastHit)(ref array[i])).collider))
			{
				return true;
			}
		}
		return false;
	}

	public void OnDrawGizmosSelected(BuildingPiece piece)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Gizmos.color = Color.red;
		Vector3 val = ((Component)piece).transform.position + ((Component)piece).transform.TransformVector(m_Position);
		Vector3 val2 = ((Component)piece).transform.TransformDirection(m_Direction);
		Gizmos.DrawRay(val, ((Vector3)(ref val2)).normalized * m_Distance);
	}
}
