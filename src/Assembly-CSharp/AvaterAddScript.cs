using System;
using BehaviorDesigner.Runtime;
using KBEngine;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000607 RID: 1543
public class AvaterAddScript : BaseAddScript
{
	// Token: 0x06002686 RID: 9862 RVA: 0x0012E928 File Offset: 0x0012CB28
	private void Awake()
	{
		base.gameObject.AddComponent<GameEntity>();
		base.gameObject.AddComponent<EquipWeapon>();
		base.gameObject.AddComponent<AvatarShowHpDamage>();
		base.gameObject.AddComponent<AvatarShowSkill>();
		base.gameObject.AddComponent<BehaviorTree>();
		BehaviorTree component = base.gameObject.GetComponent<BehaviorTree>();
		component.StartWhenEnabled = false;
		component.ExternalBehavior = (Resources.Load("MonstaeAI") as ExternalBehavior);
		base.gameObject.GetComponent<AvatarShowHpDamage>().DamageTemp = (Resources.Load("Effect/Prefab/gameEntity/Avater/Damage") as GameObject);
		GameObject damageTemp = Resources.Load("Effect/Prefab/gameEntity/Avater/ShowSkill1") as GameObject;
		base.gameObject.GetComponent<AvatarShowSkill>().DamageTemp = damageTemp;
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x0012E9D8 File Offset: 0x0012CBD8
	public override void setBuff()
	{
		foreach (ushort buffid in ((Avatar)this.entity).buffs)
		{
			base.displayBuff((int)buffid);
		}
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x0001EBBE File Offset: 0x0001CDBE
	public void Update()
	{
		((Avatar)this.entity).spell.AutoRemoveBuff();
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x0001EBD5 File Offset: 0x0001CDD5
	private new void Start()
	{
		base.Start();
		base.InvokeRepeating("avatarThink", 1f, 1f);
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x0001EBF2 File Offset: 0x0001CDF2
	public void avatarThink()
	{
		((Avatar)this.entity).ai.think();
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x0001EC09 File Offset: 0x0001CE09
	public void addNavMesh()
	{
		base.gameObject.AddComponent<NavMeshAgent>();
		base.gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		base.gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
	}
}
