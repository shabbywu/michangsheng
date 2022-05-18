using System;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200038E RID: 910
public class UINPCLiaoTian : MonoBehaviour
{
	// Token: 0x060019A6 RID: 6566 RVA: 0x00016192 File Offset: 0x00014392
	private void Awake()
	{
		UINPCLiaoTian.Inst = this;
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x0001619A File Offset: 0x0001439A
	public void RefreshUI()
	{
		this.Init();
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x000161A2 File Offset: 0x000143A2
	public void OnChatExit()
	{
		Object.Destroy(this.nowChatObj, 2f);
		if (this.onChatExit != null)
		{
			this.onChatExit.Invoke();
		}
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x000E3B1C File Offset: 0x000E1D1C
	public void ShowTalk(int avatarid, string msg, UnityAction onExit = null)
	{
		this.nowChatObj = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCLiaoTianTalk"));
		Say say = (Say)this.nowChatObj.transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
		say.pubAvatarIntID = avatarid;
		say.SetStandardText(msg);
		this.onChatExit = onExit;
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x000161C7 File Offset: 0x000143C7
	private void Init()
	{
		if (!this._inited)
		{
			this._inited = true;
		}
	}

	// Token: 0x040014D2 RID: 5330
	public static UINPCLiaoTian Inst;

	// Token: 0x040014D3 RID: 5331
	private UINPCData npc;

	// Token: 0x040014D4 RID: 5332
	private bool _inited;

	// Token: 0x040014D5 RID: 5333
	public RectTransform ContentRT;

	// Token: 0x040014D6 RID: 5334
	public GameObject SVItemPrefab;

	// Token: 0x040014D7 RID: 5335
	public Flowchart NPCFlowchart;

	// Token: 0x040014D8 RID: 5336
	private GameObject nowChatObj;

	// Token: 0x040014D9 RID: 5337
	[HideInInspector]
	public UnityAction onChatExit;
}
