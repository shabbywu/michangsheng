using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006EC RID: 1772
	public abstract class ITabTips : UIBase
	{
		// Token: 0x06003909 RID: 14601 RVA: 0x00185664 File Offset: 0x00183864
		public void Show(string msg, Vector3 position)
		{
			this._text.text = this.Replace(msg);
			this._go.SetActive(true);
			this.UpdateSize();
			this._go.transform.position = new Vector2(position.x, position.y);
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x001856BB File Offset: 0x001838BB
		public void Show(string msg)
		{
			this._text.text = this.Replace(msg);
			this._go.SetActive(true);
			this.UpdateSize();
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00185653 File Offset: 0x00183853
		public void Hide()
		{
			this._go.SetActive(false);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x001856E4 File Offset: 0x001838E4
		protected void UpdateSize()
		{
			if (this._childSizeFitter != null)
			{
				this._childSizeFitter.SetLayoutVertical();
			}
			if (this._sizeFitter != null)
			{
				this._sizeFitter.SetLayoutVertical();
			}
			if (this._rect != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(this._rect);
			}
		}

		// Token: 0x0600390D RID: 14605
		protected abstract string Replace(string msg);

		// Token: 0x04003114 RID: 12564
		protected RectTransform _rect;

		// Token: 0x04003115 RID: 12565
		protected ContentSizeFitter _childSizeFitter;

		// Token: 0x04003116 RID: 12566
		protected ContentSizeFitter _sizeFitter;

		// Token: 0x04003117 RID: 12567
		protected Text _text;
	}
}
