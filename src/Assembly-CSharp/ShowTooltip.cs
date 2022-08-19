using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000147 RID: 327
public class ShowTooltip : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000EB8 RID: 3768 RVA: 0x00059BF0 File Offset: 0x00057DF0
	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
		if (gameObject.transform.Find("Tooltip - Inventory(Clone)") != null)
		{
			this.tooltip = gameObject.transform.Find("Tooltip - Inventory(Clone)").GetComponent<Tooltip>();
			this.tooltipGameObject = gameObject.transform.Find("Tooltip - Inventory(Clone)").gameObject;
			this.tooltipRectTransform = this.tooltipGameObject.GetComponent<RectTransform>();
		}
		this.canvasRectTransform = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x00059C7C File Offset: 0x00057E7C
	public void OnPointerDown(PointerEventData data)
	{
		if (this.tooltip != null)
		{
			this.tooltip.tooltipType = TooltipType.Inventory;
			Transform transform = base.transform;
			while (transform.parent)
			{
				if (transform.tag == "MainInventory")
				{
					this.tooltip.tooltipType = TooltipType.Inventory;
					break;
				}
				if (transform.tag == "EquipmentSystem")
				{
					this.tooltip.tooltipType = TooltipType.Equipment;
					break;
				}
				transform = transform.parent;
			}
			this.item = base.GetComponent<ItemOnObject>().item;
			this.tooltip.item = this.item;
			this.tooltip.activateTooltip();
			if (this.canvasRectTransform == null)
			{
				return;
			}
			Vector3[] array = new Vector3[4];
			base.GetComponent<RectTransform>().GetWorldCorners(array);
			Vector2 vector;
			Vector2 vector2;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, array[3], data.pressEventCamera, ref vector) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasRectTransform, array[1], data.pressEventCamera, ref vector2))
			{
				float num = vector.x;
				float num2 = vector2.y;
				if (num + this.tooltipRectTransform.rect.width > this.canvasRectTransform.rect.xMax)
				{
					num = vector2.x - this.tooltipRectTransform.rect.width;
				}
				if (num2 - this.tooltipRectTransform.rect.height < this.canvasRectTransform.rect.yMin)
				{
					num2 += this.canvasRectTransform.rect.yMin - num2 + this.tooltipRectTransform.rect.height;
				}
				this.tooltipRectTransform.localPosition = new Vector3(num, num2);
			}
		}
	}

	// Token: 0x04000AE8 RID: 2792
	public Tooltip tooltip;

	// Token: 0x04000AE9 RID: 2793
	public GameObject tooltipGameObject;

	// Token: 0x04000AEA RID: 2794
	public RectTransform canvasRectTransform;

	// Token: 0x04000AEB RID: 2795
	public RectTransform tooltipRectTransform;

	// Token: 0x04000AEC RID: 2796
	private Item item;
}
