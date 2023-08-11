using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class NPCBase : Entity
{
	public EntityBaseEntityCall_NPCBase baseEntityCall;

	public EntityCellEntityCall_NPCBase cellEntityCall;

	public uint dialogID;

	public uint entityNO;

	public uint modelID;

	public byte modelScale = 30;

	public byte moveSpeed = 50;

	public string name = "";

	public uint uid;

	public uint utype;

	public virtual void onDialogIDChanged(uint oldValue)
	{
	}

	public virtual void onEntityNOChanged(uint oldValue)
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

	public virtual void onUidChanged(uint oldValue)
	{
	}

	public virtual void onUtypeChanged(uint oldValue)
	{
	}

	public NPCBase()
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
		baseEntityCall = new EntityBaseEntityCall_NPCBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_NPCBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["NPC"];
		ushort num = 0;
		ushort num2 = 0;
		num2 = ((!scriptModule.usePropertyDescrAlias) ? stream.readUint16() : stream.readUint8());
		num = ((!scriptModule.useMethodDescrAlias) ? stream.readUint16() : stream.readUint8());
		Method method = null;
		if (num2 == 0)
		{
			method = scriptModule.idmethods[num];
			_ = method.methodUtype;
		}
		else
		{
			_ = scriptModule.idpropertys[num2].properUtype;
		}
	}

	public override void onUpdatePropertys(MemoryStream stream)
	{
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["NPC"];
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
				case 81:
				{
					uint oldValue4 = dialogID;
					dialogID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onDialogIDChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onDialogIDChanged(oldValue4);
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
				case 51007:
				{
					uint oldValue10 = entityNO;
					entityNO = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onEntityNOChanged(oldValue10);
						}
					}
					else if (inWorld)
					{
						onEntityNOChanged(oldValue10);
					}
					break;
				}
				case 41006:
				{
					uint oldValue7 = modelID;
					modelID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onModelIDChanged(oldValue7);
						}
					}
					else if (inWorld)
					{
						onModelIDChanged(oldValue7);
					}
					break;
				}
				case 41007:
				{
					byte oldValue5 = modelScale;
					modelScale = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onModelScaleChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onModelScaleChanged(oldValue5);
					}
					break;
				}
				case 82:
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
					string oldValue9 = name;
					name = stream.readUnicode();
					if (property.isBase())
					{
						if (inited)
						{
							onNameChanged(oldValue9);
						}
					}
					else if (inWorld)
					{
						onNameChanged(oldValue9);
					}
					break;
				}
				case 40000:
				{
					Vector3 oldValue2 = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue2);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				case 41004:
				{
					uint oldValue6 = uid;
					uid = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUidChanged(oldValue6);
						}
					}
					else if (inWorld)
					{
						onUidChanged(oldValue6);
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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["NPC"].idpropertys;
		uint oldValue = dialogID;
		Property property = idpropertys[4];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onDialogIDChanged(oldValue);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onDialogIDChanged(oldValue);
		}
		Vector3 oldValue2 = direction;
		Property property2 = idpropertys[2];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue2);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue2);
		}
		uint oldValue3 = entityNO;
		Property property3 = idpropertys[5];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onEntityNOChanged(oldValue3);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onEntityNOChanged(oldValue3);
		}
		uint oldValue4 = modelID;
		Property property4 = idpropertys[6];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onModelIDChanged(oldValue4);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onModelIDChanged(oldValue4);
		}
		byte oldValue5 = modelScale;
		Property property5 = idpropertys[7];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onModelScaleChanged(oldValue5);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onModelScaleChanged(oldValue5);
		}
		byte oldValue6 = moveSpeed;
		Property property6 = idpropertys[8];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onMoveSpeedChanged(oldValue6);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onMoveSpeedChanged(oldValue6);
		}
		string oldValue7 = name;
		Property property7 = idpropertys[9];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue7);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue7);
		}
		Vector3 oldValue8 = position;
		Property property8 = idpropertys[1];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue8);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue8);
		}
		uint oldValue9 = uid;
		Property property9 = idpropertys[10];
		if (property9.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue9);
			}
		}
		else if (inWorld && (!property9.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue9);
		}
		uint oldValue10 = utype;
		Property property10 = idpropertys[11];
		if (property10.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue10);
			}
		}
		else if (inWorld && (!property10.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue10);
		}
	}
}
