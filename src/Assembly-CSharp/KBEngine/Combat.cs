namespace KBEngine;

public class Combat
{
	public Entity entity;

	public Combat(Entity e)
	{
		entity = e;
	}

	public void __init__()
	{
	}

	public void recvSkill(int attacker, int skillID)
	{
		Event.fireOut("recvSkill", attacker, skillID);
	}
}
