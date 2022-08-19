using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B97 RID: 2967
	public class KBEngineApp
	{
		// Token: 0x0600527C RID: 21116 RVA: 0x0022FF2C File Offset: 0x0022E12C
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

		// Token: 0x0600527D RID: 21117 RVA: 0x002300A8 File Offset: 0x0022E2A8
		public static KBEngineApp getSingleton()
		{
			if (KBEngineApp.app == null)
			{
				throw new Exception("Please create KBEngineApp!");
			}
			return KBEngineApp.app;
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x002300C1 File Offset: 0x0022E2C1
		public virtual bool initialize(KBEngineArgs args)
		{
			this._args = args;
			this._updatePlayerToServerPeroid = (float)this._args.syncPlayerMS;
			EntityDef.init();
			this.initNetwork();
			this.installEvents();
			return true;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x002300EF File Offset: 0x0022E2EF
		private void initNetwork()
		{
			this._filter = null;
			Messages.init();
			this._networkInterface = new NetworkInterfaceTCP();
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0023010C File Offset: 0x0022E30C
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

		// Token: 0x06005281 RID: 21121 RVA: 0x002301A1 File Offset: 0x0022E3A1
		public KBEngineArgs getInitArgs()
		{
			return this._args;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x002301A9 File Offset: 0x0022E3A9
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

		// Token: 0x06005283 RID: 21123 RVA: 0x002301E6 File Offset: 0x0022E3E6
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x002301EE File Offset: 0x0022E3EE
		public byte[] serverdatas()
		{
			return this._serverdatas;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x002301F6 File Offset: 0x0022E3F6
		public void entityServerPos(Vector3 pos)
		{
			this._entityServerPos = pos;
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x002301FF File Offset: 0x0022E3FF
		public void resetMessages()
		{
			this._serverErrs.Clear();
			Messages.clear();
			EntityDef.reset();
			Entity.clear();
			Dbg.DEBUG_MSG("KBEngine::resetMessages()");
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00230228 File Offset: 0x0022E428
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

		// Token: 0x06005288 RID: 21128 RVA: 0x00230313 File Offset: 0x0022E513
		public static bool validEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)\r\n\t\t\t\t|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00230320 File Offset: 0x0022E520
		public virtual void process()
		{
			if (this._networkInterface != null)
			{
				this._networkInterface.process();
			}
			Event.processInEvents();
			this.sendTick();
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00230340 File Offset: 0x0022E540
		public Entity player()
		{
			Entity result;
			if (this.entities.TryGetValue(this.entity_id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00230365 File Offset: 0x0022E565
		public void _closeNetwork(NetworkInterfaceBase networkInterface)
		{
			networkInterface.close();
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00230370 File Offset: 0x0022E570
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

		// Token: 0x0600528D RID: 21133 RVA: 0x0023049B File Offset: 0x0022E69B
		public void Client_onAppActiveTickCB()
		{
			this._lastTickCBTime = DateTime.Now;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x002304A8 File Offset: 0x0022E6A8
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

		// Token: 0x0600528F RID: 21135 RVA: 0x0023056C File Offset: 0x0022E76C
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

		// Token: 0x06005290 RID: 21136 RVA: 0x00004095 File Offset: 0x00002295
		public void Client_onImportServerErrorsDescr(MemoryStream stream)
		{
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00004095 File Offset: 0x00002295
		public void Client_onImportClientMessages(MemoryStream stream)
		{
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00004095 File Offset: 0x00002295
		public void Client_onImportClientEntityDef(MemoryStream stream)
		{
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x002306D4 File Offset: 0x0022E8D4
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

		// Token: 0x06005294 RID: 21140 RVA: 0x00230734 File Offset: 0x0022E934
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

		// Token: 0x06005295 RID: 21141 RVA: 0x002307AC File Offset: 0x0022E9AC
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

		// Token: 0x06005296 RID: 21142 RVA: 0x00230824 File Offset: 0x0022EA24
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

		// Token: 0x06005297 RID: 21143 RVA: 0x00230885 File Offset: 0x0022EA85
		public void login(string username, string password, byte[] datas)
		{
			KBEngineApp.app.username = username;
			KBEngineApp.app.password = password;
			KBEngineApp.app._clientdatas = datas;
			KBEngineApp.app.login_loginapp(true);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x002308B4 File Offset: 0x0022EAB4
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

		// Token: 0x06005299 RID: 21145 RVA: 0x00230970 File Offset: 0x0022EB70
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

		// Token: 0x0600529A RID: 21146 RVA: 0x002309D4 File Offset: 0x0022EBD4
		private void onLogin_loginapp()
		{
			this._lastTickCBTime = DateTime.Now;
			this.login_loginapp(false);
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x002309E8 File Offset: 0x0022EBE8
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

		// Token: 0x0600529C RID: 21148 RVA: 0x00230AC8 File Offset: 0x0022ECC8
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

		// Token: 0x0600529D RID: 21149 RVA: 0x00230B2C File Offset: 0x0022ED2C
		private void onLogin_baseapp()
		{
			this._lastTickCBTime = DateTime.Now;
			this.login_baseapp(false);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00230B40 File Offset: 0x0022ED40
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

		// Token: 0x0600529F RID: 21151 RVA: 0x00230C00 File Offset: 0x0022EE00
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

		// Token: 0x060052A0 RID: 21152 RVA: 0x00230C9D File Offset: 0x0022EE9D
		public void logout()
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_logoutBaseapp"]);
			bundle.writeUint64(this.entity_uuid);
			bundle.writeInt32(this.entity_id);
			bundle.send(this._networkInterface);
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x00230CDC File Offset: 0x0022EEDC
		public string serverErr(ushort id)
		{
			return this._serverErrs.serverErrStr(id);
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00230CEA File Offset: 0x0022EEEA
		public void onOpenLoginapp_resetpassword()
		{
			Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_resetpassword: successfully!");
			this.currserver = "loginapp";
			this.currstate = "resetpassword";
			this._lastTickCBTime = DateTime.Now;
			this.resetpassword_loginapp(false);
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x00230D1E File Offset: 0x0022EF1E
		public void resetPassword(string username)
		{
			KBEngineApp.app.username = username;
			this.resetpassword_loginapp(true);
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00230D34 File Offset: 0x0022EF34
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

		// Token: 0x060052A5 RID: 21157 RVA: 0x00230DAC File Offset: 0x0022EFAC
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

		// Token: 0x060052A6 RID: 21158 RVA: 0x00230DFC File Offset: 0x0022EFFC
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

		// Token: 0x060052A7 RID: 21159 RVA: 0x00230E74 File Offset: 0x0022F074
		public void bindAccountEmail(string emailAddress)
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_reqAccountBindEmail"]);
			bundle.writeInt32(this.entity_id);
			bundle.writeString(this.password);
			bundle.writeString(emailAddress);
			bundle.send(this._networkInterface);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00230EC8 File Offset: 0x0022F0C8
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

		// Token: 0x060052A9 RID: 21161 RVA: 0x00230F40 File Offset: 0x0022F140
		public void newPassword(string old_password, string new_password)
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_reqAccountNewPassword"]);
			bundle.writeInt32(this.entity_id);
			bundle.writeString(old_password);
			bundle.writeString(new_password);
			bundle.send(this._networkInterface);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00230F8C File Offset: 0x0022F18C
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

		// Token: 0x060052AB RID: 21163 RVA: 0x00231003 File Offset: 0x0022F203
		public void createAccount(string username, string password, byte[] datas)
		{
			KBEngineApp.app.username = username;
			KBEngineApp.app.password = password;
			KBEngineApp.app._clientdatas = datas;
			KBEngineApp.app.createAccount_loginapp(true);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x00231034 File Offset: 0x0022F234
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

		// Token: 0x060052AD RID: 21165 RVA: 0x002310C6 File Offset: 0x0022F2C6
		public void onOpenLoginapp_createAccount()
		{
			Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_createAccount: successfully!");
			this.currserver = "loginapp";
			this.currstate = "createAccount";
			this._lastTickCBTime = DateTime.Now;
			this.createAccount_loginapp(false);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x002310FC File Offset: 0x0022F2FC
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

		// Token: 0x060052AF RID: 21167 RVA: 0x00004095 File Offset: 0x00002295
		public void onServerDigest()
		{
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0023114C File Offset: 0x0022F34C
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

		// Token: 0x060052B1 RID: 21169 RVA: 0x002311D8 File Offset: 0x0022F3D8
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

		// Token: 0x060052B2 RID: 21170 RVA: 0x002312A8 File Offset: 0x0022F4A8
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

		// Token: 0x060052B3 RID: 21171 RVA: 0x0023130C File Offset: 0x0022F50C
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

		// Token: 0x060052B4 RID: 21172 RVA: 0x0023136D File Offset: 0x0022F56D
		public void Client_onReloginBaseappSuccessfully(MemoryStream stream)
		{
			this.entity_uuid = stream.readUint64();
			Dbg.DEBUG_MSG("KBEngine::Client_onReloginBaseappSuccessfully: name(" + this.username + ")!");
			Event.fireAll("onReloginBaseappSuccessfully", Array.Empty<object>());
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x002313A4 File Offset: 0x0022F5A4
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

		// Token: 0x060052B6 RID: 21174 RVA: 0x002314F8 File Offset: 0x0022F6F8
		public Entity findEntity(int entityID)
		{
			Entity result = null;
			if (!this.entities.TryGetValue(entityID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0023151C File Offset: 0x0022F71C
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

		// Token: 0x060052B8 RID: 21176 RVA: 0x00231580 File Offset: 0x0022F780
		public void Client_onUpdatePropertysOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			this.onUpdatePropertys_(viewEntityIDFromStream, stream);
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x002315A0 File Offset: 0x0022F7A0
		public void Client_onUpdatePropertys(MemoryStream stream)
		{
			int eid = stream.readInt32();
			this.onUpdatePropertys_(eid, stream);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x002315BC File Offset: 0x0022F7BC
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

		// Token: 0x060052BB RID: 21179 RVA: 0x00231654 File Offset: 0x0022F854
		public void Client_onRemoteMethodCallOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			this.onRemoteMethodCall_(viewEntityIDFromStream, stream);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00231674 File Offset: 0x0022F874
		public void Client_onRemoteMethodCall(MemoryStream stream)
		{
			int eid = stream.readInt32();
			this.onRemoteMethodCall_(eid, stream);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00231690 File Offset: 0x0022F890
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

		// Token: 0x060052BE RID: 21182 RVA: 0x002316D4 File Offset: 0x0022F8D4
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

		// Token: 0x060052BF RID: 21183 RVA: 0x0023190C File Offset: 0x0022FB0C
		public void Client_onEntityLeaveWorldOptimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			KBEngineApp.app.Client_onEntityLeaveWorld(viewEntityIDFromStream);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0023192C File Offset: 0x0022FB2C
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

		// Token: 0x060052C1 RID: 21185 RVA: 0x002319D0 File Offset: 0x0022FBD0
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

		// Token: 0x060052C2 RID: 21186 RVA: 0x00231A4C File Offset: 0x0022FC4C
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

		// Token: 0x060052C3 RID: 21187 RVA: 0x00231A94 File Offset: 0x0022FC94
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

		// Token: 0x060052C4 RID: 21188 RVA: 0x00231B38 File Offset: 0x0022FD38
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

		// Token: 0x060052C5 RID: 21189 RVA: 0x00231C04 File Offset: 0x0022FE04
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

		// Token: 0x060052C6 RID: 21190 RVA: 0x00232010 File Offset: 0x00230210
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

		// Token: 0x060052C7 RID: 21191 RVA: 0x00232080 File Offset: 0x00230280
		public void clearSpace(bool isall)
		{
			this._entityIDAliasIDList.Clear();
			this._spacedatas.Clear();
			this.clearEntities(isall);
			this.isLoadedGeometry = false;
			this.spaceID = 0U;
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x002320B0 File Offset: 0x002302B0
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

		// Token: 0x060052C9 RID: 21193 RVA: 0x002321D4 File Offset: 0x002303D4
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

		// Token: 0x060052CA RID: 21194 RVA: 0x00232264 File Offset: 0x00230464
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

		// Token: 0x060052CB RID: 21195 RVA: 0x002322F8 File Offset: 0x002304F8
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

		// Token: 0x060052CC RID: 21196 RVA: 0x00232364 File Offset: 0x00230564
		public string getSpaceData(string key)
		{
			string result = "";
			if (!this._spacedatas.TryGetValue(key, out result))
			{
				return "";
			}
			return result;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00232390 File Offset: 0x00230590
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

		// Token: 0x060052CE RID: 21198 RVA: 0x00232438 File Offset: 0x00230638
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

		// Token: 0x060052CF RID: 21199 RVA: 0x002324C4 File Offset: 0x002306C4
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

		// Token: 0x060052D0 RID: 21200 RVA: 0x00232544 File Offset: 0x00230744
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

		// Token: 0x060052D1 RID: 21201 RVA: 0x002325C4 File Offset: 0x002307C4
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

		// Token: 0x060052D2 RID: 21202 RVA: 0x00232608 File Offset: 0x00230808
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

		// Token: 0x060052D3 RID: 21203 RVA: 0x00232728 File Offset: 0x00230928
		public void Client_onUpdateData_ypr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, roll, -1, false);
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00232770 File Offset: 0x00230970
		public void Client_onUpdateData_yp(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x002327B4 File Offset: 0x002309B4
		public void Client_onUpdateData_yr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, roll, -1, false);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x002327F8 File Offset: 0x002309F8
		public void Client_onUpdateData_pr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, roll, -1, false);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0023283C File Offset: 0x00230A3C
		public void Client_onUpdateData_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0023287C File Offset: 0x00230A7C
		public void Client_onUpdateData_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, -1, false);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x002328BC File Offset: 0x00230ABC
		public void Client_onUpdateData_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, -1, false);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x002328FC File Offset: 0x00230AFC
		public void Client_onUpdateData_xz(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00232940 File Offset: 0x00230B40
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

		// Token: 0x060052DC RID: 21212 RVA: 0x00232990 File Offset: 0x00230B90
		public void Client_onUpdateData_xz_yp(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, pitch, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x002329DC File Offset: 0x00230BDC
		public void Client_onUpdateData_xz_yr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, roll, 1, false);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00232A28 File Offset: 0x00230C28
		public void Client_onUpdateData_xz_pr(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, roll, 1, false);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00232A74 File Offset: 0x00230C74
		public void Client_onUpdateData_xz_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x00232ABC File Offset: 0x00230CBC
		public void Client_onUpdateData_xz_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 1, false);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00232B04 File Offset: 0x00230D04
		public void Client_onUpdateData_xz_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float z = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 1, false);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00232B4C File Offset: 0x00230D4C
		public void Client_onUpdateData_xyz(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x00232B94 File Offset: 0x00230D94
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

		// Token: 0x060052E4 RID: 21220 RVA: 0x00232BE8 File Offset: 0x00230DE8
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

		// Token: 0x060052E5 RID: 21221 RVA: 0x00232C38 File Offset: 0x00230E38
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

		// Token: 0x060052E6 RID: 21222 RVA: 0x00232C88 File Offset: 0x00230E88
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

		// Token: 0x060052E7 RID: 21223 RVA: 0x00232CD8 File Offset: 0x00230ED8
		public void Client_onUpdateData_xyz_y(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float yaw = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00232D24 File Offset: 0x00230F24
		public void Client_onUpdateData_xyz_p(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float pitch = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 0, false);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00232D70 File Offset: 0x00230F70
		public void Client_onUpdateData_xyz_r(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			float x = stream.readFloat();
			float y = stream.readFloat();
			float z = stream.readFloat();
			float roll = stream.readFloat();
			this._updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 0, false);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00232DBC File Offset: 0x00230FBC
		public void Client_onUpdateData_ypr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			sbyte b3 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, (float)b3, -1, true);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x00232E04 File Offset: 0x00231004
		public void Client_onUpdateData_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x00232E48 File Offset: 0x00231048
		public void Client_onUpdateData_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, (float)b2, -1, true);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00232E8C File Offset: 0x0023108C
		public void Client_onUpdateData_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, (float)b2, -1, true);
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00232ED0 File Offset: 0x002310D0
		public void Client_onUpdateData_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00232F10 File Offset: 0x00231110
		public void Client_onUpdateData_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, -1, true);
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x00232F50 File Offset: 0x00231150
		public void Client_onUpdateData_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, -1, true);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x00232F90 File Offset: 0x00231190
		public void Client_onUpdateData_xz_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00232FDC File Offset: 0x002311DC
		public void Client_onUpdateData_xz_ypr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			sbyte b3 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, (float)b2, (float)b3, 1, true);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x00233034 File Offset: 0x00231234
		public void Client_onUpdateData_xz_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, (float)b2, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00233088 File Offset: 0x00231288
		public void Client_onUpdateData_xz_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, KBEMath.KBE_FLT_MAX, (float)b2, 1, true);
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x002330DC File Offset: 0x002312DC
		public void Client_onUpdateData_xz_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, (float)b, (float)b2, 1, true);
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00233130 File Offset: 0x00231330
		public void Client_onUpdateData_xz_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00233180 File Offset: 0x00231380
		public void Client_onUpdateData_xz_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, 1, true);
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x002331D0 File Offset: 0x002313D0
		public void Client_onUpdateData_xz_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], KBEMath.KBE_FLT_MAX, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, 1, true);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00233220 File Offset: 0x00231420
		public void Client_onUpdateData_xyz_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0023326C File Offset: 0x0023146C
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

		// Token: 0x060052FB RID: 21243 RVA: 0x002332C8 File Offset: 0x002314C8
		public void Client_onUpdateData_xyz_yp_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, (float)b2, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00233320 File Offset: 0x00231520
		public void Client_onUpdateData_xyz_yr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, KBEMath.KBE_FLT_MAX, (float)b2, 0, true);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00233378 File Offset: 0x00231578
		public void Client_onUpdateData_xyz_pr_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			sbyte b2 = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, (float)b, (float)b2, 0, true);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x002333D0 File Offset: 0x002315D0
		public void Client_onUpdateData_xyz_y_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], (float)b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00233420 File Offset: 0x00231620
		public void Client_onUpdateData_xyz_p_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, (float)b, KBEMath.KBE_FLT_MAX, 0, true);
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00233470 File Offset: 0x00231670
		public void Client_onUpdateData_xyz_r_optimized(MemoryStream stream)
		{
			int viewEntityIDFromStream = this.getViewEntityIDFromStream(stream);
			Vector2 vector = stream.readPackXZ();
			float y = stream.readPackY();
			sbyte b = stream.readInt8();
			this._updateVolatileData(viewEntityIDFromStream, vector[0], y, vector[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, (float)b, 0, true);
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x002334C0 File Offset: 0x002316C0
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

		// Token: 0x06005302 RID: 21250 RVA: 0x0023365A File Offset: 0x0023185A
		public void Client_onStreamDataStarted(short id, uint datasize, string descr)
		{
			Event.fireOut("onStreamDataStarted", new object[]
			{
				id,
				datasize,
				descr
			});
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00233684 File Offset: 0x00231884
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

		// Token: 0x06005304 RID: 21252 RVA: 0x002336BC File Offset: 0x002318BC
		public void Client_onStreamDataCompleted(short id)
		{
			Event.fireOut("onStreamDataCompleted", new object[]
			{
				id
			});
		}

		// Token: 0x04004FEA RID: 20458
		public static KBEngineApp app;

		// Token: 0x04004FEB RID: 20459
		private NetworkInterfaceBase _networkInterface;

		// Token: 0x04004FEC RID: 20460
		private KBEngineArgs _args;

		// Token: 0x04004FED RID: 20461
		public string username = "kbengine";

		// Token: 0x04004FEE RID: 20462
		public string password = "123456";

		// Token: 0x04004FEF RID: 20463
		public string baseappIP = "";

		// Token: 0x04004FF0 RID: 20464
		public ushort baseappTcpPort;

		// Token: 0x04004FF1 RID: 20465
		public ushort baseappUdpPort;

		// Token: 0x04004FF2 RID: 20466
		public string currserver = "";

		// Token: 0x04004FF3 RID: 20467
		public string currstate = "";

		// Token: 0x04004FF4 RID: 20468
		private byte[] _serverdatas = new byte[0];

		// Token: 0x04004FF5 RID: 20469
		private byte[] _clientdatas = new byte[0];

		// Token: 0x04004FF6 RID: 20470
		private byte[] _encryptedKey = new byte[0];

		// Token: 0x04004FF7 RID: 20471
		public string serverVersion = "";

		// Token: 0x04004FF8 RID: 20472
		public string clientVersion = "2.4.4";

		// Token: 0x04004FF9 RID: 20473
		public string serverScriptVersion = "";

		// Token: 0x04004FFA RID: 20474
		public string clientScriptVersion = "0.1.0";

		// Token: 0x04004FFB RID: 20475
		public string serverProtocolMD5 = "6211B63FBCDC965450606C12A12B752E";

		// Token: 0x04004FFC RID: 20476
		public string serverEntitydefMD5 = "CFFEFCB602BAD93896662DE58C2D01F2";

		// Token: 0x04004FFD RID: 20477
		public ulong entity_uuid;

		// Token: 0x04004FFE RID: 20478
		public int entity_id;

		// Token: 0x04004FFF RID: 20479
		public string entity_type = "";

		// Token: 0x04005000 RID: 20480
		private List<Entity> _controlledEntities = new List<Entity>();

		// Token: 0x04005001 RID: 20481
		private Vector3 _entityServerPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04005002 RID: 20482
		private Dictionary<string, string> _spacedatas = new Dictionary<string, string>();

		// Token: 0x04005003 RID: 20483
		public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();

		// Token: 0x04005004 RID: 20484
		private List<int> _entityIDAliasIDList = new List<int>();

		// Token: 0x04005005 RID: 20485
		private Dictionary<int, MemoryStream> _bufferedCreateEntityMessages = new Dictionary<int, MemoryStream>();

		// Token: 0x04005006 RID: 20486
		private ServerErrorDescrs _serverErrs = new ServerErrorDescrs();

		// Token: 0x04005007 RID: 20487
		private DateTime _lastTickTime = DateTime.Now;

		// Token: 0x04005008 RID: 20488
		private DateTime _lastTickCBTime = DateTime.Now;

		// Token: 0x04005009 RID: 20489
		private DateTime _lastUpdateToServerTime = DateTime.Now;

		// Token: 0x0400500A RID: 20490
		private float _updatePlayerToServerPeroid = 100f;

		// Token: 0x0400500B RID: 20491
		private const int _1MS_TO_100NS = 10000;

		// Token: 0x0400500C RID: 20492
		private EncryptionFilter _filter;

		// Token: 0x0400500D RID: 20493
		public uint spaceID;

		// Token: 0x0400500E RID: 20494
		public string spaceResPath = "";

		// Token: 0x0400500F RID: 20495
		public bool isLoadedGeometry;

		// Token: 0x04005010 RID: 20496
		public const string component = "client";

		// Token: 0x020015F6 RID: 5622
		public enum CLIENT_TYPE
		{
			// Token: 0x040070EC RID: 28908
			CLIENT_TYPE_MOBILE = 1,
			// Token: 0x040070ED RID: 28909
			CLIENT_TYPE_WIN,
			// Token: 0x040070EE RID: 28910
			CLIENT_TYPE_LINUX,
			// Token: 0x040070EF RID: 28911
			CLIENT_TYPE_MAC,
			// Token: 0x040070F0 RID: 28912
			CLIENT_TYPE_BROWSER,
			// Token: 0x040070F1 RID: 28913
			CLIENT_TYPE_BOTS,
			// Token: 0x040070F2 RID: 28914
			CLIENT_TYPE_MINI
		}

		// Token: 0x020015F7 RID: 5623
		public enum NETWORK_ENCRYPT_TYPE
		{
			// Token: 0x040070F4 RID: 28916
			ENCRYPT_TYPE_NONE,
			// Token: 0x040070F5 RID: 28917
			ENCRYPT_TYPE_BLOWFISH
		}
	}
}
