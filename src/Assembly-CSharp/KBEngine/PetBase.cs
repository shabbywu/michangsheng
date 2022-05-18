using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FE6 RID: 4070
	public abstract class PetBase : Entity
	{
		// Token: 0x0600603F RID: 24639 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onHPChanged(int oldValue)
		{
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMPChanged(int oldValue)
		{
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void on_HP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MinChanged(int oldValue)
		{
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onBuffsChanged(List<ushort> oldValue)
		{
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDefenceChanged(int oldValue)
		{
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDodgeChanged(int oldValue)
		{
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onForbidsChanged(int oldValue)
		{
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRatingChanged(int oldValue)
		{
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleSurfaceCallChanged(ushort oldValue)
		{
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleTypeCellChanged(uint oldValue)
		{
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStateChanged(sbyte oldValue)
		{
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06006056 RID: 24662
		public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

		// Token: 0x06006057 RID: 24663
		public abstract void recvSkill(int arg1, int arg2);

		// Token: 0x06006058 RID: 24664 RVA: 0x002679D4 File Offset: 0x00265BD4
		public PetBase()
		{
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x00042EBB File Offset: 0x000410BB
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_PetBase(this.id, this.className);
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x00042ED4 File Offset: 0x000410D4
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_PetBase(this.id, this.className);
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x00042EED File Offset: 0x000410ED
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x00042EF6 File Offset: 0x000410F6
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x00042EFE File Offset: 0x000410FE
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00267A24 File Offset: 0x00265C24
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

		// Token: 0x06006063 RID: 24675 RVA: 0x00267B00 File Offset: 0x00265D00
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

		// Token: 0x06006064 RID: 24676 RVA: 0x002683E4 File Offset: 0x002665E4
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

		// Token: 0x04005B88 RID: 23432
		public EntityBaseEntityCall_PetBase baseEntityCall;

		// Token: 0x04005B89 RID: 23433
		public EntityCellEntityCall_PetBase cellEntityCall;

		// Token: 0x04005B8A RID: 23434
		public int HP;

		// Token: 0x04005B8B RID: 23435
		public int MP;

		// Token: 0x04005B8C RID: 23436
		public int MP_Max;

		// Token: 0x04005B8D RID: 23437
		public int _HP_Max;

		// Token: 0x04005B8E RID: 23438
		public int attack_Max = 10;

		// Token: 0x04005B8F RID: 23439
		public int attack_Min;

		// Token: 0x04005B90 RID: 23440
		public List<ushort> buffs = new List<ushort>();

		// Token: 0x04005B91 RID: 23441
		public int defence;

		// Token: 0x04005B92 RID: 23442
		public uint dialogID;

		// Token: 0x04005B93 RID: 23443
		public int dodge;

		// Token: 0x04005B94 RID: 23444
		public uint entityNO;

		// Token: 0x04005B95 RID: 23445
		public int forbids;

		// Token: 0x04005B96 RID: 23446
		public uint modelID;

		// Token: 0x04005B97 RID: 23447
		public byte modelScale = 30;

		// Token: 0x04005B98 RID: 23448
		public byte moveSpeed = 50;

		// Token: 0x04005B99 RID: 23449
		public string name = "";

		// Token: 0x04005B9A RID: 23450
		public int rating = 99;

		// Token: 0x04005B9B RID: 23451
		public ushort roleSurfaceCall = 1;

		// Token: 0x04005B9C RID: 23452
		public uint roleTypeCell;

		// Token: 0x04005B9D RID: 23453
		public sbyte state;

		// Token: 0x04005B9E RID: 23454
		public byte subState;

		// Token: 0x04005B9F RID: 23455
		public uint uid;

		// Token: 0x04005BA0 RID: 23456
		public uint utype;
	}
}
