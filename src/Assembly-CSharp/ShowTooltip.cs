using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200021C RID: 540
public class ShowTooltip : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x060010D8 RID: 4312 RVA: 0x000A9F4C File Offset: 0x000A814C
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

	// Token: 0x060010D9 RID: 4313 RVA: 0x000A9FD8 File Offset: 0x000A81D8
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

	// Token: 0x04000D83 RID: 3459
	public Tooltip tooltip;

	// Token: 0x04000D84 RID: 3460
	public GameObject tooltipGameObject;

	// Token: 0x04000D85 RID: 3461
	public RectTransform canvasRectTransform;

	// Token: 0x04000D86 RID: 3462
	public RectTransform tooltipRectTransform;

	// Token: 0x04000D87 RID: 3463
	private Item item;
}
