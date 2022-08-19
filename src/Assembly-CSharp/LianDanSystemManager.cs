using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E5 RID: 741
public class LianDanSystemManager : MonoBehaviour
{
	// Token: 0x060019C7 RID: 6599 RVA: 0x000B8836 File Offset: 0x000B6A36
	private void Awake()
	{
		LianDanSystemManager.inst = this;
		this.FixPostion();
		this.init();
		this.initInventory();
		this.DanLuSprite = ResManager.inst.LoadSprites("NewUI/LianDan/DanLu/DanLu");
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000B8868 File Offset: 0x000B6A68
	private void init()
	{
		this.LianDanCanvas.worldCamera = UI_Manager.inst.RootCamera;
		base.transform.parent = UI_Manager.inst.gameObject.transform;
		base.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		base.transform.localPosition = Vector3.zero;
		this.DanFangPageManager.init();
		this.closeButton.onClick.AddListener(new UnityAction(this.close));
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000B88FC File Offset: 0x000B6AFC
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

	// Token: 0x060019CA RID: 6602 RVA: 0x000B89AB File Offset: 0x000B6BAB
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x000B89B9 File Offset: 0x000B6BB9
	public void close()
	{
		if (!this.BgMask.activeSelf)
		{
			LianDanSystemManager.inst = null;
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 0);
		}
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x000B89DC File Offset: 0x000B6BDC
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

	// Token: 0x060019CD RID: 6605 RVA: 0x000B8B50 File Offset: 0x000B6D50
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

	// Token: 0x060019CE RID: 6606 RVA: 0x000B8D40 File Offset: 0x000B6F40
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

	// Token: 0x060019CF RID: 6607 RVA: 0x000B8DC3 File Offset: 0x000B6FC3
	private void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 1);
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x000B8DD4 File Offset: 0x000B6FD4
	private void initInventory()
	{
		this.inventory.inventory = new List<item>();
		for (int i = 0; i < (int)this.inventory.count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x000B8E1C File Offset: 0x000B701C
	public void openMask()
	{
		this.BgMask.SetActive(true);
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x000B8E2A File Offset: 0x000B702A
	public void closeMask()
	{
		this.BgMask.SetActive(false);
	}

	// Token: 0x040014DF RID: 5343
	[SerializeField]
	private Canvas LianDanCanvas;

	// Token: 0x040014E0 RID: 5344
	public DanFangPageManager DanFangPageManager;

	// Token: 0x040014E1 RID: 5345
	public SelectLianDanCaiLiaoPage selectLianDanCaiLiaoPage;

	// Token: 0x040014E2 RID: 5346
	public PutDanLuManager putDanLuManager;

	// Token: 0x040014E3 RID: 5347
	public LianDanPageManager lianDanPageManager;

	// Token: 0x040014E4 RID: 5348
	public LianDanResultManager lianDanResultManager;

	// Token: 0x040014E5 RID: 5349
	public static LianDanSystemManager inst;

	// Token: 0x040014E6 RID: 5350
	[SerializeField]
	private Button closeButton;

	// Token: 0x040014E7 RID: 5351
	[SerializeField]
	private GameObject BgMask;

	// Token: 0x040014E8 RID: 5352
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x040014E9 RID: 5353
	public LianDanInventory inventory;

	// Token: 0x040014EA RID: 5354
	public Transform sumSelectTransform;

	// Token: 0x040014EB RID: 5355
	public Sprite LockSprite;

	// Token: 0x040014EC RID: 5356
	public Sprite NoLockSprite;

	// Token: 0x040014ED RID: 5357
	public List<Sprite> lianDanBtnWordSprite;

	// Token: 0x040014EE RID: 5358
	public Button startLianDanBtn;

	// Token: 0x040014EF RID: 5359
	public Image startLianDanImage;

	// Token: 0x040014F0 RID: 5360
	public Image startLianDanWordImage;

	// Token: 0x040014F1 RID: 5361
	[HideInInspector]
	public Object[] DanLuSprite;

	// Token: 0x0200132F RID: 4911
	public enum PutCellType
	{
		// Token: 0x040067C0 RID: 26560
		药引 = 25,
		// Token: 0x040067C1 RID: 26561
		主药1,
		// Token: 0x040067C2 RID: 26562
		主药2,
		// Token: 0x040067C3 RID: 26563
		辅药1,
		// Token: 0x040067C4 RID: 26564
		辅药2,
		// Token: 0x040067C5 RID: 26565
		丹炉
	}
}
