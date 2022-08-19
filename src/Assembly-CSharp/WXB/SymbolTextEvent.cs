using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WXB
{
	// Token: 0x020006B6 RID: 1718
	[RequireComponent(typeof(SymbolText))]
	public class SymbolTextEvent : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06003649 RID: 13897 RVA: 0x00173CDD File Offset: 0x00171EDD
		private void OnEnable()
		{
			if (this.d_symbolText == null)
			{
				this.d_symbolText = base.GetComponent<SymbolText>();
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00173CF9 File Offset: 0x00171EF9
		private void OnDisable()
		{
			this.isEnter = false;
			this.d_baseData = null;
			this.d_down_basedata = null;
			this.localPosition = Vector2.zero;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x00173D1B File Offset: 0x00171F1B
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isEnter = true;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x00173D24 File Offset: 0x00171F24
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isEnter = false;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x00173D2D File Offset: 0x00171F2D
		public void OnPointerDown(PointerEventData eventData)
		{
			this.d_down_basedata = this.d_baseData;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x00173D3B File Offset: 0x00171F3B
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.d_down_basedata != this.d_baseData)
			{
				return;
			}
			if (this.d_down_basedata != null)
			{
				this.OnClick.Invoke(this.d_down_basedata.node);
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x00173D6C File Offset: 0x00171F6C
		private void Update()
		{
			if (this.isEnter)
			{
				if (this.d_symbolText.canvas != null)
				{
					Tools.ScreenPointToWorldPointInRectangle(this.d_symbolText.rectTransform, Input.mousePosition, this.d_symbolText.canvas.worldCamera, out this.localPosition);
				}
				RenderCache.BaseData baseData = this.d_symbolText.renderCache.Get(this.localPosition);
				if (this.d_baseData != baseData)
				{
					if (this.d_baseData != null)
					{
						this.d_baseData.OnMouseLevel();
					}
					this.d_baseData = baseData;
					if (this.d_baseData != null)
					{
						this.d_baseData.OnMouseEnter();
						return;
					}
				}
			}
			else if (this.d_baseData != null)
			{
				this.d_baseData.OnMouseLevel();
				this.d_baseData = null;
			}
		}

		// Token: 0x04002F69 RID: 12137
		private SymbolText d_symbolText;

		// Token: 0x04002F6A RID: 12138
		private RenderCache.BaseData d_baseData;

		// Token: 0x04002F6B RID: 12139
		public SymbolTextEvent.OnClickEvent OnClick = new SymbolTextEvent.OnClickEvent();

		// Token: 0x04002F6C RID: 12140
		private bool isEnter;

		// Token: 0x04002F6D RID: 12141
		private RenderCache.BaseData d_down_basedata;

		// Token: 0x04002F6E RID: 12142
		private Vector2 localPosition;

		// Token: 0x0200150C RID: 5388
		[Serializable]
		public class OnClickEvent : UnityEvent<NodeBase>
		{
		}
	}
}
