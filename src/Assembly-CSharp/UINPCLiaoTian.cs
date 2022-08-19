using System;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000275 RID: 629
public class UINPCLiaoTian : MonoBehaviour
{
	// Token: 0x060016F4 RID: 5876 RVA: 0x0009C478 File Offset: 0x0009A678
	private void Awake()
	{
		UINPCLiaoTian.Inst = this;
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x0009C480 File Offset: 0x0009A680
	public void RefreshUI()
	{
		this.Init();
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x0009C488 File Offset: 0x0009A688
	public void OnChatExit()
	{
		Object.Destroy(this.nowChatObj, 2f);
		if (this.onChatExit != null)
		{
			this.onChatExit.Invoke();
		}
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x0009C4B0 File Offset: 0x0009A6B0
	public void ShowTalk(int avatarid, string msg, UnityAction onExit = null)
	{
		this.nowChatObj = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCLiaoTianTalk"));
		Say say = (Say)this.nowChatObj.transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
		say.pubAvatarIntID = avatarid;
		say.SetStandardText(msg);
		this.onChatExit = onExit;
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x0009C51A File Offset: 0x0009A71A
	private void Init()
	{
		if (!this._inited)
		{
			this._inited = true;
		}
	}

	// Token: 0x04001181 RID: 4481
	public static UINPCLiaoTian Inst;

	// Token: 0x04001182 RID: 4482
	private UINPCData npc;

	// Token: 0x04001183 RID: 4483
	private bool _inited;

	// Token: 0x04001184 RID: 4484
	public RectTransform ContentRT;

	// Token: 0x04001185 RID: 4485
	public GameObject SVItemPrefab;

	// Token: 0x04001186 RID: 4486
	public Flowchart NPCFlowchart;

	// Token: 0x04001187 RID: 4487
	private GameObject nowChatObj;

	// Token: 0x04001188 RID: 4488
	[HideInInspector]
	public UnityAction onChatExit;
}
