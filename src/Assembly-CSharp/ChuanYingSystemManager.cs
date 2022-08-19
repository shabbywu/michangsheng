using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000290 RID: 656
public class ChuanYingSystemManager : MonoBehaviour
{
	// Token: 0x060017A2 RID: 6050 RVA: 0x000A3168 File Offset: 0x000A1368
	private void Awake()
	{
		ChuanYingSystemManager.inst = this;
		this.inventory = UI_Manager.inst.inventory2;
		this.ChuanYingCanvas.worldCamera = UI_Manager.inst.RootCamera;
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		base.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		base.transform.localPosition = Vector3.zero;
		Tools.canClickFlag = false;
		this.CloseBtn.onClick.AddListener(new UnityAction(this.Close));
		this.init();
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000A3214 File Offset: 0x000A1414
	public void init()
	{
		Tools.ClearObj(this.ChuanYingMessageCell.transform);
		Avatar player = Tools.instance.getPlayer();
		JSONObject hasReadChuanYingList = player.HasReadChuanYingList;
		JSONObject newChuanYingList = player.NewChuanYingList;
		for (int i = 0; i < newChuanYingList.Count; i++)
		{
			ChuanYingMessage component = Tools.InstantiateGameObject(this.ChuanYingMessageCell, this.ChuanYingMessageCell.transform.parent).GetComponent<ChuanYingMessage>();
			component.isRead = false;
			component.ChuanYingData = newChuanYingList[i];
			component.init();
		}
		if (hasReadChuanYingList.Count > 0)
		{
			for (int j = hasReadChuanYingList.Count - 1; j >= 0; j--)
			{
				ChuanYingMessage component2 = Tools.InstantiateGameObject(this.ChuanYingMessageCell, this.ChuanYingMessageCell.transform.parent).GetComponent<ChuanYingMessage>();
				component2.isRead = true;
				component2.ChuanYingData = hasReadChuanYingList[j];
				component2.init();
			}
		}
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000A32E7 File Offset: 0x000A14E7
	public void Close()
	{
		ChuanYingSystemManager.inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 0);
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000A32FC File Offset: 0x000A14FC
	public void deleteMessage(JSONObject json, GameObject obj)
	{
		selectBox.instence.LianDanChoice("确定要删除此传音符吗", new EventDelegate(delegate()
		{
			int i = json["id"].I;
			Tools.instance.getPlayer().HasReadChuanYingList.RemoveField(i.ToString());
			if (this.curSelectChuanYingMessage != null && this.curSelectChuanYingMessage.ChuanYingData["id"].I == i)
			{
				this.curSelectChuanYingMessage = null;
			}
			Object.Destroy(obj);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		}), null, new Vector3(0.8f, 0.8f, 0.8f));
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x000A335C File Offset: 0x000A155C
	public void clickSelect(bool isShow, ChuanYingMessage chuanYingMessage)
	{
		if (isShow)
		{
			if (this.curSelectChuanYingMessage != null)
			{
				this.curSelectChuanYingMessage.isShow = false;
				this.curSelectChuanYingMessage.content.SetActive(false);
				this.curSelectChuanYingMessage.updateSelfHeight();
			}
			this.curSelectChuanYingMessage = chuanYingMessage;
			return;
		}
		this.curSelectChuanYingMessage = null;
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000A33B1 File Offset: 0x000A15B1
	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		ChuanYingSystemManager.inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 1);
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000A33CB File Offset: 0x000A15CB
	public void checkHasRead()
	{
		if (Tools.instance.getPlayer().NewChuanYingList.Count < 1)
		{
			UIHeadPanel.Inst.ChuanYinFuPoint.SetActive(false);
		}
	}

	// Token: 0x04001269 RID: 4713
	[SerializeField]
	private Canvas ChuanYingCanvas;

	// Token: 0x0400126A RID: 4714
	public static ChuanYingSystemManager inst;

	// Token: 0x0400126B RID: 4715
	[HideInInspector]
	public Inventory2 inventory;

	// Token: 0x0400126C RID: 4716
	[SerializeField]
	private Button CloseBtn;

	// Token: 0x0400126D RID: 4717
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x0400126E RID: 4718
	[HideInInspector]
	public ChuanYingMessage curSelectChuanYingMessage;

	// Token: 0x0400126F RID: 4719
	[SerializeField]
	private GameObject ChuanYingMessageCell;

	// Token: 0x04001270 RID: 4720
	public RectTransform rectTransform;
}
