using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C80 RID: 3200
	public class Monster : MonsterBase
	{
		// Token: 0x060058B2 RID: 22706 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public override void recvSkill(int arg1, int arg2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0024FA84 File Offset: 0x0024DC84
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

		// Token: 0x060058B4 RID: 22708 RVA: 0x0024FC28 File Offset: 0x0024DE28
		public override void onHPChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP");
			Event.fireOut("set_HP", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x00246801 File Offset: 0x00244A01
		public override void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0024FC5C File Offset: 0x0024DE5C
		public override void on_HP_MaxChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP_Max");
			Event.fireOut("set_HP_Max", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00248FB1 File Offset: 0x002471B1
		public override void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x0024FC90 File Offset: 0x0024DE90
		public override void onNameChanged(string oldValue)
		{
			object definedProperty = this.getDefinedProperty("name");
			Event.fireOut("set_name", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x0024FCC4 File Offset: 0x0024DEC4
		public override void onStateChanged(sbyte oldValue)
		{
			object definedProperty = this.getDefinedProperty("state");
			Event.fireOut("set_state", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x00004095 File Offset: 0x00002295
		public override void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x00004095 File Offset: 0x00002295
		public override void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x00004095 File Offset: 0x00002295
		public override void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x00249059 File Offset: 0x00247259
		public override void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0024FCF8 File Offset: 0x0024DEF8
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

		// Token: 0x060058BF RID: 22719 RVA: 0x00004095 File Offset: 0x00002295
		public override void __init__()
		{
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x0024FD45 File Offset: 0x0024DF45
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
