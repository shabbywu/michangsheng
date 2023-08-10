namespace KBEngine;

public class EntityBaseEntityCall_AccountBase : EntityCall
{
	public EntityBaseEntityCall_AccountBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}

	public void CheckIn(ushort arg1)
	{
		if (newCall("CheckIn", 0) != null)
		{
			bundle.writeUint16(arg1);
			sendCall(null);
		}
	}

	public void TeamStartMatch(ulong arg1)
	{
		if (newCall("TeamStartMatch", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void UseItem(ulong arg1)
	{
		if (newCall("UseItem", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void addFriend(string arg1)
	{
		if (newCall("addFriend", 0) != null)
		{
			bundle.writeUnicode(arg1);
			sendCall(null);
		}
	}

	public void addFriendbyDbid(ulong arg1)
	{
		if (newCall("addFriendbyDbid", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void buyShopItem(ulong arg1)
	{
		if (newCall("buyShopItem", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void cancelMatch()
	{
		if (newCall("cancelMatch", 0) != null)
		{
			sendCall(null);
		}
	}

	public void createTeam(ulong arg1)
	{
		if (newCall("createTeam", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void deliverGoods(ulong arg1)
	{
		if (newCall("deliverGoods", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void getOnlineFriend()
	{
		if (newCall("getOnlineFriend", 0) != null)
		{
			sendCall(null);
		}
	}

	public void getShopList(uint arg1)
	{
		if (newCall("getShopList", 0) != null)
		{
			bundle.writeUint32(arg1);
			sendCall(null);
		}
	}

	public void joinTeam(ulong arg1)
	{
		if (newCall("joinTeam", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void onHelloTest()
	{
		if (newCall("onHelloTest", 0) != null)
		{
			sendCall(null);
		}
	}

	public void onPayEnd()
	{
		if (newCall("onPayEnd", 0) != null)
		{
			sendCall(null);
		}
	}

	public void rejoinSpace()
	{
		if (newCall("rejoinSpace", 0) != null)
		{
			sendCall(null);
		}
	}

	public void reqAvatarList()
	{
		if (newCall("reqAvatarList", 0) != null)
		{
			sendCall(null);
		}
	}

	public void reqCreateAvatar(string arg1, byte arg2)
	{
		if (newCall("reqCreateAvatar", 0) != null)
		{
			bundle.writeUnicode(arg1);
			bundle.writeUint8(arg2);
			sendCall(null);
		}
	}

	public void reqCreatePlayer(string arg1)
	{
		if (newCall("reqCreatePlayer", 0) != null)
		{
			bundle.writeUnicode(arg1);
			sendCall(null);
		}
	}

	public void reqPlayerInfo()
	{
		if (newCall("reqPlayerInfo", 0) != null)
		{
			sendCall(null);
		}
	}

	public void reqRemoveAvatar(string arg1)
	{
		if (newCall("reqRemoveAvatar", 0) != null)
		{
			bundle.writeUnicode(arg1);
			sendCall(null);
		}
	}

	public void requestJoinTeam(ulong arg1, ulong arg2)
	{
		if (newCall("requestJoinTeam", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void requestLeaveTeam()
	{
		if (newCall("requestLeaveTeam", 0) != null)
		{
			sendCall(null);
		}
	}

	public void requestReceive(ushort arg1, ulong arg2)
	{
		if (newCall("requestReceive", 0) != null)
		{
			bundle.writeUint16(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void requestReceiveTeam(ushort arg1, ulong arg2)
	{
		if (newCall("requestReceiveTeam", 0) != null)
		{
			bundle.writeUint16(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void selectAvatarGame(ulong arg1, ulong arg2)
	{
		if (newCall("selectAvatarGame", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void sendMsg(ulong arg1, string arg2)
	{
		if (newCall("sendMsg", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeUnicode(arg2);
			sendCall(null);
		}
	}

	public void startMatch()
	{
		if (newCall("startMatch", 0) != null)
		{
			sendCall(null);
		}
	}

	public void useCDK(string arg1)
	{
		if (newCall("useCDK", 0) != null)
		{
			bundle.writeString(arg1);
			sendCall(null);
		}
	}
}
