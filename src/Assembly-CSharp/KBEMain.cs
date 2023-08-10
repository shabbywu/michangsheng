using KBEngine;
using UnityEngine;

public class KBEMain : MonoBehaviour
{
	public KBEngineApp gameapp;

	public DEBUGLEVEL debugLevel;

	public bool isMultiThreads;

	public string ip = "127.0.0.1";

	public int port = 20013;

	public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

	public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

	public int syncPlayerMS = 100;

	public int threadUpdateHZ = 20;

	public int serverHeartbeatTick = 60;

	public int TCP_SEND_BUFFER_MAX = 1460;

	public int TCP_RECV_BUFFER_MAX = 1460;

	public int UDP_SEND_BUFFER_MAX = 1472;

	public int UDP_RECV_BUFFER_MAX = 1472;

	public bool useAliasEntityID = true;

	public bool isOnInitCallPropertysSetMethods = true;

	public bool forceDisableUDP;

	public bool automaticallyUpdateSDK = true;

	protected virtual void Awake()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)((Component)this).transform).gameObject);
	}

	protected virtual void Start()
	{
		MonoBehaviour.print((object)"clientapp::start()");
		installEvents();
		initKBEngine();
	}

	public virtual void installEvents()
	{
		Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
		Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
	}

	public void onVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	public virtual void initKBEngine()
	{
		Dbg.debugLevel = debugLevel;
		KBEngineArgs kBEngineArgs = new KBEngineArgs();
		kBEngineArgs.ip = ip;
		kBEngineArgs.port = port;
		kBEngineArgs.clientType = clientType;
		kBEngineArgs.networkEncryptType = networkEncryptType;
		kBEngineArgs.syncPlayerMS = syncPlayerMS;
		kBEngineArgs.threadUpdateHZ = threadUpdateHZ;
		kBEngineArgs.serverHeartbeatTick = serverHeartbeatTick / 2;
		kBEngineArgs.useAliasEntityID = useAliasEntityID;
		kBEngineArgs.isOnInitCallPropertysSetMethods = isOnInitCallPropertysSetMethods;
		kBEngineArgs.forceDisableUDP = forceDisableUDP;
		kBEngineArgs.TCP_SEND_BUFFER_MAX = (uint)TCP_SEND_BUFFER_MAX;
		kBEngineArgs.TCP_RECV_BUFFER_MAX = (uint)TCP_RECV_BUFFER_MAX;
		kBEngineArgs.UDP_SEND_BUFFER_MAX = (uint)UDP_SEND_BUFFER_MAX;
		kBEngineArgs.UDP_RECV_BUFFER_MAX = (uint)UDP_RECV_BUFFER_MAX;
		kBEngineArgs.isMultiThreads = isMultiThreads;
		if (isMultiThreads)
		{
			gameapp = new KBEngineAppThread(kBEngineArgs);
		}
		else
		{
			gameapp = new KBEngineApp(kBEngineArgs);
		}
	}

	protected virtual void OnDestroy()
	{
		MonoBehaviour.print((object)"clientapp::OnDestroy(): begin");
		if (KBEngineApp.app != null)
		{
			KBEngineApp.app.destroy();
			KBEngineApp.app = null;
		}
		Event.clear();
		MonoBehaviour.print((object)"clientapp::OnDestroy(): end");
	}

	protected virtual void FixedUpdate()
	{
		KBEUpdate();
	}

	public virtual void KBEUpdate()
	{
		Event.processOutEvents();
	}
}
