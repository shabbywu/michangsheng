using System;
using KBEngine;
using UnityEngine.AI;

// Token: 0x0200047E RID: 1150
public class MonsterAddScript : BaseAddScript
{
	// Token: 0x060023EB RID: 9195 RVA: 0x000F5699 File Offset: 0x000F3899
	private void Awake()
	{
		base.gameObject.AddComponent<CharacterHUD>();
		base.gameObject.AddComponent<GameEntity>();
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x000F56B4 File Offset: 0x000F38B4
	private new void Start()
	{
		base.Start();
		base.gameObject.AddComponent<NavMeshAgent>();
		base.gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		base.gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
		base.gameObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = 0;
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x000F5710 File Offset: 0x000F3910
	public override void setBuff()
	{
		foreach (ushort buffid in ((Monster)this.entity).buffs)
		{
			base.displayBuff((int)buffid);
		}
	}
}
