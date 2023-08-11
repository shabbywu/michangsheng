using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class DroppedItemBase : Entity
{
	public EntityBaseEntityCall_DroppedItemBase baseEntityCall;

	public EntityCellEntityCall_DroppedItemBase cellEntityCall;

	public uint dialogID;

	public uint entityNO;

	public uint itemCount;

	public int itemId;

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

	public virtual void onItemCountChanged(uint oldValue)
	{
	}

	public virtual void onItemIdChanged(int oldValue)
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

	public DroppedItemBase()
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
		baseEntityCall = new EntityBaseEntityCall_DroppedItemBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_DroppedItemBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["DroppedItem"];
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
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["DroppedItem"];
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
				case 96:
				{
					uint oldValue9 = dialogID;
					dialogID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onDialogIDChanged(oldValue9);
						}
					}
					else if (inWorld)
					{
						onDialogIDChanged(oldValue9);
					}
					break;
				}
				case 40001:
				{
					Vector3 oldValue4 = direction;
					direction = stream.readVector3();
					if (property.isBase())
					{
						if (inited)
						{
							onDirectionChanged(oldValue4);
						}
					}
					else if (inWorld)
					{
						onDirectionChanged(oldValue4);
					}
					break;
				}
				case 51007:
				{
					uint oldValue5 = entityNO;
					entityNO = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onEntityNOChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onEntityNOChanged(oldValue5);
					}
					break;
				}
				case 91:
				{
					uint oldValue2 = itemCount;
					itemCount = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onItemCountChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onItemCountChanged(oldValue2);
					}
					break;
				}
				case 90:
				{
					int oldValue10 = itemId;
					itemId = stream.readInt32();
					if (property.isBase())
					{
						if (inited)
						{
							onItemIdChanged(oldValue10);
						}
					}
					else if (inWorld)
					{
						onItemIdChanged(oldValue10);
					}
					break;
				}
				case 41006:
				{
					uint oldValue8 = modelID;
					modelID = stream.readUint32();
					if (property.isBase())
					{
						if (inited)
						{
							onModelIDChanged(oldValue8);
						}
					}
					else if (inWorld)
					{
						onModelIDChanged(oldValue8);
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
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["DroppedItem"].idpropertys;
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
		uint oldValue4 = itemCount;
		Property property4 = idpropertys[6];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onItemCountChanged(oldValue4);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onItemCountChanged(oldValue4);
		}
		int oldValue5 = itemId;
		Property property5 = idpropertys[7];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onItemIdChanged(oldValue5);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onItemIdChanged(oldValue5);
		}
		uint oldValue6 = modelID;
		Property property6 = idpropertys[8];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onModelIDChanged(oldValue6);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onModelIDChanged(oldValue6);
		}
		byte oldValue7 = modelScale;
		Property property7 = idpropertys[9];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onModelScaleChanged(oldValue7);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onModelScaleChanged(oldValue7);
		}
		string oldValue8 = name;
		Property property8 = idpropertys[10];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onNameChanged(oldValue8);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onNameChanged(oldValue8);
		}
		Vector3 oldValue9 = position;
		Property property9 = idpropertys[1];
		if (property9.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue9);
			}
		}
		else if (inWorld && (!property9.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue9);
		}
		uint oldValue10 = uid;
		Property property10 = idpropertys[11];
		if (property10.isBase())
		{
			if (inited && !inWorld)
			{
				onUidChanged(oldValue10);
			}
		}
		else if (inWorld && (!property10.isOwnerOnly() || isPlayer()))
		{
			onUidChanged(oldValue10);
		}
		uint oldValue11 = utype;
		Property property11 = idpropertys[12];
		if (property11.isBase())
		{
			if (inited && !inWorld)
			{
				onUtypeChanged(oldValue11);
			}
		}
		else if (inWorld && (!property11.isOwnerOnly() || isPlayer()))
		{
			onUtypeChanged(oldValue11);
		}
	}
}
