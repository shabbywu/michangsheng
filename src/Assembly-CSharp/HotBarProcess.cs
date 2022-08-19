using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000132 RID: 306
public class HotBarProcess : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E37 RID: 3639 RVA: 0x000547AC File Offset: 0x000529AC
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		if (gameObject.transform.Find("Panel - Inventory(Clone)") != null)
		{
			this.inventory = gameObject.transform.Find("Panel - Inventory(Clone)").GetComponent<Inventory>();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x00054804 File Offset: 0x00052A04
	private void Awake()
	{
		HotBarProcess._instance = this;
		this.image_icon = base.transform.GetChild(0).GetComponent<Image>();
		this.text = base.transform.GetChild(1).GetComponent<Text>();
		this.image_cool = base.transform.GetChild(2).GetComponent<Image>();
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x0005485C File Offset: 0x00052A5C
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

	// Token: 0x06000E3A RID: 3642 RVA: 0x00054995 File Offset: 0x00052B95
	public void upItem(int itemId)
	{
		this.itemId = itemId;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x000549AC File Offset: 0x00052BAC
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

	// Token: 0x04000A40 RID: 2624
	public static HotBarProcess _instance;

	// Token: 0x04000A41 RID: 2625
	private Inventory inventory;

	// Token: 0x04000A42 RID: 2626
	private int itemId = -1;

	// Token: 0x04000A43 RID: 2627
	private Text text;

	// Token: 0x04000A44 RID: 2628
	private Image image_icon;

	// Token: 0x04000A45 RID: 2629
	private Image image_cool;
}
