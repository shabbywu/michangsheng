using UnityEngine;

namespace UltimateSurvival;

public class AIBehaviour : EntityBehaviour
{
	private AIEventHandler m_AI;

	public AIEventHandler AI
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_AI))
			{
				m_AI = ((Component)this).GetComponentInParent<AIEventHandler>();
			}
			return m_AI;
		}
	}
}
