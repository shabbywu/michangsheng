using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag.Filter
{
	// Token: 0x020009C4 RID: 2500
	public class LianDanFilter : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x06004589 RID: 17801 RVA: 0x001D7A4C File Offset: 0x001D5C4C
		public void Init(int value, string content, UnityAction<LianDanFilter> action, float x, float y)
		{
			this.Value = value;
			base.gameObject.name = content;
			base.transform.localPosition = new Vector2(x, y);
			this.Content1.SetText(content);
			this.Content2.SetText(content);
			this.SelectCall = action;
			base.gameObject.SetActive(true);
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x001D7AB0 File Offset: 0x001D5CB0
		public void Click()
		{
			if (this.IsSelect)
			{
				return;
			}
			UnityAction<LianDanFilter> selectCall = this.SelectCall;
			if (selectCall == null)
			{
				return;
			}
			selectCall.Invoke(this);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x001D7ACC File Offset: 0x001D5CCC
		public void SetState(bool selected)
		{
			this.IsSelect = selected;
			this.Select.SetActive(selected);
			this.UnSelect.SetActive(!selected);
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00004095 File Offset: 0x00002295
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x001D7AF0 File Offset: 0x001D5CF0
		public void OnPointerUp(PointerEventData eventData)
		{
			this.Click();
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x001D7AF8 File Offset: 0x001D5CF8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.IsSelect)
			{
				this.Select.SetActive(true);
				this.UnSelect.SetActive(false);
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x001D7B1A File Offset: 0x001D5D1A
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.IsSelect)
			{
				this.Select.SetActive(false);
				this.UnSelect.SetActive(true);
			}
		}

		// Token: 0x04004709 RID: 18185
		public bool IsSelect;

		// Token: 0x0400470A RID: 18186
		public int Value;

		// Token: 0x0400470B RID: 18187
		public GameObject Select;

		// Token: 0x0400470C RID: 18188
		public GameObject UnSelect;

		// Token: 0x0400470D RID: 18189
		public Text Content1;

		// Token: 0x0400470E RID: 18190
		public Text Content2;

		// Token: 0x0400470F RID: 18191
		public UnityAction<LianDanFilter> SelectCall;
	}
}
