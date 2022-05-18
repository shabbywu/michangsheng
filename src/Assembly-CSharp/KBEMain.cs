using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class KBEMain : MonoBehaviour
{
	// Token: 0x06000428 RID: 1064 RVA: 0x00007AB7 File Offset: 0x00005CB7
	protected virtual void Awake()
	{
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00007AC9 File Offset: 0x00005CC9
	protected virtual void Start()
	{
		MonoBehaviour.print("clientapp::start()");
		this.installEvents();
		this.initKBEngine();
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00007AE1 File Offset: 0x00005CE1
	public virtual void installEvents()
	{
		Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
		Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x000042DD File Offset: 0x000024DD
	public void onVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x000042DD File Offset: 0x000024DD
	public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0006D348 File Offset: 0x0006B548
	public virtual void initKBEngine()
	{
		Dbg.debugLevel = this.debugLevel;
		KBEngineArgs kbengineArgs = new KBEngineArgs();
		kbengineArgs.ip = this.ip;
		kbengineArgs.port = this.port;
		kbengineArgs.clientType = this.clientType;
		kbengineArgs.networkEncryptType = this.networkEncryptType;
		kbengineArgs.syncPlayerMS = this.syncPlayerMS;
		kbengineArgs.threadUpdateHZ = this.threadUpdateHZ;
		kbengineArgs.serverHeartbeatTick = this.serverHeartbeatTick / 2;
		kbengineArgs.useAliasEntityID = this.useAliasEntityID;
		kbengineArgs.isOnInitCallPropertysSetMethods = this.isOnInitCallPropertysSetMethods;
		kbengineArgs.forceDisableUDP = this.forceDisableUDP;
		kbengineArgs.TCP_SEND_BUFFER_MAX = (uint)this.TCP_SEND_BUFFER_MAX;
		kbengineArgs.TCP_RECV_BUFFER_MAX = (uint)this.TCP_RECV_BUFFER_MAX;
		kbengineArgs.UDP_SEND_BUFFER_MAX = (uint)this.UDP_SEND_BUFFER_MAX;
		kbengineArgs.UDP_RECV_BUFFER_MAX = (uint)this.UDP_RECV_BUFFER_MAX;
		kbengineArgs.isMultiThreads = this.isMultiThreads;
		if (this.isMultiThreads)
		{
			this.gameapp = new KBEngineAppThread(kbengineArgs);
			return;
		}
		this.gameapp = new KBEngineApp(kbengineArgs);
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00007B05 File Offset: 0x00005D05
	protected virtual void OnDestroy()
	{
		MonoBehaviour.print("clientapp::OnDestroy(): begin");
		if (KBEngineApp.app != null)
		{
			KBEngineApp.app.destroy();
			KBEngineApp.app = null;
		}
		Event.clear();
		MonoBehaviour.print("clientapp::OnDestroy(): end");
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00007B37 File Offset: 0x00005D37
	protected virtual void FixedUpdate()
	{
		this.KBEUpdate();
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00007B3F File Offset: 0x00005D3F
	public virtual void KBEUpdate()
	{
		Event.processOutEvents();
	}

	// Token: 0x04000255 RID: 597
	public KBEngineApp gameapp;

	// Token: 0x04000256 RID: 598
	public DEBUGLEVEL debugLevel;

	// Token: 0x04000257 RID: 599
	public bool isMultiThreads;

	// Token: 0x04000258 RID: 600
	public string ip = "127.0.0.1";

	// Token: 0x04000259 RID: 601
	public int port = 20013;

	// Token: 0x0400025A RID: 602
	public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

	// Token: 0x0400025B RID: 603
	public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

	// Token: 0x0400025C RID: 604
	public int syncPlayerMS = 100;

	// Token: 0x0400025D RID: 605
	public int threadUpdateHZ = 20;

	// Token: 0x0400025E RID: 606
	public int serverHeartbeatTick = 60;

	// Token: 0x0400025F RID: 607
	public int TCP_SEND_BUFFER_MAX = 1460;

	// Token: 0x04000260 RID: 608
	public int TCP_RECV_BUFFER_MAX = 1460;

	// Token: 0x04000261 RID: 609
	public int UDP_SEND_BUFFER_MAX = 1472;

	// Token: 0x04000262 RID: 610
	public int UDP_RECV_BUFFER_MAX = 1472;

	// Token: 0x04000263 RID: 611
	public bool useAliasEntityID = true;

	// Token: 0x04000264 RID: 612
	public bool isOnInitCallPropertysSetMethods = true;

	// Token: 0x04000265 RID: 613
	public bool forceDisableUDP;

	// Token: 0x04000266 RID: 614
	public bool automaticallyUpdateSDK = true;
}
