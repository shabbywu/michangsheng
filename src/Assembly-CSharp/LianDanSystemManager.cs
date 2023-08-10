using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LianDanSystemManager : MonoBehaviour
{
	public enum PutCellType
	{
		药引 = 25,
		主药1,
		主药2,
		辅药1,
		辅药2,
		丹炉
	}

	[SerializeField]
	private Canvas LianDanCanvas;

	public DanFangPageManager DanFangPageManager;

	public SelectLianDanCaiLiaoPage selectLianDanCaiLiaoPage;

	public PutDanLuManager putDanLuManager;

	public LianDanPageManager lianDanPageManager;

	public LianDanResultManager lianDanResultManager;

	public static LianDanSystemManager inst;

	[SerializeField]
	private Button closeButton;

	[SerializeField]
	private GameObject BgMask;

	[SerializeField]
	private UIWidget uIWidget;

	public LianDanInventory inventory;

	public Transform sumSelectTransform;

	public Sprite LockSprite;

	public Sprite NoLockSprite;

	public List<Sprite> lianDanBtnWordSprite;

	public Button startLianDanBtn;

	public Image startLianDanImage;

	public Image startLianDanWordImage;

	[HideInInspector]
	public Object[] DanLuSprite;

	private void Awake()
	{
		inst = this;
		FixPostion();
		init();
		initInventory();
		DanLuSprite = ResManager.inst.LoadSprites("NewUI/LianDan/DanLu/DanLu");
	}

	private void init()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		LianDanCanvas.worldCamera = UI_Manager.inst.RootCamera;
		((Component)this).transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		((Component)this).transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		((Component)this).transform.localPosition = Vector3.zero;
		DanFangPageManager.init();
		((UnityEvent)closeButton.onClick).AddListener(new UnityAction(close));
	}

	public void FixPostion()
	{
		((Behaviour)uIWidget).enabled = true;
		uIWidget.SetAnchor(((Component)UI_Manager.inst).gameObject);
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		uIWidget.rightAnchor.absolute = -150;
		uIWidget.topAnchor.relative = 0f;
		uIWidget.bottomAnchor.relative = 0f;
		uIWidget.topAnchor.absolute = 0;
		uIWidget.bottomAnchor.absolute = 200;
		((MonoBehaviour)this).Invoke("lateAction", 1f);
	}

	public void lateAction()
	{
		uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	public void close()
	{
		if (!BgMask.activeSelf)
		{
			inst = null;
			PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹);
		}
	}

	public void lianDanFinshCallBack()
	{
		DanFangPageManager.updateState();
		lianDanPageManager.updateDanLu();
		Avatar player = Tools.instance.getPlayer();
		for (int i = 0; i <= 4; i++)
		{
			int itemID = lianDanPageManager.putLianDanCellList[i].Item.itemID;
			int itemNum = lianDanPageManager.putLianDanCellList[i].Item.itemNum;
			bool flag = false;
			foreach (ITEM_INFO value in player.itemList.values)
			{
				if (value.itemId == itemID)
				{
					flag = true;
					if (value.itemCount < itemNum)
					{
						inventory.inventory[i + 25] = new item();
						inventory.inventory[i + 25].itemNum = 1;
						lianDanPageManager.putLianDanCellList[i].updateItem();
						break;
					}
				}
			}
			if (!flag)
			{
				inventory.inventory[i + 25] = new item();
				inventory.inventory[i + 25].itemNum = 1;
				lianDanPageManager.putLianDanCellList[i].updateItem();
			}
		}
	}

	public void putLianDanCaiLiaoCallBack()
	{
		item item = lianDanPageManager.putLianDanCellList[5].Item;
		if (item.itemID != -1)
		{
			if (item.quality >= 6)
			{
				lianDanPageManager.putLianDanCellList[2].isLock = false;
				lianDanPageManager.putLianDanCellList[4].isLock = false;
			}
			else
			{
				lianDanPageManager.putLianDanCellList[2].isLock = true;
				if (item.quality >= 2)
				{
					lianDanPageManager.putLianDanCellList[4].isLock = false;
				}
				else
				{
					lianDanPageManager.putLianDanCellList[4].isLock = true;
				}
			}
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				if (lianDanPageManager.putLianDanCellList[i].Item.itemID > 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				startLianDanImage.sprite = lianDanBtnWordSprite[0];
				startLianDanWordImage.sprite = lianDanBtnWordSprite[1];
				((Behaviour)startLianDanBtn).enabled = true;
				((Selectable)startLianDanBtn).interactable = true;
			}
			else
			{
				((Behaviour)startLianDanBtn).enabled = false;
				((Selectable)startLianDanBtn).interactable = false;
				startLianDanImage.sprite = lianDanBtnWordSprite[2];
				startLianDanWordImage.sprite = lianDanBtnWordSprite[3];
			}
		}
		else
		{
			lianDanPageManager.putLianDanCellList[2].isLock = true;
			lianDanPageManager.putLianDanCellList[4].isLock = true;
			((Behaviour)startLianDanBtn).enabled = false;
			((Selectable)startLianDanBtn).interactable = false;
			startLianDanImage.sprite = lianDanBtnWordSprite[2];
			startLianDanWordImage.sprite = lianDanBtnWordSprite[3];
		}
	}

	public void putDanLuCallBack()
	{
		if ((Object)(object)DanFangPageManager.curSelectDanFangBg != (Object)null)
		{
			lianDanPageManager.RemoveAll();
			DanFangPageManager.curSelectDanFangBg.SetActive(false);
			inst.DanFangPageManager.curSelectDanFang = null;
			inst.DanFangPageManager.curSelectDanFanParent = null;
			inst.DanFangPageManager.curSelectJSONObject = null;
			DanFangPageManager.curSelectDanFangBg = null;
		}
		lianDanPageManager.updateDanLu();
	}

	private void OnDestroy()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼丹, 1);
	}

	private void initInventory()
	{
		inventory.inventory = new List<item>();
		for (int i = 0; i < (int)inventory.count; i++)
		{
			inventory.inventory.Add(new item());
		}
	}

	public void openMask()
	{
		BgMask.SetActive(true);
	}

	public void closeMask()
	{
		BgMask.SetActive(false);
	}
}
