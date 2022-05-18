using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace WXB
{
	// Token: 0x020009DC RID: 2524
	[RequireComponent(typeof(SymbolText))]
	public class SymbolTextEvent : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06004050 RID: 16464 RVA: 0x0002E364 File Offset: 0x0002C564
		private void OnEnable()
		{
			if (this.d_symbolText == null)
			{
				this.d_symbolText = base.GetComponent<SymbolText>();
			}
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x0002E380 File Offset: 0x0002C580
		private void OnDisable()
		{
			this.isEnter = false;
			this.d_baseData = null;
			this.d_down_basedata = null;
			this.localPosition = Vector2.zero;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0002E3A2 File Offset: 0x0002C5A2
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isEnter = true;
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0002E3AB File Offset: 0x0002C5AB
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isEnter = false;
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0002E3B4 File Offset: 0x0002C5B4
		public void OnPointerDown(PointerEventData eventData)
		{
			this.d_down_basedata = this.d_baseData;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0002E3C2 File Offset: 0x0002C5C2
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

		// Token: 0x06004056 RID: 16470 RVA: 0x001BC5F8 File Offset: 0x001BA7F8
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

		// Token: 0x0400395B RID: 14683
		private SymbolText d_symbolText;

		// Token: 0x0400395C RID: 14684
		private RenderCache.BaseData d_baseData;

		// Token: 0x0400395D RID: 14685
		public SymbolTextEvent.OnClickEvent OnClick = new SymbolTextEvent.OnClickEvent();

		// Token: 0x0400395E RID: 14686
		private bool isEnter;

		// Token: 0x0400395F RID: 14687
		private RenderCache.BaseData d_down_basedata;

		// Token: 0x04003960 RID: 14688
		private Vector2 localPosition;

		// Token: 0x020009DD RID: 2525
		[Serializable]
		public class OnClickEvent : UnityEvent<NodeBase>
		{
		}
	}
}
