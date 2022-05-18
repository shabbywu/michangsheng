using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003BE RID: 958
public class ChuanYingSystemManager : MonoBehaviour
{
	// Token: 0x06001A7D RID: 6781 RVA: 0x000EA368 File Offset: 0x000E8568
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

	// Token: 0x06001A7E RID: 6782 RVA: 0x000EA414 File Offset: 0x000E8614
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

	// Token: 0x06001A7F RID: 6783 RVA: 0x000168E5 File Offset: 0x00014AE5
	public void Close()
	{
		ChuanYingSystemManager.inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 0);
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x000EA4E8 File Offset: 0x000E86E8
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

	// Token: 0x06001A81 RID: 6785 RVA: 0x000EA548 File Offset: 0x000E8748
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

	// Token: 0x06001A82 RID: 6786 RVA: 0x000168F9 File Offset: 0x00014AF9
	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		ChuanYingSystemManager.inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 1);
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x00016913 File Offset: 0x00014B13
	public void checkHasRead()
	{
		if (Tools.instance.getPlayer().NewChuanYingList.Count < 1)
		{
			UIHeadPanel.Inst.ChuanYinFuPoint.SetActive(false);
		}
	}

	// Token: 0x040015EC RID: 5612
	[SerializeField]
	private Canvas ChuanYingCanvas;

	// Token: 0x040015ED RID: 5613
	public static ChuanYingSystemManager inst;

	// Token: 0x040015EE RID: 5614
	[HideInInspector]
	public Inventory2 inventory;

	// Token: 0x040015EF RID: 5615
	[SerializeField]
	private Button CloseBtn;

	// Token: 0x040015F0 RID: 5616
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x040015F1 RID: 5617
	[HideInInspector]
	public ChuanYingMessage curSelectChuanYingMessage;

	// Token: 0x040015F2 RID: 5618
	[SerializeField]
	private GameObject ChuanYingMessageCell;

	// Token: 0x040015F3 RID: 5619
	public RectTransform rectTransform;
}
