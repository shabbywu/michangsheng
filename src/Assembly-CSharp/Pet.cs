using System;
using KBEngine;

// Token: 0x02000551 RID: 1361
public class Pet : PetBase
{
	// Token: 0x060022D0 RID: 8912 RVA: 0x0011EE44 File Offset: 0x0011D044
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

	// Token: 0x060022D1 RID: 8913 RVA: 0x0001C722 File Offset: 0x0001A922
	public override void recvSkill(int arg1, int arg2)
	{
		throw new NotImplementedException();
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x0011EE94 File Offset: 0x0011D094
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
