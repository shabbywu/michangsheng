using System;

namespace KBEngine
{
	// Token: 0x02000B6D RID: 2925
	public class EntityBaseEntityCall_AccountBase : EntityCall
	{
		// Token: 0x060051C5 RID: 20933 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_AccountBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0022372E File Offset: 0x0022192E
		public void CheckIn(ushort arg1)
		{
			if (base.newCall("CheckIn", 0) == null)
			{
				return;
			}
			this.bundle.writeUint16(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00223752 File Offset: 0x00221952
		public void TeamStartMatch(ulong arg1)
		{
			if (base.newCall("TeamStartMatch", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00223776 File Offset: 0x00221976
		public void UseItem(ulong arg1)
		{
			if (base.newCall("UseItem", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0022379A File Offset: 0x0022199A
		public void addFriend(string arg1)
		{
			if (base.newCall("addFriend", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x002237BE File Offset: 0x002219BE
		public void addFriendbyDbid(ulong arg1)
		{
			if (base.newCall("addFriendbyDbid", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x002237E2 File Offset: 0x002219E2
		public void buyShopItem(ulong arg1)
		{
			if (base.newCall("buyShopItem", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00223806 File Offset: 0x00221A06
		public void cancelMatch()
		{
			if (base.newCall("cancelMatch", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0022381E File Offset: 0x00221A1E
		public void createTeam(ulong arg1)
		{
			if (base.newCall("createTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00223842 File Offset: 0x00221A42
		public void deliverGoods(ulong arg1)
		{
			if (base.newCall("deliverGoods", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00223866 File Offset: 0x00221A66
		public void getOnlineFriend()
		{
			if (base.newCall("getOnlineFriend", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0022387E File Offset: 0x00221A7E
		public void getShopList(uint arg1)
		{
			if (base.newCall("getShopList", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x002238A2 File Offset: 0x00221AA2
		public void joinTeam(ulong arg1)
		{
			if (base.newCall("joinTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x002238C6 File Offset: 0x00221AC6
		public void onHelloTest()
		{
			if (base.newCall("onHelloTest", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x002238DE File Offset: 0x00221ADE
		public void onPayEnd()
		{
			if (base.newCall("onPayEnd", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x002238F6 File Offset: 0x00221AF6
		public void rejoinSpace()
		{
			if (base.newCall("rejoinSpace", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0022390E File Offset: 0x00221B0E
		public void reqAvatarList()
		{
			if (base.newCall("reqAvatarList", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x00223926 File Offset: 0x00221B26
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

		// Token: 0x060051D7 RID: 20951 RVA: 0x00223956 File Offset: 0x00221B56
		public void reqCreatePlayer(string arg1)
		{
			if (base.newCall("reqCreatePlayer", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0022397A File Offset: 0x00221B7A
		public void reqPlayerInfo()
		{
			if (base.newCall("reqPlayerInfo", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00223992 File Offset: 0x00221B92
		public void reqRemoveAvatar(string arg1)
		{
			if (base.newCall("reqRemoveAvatar", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x002239B6 File Offset: 0x00221BB6
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

		// Token: 0x060051DB RID: 20955 RVA: 0x002239E6 File Offset: 0x00221BE6
		public void requestLeaveTeam()
		{
			if (base.newCall("requestLeaveTeam", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x002239FE File Offset: 0x00221BFE
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

		// Token: 0x060051DD RID: 20957 RVA: 0x00223A2E File Offset: 0x00221C2E
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

		// Token: 0x060051DE RID: 20958 RVA: 0x00223A5E File Offset: 0x00221C5E
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

		// Token: 0x060051DF RID: 20959 RVA: 0x00223A8E File Offset: 0x00221C8E
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

		// Token: 0x060051E0 RID: 20960 RVA: 0x00223ABE File Offset: 0x00221CBE
		public void startMatch()
		{
			if (base.newCall("startMatch", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00223AD6 File Offset: 0x00221CD6
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
