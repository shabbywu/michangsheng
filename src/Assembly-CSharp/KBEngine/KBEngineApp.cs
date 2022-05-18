using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F17 RID: 3863
	public class KBEngineApp
	{
		// Token: 0x06005CB8 RID: 23736 RVA: 0x0025E3B0 File Offset: 0x0025C5B0
		public KBEngineApp(KBEngineArgs args)
		{
			if (KBEngineApp.app != null)
			{
				throw new Exception("Only one instance of KBEngineApp!");
			}
			KBEngineApp.app = this;
			Event.outEventsImmediately = !args.isMultiThreads;
			this.initialize(args);
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x000416A1 File Offset: 0x0003F8A1
		public static KBEngineApp getSingleton()
		{
			if (KBEngineApp.app == null)
			{
				throw new Exception("Please create KBEngineApp!");
			}
			return KBEngineApp.app;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x000416BA File Offset: 0x0003F8BA
		public virtual bool initialize(KBEngineArgs args)
		{
			this._args = args;
			this._updatePlayerToServerPeroid = (float)this._args.syncPlayerMS;
			EntityDef.init();
			this.initNetwork();
			this.installEvents();
			return true;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x000416E8 File Offset: 0x0003F8E8
		private void initNetwork()
		{
			this._filter = null;
			Messages.init();
			this._networkInterface = new NetworkInterfaceTCP();
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x0025E52C File Offset: 0x0025C72C
		private void installEvents()
		{
			Event.registerIn("createAccount", this, "createAccount");
			Event.registerIn("login", this, "login");
			Event.registerIn("logout", this, "logout");
			Event.registerIn("reloginBaseapp", this, "reloginBaseapp");
			Event.registerIn("resetPassword", this, "resetPassword");
			Event.registerIn("bindAccountEmail", this, "bindAccountEmail");
			Event.registerIn("newPassword", this, "newPassword");
			Event.registerIn("_closeNetwork", this, "_closeNetwork");
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x00041702 File Offset: 0x0003F902
		public KBEngineArgs getInitArgs()
		{
			return this._args;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x0004170A File Offset: 0x0003F90A
		public virtual void destroy()
		{
			Dbg.WARNING_MSG("KBEngine::destroy()");
			if (this.currserver == "baseapp")
			{
				this.logout();
			}
			this.reset();
			Event.deregisterIn(this);
			this.resetMessages();
			KBEngineApp.app = null;
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x00041747 File Offset: 0x0003F947
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x0004174F File Offset: 0x0003F94F
		public byte[] serverdatas()
		{
			return this._serverdatas;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00041757 File Offset: 0x0003F957
		public void entityServerPos(Vector3 pos)
		{
			this._entityServerPos = pos;
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00041760 File Offset: 0x0003F960
		public void resetMessages()
		{
			this._serverErrs.Clear();
			Messages.clear();
			EntityDef.reset();
			Entity.clear();
			Dbg.DEBUG_MSG("KBEngine::resetMessages()");
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x0025E5C4 File Offset: 0x0025C7C4
		public virtual void reset()
		{
			Event.clearFiredEvents();
			this.clearEntities(true);
			this.currserver = "";
			this.currstate = "";
			this._serverdatas = new byte[0];
			this.serverVersion = "";
			this.serverScriptVersion = "";
			this.entity_uuid = 0UL;
			this.entity_id = 0;
			this.entity_type = "";
			this._entityIDAliasIDList.Clear();
			this._bufferedCreateEntityMessages.Clear();
			this._lastTickTime = DateTime.Now;
			this._lastTickCBTime = DateTime.Now;
			this._lastUpdateToServerTime = DateTime.Now;
			this.spaceID = 0U;
			this.spaceResPath = "";
			this.isLoadedGeometry = false;
			if (this._networkInterface != null)
			{
				this._networkInterface.reset();
			}
			this._filter = null;
			this._networkInterface = new NetworkInterfaceTCP();
			this._spacedatas.Clear();
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00041787 File Offset: 0x0003F987
		public static bool validEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)\r\n\t\t\t\t|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x00041794 File Offset: 0x0003F994
		public virtual void process()
		{
			if (this._networkInterface != null)
			{
				this._networkInterface.process();
			}
			Event.processInEvents();
			this.sendTick();
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x0025E6B0 File Offset: 0x0025C8B0
		public Entity player()
		{
			Entity result;
			if (this.entities.TryGetValue(this.entity_id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x000417B4 File Offset: 0x0003F9B4
		public void _closeNetwork(NetworkInterfaceBase networkInterface)
		{
			networkInterface.close();
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x0025E6D8 File Offset: 0x0025C8D8
		public void sendTick()
		{
			if (this._networkInterface == null || !this._networkInterface.connected)
			{
				return;
			}
			TimeSpan timeSpan = DateTime.Now - this._lastTickTime;
			this.updatePlayerToServer();
			if (this._args.serverHeartbeatTick > 0 && timeSpan.Seconds > this._args.serverHeartbeatTick)
			{
				if ((this._lastTickCBTime - this._lastTickTime).Seconds < 0)
				{
					Dbg.ERROR_MSG("sendTick: Receive appTick timeout!");
					this._networkInterface.close();
					return;
				}
				Message message = null;
				Message message2 = null;
				Messages.messages.TryGetValue("Loginapp_onClientActiveTick", out message);
				Messages.messages.TryGetValue("Baseapp_onClientActiveTick", out message2);
				if (this.currserver == "loginapp")
				{
					if (message != null)
					{
						Bundle bundle = ObjectPool<Bundle>.createObject();
						bundle.newMessage(Messages.messages["Loginapp_onClientActiveTick"]);
						bundle.send(this._networkInterface);
					}
				}
				else if (message2 != null)
				{
					Bundle bundle2 = ObjectPool<Bundle>.createObject();
					bundle2.newMessage(Messages.messages["Baseapp_onClientActiveTick"]);
					bundle2.send(this._networkInterface);
				}
				this._lastTickTime = DateTime.Now;
			}
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x000417BC File Offset: 0x0003F9BC
		public void Client_onAppActiveTickCB()
		{
			this._lastTickCBTime = DateTime.Now;
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x0025E804 File Offset: 0x0025CA04
		public void hello()
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			if (this.currserver == "loginapp")
			{
				bundle.newMessage(Messages.messages["Loginapp_hello"]);
			}
			else
			{
				bundle.newMessage(Messages.messages["Baseapp_hello"]);
			}
			this._filter = null;
			if (this._args.networkEncryptType == KBEngineApp.NETWORK_ENCRYPT_TYPE.ENCRYPT_TYPE_BLOWFISH)
			{
				this._filter = new BlowfishFilter();
				this._encryptedKey = ((BlowfishFilter)this._filter).key();
				this._networkInterface.setFilter(null);
			}
			bundle.writeString(this.clientVersion);
			bundle.writeString(this.clientScriptVersion);
			bundle.writeBlob(this._encryptedKey);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x0025E8C8 File Offset: 0x0025CAC8
		public void Client_onHelloCB(MemoryStream stream)
		{
			string text = stream.readString();
			this.serverScriptVersion = stream.readString();
			stream.readString();
			string text2 = stream.readString();
			int num = stream.readInt32();
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onHelloCB: verInfo(",
				text,
				"), scriptVersion(",
				this.serverScriptVersion,
				"), srvProtocolMD5(",
				this.serverProtocolMD5,
				"), srvEntitydefMD5(",
				this.serverEntitydefMD5,
				"), + ctype(",
				num,
				")!"
			}));
			if (text != "Getting")
			{
				this.serverVersion = text;
				if (this.serverEntitydefMD5 != text2)
				{
					Dbg.ERROR_MSG(string.Concat(new string[]
					{
						"Client_onHelloCB: digest not match! serverEntitydefMD5=",
						this.serverEntitydefMD5,
						"(server: ",
						text2,
						")"
					}));
					Event.fireAll("onVersionNotMatch", new object[]
					{
						this.clientVersion,
						this.serverVersion
					});
					return;
				}
			}
			if (this._args.networkEncryptType == KBEngineApp.NETWORK_ENCRYPT_TYPE.ENCRYPT_TYPE_BLOWFISH)
			{
				this._networkInterface.setFilter(this._filter);
				this._filter = null;
			}
			this.onServerDigest();
			if (this.currserver == "baseapp")
			{
				this.onLogin_baseapp();
				return;
			}
			this.onLogin_loginapp();
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x000042DD File Offset: 0x000024DD
		public void Client_onImportServerErrorsDescr(MemoryStream stream)
		{
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x000042DD File Offset: 0x000024DD
		public void Client_onImportClientMessages(MemoryStream stream)
		{
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x000042DD File Offset: 0x000024DD
		public void Client_onImportClientEntityDef(MemoryStream stream)
		{
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x0025EA30 File Offset: 0x0025CC30
		public void Client_onImportClientSDK(MemoryStream stream)
		{
			int num = stream.readInt32();
			string text = stream.readString();
			int num2 = stream.readInt32();
			byte[] array = new byte[0];
			array = stream.readBlob();
			Event.fireIn("onImportClientSDK", new object[]
			{
				num,
				text,
				num2,
				array
			});
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x0025EA90 File Offset: 0x0025CC90
		public void Client_onVersionNotMatch(MemoryStream stream)
		{
			this.serverVersion = stream.readString();
			Dbg.ERROR_MSG(string.Concat(new string[]
			{
				"Client_onVersionNotMatch: verInfo=",
				this.clientVersion,
				"(server: ",
				this.serverVersion,
				")"
			}));
			Event.fireAll("onVersionNotMatch", new object[]
			{
				this.clientVersion,
				this.serverVersion
			});
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x0025EB08 File Offset: 0x0025CD08
		public void Client_onScriptVersionNotMatch(MemoryStream stream)
		{
			this.serverScriptVersion = stream.readString();
			Dbg.ERROR_MSG(string.Concat(new string[]
			{
				"Client_onScriptVersionNotMatch: verInfo=",
				this.clientScriptVersion,
				"(server: ",
				this.serverScriptVersion,
				")"
			}));
			Event.fireAll("onScriptVersionNotMatch", new object[]
			{
				this.clientScriptVersion,
				this.serverScriptVersion
			});
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x0025EB80 File Offset: 0x0025CD80
		public void Client_onKicked(ushort failedcode)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"Client_onKicked: failedcode=",
				failedcode,
				"(",
				this.serverErr(failedcode),
				")"
			}));
			Event.fireAll("onKicked", new object[]
			{
				failedcode
			});
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x000417C9 File Offset: 0x0003F9C9
		public void login(string username, string password, byte[] datas)
		{
			KBEngineApp.app.username = username;
			KBEngineApp.app.password = password;
			KBEngineApp.app._clientdatas = datas;
			KBEngineApp.app.login_loginapp(true);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x0025EBE4 File Offset: 0x0025CDE4
		public void login_loginapp(bool noconnect)
		{
			if (noconnect)
			{
				this.reset();
				this._networkInterface.connectTo(this._args.ip, this._args.port, new NetworkInterfaceBase.ConnectCallback(this.onConnectTo_loginapp_callback), null);
				return;
			}
			Dbg.DEBUG_MSG("KBEngine::login_loginapp(): send login! username=" + this.username);
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Loginapp_login"]);
			bundle.writeInt8((sbyte)this._args.clientType);
			bundle.writeBlob(KBEngineApp.app._clientdatas);
			bundle.writeString(this.username);
			bundle.writeString(this.password);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0025ECA0 File Offset: 0x0025CEA0
		private void onConnectTo_loginapp_callback(string ip, int port, bool success, object userData)
		{
			this._lastTickCBTime = DateTime.Now;
			if (!success)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::login_loginapp(): connect {0}:{1} error!", ip, port));
				return;
			}
			this.currserver = "loginapp";
			this.currstate = "login";
			Dbg.DEBUG_MSG(string.Format("KBEngine::login_loginapp(): connect {0}:{1} success!", ip, port));
			this.hello();
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x000417F7 File Offset: 0x0003F9F7
		private void onLogin_loginapp()
		{
			this._lastTickCBTime = DateTime.Now;
			this.login_loginapp(false);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x0025ED04 File Offset: 0x0025CF04
		public void login_baseapp(bool noconnect)
		{
			if (!noconnect)
			{
				Bundle bundle = ObjectPool<Bundle>.createObject();
				bundle.newMessage(Messages.messages["Baseapp_loginBaseapp"]);
				bundle.writeString(this.username);
				bundle.writeString(this.password);
				bundle.send(this._networkInterface);
				return;
			}
			Event.fireOut("onLoginBaseapp", Array.Empty<object>());
			this._networkInterface.reset();
			if (this._args.forceDisableUDP || this.baseappUdpPort == 0)
			{
				this._networkInterface = new NetworkInterfaceTCP();
				this._networkInterface.connectTo(this.baseappIP, (int)this.baseappTcpPort, new NetworkInterfaceBase.ConnectCallback(this.onConnectTo_baseapp_callback), null);
				return;
			}
			this._networkInterface = new NetworkInterfaceKCP();
			this._networkInterface.connectTo(this.baseappIP, (int)this.baseappUdpPort, new NetworkInterfaceBase.ConnectCallback(this.onConnectTo_baseapp_callback), null);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x0025EDE4 File Offset: 0x0025CFE4
		private void onConnectTo_baseapp_callback(string ip, int port, bool success, object userData)
		{
			this._lastTickCBTime = DateTime.Now;
			if (!success)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::login_baseapp(): connect {0}:{1} error!", ip, port));
				return;
			}
			this.currserver = "baseapp";
			this.currstate = "";
			Dbg.DEBUG_MSG(string.Format("KBEngine::login_baseapp(): connect {0}:{1} success!", ip, port));
			this.hello();
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x0004180B File Offset: 0x0003FA0B
		private void onLogin_baseapp()
		{
			this._lastTickCBTime = DateTime.Now;
			this.login_baseapp(false);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0025EE48 File Offset: 0x0025D048
		public void reloginBaseapp()
		{
			this._lastTickTime = DateTime.Now;
			this._lastTickCBTime = DateTime.Now;
			if (this._networkInterface.valid())
			{
				return;
			}
			Event.fireAll("onReloginBaseapp", Array.Empty<object>());
			this._networkInterface.reset();
			if (this._args.forceDisableUDP || this.baseappUdpPort == 0)
			{
				this._networkInterface = new NetworkInterfaceTCP();
				this._networkInterface.connectTo(this.baseappIP, (int)this.baseappTcpPort, new NetworkInterfaceBase.ConnectCallback(this.onReConnectTo_baseapp_callback), null);
				return;
			}
			this._networkInterface = new NetworkInterfaceKCP();
			this._networkInterface.connectTo(this.baseappIP, (int)this.baseappUdpPort, new NetworkInterfaceBase.ConnectCallback(this.onReConnectTo_baseapp_callback), null);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0025EF08 File Offset: 0x0025D108
		private void onReConnectTo_baseapp_callback(string ip, int port, bool success, object userData)
		{
			if (!success)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::reloginBaseapp(): connect {0}:{1} error!", ip, port));
				return;
			}
			Dbg.DEBUG_MSG(string.Format("KBEngine::relogin_baseapp(): connect {0}:{1} success!", ip, port));
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_reloginBaseapp"]);
			bundle.writeString(this.username);
			bundle.writeString(this.password);
			bundle.writeUint64(this.entity_uuid);
			bundle.writeInt32(this.entity_id);
			bundle.send(this._networkInterface);
			this._lastTickCBTime = DateTime.Now;
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x0004181F File Offset: 0x0003FA1F
		public void logout()
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_logoutBaseapp"]);
			bundle.writeUint64(this.entity_uuid);
			bundle.writeInt32(this.entity_id);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0004185E File Offset: 0x0003FA5E
		public string serverErr(ushort id)
		{
			return this._serverErrs.serverErrStr(id);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x0004186C File Offset: 0x0003FA6C
		public void onOpenLoginapp_resetpassword()
		{
			Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_resetpassword: successfully!");
			this.currserver = "loginapp";
			this.currstate = "resetpassword";
			this._lastTickCBTime = DateTime.Now;
			this.resetpassword_loginapp(false);
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x000418A0 File Offset: 0x0003FAA0
		public void resetPassword(string username)
		{
			KBEngineApp.app.username = username;
			this.resetpassword_loginapp(true);
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0025EFA8 File Offset: 0x0025D1A8
		public void resetpassword_loginapp(bool noconnect)
		{
			if (noconnect)
			{
				this.reset();
				this._networkInterface.connectTo(this._args.ip, this._args.port, new NetworkInterfaceBase.ConnectCallback(this.onConnectTo_resetpassword_callback), null);
				return;
			}
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Loginapp_reqAccountResetPassword"]);
			bundle.writeString(this.username);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0025F020 File Offset: 0x0025D220
		private void onConnectTo_resetpassword_callback(string ip, int port, bool success, object userData)
		{
			this._lastTickCBTime = DateTime.Now;
			if (!success)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::resetpassword_loginapp(): connect {0}:{1} error!", ip, port));
				return;
			}
			Dbg.DEBUG_MSG(string.Format("KBEngine::resetpassword_loginapp(): connect {0}:{1} success!", ip, port));
			this.onOpenLoginapp_resetpassword();
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0025F070 File Offset: 0x0025D270
		public void Client_onReqAccountResetPasswordCB(ushort failcode)
		{
			if (failcode != 0)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					"KBEngine::Client_onReqAccountResetPasswordCB: ",
					this.username,
					" failed! code=",
					failcode,
					"(",
					this.serverErr(failcode),
					")!"
				}));
				return;
			}
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountResetPasswordCB: " + this.username + " success!");
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x0025F0E8 File Offset: 0x0025D2E8
		public void bindAccountEmail(string emailAddress)
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_reqAccountBindEmail"]);
			bundle.writeInt32(this.entity_id);
			bundle.writeString(this.password);
			bundle.writeString(emailAddress);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x0025F13C File Offset: 0x0025D33C
		public void Client_onReqAccountBindEmailCB(ushort failcode)
		{
			if (failcode != 0)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					"KBEngine::Client_onReqAccountBindEmailCB: ",
					this.username,
					" failed! code=",
					failcode,
					"(",
					this.serverErr(failcode),
					")!"
				}));
				return;
			}
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountBindEmailCB: " + this.username + " success!");
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0025F1B4 File Offset: 0x0025D3B4
		public void newPassword(string old_password, string new_password)
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_reqAccountNewPassword"]);
			bundle.writeInt32(this.entity_id);
			bundle.writeString(old_password);
			bundle.writeString(new_password);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x0025F200 File Offset: 0x0025D400
		public void Client_onReqAccountNewPasswordCB(ushort failcode)
		{
			if (failcode != 0)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					"KBEngine::Client_onReqAccountNewPasswordCB: ",
					this.username,
					" failed! code=",
					failcode,
					"(",
					this.serverErr(failcode),
					")!"
				}));
				return;
			}
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountNewPasswordCB: " + this.username + " success!");
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x000418B4 File Offset: 0x0003FAB4
		public void createAccount(string username, string password, byte[] datas)
		{
			KBEngineApp.app.username = username;
			KBEngineApp.app.password = password;
			KBEngineApp.app._clientdatas = datas;
			KBEngineApp.app.createAccount_loginapp(true);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x0025F278 File Offset: 0x0025D478
		public void createAccount_loginapp(bool noconnect)
		{
			if (noconnect)
			{
				this.reset();
				this._networkInterface.connectTo(this._args.ip, this._args.port, new NetworkInterfaceBase.ConnectCallback(this.onConnectTo_createAccount_callback), null);
				return;
			}
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Loginapp_reqCreateAccount"]);
			bundle.writeString(this.username);
			bundle.writeString(this.password);
			bundle.writeBlob(KBEngineApp.app._clientdatas);
			bundle.send(this._networkInterface);
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x000418E2 File Offset: 0x0003FAE2
		public void onOpenLoginapp_createAccount()
		{
			Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_createAccount: successfully!");
			this.currserver = "loginapp";
			this.currstate = "createAccount";
			this._lastTickCBTime = DateTime.Now;
			this.createAccount_loginapp(false);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x0025F30C File Offset: 0x0025D50C
		private void onConnectTo_createAccount_callback(string ip, int port, bool success, object userData)
		{
			this._lastTickCBTime = DateTime.Now;
			if (!success)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::createAccount_loginapp(): connect {0}:{1} error!", ip, port));
				return;
			}
			Dbg.DEBUG_MSG(string.Format("KBEngine::createAccount_loginapp(): connect {0}:{1} success!", ip, port));
			this.onOpenLoginapp_createAccount();
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x000042DD File Offset: 0x000024DD
		public void onServerDigest()
		{
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0025F35C File Offset: 0x0025D55C
		public void Client_onLoginFailed(MemoryStream stream)
		{
			ushort num = stream.readUint16();
			this._serverdatas = stream.readBlob();
			Dbg.ERROR_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onLoginFailed: failedcode(",
				num,
				":",
				this.serverErr(num),
				"), datas(",
				this._serverdatas.Length,
				")!"
			}));
			Event.fireAll("onLoginFailed", new object[]
			{
				num
			});
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x0025F3E8 File Offset: 0x0025D5E8
		public void Client_onLoginSuccessfully(MemoryStream stream)
		{
			string text = stream.readString();
			this.username = text;
			this.baseappIP = stream.readString();
			this.baseappTcpPort = stream.readUint16();
			this.baseappUdpPort = stream.readUint16();
			this._serverdatas = stream.readBlob();
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onLoginSuccessfully: accountName(",
				text,
				"), addr(",
				this.baseappIP,
				":",
				this.baseappTcpPort,
				"|",
				this.baseappUdpPort,
				"), datas(",
				this._serverdatas.Length,
				")!"
			}));
			this.login_baseapp(true);
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x0025F4B8 File Offset: 0x0025D6B8
		public void Client_onLoginBaseappFailed(ushort failedcode)
		{
			Dbg.ERROR_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onLoginBaseappFailed: failedcode=",
				failedcode,
				"(",
				this.serverErr(failedcode),
				")!"
			}));
			Event.fireAll("onLoginBaseappFailed", new object[]
			{
				failedcode
			});
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x0025F51C File Offset: 0x0025D71C
		public void Client_onReloginBaseappFailed(ushort failedcode)
		{
			Dbg.ERROR_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onReloginBaseappFailed: failedcode=",
				failedcode,
				"(",
				this.serverErr(failedcode),
				")!"
			}));
			Event.fireAll("onReloginBaseappFailed", new object[]
			{
				failedcode
			});
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00041916 File Offset: 0x0003FB16
		public void Client_onReloginBaseappSuccessfully(MemoryStream stream)
		{
			this.entity_uuid = stream.readUint64();
			Dbg.DEBUG_MSG("KBEngine::Client_onReloginBaseappSuccessfully: name(" + this.username + ")!");
			Event.fireAll("onReloginBaseappSuccessfully", Array.Empty<object>());
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0025F580 File Offset: 0x0025D780
		public void Client_onCreatedProxies(ulong rndUUID, int eid, string entityType)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_onCreatedProxies: eid(",
				eid,
				"), entityType(",
				entityType,
				")!"
			}));
			this.entity_uuid = rndUUID;
			this.entity_id = eid;
			this.entity_type = entityType;
			if (!this.entities.ContainsKey(eid))
			{
				ScriptModule scriptModule = null;
				if (!EntityDef.moduledefs.TryGetValue(entityType, out scriptModule))
				{
					Dbg.ERROR_MSG("KBEngine::Client_onCreatedProxies: not found module(" + entityType + ")!");
					return;
				}
				Type entityScript = scriptModule.entityScript;
				if (entityScript == null)
				{
					return;
				}
				Entity entity = (Entity)Activator.CreateInstance(entityScript);
				entity.id = eid;
				entity.className = entityType;
				entity.onGetBase();
				this.entities[eid] = entity;
				MemoryStream memoryStream = null;
				this._bufferedCreateEntityMessages.TryGetValue(eid, out memoryStream);
				if (memoryStream != null)
				{
					this.Client_onUpdatePropertys(memoryStream);
					this._bufferedCreateEntityMessages.Remove(eid);
					memoryStream.reclaimObject();
				}
				entity.__init__();
				entity.attachComponents();
				entity.inited = true;
				if (this._args.isOnInitCallPropertysSetMethods)
				{
					entity.callPropertysSetMethods();
					return;
				}
			}
			else
			{
				MemoryStream memoryStream2 = null;
				this._bufferedCreateEntityMessages.TryGetValue(eid, out memoryStream2);
				if (memoryStream2 != null)
				{
					this.Client_onUpdatePropertys(memoryStream2);
					this._bufferedCreateEntityMessages.Remove(eid);
					memoryStream2.reclaimObject();
				}
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0025F6D4 File Offset: 0x0025D8D4
		public Entity findEntity(int entityID)
		{
			Entity result = null;
			if (!this.entities.TryGetValue(entityID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x0025F6F8 File Offset: 0x0025D8F8
		public int getViewEntityIDFromStream(MemoryStream stream)
		{
			if (!this._args.useAliasEntityID)
			{
				return stream.readInt32();
			}
			int result;
			if (this._entityIDAliasIDList.Count > 255)
			{
				result = stream.readInt32();
			}
			else
			{
				byte b = stream.readUint8();
				if (this._entityIDAliasIDList.Count <= (int)b)
				{
					return 0;
				}
				result = this._entityIDAliasIDList[(int)b];
			}
			return result;
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0025F75C File Offset: 0x0025D95C
		public void Client_onUpdatePropertysOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			this.onUpdatePropertys_(viewEntityIDFromStream, stream);
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x0025F77C File Offset: 0x0025D97C
		public void Client_onUpdatePropertys(MemoryStream stream)
		{
			int eid = stream.readInt32();
			this.onUpdatePropertys_(eid, stream);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x0025F798 File Offset: 0x0025D998
		public void onUpdatePropertys_(int eid, MemoryStream stream)
		{
			Entity entity = null;
			if (this.entities.TryGetValue(eid, out entity))
			{
				entity.onUpdatePropertys(stream);
				return;
			}
			MemoryStream memoryStream = null;
			if (this._bufferedCreateEntityMessages.TryGetValue(eid, out memoryStream))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onUpdatePropertys: entity(" + eid + ") not found!");
				return;
			}
			MemoryStream memoryStream2 = ObjectPool<MemoryStream>.createObject();
			memoryStream2.wpos = stream.wpos;
			memoryStream2.rpos = stream.rpos - 4;
			Array.Copy(stream.data(), memoryStream2.data(), stream.wpos);
			this._bufferedCreateEntityMessages[eid] = memoryStream2;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x0025F830 File Offset: 0x0025DA30
		public void Client_onRemoteMethodCallOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			this.onRemoteMethodCall_(viewEntityIDFromStream, stream);
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x0025F850 File Offset: 0x0025DA50
		public void Client_onRemoteMethodCall(MemoryStream stream)
		{
			int eid = stream.readInt32();
			this.onRemoteMethodCall_(eid, stream);
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x0025F86C File Offset: 0x0025DA6C
		public void onRemoteMethodCall_(int eid, MemoryStream stream)
		{
			Entity entity = null;
			if (!this.entities.TryGetValue(eid, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onRemoteMethodCall: entity(" + eid + ") not found!");
				return;
			}
			entity.onRemoteMethodCall(stream);
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x0025F8B0 File Offset: 0x0025DAB0
		public void Client_onEntityEnterWorld(MemoryStream stream)
		{
			int num = stream.readInt32();
			if (this.entity_id > 0 && this.entity_id != num)
			{
				this._entityIDAliasIDList.Add(num);
			}
			ushort key;
			if (EntityDef.idmoduledefs.Count > 255)
			{
				key = stream.readUint16();
			}
			else
			{
				key = (ushort)stream.readUint8();
			}
			sbyte b = 1;
			if (stream.length() > 0U)
			{
				b = stream.readInt8();
			}
			string name = EntityDef.idmoduledefs[key].name;
			Entity entity = null;
			if (!this.entities.TryGetValue(num, out entity))
			{
				MemoryStream memoryStream = null;
				if (!this._bufferedCreateEntityMessages.TryGetValue(num, out memoryStream))
				{
					Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterWorld: entity(" + num + ") not found!");
					return;
				}
				ScriptModule scriptModule = null;
				if (!EntityDef.moduledefs.TryGetValue(name, out scriptModule))
				{
					Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterWorld: not found module(" + name + ")!");
				}
				Type entityScript = scriptModule.entityScript;
				if (entityScript == null)
				{
					return;
				}
				entity = (Entity)Activator.CreateInstance(entityScript);
				entity.id = num;
				entity.className = name;
				entity.onGetCell();
				this.entities[num] = entity;
				this.Client_onUpdatePropertys(memoryStream);
				this._bufferedCreateEntityMessages.Remove(num);
				memoryStream.reclaimObject();
				entity.isOnGround = (b > 0);
				entity.onDirectionChanged(entity.direction);
				entity.onPositionChanged(entity.position);
				entity.__init__();
				entity.attachComponents();
				entity.inited = true;
				entity.inWorld = true;
				entity.enterWorld();
				if (this._args.isOnInitCallPropertysSetMethods)
				{
					entity.callPropertysSetMethods();
					return;
				}
			}
			else if (!entity.inWorld)
			{
				this._entityIDAliasIDList.Clear();
				this.clearEntities(false);
				this.entities[entity.id] = entity;
				entity.onGetCell();
				entity.onDirectionChanged(entity.direction);
				entity.onPositionChanged(entity.position);
				this._entityServerPos = entity.position;
				entity.isOnGround = (b > 0);
				entity.inWorld = true;
				entity.enterWorld();
				if (this._args.isOnInitCallPropertysSetMethods)
				{
					entity.callPropertysSetMethods();
				}
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x0025FAE8 File Offset: 0x0025DCE8
		public void Client_onEntityLeaveWorldOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			KBEngineApp.app.Client_onEntityLeaveWorld(viewEntityIDFromStream);
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x0025FB08 File Offset: 0x0025DD08
		public void Client_onEntityLeaveWorld(int eid)
		{
			Entity entity = null;
			if (!this.entities.TryGetValue(eid, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityLeaveWorld: entity(" + eid + ") not found!");
				return;
			}
			if (entity.inWorld)
			{
				entity.leaveWorld();
			}
			if (this.entity_id == eid)
			{
				this.clearSpace(false);
				entity.onLoseCell();
				return;
			}
			if (this._controlledEntities.Remove(entity))
			{
				Event.fireOut("onLoseControlledEntity", new object[]
				{
					entity
				});
			}
			this.entities.Remove(eid);
			entity.destroy();
			this._entityIDAliasIDList.Remove(eid);
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x0025FBAC File Offset: 0x0025DDAC
		public void Client_onEntityEnterSpace(MemoryStream stream)
		{
			int num = stream.readInt32();
			this.spaceID = stream.readUint32();
			sbyte b = 1;
			if (stream.length() > 0U)
			{
				b = stream.readInt8();
			}
			Entity entity = null;
			if (!this.entities.TryGetValue(num, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterSpace: entity(" + num + ") not found!");
				return;
			}
			entity.isOnGround = (b > 0);
			this._entityServerPos = entity.position;
			entity.enterSpace();
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x0025FC28 File Offset: 0x0025DE28
		public void Client_onEntityLeaveSpace(int eid)
		{
			Entity entity = null;
			if (!this.entities.TryGetValue(eid, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityLeaveSpace: entity(" + eid + ") not found!");
				return;
			}
			entity.leaveSpace();
			this.clearSpace(false);
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x0025FC70 File Offset: 0x0025DE70
		public void Client_onCreateAccountResult(MemoryStream stream)
		{
			ushort num = stream.readUint16();
			byte[] array = stream.readBlob();
			Event.fireOut("onCreateAccountResult", new object[]
			{
				num,
				array
			});
			if (num != 0)
			{
				Dbg.WARNING_MSG(string.Concat(new object[]
				{
					"KBEngine::Client_onCreateAccountResult: ",
					this.username,
					" create is failed! code=",
					num,
					"(",
					this.serverErr(num),
					")!"
				}));
				return;
			}
			Dbg.DEBUG_MSG("KBEngine::Client_onCreateAccountResult: " + this.username + " create is successfully!");
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0025FD14 File Offset: 0x0025DF14
		public void Client_onControlEntity(int eid, sbyte isControlled)
		{
			Entity entity = null;
			if (!this.entities.TryGetValue(eid, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onControlEntity: entity(" + eid + ") not found!");
				return;
			}
			bool flag = isControlled != 0;
			if (flag)
			{
				if (this.player().id != entity.id)
				{
					this._controlledEntities.Add(entity);
				}
			}
			else
			{
				this._controlledEntities.Remove(entity);
			}
			entity.isControlled = flag;
			try
			{
				entity.onControlled(flag);
				Event.fireOut("onControlled", new object[]
				{
					entity,
					flag
				});
			}
			catch (Exception arg)
			{
				Dbg.ERROR_MSG(string.Format("KBEngine::Client_onControlEntity: entity id = '{0}', is controlled = '{1}', error = '{1}'", eid, flag, arg));
			}
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x0025FDE0 File Offset: 0x0025DFE0
		public void updatePlayerToServer()
		{
			if (this._updatePlayerToServerPeroid <= 0.01f || this.spaceID == 0U)
			{
				return;
			}
			DateTime now = DateTime.Now;
			TimeSpan t = now - this._lastUpdateToServerTime;
			if ((float)t.Ticks < this._updatePlayerToServerPeroid * 10000f)
			{
				return;
			}
			Entity entity = this.player();
			if (entity == null || !entity.inWorld || entity.isControlled)
			{
				return;
			}
			this._lastUpdateToServerTime = now - (t - TimeSpan.FromTicks(Convert.ToInt64(this._updatePlayerToServerPeroid * 10000f)));
			Vector3 position = entity.position;
			Vector3 direction = entity.direction;
			bool flag = Vector3.Distance(entity._entityLastLocalPos, position) > 0.001f;
			bool flag2 = Vector3.Distance(entity._entityLastLocalDir, direction) > 0.001f;
			if (flag || flag2)
			{
				entity._entityLastLocalPos = position;
				entity._entityLastLocalDir = direction;
				Bundle bundle = ObjectPool<Bundle>.createObject();
				bundle.newMessage(Messages.messages["Baseapp_onUpdateDataFromClient"]);
				bundle.writeFloat(position.x);
				bundle.writeFloat(position.y);
				bundle.writeFloat(position.z);
				double num = (double)direction.x / 360.0 * 6.283185307179586;
				double num2 = (double)direction.y / 360.0 * 6.283185307179586;
				double num3 = (double)direction.z / 360.0 * 6.283185307179586;
				if (num - 3.141592653589793 > 0.0)
				{
					num -= 6.283185307179586;
				}
				if (num2 - 3.141592653589793 > 0.0)
				{
					num2 -= 6.283185307179586;
				}
				if (num3 - 3.141592653589793 > 0.0)
				{
					num3 -= 6.283185307179586;
				}
				bundle.writeFloat((float)num);
				bundle.writeFloat((float)num2);
				bundle.writeFloat((float)num3);
				bundle.writeUint8(entity.isOnGround ? 1 : 0);
				bundle.writeUint32(this.spaceID);
				bundle.send(this._networkInterface);
			}
			for (int i = 0; i < this._controlledEntities.Count; i++)
			{
				Entity entity2 = this._controlledEntities[i];
				position = entity2.position;
				direction = entity2.direction;
				bool flag3 = Vector3.Distance(entity2._entityLastLocalPos, position) > 0.001f;
				flag2 = (Vector3.Distance(entity2._entityLastLocalDir, direction) > 0.001f);
				if (flag3 || flag2)
				{
					entity2._entityLastLocalPos = position;
					entity2._entityLastLocalDir = direction;
					Bundle bundle2 = ObjectPool<Bundle>.createObject();
					bundle2.newMessage(Messages.messages["Baseapp_onUpdateDataFromClientForControlledEntity"]);
					bundle2.writeInt32(entity2.id);
					bundle2.writeFloat(position.x);
					bundle2.writeFloat(position.y);
					bundle2.writeFloat(position.z);
					double num4 = (double)direction.x / 360.0 * 6.283185307179586;
					double num5 = (double)direction.y / 360.0 * 6.283185307179586;
					double num6 = (double)direction.z / 360.0 * 6.283185307179586;
					if (num4 - 3.141592653589793 > 0.0)
					{
						num4 -= 6.283185307179586;
					}
					if (num5 - 3.141592653589793 > 0.0)
					{
						num5 -= 6.283185307179586;
					}
					if (num6 - 3.141592653589793 > 0.0)
					{
						num6 -= 6.283185307179586;
					}
					bundle2.writeFloat((float)num4);
					bundle2.writeFloat((float)num5);
					bundle2.writeFloat((float)num6);
					bundle2.writeUint8(entity2.isOnGround ? 1 : 0);
					bundle2.writeUint32(this.spaceID);
					bundle2.send(this._networkInterface);
				}
			}
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x002601EC File Offset: 0x0025E3EC
		public void addSpaceGeometryMapping(uint uspaceID, string respath)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::addSpaceGeometryMapping: spaceID(",
				uspaceID,
				"), respath(",
				respath,
				")!"
			}));
			this.isLoadedGeometry = true;
			this.spaceID = uspaceID;
			this.spaceResPath = respath;
			Event.fireOut("addSpaceGeometryMapping", new object[]
			{
				this.spaceResPath
			});
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x0004194D File Offset: 0x0003FB4D
		public void clearSpace(bool isall)
		{
			this._entityIDAliasIDList.Clear();
			this._spacedatas.Clear();
			this.clearEntities(isall);
			this.isLoadedGeometry = false;
			this.spaceID = 0U;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x0026025C File Offset: 0x0025E45C
		public void clearEntities(bool isall)
		{
			this._controlledEntities.Clear();
			if (!isall)
			{
				Entity entity = this.player();
				foreach (KeyValuePair<int, Entity> keyValuePair in this.entities)
				{
					if (keyValuePair.Key != entity.id)
					{
						if (keyValuePair.Value.inWorld)
						{
							keyValuePair.Value.leaveWorld();
						}
						keyValuePair.Value.destroy();
					}
				}
				this.entities.Clear();
				this.entities[entity.id] = entity;
				return;
			}
			foreach (KeyValuePair<int, Entity> keyValuePair2 in this.entities)
			{
				if (keyValuePair2.Value.inWorld)
				{
					keyValuePair2.Value.leaveWorld();
				}
				keyValuePair2.Value.destroy();
			}
			this.entities.Clear();
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x00260380 File Offset: 0x0025E580
		public void Client_initSpaceData(MemoryStream stream)
		{
			this.clearSpace(false);
			this.spaceID = stream.readUint32();
			while (stream.length() > 0U)
			{
				string key = stream.readString();
				string value = stream.readString();
				this.Client_setSpaceData(this.spaceID, key, value);
			}
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_initSpaceData: spaceID(",
				this.spaceID,
				"), size(",
				this._spacedatas.Count,
				")!"
			}));
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x00260410 File Offset: 0x0025E610
		public void Client_setSpaceData(uint spaceID, string key, string value)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_setSpaceData: spaceID(",
				spaceID,
				"), key(",
				key,
				"), value(",
				value,
				")!"
			}));
			this._spacedatas[key] = value;
			if (key == "_mapping")
			{
				this.addSpaceGeometryMapping(spaceID, value);
			}
			Event.fireOut("onSetSpaceData", new object[]
			{
				spaceID,
				key,
				value
			});
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x002604A4 File Offset: 0x0025E6A4
		public void Client_delSpaceData(uint spaceID, string key)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"KBEngine::Client_delSpaceData: spaceID(",
				spaceID,
				"), key(",
				key,
				")"
			}));
			this._spacedatas.Remove(key);
			Event.fireOut("onDelSpaceData", new object[]
			{
				spaceID,
				key
			});
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x00260510 File Offset: 0x0025E710
		public string getSpaceData(string key)
		{
			string result = "";
			if (!this._spacedatas.TryGetValue(key, out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x0026053C File Offset: 0x0025E73C
		public void Client_onEntityDestroyed(int eid)
		{
			Dbg.DEBUG_MSG("KBEngine::Client_onEntityDestroyed: entity(" + eid + ")");
			Entity entity = null;
			if (!this.entities.TryGetValue(eid, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityDestroyed: entity(" + eid + ") not found!");
				return;
			}
			if (entity.inWorld)
			{
				if (this.entity_id == eid)
				{
					this.clearSpace(false);
				}
				entity.leaveWorld();
			}
			if (this._controlledEntities.Remove(entity))
			{
				Event.fireOut("onLoseControlledEntity", new object[]
				{
					entity
				});
			}
			this.entities.Remove(eid);
			entity.destroy();
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x002605E4 File Offset: 0x0025E7E4
		public void Client_onUpdateBasePos(float x, float y, float z)
		{
			this._entityServerPos.x = x;
			this._entityServerPos.y = y;
			this._entityServerPos.z = z;
			Entity entity = this.player();
			if (entity != null && entity.isControlled)
			{
				entity.position.Set(this._entityServerPos.x, this._entityServerPos.y, this._entityServerPos.z);
				Event.fireOut("updatePosition", new object[]
				{
					entity
				});
				entity.onUpdateVolatileData();
			}
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x00260670 File Offset: 0x0025E870
		public void Client_onUpdateBasePosXZ(float x, float z)
		{
			this._entityServerPos.x = x;
			this._entityServerPos.z = z;
			Entity entity = this.player();
			if (entity != null && entity.isControlled)
			{
				entity.position.x = this._entityServerPos.x;
				entity.position.z = this._entityServerPos.z;
				Event.fireOut("updatePosition", new object[]
				{
					entity
				});
				entity.onUpdateVolatileData();
			}
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x002606F0 File Offset: 0x0025E8F0
		public void Client_onUpdateBaseDir(MemoryStream stream)
		{
			float num = stream.readFloat() * 360f / 6.2831855f;
			float num2 = stream.readFloat() * 360f / 6.2831855f;
			float num3 = stream.readFloat() * 360f / 6.2831855f;
			Entity entity = this.player();
			if (entity != null && entity.isControlled)
			{
				entity.direction.Set(num3, num2, num);
				Event.fireOut("set_direction", new object[]
				{
					entity
				});
				entity.onUpdateVolatileData();
			}
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x00260770 File Offset: 0x0025E970
		public void Client_onUpdateData(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Entity entity = null;
			if (!this.entities.TryGetValue(viewEntityIDFromStream, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onUpdateData: entity(" + viewEntityIDFromStream + ") not found!");
				return;
			}
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x002607B4 File Offset: 0x0025E9B4
		public void Client_onSetEntityPosAndDir(MemoryStream stream)
		{
			int num = stream.readInt32();
			Entity entity = null;
			if (!this.entities.TryGetValue(num, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onSetEntityPosAndDir: entity(" + num + ") not found!");
				return;
			}
			Vector3 oldValue;
			oldValue..ctor(entity.position.x, entity.position.y, entity.position.z);
			Vector3 oldValue2;
			oldValue2..ctor(entity.direction.x, entity.direction.y, entity.direction.z);
			entity.position.x = stream.readFloat();
			entity.position.y = stream.readFloat();
			entity.position.z = stream.readFloat();
			entity.direction.x = stream.readFloat();
			entity.direction.y = stream.readFloat();
			entity.direction.z = stream.readFloat();
			entity._entityLastLocalPos = entity.position;
			entity._entityLastLocalDir = entity.direction;
			entity.onDirectionChanged(oldValue2);
			entity.onPositionChanged(oldValue);
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x002608D4 File Offset: 0x0025EAD4
		public void Client_onUpdateData_ypr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, roll, -1, false);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0026091C File Offset: 0x0025EB1C
		public void Client_onUpdateData_yp(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x00260960 File Offset: 0x0025EB60
		public void Client_onUpdateData_yr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, roll, -1, false);
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x002609A4 File Offset: 0x0025EBA4
		public void Client_onUpdateData_pr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, roll, -1, false);
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x002609E8 File Offset: 0x0025EBE8
		public void Client_onUpdateData_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x00260A28 File Offset: 0x0025EC28
		public void Client_onUpdateData_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x00260A68 File Offset: 0x0025EC68
		public void Client_onUpdateData_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, -1, false);
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x00260AA8 File Offset: 0x0025ECA8
		public void Client_onUpdateData_xz(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x00260AEC File Offset: 0x0025ECEC
		public void Client_onUpdateData_xz_ypr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, pitch, roll, 1, false);
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x00260B3C File Offset: 0x0025ED3C
		public void Client_onUpdateData_xz_yp(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, pitch, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x00260B88 File Offset: 0x0025ED88
		public void Client_onUpdateData_xz_yr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, roll, 1, false);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00260BD4 File Offset: 0x0025EDD4
		public void Client_onUpdateData_xz_pr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, roll, 1, false);
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00260C20 File Offset: 0x0025EE20
		public void Client_onUpdateData_xz_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00260C68 File Offset: 0x0025EE68
		public void Client_onUpdateData_xz_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x00260CB0 File Offset: 0x0025EEB0
		public void Client_onUpdateData_xz_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 1, false);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x00260CF8 File Offset: 0x0025EEF8
		public void Client_onUpdateData_xyz(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x00260D40 File Offset: 0x0025EF40
		public void Client_onUpdateData_xyz_ypr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, pitch, roll, 0, false);
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x00260D94 File Offset: 0x0025EF94
		public void Client_onUpdateData_xyz_yp(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, pitch, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00260DE4 File Offset: 0x0025EFE4
		public void Client_onUpdateData_xyz_yr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, KBEMath.KBE_FLT_MAX, roll, 0, false);
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00260E34 File Offset: 0x0025F034
		public void Client_onUpdateData_xyz_pr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, pitch, roll, 0, false);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x00260E84 File Offset: 0x0025F084
		public void Client_onUpdateData_xyz_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x00260ED0 File Offset: 0x0025F0D0
		public void Client_onUpdateData_xyz_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x00260F1C File Offset: 0x0025F11C
		public void Client_onUpdateData_xyz_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 0, false);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x00260F68 File Offset: 0x0025F168
		public void Client_onUpdateData_ypr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			sbyte b3 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, (float)b3, -1, true);
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x00260FB0 File Offset: 0x0025F1B0
		public void Client_onUpdateData_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x00260FF4 File Offset: 0x0025F1F4
		public void Client_onUpdateData_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, (float)b2, -1, true);
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x00261038 File Offset: 0x0025F238
		public void Client_onUpdateData_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, -1, true);
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x0026107C File Offset: 0x0025F27C
		public void Client_onUpdateData_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x002610BC File Offset: 0x0025F2BC
		public void Client_onUpdateData_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x002610FC File Offset: 0x0025F2FC
		public void Client_onUpdateData_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, -1, true);
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0026113C File Offset: 0x0025F33C
		public void Client_onUpdateData_xz_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x00261188 File Offset: 0x0025F388
		public void Client_onUpdateData_xz_ypr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			sbyte b3 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, (float)b2, (float)b3, 1, true);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x002611E0 File Offset: 0x0025F3E0
		public void Client_onUpdateData_xz_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, (float)b2, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x00261234 File Offset: 0x0025F434
		public void Client_onUpdateData_xz_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, KBEMath.KBE_FLT_MAX, (float)b2, 1, true);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00261288 File Offset: 0x0025F488
		public void Client_onUpdateData_xz_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, (float)b, (float)b2, 1, true);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x002612DC File Offset: 0x0025F4DC
		public void Client_onUpdateData_xz_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0026132C File Offset: 0x0025F52C
		public void Client_onUpdateData_xz_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x0026137C File Offset: 0x0025F57C
		public void Client_onUpdateData_xz_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, 1, true);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x002613CC File Offset: 0x0025F5CC
		public void Client_onUpdateData_xyz_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x00261418 File Offset: 0x0025F618
		public void Client_onUpdateData_xyz_ypr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			sbyte b3 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, (float)b2, (float)b3, 0, true);
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00261474 File Offset: 0x0025F674
		public void Client_onUpdateData_xyz_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, (float)b2, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x002614CC File Offset: 0x0025F6CC
		public void Client_onUpdateData_xyz_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, KBEMath.KBE_FLT_MAX, (float)b2, 0, true);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x00261524 File Offset: 0x0025F724
		public void Client_onUpdateData_xyz_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, (float)b, (float)b2, 0, true);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0026157C File Offset: 0x0025F77C
		public void Client_onUpdateData_xyz_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x002615CC File Offset: 0x0025F7CC
		public void Client_onUpdateData_xyz_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0026161C File Offset: 0x0025F81C
		public void Client_onUpdateData_xyz_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, 0, true);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x0026166C File Offset: 0x0025F86C
		private void _updateVolatileData(int entityID, float x, float y, float z, float yaw, float pitch, float roll, sbyte isOnGround, bool isOptimized)
		{
			Entity entity = null;
			if (!this.entities.TryGetValue(entityID, out entity))
			{
				Dbg.ERROR_MSG("KBEngine::_updateVolatileData: entity(" + entityID + ") not found!");
				return;
			}
			if (isOnGround >= 0)
			{
				entity.isOnGround = (isOnGround > 0);
			}
			bool flag = false;
			if (roll != KBEMath.KBE_FLT_MAX)
			{
				flag = true;
				entity.direction.x = KBEMath.int82angle((sbyte)roll, false) * 360f / 6.2831855f;
			}
			if (pitch != KBEMath.KBE_FLT_MAX)
			{
				flag = true;
				entity.direction.y = KBEMath.int82angle((sbyte)pitch, false) * 360f / 6.2831855f;
			}
			if (yaw != KBEMath.KBE_FLT_MAX)
			{
				flag = true;
				entity.direction.z = KBEMath.int82angle((sbyte)yaw, false) * 360f / 6.2831855f;
			}
			bool flag2 = false;
			if (flag)
			{
				Event.fireOut("set_direction", new object[]
				{
					entity
				});
				flag2 = true;
			}
			bool flag3 = x != KBEMath.KBE_FLT_MAX || y != KBEMath.KBE_FLT_MAX || z != KBEMath.KBE_FLT_MAX;
			if (x == KBEMath.KBE_FLT_MAX)
			{
				x = 0f;
			}
			if (y == KBEMath.KBE_FLT_MAX)
			{
				y = 0f;
			}
			if (z == KBEMath.KBE_FLT_MAX)
			{
				z = 0f;
			}
			if (flag3)
			{
				Vector3 position = isOptimized ? new Vector3(x + this._entityServerPos.x, y + this._entityServerPos.y, z + this._entityServerPos.z) : new Vector3(x, y, z);
				entity.position = position;
				flag2 = true;
				Event.fireOut("updatePosition", new object[]
				{
					entity
				});
			}
			if (flag2)
			{
				entity.onUpdateVolatileData();
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x0004197A File Offset: 0x0003FB7A
		public void Client_onStreamDataStarted(short id, uint datasize, string descr)
		{
			Event.fireOut("onStreamDataStarted", new object[]
			{
				id,
				datasize,
				descr
			});
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00261808 File Offset: 0x0025FA08
		public void Client_onStreamDataRecv(MemoryStream stream)
		{
			short num = stream.readInt16();
			byte[] array = stream.readBlob();
			Event.fireOut("onStreamDataRecv", new object[]
			{
				num,
				array
			});
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x000419A2 File Offset: 0x0003FBA2
		public void Client_onStreamDataCompleted(short id)
		{
			Event.fireOut("onStreamDataCompleted", new object[]
			{
				id
			});
		}

		// Token: 0x04005A7E RID: 23166
		public static KBEngineApp app;

		// Token: 0x04005A7F RID: 23167
		private NetworkInterfaceBase _networkInterface;

		// Token: 0x04005A80 RID: 23168
		private KBEngineArgs _args;

		// Token: 0x04005A81 RID: 23169
		public string username = "kbengine";

		// Token: 0x04005A82 RID: 23170
		public string password = "123456";

		// Token: 0x04005A83 RID: 23171
		public string baseappIP = "";

		// Token: 0x04005A84 RID: 23172
		public ushort baseappTcpPort;

		// Token: 0x04005A85 RID: 23173
		public ushort baseappUdpPort;

		// Token: 0x04005A86 RID: 23174
		public string currserver = "";

		// Token: 0x04005A87 RID: 23175
		public string currstate = "";

		// Token: 0x04005A88 RID: 23176
		private byte[] _serverdatas = new byte[0];

		// Token: 0x04005A89 RID: 23177
		private byte[] _clientdatas = new byte[0];

		// Token: 0x04005A8A RID: 23178
		private byte[] _encryptedKey = new byte[0];

		// Token: 0x04005A8B RID: 23179
		public string serverVersion = "";

		// Token: 0x04005A8C RID: 23180
		public string clientVersion = "2.4.4";

		// Token: 0x04005A8D RID: 23181
		public string serverScriptVersion = "";

		// Token: 0x04005A8E RID: 23182
		public string clientScriptVersion = "0.1.0";

		// Token: 0x04005A8F RID: 23183
		public string serverProtocolMD5 = "6211B63FBCDC965450606C12A12B752E";

		// Token: 0x04005A90 RID: 23184
		public string serverEntitydefMD5 = "CFFEFCB602BAD93896662DE58C2D01F2";

		// Token: 0x04005A91 RID: 23185
		public ulong entity_uuid;

		// Token: 0x04005A92 RID: 23186
		public int entity_id;

		// Token: 0x04005A93 RID: 23187
		public string entity_type = "";

		// Token: 0x04005A94 RID: 23188
		private List<Entity> _controlledEntities = new List<Entity>();

		// Token: 0x04005A95 RID: 23189
		private Vector3 _entityServerPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04005A96 RID: 23190
		private Dictionary<string, string> _spacedatas = new Dictionary<string, string>();

		// Token: 0x04005A97 RID: 23191
		public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();

		// Token: 0x04005A98 RID: 23192
		private List<int> _entityIDAliasIDList = new List<int>();

		// Token: 0x04005A99 RID: 23193
		private Dictionary<int, MemoryStream> _bufferedCreateEntityMessages = new Dictionary<int, MemoryStream>();

		// Token: 0x04005A9A RID: 23194
		private ServerErrorDescrs _serverErrs = new ServerErrorDescrs();

		// Token: 0x04005A9B RID: 23195
		private DateTime _lastTickTime = DateTime.Now;

		// Token: 0x04005A9C RID: 23196
		private DateTime _lastTickCBTime = DateTime.Now;

		// Token: 0x04005A9D RID: 23197
		private DateTime _lastUpdateToServerTime = DateTime.Now;

		// Token: 0x04005A9E RID: 23198
		private float _updatePlayerToServerPeroid = 100f;

		// Token: 0x04005A9F RID: 23199
		private const int _1MS_TO_100NS = 10000;

		// Token: 0x04005AA0 RID: 23200
		private EncryptionFilter _filter;

		// Token: 0x04005AA1 RID: 23201
		public uint spaceID;

		// Token: 0x04005AA2 RID: 23202
		public string spaceResPath = "";

		// Token: 0x04005AA3 RID: 23203
		public bool isLoadedGeometry;

		// Token: 0x04005AA4 RID: 23204
		public const string component = "client";

		// Token: 0x02000F18 RID: 3864
		public enum CLIENT_TYPE
		{
			// Token: 0x04005AA6 RID: 23206
			CLIENT_TYPE_MOBILE = 1,
			// Token: 0x04005AA7 RID: 23207
			CLIENT_TYPE_WIN,
			// Token: 0x04005AA8 RID: 23208
			CLIENT_TYPE_LINUX,
			// Token: 0x04005AA9 RID: 23209
			CLIENT_TYPE_MAC,
			// Token: 0x04005AAA RID: 23210
			CLIENT_TYPE_BROWSER,
			// Token: 0x04005AAB RID: 23211
			CLIENT_TYPE_BOTS,
			// Token: 0x04005AAC RID: 23212
			CLIENT_TYPE_MINI
		}

		// Token: 0x02000F19 RID: 3865
		public enum NETWORK_ENCRYPT_TYPE
		{
			// Token: 0x04005AAE RID: 23214
			ENCRYPT_TYPE_NONE,
			// Token: 0x04005AAF RID: 23215
			ENCRYPT_TYPE_BLOWFISH
		}
	}
}
