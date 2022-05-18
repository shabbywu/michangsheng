using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EAB RID: 3755
	public abstract class AvatarBase : Entity
	{
		// Token: 0x06005A3F RID: 23103 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAvatarTypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onHPChanged(int oldValue)
		{
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onHungerChanged(short oldValue)
		{
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLingGengChanged(List<int> oldValue)
		{
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMPChanged(int oldValue)
		{
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMaxCardChanged(uint oldValue)
		{
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSurvivalDaysChanged(ushort oldValue)
		{
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onThirstChanged(short oldValue)
		{
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onZiZhiChanged(int oldValue)
		{
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void on_HP_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void on_dunSuChanged(int oldValue)
		{
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void on_shengShiChanged(int oldValue)
		{
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAgeChanged(uint oldValue)
		{
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MaxChanged(int oldValue)
		{
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttack_MinChanged(int oldValue)
		{
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onBuffsChanged(List<ushort> oldValue)
		{
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onCrystalChanged(List<int> oldValue)
		{
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDefenceChanged(int oldValue)
		{
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDexterityChanged(int oldValue)
		{
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDodgeChanged(int oldValue)
		{
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDrawCardChanged(uint oldValue)
		{
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEquipItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEquipWeaponChanged(int oldValue)
		{
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onExpChanged(ulong oldValue)
		{
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onForbidsChanged(int oldValue)
		{
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLevelChanged(ushort oldValue)
		{
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMenPaiChanged(ushort oldValue)
		{
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMoneyChanged(ulong oldValue)
		{
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMoveSpeedChanged(byte oldValue)
		{
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRatingChanged(int oldValue)
		{
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleSurfaceChanged(ushort oldValue)
		{
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleSurfaceCallChanged(ushort oldValue)
		{
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleTypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRoleTypeCellChanged(uint oldValue)
		{
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onShaQiChanged(uint oldValue)
		{
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onShouYuanChanged(uint oldValue)
		{
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSkillsChanged(List<int> oldValue)
		{
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStaminaChanged(int oldValue)
		{
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStateChanged(sbyte oldValue)
		{
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStrengthChanged(int oldValue)
		{
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onWuXinChanged(uint oldValue)
		{
		}

		// Token: 0x06005A71 RID: 23153
		public abstract void GameErrorMsg(string arg1);

		// Token: 0x06005A72 RID: 23154
		public abstract void PlayerAddGoods(ITEM_INFO_LIST arg1, ushort arg2, ushort arg3);

		// Token: 0x06005A73 RID: 23155
		public abstract void PlayerLvUP();

		// Token: 0x06005A74 RID: 23156
		public abstract void ReceiveChatMessage(string arg1);

		// Token: 0x06005A75 RID: 23157
		public abstract void clearSkills();

		// Token: 0x06005A76 RID: 23158
		public abstract void createItem(ITEM_INFO arg1);

		// Token: 0x06005A77 RID: 23159
		public abstract void dialog_close();

		// Token: 0x06005A78 RID: 23160
		public abstract void dialog_setContent(int arg1, List<uint> arg2, List<string> arg3, string arg4, string arg5, string arg6);

		// Token: 0x06005A79 RID: 23161
		public abstract void dropItem_re(int arg1, ulong arg2);

		// Token: 0x06005A7A RID: 23162
		public abstract void equipItemRequest_re(ITEM_INFO arg1, ITEM_INFO arg2);

		// Token: 0x06005A7B RID: 23163
		public abstract void errorInfo(int arg1);

		// Token: 0x06005A7C RID: 23164
		public abstract void onAddSkill(int arg1);

		// Token: 0x06005A7D RID: 23165
		public abstract void onRemoveSkill(int arg1);

		// Token: 0x06005A7E RID: 23166
		public abstract void onReqItemList(ITEM_INFO_LIST arg1, ITEM_INFO_LIST arg2);

		// Token: 0x06005A7F RID: 23167
		public abstract void onStartGame();

		// Token: 0x06005A80 RID: 23168
		public abstract void pickUp_re(ITEM_INFO arg1);

		// Token: 0x06005A81 RID: 23169
		public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

		// Token: 0x06005A82 RID: 23170
		public abstract void recvSkill(int arg1, int arg2);

		// Token: 0x06005A83 RID: 23171
		public abstract void setPlayerTime(uint arg1);

		// Token: 0x06005A84 RID: 23172 RVA: 0x0024C7EC File Offset: 0x0024A9EC
		public AvatarBase()
		{
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x0003FE0B File Offset: 0x0003E00B
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_AvatarBase(this.id, this.className);
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x0003FE24 File Offset: 0x0003E024
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_AvatarBase(this.id, this.className);
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x0003FE3D File Offset: 0x0003E03D
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0003FE46 File Offset: 0x0003E046
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0003FE4E File Offset: 0x0003E04E
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x0024C8E0 File Offset: 0x0024AAE0
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

		// Token: 0x06005A8F RID: 23183 RVA: 0x0024CC30 File Offset: 0x0024AE30
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

		// Token: 0x06005A90 RID: 23184 RVA: 0x0024DDEC File Offset: 0x0024BFEC
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

		// Token: 0x040059AC RID: 22956
		public EntityBaseEntityCall_AvatarBase baseEntityCall;

		// Token: 0x040059AD RID: 22957
		public EntityCellEntityCall_AvatarBase cellEntityCall;

		// Token: 0x040059AE RID: 22958
		public uint AvatarType = 5U;

		// Token: 0x040059AF RID: 22959
		public int HP;

		// Token: 0x040059B0 RID: 22960
		public short Hunger = 100;

		// Token: 0x040059B1 RID: 22961
		public List<int> LingGeng = new List<int>();

		// Token: 0x040059B2 RID: 22962
		public int MP;

		// Token: 0x040059B3 RID: 22963
		public int MP_Max;

		// Token: 0x040059B4 RID: 22964
		public uint MaxCard = 5U;

		// Token: 0x040059B5 RID: 22965
		public ushort SurvivalDays;

		// Token: 0x040059B6 RID: 22966
		public short Thirst = 100;

		// Token: 0x040059B7 RID: 22967
		public int ZiZhi = 5;

		// Token: 0x040059B8 RID: 22968
		public int _HP_Max;

		// Token: 0x040059B9 RID: 22969
		public int _dunSu = 5;

		// Token: 0x040059BA RID: 22970
		public int _shengShi = 5;

		// Token: 0x040059BB RID: 22971
		public uint age = 5U;

		// Token: 0x040059BC RID: 22972
		public int attack_Max = 10;

		// Token: 0x040059BD RID: 22973
		public int attack_Min;

		// Token: 0x040059BE RID: 22974
		public List<ushort> buffs = new List<ushort>();

		// Token: 0x040059BF RID: 22975
		public List<int> crystal = new List<int>();

		// Token: 0x040059C0 RID: 22976
		public int defence;

		// Token: 0x040059C1 RID: 22977
		public int dexterity;

		// Token: 0x040059C2 RID: 22978
		public int dodge;

		// Token: 0x040059C3 RID: 22979
		public uint drawCard = 5U;

		// Token: 0x040059C4 RID: 22980
		public ITEM_INFO_LIST equipItemList = new ITEM_INFO_LIST();

		// Token: 0x040059C5 RID: 22981
		public int equipWeapon = -1;

		// Token: 0x040059C6 RID: 22982
		public ulong exp;

		// Token: 0x040059C7 RID: 22983
		public int forbids;

		// Token: 0x040059C8 RID: 22984
		public ITEM_INFO_LIST itemList = new ITEM_INFO_LIST();

		// Token: 0x040059C9 RID: 22985
		public ushort level = 1;

		// Token: 0x040059CA RID: 22986
		public ushort menPai;

		// Token: 0x040059CB RID: 22987
		public uint modelID;

		// Token: 0x040059CC RID: 22988
		public byte modelScale = 30;

		// Token: 0x040059CD RID: 22989
		public ulong money;

		// Token: 0x040059CE RID: 22990
		public byte moveSpeed = 50;

		// Token: 0x040059CF RID: 22991
		public string name = "";

		// Token: 0x040059D0 RID: 22992
		public int rating = 99;

		// Token: 0x040059D1 RID: 22993
		public ushort roleSurface = 1;

		// Token: 0x040059D2 RID: 22994
		public ushort roleSurfaceCall = 1;

		// Token: 0x040059D3 RID: 22995
		public uint roleType;

		// Token: 0x040059D4 RID: 22996
		public uint roleTypeCell;

		// Token: 0x040059D5 RID: 22997
		public uint shaQi = 5U;

		// Token: 0x040059D6 RID: 22998
		public uint shouYuan = 5U;

		// Token: 0x040059D7 RID: 22999
		public List<int> skills = new List<int>();

		// Token: 0x040059D8 RID: 23000
		public uint spaceUType;

		// Token: 0x040059D9 RID: 23001
		public int stamina;

		// Token: 0x040059DA RID: 23002
		public sbyte state;

		// Token: 0x040059DB RID: 23003
		public int strength;

		// Token: 0x040059DC RID: 23004
		public byte subState;

		// Token: 0x040059DD RID: 23005
		public uint uid;

		// Token: 0x040059DE RID: 23006
		public uint utype;

		// Token: 0x040059DF RID: 23007
		public uint wuXin = 5U;
	}
}
