using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag.Filter
{
	// Token: 0x02000D4D RID: 3405
	public class LianDanFilter : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x060050EE RID: 20718 RVA: 0x0021B8AC File Offset: 0x00219AAC
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

		// Token: 0x060050EF RID: 20719 RVA: 0x0003A417 File Offset: 0x00038617
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

		// Token: 0x060050F0 RID: 20720 RVA: 0x0003A433 File Offset: 0x00038633
		public void SetState(bool selected)
		{
			this.IsSelect = selected;
			this.Select.SetActive(selected);
			this.UnSelect.SetActive(!selected);
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x000042DD File Offset: 0x000024DD
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0003A457 File Offset: 0x00038657
		public void OnPointerUp(PointerEventData eventData)
		{
			this.Click();
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0003A45F File Offset: 0x0003865F
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.IsSelect)
			{
				this.Select.SetActive(true);
				this.UnSelect.SetActive(false);
			}
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0003A481 File Offset: 0x00038681
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.IsSelect)
			{
				this.Select.SetActive(false);
				this.UnSelect.SetActive(true);
			}
		}

		// Token: 0x0400520D RID: 21005
		public bool IsSelect;

		// Token: 0x0400520E RID: 21006
		public int Value;

		// Token: 0x0400520F RID: 21007
		public GameObject Select;

		// Token: 0x04005210 RID: 21008
		public GameObject UnSelect;

		// Token: 0x04005211 RID: 21009
		public Text Content1;

		// Token: 0x04005212 RID: 21010
		public Text Content2;

		// Token: 0x04005213 RID: 21011
		public UnityAction<LianDanFilter> SelectCall;
	}
}
