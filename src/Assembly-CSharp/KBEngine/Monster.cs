using System;
using System.Collections.Generic;

namespace KBEngine;

public class Monster : MonsterBase
{
	public override void recvSkill(int arg1, int arg2)
	{
		throw new NotImplementedException();
	}

	public override object getDefinedProperty(string name)
	{
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		return name switch
		{
			"roleTypeCell" => roleTypeCell, 
			"roleSurfaceCall" => roleSurfaceCall, 
			"position" => position, 
			"HP_Max" => _HP_Max, 
			"HP" => HP, 
			"state" => state, 
			"name" => base.name, 
			"MP" => MP, 
			"MP_Max" => MP_Max, 
			_ => null, 
		};
	}

	public override void onHPChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP");
		Event.fireOut("set_HP", this, definedProperty);
	}

	public override void onMPChanged(int oldValue)
	{
		getDefinedProperty("MP");
	}

	public override void on_HP_MaxChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP_Max");
		Event.fireOut("set_HP_Max", this, definedProperty);
	}

	public override void onMP_MaxChanged(int oldValue)
	{
		getDefinedProperty("MP_Max");
	}

	public override void onNameChanged(string oldValue)
	{
		object definedProperty = getDefinedProperty("name");
		Event.fireOut("set_name", this, definedProperty);
	}

	public override void onStateChanged(sbyte oldValue)
	{
		object definedProperty = getDefinedProperty("state");
		Event.fireOut("set_state", this, definedProperty);
	}

	public override void onSubStateChanged(byte oldValue)
	{
	}

	public override void onUtypeChanged(uint oldValue)
	{
	}

	public override void onUidChanged(uint oldValue)
	{
	}

	public override void onMoveSpeedChanged(byte oldValue)
	{
		getDefinedProperty("moveSpeed");
	}

	public override void recvDamage(int attackerID, int skillID, int damageType, int damage)
	{
		Entity entity = KBEngineApp.app.findEntity(attackerID);
		Event.fireOut("recvDamage", this, entity, skillID, damageType, damage);
	}

	public override void __init__()
	{
	}

	public override void onBuffsChanged(List<ushort> oldValue)
	{
		if (renderObj != null)
		{
			Event.fireOut("set_Buffs", this, oldValue, buffs);
		}
	}
}
