using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200043E RID: 1086
public class LianDanSystemManager : MonoBehaviour
{
	// Token: 0x06001CE9 RID: 7401 RVA: 0x00018200 File Offset: 0x00016400
	private void Awake()
	{
		LianDanSystemManager.inst = this;
		this.FixPostion();
		this.init();
		this.initInventory();
		this.DanLuSprite = ResManager.inst.LoadSprites("NewUI/LianDan/DanLu/DanLu");
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x000FF250 File Offset: 0x000FD450
	private void init()
	{
		this.LianDanCanvas.worldCamera = UI_Manager.inst.RootCamera;
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		base.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		base.transform.localPosition = Vector3.zero;
		this.DanFangPageManager.init();
		this.closeButton.onClick.AddListener(new UnityAction(this.close));
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x000FF2E4 File Offset: 0x000FD4E4
	public void FixPostion()
	{
		this.uIWidget.enabled = true;
		this.uIWidget.SetAnchor(UI_Manager.inst.gameObject);
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		this.uIWidget.rightAnchor.absolute = -150;
		this.uIWidget.topAnchor.relative = 0f;
		this.uIWidget.bottomAnchor.relative = 0f;
		this.uIWidget.topAnchor.absolute = 0;
		this.uIWidget.bottomAnchor.absolute = 200;
		base.Invoke("lateAction", 1f);
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x0001822F File Offset: 0x0001642F
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0001823D File Offset: 0x0001643D
	public void close()
	{
		if (!this.BgMask.activeSelf)
		{
			LianDanSystemManager.inst = null;
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 0);
		}
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x000FF394 File Offset: 0x000FD594
	public void lianDanFinshCallBack()
	{
		this.DanFangPageManager.updateState();
		this.lianDanPageManager.updateDanLu();
		Avatar player = Tools.instance.getPlayer();
		for (int i = 0; i <= 4; i++)
		{
			int itemID = this.lianDanPageManager.putLianDanCellList[i].Item.itemID;
			int itemNum = this.lianDanPageManager.putLianDanCellList[i].Item.itemNum;
			bool flag = false;
			foreach (ITEM_INFO item_INFO in player.itemList.values)
			{
				if (item_INFO.itemId == itemID)
				{
					flag = true;
					if ((ulong)item_INFO.itemCount < (ulong)((long)itemNum))
					{
						this.inventory.inventory[i + 25] = new item();
						this.inventory.inventory[i + 25].itemNum = 1;
						this.lianDanPageManager.putLianDanCellList[i].updateItem();
						break;
					}
				}
			}
			if (!flag)
			{
				this.inventory.inventory[i + 25] = new item();
				this.inventory.inventory[i + 25].itemNum = 1;
				this.lianDanPageManager.putLianDanCellList[i].updateItem();
			}
		}
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x000FF508 File Offset: 0x000FD708
	public void putLianDanCaiLiaoCallBack()
	{
		item item = this.lianDanPageManager.putLianDanCellList[5].Item;
		if (item.itemID == -1)
		{
			this.lianDanPageManager.putLianDanCellList[2].isLock = true;
			this.lianDanPageManager.putLianDanCellList[4].isLock = true;
			this.startLianDanBtn.enabled = false;
			this.startLianDanBtn.interactable = false;
			this.startLianDanImage.sprite = this.lianDanBtnWordSprite[2];
			this.startLianDanWordImage.sprite = this.lianDanBtnWordSprite[3];
			return;
		}
		if (item.quality >= 6)
		{
			this.lianDanPageManager.putLianDanCellList[2].isLock = false;
			this.lianDanPageManager.putLianDanCellList[4].isLock = false;
		}
		else
		{
			this.lianDanPageManager.putLianDanCellList[2].isLock = true;
			if (item.quality >= 2)
			{
				this.lianDanPageManager.putLianDanCellList[4].isLock = false;
			}
			else
			{
				this.lianDanPageManager.putLianDanCellList[4].isLock = true;
			}
		}
		bool flag = false;
		for (int i = 0; i < 5; i++)
		{
			if (this.lianDanPageManager.putLianDanCellList[i].Item.itemID > 0)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.startLianDanImage.sprite = this.lianDanBtnWordSprite[0];
			this.startLianDanWordImage.sprite = this.lianDanBtnWordSprite[1];
			this.startLianDanBtn.enabled = true;
			this.startLianDanBtn.interactable = true;
			return;
		}
		this.startLianDanBtn.enabled = false;
		this.startLianDanBtn.interactable = false;
		this.startLianDanImage.sprite = this.lianDanBtnWordSprite[2];
		this.startLianDanWordImage.sprite = this.lianDanBtnWordSprite[3];
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x000FF6F8 File Offset: 0x000FD8F8
	public void putDanLuCallBack()
	{
		if (this.DanFangPageManager.curSelectDanFangBg != null)
		{
			this.lianDanPageManager.RemoveAll();
			this.DanFangPageManager.curSelectDanFangBg.SetActive(false);
			LianDanSystemManager.inst.DanFangPageManager.curSelectDanFang = null;
			LianDanSystemManager.inst.DanFangPageManager.curSelectDanFanParent = null;
			LianDanSystemManager.inst.DanFangPageManager.curSelectJSONObject = null;
			this.DanFangPageManager.curSelectDanFangBg = null;
		}
		this.lianDanPageManager.updateDanLu();
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x0001825E File Offset: 0x0001645E
	private void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 1);
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x000FF77C File Offset: 0x000FD97C
	private void initInventory()
	{
		this.inventory.inventory = new List<item>();
		for (int i = 0; i < (int)this.inventory.count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0001826C File Offset: 0x0001646C
	public void openMask()
	{
		this.BgMask.SetActive(true);
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0001827A File Offset: 0x0001647A
	public void closeMask()
	{
		this.BgMask.SetActive(false);
	}

	// Token: 0x040018DC RID: 6364
	[SerializeField]
	private Canvas LianDanCanvas;

	// Token: 0x040018DD RID: 6365
	public DanFangPageManager DanFangPageManager;

	// Token: 0x040018DE RID: 6366
	public SelectLianDanCaiLiaoPage selectLianDanCaiLiaoPage;

	// Token: 0x040018DF RID: 6367
	public PutDanLuManager putDanLuManager;

	// Token: 0x040018E0 RID: 6368
	public LianDanPageManager lianDanPageManager;

	// Token: 0x040018E1 RID: 6369
	public LianDanResultManager lianDanResultManager;

	// Token: 0x040018E2 RID: 6370
	public static LianDanSystemManager inst;

	// Token: 0x040018E3 RID: 6371
	[SerializeField]
	private Button closeButton;

	// Token: 0x040018E4 RID: 6372
	[SerializeField]
	private GameObject BgMask;

	// Token: 0x040018E5 RID: 6373
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x040018E6 RID: 6374
	public LianDanInventory inventory;

	// Token: 0x040018E7 RID: 6375
	public Transform sumSelectTransform;

	// Token: 0x040018E8 RID: 6376
	public Sprite LockSprite;

	// Token: 0x040018E9 RID: 6377
	public Sprite NoLockSprite;

	// Token: 0x040018EA RID: 6378
	public List<Sprite> lianDanBtnWordSprite;

	// Token: 0x040018EB RID: 6379
	public Button startLianDanBtn;

	// Token: 0x040018EC RID: 6380
	public Image startLianDanImage;

	// Token: 0x040018ED RID: 6381
	public Image startLianDanWordImage;

	// Token: 0x040018EE RID: 6382
	[HideInInspector]
	public Object[] DanLuSprite;

	// Token: 0x0200043F RID: 1087
	public enum PutCellType
	{
		// Token: 0x040018F0 RID: 6384
		药引 = 25,
		// Token: 0x040018F1 RID: 6385
		主药1,
		// Token: 0x040018F2 RID: 6386
		主药2,
		// Token: 0x040018F3 RID: 6387
		辅药1,
		// Token: 0x040018F4 RID: 6388
		辅药2,
		// Token: 0x040018F5 RID: 6389
		丹炉
	}
}
