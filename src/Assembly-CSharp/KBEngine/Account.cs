using System;
using System.Collections.Generic;
using UltimateSurvival;

namespace KBEngine
{
	// Token: 0x02000C68 RID: 3176
	public class Account : AccountBase
	{
		// Token: 0x06005681 RID: 22145 RVA: 0x0023EAF4 File Offset: 0x0023CCF4
		public override object getDefinedProperty(string name)
		{
			Property property = null;
			if (!this.defpropertys_.TryGetValue(name, out property))
			{
				return null;
			}
			return this.defpropertys_[name].defaultVal;
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0023EB28 File Offset: 0x0023CD28
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

		// Token: 0x06005683 RID: 22147 RVA: 0x0023EB79 File Offset: 0x0023CD79
		public void reqCreateAvatar(string name, byte roleType)
		{
			base.baseCall("reqCreateAvatar", new object[]
			{
				name,
				roleType
			});
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x0023EB99 File Offset: 0x0023CD99
		public override void MatchSuccess()
		{
			Event.fireOut("MatchSuccess", Array.Empty<object>());
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0023EBAA File Offset: 0x0023CDAA
		public void reqRemoveAvatar(string name)
		{
			Dbg.DEBUG_MSG("Account::reqRemoveAvatar: name=" + name);
			base.baseCall("reqRemoveAvatar", new object[]
			{
				name
			});
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x0023EBD1 File Offset: 0x0023CDD1
		public override void onReqAvatarList(ITEM_INFO_LIST infos)
		{
			Event.fireOut("onReqAvatarList", new object[]
			{
				infos
			});
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0023EBE8 File Offset: 0x0023CDE8
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

		// Token: 0x06005688 RID: 22152 RVA: 0x0023EC6C File Offset: 0x0023CE6C
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

		// Token: 0x06005689 RID: 22153 RVA: 0x0023ECBD File Offset: 0x0023CEBD
		public void selectAvatarGame(ulong AvaterType, ulong AvaterSurface)
		{
			Dbg.DEBUG_MSG("Account::selectAvatarGame: dbid=" + AvaterType);
			base.baseCall("selectAvatarGame", new object[]
			{
				AvaterType,
				AvaterSurface
			});
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x0023ECF7 File Offset: 0x0023CEF7
		public void startMatch()
		{
			base.baseCall("startMatch", Array.Empty<object>());
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0023ED09 File Offset: 0x0023CF09
		public void TeamStartMatch(ulong teamuuid)
		{
			base.baseCall("TeamStartMatch", new object[]
			{
				teamuuid
			});
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x0023ED25 File Offset: 0x0023CF25
		public void cancelMatch()
		{
			base.baseCall("cancelMatch", Array.Empty<object>());
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x0023ED38 File Offset: 0x0023CF38
		public override void goToHome(PLAYER_INFO _plyaerInfo)
		{
			this.plyaerInfo["name"] = _plyaerInfo.name;
			this.plyaerInfo["jade"] = _plyaerInfo.jade;
			this.plyaerInfo["gold"] = _plyaerInfo.gold;
			this.plyaerInfo["level"] = _plyaerInfo.level;
			Event.fireOut("goToHome", Array.Empty<object>());
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x0023EDBB File Offset: 0x0023CFBB
		public override void goToCreatePlayer()
		{
			Event.fireOut("goToCreatePlayer", Array.Empty<object>());
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x0023EDCC File Offset: 0x0023CFCC
		public void reqCreatePlayer(string name)
		{
			base.baseCall("reqCreatePlayer", new object[]
			{
				name
			});
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x0023EDE3 File Offset: 0x0023CFE3
		public override void goToSpace()
		{
			Event.fireOut("goToSpace", Array.Empty<object>());
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0023EDF4 File Offset: 0x0023CFF4
		public void rejoinSpace()
		{
			base.baseCall("rejoinSpace", Array.Empty<object>());
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x0023EE06 File Offset: 0x0023D006
		public void buyShopItem(ulong itemUUID)
		{
			base.baseCall("buyShopItem", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x0023EE22 File Offset: 0x0023D022
		public void getShopList(uint listID)
		{
			base.baseCall("getShopList", new object[]
			{
				listID
			});
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0023EE3E File Offset: 0x0023D03E
		public override void onReqShopList(ITEM_INFO_LIST infos, string shopPrice)
		{
			Event.fireOut("onReqShopList", new object[]
			{
				infos,
				shopPrice
			});
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0023EE58 File Offset: 0x0023D058
		public override void buySuccess(ITEM_INFO_LIST infos)
		{
			this.itemList = infos;
			Event.fireOut("HomeErrorMessage", new object[]
			{
				"购买成功"
			});
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x0023EE79 File Offset: 0x0023D079
		public override void HomeErrorMessage(string msg)
		{
			Event.fireOut("HomeErrorMessage", new object[]
			{
				msg
			});
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x0023EE8F File Offset: 0x0023D08F
		public void UseItem(ulong itemUUID)
		{
			base.baseCall("UseItem", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0023EEAB File Offset: 0x0023D0AB
		public override void createOder(string info)
		{
			Event.fireOut("createOder", new object[]
			{
				info
			});
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0023EEC1 File Offset: 0x0023D0C1
		public void onPayEnd()
		{
			base.baseCall("onPayEnd", Array.Empty<object>());
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x0023EED3 File Offset: 0x0023D0D3
		public override void requestOnlineFriend(FRIEND_INFO_LIST infos)
		{
			Event.fireOut("requestOnlineFriend", new object[]
			{
				infos
			});
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x0023EEE9 File Offset: 0x0023D0E9
		public override void getTalkingMsg(FRIEND_INFO Info, string msg)
		{
			Event.fireOut("getTalkingMsg", new object[]
			{
				Info,
				msg
			});
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x0023EF03 File Offset: 0x0023D103
		public void sendMsg(ulong playerDBid, string msg)
		{
			base.baseCall("sendMsg", new object[]
			{
				playerDBid,
				msg
			});
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x0023EF23 File Offset: 0x0023D123
		public void getOnlineFriend()
		{
			base.baseCall("getOnlineFriend", Array.Empty<object>());
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x0023EF35 File Offset: 0x0023D135
		public void addFriend(string friendname)
		{
			base.baseCall("addFriend", new object[]
			{
				friendname
			});
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x0023EF4C File Offset: 0x0023D14C
		public override void addFriendSuccess(FRIEND_INFO Info)
		{
			Event.fireOut("HomeErrorMessage", new object[]
			{
				"添加成功"
			});
			Event.fireOut("OpenFriendUI", Array.Empty<object>());
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x0023EF75 File Offset: 0x0023D175
		public override void receiveaddfriend(string friendname, ulong frienddbid)
		{
			Event.fireOut("receiveaddfriend", new object[]
			{
				friendname,
				frienddbid
			});
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x0023EF94 File Offset: 0x0023D194
		public void requestReceive(ushort choice, ulong frienddbid)
		{
			base.baseCall("requestReceive", new object[]
			{
				choice,
				frienddbid
			});
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x0023EFB9 File Offset: 0x0023D1B9
		public void createTeam(ulong friendDbid)
		{
			base.baseCall("createTeam", new object[]
			{
				friendDbid
			});
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x0023EFD5 File Offset: 0x0023D1D5
		public override void setTeamMember(string name, uint LV)
		{
			Event.fireOut("setTeamMember", new object[]
			{
				name,
				LV
			});
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x0023EFF4 File Offset: 0x0023D1F4
		public override void setAllTeamMember(string jsonInfo, ulong teamuuid)
		{
			Event.fireOut("setAllTeamMember", new object[]
			{
				jsonInfo,
				teamuuid
			});
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x0023F013 File Offset: 0x0023D213
		public void requestLeaveTeam()
		{
			base.baseCall("requestLeaveTeam", Array.Empty<object>());
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x0023F025 File Offset: 0x0023D225
		public override void receiveaddTeam(string friendname, ulong teamuuid, ulong frienddbid)
		{
			Event.fireOut("receiveaddTeam", new object[]
			{
				friendname,
				teamuuid,
				frienddbid
			});
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x0023F04D File Offset: 0x0023D24D
		public void requestReceiveTeam(ushort choice, ulong frienddbid)
		{
			base.baseCall("requestReceiveTeam", new object[]
			{
				choice,
				frienddbid
			});
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x0023F072 File Offset: 0x0023D272
		public void requestJoinTeam(ulong teamuuid, ulong frienddbid)
		{
			base.baseCall("requestJoinTeam", new object[]
			{
				teamuuid,
				frienddbid
			});
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x0023F098 File Offset: 0x0023D298
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

		// Token: 0x060056AA RID: 22186 RVA: 0x0023F0F9 File Offset: 0x0023D2F9
		public override void createItem(ITEM_INFO info)
		{
			this.itemList.values.Add(info);
			Event.fireOut("openCollect", Array.Empty<object>());
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x0023F11C File Offset: 0x0023D31C
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

		// Token: 0x060056AC RID: 22188 RVA: 0x0023F1AC File Offset: 0x0023D3AC
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

		// Token: 0x060056AD RID: 22189 RVA: 0x0023F20F File Offset: 0x0023D40F
		public void useCDK(string cdk)
		{
			base.baseCall("useCDK", new object[]
			{
				cdk
			});
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0023F226 File Offset: 0x0023D426
		public void CheckIn(ushort UUID)
		{
			base.baseCall("CheckIn", new object[]
			{
				UUID
			});
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x0023F244 File Offset: 0x0023D444
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

		// Token: 0x060056B0 RID: 22192 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public override void leaveTeam()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public override void onHelloTestBacke()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public override void onStartGame()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005128 RID: 20776
		public Dictionary<ulong, AVATAR_INFO> avatars = new Dictionary<ulong, AVATAR_INFO>();

		// Token: 0x04005129 RID: 20777
		public string name = "";

		// Token: 0x0400512A RID: 20778
		public int gold;

		// Token: 0x0400512B RID: 20779
		public int soul;

		// Token: 0x0400512C RID: 20780
		public Dictionary<string, object> plyaerInfo = new Dictionary<string, object>();

		// Token: 0x0400512D RID: 20781
		public Dictionary<string, ulong> dic_name_to_dbid = new Dictionary<string, ulong>();

		// Token: 0x0400512E RID: 20782
		private Dictionary<string, Property> defpropertys_ = new Dictionary<string, Property>();
	}
}
