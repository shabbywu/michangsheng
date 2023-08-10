using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KBEngine;

public class KBEngineApp
{
	public enum CLIENT_TYPE
	{
		CLIENT_TYPE_MOBILE = 1,
		CLIENT_TYPE_WIN,
		CLIENT_TYPE_LINUX,
		CLIENT_TYPE_MAC,
		CLIENT_TYPE_BROWSER,
		CLIENT_TYPE_BOTS,
		CLIENT_TYPE_MINI
	}

	public enum NETWORK_ENCRYPT_TYPE
	{
		ENCRYPT_TYPE_NONE,
		ENCRYPT_TYPE_BLOWFISH
	}

	public static KBEngineApp app;

	private NetworkInterfaceBase _networkInterface;

	private KBEngineArgs _args;

	public string username = "kbengine";

	public string password = "123456";

	public string baseappIP = "";

	public ushort baseappTcpPort;

	public ushort baseappUdpPort;

	public string currserver = "";

	public string currstate = "";

	private byte[] _serverdatas = new byte[0];

	private byte[] _clientdatas = new byte[0];

	private byte[] _encryptedKey = new byte[0];

	public string serverVersion = "";

	public string clientVersion = "2.4.4";

	public string serverScriptVersion = "";

	public string clientScriptVersion = "0.1.0";

	public string serverProtocolMD5 = "6211B63FBCDC965450606C12A12B752E";

	public string serverEntitydefMD5 = "CFFEFCB602BAD93896662DE58C2D01F2";

	public ulong entity_uuid;

	public int entity_id;

	public string entity_type = "";

	private List<Entity> _controlledEntities = new List<Entity>();

	private Vector3 _entityServerPos = new Vector3(0f, 0f, 0f);

	private Dictionary<string, string> _spacedatas = new Dictionary<string, string>();

	public Dictionary<int, Entity> entities = new Dictionary<int, Entity>();

	private List<int> _entityIDAliasIDList = new List<int>();

	private Dictionary<int, MemoryStream> _bufferedCreateEntityMessages = new Dictionary<int, MemoryStream>();

	private ServerErrorDescrs _serverErrs = new ServerErrorDescrs();

	private DateTime _lastTickTime = DateTime.Now;

	private DateTime _lastTickCBTime = DateTime.Now;

	private DateTime _lastUpdateToServerTime = DateTime.Now;

	private float _updatePlayerToServerPeroid = 100f;

	private const int _1MS_TO_100NS = 10000;

	private EncryptionFilter _filter;

	public uint spaceID;

	public string spaceResPath = "";

	public bool isLoadedGeometry;

	public const string component = "client";

	public KBEngineApp(KBEngineArgs args)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (app != null)
		{
			throw new Exception("Only one instance of KBEngineApp!");
		}
		app = this;
		Event.outEventsImmediately = !args.isMultiThreads;
		initialize(args);
	}

	public static KBEngineApp getSingleton()
	{
		if (app == null)
		{
			throw new Exception("Please create KBEngineApp!");
		}
		return app;
	}

	public virtual bool initialize(KBEngineArgs args)
	{
		_args = args;
		_updatePlayerToServerPeroid = _args.syncPlayerMS;
		EntityDef.init();
		initNetwork();
		installEvents();
		return true;
	}

	private void initNetwork()
	{
		_filter = null;
		Messages.init();
		_networkInterface = new NetworkInterfaceTCP();
	}

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

	public KBEngineArgs getInitArgs()
	{
		return _args;
	}

	public virtual void destroy()
	{
		Dbg.WARNING_MSG("KBEngine::destroy()");
		if (currserver == "baseapp")
		{
			logout();
		}
		reset();
		Event.deregisterIn(this);
		resetMessages();
		app = null;
	}

	public NetworkInterfaceBase networkInterface()
	{
		return _networkInterface;
	}

	public byte[] serverdatas()
	{
		return _serverdatas;
	}

	public void entityServerPos(Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_entityServerPos = pos;
	}

	public void resetMessages()
	{
		_serverErrs.Clear();
		Messages.clear();
		EntityDef.reset();
		Entity.clear();
		Dbg.DEBUG_MSG("KBEngine::resetMessages()");
	}

	public virtual void reset()
	{
		Event.clearFiredEvents();
		clearEntities(isall: true);
		currserver = "";
		currstate = "";
		_serverdatas = new byte[0];
		serverVersion = "";
		serverScriptVersion = "";
		entity_uuid = 0uL;
		entity_id = 0;
		entity_type = "";
		_entityIDAliasIDList.Clear();
		_bufferedCreateEntityMessages.Clear();
		_lastTickTime = DateTime.Now;
		_lastTickCBTime = DateTime.Now;
		_lastUpdateToServerTime = DateTime.Now;
		spaceID = 0u;
		spaceResPath = "";
		isLoadedGeometry = false;
		if (_networkInterface != null)
		{
			_networkInterface.reset();
		}
		_filter = null;
		_networkInterface = new NetworkInterfaceTCP();
		_spacedatas.Clear();
	}

	public static bool validEmail(string strEmail)
	{
		return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)\r\n\t\t\t\t|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
	}

	public virtual void process()
	{
		if (_networkInterface != null)
		{
			_networkInterface.process();
		}
		Event.processInEvents();
		sendTick();
	}

	public Entity player()
	{
		if (entities.TryGetValue(entity_id, out var value))
		{
			return value;
		}
		return null;
	}

	public void _closeNetwork(NetworkInterfaceBase networkInterface)
	{
		networkInterface.close();
	}

	public void sendTick()
	{
		if (_networkInterface == null || !_networkInterface.connected)
		{
			return;
		}
		TimeSpan timeSpan = DateTime.Now - _lastTickTime;
		updatePlayerToServer();
		if (_args.serverHeartbeatTick <= 0 || timeSpan.Seconds <= _args.serverHeartbeatTick)
		{
			return;
		}
		if ((_lastTickCBTime - _lastTickTime).Seconds < 0)
		{
			Dbg.ERROR_MSG("sendTick: Receive appTick timeout!");
			_networkInterface.close();
			return;
		}
		Message value = null;
		Message value2 = null;
		Messages.messages.TryGetValue("Loginapp_onClientActiveTick", out value);
		Messages.messages.TryGetValue("Baseapp_onClientActiveTick", out value2);
		if (currserver == "loginapp")
		{
			if (value != null)
			{
				Bundle bundle = ObjectPool<Bundle>.createObject();
				bundle.newMessage(Messages.messages["Loginapp_onClientActiveTick"]);
				bundle.send(_networkInterface);
			}
		}
		else if (value2 != null)
		{
			Bundle bundle2 = ObjectPool<Bundle>.createObject();
			bundle2.newMessage(Messages.messages["Baseapp_onClientActiveTick"]);
			bundle2.send(_networkInterface);
		}
		_lastTickTime = DateTime.Now;
	}

	public void Client_onAppActiveTickCB()
	{
		_lastTickCBTime = DateTime.Now;
	}

	public void hello()
	{
		Bundle bundle = ObjectPool<Bundle>.createObject();
		if (currserver == "loginapp")
		{
			bundle.newMessage(Messages.messages["Loginapp_hello"]);
		}
		else
		{
			bundle.newMessage(Messages.messages["Baseapp_hello"]);
		}
		_filter = null;
		if (_args.networkEncryptType == NETWORK_ENCRYPT_TYPE.ENCRYPT_TYPE_BLOWFISH)
		{
			_filter = new BlowfishFilter();
			_encryptedKey = ((BlowfishFilter)_filter).key();
			_networkInterface.setFilter(null);
		}
		bundle.writeString(clientVersion);
		bundle.writeString(clientScriptVersion);
		bundle.writeBlob(_encryptedKey);
		bundle.send(_networkInterface);
	}

	public void Client_onHelloCB(MemoryStream stream)
	{
		string text = stream.readString();
		serverScriptVersion = stream.readString();
		stream.readString();
		string text2 = stream.readString();
		int num = stream.readInt32();
		Dbg.DEBUG_MSG("KBEngine::Client_onHelloCB: verInfo(" + text + "), scriptVersion(" + serverScriptVersion + "), srvProtocolMD5(" + serverProtocolMD5 + "), srvEntitydefMD5(" + serverEntitydefMD5 + "), + ctype(" + num + ")!");
		if (text != "Getting")
		{
			serverVersion = text;
			if (serverEntitydefMD5 != text2)
			{
				Dbg.ERROR_MSG("Client_onHelloCB: digest not match! serverEntitydefMD5=" + serverEntitydefMD5 + "(server: " + text2 + ")");
				Event.fireAll("onVersionNotMatch", clientVersion, serverVersion);
				return;
			}
		}
		if (_args.networkEncryptType == NETWORK_ENCRYPT_TYPE.ENCRYPT_TYPE_BLOWFISH)
		{
			_networkInterface.setFilter(_filter);
			_filter = null;
		}
		onServerDigest();
		if (currserver == "baseapp")
		{
			onLogin_baseapp();
		}
		else
		{
			onLogin_loginapp();
		}
	}

	public void Client_onImportServerErrorsDescr(MemoryStream stream)
	{
	}

	public void Client_onImportClientMessages(MemoryStream stream)
	{
	}

	public void Client_onImportClientEntityDef(MemoryStream stream)
	{
	}

	public void Client_onImportClientSDK(MemoryStream stream)
	{
		int num = 0;
		num = stream.readInt32();
		string text = stream.readString();
		int num2 = 0;
		num2 = stream.readInt32();
		byte[] array = new byte[0];
		array = stream.readBlob();
		Event.fireIn("onImportClientSDK", num, text, num2, array);
	}

	public void Client_onVersionNotMatch(MemoryStream stream)
	{
		serverVersion = stream.readString();
		Dbg.ERROR_MSG("Client_onVersionNotMatch: verInfo=" + clientVersion + "(server: " + serverVersion + ")");
		Event.fireAll("onVersionNotMatch", clientVersion, serverVersion);
	}

	public void Client_onScriptVersionNotMatch(MemoryStream stream)
	{
		serverScriptVersion = stream.readString();
		Dbg.ERROR_MSG("Client_onScriptVersionNotMatch: verInfo=" + clientScriptVersion + "(server: " + serverScriptVersion + ")");
		Event.fireAll("onScriptVersionNotMatch", clientScriptVersion, serverScriptVersion);
	}

	public void Client_onKicked(ushort failedcode)
	{
		Dbg.DEBUG_MSG("Client_onKicked: failedcode=" + failedcode + "(" + serverErr(failedcode) + ")");
		Event.fireAll("onKicked", failedcode);
	}

	public void login(string username, string password, byte[] datas)
	{
		app.username = username;
		app.password = password;
		app._clientdatas = datas;
		app.login_loginapp(noconnect: true);
	}

	public void login_loginapp(bool noconnect)
	{
		if (noconnect)
		{
			reset();
			_networkInterface.connectTo(_args.ip, _args.port, onConnectTo_loginapp_callback, null);
			return;
		}
		Dbg.DEBUG_MSG("KBEngine::login_loginapp(): send login! username=" + username);
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Loginapp_login"]);
		bundle.writeInt8((sbyte)_args.clientType);
		bundle.writeBlob(app._clientdatas);
		bundle.writeString(username);
		bundle.writeString(password);
		bundle.send(_networkInterface);
	}

	private void onConnectTo_loginapp_callback(string ip, int port, bool success, object userData)
	{
		_lastTickCBTime = DateTime.Now;
		if (!success)
		{
			Dbg.ERROR_MSG($"KBEngine::login_loginapp(): connect {ip}:{port} error!");
			return;
		}
		currserver = "loginapp";
		currstate = "login";
		Dbg.DEBUG_MSG($"KBEngine::login_loginapp(): connect {ip}:{port} success!");
		hello();
	}

	private void onLogin_loginapp()
	{
		_lastTickCBTime = DateTime.Now;
		login_loginapp(noconnect: false);
	}

	public void login_baseapp(bool noconnect)
	{
		if (noconnect)
		{
			Event.fireOut("onLoginBaseapp");
			_networkInterface.reset();
			if (_args.forceDisableUDP || baseappUdpPort == 0)
			{
				_networkInterface = new NetworkInterfaceTCP();
				_networkInterface.connectTo(baseappIP, baseappTcpPort, onConnectTo_baseapp_callback, null);
			}
			else
			{
				_networkInterface = new NetworkInterfaceKCP();
				_networkInterface.connectTo(baseappIP, baseappUdpPort, onConnectTo_baseapp_callback, null);
			}
		}
		else
		{
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_loginBaseapp"]);
			bundle.writeString(username);
			bundle.writeString(password);
			bundle.send(_networkInterface);
		}
	}

	private void onConnectTo_baseapp_callback(string ip, int port, bool success, object userData)
	{
		_lastTickCBTime = DateTime.Now;
		if (!success)
		{
			Dbg.ERROR_MSG($"KBEngine::login_baseapp(): connect {ip}:{port} error!");
			return;
		}
		currserver = "baseapp";
		currstate = "";
		Dbg.DEBUG_MSG($"KBEngine::login_baseapp(): connect {ip}:{port} success!");
		hello();
	}

	private void onLogin_baseapp()
	{
		_lastTickCBTime = DateTime.Now;
		login_baseapp(noconnect: false);
	}

	public void reloginBaseapp()
	{
		_lastTickTime = DateTime.Now;
		_lastTickCBTime = DateTime.Now;
		if (!_networkInterface.valid())
		{
			Event.fireAll("onReloginBaseapp");
			_networkInterface.reset();
			if (_args.forceDisableUDP || baseappUdpPort == 0)
			{
				_networkInterface = new NetworkInterfaceTCP();
				_networkInterface.connectTo(baseappIP, baseappTcpPort, onReConnectTo_baseapp_callback, null);
			}
			else
			{
				_networkInterface = new NetworkInterfaceKCP();
				_networkInterface.connectTo(baseappIP, baseappUdpPort, onReConnectTo_baseapp_callback, null);
			}
		}
	}

	private void onReConnectTo_baseapp_callback(string ip, int port, bool success, object userData)
	{
		if (!success)
		{
			Dbg.ERROR_MSG($"KBEngine::reloginBaseapp(): connect {ip}:{port} error!");
			return;
		}
		Dbg.DEBUG_MSG($"KBEngine::relogin_baseapp(): connect {ip}:{port} success!");
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Baseapp_reloginBaseapp"]);
		bundle.writeString(username);
		bundle.writeString(password);
		bundle.writeUint64(entity_uuid);
		bundle.writeInt32(entity_id);
		bundle.send(_networkInterface);
		_lastTickCBTime = DateTime.Now;
	}

	public void logout()
	{
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Baseapp_logoutBaseapp"]);
		bundle.writeUint64(entity_uuid);
		bundle.writeInt32(entity_id);
		bundle.send(_networkInterface);
	}

	public string serverErr(ushort id)
	{
		return _serverErrs.serverErrStr(id);
	}

	public void onOpenLoginapp_resetpassword()
	{
		Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_resetpassword: successfully!");
		currserver = "loginapp";
		currstate = "resetpassword";
		_lastTickCBTime = DateTime.Now;
		resetpassword_loginapp(noconnect: false);
	}

	public void resetPassword(string username)
	{
		app.username = username;
		resetpassword_loginapp(noconnect: true);
	}

	public void resetpassword_loginapp(bool noconnect)
	{
		if (noconnect)
		{
			reset();
			_networkInterface.connectTo(_args.ip, _args.port, onConnectTo_resetpassword_callback, null);
			return;
		}
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Loginapp_reqAccountResetPassword"]);
		bundle.writeString(username);
		bundle.send(_networkInterface);
	}

	private void onConnectTo_resetpassword_callback(string ip, int port, bool success, object userData)
	{
		_lastTickCBTime = DateTime.Now;
		if (!success)
		{
			Dbg.ERROR_MSG($"KBEngine::resetpassword_loginapp(): connect {ip}:{port} error!");
			return;
		}
		Dbg.DEBUG_MSG($"KBEngine::resetpassword_loginapp(): connect {ip}:{port} success!");
		onOpenLoginapp_resetpassword();
	}

	public void Client_onReqAccountResetPasswordCB(ushort failcode)
	{
		if (failcode != 0)
		{
			Dbg.ERROR_MSG("KBEngine::Client_onReqAccountResetPasswordCB: " + username + " failed! code=" + failcode + "(" + serverErr(failcode) + ")!");
		}
		else
		{
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountResetPasswordCB: " + username + " success!");
		}
	}

	public void bindAccountEmail(string emailAddress)
	{
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Baseapp_reqAccountBindEmail"]);
		bundle.writeInt32(entity_id);
		bundle.writeString(password);
		bundle.writeString(emailAddress);
		bundle.send(_networkInterface);
	}

	public void Client_onReqAccountBindEmailCB(ushort failcode)
	{
		if (failcode != 0)
		{
			Dbg.ERROR_MSG("KBEngine::Client_onReqAccountBindEmailCB: " + username + " failed! code=" + failcode + "(" + serverErr(failcode) + ")!");
		}
		else
		{
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountBindEmailCB: " + username + " success!");
		}
	}

	public void newPassword(string old_password, string new_password)
	{
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Baseapp_reqAccountNewPassword"]);
		bundle.writeInt32(entity_id);
		bundle.writeString(old_password);
		bundle.writeString(new_password);
		bundle.send(_networkInterface);
	}

	public void Client_onReqAccountNewPasswordCB(ushort failcode)
	{
		if (failcode != 0)
		{
			Dbg.ERROR_MSG("KBEngine::Client_onReqAccountNewPasswordCB: " + username + " failed! code=" + failcode + "(" + serverErr(failcode) + ")!");
		}
		else
		{
			Dbg.DEBUG_MSG("KBEngine::Client_onReqAccountNewPasswordCB: " + username + " success!");
		}
	}

	public void createAccount(string username, string password, byte[] datas)
	{
		app.username = username;
		app.password = password;
		app._clientdatas = datas;
		app.createAccount_loginapp(noconnect: true);
	}

	public void createAccount_loginapp(bool noconnect)
	{
		if (noconnect)
		{
			reset();
			_networkInterface.connectTo(_args.ip, _args.port, onConnectTo_createAccount_callback, null);
			return;
		}
		Bundle bundle = ObjectPool<Bundle>.createObject();
		bundle.newMessage(Messages.messages["Loginapp_reqCreateAccount"]);
		bundle.writeString(username);
		bundle.writeString(password);
		bundle.writeBlob(app._clientdatas);
		bundle.send(_networkInterface);
	}

	public void onOpenLoginapp_createAccount()
	{
		Dbg.DEBUG_MSG("KBEngine::onOpenLoginapp_createAccount: successfully!");
		currserver = "loginapp";
		currstate = "createAccount";
		_lastTickCBTime = DateTime.Now;
		createAccount_loginapp(noconnect: false);
	}

	private void onConnectTo_createAccount_callback(string ip, int port, bool success, object userData)
	{
		_lastTickCBTime = DateTime.Now;
		if (!success)
		{
			Dbg.ERROR_MSG($"KBEngine::createAccount_loginapp(): connect {ip}:{port} error!");
			return;
		}
		Dbg.DEBUG_MSG($"KBEngine::createAccount_loginapp(): connect {ip}:{port} success!");
		onOpenLoginapp_createAccount();
	}

	public void onServerDigest()
	{
	}

	public void Client_onLoginFailed(MemoryStream stream)
	{
		ushort num = stream.readUint16();
		_serverdatas = stream.readBlob();
		Dbg.ERROR_MSG("KBEngine::Client_onLoginFailed: failedcode(" + num + ":" + serverErr(num) + "), datas(" + _serverdatas.Length + ")!");
		Event.fireAll("onLoginFailed", num);
	}

	public void Client_onLoginSuccessfully(MemoryStream stream)
	{
		string text = (username = stream.readString());
		baseappIP = stream.readString();
		baseappTcpPort = stream.readUint16();
		baseappUdpPort = stream.readUint16();
		_serverdatas = stream.readBlob();
		Dbg.DEBUG_MSG("KBEngine::Client_onLoginSuccessfully: accountName(" + text + "), addr(" + baseappIP + ":" + baseappTcpPort + "|" + baseappUdpPort + "), datas(" + _serverdatas.Length + ")!");
		login_baseapp(noconnect: true);
	}

	public void Client_onLoginBaseappFailed(ushort failedcode)
	{
		Dbg.ERROR_MSG("KBEngine::Client_onLoginBaseappFailed: failedcode=" + failedcode + "(" + serverErr(failedcode) + ")!");
		Event.fireAll("onLoginBaseappFailed", failedcode);
	}

	public void Client_onReloginBaseappFailed(ushort failedcode)
	{
		Dbg.ERROR_MSG("KBEngine::Client_onReloginBaseappFailed: failedcode=" + failedcode + "(" + serverErr(failedcode) + ")!");
		Event.fireAll("onReloginBaseappFailed", failedcode);
	}

	public void Client_onReloginBaseappSuccessfully(MemoryStream stream)
	{
		entity_uuid = stream.readUint64();
		Dbg.DEBUG_MSG("KBEngine::Client_onReloginBaseappSuccessfully: name(" + username + ")!");
		Event.fireAll("onReloginBaseappSuccessfully");
	}

	public void Client_onCreatedProxies(ulong rndUUID, int eid, string entityType)
	{
		Dbg.DEBUG_MSG("KBEngine::Client_onCreatedProxies: eid(" + eid + "), entityType(" + entityType + ")!");
		entity_uuid = rndUUID;
		entity_id = eid;
		entity_type = entityType;
		if (!entities.ContainsKey(eid))
		{
			ScriptModule value = null;
			if (!EntityDef.moduledefs.TryGetValue(entityType, out value))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onCreatedProxies: not found module(" + entityType + ")!");
				return;
			}
			Type entityScript = value.entityScript;
			if (!(entityScript == null))
			{
				Entity entity = (Entity)Activator.CreateInstance(entityScript);
				entity.id = eid;
				entity.className = entityType;
				entity.onGetBase();
				entities[eid] = entity;
				MemoryStream value2 = null;
				_bufferedCreateEntityMessages.TryGetValue(eid, out value2);
				if (value2 != null)
				{
					Client_onUpdatePropertys(value2);
					_bufferedCreateEntityMessages.Remove(eid);
					value2.reclaimObject();
				}
				entity.__init__();
				entity.attachComponents();
				entity.inited = true;
				if (_args.isOnInitCallPropertysSetMethods)
				{
					entity.callPropertysSetMethods();
				}
			}
		}
		else
		{
			MemoryStream value3 = null;
			_bufferedCreateEntityMessages.TryGetValue(eid, out value3);
			if (value3 != null)
			{
				Client_onUpdatePropertys(value3);
				_bufferedCreateEntityMessages.Remove(eid);
				value3.reclaimObject();
			}
		}
	}

	public Entity findEntity(int entityID)
	{
		Entity value = null;
		if (!entities.TryGetValue(entityID, out value))
		{
			return null;
		}
		return value;
	}

	public int getViewEntityIDFromStream(MemoryStream stream)
	{
		if (!_args.useAliasEntityID)
		{
			return stream.readInt32();
		}
		int num = 0;
		if (_entityIDAliasIDList.Count > 255)
		{
			return stream.readInt32();
		}
		byte b = stream.readUint8();
		if (_entityIDAliasIDList.Count <= b)
		{
			return 0;
		}
		return _entityIDAliasIDList[b];
	}

	public void Client_onUpdatePropertysOptimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		onUpdatePropertys_(viewEntityIDFromStream, stream);
	}

	public void Client_onUpdatePropertys(MemoryStream stream)
	{
		int eid = stream.readInt32();
		onUpdatePropertys_(eid, stream);
	}

	public void onUpdatePropertys_(int eid, MemoryStream stream)
	{
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			MemoryStream value2 = null;
			if (_bufferedCreateEntityMessages.TryGetValue(eid, out value2))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onUpdatePropertys: entity(" + eid + ") not found!");
				return;
			}
			MemoryStream memoryStream = ObjectPool<MemoryStream>.createObject();
			memoryStream.wpos = stream.wpos;
			memoryStream.rpos = stream.rpos - 4;
			Array.Copy(stream.data(), memoryStream.data(), stream.wpos);
			_bufferedCreateEntityMessages[eid] = memoryStream;
		}
		else
		{
			value.onUpdatePropertys(stream);
		}
	}

	public void Client_onRemoteMethodCallOptimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		onRemoteMethodCall_(viewEntityIDFromStream, stream);
	}

	public void Client_onRemoteMethodCall(MemoryStream stream)
	{
		int eid = stream.readInt32();
		onRemoteMethodCall_(eid, stream);
	}

	public void onRemoteMethodCall_(int eid, MemoryStream stream)
	{
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onRemoteMethodCall: entity(" + eid + ") not found!");
		}
		else
		{
			value.onRemoteMethodCall(stream);
		}
	}

	public void Client_onEntityEnterWorld(MemoryStream stream)
	{
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		int num = stream.readInt32();
		if (entity_id > 0 && entity_id != num)
		{
			_entityIDAliasIDList.Add(num);
		}
		ushort key = ((EntityDef.idmoduledefs.Count <= 255) ? stream.readUint8() : stream.readUint16());
		sbyte b = 1;
		if (stream.length() != 0)
		{
			b = stream.readInt8();
		}
		string name = EntityDef.idmoduledefs[key].name;
		Entity value = null;
		if (!entities.TryGetValue(num, out value))
		{
			MemoryStream value2 = null;
			if (!_bufferedCreateEntityMessages.TryGetValue(num, out value2))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterWorld: entity(" + num + ") not found!");
				return;
			}
			ScriptModule value3 = null;
			if (!EntityDef.moduledefs.TryGetValue(name, out value3))
			{
				Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterWorld: not found module(" + name + ")!");
			}
			Type entityScript = value3.entityScript;
			if (!(entityScript == null))
			{
				value = (Entity)Activator.CreateInstance(entityScript);
				value.id = num;
				value.className = name;
				value.onGetCell();
				entities[num] = value;
				Client_onUpdatePropertys(value2);
				_bufferedCreateEntityMessages.Remove(num);
				value2.reclaimObject();
				value.isOnGround = b > 0;
				value.onDirectionChanged(value.direction);
				value.onPositionChanged(value.position);
				value.__init__();
				value.attachComponents();
				value.inited = true;
				value.inWorld = true;
				value.enterWorld();
				if (_args.isOnInitCallPropertysSetMethods)
				{
					value.callPropertysSetMethods();
				}
			}
		}
		else if (!value.inWorld)
		{
			_entityIDAliasIDList.Clear();
			clearEntities(isall: false);
			entities[value.id] = value;
			value.onGetCell();
			value.onDirectionChanged(value.direction);
			value.onPositionChanged(value.position);
			_entityServerPos = value.position;
			value.isOnGround = b > 0;
			value.inWorld = true;
			value.enterWorld();
			if (_args.isOnInitCallPropertysSetMethods)
			{
				value.callPropertysSetMethods();
			}
		}
	}

	public void Client_onEntityLeaveWorldOptimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		app.Client_onEntityLeaveWorld(viewEntityIDFromStream);
	}

	public void Client_onEntityLeaveWorld(int eid)
	{
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onEntityLeaveWorld: entity(" + eid + ") not found!");
			return;
		}
		if (value.inWorld)
		{
			value.leaveWorld();
		}
		if (entity_id == eid)
		{
			clearSpace(isall: false);
			value.onLoseCell();
			return;
		}
		if (_controlledEntities.Remove(value))
		{
			Event.fireOut("onLoseControlledEntity", value);
		}
		entities.Remove(eid);
		value.destroy();
		_entityIDAliasIDList.Remove(eid);
	}

	public void Client_onEntityEnterSpace(MemoryStream stream)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		int num = stream.readInt32();
		spaceID = stream.readUint32();
		sbyte b = 1;
		if (stream.length() != 0)
		{
			b = stream.readInt8();
		}
		Entity value = null;
		if (!entities.TryGetValue(num, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onEntityEnterSpace: entity(" + num + ") not found!");
			return;
		}
		value.isOnGround = b > 0;
		_entityServerPos = value.position;
		value.enterSpace();
	}

	public void Client_onEntityLeaveSpace(int eid)
	{
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onEntityLeaveSpace: entity(" + eid + ") not found!");
			return;
		}
		value.leaveSpace();
		clearSpace(isall: false);
	}

	public void Client_onCreateAccountResult(MemoryStream stream)
	{
		ushort num = stream.readUint16();
		byte[] array = stream.readBlob();
		Event.fireOut("onCreateAccountResult", num, array);
		if (num != 0)
		{
			Dbg.WARNING_MSG("KBEngine::Client_onCreateAccountResult: " + username + " create is failed! code=" + num + "(" + serverErr(num) + ")!");
		}
		else
		{
			Dbg.DEBUG_MSG("KBEngine::Client_onCreateAccountResult: " + username + " create is successfully!");
		}
	}

	public void Client_onControlEntity(int eid, sbyte isControlled)
	{
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onControlEntity: entity(" + eid + ") not found!");
			return;
		}
		bool flag = isControlled != 0;
		if (flag)
		{
			if (player().id != value.id)
			{
				_controlledEntities.Add(value);
			}
		}
		else
		{
			_controlledEntities.Remove(value);
		}
		value.isControlled = flag;
		try
		{
			value.onControlled(flag);
			Event.fireOut("onControlled", value, flag);
		}
		catch (Exception arg)
		{
			Dbg.ERROR_MSG(string.Format("KBEngine::Client_onControlEntity: entity id = '{0}', is controlled = '{1}', error = '{1}'", eid, flag, arg));
		}
	}

	public void updatePlayerToServer()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		if (_updatePlayerToServerPeroid <= 0.01f || spaceID == 0)
		{
			return;
		}
		DateTime now = DateTime.Now;
		TimeSpan timeSpan = now - _lastUpdateToServerTime;
		if ((float)timeSpan.Ticks < _updatePlayerToServerPeroid * 10000f)
		{
			return;
		}
		Entity entity = player();
		if (entity == null || !entity.inWorld || entity.isControlled)
		{
			return;
		}
		_lastUpdateToServerTime = now - (timeSpan - TimeSpan.FromTicks(Convert.ToInt64(_updatePlayerToServerPeroid * 10000f)));
		Vector3 position = entity.position;
		Vector3 direction = entity.direction;
		bool num = Vector3.Distance(entity._entityLastLocalPos, position) > 0.001f;
		bool flag = Vector3.Distance(entity._entityLastLocalDir, direction) > 0.001f;
		if (num || flag)
		{
			entity._entityLastLocalPos = position;
			entity._entityLastLocalDir = direction;
			Bundle bundle = ObjectPool<Bundle>.createObject();
			bundle.newMessage(Messages.messages["Baseapp_onUpdateDataFromClient"]);
			bundle.writeFloat(position.x);
			bundle.writeFloat(position.y);
			bundle.writeFloat(position.z);
			double num2 = (double)direction.x / 360.0 * (Math.PI * 2.0);
			double num3 = (double)direction.y / 360.0 * (Math.PI * 2.0);
			double num4 = (double)direction.z / 360.0 * (Math.PI * 2.0);
			if (num2 - Math.PI > 0.0)
			{
				num2 -= Math.PI * 2.0;
			}
			if (num3 - Math.PI > 0.0)
			{
				num3 -= Math.PI * 2.0;
			}
			if (num4 - Math.PI > 0.0)
			{
				num4 -= Math.PI * 2.0;
			}
			bundle.writeFloat((float)num2);
			bundle.writeFloat((float)num3);
			bundle.writeFloat((float)num4);
			bundle.writeUint8((byte)(entity.isOnGround ? 1u : 0u));
			bundle.writeUint32(spaceID);
			bundle.send(_networkInterface);
		}
		for (int i = 0; i < _controlledEntities.Count; i++)
		{
			Entity entity2 = _controlledEntities[i];
			position = entity2.position;
			direction = entity2.direction;
			bool num5 = Vector3.Distance(entity2._entityLastLocalPos, position) > 0.001f;
			flag = Vector3.Distance(entity2._entityLastLocalDir, direction) > 0.001f;
			if (num5 || flag)
			{
				entity2._entityLastLocalPos = position;
				entity2._entityLastLocalDir = direction;
				Bundle bundle2 = ObjectPool<Bundle>.createObject();
				bundle2.newMessage(Messages.messages["Baseapp_onUpdateDataFromClientForControlledEntity"]);
				bundle2.writeInt32(entity2.id);
				bundle2.writeFloat(position.x);
				bundle2.writeFloat(position.y);
				bundle2.writeFloat(position.z);
				double num6 = (double)direction.x / 360.0 * (Math.PI * 2.0);
				double num7 = (double)direction.y / 360.0 * (Math.PI * 2.0);
				double num8 = (double)direction.z / 360.0 * (Math.PI * 2.0);
				if (num6 - Math.PI > 0.0)
				{
					num6 -= Math.PI * 2.0;
				}
				if (num7 - Math.PI > 0.0)
				{
					num7 -= Math.PI * 2.0;
				}
				if (num8 - Math.PI > 0.0)
				{
					num8 -= Math.PI * 2.0;
				}
				bundle2.writeFloat((float)num6);
				bundle2.writeFloat((float)num7);
				bundle2.writeFloat((float)num8);
				bundle2.writeUint8((byte)(entity2.isOnGround ? 1u : 0u));
				bundle2.writeUint32(spaceID);
				bundle2.send(_networkInterface);
			}
		}
	}

	public void addSpaceGeometryMapping(uint uspaceID, string respath)
	{
		Dbg.DEBUG_MSG("KBEngine::addSpaceGeometryMapping: spaceID(" + uspaceID + "), respath(" + respath + ")!");
		isLoadedGeometry = true;
		spaceID = uspaceID;
		spaceResPath = respath;
		Event.fireOut("addSpaceGeometryMapping", spaceResPath);
	}

	public void clearSpace(bool isall)
	{
		_entityIDAliasIDList.Clear();
		_spacedatas.Clear();
		clearEntities(isall);
		isLoadedGeometry = false;
		spaceID = 0u;
	}

	public void clearEntities(bool isall)
	{
		_controlledEntities.Clear();
		if (!isall)
		{
			Entity entity = player();
			foreach (KeyValuePair<int, Entity> entity2 in entities)
			{
				if (entity2.Key != entity.id)
				{
					if (entity2.Value.inWorld)
					{
						entity2.Value.leaveWorld();
					}
					entity2.Value.destroy();
				}
			}
			entities.Clear();
			entities[entity.id] = entity;
			return;
		}
		foreach (KeyValuePair<int, Entity> entity3 in entities)
		{
			if (entity3.Value.inWorld)
			{
				entity3.Value.leaveWorld();
			}
			entity3.Value.destroy();
		}
		entities.Clear();
	}

	public void Client_initSpaceData(MemoryStream stream)
	{
		clearSpace(isall: false);
		spaceID = stream.readUint32();
		while (stream.length() != 0)
		{
			string key = stream.readString();
			string value = stream.readString();
			Client_setSpaceData(spaceID, key, value);
		}
		Dbg.DEBUG_MSG("KBEngine::Client_initSpaceData: spaceID(" + spaceID + "), size(" + _spacedatas.Count + ")!");
	}

	public void Client_setSpaceData(uint spaceID, string key, string value)
	{
		Dbg.DEBUG_MSG("KBEngine::Client_setSpaceData: spaceID(" + spaceID + "), key(" + key + "), value(" + value + ")!");
		_spacedatas[key] = value;
		if (key == "_mapping")
		{
			addSpaceGeometryMapping(spaceID, value);
		}
		Event.fireOut("onSetSpaceData", spaceID, key, value);
	}

	public void Client_delSpaceData(uint spaceID, string key)
	{
		Dbg.DEBUG_MSG("KBEngine::Client_delSpaceData: spaceID(" + spaceID + "), key(" + key + ")");
		_spacedatas.Remove(key);
		Event.fireOut("onDelSpaceData", spaceID, key);
	}

	public string getSpaceData(string key)
	{
		string value = "";
		if (!_spacedatas.TryGetValue(key, out value))
		{
			return "";
		}
		return value;
	}

	public void Client_onEntityDestroyed(int eid)
	{
		Dbg.DEBUG_MSG("KBEngine::Client_onEntityDestroyed: entity(" + eid + ")");
		Entity value = null;
		if (!entities.TryGetValue(eid, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onEntityDestroyed: entity(" + eid + ") not found!");
			return;
		}
		if (value.inWorld)
		{
			if (entity_id == eid)
			{
				clearSpace(isall: false);
			}
			value.leaveWorld();
		}
		if (_controlledEntities.Remove(value))
		{
			Event.fireOut("onLoseControlledEntity", value);
		}
		entities.Remove(eid);
		value.destroy();
	}

	public void Client_onUpdateBasePos(float x, float y, float z)
	{
		_entityServerPos.x = x;
		_entityServerPos.y = y;
		_entityServerPos.z = z;
		Entity entity = player();
		if (entity != null && entity.isControlled)
		{
			((Vector3)(ref entity.position)).Set(_entityServerPos.x, _entityServerPos.y, _entityServerPos.z);
			Event.fireOut("updatePosition", entity);
			entity.onUpdateVolatileData();
		}
	}

	public void Client_onUpdateBasePosXZ(float x, float z)
	{
		_entityServerPos.x = x;
		_entityServerPos.z = z;
		Entity entity = player();
		if (entity != null && entity.isControlled)
		{
			entity.position.x = _entityServerPos.x;
			entity.position.z = _entityServerPos.z;
			Event.fireOut("updatePosition", entity);
			entity.onUpdateVolatileData();
		}
	}

	public void Client_onUpdateBaseDir(MemoryStream stream)
	{
		float num = stream.readFloat() * 360f / ((float)Math.PI * 2f);
		float num2 = stream.readFloat() * 360f / ((float)Math.PI * 2f);
		float num3 = stream.readFloat() * 360f / ((float)Math.PI * 2f);
		Entity entity = player();
		if (entity != null && entity.isControlled)
		{
			((Vector3)(ref entity.direction)).Set(num3, num2, num);
			Event.fireOut("set_direction", entity);
			entity.onUpdateVolatileData();
		}
	}

	public void Client_onUpdateData(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Entity value = null;
		if (!entities.TryGetValue(viewEntityIDFromStream, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onUpdateData: entity(" + viewEntityIDFromStream + ") not found!");
		}
	}

	public void Client_onSetEntityPosAndDir(MemoryStream stream)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		int num = stream.readInt32();
		Entity value = null;
		if (!entities.TryGetValue(num, out value))
		{
			Dbg.ERROR_MSG("KBEngine::Client_onSetEntityPosAndDir: entity(" + num + ") not found!");
			return;
		}
		Vector3 oldValue = default(Vector3);
		((Vector3)(ref oldValue))._002Ector(value.position.x, value.position.y, value.position.z);
		Vector3 oldValue2 = default(Vector3);
		((Vector3)(ref oldValue2))._002Ector(value.direction.x, value.direction.y, value.direction.z);
		value.position.x = stream.readFloat();
		value.position.y = stream.readFloat();
		value.position.z = stream.readFloat();
		value.direction.x = stream.readFloat();
		value.direction.y = stream.readFloat();
		value.direction.z = stream.readFloat();
		value._entityLastLocalPos = value.position;
		value._entityLastLocalDir = value.direction;
		value.onDirectionChanged(oldValue2);
		value.onPositionChanged(oldValue);
	}

	public void Client_onUpdateData_ypr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, roll, -1, isOptimized: false);
	}

	public void Client_onUpdateData_yp(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, pitch, KBEMath.KBE_FLT_MAX, -1, isOptimized: false);
	}

	public void Client_onUpdateData_yr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float yaw = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, roll, -1, isOptimized: false);
	}

	public void Client_onUpdateData_pr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, roll, -1, isOptimized: false);
	}

	public void Client_onUpdateData_y(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float yaw = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, isOptimized: false);
	}

	public void Client_onUpdateData_p(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, -1, isOptimized: false);
	}

	public void Client_onUpdateData_r(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, -1, isOptimized: false);
	}

	public void Client_onUpdateData_xz(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_ypr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, pitch, roll, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_yp(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, pitch, KBEMath.KBE_FLT_MAX, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_yr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, roll, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_pr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, roll, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_y(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_p(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xz_r(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float z = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, KBEMath.KBE_FLT_MAX, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 1, isOptimized: false);
	}

	public void Client_onUpdateData_xyz(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_ypr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, pitch, roll, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_yp(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, pitch, KBEMath.KBE_FLT_MAX, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_yr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, KBEMath.KBE_FLT_MAX, roll, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_pr(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float pitch = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, pitch, roll, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_y(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float yaw = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, yaw, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_p(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float pitch = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, pitch, KBEMath.KBE_FLT_MAX, 0, isOptimized: false);
	}

	public void Client_onUpdateData_xyz_r(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		float x = stream.readFloat();
		float y = stream.readFloat();
		float z = stream.readFloat();
		float roll = stream.readFloat();
		_updateVolatileData(viewEntityIDFromStream, x, y, z, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, roll, 0, isOptimized: false);
	}

	public void Client_onUpdateData_ypr_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		sbyte b3 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, b2, b3, -1, isOptimized: true);
	}

	public void Client_onUpdateData_yp_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, b2, KBEMath.KBE_FLT_MAX, -1, isOptimized: true);
	}

	public void Client_onUpdateData_yr_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, KBEMath.KBE_FLT_MAX, b2, -1, isOptimized: true);
	}

	public void Client_onUpdateData_pr_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, b2, -1, isOptimized: true);
	}

	public void Client_onUpdateData_y_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, -1, isOptimized: true);
	}

	public void Client_onUpdateData_p_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, KBEMath.KBE_FLT_MAX, -1, isOptimized: true);
	}

	public void Client_onUpdateData_r_optimized(MemoryStream stream)
	{
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, -1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_ypr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		sbyte b3 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], b, b2, b3, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_yp_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], b, b2, KBEMath.KBE_FLT_MAX, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_yr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], b, KBEMath.KBE_FLT_MAX, b2, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_pr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, b, b2, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_y_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_p_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, b, KBEMath.KBE_FLT_MAX, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xz_r_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], KBEMath.KBE_FLT_MAX, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, 1, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_ypr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		sbyte b3 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], b, b2, b3, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_yp_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], b, b2, KBEMath.KBE_FLT_MAX, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_yr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], b, KBEMath.KBE_FLT_MAX, b2, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_pr_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		sbyte b2 = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, b, b2, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_y_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], b, KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_p_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, b, KBEMath.KBE_FLT_MAX, 0, isOptimized: true);
	}

	public void Client_onUpdateData_xyz_r_optimized(MemoryStream stream)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		int viewEntityIDFromStream = getViewEntityIDFromStream(stream);
		Vector2 val = stream.readPackXZ();
		float y = stream.readPackY();
		sbyte b = stream.readInt8();
		_updateVolatileData(viewEntityIDFromStream, ((Vector2)(ref val))[0], y, ((Vector2)(ref val))[1], KBEMath.KBE_FLT_MAX, KBEMath.KBE_FLT_MAX, b, 0, isOptimized: true);
	}

	private void _updateVolatileData(int entityID, float x, float y, float z, float yaw, float pitch, float roll, sbyte isOnGround, bool isOptimized)
	{
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		Entity value = null;
		if (!entities.TryGetValue(entityID, out value))
		{
			Dbg.ERROR_MSG("KBEngine::_updateVolatileData: entity(" + entityID + ") not found!");
			return;
		}
		if (isOnGround >= 0)
		{
			value.isOnGround = isOnGround > 0;
		}
		bool flag = false;
		if (roll != KBEMath.KBE_FLT_MAX)
		{
			flag = true;
			value.direction.x = KBEMath.int82angle((sbyte)roll, half: false) * 360f / ((float)Math.PI * 2f);
		}
		if (pitch != KBEMath.KBE_FLT_MAX)
		{
			flag = true;
			value.direction.y = KBEMath.int82angle((sbyte)pitch, half: false) * 360f / ((float)Math.PI * 2f);
		}
		if (yaw != KBEMath.KBE_FLT_MAX)
		{
			flag = true;
			value.direction.z = KBEMath.int82angle((sbyte)yaw, half: false) * 360f / ((float)Math.PI * 2f);
		}
		bool flag2 = false;
		if (flag)
		{
			Event.fireOut("set_direction", value);
			flag2 = true;
		}
		bool num = x != KBEMath.KBE_FLT_MAX || y != KBEMath.KBE_FLT_MAX || z != KBEMath.KBE_FLT_MAX;
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
		if (num)
		{
			Vector3 position = (isOptimized ? new Vector3(x + _entityServerPos.x, y + _entityServerPos.y, z + _entityServerPos.z) : new Vector3(x, y, z));
			value.position = position;
			flag2 = true;
			Event.fireOut("updatePosition", value);
		}
		if (flag2)
		{
			value.onUpdateVolatileData();
		}
	}

	public void Client_onStreamDataStarted(short id, uint datasize, string descr)
	{
		Event.fireOut("onStreamDataStarted", id, datasize, descr);
	}

	public void Client_onStreamDataRecv(MemoryStream stream)
	{
		short num = stream.readInt16();
		byte[] array = stream.readBlob();
		Event.fireOut("onStreamDataRecv", num, array);
	}

	public void Client_onStreamDataCompleted(short id)
	{
		Event.fireOut("onStreamDataCompleted", id);
	}
}
