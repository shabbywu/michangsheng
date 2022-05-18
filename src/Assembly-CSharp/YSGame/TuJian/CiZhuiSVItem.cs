using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DE0 RID: 3552
	public class CiZhuiSVItem : MonoBehaviour
	{
		// Token: 0x060055A7 RID: 21927 RVA: 0x0003D477 File Offset: 0x0003B677
		private void Awake()
		{
			this._RT = (base.transform as RectTransform);
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x0003D48A File Offset: 0x0003B68A
		private void Update()
		{
			this.RefreshHeight();
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x0003D492 File Offset: 0x0003B692
		public void SetCiZhui(int id, string name, string desc)
		{
			this.ID = id;
			this._TitleText.text = name;
			this._HyText.text = desc;
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x0023A508 File Offset: 0x00238708
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

		// Token: 0x0400555E RID: 21854
		[HideInInspector]
		public int ID;

		// Token: 0x0400555F RID: 21855
		[HideInInspector]
		public float Height;

		// Token: 0x04005560 RID: 21856
		public Text _TitleText;

		// Token: 0x04005561 RID: 21857
		public SymbolText _HyText;

		// Token: 0x04005562 RID: 21858
		private RectTransform _RT;
	}
}
