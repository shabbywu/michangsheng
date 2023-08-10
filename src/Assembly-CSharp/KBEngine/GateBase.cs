using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class GateBase : Entity
{
	public EntityBaseEntityCall_GateBase baseEntityCall;

	public EntityCellEntityCall_GateBase cellEntityCall;

	public uint dialogID;

	public uint entityNO;

	public uint modelID;

	public byte modelScale = 30;

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

	public virtual void onNameChanged(string oldValue)
	{
	}

	public virtual void onUidChanged(uint oldValue)
	{
	}

	public virtual void onUtypeChanged(uint oldValue)
	{
	}

	public GateBase()
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
		baseEntityCall = new EntityBaseEntityCall_GateBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_GateBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
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
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Gate"];
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
				case 89:
				{
					uint oldValue6 = dialogID;
					dialogID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onDialogIDChanged(oldValue6);
						}
					}
					else if (inWorld)
					{
						onDialogIDChanged(oldValue6);
					}
					break;
				}
				case 40001:
				{
					Vector3 oldValue2 = direction;
					direction = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onDirectionChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onDirectionChanged(oldValue2);
					}
					break;
				}
				case 51007:
				{
					uint oldValue4 = entityNO;
					entityNO = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onEntityNOChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onEntityNOChanged(oldValue4);
					}
					break;
				}
				case 41006:
				{
					uint oldValue9 = modelID;
					modelID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onModelIDChanged(oldValue9);
						}
					}
					else if (inWorld)
					{
						onModelIDChanged(oldValue9);
					}
					break;
				}
				case 41007:
				{
					byte oldValue8 = modelScale;
					modelScale = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onModelScaleChanged(oldValue8);
						}
					}
					else if (inWorld)
					{
						onModelScaleChanged(oldValue8);
					}
					break;
				}
				case 41003:
				{
					string oldValue7 = name;
					name = stream.readUnicode();
					if (property.isBase())
					{
						if (inited)
						{
							onNameChanged(oldValue7);
						}
					}
					else if (inWorld)
					{
						onNameChanged(oldValue7);
					}
					break;
				}
				case 40000:
				{
					Vector3 oldValue3 = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue3);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				case 41004:
				{
					uint oldValue5 = uid;
					uid = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onUidChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onUidChanged(oldValue5);
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
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Gate"].idpropertys;
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
		string oldValue6 = name;
		Property property6 = idpropertys[8];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue6);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue6);
		}
		Vector3 oldValue7 = position;
		Property property7 = idpropertys[1];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue7);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue7);
		}
		uint oldValue8 = uid;
		Property property8 = idpropertys[9];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue8);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue8);
		}
		uint oldValue9 = utype;
		Property property9 = idpropertys[10];
		if (property9.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue9);
			}
		}
		else if (inWorld && (!property9.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue9);
		}
	}
}
