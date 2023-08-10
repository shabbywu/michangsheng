using BehaviorDesigner.Runtime;
using KBEngine;
using UnityEngine;
using UnityEngine.AI;

public class AvaterAddScript : BaseAddScript
{
	private void Awake()
	{
		((Component)this).gameObject.AddComponent<GameEntity>();
		((Component)this).gameObject.AddComponent<EquipWeapon>();
		((Component)this).gameObject.AddComponent<AvatarShowHpDamage>();
		((Component)this).gameObject.AddComponent<AvatarShowSkill>();
		((Component)this).gameObject.AddComponent<BehaviorTree>();
		BehaviorTree component = ((Component)this).gameObject.GetComponent<BehaviorTree>();
		((Behavior)component).StartWhenEnabled = false;
		_003F val = component;
		Object obj = Resources.Load("MonstaeAI");
		((Behavior)val).ExternalBehavior = (ExternalBehavior)(object)((obj is ExternalBehavior) ? obj : null);
		ref GameObject damageTemp = ref ((Component)this).gameObject.GetComponent<AvatarShowHpDamage>().DamageTemp;
		Object obj2 = Resources.Load("Effect/Prefab/gameEntity/Avater/Damage");
		damageTemp = (GameObject)(object)((obj2 is GameObject) ? obj2 : null);
		Object obj3 = Resources.Load("Effect/Prefab/gameEntity/Avater/ShowSkill1");
		GameObject damageTemp2 = (GameObject)(object)((obj3 is GameObject) ? obj3 : null);
		((Component)this).gameObject.GetComponent<AvatarShowSkill>().DamageTemp = damageTemp2;
	}

	public override void setBuff()
	{
		foreach (ushort buff in ((Avatar)entity).buffs)
		{
			displayBuff(buff);
		}
	}

	public void Update()
	{
		((Avatar)entity).spell.AutoRemoveBuff();
	}

	private new void Start()
	{
		base.Start();
		((MonoBehaviour)this).InvokeRepeating("avatarThink", 1f, 1f);
	}

	public void avatarThink()
	{
		((Avatar)entity).ai.think();
	}

	public void addNavMesh()
	{
		((Component)this).gameObject.AddComponent<NavMeshAgent>();
		((Component)this).gameObject.GetComponent<NavMeshAgent>().speed = 10f;
		((Component)this).gameObject.GetComponent<NavMeshAgent>().acceleration = 180f;
	}
}
