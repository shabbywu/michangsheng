using UnityEngine;

namespace UltimateSurvival;

public class LootOnDeath : EntityBehaviour
{
	[SerializeField]
	private LootObject m_LootContainer;

	public LootObject LootContainer
	{
		get
		{
			return m_LootContainer;
		}
		set
		{
			m_LootContainer = value;
		}
	}

	private void Start()
	{
		base.Entity.Death.AddListener(On_Death);
		((Behaviour)m_LootContainer).enabled = false;
	}

	private void On_Death()
	{
		((Behaviour)m_LootContainer).enabled = true;
	}
}
