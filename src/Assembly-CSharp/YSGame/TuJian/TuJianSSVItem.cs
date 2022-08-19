using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000AB2 RID: 2738
	public class TuJianSSVItem : SSVItem
	{
		// Token: 0x06004CC1 RID: 19649 RVA: 0x0020D27E File Offset: 0x0020B47E
		public override void Start()
		{
			base.Start();
			this._BtnImage = this._button.GetComponent<Image>();
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0020D298 File Offset: 0x0020B498
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

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0020D2E6 File Offset: 0x0020B4E6
		private void OnEnable()
		{
			this._button.onClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0020D304 File Offset: 0x0020B504
		private void OnDisable()
		{
			this._button.onClick.RemoveAllListeners();
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0020D316 File Offset: 0x0020B516
		public void OnClick()
		{
			if (this.SSV.NowSelectItemIndex != base.DataIndex)
			{
				this.SSV.NowSelectItemIndex = base.DataIndex;
				this.OnSelect();
				TuJianManager.Inst.OnButtonClick();
			}
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0020D34C File Offset: 0x0020B54C
		public void OnSelect()
		{
			this._IsSelected = true;
			this._text.color = this._SelectedTextColor;
			this._BtnImage.color = Color.white;
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0020D376 File Offset: 0x0020B576
		public void OnLoseSelect()
		{
			this._IsSelected = false;
			this._text.color = this._NormalTextColor;
			this._BtnImage.color = this._AlphaColor;
		}

		// Token: 0x04004BD4 RID: 19412
		[HideInInspector]
		public bool needRefresh;

		// Token: 0x04004BD5 RID: 19413
		private bool _IsSelected;

		// Token: 0x04004BD6 RID: 19414
		private Color _SelectedTextColor = new Color(0.46666667f, 0.87058824f, 0.76862746f);

		// Token: 0x04004BD7 RID: 19415
		private Color _NormalTextColor = new Color(0.14117648f, 0.34509805f, 0.35686275f);

		// Token: 0x04004BD8 RID: 19416
		private Color _AlphaColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04004BD9 RID: 19417
		private Image _BtnImage;
	}
}
