using System;
using KBEngine;

public class Pet : PetBase
{
	public override void recvDamage(int attackerID, int skillID, int damageType, int damage)
	{
		Entity entity = KBEngineApp.app.findEntity(attackerID);
		Event.fireOut("recvDamage", this, entity, skillID, damageType, damage);
	}

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
}
