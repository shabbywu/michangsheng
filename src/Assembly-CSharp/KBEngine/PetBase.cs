using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class PetBase : Entity
{
	public EntityBaseEntityCall_PetBase baseEntityCall;

	public EntityCellEntityCall_PetBase cellEntityCall;

	public int HP;

	public int MP;

	public int MP_Max;

	public int _HP_Max;

	public int attack_Max = 10;

	public int attack_Min;

	public List<ushort> buffs = new List<ushort>();

	public int defence;

	public uint dialogID;

	public int dodge;

	public uint entityNO;

	public int forbids;

	public uint modelID;

	public byte modelScale = 30;

	public byte moveSpeed = 50;

	public string name = "";

	public int rating = 99;

	public ushort roleSurfaceCall = 1;

	public uint roleTypeCell;

	public sbyte state;

	public byte subState;

	public uint uid;

	public uint utype;

	public virtual void onHPChanged(int oldValue)
	{
	}

	public virtual void onMPChanged(int oldValue)
	{
	}

	public virtual void onMP_MaxChanged(int oldValue)
	{
	}

	public virtual void on_HP_MaxChanged(int oldValue)
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

	public virtual void onDefenceChanged(int oldValue)
	{
	}

	public virtual void onDialogIDChanged(uint oldValue)
	{
	}

	public virtual void onDodgeChanged(int oldValue)
	{
	}

	public virtual void onEntityNOChanged(uint oldValue)
	{
	}

	public virtual void onForbidsChanged(int oldValue)
	{
	}

	public virtual void onModelIDChanged(uint oldValue)
	{
	}

	public virtual void onModelScaleChanged(byte oldValue)
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

	public virtual void onRoleSurfaceCallChanged(ushort oldValue)
	{
	}

	public virtual void onRoleTypeCellChanged(uint oldValue)
	{
	}

	public virtual void onStateChanged(sbyte oldValue)
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

	public abstract void recvDamage(int arg1, int arg2, int arg3, int arg4);

	public abstract void recvSkill(int arg1, int arg2);

	public PetBase()
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
		baseEntityCall = new EntityBaseEntityCall_PetBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_PetBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["Pet"];
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
			case 161:
			{
				int arg3 = stream.readInt32();
				int arg4 = stream.readInt32();
				int arg5 = stream.readInt32();
				int arg6 = stream.readInt32();
				recvDamage(arg3, arg4, arg5, arg6);
				break;
			}
			case 162:
			{
				int arg = stream.readInt32();
				int arg2 = stream.readInt32();
				recvSkill(arg, arg2);
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
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Pet"];
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
				case 115:
				{
					int oldValue12 = attack_Max;
					attack_Max = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onAttack_MaxChanged(oldValue12);
						}
					}
					else if (inWorld)
					{
						onAttack_MaxChanged(oldValue12);
					}
					break;
				}
				case 116:
				{
					int oldValue13 = attack_Min;
					attack_Min = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onAttack_MinChanged(oldValue13);
						}
					}
					else if (inWorld)
					{
						onAttack_MinChanged(oldValue13);
					}
					break;
				}
				case 120:
				{
					List<ushort> oldValue20 = buffs;
					buffs = ((DATATYPE_AnonymousArray_50)EntityDef.id2datatypes[50]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onBuffsChanged(oldValue20);
						}
					}
					else if (inWorld)
					{
						onBuffsChanged(oldValue20);
					}
					break;
				}
				case 117:
				{
					int oldValue6 = defence;
					defence = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onDefenceChanged(oldValue6);
						}
					}
					else if (inWorld)
					{
						onDefenceChanged(oldValue6);
					}
					break;
				}
				case 110:
				{
					uint oldValue16 = dialogID;
					dialogID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onDialogIDChanged(oldValue16);
						}
					}
					else if (inWorld)
					{
						onDialogIDChanged(oldValue16);
					}
					break;
				}
				case 40001:
				{
					Vector3 oldValue5 = direction;
					direction = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onDirectionChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onDirectionChanged(oldValue5);
					}
					break;
				}
				case 119:
				{
					int oldValue19 = dodge;
					dodge = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onDodgeChanged(oldValue19);
						}
					}
					else if (inWorld)
					{
						onDodgeChanged(oldValue19);
					}
					break;
				}
				case 51007:
				{
					uint oldValue8 = entityNO;
					entityNO = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onEntityNOChanged(oldValue8);
						}
					}
					else if (inWorld)
					{
						onEntityNOChanged(oldValue8);
					}
					break;
				}
				case 47005:
				{
					int oldValue21 = forbids;
					forbids = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onForbidsChanged(oldValue21);
						}
					}
					else if (inWorld)
					{
						onForbidsChanged(oldValue21);
					}
					break;
				}
				case 41006:
				{
					uint oldValue15 = modelID;
					modelID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onModelIDChanged(oldValue15);
						}
					}
					else if (inWorld)
					{
						onModelIDChanged(oldValue15);
					}
					break;
				}
				case 41007:
				{
					byte oldValue10 = modelScale;
					modelScale = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onModelScaleChanged(oldValue10);
						}
					}
					else if (inWorld)
					{
						onModelScaleChanged(oldValue10);
					}
					break;
				}
				case 111:
				{
					byte oldValue3 = moveSpeed;
					moveSpeed = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onMoveSpeedChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onMoveSpeedChanged(oldValue3);
					}
					break;
				}
				case 41003:
				{
					string oldValue17 = name;
					name = stream.readUnicode();
					if (property.isBase())
					{
						if (inited)
						{
							onNameChanged(oldValue17);
						}
					}
					else if (inWorld)
					{
						onNameChanged(oldValue17);
					}
					break;
				}
				case 40000:
				{
					Vector3 oldValue11 = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue11);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue11);
					}
					break;
				}
				case 118:
				{
					int oldValue7 = rating;
					rating = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onRatingChanged(oldValue7);
						}
					}
					else if (inWorld)
					{
						onRatingChanged(oldValue7);
					}
					break;
				}
				case 104:
				{
					ushort oldValue2 = roleSurfaceCall;
					roleSurfaceCall = stream.readUint16();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleSurfaceCallChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onRoleSurfaceCallChanged(oldValue2);
					}
					break;
				}
				case 103:
				{
					uint oldValue18 = roleTypeCell;
					roleTypeCell = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onRoleTypeCellChanged(oldValue18);
						}
					}
					else if (inWorld)
					{
						onRoleTypeCellChanged(oldValue18);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				case 47006:
				{
					sbyte oldValue14 = state;
					state = stream.readInt8();
					if (property.isBase())
					{
						if (inited)
						{
							onStateChanged(oldValue14);
						}
					}
					else if (inWorld)
					{
						onStateChanged(oldValue14);
					}
					break;
				}
				case 47007:
				{
					byte oldValue9 = subState;
					subState = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onSubStateChanged(oldValue9);
						}
					}
					else if (inWorld)
					{
						onSubStateChanged(oldValue9);
					}
					break;
				}
				case 41004:
				{
					uint oldValue4 = uid;
					uid = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUidChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onUidChanged(oldValue4);
					}
					break;
				}
				case 41005:
				{
					uint oldValue = utype;
					utype = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUtypeChanged(oldValue);
						}
					}
					else if (inWorld)
					{
						onUtypeChanged(oldValue);
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
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Pet"].idpropertys;
		int hP = HP;
		Property property = idpropertys[4];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onHPChanged(hP);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onHPChanged(hP);
		}
		int mP = MP;
		Property property2 = idpropertys[5];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onMPChanged(mP);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onMPChanged(mP);
		}
		int mP_Max = MP_Max;
		Property property3 = idpropertys[6];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onMP_MaxChanged(mP_Max);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onMP_MaxChanged(mP_Max);
		}
		int hP_Max = _HP_Max;
		Property property4 = idpropertys[7];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				on_HP_MaxChanged(hP_Max);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			on_HP_MaxChanged(hP_Max);
		}
		int oldValue = attack_Max;
		Property property5 = idpropertys[8];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onAttack_MaxChanged(oldValue);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onAttack_MaxChanged(oldValue);
		}
		int oldValue2 = attack_Min;
		Property property6 = idpropertys[9];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onAttack_MinChanged(oldValue2);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onAttack_MinChanged(oldValue2);
		}
		List<ushort> oldValue3 = buffs;
		Property property7 = idpropertys[10];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onBuffsChanged(oldValue3);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onBuffsChanged(oldValue3);
		}
		int oldValue4 = defence;
		Property property8 = idpropertys[11];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onDefenceChanged(oldValue4);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onDefenceChanged(oldValue4);
		}
		uint oldValue5 = dialogID;
		Property property9 = idpropertys[12];
		if (property9.isBase())
		{
			if (inited && !inWorld)
			{
				onDialogIDChanged(oldValue5);
			}
		}
		else if (inWorld && (!property9.isOwnerOnly() || isPlayer()))
		{
			onDialogIDChanged(oldValue5);
		}
		Vector3 oldValue6 = direction;
		Property property10 = idpropertys[2];
		if (property10.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue6);
			}
		}
		else if (inWorld && (!property10.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue6);
		}
		int oldValue7 = dodge;
		Property property11 = idpropertys[13];
		if (property11.isBase())
		{
			if (inited && !inWorld)
			{
				onDodgeChanged(oldValue7);
			}
		}
		else if (inWorld && (!property11.isOwnerOnly() || isPlayer()))
		{
			onDodgeChanged(oldValue7);
		}
		uint oldValue8 = entityNO;
		Property property12 = idpropertys[14];
		if (property12.isBase())
		{
			if (inited && !inWorld)
			{
				onEntityNOChanged(oldValue8);
			}
		}
		else if (inWorld && (!property12.isOwnerOnly() || isPlayer()))
		{
			onEntityNOChanged(oldValue8);
		}
		int oldValue9 = forbids;
		Property property13 = idpropertys[15];
		if (property13.isBase())
		{
			if (inited && !inWorld)
			{
				onForbidsChanged(oldValue9);
			}
		}
		else if (inWorld && (!property13.isOwnerOnly() || isPlayer()))
		{
			onForbidsChanged(oldValue9);
		}
		uint oldValue10 = modelID;
		Property property14 = idpropertys[16];
		if (property14.isBase())
		{
			if (inited && !inWorld)
			{
				onModelIDChanged(oldValue10);
			}
		}
		else if (inWorld && (!property14.isOwnerOnly() || isPlayer()))
		{
			onModelIDChanged(oldValue10);
		}
		byte oldValue11 = modelScale;
		Property property15 = idpropertys[17];
		if (property15.isBase())
		{
			if (inited && !inWorld)
			{
				onModelScaleChanged(oldValue11);
			}
		}
		else if (inWorld && (!property15.isOwnerOnly() || isPlayer()))
		{
			onModelScaleChanged(oldValue11);
		}
		byte oldValue12 = moveSpeed;
		Property property16 = idpropertys[18];
		if (property16.isBase())
		{
			if (inited && !inWorld)
			{
				onMoveSpeedChanged(oldValue12);
			}
		}
		else if (inWorld && (!property16.isOwnerOnly() || isPlayer()))
		{
			onMoveSpeedChanged(oldValue12);
		}
		string oldValue13 = name;
		Property property17 = idpropertys[19];
		if (property17.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue13);
			}
		}
		else if (inWorld && (!property17.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue13);
		}
		Vector3 oldValue14 = position;
		Property property18 = idpropertys[1];
		if (property18.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue14);
			}
		}
		else if (inWorld && (!property18.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue14);
		}
		int oldValue15 = rating;
		Property property19 = idpropertys[20];
		if (property19.isBase())
		{
			if (inited && !inWorld)
			{
				onRatingChanged(oldValue15);
			}
		}
		else if (inWorld && (!property19.isOwnerOnly() || isPlayer()))
		{
			onRatingChanged(oldValue15);
		}
		ushort oldValue16 = roleSurfaceCall;
		Property property20 = idpropertys[21];
		if (property20.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleSurfaceCallChanged(oldValue16);
			}
		}
		else if (inWorld && (!property20.isOwnerOnly() || isPlayer()))
		{
			onRoleSurfaceCallChanged(oldValue16);
		}
		uint oldValue17 = roleTypeCell;
		Property property21 = idpropertys[22];
		if (property21.isBase())
		{
			if (inited && !inWorld)
			{
				onRoleTypeCellChanged(oldValue17);
			}
		}
		else if (inWorld && (!property21.isOwnerOnly() || isPlayer()))
		{
			onRoleTypeCellChanged(oldValue17);
		}
		sbyte oldValue18 = state;
		Property property22 = idpropertys[23];
		if (property22.isBase())
		{
			if (inited && !inWorld)
			{
				onStateChanged(oldValue18);
			}
		}
		else if (inWorld && (!property22.isOwnerOnly() || isPlayer()))
		{
			onStateChanged(oldValue18);
		}
		byte oldValue19 = subState;
		Property property23 = idpropertys[24];
		if (property23.isBase())
		{
			if (inited && !inWorld)
			{
				onSubStateChanged(oldValue19);
			}
		}
		else if (inWorld && (!property23.isOwnerOnly() || isPlayer()))
		{
			onSubStateChanged(oldValue19);
		}
		uint oldValue20 = uid;
		Property property24 = idpropertys[25];
		if (property24.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue20);
			}
		}
		else if (inWorld && (!property24.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue20);
		}
		uint oldValue21 = utype;
		Property property25 = idpropertys[26];
		if (property25.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue21);
			}
		}
		else if (inWorld && (!property25.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue21);
		}
	}
}
