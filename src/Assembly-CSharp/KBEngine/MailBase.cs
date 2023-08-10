using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class MailBase : Entity
{
	public EntityBaseEntityCall_MailBase baseEntityCall;

	public EntityCellEntityCall_MailBase cellEntityCall;

	public byte MailType;

	public byte status;

	public ulong userID;

	public virtual void onMailTypeChanged(byte oldValue)
	{
	}

	public virtual void onStatusChanged(byte oldValue)
	{
	}

	public virtual void onUserIDChanged(ulong oldValue)
	{
	}

	public MailBase()
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
		baseEntityCall = new EntityBaseEntityCall_MailBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_MailBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["Mail"];
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
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Mail"];
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
				case 155:
				{
					byte mailType = MailType;
					MailType = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onMailTypeChanged(mailType);
						}
					}
					else if (inWorld)
					{
						onMailTypeChanged(mailType);
					}
					break;
				}
				case 40001:
				{
					Vector3 oldValue3 = direction;
					direction = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onDirectionChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onDirectionChanged(oldValue3);
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
				case 156:
				{
					byte oldValue4 = status;
					status = stream.readUint8();
					if (property.isBase())
					{
						if (inited)
						{
							onStatusChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onStatusChanged(oldValue4);
					}
					break;
				}
				case 154:
				{
					ulong oldValue = userID;
					userID = stream.readUint64();
					if (property.isBase())
					{
						if (inited)
						{
							onUserIDChanged(oldValue);
						}
					}
					else if (inWorld)
					{
						onUserIDChanged(oldValue);
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
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Mail"].idpropertys;
		byte mailType = MailType;
		Property property = idpropertys[4];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onMailTypeChanged(mailType);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onMailTypeChanged(mailType);
		}
		Vector3 oldValue = direction;
		Property property2 = idpropertys[2];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue);
		}
		Vector3 oldValue2 = position;
		Property property3 = idpropertys[1];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue2);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue2);
		}
		byte oldValue3 = status;
		Property property4 = idpropertys[5];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onStatusChanged(oldValue3);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onStatusChanged(oldValue3);
		}
		ulong oldValue4 = userID;
		Property property5 = idpropertys[6];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onUserIDChanged(oldValue4);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onUserIDChanged(oldValue4);
		}
	}
}
