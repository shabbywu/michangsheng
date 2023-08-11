using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class SpacesBase : Entity
{
	public EntityBaseEntityCall_SpacesBase baseEntityCall;

	public EntityCellEntityCall_SpacesBase cellEntityCall;

	public uint modelID;

	public byte modelScale = 30;

	public string name = "";

	public uint uid;

	public uint utype;

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

	public SpacesBase()
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
		baseEntityCall = new EntityBaseEntityCall_SpacesBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_SpacesBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
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
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
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
					byte oldValue3 = modelScale;
					modelScale = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onModelScaleChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onModelScaleChanged(oldValue3);
					}
					break;
				}
				case 41003:
				{
					string oldValue6 = name;
					name = stream.readUnicode();
					if (property.isBase())
					{
						if (inited)
						{
							onNameChanged(oldValue6);
						}
					}
					else if (inWorld)
					{
						onNameChanged(oldValue6);
					}
					break;
				}
				case 40000:
				{
					Vector3 oldValue4 = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue4);
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Spaces"].idpropertys;
		Vector3 oldValue = direction;
		Property property = idpropertys[2];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue);
		}
		uint oldValue2 = modelID;
		Property property2 = idpropertys[4];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onModelIDChanged(oldValue2);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onModelIDChanged(oldValue2);
		}
		byte oldValue3 = modelScale;
		Property property3 = idpropertys[5];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onModelScaleChanged(oldValue3);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onModelScaleChanged(oldValue3);
		}
		string oldValue4 = name;
		Property property4 = idpropertys[6];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue4);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue4);
		}
		Vector3 oldValue5 = position;
		Property property5 = idpropertys[1];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue5);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue5);
		}
		uint oldValue6 = uid;
		Property property6 = idpropertys[7];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue6);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue6);
		}
		uint oldValue7 = utype;
		Property property7 = idpropertys[8];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue7);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue7);
		}
	}
}
