using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class CDKBase : Entity
{
	public EntityBaseEntityCall_CDKBase baseEntityCall;

	public EntityCellEntityCall_CDKBase cellEntityCall;

	public CDKBase()
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
		baseEntityCall = new EntityBaseEntityCall_CDKBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_CDKBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
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
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
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
				case 40000:
				{
					Vector3 oldValue = position;
					position = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onPositionChanged(oldValue);
						}
					}
					else if (inWorld)
					{
						onPositionChanged(oldValue);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
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
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["CDK"].idpropertys;
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
		Vector3 oldValue2 = position;
		Property property2 = idpropertys[1];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue2);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue2);
		}
	}
}
