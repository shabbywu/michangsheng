using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DEF RID: 3567
	public class TuJianSSVItem : SSVItem
	{
		// Token: 0x0600560E RID: 22030 RVA: 0x0003D903 File Offset: 0x0003BB03
		public override void Start()
		{
			base.Start();
			this._BtnImage = this._button.GetComponent<Image>();
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0023E01C File Offset: 0x0023C21C
		public override void Update()
		{
			base.Update();
			if (this._IsSelected)
			{
				if (this.SSV.NowSelectItemIndex != base.DataIndex)
				{
					this.OnLoseSelect();
					return;
				}
			}
			else if (this.SSV.NowSelectItemIndex == base.DataIndex)
			{
				this.OnSelect();
			}
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x0003D91C File Offset: 0x0003BB1C
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x0003D93A File Offset: 0x0003BB3A
		private void OnDisable()
		{
			this._button.onClick.RemoveAllListeners();
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0003D94C File Offset: 0x0003BB4C
		public void OnClick()
		{
			if (this.SSV.NowSelectItemIndex != base.DataIndex)
			{
				this.SSV.NowSelectItemIndex = base.DataIndex;
				this.OnSelect();
				TuJianManager.Inst.OnButtonClick();
			}
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x0003D982 File Offset: 0x0003BB82
		public void OnSelect()
		{
			this._IsSelected = true;
			this._text.color = this._SelectedTextColor;
			this._BtnImage.color = Color.white;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x0003D9AC File Offset: 0x0003BBAC
		public void OnLoseSelect()
		{
			this._IsSelected = false;
			this._text.color = this._NormalTextColor;
			this._BtnImage.color = this._AlphaColor;
		}

		// Token: 0x040055B2 RID: 21938
		[HideInInspector]
		public bool needRefresh;

		// Token: 0x040055B3 RID: 21939
		private bool _IsSelected;

		// Token: 0x040055B4 RID: 21940
		private Color _SelectedTextColor = new Color(0.46666667f, 0.87058824f, 0.76862746f);

		// Token: 0x040055B5 RID: 21941
		private Color _NormalTextColor = new Color(0.14117648f, 0.34509805f, 0.35686275f);

		// Token: 0x040055B6 RID: 21942
		private Color _AlphaColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x040055B7 RID: 21943
		private Image _BtnImage;
	}
}
