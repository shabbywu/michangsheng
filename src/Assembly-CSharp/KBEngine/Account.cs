using System;
using System.Collections.Generic;
using UltimateSurvival;

namespace KBEngine
{
	// Token: 0x02000FF3 RID: 4083
	public class Account : AccountBase
	{
		// Token: 0x060060D0 RID: 24784 RVA: 0x0026B450 File Offset: 0x00269650
		public override object getDefinedProperty(string name)
		{
			Property property = null;
			if (!this.defpropertys_.TryGetValue(name, out property))
			{
				return null;
			}
			return this.defpropertys_[name].defaultVal;
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x0026B484 File Offset: 0x00269684
		public override void __init__()
		{
			Event.fireOut("onLoginSuccessfully", new object[]
			{
				KBEngineApp.app.entity_uuid,
				this.id,
				this
			});
			base.baseCall("reqPlayerInfo", Array.Empty<object>());
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x000431D1 File Offset: 0x000413D1
		public void reqCreateAvatar(string name, byte roleType)
		{
			base.baseCall("reqCreateAvatar", new object[]
			{
				name,
				roleType
			});
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x000431F1 File Offset: 0x000413F1
		public override void MatchSuccess()
		{
			Event.fireOut("MatchSuccess", Array.Empty<object>());
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x00043202 File Offset: 0x00041402
		public void reqRemoveAvatar(string name)
		{
			Dbg.DEBUG_MSG("Account::reqRemoveAvatar: name=" + name);
			base.baseCall("reqRemoveAvatar", new object[]
			{
				name
			});
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x00043229 File Offset: 0x00041429
		public override void onReqAvatarList(ITEM_INFO_LIST infos)
		{
			Event.fireOut("onReqAvatarList", new object[]
			{
				infos
			});
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x0026B4D8 File Offset: 0x002696D8
		public override void onCreateAvatarResult(byte retcode, AVATAR_INFO info)
		{
			if (retcode == 0)
			{
				this.avatars.Add(info.dbid, info);
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
			Event.fireOut("onCreateAvatarResult", new object[]
			{
				retcode,
				info,
				this.avatars
			});
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x0026B55C File Offset: 0x0026975C
		public override void onRemoveAvatar(ulong dbid)
		{
			Dbg.DEBUG_MSG("Account::onRemoveAvatar: dbid=" + dbid);
			this.avatars.Remove(dbid);
			Event.fireOut("onRemoveAvatar", new object[]
			{
				dbid,
				this.avatars
			});
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x0004323F File Offset: 0x0004143F
		public void selectAvatarGame(ulong AvaterType, ulong AvaterSurface)
		{
			Dbg.DEBUG_MSG("Account::selectAvatarGame: dbid=" + AvaterType);
			base.baseCall("selectAvatarGame", new object[]
			{
				AvaterType,
				AvaterSurface
			});
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x00043279 File Offset: 0x00041479
		public void startMatch()
		{
			base.baseCall("startMatch", Array.Empty<object>());
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x0004328B File Offset: 0x0004148B
		public void TeamStartMatch(ulong teamuuid)
		{
			base.baseCall("TeamStartMatch", new object[]
			{
				teamuuid
			});
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x000432A7 File Offset: 0x000414A7
		public void cancelMatch()
		{
			base.baseCall("cancelMatch", Array.Empty<object>());
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x0026B5B0 File Offset: 0x002697B0
		public override void goToHome(PLAYER_INFO _plyaerInfo)
		{
			this.plyaerInfo["name"] = _plyaerInfo.name;
			this.plyaerInfo["jade"] = _plyaerInfo.jade;
			this.plyaerInfo["gold"] = _plyaerInfo.gold;
			this.plyaerInfo["level"] = _plyaerInfo.level;
			Event.fireOut("goToHome", Array.Empty<object>());
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x000432B9 File Offset: 0x000414B9
		public override void goToCreatePlayer()
		{
			Event.fireOut("goToCreatePlayer", Array.Empty<object>());
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x000432CA File Offset: 0x000414CA
		public void reqCreatePlayer(string name)
		{
			base.baseCall("reqCreatePlayer", new object[]
			{
				name
			});
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x000432E1 File Offset: 0x000414E1
		public override void goToSpace()
		{
			Event.fireOut("goToSpace", Array.Empty<object>());
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x000432F2 File Offset: 0x000414F2
		public void rejoinSpace()
		{
			base.baseCall("rejoinSpace", Array.Empty<object>());
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x00043304 File Offset: 0x00041504
		public void buyShopItem(ulong itemUUID)
		{
			base.baseCall("buyShopItem", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x00043320 File Offset: 0x00041520
		public void getShopList(uint listID)
		{
			base.baseCall("getShopList", new object[]
			{
				listID
			});
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x0004333C File Offset: 0x0004153C
		public override void onReqShopList(ITEM_INFO_LIST infos, string shopPrice)
		{
			Event.fireOut("onReqShopList", new object[]
			{
				infos,
				shopPrice
			});
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x00043356 File Offset: 0x00041556
		public override void buySuccess(ITEM_INFO_LIST infos)
		{
			this.itemList = infos;
			Event.fireOut("HomeErrorMessage", new object[]
			{
				"购买成功"
			});
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x00043377 File Offset: 0x00041577
		public override void HomeErrorMessage(string msg)
		{
			Event.fireOut("HomeErrorMessage", new object[]
			{
				msg
			});
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x0004338D File Offset: 0x0004158D
		public void UseItem(ulong itemUUID)
		{
			base.baseCall("UseItem", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x000433A9 File Offset: 0x000415A9
		public override void createOder(string info)
		{
			Event.fireOut("createOder", new object[]
			{
				info
			});
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x000433BF File Offset: 0x000415BF
		public void onPayEnd()
		{
			base.baseCall("onPayEnd", Array.Empty<object>());
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x000433D1 File Offset: 0x000415D1
		public override void requestOnlineFriend(FRIEND_INFO_LIST infos)
		{
			Event.fireOut("requestOnlineFriend", new object[]
			{
				infos
			});
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x000433E7 File Offset: 0x000415E7
		public override void getTalkingMsg(FRIEND_INFO Info, string msg)
		{
			Event.fireOut("getTalkingMsg", new object[]
			{
				Info,
				msg
			});
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x00043401 File Offset: 0x00041601
		public void sendMsg(ulong playerDBid, string msg)
		{
			base.baseCall("sendMsg", new object[]
			{
				playerDBid,
				msg
			});
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x00043421 File Offset: 0x00041621
		public void getOnlineFriend()
		{
			base.baseCall("getOnlineFriend", Array.Empty<object>());
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x00043433 File Offset: 0x00041633
		public void addFriend(string friendname)
		{
			base.baseCall("addFriend", new object[]
			{
				friendname
			});
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0004344A File Offset: 0x0004164A
		public override void addFriendSuccess(FRIEND_INFO Info)
		{
			Event.fireOut("HomeErrorMessage", new object[]
			{
				"添加成功"
			});
			Event.fireOut("OpenFriendUI", Array.Empty<object>());
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x00043473 File Offset: 0x00041673
		public override void receiveaddfriend(string friendname, ulong frienddbid)
		{
			Event.fireOut("receiveaddfriend", new object[]
			{
				friendname,
				frienddbid
			});
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x00043492 File Offset: 0x00041692
		public void requestReceive(ushort choice, ulong frienddbid)
		{
			base.baseCall("requestReceive", new object[]
			{
				choice,
				frienddbid
			});
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x000434B7 File Offset: 0x000416B7
		public void createTeam(ulong friendDbid)
		{
			base.baseCall("createTeam", new object[]
			{
				friendDbid
			});
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x000434D3 File Offset: 0x000416D3
		public override void setTeamMember(string name, uint LV)
		{
			Event.fireOut("setTeamMember", new object[]
			{
				name,
				LV
			});
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x000434F2 File Offset: 0x000416F2
		public override void setAllTeamMember(string jsonInfo, ulong teamuuid)
		{
			Event.fireOut("setAllTeamMember", new object[]
			{
				jsonInfo,
				teamuuid
			});
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x00043511 File Offset: 0x00041711
		public void requestLeaveTeam()
		{
			base.baseCall("requestLeaveTeam", Array.Empty<object>());
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x00043523 File Offset: 0x00041723
		public override void receiveaddTeam(string friendname, ulong teamuuid, ulong frienddbid)
		{
			Event.fireOut("receiveaddTeam", new object[]
			{
				friendname,
				teamuuid,
				frienddbid
			});
		}

		// Token: 0x060060F6 RID: 24822 RVA: 0x0004354B File Offset: 0x0004174B
		public void requestReceiveTeam(ushort choice, ulong frienddbid)
		{
			base.baseCall("requestReceiveTeam", new object[]
			{
				choice,
				frienddbid
			});
		}

		// Token: 0x060060F7 RID: 24823 RVA: 0x00043570 File Offset: 0x00041770
		public void requestJoinTeam(ulong teamuuid, ulong frienddbid)
		{
			base.baseCall("requestJoinTeam", new object[]
			{
				teamuuid,
				frienddbid
			});
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x0026B634 File Offset: 0x00269834
		public override void addItem(ulong itemuuid, uint itemCount)
		{
			ITEM_INFO_LIST itemList = this.itemList;
			List<ITEM_INFO> values = itemList.values;
			for (int i = 0; i < values.Count; i++)
			{
				ITEM_INFO item_INFO = values[i];
				if (item_INFO.UUID == itemuuid)
				{
					item_INFO.itemCount = itemCount + item_INFO.itemCount;
				}
			}
			this.itemList = itemList;
			Event.fireOut("openCollect", Array.Empty<object>());
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x00043595 File Offset: 0x00041795
		public override void createItem(ITEM_INFO info)
		{
			this.itemList.values.Add(info);
			Event.fireOut("openCollect", Array.Empty<object>());
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0026B698 File Offset: 0x00269898
		public override void removeItem(ulong itemuuid, uint itemCount)
		{
			ITEM_INFO_LIST itemList = this.itemList;
			List<ITEM_INFO> values = itemList.values;
			for (int i = 0; i < values.Count; i++)
			{
				ITEM_INFO item_INFO = values[i];
				if (item_INFO.UUID == itemuuid)
				{
					if (item_INFO.itemCount > itemCount)
					{
						item_INFO.itemCount -= itemCount;
					}
					else if (item_INFO.itemCount == itemCount)
					{
						itemList.values.Remove(itemList.values[i]);
						break;
					}
				}
			}
			this.itemList = itemList;
			Event.fireOut("openCollect", Array.Empty<object>());
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0026B728 File Offset: 0x00269928
		public override void boxAddItem(ulong goodid, uint goodnum)
		{
			ItemData itemData;
			jsonData.instance.playerDatabase.FindItemById((int)goodid, out itemData);
			string text = string.Concat(new object[]
			{
				"获得",
				itemData.Name,
				":x",
				goodnum
			});
			Event.fireOut("HomeErrorMessage", new object[]
			{
				text
			});
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x000435B7 File Offset: 0x000417B7
		public void useCDK(string cdk)
		{
			base.baseCall("useCDK", new object[]
			{
				cdk
			});
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x000435CE File Offset: 0x000417CE
		public void CheckIn(ushort UUID)
		{
			base.baseCall("CheckIn", new object[]
			{
				UUID
			});
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0026B78C File Offset: 0x0026998C
		public override void CheckInSuccess(uint arg1)
		{
			long num = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
			int num2 = 0;
			foreach (CHECKIN_INFO checkin_INFO in this.CheckInList.values)
			{
				if ((uint)checkin_INFO.type == arg1)
				{
					CHECKIN_INFO checkin_INFO2 = checkin_INFO;
					checkin_INFO2.count += 1;
					checkin_INFO.time = (uint)num;
					num2 = 1;
				}
			}
			if (num2 == 0)
			{
				CHECKIN_INFO checkin_INFO3 = new CHECKIN_INFO();
				checkin_INFO3.type = (ushort)arg1;
				checkin_INFO3.count = 1;
				checkin_INFO3.time = (uint)num;
				this.CheckInList.values.Add(checkin_INFO3);
			}
			Event.fireOut("getCheckInList", new object[]
			{
				(int)arg1
			});
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0001C722 File Offset: 0x0001A922
		public override void leaveTeam()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x0001C722 File Offset: 0x0001A922
		public override void onHelloTestBacke()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x0001C722 File Offset: 0x0001A922
		public override void onStartGame()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005BE2 RID: 23522
		public Dictionary<ulong, AVATAR_INFO> avatars = new Dictionary<ulong, AVATAR_INFO>();

		// Token: 0x04005BE3 RID: 23523
		public string name = "";

		// Token: 0x04005BE4 RID: 23524
		public int gold;

		// Token: 0x04005BE5 RID: 23525
		public int soul;

		// Token: 0x04005BE6 RID: 23526
		public Dictionary<string, object> plyaerInfo = new Dictionary<string, object>();

		// Token: 0x04005BE7 RID: 23527
		public Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();

		// Token: 0x04005BE8 RID: 23528
		private Dictionary<string, Property> defpropertys_ = new Dictionary<string, Property>();
	}
}
