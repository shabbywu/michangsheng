namespace KBEngine;

public class GameObject : if_Entity_error_use______git_submodule_update_____kbengine_plugins_______open_this_file_and_I_will_tell_you
{
	public virtual void onHPChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP");
		Event.fireOut("set_HP", this, definedProperty);
	}

	public virtual void onMPChanged(int oldValue)
	{
		getDefinedProperty("MP");
	}

	public virtual void onHP_MaxChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP_Max");
		Event.fireOut("set_HP_Max", this, definedProperty);
	}

	public virtual void onMP_MaxChanged(int oldValue)
	{
		getDefinedProperty("MP_Max");
	}

	public virtual void onLevelChanged(ushort oldValue)
	{
		object definedProperty = getDefinedProperty("level");
		Event.fireOut("set_level", this, definedProperty);
	}

	public virtual void onNameChanged(string oldValue)
	{
		object definedProperty = getDefinedProperty("name");
		Event.fireOut("set_name", this, definedProperty);
	}

	public virtual void onStateChanged(sbyte oldValue)
	{
		object definedProperty = getDefinedProperty("state");
		Event.fireOut("set_state", this, definedProperty);
	}

	public virtual void onSubStateChanged(byte oldValue)
	{
	}

	public virtual void onUtypeChanged(uint oldValue)
	{
	}

	public virtual void onUidChanged(uint oldValue)
	{
	}

	public virtual void onSpaceUTypeChanged(uint oldValue)
	{
	}

	public virtual void onMoveSpeedChanged(byte oldValue)
	{
		getDefinedProperty("moveSpeed");
	}

	public virtual void set_modelScale(object old)
	{
		getDefinedProperty("modelScale");
	}

	public virtual void set_modelID(object old)
	{
		getDefinedProperty("modelID");
	}

	public virtual void set_forbids(object old)
	{
	}

	public virtual void recvDamage(int attackerID, int skillID, int damageType, int damage)
	{
		Entity entity = KBEngineApp.app.findEntity(attackerID);
		Event.fireOut("recvDamage", this, entity, skillID, damageType, damage);
	}
}
