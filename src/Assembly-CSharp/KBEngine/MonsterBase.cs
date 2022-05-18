using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FD4 RID: 4052
	public abstract class MonsterBase : Entity
	{
		// Token: 0x06005F96 RID: 24470 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onHPChanged(int oldValue)
		{
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMPChanged(int oldValue)
		{
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void on_HP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MinChanged(int oldValue)
		{
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onBuffsChanged(List<ushort> oldValue)
		{
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDefenceChanged(int oldValue)
		{
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDodgeChanged(int oldValue)
		{
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onForbidsChanged(int oldValue)
		{
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRatingChanged(int oldValue)
		{
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleSurfaceCallChanged(ushort oldValue)
		{
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleTypeCellChanged(uint oldValue)
		{
		}

		// Token: 0x06005FA9 RID: 24489 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStateChanged(sbyte oldValue)
		{
		}

		// Token: 0x06005FAA RID: 24490 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005FAD RID: 24493
		public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

		// Token: 0x06005FAE RID: 24494
		public abstract void recvSkill(int arg1, int arg2);

		// Token: 0x06005FAF RID: 24495 RVA: 0x00264C44 File Offset: 0x00262E44
		public MonsterBase()
		{
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00042A10 File Offset: 0x00040C10
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_MonsterBase(this.id, this.className);
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x00042A29 File Offset: 0x00040C29
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_MonsterBase(this.id, this.className);
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x00042A42 File Offset: 0x00040C42
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x00042A4B File Offset: 0x00040C4B
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00042A53 File Offset: 0x00040C53
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x00264C94 File Offset: 0x00262E94
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Monster"];
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
			if (methodUtype == 143)
			{
				int arg = stream.readInt32();
				int arg2 = stream.readInt32();
				int arg3 = stream.readInt32();
				int arg4 = stream.readInt32();
				this.recvDamage(arg, arg2, arg3, arg4);
				return;
			}
			if (methodUtype != 144)
			{
				return;
			}
			int arg5 = stream.readInt32();
			int arg6 = stream.readInt32();
			this.recvSkill(arg5, arg6);
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x00264D70 File Offset: 0x00262F70
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Monster"];
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
					case 55:
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
					case 56:
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
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 64:
					case 65:
					case 66:
						break;
					case 62:
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
					case 63:
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
					case 67:
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
					case 68:
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
					case 69:
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
					case 70:
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
					case 71:
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
					case 72:
					{
						List<ushort> oldValue10 = this.buffs;
						this.buffs = ((DATATYPE_AnonymousArray_49)EntityDef.id2datatypes[49]).createFromStreamEx(stream);
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

		// Token: 0x06005FBB RID: 24507 RVA: 0x00265654 File Offset: 0x00263854
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Monster"].idpropertys;
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

		// Token: 0x04005B3D RID: 23357
		public EntityBaseEntityCall_MonsterBase baseEntityCall;

		// Token: 0x04005B3E RID: 23358
		public EntityCellEntityCall_MonsterBase cellEntityCall;

		// Token: 0x04005B3F RID: 23359
		public int HP;

		// Token: 0x04005B40 RID: 23360
		public int MP;

		// Token: 0x04005B41 RID: 23361
		public int MP_Max;

		// Token: 0x04005B42 RID: 23362
		public int _HP_Max;

		// Token: 0x04005B43 RID: 23363
		public int attack_Max = 10;

		// Token: 0x04005B44 RID: 23364
		public int attack_Min;

		// Token: 0x04005B45 RID: 23365
		public List<ushort> buffs = new List<ushort>();

		// Token: 0x04005B46 RID: 23366
		public int defence;

		// Token: 0x04005B47 RID: 23367
		public uint dialogID;

		// Token: 0x04005B48 RID: 23368
		public int dodge;

		// Token: 0x04005B49 RID: 23369
		public uint entityNO;

		// Token: 0x04005B4A RID: 23370
		public int forbids;

		// Token: 0x04005B4B RID: 23371
		public uint modelID;

		// Token: 0x04005B4C RID: 23372
		public byte modelScale = 30;

		// Token: 0x04005B4D RID: 23373
		public byte moveSpeed = 50;

		// Token: 0x04005B4E RID: 23374
		public string name = "";

		// Token: 0x04005B4F RID: 23375
		public int rating = 99;

		// Token: 0x04005B50 RID: 23376
		public ushort roleSurfaceCall = 1;

		// Token: 0x04005B51 RID: 23377
		public uint roleTypeCell;

		// Token: 0x04005B52 RID: 23378
		public sbyte state;

		// Token: 0x04005B53 RID: 23379
		public byte subState;

		// Token: 0x04005B54 RID: 23380
		public uint uid;

		// Token: 0x04005B55 RID: 23381
		public uint utype;
	}
}
