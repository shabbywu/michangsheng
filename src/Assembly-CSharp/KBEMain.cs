using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class KBEMain : MonoBehaviour
{
	// Token: 0x060003E0 RID: 992 RVA: 0x0001593B File Offset: 0x00013B3B
	protected virtual void Awake()
	{
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001594D File Offset: 0x00013B4D
	protected virtual void Start()
	{
		MonoBehaviour.print("clientapp::start()");
		this.installEvents();
		this.initKBEngine();
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00015965 File Offset: 0x00013B65
	public virtual void installEvents()
	{
		Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
		Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00004095 File Offset: 0x00002295
	public void onVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00004095 File Offset: 0x00002295
	public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
	{
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001598C File Offset: 0x00013B8C
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

	// Token: 0x060003E6 RID: 998 RVA: 0x00015A81 File Offset: 0x00013C81
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

	// Token: 0x060003E7 RID: 999 RVA: 0x00015AB3 File Offset: 0x00013CB3
	protected virtual void FixedUpdate()
	{
		this.KBEUpdate();
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00015ABB File Offset: 0x00013CBB
	public virtual void KBEUpdate()
	{
		Event.processOutEvents();
	}

	// Token: 0x0400020F RID: 527
	public KBEngineApp gameapp;

	// Token: 0x04000210 RID: 528
	public DEBUGLEVEL debugLevel;

	// Token: 0x04000211 RID: 529
	public bool isMultiThreads;

	// Token: 0x04000212 RID: 530
	public string ip = "127.0.0.1";

	// Token: 0x04000213 RID: 531
	public int port = 20013;

	// Token: 0x04000214 RID: 532
	public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

	// Token: 0x04000215 RID: 533
	public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

	// Token: 0x04000216 RID: 534
	public int syncPlayerMS = 100;

	// Token: 0x04000217 RID: 535
	public int threadUpdateHZ = 20;

	// Token: 0x04000218 RID: 536
	public int serverHeartbeatTick = 60;

	// Token: 0x04000219 RID: 537
	public int TCP_SEND_BUFFER_MAX = 1460;

	// Token: 0x0400021A RID: 538
	public int TCP_RECV_BUFFER_MAX = 1460;

	// Token: 0x0400021B RID: 539
	public int UDP_SEND_BUFFER_MAX = 1472;

	// Token: 0x0400021C RID: 540
	public int UDP_RECV_BUFFER_MAX = 1472;

	// Token: 0x0400021D RID: 541
	public bool useAliasEntityID = true;

	// Token: 0x0400021E RID: 542
	public bool isOnInitCallPropertysSetMethods = true;

	// Token: 0x0400021F RID: 543
	public bool forceDisableUDP;

	// Token: 0x04000220 RID: 544
	public bool automaticallyUpdateSDK = true;
}
