using System;
using KBEngine;

// Token: 0x020003BE RID: 958
public class Pet : PetBase
{
	// Token: 0x06001F44 RID: 8004 RVA: 0x000DBF5C File Offset: 0x000DA15C
	public override void recvDamage(int attackerID, int skillID, int damageType, int damage)
	{
		Entity entity = KBEngineApp.app.findEntity(attackerID);
		Event.fireOut("recvDamage", new object[]
		{
			this,
			entity,
			skillID,
			damageType,
			damage
		});
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
	public override void recvSkill(int arg1, int arg2)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x000DBFB0 File Offset: 0x000DA1B0
	public override object getDefinedProperty(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1437283280U)
		{
			if (num <= 611129337U)
			{
				if (num != 339128649U)
				{
					if (num == 611129337U)
					{
						if (name == "MP_Max")
						{
							return this.MP_Max;
						}
					}
				}
				else if (name == "roleTypeCell")
				{
					return this.roleTypeCell;
				}
			}
			else if (num != 853522520U)
			{
				if (num == 1437283280U)
				{
					if (name == "HP_Max")
					{
						return this._HP_Max;
					}
				}
			}
			else if (name == "MP")
			{
				return this.MP;
			}
		}
		else if (num <= 1894470373U)
		{
			if (num != 1802331040U)
			{
				if (num == 1894470373U)
				{
					if (name == "HP")
					{
						return this.HP;
					}
				}
			}
			else if (name == "roleSurfaceCall")
			{
				return this.roleSurfaceCall;
			}
		}
		else if (num != 2016490230U)
		{
			if (num != 2369371622U)
			{
				if (num == 2471448074U)
				{
					if (name == "position")
					{
						return this.position;
					}
				}
			}
			else if (name == "name")
			{
				return this.name;
			}
		}
		else if (name == "state")
		{
			return this.state;
		}
		return null;
	}
}
