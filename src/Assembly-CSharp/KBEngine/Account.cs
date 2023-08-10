using System;
using System.Collections.Generic;

namespace KBEngine;

public class Account : AccountBase
{
	public Dictionary<ulong, AVATAR_INFO> avatars = new Dictionary<ulong, AVATAR_INFO>();

	public string name = "";

	public int gold;

	public int soul;

	public Dictionary<string, object> plyaerInfo = new Dictionary<string, object>();

	public Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();

	private Dictionary<string, Property> defpropertys_ = new Dictionary<string, Property>();

	public override object getDefinedProperty(string name)
	{
		Property value = null;
		if (!defpropertys_.TryGetValue(name, out value))
		{
			return null;
		}
		return defpropertys_[name].defaultVal;
	}

	public override void __init__()
	{
		Event.fireOut("onLoginSuccessfully", KBEngineApp.app.entity_uuid, id, this);
		baseCall("reqPlayerInfo");
	}

	public void reqCreateAvatar(string name, byte roleType)
	{
		baseCall("reqCreateAvatar", name, roleType);
	}

	public override void MatchSuccess()
	{
		Event.fireOut("MatchSuccess");
	}

	public void reqRemoveAvatar(string name)
	{
		Dbg.DEBUG_MSG("Account::reqRemoveAvatar: name=" + name);
		baseCall("reqRemoveAvatar", name);
	}

	public override void onReqAvatarList(ITEM_INFO_LIST infos)
	{
		Event.fireOut("onReqAvatarList", infos);
	}

	public override void onCreateAvatarResult(byte retcode, AVATAR_INFO info)
	{
		if (retcode == 0)
		{
			avatars.Add(info.dbid, info);
			Dbg.DEBUG_MSG("Account::onCreateAvatarResult: name=" + info.name);
		}
		else
		{
			Dbg.ERROR_MSG("Account::onCreateAvatarResult: retcode=" + retcode);
			if (retcode == 3)
			{
				Dbg.ERROR_MSG("角色数量不能超过三个！");
			}
		}
		Event.fireOut("onCreateAvatarResult", retcode, info, avatars);
	}

	public override void onRemoveAvatar(ulong dbid)
	{
		Dbg.DEBUG_MSG("Account::onRemoveAvatar: dbid=" + dbid);
		avatars.Remove(dbid);
		Event.fireOut("onRemoveAvatar", dbid, avatars);
	}

	public void selectAvatarGame(ulong AvaterType, ulong AvaterSurface)
	{
		Dbg.DEBUG_MSG("Account::selectAvatarGame: dbid=" + AvaterType);
		baseCall("selectAvatarGame", AvaterType, AvaterSurface);
	}

	public void startMatch()
	{
		baseCall("startMatch");
	}

	public void TeamStartMatch(ulong teamuuid)
	{
		baseCall("TeamStartMatch", teamuuid);
	}

	public void cancelMatch()
	{
		baseCall("cancelMatch");
	}

	public override void goToHome(PLAYER_INFO _plyaerInfo)
	{
		plyaerInfo["name"] = _plyaerInfo.name;
		plyaerInfo["jade"] = _plyaerInfo.jade;
		plyaerInfo["gold"] = _plyaerInfo.gold;
		plyaerInfo["level"] = _plyaerInfo.level;
		Event.fireOut("goToHome");
	}

	public override void goToCreatePlayer()
	{
		Event.fireOut("goToCreatePlayer");
	}

	public void reqCreatePlayer(string name)
	{
		baseCall("reqCreatePlayer", name);
	}

	public override void goToSpace()
	{
		Event.fireOut("goToSpace");
	}

	public void rejoinSpace()
	{
		baseCall("rejoinSpace");
	}

	public void buyShopItem(ulong itemUUID)
	{
		baseCall("buyShopItem", itemUUID);
	}

	public void getShopList(uint listID)
	{
		baseCall("getShopList", listID);
	}

	public override void onReqShopList(ITEM_INFO_LIST infos, string shopPrice)
	{
		Event.fireOut("onReqShopList", infos, shopPrice);
	}

	public override void buySuccess(ITEM_INFO_LIST infos)
	{
		itemList = infos;
		Event.fireOut("HomeErrorMessage", "购买成功");
	}

	public override void HomeErrorMessage(string msg)
	{
		Event.fireOut("HomeErrorMessage", msg);
	}

	public void UseItem(ulong itemUUID)
	{
		baseCall("UseItem", itemUUID);
	}

	public override void createOder(string info)
	{
		Event.fireOut("createOder", info);
	}

	public void onPayEnd()
	{
		baseCall("onPayEnd");
	}

	public override void requestOnlineFriend(FRIEND_INFO_LIST infos)
	{
		Event.fireOut("requestOnlineFriend", infos);
	}

	public override void getTalkingMsg(FRIEND_INFO Info, string msg)
	{
		Event.fireOut("getTalkingMsg", Info, msg);
	}

	public void sendMsg(ulong playerDBid, string msg)
	{
		baseCall("sendMsg", playerDBid, msg);
	}

	public void getOnlineFriend()
	{
		baseCall("getOnlineFriend");
	}

	public void addFriend(string friendname)
	{
		baseCall("addFriend", friendname);
	}

	public override void addFriendSuccess(FRIEND_INFO Info)
	{
		Event.fireOut("HomeErrorMessage", "添加成功");
		Event.fireOut("OpenFriendUI");
	}

	public override void receiveaddfriend(string friendname, ulong frienddbid)
	{
		Event.fireOut("receiveaddfriend", friendname, frienddbid);
	}

	public void requestReceive(ushort choice, ulong frienddbid)
	{
		baseCall("requestReceive", choice, frienddbid);
	}

	public void createTeam(ulong friendDbid)
	{
		baseCall("createTeam", friendDbid);
	}

	public override void setTeamMember(string name, uint LV)
	{
		Event.fireOut("setTeamMember", name, LV);
	}

	public override void setAllTeamMember(string jsonInfo, ulong teamuuid)
	{
		Event.fireOut("setAllTeamMember", jsonInfo, teamuuid);
	}

	public void requestLeaveTeam()
	{
		baseCall("requestLeaveTeam");
	}

	public override void receiveaddTeam(string friendname, ulong teamuuid, ulong frienddbid)
	{
		Event.fireOut("receiveaddTeam", friendname, teamuuid, frienddbid);
	}

	public void requestReceiveTeam(ushort choice, ulong frienddbid)
	{
		baseCall("requestReceiveTeam", choice, frienddbid);
	}

	public void requestJoinTeam(ulong teamuuid, ulong frienddbid)
	{
		baseCall("requestJoinTeam", teamuuid, frienddbid);
	}

	public override void addItem(ulong itemuuid, uint itemCount)
	{
		ITEM_INFO_LIST iTEM_INFO_LIST = itemList;
		List<ITEM_INFO> values = iTEM_INFO_LIST.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO iTEM_INFO = values[i];
			if (iTEM_INFO.UUID == itemuuid)
			{
				iTEM_INFO.itemCount = itemCount + iTEM_INFO.itemCount;
			}
		}
		itemList = iTEM_INFO_LIST;
		Event.fireOut("openCollect");
	}

	public override void createItem(ITEM_INFO info)
	{
		itemList.values.Add(info);
		Event.fireOut("openCollect");
	}

	public override void removeItem(ulong itemuuid, uint itemCount)
	{
		ITEM_INFO_LIST iTEM_INFO_LIST = itemList;
		List<ITEM_INFO> values = iTEM_INFO_LIST.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO iTEM_INFO = values[i];
			if (iTEM_INFO.UUID == itemuuid)
			{
				if (iTEM_INFO.itemCount > itemCount)
				{
					iTEM_INFO.itemCount -= itemCount;
				}
				else if (iTEM_INFO.itemCount == itemCount)
				{
					iTEM_INFO_LIST.values.Remove(iTEM_INFO_LIST.values[i]);
					break;
				}
			}
		}
		itemList = iTEM_INFO_LIST;
		Event.fireOut("openCollect");
	}

	public override void boxAddItem(ulong goodid, uint goodnum)
	{
		jsonData.instance.playerDatabase.FindItemById((int)goodid, out var itemData);
		string text = "获得" + itemData.Name + ":x" + goodnum;
		Event.fireOut("HomeErrorMessage", text);
	}

	public void useCDK(string cdk)
	{
		baseCall("useCDK", cdk);
	}

	public void CheckIn(ushort UUID)
	{
		baseCall("CheckIn", UUID);
	}

	public override void CheckInSuccess(uint arg1)
	{
		long num = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		int num2 = 0;
		foreach (CHECKIN_INFO value in CheckInList.values)
		{
			if (value.type == arg1)
			{
				value.count++;
				value.time = (uint)num;
				num2 = 1;
			}
		}
		if (num2 == 0)
		{
			CHECKIN_INFO cHECKIN_INFO = new CHECKIN_INFO();
			cHECKIN_INFO.type = (ushort)arg1;
			cHECKIN_INFO.count = 1;
			cHECKIN_INFO.time = (uint)num;
			CheckInList.values.Add(cHECKIN_INFO);
		}
		Event.fireOut("getCheckInList", (int)arg1);
	}

	public override void leaveTeam()
	{
		throw new NotImplementedException();
	}

	public override void onHelloTestBacke()
	{
		throw new NotImplementedException();
	}

	public override void onStartGame()
	{
		throw new NotImplementedException();
	}
}
