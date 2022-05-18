using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A2D RID: 2605
	public abstract class ITabTips : UIBase
	{
		// Token: 0x06004374 RID: 17268 RVA: 0x001CD160 File Offset: 0x001CB360
		public void Show(string msg, Vector3 position)
		{
			this._text.text = this.Replace(msg);
			this._go.SetActive(true);
			this.UpdateSize();
			this._go.transform.position = new Vector2(position.x, position.y);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x0003036F File Offset: 0x0002E56F
		public void Show(string msg)
		{
			this._text.text = this.Replace(msg);
			this._go.SetActive(true);
			this.UpdateSize();
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x00030361 File Offset: 0x0002E561
		public void Hide()
		{
			this._go.SetActive(false);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001CD1B8 File Offset: 0x001CB3B8
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

		// Token: 0x06004378 RID: 17272
		protected abstract string Replace(string msg);

		// Token: 0x04003B79 RID: 15225
		protected RectTransform _rect;

		// Token: 0x04003B7A RID: 15226
		protected ContentSizeFitter _childSizeFitter;

		// Token: 0x04003B7B RID: 15227
		protected ContentSizeFitter _sizeFitter;

		// Token: 0x04003B7C RID: 15228
		protected Text _text;
	}
}
