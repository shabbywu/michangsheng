using System;
using BehaviorDesigner.Runtime;
using KBEngine;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200044E RID: 1102
public class AvaterAddScript : BaseAddScript
{
	// Token: 0x060022CF RID: 8911 RVA: 0x000EE068 File Offset: 0x000EC268
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

	// Token: 0x060022D0 RID: 8912 RVA: 0x000EE118 File Offset: 0x000EC318
	public override void setBuff()
	{
		foreach (ushort buffid in ((Avatar)this.entity).buffs)
		{
			base.displayBuff((int)buffid);
		}
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x000EE178 File Offset: 0x000EC378
	public void Update()
	{
		((Avatar)this.entity).spell.AutoRemoveBuff();
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x000EE18F File Offset: 0x000EC38F
	private new void Start()
	{
		base.Start();
		base.InvokeRepeating("avatarThink", 1f, 1f);
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x000EE1AC File Offset: 0x000EC3AC
	public void avatarThink()
	{
		((Avatar)this.entity).ai.think();
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x000EE1C3 File Offset: 0x000EC3C3
	public void addNavMesh()
	{
		base.gameObject.AddComponent<NavMeshAgent>();
		base.gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		base.gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
	}
}
