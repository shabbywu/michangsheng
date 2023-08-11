using KBEngine;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAddScript : BaseAddScript
{
	private void Awake()
	{
		((Component)this).gameObject.AddComponent<CharacterHUD>();
		((Component)this).gameObject.AddComponent<GameEntity>();
	}

	private new void Start()
	{
		base.Start();
		((Component)this).gameObject.AddComponent<NavMeshAgent>();
		((Component)this).gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		((Component)this).gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
		((Component)this).gameObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = (ObstacleAvoidanceType)0;
	}

	public override void setBuff()
	{
		foreach (ushort buff in ((Monster)entity).buffs)
		{
			displayBuff(buff);
		}
	}
}
