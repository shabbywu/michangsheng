using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EAA RID: 3754
	public abstract class AccountBase : Entity
	{
		// Token: 0x06005A12 RID: 23058 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onCheckInListChanged(CHECKIN_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onFriendListChanged(FRIEND_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onShopBuyTimeChanged(SHOPBUY_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEquipItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onItemListChanged(ITEM_INFO_LIST oldValue)
		{
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onPlayerInfoChanged(PLAYER_INFO oldValue)
		{
		}

		// Token: 0x06005A18 RID: 23064
		public abstract void CheckInSuccess(uint arg1);

		// Token: 0x06005A19 RID: 23065
		public abstract void HomeErrorMessage(string arg1);

		// Token: 0x06005A1A RID: 23066
		public abstract void MatchSuccess();

		// Token: 0x06005A1B RID: 23067
		public abstract void addFriendSuccess(FRIEND_INFO arg1);

		// Token: 0x06005A1C RID: 23068
		public abstract void addItem(ulong arg1, uint arg2);

		// Token: 0x06005A1D RID: 23069
		public abstract void boxAddItem(ulong arg1, uint arg2);

		// Token: 0x06005A1E RID: 23070
		public abstract void buySuccess(ITEM_INFO_LIST arg1);

		// Token: 0x06005A1F RID: 23071
		public abstract void createItem(ITEM_INFO arg1);

		// Token: 0x06005A20 RID: 23072
		public abstract void createOder(string arg1);

		// Token: 0x06005A21 RID: 23073
		public abstract void getTalkingMsg(FRIEND_INFO arg1, string arg2);

		// Token: 0x06005A22 RID: 23074
		public abstract void goToCreatePlayer();

		// Token: 0x06005A23 RID: 23075
		public abstract void goToHome(PLAYER_INFO arg1);

		// Token: 0x06005A24 RID: 23076
		public abstract void goToSpace();

		// Token: 0x06005A25 RID: 23077
		public abstract void leaveTeam();

		// Token: 0x06005A26 RID: 23078
		public abstract void onCreateAvatarResult(byte arg1, AVATAR_INFO arg2);

		// Token: 0x06005A27 RID: 23079
		public abstract void onHelloTestBacke();

		// Token: 0x06005A28 RID: 23080
		public abstract void onRemoveAvatar(ulong arg1);

		// Token: 0x06005A29 RID: 23081
		public abstract void onReqAvatarList(ITEM_INFO_LIST arg1);

		// Token: 0x06005A2A RID: 23082
		public abstract void onReqShopList(ITEM_INFO_LIST arg1, string arg2);

		// Token: 0x06005A2B RID: 23083
		public abstract void onStartGame();

		// Token: 0x06005A2C RID: 23084
		public abstract void receiveaddTeam(string arg1, ulong arg2, ulong arg3);

		// Token: 0x06005A2D RID: 23085
		public abstract void receiveaddfriend(string arg1, ulong arg2);

		// Token: 0x06005A2E RID: 23086
		public abstract void removeItem(ulong arg1, uint arg2);

		// Token: 0x06005A2F RID: 23087
		public abstract void requestOnlineFriend(FRIEND_INFO_LIST arg1);

		// Token: 0x06005A30 RID: 23088
		public abstract void setAllTeamMember(string arg1, ulong arg2);

		// Token: 0x06005A31 RID: 23089
		public abstract void setTeamMember(string arg1, uint arg2);

		// Token: 0x06005A32 RID: 23090 RVA: 0x0024BDCC File Offset: 0x00249FCC
		public AccountBase()
		{
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0003FDC0 File Offset: 0x0003DFC0
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_AccountBase(this.id, this.className);
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x0003FDD9 File Offset: 0x0003DFD9
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_AccountBase(this.id, this.className);
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0003FDF2 File Offset: 0x0003DFF2
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x0003FDFB File Offset: 0x0003DFFB
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0003FE03 File Offset: 0x0003E003
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0024BE24 File Offset: 0x0024A024
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Account"];
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
			if (num != 0)
			{
				ushort properUtype = scriptModule.idpropertys[num].properUtype;
				return;
			}
			Method method = scriptModule.idmethods[key];
			switch (method.methodUtype)
			{
			case 39:
			{
				ITEM_INFO_LIST arg = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				this.onReqAvatarList(arg);
				return;
			}
			case 40:
			{
				FRIEND_INFO_LIST arg2 = ((DATATYPE_FRIEND_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				this.requestOnlineFriend(arg2);
				return;
			}
			case 41:
			{
				FRIEND_INFO arg3 = ((DATATYPE_FRIEND_INFO)method.args[0]).createFromStreamEx(stream);
				string arg4 = stream.readUnicode();
				this.getTalkingMsg(arg3, arg4);
				return;
			}
			case 42:
			{
				byte arg5 = stream.readUint8();
				AVATAR_INFO arg6 = ((DATATYPE_AVATAR_INFO)method.args[1]).createFromStreamEx(stream);
				this.onCreateAvatarResult(arg5, arg6);
				return;
			}
			case 43:
				this.onHelloTestBacke();
				return;
			case 44:
				this.onStartGame();
				return;
			case 45:
				this.goToCreatePlayer();
				return;
			case 46:
				this.goToSpace();
				return;
			case 47:
			{
				PLAYER_INFO arg7 = ((DATATYPE_PLAYER_INFO)method.args[0]).createFromStreamEx(stream);
				this.goToHome(arg7);
				return;
			}
			case 48:
			{
				ulong arg8 = stream.readUint64();
				this.onRemoveAvatar(arg8);
				return;
			}
			case 49:
				this.MatchSuccess();
				return;
			case 50:
			{
				ITEM_INFO_LIST arg9 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				string arg10 = stream.readString();
				this.onReqShopList(arg9, arg10);
				return;
			}
			case 51:
			{
				ITEM_INFO_LIST arg11 = ((DATATYPE_ITEM_INFO_LIST)method.args[0]).createFromStreamEx(stream);
				this.buySuccess(arg11);
				return;
			}
			case 52:
			{
				string arg12 = stream.readUnicode();
				this.HomeErrorMessage(arg12);
				return;
			}
			case 53:
			{
				string arg13 = stream.readString();
				this.createOder(arg13);
				return;
			}
			case 54:
			{
				FRIEND_INFO arg14 = ((DATATYPE_FRIEND_INFO)method.args[0]).createFromStreamEx(stream);
				this.addFriendSuccess(arg14);
				return;
			}
			case 55:
			{
				string arg15 = stream.readUnicode();
				ulong arg16 = stream.readUint64();
				this.receiveaddfriend(arg15, arg16);
				return;
			}
			case 56:
				this.leaveTeam();
				return;
			case 57:
			{
				string arg17 = stream.readUnicode();
				uint arg18 = stream.readUint32();
				this.setTeamMember(arg17, arg18);
				return;
			}
			case 58:
			{
				string arg19 = stream.readUnicode();
				ulong arg20 = stream.readUint64();
				this.setAllTeamMember(arg19, arg20);
				return;
			}
			case 59:
			{
				string arg21 = stream.readUnicode();
				ulong arg22 = stream.readUint64();
				ulong arg23 = stream.readUint64();
				this.receiveaddTeam(arg21, arg22, arg23);
				return;
			}
			case 60:
			{
				ulong arg24 = stream.readUint64();
				uint arg25 = stream.readUint32();
				this.removeItem(arg24, arg25);
				return;
			}
			case 61:
			{
				ulong arg26 = stream.readUint64();
				uint arg27 = stream.readUint32();
				this.addItem(arg26, arg27);
				return;
			}
			case 62:
			{
				ulong arg28 = stream.readUint64();
				uint arg29 = stream.readUint32();
				this.boxAddItem(arg28, arg29);
				return;
			}
			case 63:
			{
				ITEM_INFO arg30 = ((DATATYPE_ITEM_INFO)method.args[0]).createFromStreamEx(stream);
				this.createItem(arg30);
				return;
			}
			case 64:
			{
				uint arg31 = stream.readUint32();
				this.CheckInSuccess(arg31);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0024C19C File Offset: 0x0024A39C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Account"];
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
				switch (properUtype2)
				{
				case 1:
				{
					PLAYER_INFO oldValue = this.playerInfo;
					this.playerInfo = ((DATATYPE_PLAYER_INFO)EntityDef.id2datatypes[34]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPlayerInfoChanged(oldValue);
						}
					}
					else if (this.inWorld)
					{
						this.onPlayerInfoChanged(oldValue);
					}
					break;
				}
				case 2:
				case 3:
				case 4:
				case 5:
					break;
				case 6:
				{
					ITEM_INFO_LIST oldValue2 = this.itemList;
					this.itemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onItemListChanged(oldValue2);
						}
					}
					else if (this.inWorld)
					{
						this.onItemListChanged(oldValue2);
					}
					break;
				}
				case 7:
				{
					ITEM_INFO_LIST oldValue3 = this.equipItemList;
					this.equipItemList = ((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onEquipItemListChanged(oldValue3);
						}
					}
					else if (this.inWorld)
					{
						this.onEquipItemListChanged(oldValue3);
					}
					break;
				}
				case 8:
				{
					FRIEND_INFO_LIST friendList = this.FriendList;
					this.FriendList = ((DATATYPE_FRIEND_INFO_LIST)EntityDef.id2datatypes[32]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onFriendListChanged(friendList);
						}
					}
					else if (this.inWorld)
					{
						this.onFriendListChanged(friendList);
					}
					break;
				}
				case 9:
				{
					SHOPBUY_INFO_LIST shopBuyTime = this.ShopBuyTime;
					this.ShopBuyTime = ((DATATYPE_SHOPBUY_INFO_LIST)EntityDef.id2datatypes[28]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onShopBuyTimeChanged(shopBuyTime);
						}
					}
					else if (this.inWorld)
					{
						this.onShopBuyTimeChanged(shopBuyTime);
					}
					break;
				}
				case 10:
				{
					CHECKIN_INFO_LIST checkInList = this.CheckInList;
					this.CheckInList = ((DATATYPE_CHECKIN_INFO_LIST)EntityDef.id2datatypes[41]).createFromStreamEx(stream);
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onCheckInListChanged(checkInList);
						}
					}
					else if (this.inWorld)
					{
						this.onCheckInListChanged(checkInList);
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
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0024C530 File Offset: 0x0024A730
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Account"].idpropertys;
			CHECKIN_INFO_LIST checkInList = this.CheckInList;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onCheckInListChanged(checkInList);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onCheckInListChanged(checkInList);
			}
			FRIEND_INFO_LIST friendList = this.FriendList;
			Property property2 = idpropertys[5];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onFriendListChanged(friendList);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onFriendListChanged(friendList);
			}
			SHOPBUY_INFO_LIST shopBuyTime = this.ShopBuyTime;
			Property property3 = idpropertys[6];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onShopBuyTimeChanged(shopBuyTime);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onShopBuyTimeChanged(shopBuyTime);
			}
			Vector3 direction = this.direction;
			Property property4 = idpropertys[2];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			ITEM_INFO_LIST oldValue = this.equipItemList;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEquipItemListChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onEquipItemListChanged(oldValue);
			}
			ITEM_INFO_LIST oldValue2 = this.itemList;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onItemListChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onItemListChanged(oldValue2);
			}
			PLAYER_INFO oldValue3 = this.playerInfo;
			Property property7 = idpropertys[9];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPlayerInfoChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onPlayerInfoChanged(oldValue3);
			}
			Vector3 position = this.position;
			Property property8 = idpropertys[1];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x040059A4 RID: 22948
		public EntityBaseEntityCall_AccountBase baseEntityCall;

		// Token: 0x040059A5 RID: 22949
		public EntityCellEntityCall_AccountBase cellEntityCall;

		// Token: 0x040059A6 RID: 22950
		public CHECKIN_INFO_LIST CheckInList = new CHECKIN_INFO_LIST();

		// Token: 0x040059A7 RID: 22951
		public FRIEND_INFO_LIST FriendList = new FRIEND_INFO_LIST();

		// Token: 0x040059A8 RID: 22952
		public SHOPBUY_INFO_LIST ShopBuyTime = new SHOPBUY_INFO_LIST();

		// Token: 0x040059A9 RID: 22953
		public ITEM_INFO_LIST equipItemList = new ITEM_INFO_LIST();

		// Token: 0x040059AA RID: 22954
		public ITEM_INFO_LIST itemList = new ITEM_INFO_LIST();

		// Token: 0x040059AB RID: 22955
		public PLAYER_INFO playerInfo = new PLAYER_INFO();
	}
}
