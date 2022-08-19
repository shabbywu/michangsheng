using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B34 RID: 2868
	public abstract class AvatarBase : Entity
	{
		// Token: 0x06005015 RID: 20501 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAvatarTypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onHPChanged(int oldValue)
		{
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onHungerChanged(short oldValue)
		{
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLingGengChanged(List<int> oldValue)
		{
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMPChanged(int oldValue)
		{
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMaxCardChanged(uint oldValue)
		{
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSurvivalDaysChanged(ushort oldValue)
		{
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onThirstChanged(short oldValue)
		{
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onZiZhiChanged(int oldValue)
		{
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void on_HP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void on_dunSuChanged(int oldValue)
		{
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void on_shengShiChanged(int oldValue)
		{
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAgeChanged(uint oldValue)
		{
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAttack_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAttack_MinChanged(int oldValue)
		{
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onBuffsChanged(List<ushort> oldValue)
		{
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onCrystalChanged(List<int> oldValue)
		{
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDefenceChanged(int oldValue)
		{
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDexterityChanged(int oldValue)
		{
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDodgeChanged(int oldValue)
		{
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDrawCardChanged(uint oldValue)
		{
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEquipItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEquipWeaponChanged(int oldValue)
		{
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onExpChanged(ulong oldValue)
		{
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onForbidsChanged(int oldValue)
		{
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLevelChanged(ushort oldValue)
		{
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMenPaiChanged(ushort oldValue)
		{
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMoneyChanged(ulong oldValue)
		{
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRatingChanged(int oldValue)
		{
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleSurfaceChanged(ushort oldValue)
		{
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleSurfaceCallChanged(ushort oldValue)
		{
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleTypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRoleTypeCellChanged(uint oldValue)
		{
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onShaQiChanged(uint oldValue)
		{
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onShouYuanChanged(uint oldValue)
		{
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSkillsChanged(List<int> oldValue)
		{
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onStaminaChanged(int oldValue)
		{
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onStateChanged(sbyte oldValue)
		{
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onStrengthChanged(int oldValue)
		{
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onWuXinChanged(uint oldValue)
		{
		}

		// Token: 0x06005047 RID: 20551
		public abstract void GameErrorMsg(string arg1);

		// Token: 0x06005048 RID: 20552
		public abstract void PlayerAddGoods(ITEM_INFO_LIST arg1, ushort arg2, ushort arg3);

		// Token: 0x06005049 RID: 20553
		public abstract void PlayerLvUP();

		// Token: 0x0600504A RID: 20554
		public abstract void ReceiveChatMessage(string arg1);

		// Token: 0x0600504B RID: 20555
		public abstract void clearSkills();

		// Token: 0x0600504C RID: 20556
		public abstract void createItem(ITEM_INFO arg1);

		// Token: 0x0600504D RID: 20557
		public abstract void dialog_close();

		// Token: 0x0600504E RID: 20558
		public abstract void dialog_setContent(int arg1, List<uint> arg2, List<string> arg3, string arg4, string arg5, string arg6);

		// Token: 0x0600504F RID: 20559
		public abstract void dropItem_re(int arg1, ulong arg2);

		// Token: 0x06005050 RID: 20560
		public abstract void equipItemRequest_re(ITEM_INFO arg1, ITEM_INFO arg2);

		// Token: 0x06005051 RID: 20561
		public abstract void errorInfo(int arg1);

		// Token: 0x06005052 RID: 20562
		public abstract void onAddSkill(int arg1);

		// Token: 0x06005053 RID: 20563
		public abstract void onRemoveSkill(int arg1);

		// Token: 0x06005054 RID: 20564
		public abstract void onReqItemList(ITEM_INFO_LIST arg1, ITEM_INFO_LIST arg2);

		// Token: 0x06005055 RID: 20565
		public abstract void onStartGame();

		// Token: 0x06005056 RID: 20566
		public abstract void pickUp_re(ITEM_INFO arg1);

		// Token: 0x06005057 RID: 20567
		public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

		// Token: 0x06005058 RID: 20568
		public abstract void recvSkill(int arg1, int arg2);

		// Token: 0x06005059 RID: 20569
		public abstract void setPlayerTime(uint arg1);

		// Token: 0x0600505A RID: 20570 RVA: 0x0021CC04 File Offset: 0x0021AE04
		public AvatarBase()
		{
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0021CCF6 File Offset: 0x0021AEF6
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_AvatarBase(this.id, this.className);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0021CD0F File Offset: 0x0021AF0F
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_AvatarBase(this.id, this.className);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0021CD28 File Offset: 0x0021AF28
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0021CD31 File Offset: 0x0021AF31
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0021CD39 File Offset: 0x0021AF39
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0021CD44 File Offset: 0x0021AF44
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Avatar"];
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
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				switch (methodUtype)
				{
				case 99:
					this.PlayerLvUP();
					return;
				case 100:
					this.clearSkills();
					return;
				case 101:
				{
					string arg = stream.readUnicode();
					this.GameErrorMsg(arg);
					return;
				}
				case 102:
				{
					string arg2 = stream.readUnicode();
					this.ReceiveChatMessage(arg2);
					return;
				}
				case 103:
				{
					ITEM_INFO_LIST arg3 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
					ITEM_INFO_LIST arg4 = ((DATATYPE_ITEM_INFO_LIST)method.args[1]).createFromStreamEx(stream);
					this.onReqItemList(arg3, arg4);
					return;
				}
				case 104:
				{
					ITEM_INFO arg5 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
					this.pickUp_re(arg5);
					return;
				}
				case 105:
				{
					int arg6 = stream.readInt32();
					ulong arg7 = stream.readUint64();
					this.dropItem_re(arg6, arg7);
					return;
				}
				case 106:
				{
					ITEM_INFO arg8 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
					ITEM_INFO arg9 = ((DATATYPE_ITEM_INFO)method.args[1]).createFromStreamEx(stream);
					this.equipItemRequest_re(arg8, arg9);
					return;
				}
				case 107:
				{
					ITEM_INFO_LIST arg10 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
					ushort arg11 = stream.readUint16();
					ushort arg12 = stream.readUint16();
					this.PlayerAddGoods(arg10, arg11, arg12);
					return;
				}
				case 108:
				{
					int arg13 = stream.readInt32();
					this.errorInfo(arg13);
					return;
				}
				case 109:
					this.onStartGame();
					return;
				case 110:
				{
					uint playerTime = stream.readUint32();
					this.setPlayerTime(playerTime);
					break;
				}
				case 111:
				{
					ITEM_INFO arg14 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
					this.createItem(arg14);
					return;
				}
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 122:
				case 123:
					break;
				case 120:
				{
					int arg15 = stream.readInt32();
					this.onAddSkill(arg15);
					return;
				}
				case 121:
				{
					int arg16 = stream.readInt32();
					this.onRemoveSkill(arg16);
					return;
				}
				case 124:
				{
					int arg17 = stream.readInt32();
					int arg18 = stream.readInt32();
					int arg19 = stream.readInt32();
					int arg20 = stream.readInt32();
					this.recvDamage(arg17, arg18, arg19, arg20);
					return;
				}
				case 125:
				{
					int arg21 = stream.readInt32();
					int arg22 = stream.readInt32();
					this.recvSkill(arg21, arg22);
					return;
				}
				default:
					if (methodUtype == 10101)
					{
						int arg23 = stream.readInt32();
						List<uint> arg24 = ((DATATYPE_AnonymousArray_47)method.args[1]).createFromStreamEx(stream);
						List<string> arg25 = ((DATATYPE_AnonymousArray_48)method.args[2]).createFromStreamEx(stream);
						string arg26 = stream.readUnicode();
						string arg27 = stream.readUnicode();
						string arg28 = stream.readUnicode();
						this.dialog_setContent(arg23, arg24, arg25, arg26, arg27, arg28);
						return;
					}
					if (methodUtype != 10104)
					{
						return;
					}
					this.dialog_close();
					return;
				}
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0021D094 File Offset: 0x0021B294
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Avatar"];
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
					case 12:
					{
						uint oldValue = this.roleType;
						this.roleType = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleTypeChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleTypeChanged(oldValue);
						}
						break;
					}
					case 13:
					{
						uint oldValue2 = this.drawCard;
						this.drawCard = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDrawCardChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onDrawCardChanged(oldValue2);
						}
						break;
					}
					case 14:
					{
						uint maxCard = this.MaxCard;
						this.MaxCard = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onMaxCardChanged(maxCard);
							}
						}
						else if (this.inWorld)
						{
							this.onMaxCardChanged(maxCard);
						}
						break;
					}
					case 15:
					{
						int ziZhi = this.ZiZhi;
						this.ZiZhi = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onZiZhiChanged(ziZhi);
							}
						}
						else if (this.inWorld)
						{
							this.onZiZhiChanged(ziZhi);
						}
						break;
					}
					case 16:
					{
						int dunSu = this._dunSu;
						this._dunSu = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.on_dunSuChanged(dunSu);
							}
						}
						else if (this.inWorld)
						{
							this.on_dunSuChanged(dunSu);
						}
						break;
					}
					case 17:
					{
						uint oldValue3 = this.wuXin;
						this.wuXin = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onWuXinChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onWuXinChanged(oldValue3);
						}
						break;
					}
					case 18:
					{
						int shengShi = this._shengShi;
						this._shengShi = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.on_shengShiChanged(shengShi);
							}
						}
						else if (this.inWorld)
						{
							this.on_shengShiChanged(shengShi);
						}
						break;
					}
					case 19:
					{
						uint oldValue4 = this.shaQi;
						this.shaQi = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onShaQiChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onShaQiChanged(oldValue4);
						}
						break;
					}
					case 20:
					{
						uint oldValue5 = this.shouYuan;
						this.shouYuan = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onShouYuanChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onShouYuanChanged(oldValue5);
						}
						break;
					}
					case 21:
					{
						uint oldValue6 = this.age;
						this.age = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAgeChanged(oldValue6);
							}
						}
						else if (this.inWorld)
						{
							this.onAgeChanged(oldValue6);
						}
						break;
					}
					case 22:
					{
						uint avatarType = this.AvatarType;
						this.AvatarType = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAvatarTypeChanged(avatarType);
							}
						}
						else if (this.inWorld)
						{
							this.onAvatarTypeChanged(avatarType);
						}
						break;
					}
					case 23:
					{
						ushort oldValue7 = this.roleSurface;
						this.roleSurface = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleSurfaceChanged(oldValue7);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleSurfaceChanged(oldValue7);
						}
						break;
					}
					case 24:
					{
						ushort oldValue8 = this.roleSurfaceCall;
						this.roleSurfaceCall = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleSurfaceCallChanged(oldValue8);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleSurfaceCallChanged(oldValue8);
						}
						break;
					}
					case 25:
					{
						short thirst = this.Thirst;
						this.Thirst = stream.readInt16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onThirstChanged(thirst);
							}
						}
						else if (this.inWorld)
						{
							this.onThirstChanged(thirst);
						}
						break;
					}
					case 26:
					{
						short hunger = this.Hunger;
						this.Hunger = stream.readInt16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onHungerChanged(hunger);
							}
						}
						else if (this.inWorld)
						{
							this.onHungerChanged(hunger);
						}
						break;
					}
					case 27:
					{
						List<int> oldValue9 = this.crystal;
						this.crystal = ((DATATYPE_AnonymousArray_43)EntityDef.id2datatypes[43]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onCrystalChanged(oldValue9);
							}
						}
						else if (this.inWorld)
						{
							this.onCrystalChanged(oldValue9);
						}
						break;
					}
					case 28:
					{
						List<int> lingGeng = this.LingGeng;
						this.LingGeng = ((DATATYPE_AnonymousArray_44)EntityDef.id2datatypes[44]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onLingGengChanged(lingGeng);
							}
						}
						else if (this.inWorld)
						{
							this.onLingGengChanged(lingGeng);
						}
						break;
					}
					case 29:
					{
						uint oldValue10 = this.roleTypeCell;
						this.roleTypeCell = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRoleTypeCellChanged(oldValue10);
							}
						}
						else if (this.inWorld)
						{
							this.onRoleTypeCellChanged(oldValue10);
						}
						break;
					}
					case 30:
					{
						ushort oldValue11 = this.level;
						this.level = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onLevelChanged(oldValue11);
							}
						}
						else if (this.inWorld)
						{
							this.onLevelChanged(oldValue11);
						}
						break;
					}
					case 31:
					{
						ITEM_INFO_LIST oldValue12 = this.itemList;
						this.itemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onItemListChanged(oldValue12);
							}
						}
						else if (this.inWorld)
						{
							this.onItemListChanged(oldValue12);
						}
						break;
					}
					case 32:
					{
						ushort oldValue13 = this.menPai;
						this.menPai = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onMenPaiChanged(oldValue13);
							}
						}
						else if (this.inWorld)
						{
							this.onMenPaiChanged(oldValue13);
						}
						break;
					}
					case 33:
					{
						ITEM_INFO_LIST oldValue14 = this.equipItemList;
						this.equipItemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onEquipItemListChanged(oldValue14);
							}
						}
						else if (this.inWorld)
						{
							this.onEquipItemListChanged(oldValue14);
						}
						break;
					}
					case 34:
					{
						int oldValue15 = this.equipWeapon;
						this.equipWeapon = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onEquipWeaponChanged(oldValue15);
							}
						}
						else if (this.inWorld)
						{
							this.onEquipWeaponChanged(oldValue15);
						}
						break;
					}
					case 35:
					{
						ushort survivalDays = this.SurvivalDays;
						this.SurvivalDays = stream.readUint16();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onSurvivalDaysChanged(survivalDays);
							}
						}
						else if (this.inWorld)
						{
							this.onSurvivalDaysChanged(survivalDays);
						}
						break;
					}
					case 36:
					case 37:
					case 38:
					case 40:
					case 41:
					case 43:
						break;
					case 39:
					{
						byte oldValue16 = this.moveSpeed;
						this.moveSpeed = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onMoveSpeedChanged(oldValue16);
							}
						}
						else if (this.inWorld)
						{
							this.onMoveSpeedChanged(oldValue16);
						}
						break;
					}
					case 42:
					{
						List<int> oldValue17 = this.skills;
						this.skills = ((DATATYPE_AnonymousArray_45)EntityDef.id2datatypes[45]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onSkillsChanged(oldValue17);
							}
						}
						else if (this.inWorld)
						{
							this.onSkillsChanged(oldValue17);
						}
						break;
					}
					case 44:
					{
						int oldValue18 = this.attack_Max;
						this.attack_Max = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAttack_MaxChanged(oldValue18);
							}
						}
						else if (this.inWorld)
						{
							this.onAttack_MaxChanged(oldValue18);
						}
						break;
					}
					case 45:
					{
						int oldValue19 = this.attack_Min;
						this.attack_Min = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onAttack_MinChanged(oldValue19);
							}
						}
						else if (this.inWorld)
						{
							this.onAttack_MinChanged(oldValue19);
						}
						break;
					}
					case 46:
					{
						int oldValue20 = this.defence;
						this.defence = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDefenceChanged(oldValue20);
							}
						}
						else if (this.inWorld)
						{
							this.onDefenceChanged(oldValue20);
						}
						break;
					}
					case 47:
					{
						int oldValue21 = this.rating;
						this.rating = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onRatingChanged(oldValue21);
							}
						}
						else if (this.inWorld)
						{
							this.onRatingChanged(oldValue21);
						}
						break;
					}
					case 48:
					{
						int oldValue22 = this.dodge;
						this.dodge = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDodgeChanged(oldValue22);
							}
						}
						else if (this.inWorld)
						{
							this.onDodgeChanged(oldValue22);
						}
						break;
					}
					case 49:
					{
						List<ushort> oldValue23 = this.buffs;
						this.buffs = ((DATATYPE_AnonymousArray_46)EntityDef.id2datatypes[46]).createFromStreamEx(stream);
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onBuffsChanged(oldValue23);
							}
						}
						else if (this.inWorld)
						{
							this.onBuffsChanged(oldValue23);
						}
						break;
					}
					case 50:
					{
						ulong oldValue24 = this.money;
						this.money = stream.readUint64();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onMoneyChanged(oldValue24);
							}
						}
						else if (this.inWorld)
						{
							this.onMoneyChanged(oldValue24);
						}
						break;
					}
					case 51:
					{
						ulong oldValue25 = this.exp;
						this.exp = stream.readUint64();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onExpChanged(oldValue25);
							}
						}
						else if (this.inWorld)
						{
							this.onExpChanged(oldValue25);
						}
						break;
					}
					case 52:
					{
						int oldValue26 = this.strength;
						this.strength = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onStrengthChanged(oldValue26);
							}
						}
						else if (this.inWorld)
						{
							this.onStrengthChanged(oldValue26);
						}
						break;
					}
					case 53:
					{
						int oldValue27 = this.dexterity;
						this.dexterity = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDexterityChanged(oldValue27);
							}
						}
						else if (this.inWorld)
						{
							this.onDexterityChanged(oldValue27);
						}
						break;
					}
					case 54:
					{
						int oldValue28 = this.stamina;
						this.stamina = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onStaminaChanged(oldValue28);
							}
						}
						else if (this.inWorld)
						{
							this.onStaminaChanged(oldValue28);
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
					case 41001:
					{
						uint oldValue29 = this.spaceUType;
						this.spaceUType = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onSpaceUTypeChanged(oldValue29);
							}
						}
						else if (this.inWorld)
						{
							this.onSpaceUTypeChanged(oldValue29);
						}
						break;
					}
					case 41002:
						break;
					case 41003:
					{
						string oldValue30 = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue30);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue30);
						}
						break;
					}
					case 41004:
					{
						uint oldValue31 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue31);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue31);
						}
						break;
					}
					case 41005:
					{
						uint oldValue32 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue32);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue32);
						}
						break;
					}
					case 41006:
					{
						uint oldValue33 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue33);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue33);
						}
						break;
					}
					case 41007:
					{
						byte oldValue34 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue34);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue34);
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
							int oldValue35 = this.forbids;
							this.forbids = stream.readInt32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onForbidsChanged(oldValue35);
								}
							}
							else if (this.inWorld)
							{
								this.onForbidsChanged(oldValue35);
							}
							break;
						}
						case 47006:
						{
							sbyte oldValue36 = this.state;
							this.state = stream.readInt8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onStateChanged(oldValue36);
								}
							}
							else if (this.inWorld)
							{
								this.onStateChanged(oldValue36);
							}
							break;
						}
						case 47007:
						{
							byte oldValue37 = this.subState;
							this.subState = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onSubStateChanged(oldValue37);
								}
							}
							else if (this.inWorld)
							{
								this.onSubStateChanged(oldValue37);
							}
							break;
						}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0021E250 File Offset: 0x0021C450
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Avatar"].idpropertys;
			uint avatarType = this.AvatarType;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAvatarTypeChanged(avatarType);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onAvatarTypeChanged(avatarType);
			}
			int hp = this.HP;
			Property property2 = idpropertys[5];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onHPChanged(hp);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onHPChanged(hp);
			}
			short hunger = this.Hunger;
			Property property3 = idpropertys[6];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onHungerChanged(hunger);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onHungerChanged(hunger);
			}
			List<int> lingGeng = this.LingGeng;
			Property property4 = idpropertys[7];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onLingGengChanged(lingGeng);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onLingGengChanged(lingGeng);
			}
			int mp = this.MP;
			Property property5 = idpropertys[8];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMPChanged(mp);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onMPChanged(mp);
			}
			int mp_Max = this.MP_Max;
			Property property6 = idpropertys[9];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMP_MaxChanged(mp_Max);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onMP_MaxChanged(mp_Max);
			}
			uint maxCard = this.MaxCard;
			Property property7 = idpropertys[10];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMaxCardChanged(maxCard);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onMaxCardChanged(maxCard);
			}
			ushort survivalDays = this.SurvivalDays;
			Property property8 = idpropertys[11];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onSurvivalDaysChanged(survivalDays);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onSurvivalDaysChanged(survivalDays);
			}
			short thirst = this.Thirst;
			Property property9 = idpropertys[12];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onThirstChanged(thirst);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onThirstChanged(thirst);
			}
			int ziZhi = this.ZiZhi;
			Property property10 = idpropertys[13];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onZiZhiChanged(ziZhi);
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onZiZhiChanged(ziZhi);
			}
			int hp_Max = this._HP_Max;
			Property property11 = idpropertys[14];
			if (property11.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.on_HP_MaxChanged(hp_Max);
				}
			}
			else if (this.inWorld && (!property11.isOwnerOnly() || base.isPlayer()))
			{
				this.on_HP_MaxChanged(hp_Max);
			}
			int dunSu = this._dunSu;
			Property property12 = idpropertys[15];
			if (property12.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.on_dunSuChanged(dunSu);
				}
			}
			else if (this.inWorld && (!property12.isOwnerOnly() || base.isPlayer()))
			{
				this.on_dunSuChanged(dunSu);
			}
			int shengShi = this._shengShi;
			Property property13 = idpropertys[16];
			if (property13.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.on_shengShiChanged(shengShi);
				}
			}
			else if (this.inWorld && (!property13.isOwnerOnly() || base.isPlayer()))
			{
				this.on_shengShiChanged(shengShi);
			}
			uint oldValue = this.age;
			Property property14 = idpropertys[17];
			if (property14.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAgeChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property14.isOwnerOnly() || base.isPlayer()))
			{
				this.onAgeChanged(oldValue);
			}
			int oldValue2 = this.attack_Max;
			Property property15 = idpropertys[18];
			if (property15.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAttack_MaxChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property15.isOwnerOnly() || base.isPlayer()))
			{
				this.onAttack_MaxChanged(oldValue2);
			}
			int oldValue3 = this.attack_Min;
			Property property16 = idpropertys[19];
			if (property16.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onAttack_MinChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property16.isOwnerOnly() || base.isPlayer()))
			{
				this.onAttack_MinChanged(oldValue3);
			}
			List<ushort> oldValue4 = this.buffs;
			Property property17 = idpropertys[20];
			if (property17.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onBuffsChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property17.isOwnerOnly() || base.isPlayer()))
			{
				this.onBuffsChanged(oldValue4);
			}
			List<int> oldValue5 = this.crystal;
			Property property18 = idpropertys[21];
			if (property18.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onCrystalChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property18.isOwnerOnly() || base.isPlayer()))
			{
				this.onCrystalChanged(oldValue5);
			}
			int oldValue6 = this.defence;
			Property property19 = idpropertys[22];
			if (property19.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDefenceChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property19.isOwnerOnly() || base.isPlayer()))
			{
				this.onDefenceChanged(oldValue6);
			}
			int oldValue7 = this.dexterity;
			Property property20 = idpropertys[23];
			if (property20.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDexterityChanged(oldValue7);
				}
			}
			else if (this.inWorld && (!property20.isOwnerOnly() || base.isPlayer()))
			{
				this.onDexterityChanged(oldValue7);
			}
			Vector3 direction = this.direction;
			Property property21 = idpropertys[2];
			if (property21.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property21.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			int oldValue8 = this.dodge;
			Property property22 = idpropertys[24];
			if (property22.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDodgeChanged(oldValue8);
				}
			}
			else if (this.inWorld && (!property22.isOwnerOnly() || base.isPlayer()))
			{
				this.onDodgeChanged(oldValue8);
			}
			uint oldValue9 = this.drawCard;
			Property property23 = idpropertys[25];
			if (property23.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDrawCardChanged(oldValue9);
				}
			}
			else if (this.inWorld && (!property23.isOwnerOnly() || base.isPlayer()))
			{
				this.onDrawCardChanged(oldValue9);
			}
			ITEM_INFO_LIST oldValue10 = this.equipItemList;
			Property property24 = idpropertys[26];
			if (property24.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEquipItemListChanged(oldValue10);
				}
			}
			else if (this.inWorld && (!property24.isOwnerOnly() || base.isPlayer()))
			{
				this.onEquipItemListChanged(oldValue10);
			}
			int oldValue11 = this.equipWeapon;
			Property property25 = idpropertys[27];
			if (property25.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEquipWeaponChanged(oldValue11);
				}
			}
			else if (this.inWorld && (!property25.isOwnerOnly() || base.isPlayer()))
			{
				this.onEquipWeaponChanged(oldValue11);
			}
			ulong oldValue12 = this.exp;
			Property property26 = idpropertys[28];
			if (property26.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onExpChanged(oldValue12);
				}
			}
			else if (this.inWorld && (!property26.isOwnerOnly() || base.isPlayer()))
			{
				this.onExpChanged(oldValue12);
			}
			int oldValue13 = this.forbids;
			Property property27 = idpropertys[29];
			if (property27.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onForbidsChanged(oldValue13);
				}
			}
			else if (this.inWorld && (!property27.isOwnerOnly() || base.isPlayer()))
			{
				this.onForbidsChanged(oldValue13);
			}
			ITEM_INFO_LIST oldValue14 = this.itemList;
			Property property28 = idpropertys[30];
			if (property28.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onItemListChanged(oldValue14);
				}
			}
			else if (this.inWorld && (!property28.isOwnerOnly() || base.isPlayer()))
			{
				this.onItemListChanged(oldValue14);
			}
			ushort oldValue15 = this.level;
			Property property29 = idpropertys[31];
			if (property29.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onLevelChanged(oldValue15);
				}
			}
			else if (this.inWorld && (!property29.isOwnerOnly() || base.isPlayer()))
			{
				this.onLevelChanged(oldValue15);
			}
			ushort oldValue16 = this.menPai;
			Property property30 = idpropertys[32];
			if (property30.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMenPaiChanged(oldValue16);
				}
			}
			else if (this.inWorld && (!property30.isOwnerOnly() || base.isPlayer()))
			{
				this.onMenPaiChanged(oldValue16);
			}
			uint oldValue17 = this.modelID;
			Property property31 = idpropertys[33];
			if (property31.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue17);
				}
			}
			else if (this.inWorld && (!property31.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue17);
			}
			byte oldValue18 = this.modelScale;
			Property property32 = idpropertys[34];
			if (property32.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue18);
				}
			}
			else if (this.inWorld && (!property32.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue18);
			}
			ulong oldValue19 = this.money;
			Property property33 = idpropertys[35];
			if (property33.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMoneyChanged(oldValue19);
				}
			}
			else if (this.inWorld && (!property33.isOwnerOnly() || base.isPlayer()))
			{
				this.onMoneyChanged(oldValue19);
			}
			byte oldValue20 = this.moveSpeed;
			Property property34 = idpropertys[36];
			if (property34.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMoveSpeedChanged(oldValue20);
				}
			}
			else if (this.inWorld && (!property34.isOwnerOnly() || base.isPlayer()))
			{
				this.onMoveSpeedChanged(oldValue20);
			}
			string oldValue21 = this.name;
			Property property35 = idpropertys[37];
			if (property35.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue21);
				}
			}
			else if (this.inWorld && (!property35.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue21);
			}
			Vector3 position = this.position;
			Property property36 = idpropertys[1];
			if (property36.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property36.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			int oldValue22 = this.rating;
			Property property37 = idpropertys[38];
			if (property37.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRatingChanged(oldValue22);
				}
			}
			else if (this.inWorld && (!property37.isOwnerOnly() || base.isPlayer()))
			{
				this.onRatingChanged(oldValue22);
			}
			ushort oldValue23 = this.roleSurface;
			Property property38 = idpropertys[39];
			if (property38.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleSurfaceChanged(oldValue23);
				}
			}
			else if (this.inWorld && (!property38.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleSurfaceChanged(oldValue23);
			}
			ushort oldValue24 = this.roleSurfaceCall;
			Property property39 = idpropertys[40];
			if (property39.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleSurfaceCallChanged(oldValue24);
				}
			}
			else if (this.inWorld && (!property39.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleSurfaceCallChanged(oldValue24);
			}
			uint oldValue25 = this.roleType;
			Property property40 = idpropertys[41];
			if (property40.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleTypeChanged(oldValue25);
				}
			}
			else if (this.inWorld && (!property40.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleTypeChanged(oldValue25);
			}
			uint oldValue26 = this.roleTypeCell;
			Property property41 = idpropertys[42];
			if (property41.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onRoleTypeCellChanged(oldValue26);
				}
			}
			else if (this.inWorld && (!property41.isOwnerOnly() || base.isPlayer()))
			{
				this.onRoleTypeCellChanged(oldValue26);
			}
			uint oldValue27 = this.shaQi;
			Property property42 = idpropertys[43];
			if (property42.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onShaQiChanged(oldValue27);
				}
			}
			else if (this.inWorld && (!property42.isOwnerOnly() || base.isPlayer()))
			{
				this.onShaQiChanged(oldValue27);
			}
			uint oldValue28 = this.shouYuan;
			Property property43 = idpropertys[44];
			if (property43.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onShouYuanChanged(oldValue28);
				}
			}
			else if (this.inWorld && (!property43.isOwnerOnly() || base.isPlayer()))
			{
				this.onShouYuanChanged(oldValue28);
			}
			List<int> oldValue29 = this.skills;
			Property property44 = idpropertys[45];
			if (property44.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onSkillsChanged(oldValue29);
				}
			}
			else if (this.inWorld && (!property44.isOwnerOnly() || base.isPlayer()))
			{
				this.onSkillsChanged(oldValue29);
			}
			uint oldValue30 = this.spaceUType;
			Property property45 = idpropertys[46];
			if (property45.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onSpaceUTypeChanged(oldValue30);
				}
			}
			else if (this.inWorld && (!property45.isOwnerOnly() || base.isPlayer()))
			{
				this.onSpaceUTypeChanged(oldValue30);
			}
			int oldValue31 = this.stamina;
			Property property46 = idpropertys[47];
			if (property46.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onStaminaChanged(oldValue31);
				}
			}
			else if (this.inWorld && (!property46.isOwnerOnly() || base.isPlayer()))
			{
				this.onStaminaChanged(oldValue31);
			}
			sbyte oldValue32 = this.state;
			Property property47 = idpropertys[48];
			if (property47.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onStateChanged(oldValue32);
				}
			}
			else if (this.inWorld && (!property47.isOwnerOnly() || base.isPlayer()))
			{
				this.onStateChanged(oldValue32);
			}
			int oldValue33 = this.strength;
			Property property48 = idpropertys[49];
			if (property48.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onStrengthChanged(oldValue33);
				}
			}
			else if (this.inWorld && (!property48.isOwnerOnly() || base.isPlayer()))
			{
				this.onStrengthChanged(oldValue33);
			}
			byte oldValue34 = this.subState;
			Property property49 = idpropertys[50];
			if (property49.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onSubStateChanged(oldValue34);
				}
			}
			else if (this.inWorld && (!property49.isOwnerOnly() || base.isPlayer()))
			{
				this.onSubStateChanged(oldValue34);
			}
			uint oldValue35 = this.uid;
			Property property50 = idpropertys[51];
			if (property50.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue35);
				}
			}
			else if (this.inWorld && (!property50.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue35);
			}
			uint oldValue36 = this.utype;
			Property property51 = idpropertys[52];
			if (property51.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue36);
				}
			}
			else if (this.inWorld && (!property51.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue36);
			}
			uint oldValue37 = this.wuXin;
			Property property52 = idpropertys[53];
			if (property52.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onWuXinChanged(oldValue37);
					return;
				}
			}
			else if (this.inWorld && (!property52.isOwnerOnly() || base.isPlayer()))
			{
				this.onWuXinChanged(oldValue37);
			}
		}

		// Token: 0x04004F27 RID: 20263
		public EntityBaseEntityCall_AvatarBase baseEntityCall;

		// Token: 0x04004F28 RID: 20264
		public EntityCellEntityCall_AvatarBase cellEntityCall;

		// Token: 0x04004F29 RID: 20265
		public uint AvatarType = 5U;

		// Token: 0x04004F2A RID: 20266
		public int HP;

		// Token: 0x04004F2B RID: 20267
		public short Hunger = 100;

		// Token: 0x04004F2C RID: 20268
		public List<int> LingGeng = new List<int>();

		// Token: 0x04004F2D RID: 20269
		public int MP;

		// Token: 0x04004F2E RID: 20270
		public int MP_Max;

		// Token: 0x04004F2F RID: 20271
		public uint MaxCard = 5U;

		// Token: 0x04004F30 RID: 20272
		public ushort SurvivalDays;

		// Token: 0x04004F31 RID: 20273
		public short Thirst = 100;

		// Token: 0x04004F32 RID: 20274
		public int ZiZhi = 5;

		// Token: 0x04004F33 RID: 20275
		public int _HP_Max;

		// Token: 0x04004F34 RID: 20276
		public int _dunSu = 5;

		// Token: 0x04004F35 RID: 20277
		public int _shengShi = 5;

		// Token: 0x04004F36 RID: 20278
		public uint age = 5U;

		// Token: 0x04004F37 RID: 20279
		public int attack_Max = 10;

		// Token: 0x04004F38 RID: 20280
		public int attack_Min;

		// Token: 0x04004F39 RID: 20281
		public List<ushort> buffs = new List<ushort>();

		// Token: 0x04004F3A RID: 20282
		public List<int> crystal = new List<int>();

		// Token: 0x04004F3B RID: 20283
		public int defence;

		// Token: 0x04004F3C RID: 20284
		public int dexterity;

		// Token: 0x04004F3D RID: 20285
		public int dodge;

		// Token: 0x04004F3E RID: 20286
		public uint drawCard = 5U;

		// Token: 0x04004F3F RID: 20287
		public ITEM_INFO_LIST equipItemList = new ITEM_INFO_LIST();

		// Token: 0x04004F40 RID: 20288
		public int equipWeapon = -1;

		// Token: 0x04004F41 RID: 20289
		public ulong exp;

		// Token: 0x04004F42 RID: 20290
		public int forbids;

		// Token: 0x04004F43 RID: 20291
		public ITEM_INFO_LIST itemList = new ITEM_INFO_LIST();

		// Token: 0x04004F44 RID: 20292
		public ushort level = 1;

		// Token: 0x04004F45 RID: 20293
		public ushort menPai;

		// Token: 0x04004F46 RID: 20294
		public uint modelID;

		// Token: 0x04004F47 RID: 20295
		public byte modelScale = 30;

		// Token: 0x04004F48 RID: 20296
		public ulong money;

		// Token: 0x04004F49 RID: 20297
		public byte moveSpeed = 50;

		// Token: 0x04004F4A RID: 20298
		public string name = "";

		// Token: 0x04004F4B RID: 20299
		public int rating = 99;

		// Token: 0x04004F4C RID: 20300
		public ushort roleSurface = 1;

		// Token: 0x04004F4D RID: 20301
		public ushort roleSurfaceCall = 1;

		// Token: 0x04004F4E RID: 20302
		public uint roleType;

		// Token: 0x04004F4F RID: 20303
		public uint roleTypeCell;

		// Token: 0x04004F50 RID: 20304
		public uint shaQi = 5U;

		// Token: 0x04004F51 RID: 20305
		public uint shouYuan = 5U;

		// Token: 0x04004F52 RID: 20306
		public List<int> skills = new List<int>();

		// Token: 0x04004F53 RID: 20307
		public uint spaceUType;

		// Token: 0x04004F54 RID: 20308
		public int stamina;

		// Token: 0x04004F55 RID: 20309
		public sbyte state;

		// Token: 0x04004F56 RID: 20310
		public int strength;

		// Token: 0x04004F57 RID: 20311
		public byte subState;

		// Token: 0x04004F58 RID: 20312
		public uint uid;

		// Token: 0x04004F59 RID: 20313
		public uint utype;

		// Token: 0x04004F5A RID: 20314
		public uint wuXin = 5U;
	}
}
