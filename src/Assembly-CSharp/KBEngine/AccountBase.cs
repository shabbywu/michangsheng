using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public abstract class AccountBase : Entity
{
	public EntityBaseEntityCall_AccountBase baseEntityCall;

	public EntityCellEntityCall_AccountBase cellEntityCall;

	public CHECKIN_INFO_LIST CheckInList = new CHECKIN_INFO_LIST();

	public FRIEND_INFO_LIST FriendList = new FRIEND_INFO_LIST();

	public SHOPBUY_INFO_LIST ShopBuyTime = new SHOPBUY_INFO_LIST();

	public ITEM_INFO_LIST equipItemList = new ITEM_INFO_LIST();

	public ITEM_INFO_LIST itemList = new ITEM_INFO_LIST();

	public PLAYER_INFO playerInfo = new PLAYER_INFO();

	public virtual void onCheckInListChanged(CHECKIN_INFO_LIST oldValue)
	{
	}

	public virtual void onFriendListChanged(FRIEND_INFO_LIST oldValue)
	{
	}

	public virtual void onShopBuyTimeChanged(SHOPBUY_INFO_LIST oldValue)
	{
	}

	public virtual void onEquipItemListChanged(ITEM_INFO_LIST oldValue)
	{
	}

	public virtual void onItemListChanged(ITEM_INFO_LIST oldValue)
	{
	}

	public virtual void onPlayerInfoChanged(PLAYER_INFO oldValue)
	{
	}

	public abstract void CheckInSuccess(uint arg1);

	public abstract void HomeErrorMessage(string arg1);

	public abstract void MatchSuccess();

	public abstract void addFriendSuccess(FRIEND_INFO arg1);

	public abstract void addItem(ulong arg1, uint arg2);

	public abstract void boxAddItem(ulong arg1, uint arg2);

	public abstract void buySuccess(ITEM_INFO_LIST arg1);

	public abstract void createItem(ITEM_INFO arg1);

	public abstract void createOder(string arg1);

	public abstract void getTalkingMsg(FRIEND_INFO arg1, string arg2);

	public abstract void goToCreatePlayer();

	public abstract void goToHome(PLAYER_INFO arg1);

	public abstract void goToSpace();

	public abstract void leaveTeam();

	public abstract void onCreateAvatarResult(byte arg1, AVATAR_INFO arg2);

	public abstract void onHelloTestBacke();

	public abstract void onRemoveAvatar(ulong arg1);

	public abstract void onReqAvatarList(ITEM_INFO_LIST arg1);

	public abstract void onReqShopList(ITEM_INFO_LIST arg1, string arg2);

	public abstract void onStartGame();

	public abstract void receiveaddTeam(string arg1, ulong arg2, ulong arg3);

	public abstract void receiveaddfriend(string arg1, ulong arg2);

	public abstract void removeItem(ulong arg1, uint arg2);

	public abstract void requestOnlineFriend(FRIEND_INFO_LIST arg1);

	public abstract void setAllTeamMember(string arg1, ulong arg2);

	public abstract void setTeamMember(string arg1, uint arg2);

	public AccountBase()
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
		baseEntityCall = new EntityBaseEntityCall_AccountBase(id, className);
	}

	public override void onGetCell()
	{
		cellEntityCall = new EntityCellEntityCall_AccountBase(id, className);
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
		ScriptModule scriptModule = EntityDef.moduledefs["Account"];
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
			case 64:
			{
				uint arg31 = stream.readUint32();
				CheckInSuccess(arg31);
				break;
			}
			case 52:
			{
				string arg30 = stream.readUnicode();
				HomeErrorMessage(arg30);
				break;
			}
			case 49:
				MatchSuccess();
				break;
			case 54:
			{
				FRIEND_INFO arg29 = ((DATATYPE_FRIEND_INFO)method.args[0]).createFromStreamEx(stream);
				addFriendSuccess(arg29);
				break;
			}
			case 61:
			{
				ulong arg27 = stream.readUint64();
				uint arg28 = stream.readUint32();
				addItem(arg27, arg28);
				break;
			}
			case 62:
			{
				ulong arg25 = stream.readUint64();
				uint arg26 = stream.readUint32();
				boxAddItem(arg25, arg26);
				break;
			}
			case 51:
			{
				ITEM_INFO_LIST arg24 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				buySuccess(arg24);
				break;
			}
			case 63:
			{
				ITEM_INFO arg23 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
				createItem(arg23);
				break;
			}
			case 53:
			{
				string arg22 = stream.readString();
				createOder(arg22);
				break;
			}
			case 41:
			{
				FRIEND_INFO arg20 = ((DATATYPE_FRIEND_INFO)method.args[0]).createFromStreamEx(stream);
				string arg21 = stream.readUnicode();
				getTalkingMsg(arg20, arg21);
				break;
			}
			case 45:
				goToCreatePlayer();
				break;
			case 47:
			{
				PLAYER_INFO arg19 = ((DATATYPE_PLAYER_INFO)method.args[0]).createFromStreamEx(stream);
				goToHome(arg19);
				break;
			}
			case 46:
				goToSpace();
				break;
			case 56:
				leaveTeam();
				break;
			case 42:
			{
				byte arg17 = stream.readUint8();
				AVATAR_INFO arg18 = ((DATATYPE_AVATAR_INFO)method.args[1]).createFromStreamEx(stream);
				onCreateAvatarResult(arg17, arg18);
				break;
			}
			case 43:
				onHelloTestBacke();
				break;
			case 48:
			{
				ulong arg16 = stream.readUint64();
				onRemoveAvatar(arg16);
				break;
			}
			case 39:
			{
				ITEM_INFO_LIST arg15 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				onReqAvatarList(arg15);
				break;
			}
			case 50:
			{
				ITEM_INFO_LIST arg13 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				string arg14 = stream.readString();
				onReqShopList(arg13, arg14);
				break;
			}
			case 44:
				onStartGame();
				break;
			case 59:
			{
				string arg10 = stream.readUnicode();
				ulong arg11 = stream.readUint64();
				ulong arg12 = stream.readUint64();
				receiveaddTeam(arg10, arg11, arg12);
				break;
			}
			case 55:
			{
				string arg8 = stream.readUnicode();
				ulong arg9 = stream.readUint64();
				receiveaddfriend(arg8, arg9);
				break;
			}
			case 60:
			{
				ulong arg6 = stream.readUint64();
				uint arg7 = stream.readUint32();
				removeItem(arg6, arg7);
				break;
			}
			case 40:
			{
				FRIEND_INFO_LIST arg5 = ((DATATYPE_FRIEND_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				requestOnlineFriend(arg5);
				break;
			}
			case 58:
			{
				string arg3 = stream.readUnicode();
				ulong arg4 = stream.readUint64();
				setAllTeamMember(arg3, arg4);
				break;
			}
			case 57:
			{
				string arg = stream.readUnicode();
				uint arg2 = stream.readUint32();
				setTeamMember(arg, arg2);
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
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		ScriptModule scriptModule = EntityDef.moduledefs["Account"];
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
				case 10:
				{
					CHECKIN_INFO_LIST checkInList = CheckInList;
					CheckInList = ((DATATYPE_CHECKIN_INFO_LIST)EntityDef.id2datatypes[41]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onCheckInListChanged(checkInList);
						}
					}
					else if (inWorld)
					{
						onCheckInListChanged(checkInList);
					}
					break;
				}
				case 8:
				{
					FRIEND_INFO_LIST friendList = FriendList;
					FriendList = ((DATATYPE_FRIEND_INFO_LIST)EntityDef.id2datatypes[32]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onFriendListChanged(friendList);
						}
					}
					else if (inWorld)
					{
						onFriendListChanged(friendList);
					}
					break;
				}
				case 9:
				{
					SHOPBUY_INFO_LIST shopBuyTime = ShopBuyTime;
					ShopBuyTime = ((DATATYPE_SHOPBUY_INFO_LIST)EntityDef.id2datatypes[28]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onShopBuyTimeChanged(shopBuyTime);
						}
					}
					else if (inWorld)
					{
						onShopBuyTimeChanged(shopBuyTime);
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
				case 7:
				{
					ITEM_INFO_LIST oldValue3 = equipItemList;
					equipItemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onEquipItemListChanged(oldValue3);
						}
					}
					else if (inWorld)
					{
						onEquipItemListChanged(oldValue3);
					}
					break;
				}
				case 6:
				{
					ITEM_INFO_LIST oldValue2 = itemList;
					itemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onItemListChanged(oldValue2);
						}
					}
					else if (inWorld)
					{
						onItemListChanged(oldValue2);
					}
					break;
				}
				case 1:
				{
					PLAYER_INFO oldValue5 = playerInfo;
					playerInfo = ((DATATYPE_PLAYER_INFO)EntityDef.id2datatypes[34]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (inited)
						{
							onPlayerInfoChanged(oldValue5);
						}
					}
					else if (inWorld)
					{
						onPlayerInfoChanged(oldValue5);
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
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Account"].idpropertys;
		CHECKIN_INFO_LIST checkInList = CheckInList;
		Property property = idpropertys[4];
		if (property.isBase())
		{
			if (inited && !inWorld)
			{
				onCheckInListChanged(checkInList);
			}
		}
		else if (inWorld && (!property.isOwnerOnly() || isPlayer()))
		{
			onCheckInListChanged(checkInList);
		}
		FRIEND_INFO_LIST friendList = FriendList;
		Property property2 = idpropertys[5];
		if (property2.isBase())
		{
			if (inited && !inWorld)
			{
				onFriendListChanged(friendList);
			}
		}
		else if (inWorld && (!property2.isOwnerOnly() || isPlayer()))
		{
			onFriendListChanged(friendList);
		}
		SHOPBUY_INFO_LIST shopBuyTime = ShopBuyTime;
		Property property3 = idpropertys[6];
		if (property3.isBase())
		{
			if (inited && !inWorld)
			{
				onShopBuyTimeChanged(shopBuyTime);
			}
		}
		else if (inWorld && (!property3.isOwnerOnly() || isPlayer()))
		{
			onShopBuyTimeChanged(shopBuyTime);
		}
		Vector3 oldValue = direction;
		Property property4 = idpropertys[2];
		if (property4.isBase())
		{
			if (inited && !inWorld)
			{
				onDirectionChanged(oldValue);
			}
		}
		else if (inWorld && (!property4.isOwnerOnly() || isPlayer()))
		{
			onDirectionChanged(oldValue);
		}
		ITEM_INFO_LIST oldValue2 = equipItemList;
		Property property5 = idpropertys[7];
		if (property5.isBase())
		{
			if (inited && !inWorld)
			{
				onEquipItemListChanged(oldValue2);
			}
		}
		else if (inWorld && (!property5.isOwnerOnly() || isPlayer()))
		{
			onEquipItemListChanged(oldValue2);
		}
		ITEM_INFO_LIST oldValue3 = itemList;
		Property property6 = idpropertys[8];
		if (property6.isBase())
		{
			if (inited && !inWorld)
			{
				onItemListChanged(oldValue3);
			}
		}
		else if (inWorld && (!property6.isOwnerOnly() || isPlayer()))
		{
			onItemListChanged(oldValue3);
		}
		PLAYER_INFO oldValue4 = playerInfo;
		Property property7 = idpropertys[9];
		if (property7.isBase())
		{
			if (inited && !inWorld)
			{
				onPlayerInfoChanged(oldValue4);
			}
		}
		else if (inWorld && (!property7.isOwnerOnly() || isPlayer()))
		{
			onPlayerInfoChanged(oldValue4);
		}
		Vector3 oldValue5 = position;
		Property property8 = idpropertys[1];
		if (property8.isBase())
		{
			if (inited && !inWorld)
			{
				onPositionChanged(oldValue5);
			}
		}
		else if (inWorld && (!property8.isOwnerOnly() || isPlayer()))
		{
			onPositionChanged(oldValue5);
		}
	}
}
