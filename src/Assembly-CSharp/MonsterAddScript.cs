using System;
using KBEngine;
using UnityEngine.AI;

// Token: 0x0200063F RID: 1599
public class MonsterAddScript : BaseAddScript
{
	// Token: 0x060027AF RID: 10159 RVA: 0x0001F5D6 File Offset: 0x0001D7D6
	private void Awake()
	{
		base.gameObject.AddComponent<CharacterHUD>();
		base.gameObject.AddComponent<GameEntity>();
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x00135538 File Offset: 0x00133738
	private new void Start()
	{
		base.Start();
		base.gameObject.AddComponent<NavMeshAgent>();
		base.gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		base.gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
		base.gameObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = 0;
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x00135594 File Offset: 0x00133794
	public override void setBuff()
	{
		foreach (ushort buffid in ((Monster)this.entity).buffs)
		{
			base.displayBuff((int)buffid);
		}
	}
}
