using System;

namespace KBEngine
{
	// Token: 0x02000EEB RID: 3819
	public class EntityBaseEntityCall_AccountBase : EntityCall
	{
		// Token: 0x06005C01 RID: 23553 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_AccountBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x00040AD0 File Offset: 0x0003ECD0
		public void CheckIn(ushort arg1)
		{
			if (base.newCall("CheckIn", 0) == null)
			{
				return;
			}
			this.bundle.writeUint16(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x00040AF4 File Offset: 0x0003ECF4
		public void TeamStartMatch(ulong arg1)
		{
			if (base.newCall("TeamStartMatch", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00040B18 File Offset: 0x0003ED18
		public void UseItem(ulong arg1)
		{
			if (base.newCall("UseItem", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00040B3C File Offset: 0x0003ED3C
		public void addFriend(string arg1)
		{
			if (base.newCall("addFriend", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x00040B60 File Offset: 0x0003ED60
		public void addFriendbyDbid(ulong arg1)
		{
			if (base.newCall("addFriendbyDbid", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x00040B84 File Offset: 0x0003ED84
		public void buyShopItem(ulong arg1)
		{
			if (base.newCall("buyShopItem", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		public void cancelMatch()
		{
			if (base.newCall("cancelMatch", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x00040BC0 File Offset: 0x0003EDC0
		public void createTeam(ulong arg1)
		{
			if (base.newCall("createTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x00040BE4 File Offset: 0x0003EDE4
		public void deliverGoods(ulong arg1)
		{
			if (base.newCall("deliverGoods", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x00040C08 File Offset: 0x0003EE08
		public void getOnlineFriend()
		{
			if (base.newCall("getOnlineFriend", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00040C20 File Offset: 0x0003EE20
		public void getShopList(uint arg1)
		{
			if (base.newCall("getShopList", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x00040C44 File Offset: 0x0003EE44
		public void joinTeam(ulong arg1)
		{
			if (base.newCall("joinTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x00040C68 File Offset: 0x0003EE68
		public void onHelloTest()
		{
			if (base.newCall("onHelloTest", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00040C80 File Offset: 0x0003EE80
		public void onPayEnd()
		{
			if (base.newCall("onPayEnd", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x00040C98 File Offset: 0x0003EE98
		public void rejoinSpace()
		{
			if (base.newCall("rejoinSpace", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00040CB0 File Offset: 0x0003EEB0
		public void reqAvatarList()
		{
			if (base.newCall("reqAvatarList", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x00040CC8 File Offset: 0x0003EEC8
		public void reqCreateAvatar(string arg1, byte arg2)
		{
			if (base.newCall("reqCreateAvatar", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			this.bundle.writeUint8(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x00040CF8 File Offset: 0x0003EEF8
		public void reqCreatePlayer(string arg1)
		{
			if (base.newCall("reqCreatePlayer", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00040D1C File Offset: 0x0003EF1C
		public void reqPlayerInfo()
		{
			if (base.newCall("reqPlayerInfo", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00040D34 File Offset: 0x0003EF34
		public void reqRemoveAvatar(string arg1)
		{
			if (base.newCall("reqRemoveAvatar", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00040D58 File Offset: 0x0003EF58
		public void requestJoinTeam(ulong arg1, ulong arg2)
		{
			if (base.newCall("requestJoinTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00040D88 File Offset: 0x0003EF88
		public void requestLeaveTeam()
		{
			if (base.newCall("requestLeaveTeam", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		public void requestReceive(ushort arg1, ulong arg2)
		{
			if (base.newCall("requestReceive", 0) == null)
			{
				return;
			}
			this.bundle.writeUint16(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		public void requestReceiveTeam(ushort arg1, ulong arg2)
		{
			if (base.newCall("requestReceiveTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint16(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x00040E00 File Offset: 0x0003F000
		public void selectAvatarGame(ulong arg1, ulong arg2)
		{
			if (base.newCall("selectAvatarGame", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00040E30 File Offset: 0x0003F030
		public void sendMsg(ulong arg1, string arg2)
		{
			if (base.newCall("sendMsg", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeUnicode(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x00040E60 File Offset: 0x0003F060
		public void startMatch()
		{
			if (base.newCall("startMatch", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x00040E78 File Offset: 0x0003F078
		public void useCDK(string arg1)
		{
			if (base.newCall("useCDK", 0) == null)
			{
				return;
			}
			this.bundle.writeString(arg1);
			base.sendCall(null);
		}
	}
}
