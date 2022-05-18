using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02001038 RID: 4152
	public class Monster : MonsterBase
	{
		// Token: 0x0600635B RID: 25435 RVA: 0x0001C722 File Offset: 0x0001A922
		public override void recvSkill(int arg1, int arg2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0027B658 File Offset: 0x00279858
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

		// Token: 0x0600635D RID: 25437 RVA: 0x00272248 File Offset: 0x00270448
		public override void onHPChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP");
			Event.fireOut("set_HP", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x00043EC5 File Offset: 0x000420C5
		public override void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0027227C File Offset: 0x0027047C
		public override void on_HP_MaxChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP_Max");
			Event.fireOut("set_HP_Max", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x00043ED3 File Offset: 0x000420D3
		public override void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x002722B0 File Offset: 0x002704B0
		public override void onNameChanged(string oldValue)
		{
			object definedProperty = this.getDefinedProperty("name");
			Event.fireOut("set_name", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x002722E4 File Offset: 0x002704E4
		public override void onStateChanged(sbyte oldValue)
		{
			object definedProperty = this.getDefinedProperty("state");
			Event.fireOut("set_state", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x00043EE1 File Offset: 0x000420E1
		public override void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0011EE44 File Offset: 0x0011D044
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

		// Token: 0x06006368 RID: 25448 RVA: 0x000042DD File Offset: 0x000024DD
		public override void __init__()
		{
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x000448F4 File Offset: 0x00042AF4
		public override void onBuffsChanged(List<ushort> oldValue)
		{
			if (this.renderObj != null)
			{
				Event.fireOut("set_Buffs", new object[]
				{
					this,
					oldValue,
					this.buffs
				});
			}
		}
	}
}
