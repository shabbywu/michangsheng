using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000203 RID: 515
public class HotBarProcess : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06001045 RID: 4165 RVA: 0x000A4DE4 File Offset: 0x000A2FE4
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		if (gameObject.transform.Find("Panel - Inventory(Clone)") != null)
		{
			this.inventory = gameObject.transform.Find("Panel - Inventory(Clone)").GetComponent<Inventory>();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000A4E3C File Offset: 0x000A303C
	private void Awake()
	{
		HotBarProcess._instance = this;
		this.image_icon = base.transform.GetChild(0).GetComponent<Image>();
		this.text = base.transform.GetChild(1).GetComponent<Text>();
		this.image_cool = base.transform.GetChild(2).GetComponent<Image>();
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x000A4E94 File Offset: 0x000A3094
	private void Update()
	{
		if (this.itemId == -1)
		{
			return;
		}
		if (this.inventory == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
			this.inventory = gameObject.transform.Find("Panel - Inventory(Clone)").GetComponent<Inventory>();
		}
		if (this.inventory == null)
		{
			return;
		}
		GameObject itemGameObject = this.inventory.getItemGameObject(this.itemId);
		if (itemGameObject == null)
		{
			this.itemId = -1;
			base.gameObject.SetActive(false);
			return;
		}
		this.image_icon.sprite = itemGameObject.GetComponent<ItemOnObject>().item.itemIcon;
		this.text.rectTransform.localPosition = itemGameObject.transform.GetChild(1).GetComponent<Text>().transform.localPosition;
		this.text.enabled = true;
		this.text.text = itemGameObject.transform.GetChild(1).GetComponent<Text>().text;
		if (ConsumeLimitCD.instance.isWaiting())
		{
			this.image_cool.fillAmount = ConsumeLimitCD.instance.restTime / ConsumeLimitCD.instance.totalTime;
			return;
		}
		this.image_cool.fillAmount = 0f;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x00010454 File Offset: 0x0000E654
	public void upItem(int itemId)
	{
		this.itemId = itemId;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000A4FD0 File Offset: 0x000A31D0
	public void OnPointerDown(PointerEventData data)
	{
		GameObject itemGameObject = this.inventory.getItemGameObject(this.itemId);
		if (itemGameObject != null)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar != null)
			{
				avatar.useItemRequest((ulong)((long)itemGameObject.GetComponent<ItemOnObject>().item.itemIndex));
			}
		}
	}

	// Token: 0x04000CD8 RID: 3288
	public static HotBarProcess _instance;

	// Token: 0x04000CD9 RID: 3289
	private Inventory inventory;

	// Token: 0x04000CDA RID: 3290
	private int itemId = -1;

	// Token: 0x04000CDB RID: 3291
	private Text text;

	// Token: 0x04000CDC RID: 3292
	private Image image_icon;

	// Token: 0x04000CDD RID: 3293
	private Image image_cool;
}
