using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class AvatarBase : Entity
{
	public EntityBaseEntityCall_AvatarBase baseEntityCall;

	public EntityCellEntityCall_AvatarBase cellEntityCall;

	public uint AvatarType = 5u;

	public int HP;

	public short Hunger = 100;

	public List<int> LingGeng = new List<int>();

	public int MP;

	public int MP_Max;

	public uint MaxCard = 5u;

	public ushort SurvivalDays;

	public short Thirst = 100;

	public int ZiZhi = 5;

	public int _HP_Max;

	public int _dunSu = 5;

	public int _shengShi = 5;

	public uint age = 5u;

	public int attack_Max = 10;

	public int attack_Min;

	public List<ushort> buffs = new List<ushort>();

	public List<int> crystal = new List<int>();

	public int defence;

	public int dexterity;

	public int dodge;

	public uint drawCard = 5u;

	public ITEM_INFO_LIST equipItemList = new ITEM_INFO_LIST();

	public int equipWeapon = -1;

	public ulong exp;

	public int forbids;

	public ITEM_INFO_LIST itemList = new ITEM_INFO_LIST();

	public ushort level = 1;

	public ushort menPai;

	public uint modelID;

	public byte modelScale = 30;

	public ulong money;

	public byte moveSpeed = 50;

	public string name = "";

	public int rating = 99;

	public ushort roleSurface = 1;

	public ushort roleSurfaceCall = 1;

	public uint roleType;

	public uint roleTypeCell;

	public uint shaQi = 5u;

	public uint shouYuan = 5u;

	public List<int> skills = new List<int>();

	public uint spaceUType;

	public int stamina;

	public sbyte state;

	public int strength;

	public byte subState;

	public uint uid;

	public uint utype;

	public uint wuXin = 5u;

	public virtual void onAvatarTypeChanged(uint oldValue)
	{
	}

	public virtual void onHPChanged(int oldValue)
	{
	}

	public virtual void onHungerChanged(short oldValue)
	{
	}

	public virtual void onLingGengChanged(List<int> oldValue)
	{
	}

	public virtual void onMPChanged(int oldValue)
	{
	}

	public virtual void onMP_MaxChanged(int oldValue)
	{
	}

	public virtual void onMaxCardChanged(uint oldValue)
	{
	}

	public virtual void onSurvivalDaysChanged(ushort oldValue)
	{
	}

	public virtual void onThirstChanged(short oldValue)
	{
	}

	public virtual void onZiZhiChanged(int oldValue)
	{
	}

	public virtual void on_HP_MaxChanged(int oldValue)
	{
	}

	public virtual void on_dunSuChanged(int oldValue)
	{
	}

	public virtual void on_shengShiChanged(int oldValue)
	{
	}

	public virtual void onAgeChanged(uint oldValue)
	{
	}

	public virtual void onAttack_MaxChanged(int oldValue)
	{
	}

	public virtual void onAttack_MinChanged(int oldValue)
	{
	}

	public virtual void onBuffsChanged(List<ushort> oldValue)
	{
	}

	public virtual void onCrystalChanged(List<int> oldValue)
	{
	}

	public virtual void onDefenceChanged(int oldValue)
	{
	}

	public virtual void onDexterityChanged(int oldValue)
	{
	}

	public virtual void onDodgeChanged(int oldValue)
	{
	}

	public virtual void onDrawCardChanged(uint oldValue)
	{
	}

	public virtual void onEquipItemListChanged(ITEM_INFO_LIST oldValue)
	{
	}

	public virtual void onEquipWeaponChanged(int oldValue)
	{
	}

	public virtual void onExpChanged(ulong oldValue)
	{
	}

	public virtual void onForbidsChanged(int oldValue)
	{
	}

	public virtual void onItemListChanged(ITEM_INFO_LIST oldValue)
	{
	}

	public virtual void onLevelChanged(ushort oldValue)
	{
	}

	public virtual void onMenPaiChanged(ushort oldValue)
	{
	}

	public virtual void onModelIDChanged(uint oldValue)
	{
	}

	public virtual void onModelScaleChanged(byte oldValue)
	{
	}

	public virtual void onMoneyChanged(ulong oldValue)
	{
	}

	public virtual void onMoveSpeedChanged(byte oldValue)
	{
	}

	public virtual void onNameChanged(string oldValue)
	{
	}

	public virtual void onRatingChanged(int oldValue)
	{
	}

	public virtual void onRoleSurfaceChanged(ushort oldValue)
	{
	}

	public virtual void onRoleSurfaceCallChanged(ushort oldValue)
	{
	}

	public virtual void onRoleTypeChanged(uint oldValue)
	{
	}

	public virtual void onRoleTypeCellChanged(uint oldValue)
	{
	}

	public virtual void onShaQiChanged(uint oldValue)
	{
	}

	public virtual void onShouYuanChanged(uint oldValue)
	{
	}

	public virtual void onSkillsChanged(List<int> oldValue)
	{
	}

	public virtual void onSpaceUTypeChanged(uint oldValue)
	{
	}

	public virtual void onStaminaChanged(int oldValue)
	{
	}

	public virtual void onStateChanged(sbyte oldValue)
	{
	}

	public virtual void onStrengthChanged(int oldValue)
	{
	}

	public virtual void onSubStateChanged(byte oldValue)
	{
	}

	public virtual void onUidChanged(uint oldValue)
	{
	}

	public virtual void onUtypeChanged(uint oldValue)
	{
	}

	public virtual void onWuXinChanged(uint oldValue)
	{
	}

	public abstract void GameErrorMsg(string arg1);

	public abstract void PlayerAddGoods(ITEM_INFO_LIST arg1, ushort arg2, ushort arg3);

	public abstract void PlayerLvUP();

	public abstract void ReceiveChatMessage(string arg1);

	public abstract void clearSkills();

	public abstract void createItem(ITEM_INFO arg1);

	public abstract void dialog_close();

	public abstract void dialog_setContent(int arg1, List<uint> arg2, List<string> arg3, string arg4, string arg5, string arg6);

	public abstract void dropItem_re(int arg1, ulong arg2);

	public abstract void equipItemRequest_re(ITEM_INFO arg1, ITEM_INFO arg2);

	public abstract void errorInfo(int arg1);

	public abstract void onAddSkill(int arg1);

	public abstract void onRemoveSkill(int arg1);

	public abstract void onReqItemList(ITEM_INFO_LIST arg1, ITEM_INFO_LIST arg2);

	public abstract void onStartGame();

	public abstract void pickUp_re(ITEM_INFO arg1);

	public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

	public abstract void recvSkill(int arg1, int arg2);

	public abstract void setPlayerTime(uint arg1);

	public AvatarBase()
	{
	}

	public override void onComponentsEnterworld()
	{
	}

	public override void onComponentsLeaveworld()
	{
	}

	public override void onGetBase()
	{
		baseEntityCall = new EntityBaseEntityCall_AvatarBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_AvatarBase(id, className);
	}

	public override void onLoseCell()
	{
		cellEntityCall = null;
	}

	public override EntityCall getBaseEntityCall()
	{
		return baseEntityCall;
	}

	public override EntityCall getCellEntityCall()
	{
		return cellEntityCall;
	}

	public override void attachComponents()
	{
	}

	public override void detachComponents()
	{
	}

	public override void onRemoteMethodCall(MemoryStream stream)
	{
		ScriptModule scriptModule = EntityDef.moduledefs["Avatar"];
		ushort num = 0;
		ushort num2 = 0;
		num2 = ((!scriptModule.usePropertyDescrAlias) ? stream.readUint16() : stream.readUint8());
		num = ((!scriptModule.useMethodDescrAlias) ? stream.readUint16() : stream.readUint8());
		Method method = null;
		if (num2 == 0)
		{
			method = scriptModule.idmethods[num];
			switch (method.methodUtype)
			{
			case 101:
			{
				string arg28 = stream.readUnicode();
				GameErrorMsg(arg28);
				break;
			}
			case 107:
			{
				ITEM_INFO_LIST arg25 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				ushort arg26 = stream.readUint16();
				ushort arg27 = stream.readUint16();
				PlayerAddGoods(arg25, arg26, arg27);
				break;
			}
			case 99:
				PlayerLvUP();
				break;
			case 102:
			{
				string arg24 = stream.readUnicode();
				ReceiveChatMessage(arg24);
				break;
			}
			case 100:
				clearSkills();
				break;
			case 111:
			{
				ITEM_INFO arg23 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
				createItem(arg23);
				break;
			}
			case 10104:
				dialog_close();
				break;
			case 10101:
			{
				int arg17 = stream.readInt32();
				List<uint> arg18 = ((DATATYPE_AnonymousArray_47)method.args[1]).createFromStreamEx(stream);
				List<string> arg19 = ((DATATYPE_AnonymousArray_48)method.args[2]).createFromStreamEx(stream);
				string arg20 = stream.readUnicode();
				string arg21 = stream.readUnicode();
				string arg22 = stream.readUnicode();
				dialog_setContent(arg17, arg18, arg19, arg20, arg21, arg22);
				break;
			}
			case 105:
			{
				int arg15 = stream.readInt32();
				ulong arg16 = stream.readUint64();
				dropItem_re(arg15, arg16);
				break;
			}
			case 106:
			{
				ITEM_INFO arg13 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
				ITEM_INFO arg14 = ((DATATYPE_ITEM_INFO)method.args[1]).createFromStreamEx(stream);
				equipItemRequest_re(arg13, arg14);
				break;
			}
			case 108:
			{
				int arg12 = stream.readInt32();
				errorInfo(arg12);
				break;
			}
			case 120:
			{
				int arg11 = stream.readInt32();
				onAddSkill(arg11);
				break;
			}
			case 121:
			{
				int arg10 = stream.readInt32();
				onRemoveSkill(arg10);
				break;
			}
			case 103:
			{
				ITEM_INFO_LIST arg8 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				ITEM_INFO_LIST arg9 = ((DATATYPE_ITEM_INFO_LIST)method.args[1]).createFromStreamEx(stream);
				onReqItemList(arg8, arg9);
				break;
			}
			case 109:
				onStartGame();
				break;
			case 104:
			{
				ITEM_INFO arg7 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
				pickUp_re(arg7);
				break;
			}
			case 124:
			{
				int arg3 = stream.readInt32();
				int arg4 = stream.readInt32();
				int arg5 = stream.readInt32();
				int arg6 = stream.readInt32();
				recvDamage(arg3, arg4, arg5, arg6);
				break;
			}
			case 125:
			{
				int arg = stream.readInt32();
				int arg2 = stream.readInt32();
				recvSkill(arg, arg2);
				break;
			}
			case 110:
			{
				uint playerTime = stream.readUint32();
				setPlayerTime(playerTime);
				break;
			}
			}
		}
		else
		{
			_ = scriptModule.idpropertys[num2].properUtype;
		}
	}

	public override void onUpdatePropertys(MemoryStream stream)
	{
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Avatar"];
		Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
		while (stream.length() != 0)
		{
			ushort num = 0;
			ushort num2 = 0;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = stream.readUint8();
				num2 = stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
				num2 = stream.readUint16();
			}
			Property property = null;
			if (num == 0)
			{
				property = idpropertys[num2];
				switch (property.properUtype)
				{
				case 22:
				{
					uint avatarType = AvatarType;
					AvatarType = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onAvatarTypeChanged(avatarType);
						}
					}
					else if (inWorld)
					{
						onAvatarTypeChanged(avatarType);
					}
					break;
				}
				case 47001:
				{
					int hP = HP;
					HP = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onHPChanged(hP);
						}
					}
					else if (inWorld)
					{
						onHPChanged(hP);
					}
					break;
				}
				case 26:
				{
					short hunger = Hunger;
					Hunger = stream.readInt16();
					if (property.isBase())
					{
						if (inited)
						{
							onHungerChanged(hunger);
						}
					}
					else if (inWorld)
					{
						onHungerChanged(hunger);
					}
					break;
				}
				case 28:
				{
					List<int> lingGeng = LingGeng;
					LingGeng = ((DATATYPE_AnonymousArray_44)EntityDef.id2datatypes[44]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onLingGengChanged(lingGeng);
						}
					}
					else if (inWorld)
					{
						onLingGengChanged(lingGeng);
					}
					break;
				}
				case 47003:
				{
					int mP = MP;
					MP = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onMPChanged(mP);
						}
					}
					else if (inWorld)
					{
						onMPChanged(mP);
					}
					break;
				}
				case 47004:
				{
					int mP_Max = MP_Max;
					MP_Max = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onMP_MaxChanged(mP_Max);
						}
					}
					else if (inWorld)
					{
						onMP_MaxChanged(mP_Max);
					}
					break;
				}
				case 14:
				{
					uint maxCard = MaxCard;
					MaxCard = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onMaxCardChanged(maxCard);
						}
					}
					else if (inWorld)
					{
						onMaxCardChanged(maxCard);
					}
					break;
				}
				case 35:
				{
					ushort survivalDays = SurvivalDays;
					SurvivalDays = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onSurvivalDaysChanged(survivalDays);
						}
					}
					else if (inWorld)
					{
						onSurvivalDaysChanged(survivalDays);
					}
					break;
				}
				case 25:
				{
					short thirst = Thirst;
					Thirst = stream.readInt16();
					if (property.isBase())
					{
						if (inited)
						{
							onThirstChanged(thirst);
						}
					}
					else if (inWorld)
					{
						onThirstChanged(thirst);
					}
					break;
				}
				case 15:
				{
					int ziZhi = ZiZhi;
					ZiZhi = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onZiZhiChanged(ziZhi);
						}
					}
					else if (inWorld)
					{
						onZiZhiChanged(ziZhi);
					}
					break;
				}
				case 47002:
				{
					int hP_Max = _HP_Max;
					_HP_Max = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							on_HP_MaxChanged(hP_Max);
						}
					}
					else if (inWorld)
					{
						on_HP_MaxChanged(hP_Max);
					}
					break;
				}
				case 16:
				{
					int dunSu = _dunSu;
					_dunSu = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							on_dunSuChanged(dunSu);
						}
					}
					else if (inWorld)
					{
						on_dunSuChanged(dunSu);
					}
					break;
				}
				case 18:
				{
					int shengShi = _shengShi;
					_shengShi = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							on_shengShiChanged(shengShi);
						}
					}
					else if (inWorld)
					{
						on_shengShiChanged(shengShi);
					}
					break;
				}
				case 21:
				{
					uint oldValue38 = age;
					age = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onAgeChanged(oldValue38);
						}
					}
					else if (inWorld)
					{
						onAgeChanged(oldValue38);
					}
					break;
				}
				case 44:
				{
					int oldValue30 = attack_Max;
					attack_Max = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onAttack_MaxChanged(oldValue30);
						}
					}
					else if (inWorld)
					{
						onAttack_MaxChanged(oldValue30);
					}
					break;
				}
				case 45:
				{
					int oldValue19 = attack_Min;
					attack_Min = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onAttack_MinChanged(oldValue19);
						}
					}
					else if (inWorld)
					{
						onAttack_MinChanged(oldValue19);
					}
					break;
				}
				case 49:
				{
					List<ushort> oldValue2 = buffs;
					buffs = ((DATATYPE_AnonymousArray_46)EntityDef.id2datatypes[46]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onBuffsChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onBuffsChanged(oldValue2);
					}
					break;
				}
				case 27:
				{
					List<int> oldValue32 = crystal;
					crystal = ((DATATYPE_AnonymousArray_43)EntityDef.id2datatypes[43]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onCrystalChanged(oldValue32);
						}
					}
					else if (inWorld)
					{
						onCrystalChanged(oldValue32);
					}
					break;
				}
				case 46:
				{
					int oldValue25 = defence;
					defence = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onDefenceChanged(oldValue25);
						}
					}
					else if (inWorld)
					{
						onDefenceChanged(oldValue25);
					}
					break;
				}
				case 53:
				{
					int oldValue15 = dexterity;
					dexterity = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onDexterityChanged(oldValue15);
						}
					}
					else if (inWorld)
					{
						onDexterityChanged(oldValue15);
					}
					break;
				}
				case 40001:
				{
					Vector3 oldValue8 = direction;
					direction = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onDirectionChanged(oldValue8);
						}
					}
					else if (inWorld)
					{
						onDirectionChanged(oldValue8);
					}
					break;
				}
				case 48:
				{
					int oldValue35 = dodge;
					dodge = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onDodgeChanged(oldValue35);
						}
					}
					else if (inWorld)
					{
						onDodgeChanged(oldValue35);
					}
					break;
				}
				case 13:
				{
					uint oldValue29 = drawCard;
					drawCard = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onDrawCardChanged(oldValue29);
						}
					}
					else if (inWorld)
					{
						onDrawCardChanged(oldValue29);
					}
					break;
				}
				case 33:
				{
					ITEM_INFO_LIST oldValue22 = equipItemList;
					equipItemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onEquipItemListChanged(oldValue22);
						}
					}
					else if (inWorld)
					{
						onEquipItemListChanged(oldValue22);
					}
					break;
				}
				case 34:
				{
					int oldValue13 = equipWeapon;
					equipWeapon = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onEquipWeaponChanged(oldValue13);
						}
					}
					else if (inWorld)
					{
						onEquipWeaponChanged(oldValue13);
					}
					break;
				}
				case 51:
				{
					ulong oldValue7 = exp;
					exp = stream.readUint64();
					if (property.isBase())
					{
						if (inited)
						{
							onExpChanged(oldValue7);
						}
					}
					else if (inWorld)
					{
						onExpChanged(oldValue7);
					}
					break;
				}
				case 47005:
				{
					int oldValue4 = forbids;
					forbids = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onForbidsChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onForbidsChanged(oldValue4);
					}
					break;
				}
				case 31:
				{
					ITEM_INFO_LIST oldValue33 = itemList;
					itemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onItemListChanged(oldValue33);
						}
					}
					else if (inWorld)
					{
						onItemListChanged(oldValue33);
					}
					break;
				}
				case 30:
				{
					ushort oldValue26 = level;
					level = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onLevelChanged(oldValue26);
						}
					}
					else if (inWorld)
					{
						onLevelChanged(oldValue26);
					}
					break;
				}
				case 32:
				{
					ushort oldValue21 = menPai;
					menPai = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onMenPaiChanged(oldValue21);
						}
					}
					else if (inWorld)
					{
						onMenPaiChanged(oldValue21);
					}
					break;
				}
				case 41006:
				{
					uint oldValue17 = modelID;
					modelID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onModelIDChanged(oldValue17);
						}
					}
					else if (inWorld)
					{
						onModelIDChanged(oldValue17);
					}
					break;
				}
				case 41007:
				{
					byte oldValue11 = modelScale;
					modelScale = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onModelScaleChanged(oldValue11);
						}
					}
					else if (inWorld)
					{
						onModelScaleChanged(oldValue11);
					}
					break;
				}
				case 50:
				{
					ulong oldValue3 = money;
					money = stream.readUint64();
					if (property.isBase())
					{
						if (inited)
						{
							onMoneyChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onMoneyChanged(oldValue3);
					}
					break;
				}
				case 39:
				{
					byte oldValue37 = moveSpeed;
					moveSpeed = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onMoveSpeedChanged(oldValue37);
						}
					}
					else if (inWorld)
					{
						onMoveSpeedChanged(oldValue37);
					}
					break;
				}
				case 41003:
				{
					string oldValue34 = name;
					name = stream.readUnicode();
					if (property.isBase())
					{
						if (inited)
						{
							onNameChanged(oldValue34);
						}
					}
					else if (inWorld)
					{
						onNameChanged(oldValue34);
					}
					break;
				}
				case 40000:
				{
					Vector3 oldValue28 = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue28);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue28);
					}
					break;
				}
				case 47:
				{
					int oldValue23 = rating;
					rating = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onRatingChanged(oldValue23);
						}
					}
					else if (inWorld)
					{
						onRatingChanged(oldValue23);
					}
					break;
				}
				case 23:
				{
					ushort oldValue18 = roleSurface;
					roleSurface = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleSurfaceChanged(oldValue18);
						}
					}
					else if (inWorld)
					{
						onRoleSurfaceChanged(oldValue18);
					}
					break;
				}
				case 24:
				{
					ushort oldValue14 = roleSurfaceCall;
					roleSurfaceCall = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleSurfaceCallChanged(oldValue14);
						}
					}
					else if (inWorld)
					{
						onRoleSurfaceCallChanged(oldValue14);
					}
					break;
				}
				case 12:
				{
					uint oldValue10 = roleType;
					roleType = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleTypeChanged(oldValue10);
						}
					}
					else if (inWorld)
					{
						onRoleTypeChanged(oldValue10);
					}
					break;
				}
				case 29:
				{
					uint oldValue5 = roleTypeCell;
					roleTypeCell = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleTypeCellChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onRoleTypeCellChanged(oldValue5);
					}
					break;
				}
				case 19:
				{
					uint oldValue39 = shaQi;
					shaQi = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onShaQiChanged(oldValue39);
						}
					}
					else if (inWorld)
					{
						onShaQiChanged(oldValue39);
					}
					break;
				}
				case 20:
				{
					uint oldValue36 = shouYuan;
					shouYuan = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onShouYuanChanged(oldValue36);
						}
					}
					else if (inWorld)
					{
						onShouYuanChanged(oldValue36);
					}
					break;
				}
				case 42:
				{
					List<int> oldValue31 = skills;
					skills = ((DATATYPE_AnonymousArray_45)EntityDef.id2datatypes[45]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onSkillsChanged(oldValue31);
						}
					}
					else if (inWorld)
					{
						onSkillsChanged(oldValue31);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				case 41001:
				{
					uint oldValue27 = spaceUType;
					spaceUType = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onSpaceUTypeChanged(oldValue27);
						}
					}
					else if (inWorld)
					{
						onSpaceUTypeChanged(oldValue27);
					}
					break;
				}
				case 54:
				{
					int oldValue24 = stamina;
					stamina = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onStaminaChanged(oldValue24);
						}
					}
					else if (inWorld)
					{
						onStaminaChanged(oldValue24);
					}
					break;
				}
				case 47006:
				{
					sbyte oldValue20 = state;
					state = stream.readInt8();
					if (property.isBase())
					{
						if (inited)
						{
							onStateChanged(oldValue20);
						}
					}
					else if (inWorld)
					{
						onStateChanged(oldValue20);
					}
					break;
				}
				case 52:
				{
					int oldValue16 = strength;
					strength = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onStrengthChanged(oldValue16);
						}
					}
					else if (inWorld)
					{
						onStrengthChanged(oldValue16);
					}
					break;
				}
				case 47007:
				{
					byte oldValue12 = subState;
					subState = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onSubStateChanged(oldValue12);
						}
					}
					else if (inWorld)
					{
						onSubStateChanged(oldValue12);
					}
					break;
				}
				case 41004:
				{
					uint oldValue9 = uid;
					uid = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUidChanged(oldValue9);
						}
					}
					else if (inWorld)
					{
						onUidChanged(oldValue9);
					}
					break;
				}
				case 41005:
				{
					uint oldValue6 = utype;
					utype = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUtypeChanged(oldValue6);
						}
					}
					else if (inWorld)
					{
						onUtypeChanged(oldValue6);
					}
					break;
				}
				case 17:
				{
					uint oldValue = wuXin;
					wuXin = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onWuXinChanged(oldValue);
						}
					}
					else if (inWorld)
					{
						onWuXinChanged(oldValue);
					}
					break;
				}
				}
				continue;
			}
			_ = idpropertys[num].properUtype;
			break;
		}
	}

	public override void callPropertysSetMethods()
	{
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Avatar"].idpropertys;
		uint avatarType = AvatarType;
		Property property = idpropertys[4];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onAvatarTypeChanged(avatarType);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onAvatarTypeChanged(avatarType);
		}
		int hP = HP;
		Property property2 = idpropertys[5];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onHPChanged(hP);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onHPChanged(hP);
		}
		short hunger = Hunger;
		Property property3 = idpropertys[6];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onHungerChanged(hunger);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onHungerChanged(hunger);
		}
		List<int> lingGeng = LingGeng;
		Property property4 = idpropertys[7];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onLingGengChanged(lingGeng);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onLingGengChanged(lingGeng);
		}
		int mP = MP;
		Property property5 = idpropertys[8];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onMPChanged(mP);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onMPChanged(mP);
		}
		int mP_Max = MP_Max;
		Property property6 = idpropertys[9];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onMP_MaxChanged(mP_Max);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onMP_MaxChanged(mP_Max);
		}
		uint maxCard = MaxCard;
		Property property7 = idpropertys[10];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onMaxCardChanged(maxCard);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onMaxCardChanged(maxCard);
		}
		ushort survivalDays = SurvivalDays;
		Property property8 = idpropertys[11];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onSurvivalDaysChanged(survivalDays);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onSurvivalDaysChanged(survivalDays);
		}
		short thirst = Thirst;
		Property property9 = idpropertys[12];
		if (property9.isBase())
		{
			if (inited && !inWorld)
			{
				onThirstChanged(thirst);
			}
		}
		else if (inWorld && (!property9.isOwnerOnly() || isPlayer()))
		{
			onThirstChanged(thirst);
		}
		int ziZhi = ZiZhi;
		Property property10 = idpropertys[13];
		if (property10.isBase())
		{
			if (inited && !inWorld)
			{
				onZiZhiChanged(ziZhi);
			}
		}
		else if (inWorld && (!property10.isOwnerOnly() || isPlayer()))
		{
			onZiZhiChanged(ziZhi);
		}
		int hP_Max = _HP_Max;
		Property property11 = idpropertys[14];
		if (property11.isBase())
		{
			if (inited && !inWorld)
			{
				on_HP_MaxChanged(hP_Max);
			}
		}
		else if (inWorld && (!property11.isOwnerOnly() || isPlayer()))
		{
			on_HP_MaxChanged(hP_Max);
		}
		int dunSu = _dunSu;
		Property property12 = idpropertys[15];
		if (property12.isBase())
		{
			if (inited && !inWorld)
			{
				on_dunSuChanged(dunSu);
			}
		}
		else if (inWorld && (!property12.isOwnerOnly() || isPlayer()))
		{
			on_dunSuChanged(dunSu);
		}
		int shengShi = _shengShi;
		Property property13 = idpropertys[16];
		if (property13.isBase())
		{
			if (inited && !inWorld)
			{
				on_shengShiChanged(shengShi);
			}
		}
		else if (inWorld && (!property13.isOwnerOnly() || isPlayer()))
		{
			on_shengShiChanged(shengShi);
		}
		uint oldValue = age;
		Property property14 = idpropertys[17];
		if (property14.isBase())
		{
			if (inited && !inWorld)
			{
				onAgeChanged(oldValue);
			}
		}
		else if (inWorld && (!property14.isOwnerOnly() || isPlayer()))
		{
			onAgeChanged(oldValue);
		}
		int oldValue2 = attack_Max;
		Property property15 = idpropertys[18];
		if (property15.isBase())
		{
			if (inited && !inWorld)
			{
				onAttack_MaxChanged(oldValue2);
			}
		}
		else if (inWorld && (!property15.isOwnerOnly() || isPlayer()))
		{
			onAttack_MaxChanged(oldValue2);
		}
		int oldValue3 = attack_Min;
		Property property16 = idpropertys[19];
		if (property16.isBase())
		{
			if (inited && !inWorld)
			{
				onAttack_MinChanged(oldValue3);
			}
		}
		else if (inWorld && (!property16.isOwnerOnly() || isPlayer()))
		{
			onAttack_MinChanged(oldValue3);
		}
		List<ushort> oldValue4 = buffs;
		Property property17 = idpropertys[20];
		if (property17.isBase())
		{
			if (inited && !inWorld)
			{
				onBuffsChanged(oldValue4);
			}
		}
		else if (inWorld && (!property17.isOwnerOnly() || isPlayer()))
		{
			onBuffsChanged(oldValue4);
		}
		List<int> oldValue5 = crystal;
		Property property18 = idpropertys[21];
		if (property18.isBase())
		{
			if (inited && !inWorld)
			{
				onCrystalChanged(oldValue5);
			}
		}
		else if (inWorld && (!property18.isOwnerOnly() || isPlayer()))
		{
			onCrystalChanged(oldValue5);
		}
		int oldValue6 = defence;
		Property property19 = idpropertys[22];
		if (property19.isBase())
		{
			if (inited && !inWorld)
			{
				onDefenceChanged(oldValue6);
			}
		}
		else if (inWorld && (!property19.isOwnerOnly() || isPlayer()))
		{
			onDefenceChanged(oldValue6);
		}
		int oldValue7 = dexterity;
		Property property20 = idpropertys[23];
		if (property20.isBase())
		{
			if (inited && !inWorld)
			{
				onDexterityChanged(oldValue7);
			}
		}
		else if (inWorld && (!property20.isOwnerOnly() || isPlayer()))
		{
			onDexterityChanged(oldValue7);
		}
		Vector3 oldValue8 = direction;
		Property property21 = idpropertys[2];
		if (property21.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue8);
			}
		}
		else if (inWorld && (!property21.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue8);
		}
		int oldValue9 = dodge;
		Property property22 = idpropertys[24];
		if (property22.isBase())
		{
			if (inited && !inWorld)
			{
				onDodgeChanged(oldValue9);
			}
		}
		else if (inWorld && (!property22.isOwnerOnly() || isPlayer()))
		{
			onDodgeChanged(oldValue9);
		}
		uint oldValue10 = drawCard;
		Property property23 = idpropertys[25];
		if (property23.isBase())
		{
			if (inited && !inWorld)
			{
				onDrawCardChanged(oldValue10);
			}
		}
		else if (inWorld && (!property23.isOwnerOnly() || isPlayer()))
		{
			onDrawCardChanged(oldValue10);
		}
		ITEM_INFO_LIST oldValue11 = equipItemList;
		Property property24 = idpropertys[26];
		if (property24.isBase())
		{
			if (inited && !inWorld)
			{
				onEquipItemListChanged(oldValue11);
			}
		}
		else if (inWorld && (!property24.isOwnerOnly() || isPlayer()))
		{
			onEquipItemListChanged(oldValue11);
		}
		int oldValue12 = equipWeapon;
		Property property25 = idpropertys[27];
		if (property25.isBase())
		{
			if (inited && !inWorld)
			{
				onEquipWeaponChanged(oldValue12);
			}
		}
		else if (inWorld && (!property25.isOwnerOnly() || isPlayer()))
		{
			onEquipWeaponChanged(oldValue12);
		}
		ulong oldValue13 = exp;
		Property property26 = idpropertys[28];
		if (property26.isBase())
		{
			if (inited && !inWorld)
			{
				onExpChanged(oldValue13);
			}
		}
		else if (inWorld && (!property26.isOwnerOnly() || isPlayer()))
		{
			onExpChanged(oldValue13);
		}
		int oldValue14 = forbids;
		Property property27 = idpropertys[29];
		if (property27.isBase())
		{
			if (inited && !inWorld)
			{
				onForbidsChanged(oldValue14);
			}
		}
		else if (inWorld && (!property27.isOwnerOnly() || isPlayer()))
		{
			onForbidsChanged(oldValue14);
		}
		ITEM_INFO_LIST oldValue15 = itemList;
		Property property28 = idpropertys[30];
		if (property28.isBase())
		{
			if (inited && !inWorld)
			{
				onItemListChanged(oldValue15);
			}
		}
		else if (inWorld && (!property28.isOwnerOnly() || isPlayer()))
		{
			onItemListChanged(oldValue15);
		}
		ushort oldValue16 = level;
		Property property29 = idpropertys[31];
		if (property29.isBase())
		{
			if (inited && !inWorld)
			{
				onLevelChanged(oldValue16);
			}
		}
		else if (inWorld && (!property29.isOwnerOnly() || isPlayer()))
		{
			onLevelChanged(oldValue16);
		}
		ushort oldValue17 = menPai;
		Property property30 = idpropertys[32];
		if (property30.isBase())
		{
			if (inited && !inWorld)
			{
				onMenPaiChanged(oldValue17);
			}
		}
		else if (inWorld && (!property30.isOwnerOnly() || isPlayer()))
		{
			onMenPaiChanged(oldValue17);
		}
		uint oldValue18 = modelID;
		Property property31 = idpropertys[33];
		if (property31.isBase())
		{
			if (inited && !inWorld)
			{
				onModelIDChanged(oldValue18);
			}
		}
		else if (inWorld && (!property31.isOwnerOnly() || isPlayer()))
		{
			onModelIDChanged(oldValue18);
		}
		byte oldValue19 = modelScale;
		Property property32 = idpropertys[34];
		if (property32.isBase())
		{
			if (inited && !inWorld)
			{
				onModelScaleChanged(oldValue19);
			}
		}
		else if (inWorld && (!property32.isOwnerOnly() || isPlayer()))
		{
			onModelScaleChanged(oldValue19);
		}
		ulong oldValue20 = money;
		Property property33 = idpropertys[35];
		if (property33.isBase())
		{
			if (inited && !inWorld)
			{
				onMoneyChanged(oldValue20);
			}
		}
		else if (inWorld && (!property33.isOwnerOnly() || isPlayer()))
		{
			onMoneyChanged(oldValue20);
		}
		byte oldValue21 = moveSpeed;
		Property property34 = idpropertys[36];
		if (property34.isBase())
		{
			if (inited && !inWorld)
			{
				onMoveSpeedChanged(oldValue21);
			}
		}
		else if (inWorld && (!property34.isOwnerOnly() || isPlayer()))
		{
			onMoveSpeedChanged(oldValue21);
		}
		string oldValue22 = name;
		Property property35 = idpropertys[37];
		if (property35.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue22);
			}
		}
		else if (inWorld && (!property35.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue22);
		}
		Vector3 oldValue23 = position;
		Property property36 = idpropertys[1];
		if (property36.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue23);
			}
		}
		else if (inWorld && (!property36.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue23);
		}
		int oldValue24 = rating;
		Property property37 = idpropertys[38];
		if (property37.isBase())
		{
			if (inited && !inWorld)
			{
				onRatingChanged(oldValue24);
			}
		}
		else if (inWorld && (!property37.isOwnerOnly() || isPlayer()))
		{
			onRatingChanged(oldValue24);
		}
		ushort oldValue25 = roleSurface;
		Property property38 = idpropertys[39];
		if (property38.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleSurfaceChanged(oldValue25);
			}
		}
		else if (inWorld && (!property38.isOwnerOnly() || isPlayer()))
		{
			onRoleSurfaceChanged(oldValue25);
		}
		ushort oldValue26 = roleSurfaceCall;
		Property property39 = idpropertys[40];
		if (property39.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleSurfaceCallChanged(oldValue26);
			}
		}
		else if (inWorld && (!property39.isOwnerOnly() || isPlayer()))
		{
			onRoleSurfaceCallChanged(oldValue26);
		}
		uint oldValue27 = roleType;
		Property property40 = idpropertys[41];
		if (property40.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleTypeChanged(oldValue27);
			}
		}
		else if (inWorld && (!property40.isOwnerOnly() || isPlayer()))
		{
			onRoleTypeChanged(oldValue27);
		}
		uint oldValue28 = roleTypeCell;
		Property property41 = idpropertys[42];
		if (property41.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleTypeCellChanged(oldValue28);
			}
		}
		else if (inWorld && (!property41.isOwnerOnly() || isPlayer()))
		{
			onRoleTypeCellChanged(oldValue28);
		}
		uint oldValue29 = shaQi;
		Property property42 = idpropertys[43];
		if (property42.isBase())
		{
			if (inited && !inWorld)
			{
				onShaQiChanged(oldValue29);
			}
		}
		else if (inWorld && (!property42.isOwnerOnly() || isPlayer()))
		{
			onShaQiChanged(oldValue29);
		}
		uint oldValue30 = shouYuan;
		Property property43 = idpropertys[44];
		if (property43.isBase())
		{
			if (inited && !inWorld)
			{
				onShouYuanChanged(oldValue30);
			}
		}
		else if (inWorld && (!property43.isOwnerOnly() || isPlayer()))
		{
			onShouYuanChanged(oldValue30);
		}
		List<int> oldValue31 = skills;
		Property property44 = idpropertys[45];
		if (property44.isBase())
		{
			if (inited && !inWorld)
			{
				onSkillsChanged(oldValue31);
			}
		}
		else if (inWorld && (!property44.isOwnerOnly() || isPlayer()))
		{
			onSkillsChanged(oldValue31);
		}
		uint oldValue32 = spaceUType;
		Property property45 = idpropertys[46];
		if (property45.isBase())
		{
			if (inited && !inWorld)
			{
				onSpaceUTypeChanged(oldValue32);
			}
		}
		else if (inWorld && (!property45.isOwnerOnly() || isPlayer()))
		{
			onSpaceUTypeChanged(oldValue32);
		}
		int oldValue33 = stamina;
		Property property46 = idpropertys[47];
		if (property46.isBase())
		{
			if (inited && !inWorld)
			{
				onStaminaChanged(oldValue33);
			}
		}
		else if (inWorld && (!property46.isOwnerOnly() || isPlayer()))
		{
			onStaminaChanged(oldValue33);
		}
		sbyte oldValue34 = state;
		Property property47 = idpropertys[48];
		if (property47.isBase())
		{
			if (inited && !inWorld)
			{
				onStateChanged(oldValue34);
			}
		}
		else if (inWorld && (!property47.isOwnerOnly() || isPlayer()))
		{
			onStateChanged(oldValue34);
		}
		int oldValue35 = strength;
		Property property48 = idpropertys[49];
		if (property48.isBase())
		{
			if (inited && !inWorld)
			{
				onStrengthChanged(oldValue35);
			}
		}
		else if (inWorld && (!property48.isOwnerOnly() || isPlayer()))
		{
			onStrengthChanged(oldValue35);
		}
		byte oldValue36 = subState;
		Property property49 = idpropertys[50];
		if (property49.isBase())
		{
			if (inited && !inWorld)
			{
				onSubStateChanged(oldValue36);
			}
		}
		else if (inWorld && (!property49.isOwnerOnly() || isPlayer()))
		{
			onSubStateChanged(oldValue36);
		}
		uint oldValue37 = uid;
		Property property50 = idpropertys[51];
		if (property50.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue37);
			}
		}
		else if (inWorld && (!property50.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue37);
		}
		uint oldValue38 = utype;
		Property property51 = idpropertys[52];
		if (property51.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue38);
			}
		}
		else if (inWorld && (!property51.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue38);
		}
		uint oldValue39 = wuXin;
		Property property52 = idpropertys[53];
		if (property52.isBase())
		{
			if (inited && !inWorld)
			{
				onWuXinChanged(oldValue39);
			}
		}
		else if (inWorld && (!property52.isOwnerOnly() || isPlayer()))
		{
			onWuXinChanged(oldValue39);
		}
	}
}
