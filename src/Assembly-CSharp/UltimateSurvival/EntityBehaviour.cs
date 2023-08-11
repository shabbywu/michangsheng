using UnityEngine;

namespace UltimateSurvival;

public class EntityBehaviour : MonoBehaviour
{
	private EntityEventHandler m_Entity;

	public EntityEventHandler Entity
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_Entity))
			{
				m_Entity = ((Component)this).GetComponent<EntityEventHandler>();
			}
			if (!Object.op_Implicit((Object)(object)m_Entity))
			{
				m_Entity = ((Component)this).GetComponentInParent<EntityEventHandler>();
			}
			return m_Entity;
		}
	}
}
