using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000AA3 RID: 2723
	public class CiZhuiSVItem : MonoBehaviour
	{
		// Token: 0x06004C5A RID: 19546 RVA: 0x002092E0 File Offset: 0x002074E0
		private void Awake()
		{
			this._RT = (base.transform as RectTransform);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x002092F3 File Offset: 0x002074F3
		private void Update()
		{
			this.RefreshHeight();
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x002092FB File Offset: 0x002074FB
		public void SetCiZhui(int id, string name, string desc)
		{
			this.ID = id;
			this._TitleText.text = name;
			this._HyText.text = desc;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0020931C File Offset: 0x0020751C
		public void RefreshHeight()
		{
			if (this._RT.sizeDelta.y != this._HyText.preferredHeight)
			{
				if (this._HyText.preferredHeight < 40f)
				{
					if (this._RT.sizeDelta.y != 40f)
					{
						this._RT.sizeDelta = new Vector2(this._RT.sizeDelta.x, 40f);
						this._HyText.rectTransform.sizeDelta = new Vector2(this._HyText.rectTransform.sizeDelta.x, 40f);
					}
				}
				else
				{
					this._RT.sizeDelta = new Vector2(this._RT.sizeDelta.x, this._HyText.preferredHeight);
					this._HyText.rectTransform.sizeDelta = new Vector2(this._HyText.rectTransform.sizeDelta.x, this._HyText.preferredHeight);
				}
			}
			this.Height = this._RT.sizeDelta.y + 10f;
		}

		// Token: 0x04004B80 RID: 19328
		[HideInInspector]
		public int ID;

		// Token: 0x04004B81 RID: 19329
		[HideInInspector]
		public float Height;

		// Token: 0x04004B82 RID: 19330
		public Text _TitleText;

		// Token: 0x04004B83 RID: 19331
		public SymbolText _HyText;

		// Token: 0x04004B84 RID: 19332
		private RectTransform _RT;
	}
}
