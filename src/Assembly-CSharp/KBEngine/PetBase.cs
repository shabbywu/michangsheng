using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C5C RID: 3164
	public abstract class PetBase : Entity
	{
		// Token: 0x060055F0 RID: 22000 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onHPChanged(int oldValue)
		{
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMPChanged(int oldValue)
		{
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void on_HP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAttack_MaxChanged(int oldValue)
		{
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAttack_MinChanged(int oldValue)
		{
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onBuffsChanged(List<ushort> oldValue)
		{
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDefenceChanged(int oldValue)
		{
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDodgeChanged(int oldValue)
		{
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onForbidsChanged(int oldValue)
		{
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRatingChanged(int oldValue)
		{
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleSurfaceCallChanged(ushort oldValue)
		{
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleTypeCellChanged(uint oldValue)
		{
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onStateChanged(sbyte oldValue)
		{
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005607 RID: 22023
		public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

		// Token: 0x06005608 RID: 22024
		public abstract void recvSkill(int arg1, int arg2);

		// Token: 0x06005609 RID: 22025 RVA: 0x0023AD40 File Offset: 0x00238F40
		public PetBase()
		{
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x0023AD90 File Offset: 0x00238F90
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_PetBase(this.id, this.className);
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0023ADA9 File Offset: 0x00238FA9
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_PetBase(this.id, this.className);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0023ADC2 File Offset: 0x00238FC2
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0023ADCB File Offset: 0x00238FCB
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x0023ADD3 File Offset: 0x00238FD3
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0023ADDC File Offset: 0x00238FDC
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Pet"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num != 0)
			{
				ushort properUtype = scriptModule.idpropertys[num].properUtype;
				return;
			}
			Method method = scriptModule.idmethods[key];
			ushort methodUtype = method.methodUtype;
			if (methodUtype == 161)
			{
				int arg = stream.readInt32();
				int arg2 = stream.readInt32();
				int arg3 = stream.readInt32();
				int arg4 = stream.readInt32();
				this.recvDamage(arg, arg2, arg3, arg4);
				return;
			}
			if (methodUtype != 162)
			{
				return;
			}
			int arg5 = stream.readInt32();
			int arg6 = stream.readInt32();
			this.recvSkill(arg5, arg6);
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x0023AEB8 File Offset: 0x002390B8
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Pet"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 40002)
				{
					switch (properUtype2)
					{
					case 103:
					{
						uint oldValue = this.roleTypeCell;
						this.roleTypeCell = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleTypeCellChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleTypeCellChanged(oldValue);
						}
						break;
					}
					case 104:
					{
						ushort oldValue2 = this.roleSurfaceCall;
						this.roleSurfaceCall = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleSurfaceCallChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleSurfaceCallChanged(oldValue2);
						}
						break;
					}
					case 105:
					case 106:
					case 107:
					case 108:
					case 109:
					case 112:
					case 113:
					case 114:
						break;
					case 110:
					{
						uint oldValue3 = this.dialogID;
						this.dialogID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDialogIDChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onDialogIDChanged(oldValue3);
						}
						break;
					}
					case 111:
					{
						byte oldValue4 = this.moveSpeed;
						this.moveSpeed = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onMoveSpeedChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onMoveSpeedChanged(oldValue4);
						}
						break;
					}
					case 115:
					{
						int oldValue5 = this.attack_Max;
						this.attack_Max = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAttack_MaxChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onAttack_MaxChanged(oldValue5);
						}
						break;
					}
					case 116:
					{
						int oldValue6 = this.attack_Min;
						this.attack_Min = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAttack_MinChanged(oldValue6);
							}
						}
						else if (this.inWorld)
						{
							this.onAttack_MinChanged(oldValue6);
						}
						break;
					}
					case 117:
					{
						int oldValue7 = this.defence;
						this.defence = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDefenceChanged(oldValue7);
							}
						}
						else if (this.inWorld)
						{
							this.onDefenceChanged(oldValue7);
						}
						break;
					}
					case 118:
					{
						int oldValue8 = this.rating;
						this.rating = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRatingChanged(oldValue8);
							}
						}
						else if (this.inWorld)
						{
							this.onRatingChanged(oldValue8);
						}
						break;
					}
					case 119:
					{
						int oldValue9 = this.dodge;
						this.dodge = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDodgeChanged(oldValue9);
							}
						}
						else if (this.inWorld)
						{
							this.onDodgeChanged(oldValue9);
						}
						break;
					}
					case 120:
					{
						List<ushort> oldValue10 = this.buffs;
						this.buffs = ((DATATYPE_AnonymousArray_50)EntityDef.id2datatypes[50]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onBuffsChanged(oldValue10);
							}
						}
						else if (this.inWorld)
						{
							this.onBuffsChanged(oldValue10);
						}
						break;
					}
					default:
						switch (properUtype2)
						{
						case 40000:
						{
							Vector3 position = this.position;
							this.position = stream.readVector3();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onPositionChanged(position);
								}
							}
							else if (this.inWorld)
							{
								this.onPositionChanged(position);
							}
							break;
						}
						case 40001:
						{
							Vector3 direction = this.direction;
							this.direction = stream.readVector3();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onDirectionChanged(direction);
								}
							}
							else if (this.inWorld)
							{
								this.onDirectionChanged(direction);
							}
							break;
						}
						case 40002:
							stream.readUint32();
							break;
						}
						break;
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue11 = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue11);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue11);
						}
						break;
					}
					case 41004:
					{
						uint oldValue12 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue12);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue12);
						}
						break;
					}
					case 41005:
					{
						uint oldValue13 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue13);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue13);
						}
						break;
					}
					case 41006:
					{
						uint oldValue14 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue14);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue14);
						}
						break;
					}
					case 41007:
					{
						byte oldValue15 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue15);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue15);
						}
						break;
					}
					default:
						switch (properUtype2)
						{
						case 47001:
						{
							int hp = this.HP;
							this.HP = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onHPChanged(hp);
								}
							}
							else if (this.inWorld)
							{
								this.onHPChanged(hp);
							}
							break;
						}
						case 47002:
						{
							int hp_Max = this._HP_Max;
							this._HP_Max = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.on_HP_MaxChanged(hp_Max);
								}
							}
							else if (this.inWorld)
							{
								this.on_HP_MaxChanged(hp_Max);
							}
							break;
						}
						case 47003:
						{
							int mp = this.MP;
							this.MP = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onMPChanged(mp);
								}
							}
							else if (this.inWorld)
							{
								this.onMPChanged(mp);
							}
							break;
						}
						case 47004:
						{
							int mp_Max = this.MP_Max;
							this.MP_Max = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onMP_MaxChanged(mp_Max);
								}
							}
							else if (this.inWorld)
							{
								this.onMP_MaxChanged(mp_Max);
							}
							break;
						}
						case 47005:
						{
							int oldValue16 = this.forbids;
							this.forbids = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onForbidsChanged(oldValue16);
								}
							}
							else if (this.inWorld)
							{
								this.onForbidsChanged(oldValue16);
							}
							break;
						}
						case 47006:
						{
							sbyte oldValue17 = this.state;
							this.state = stream.readInt8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onStateChanged(oldValue17);
								}
							}
							else if (this.inWorld)
							{
								this.onStateChanged(oldValue17);
							}
							break;
						}
						case 47007:
						{
							byte oldValue18 = this.subState;
							this.subState = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onSubStateChanged(oldValue18);
								}
							}
							else if (this.inWorld)
							{
								this.onSubStateChanged(oldValue18);
							}
							break;
						}
						default:
							if (properUtype2 == 51007)
							{
								uint oldValue19 = this.entityNO;
								this.entityNO = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onEntityNOChanged(oldValue19);
									}
								}
								else if (this.inWorld)
								{
									this.onEntityNOChanged(oldValue19);
								}
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x0023B79C File Offset: 0x0023999C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Pet"].idpropertys;
			int hp = this.HP;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onHPChanged(hp);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onHPChanged(hp);
			}
			int mp = this.MP;
			Property property2 = idpropertys[5];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMPChanged(mp);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onMPChanged(mp);
			}
			int mp_Max = this.MP_Max;
			Property property3 = idpropertys[6];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMP_MaxChanged(mp_Max);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onMP_MaxChanged(mp_Max);
			}
			int hp_Max = this._HP_Max;
			Property property4 = idpropertys[7];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.on_HP_MaxChanged(hp_Max);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.on_HP_MaxChanged(hp_Max);
			}
			int oldValue = this.attack_Max;
			Property property5 = idpropertys[8];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAttack_MaxChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onAttack_MaxChanged(oldValue);
			}
			int oldValue2 = this.attack_Min;
			Property property6 = idpropertys[9];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAttack_MinChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onAttack_MinChanged(oldValue2);
			}
			List<ushort> oldValue3 = this.buffs;
			Property property7 = idpropertys[10];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onBuffsChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onBuffsChanged(oldValue3);
			}
			int oldValue4 = this.defence;
			Property property8 = idpropertys[11];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDefenceChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onDefenceChanged(oldValue4);
			}
			uint oldValue5 = this.dialogID;
			Property property9 = idpropertys[12];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue5);
			}
			Vector3 direction = this.direction;
			Property property10 = idpropertys[2];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			int oldValue6 = this.dodge;
			Property property11 = idpropertys[13];
			if (property11.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDodgeChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property11.isOwnerOnly() || base.isPlayer()))
			{
				this.onDodgeChanged(oldValue6);
			}
			uint oldValue7 = this.entityNO;
			Property property12 = idpropertys[14];
			if (property12.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue7);
				}
			}
			else if (this.inWorld && (!property12.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue7);
			}
			int oldValue8 = this.forbids;
			Property property13 = idpropertys[15];
			if (property13.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onForbidsChanged(oldValue8);
				}
			}
			else if (this.inWorld && (!property13.isOwnerOnly() || base.isPlayer()))
			{
				this.onForbidsChanged(oldValue8);
			}
			uint oldValue9 = this.modelID;
			Property property14 = idpropertys[16];
			if (property14.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue9);
				}
			}
			else if (this.inWorld && (!property14.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue9);
			}
			byte oldValue10 = this.modelScale;
			Property property15 = idpropertys[17];
			if (property15.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue10);
				}
			}
			else if (this.inWorld && (!property15.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue10);
			}
			byte oldValue11 = this.moveSpeed;
			Property property16 = idpropertys[18];
			if (property16.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMoveSpeedChanged(oldValue11);
				}
			}
			else if (this.inWorld && (!property16.isOwnerOnly() || base.isPlayer()))
			{
				this.onMoveSpeedChanged(oldValue11);
			}
			string oldValue12 = this.name;
			Property property17 = idpropertys[19];
			if (property17.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue12);
				}
			}
			else if (this.inWorld && (!property17.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue12);
			}
			Vector3 position = this.position;
			Property property18 = idpropertys[1];
			if (property18.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property18.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			int oldValue13 = this.rating;
			Property property19 = idpropertys[20];
			if (property19.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRatingChanged(oldValue13);
				}
			}
			else if (this.inWorld && (!property19.isOwnerOnly() || base.isPlayer()))
			{
				this.onRatingChanged(oldValue13);
			}
			ushort oldValue14 = this.roleSurfaceCall;
			Property property20 = idpropertys[21];
			if (property20.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleSurfaceCallChanged(oldValue14);
				}
			}
			else if (this.inWorld && (!property20.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleSurfaceCallChanged(oldValue14);
			}
			uint oldValue15 = this.roleTypeCell;
			Property property21 = idpropertys[22];
			if (property21.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleTypeCellChanged(oldValue15);
				}
			}
			else if (this.inWorld && (!property21.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleTypeCellChanged(oldValue15);
			}
			sbyte oldValue16 = this.state;
			Property property22 = idpropertys[23];
			if (property22.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onStateChanged(oldValue16);
				}
			}
			else if (this.inWorld && (!property22.isOwnerOnly() || base.isPlayer()))
			{
				this.onStateChanged(oldValue16);
			}
			byte oldValue17 = this.subState;
			Property property23 = idpropertys[24];
			if (property23.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onSubStateChanged(oldValue17);
				}
			}
			else if (this.inWorld && (!property23.isOwnerOnly() || base.isPlayer()))
			{
				this.onSubStateChanged(oldValue17);
			}
			uint oldValue18 = this.uid;
			Property property24 = idpropertys[25];
			if (property24.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue18);
				}
			}
			else if (this.inWorld && (!property24.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue18);
			}
			uint oldValue19 = this.utype;
			Property property25 = idpropertys[26];
			if (property25.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue19);
					return;
				}
			}
			else if (this.inWorld && (!property25.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue19);
			}
		}

		// Token: 0x040050D8 RID: 20696
		public EntityBaseEntityCall_PetBase baseEntityCall;

		// Token: 0x040050D9 RID: 20697
		public EntityCellEntityCall_PetBase cellEntityCall;

		// Token: 0x040050DA RID: 20698
		public int HP;

		// Token: 0x040050DB RID: 20699
		public int MP;

		// Token: 0x040050DC RID: 20700
		public int MP_Max;

		// Token: 0x040050DD RID: 20701
		public int _HP_Max;

		// Token: 0x040050DE RID: 20702
		public int attack_Max = 10;

		// Token: 0x040050DF RID: 20703
		public int attack_Min;

		// Token: 0x040050E0 RID: 20704
		public List<ushort> buffs = new List<ushort>();

		// Token: 0x040050E1 RID: 20705
		public int defence;

		// Token: 0x040050E2 RID: 20706
		public uint dialogID;

		// Token: 0x040050E3 RID: 20707
		public int dodge;

		// Token: 0x040050E4 RID: 20708
		public uint entityNO;

		// Token: 0x040050E5 RID: 20709
		public int forbids;

		// Token: 0x040050E6 RID: 20710
		public uint modelID;

		// Token: 0x040050E7 RID: 20711
		public byte modelScale = 30;

		// Token: 0x040050E8 RID: 20712
		public byte moveSpeed = 50;

		// Token: 0x040050E9 RID: 20713
		public string name = "";

		// Token: 0x040050EA RID: 20714
		public int rating = 99;

		// Token: 0x040050EB RID: 20715
		public ushort roleSurfaceCall = 1;

		// Token: 0x040050EC RID: 20716
		public uint roleTypeCell;

		// Token: 0x040050ED RID: 20717
		public sbyte state;

		// Token: 0x040050EE RID: 20718
		public byte subState;

		// Token: 0x040050EF RID: 20719
		public uint uid;

		// Token: 0x040050F0 RID: 20720
		public uint utype;
	}
}
