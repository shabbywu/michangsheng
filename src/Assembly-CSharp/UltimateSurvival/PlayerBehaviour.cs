using UnityEngine;

namespace UltimateSurvival;

public class PlayerBehaviour : EntityBehaviour
{
	private PlayerEventHandler m_Player;

	public PlayerEventHandler Player
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_Player))
			{
				m_Player = ((Component)this).GetComponent<PlayerEventHandler>();
			}
			if (!Object.op_Implicit((Object)(object)m_Player))
			{
				m_Player = ((Component)this).GetComponentInParent<PlayerEventHandler>();
			}
			return m_Player;
		}
	}
}
