using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DE4 RID: 3556
	public class InfoPanelBase1 : InfoPanelBase
	{
		// Token: 0x060055C0 RID: 21952 RVA: 0x0003D582 File Offset: 0x0003B782
		public virtual void Start()
		{
			this.Init();
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0003D58A File Offset: 0x0003B78A
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0023B950 File Offset: 0x00239B50
		public void Init()
		{
			this._QualityImage = base.transform.Find("ItemBG/QualityBg").GetComponent<Image>();
			this._QualityUpImage = base.transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp").GetComponent<Image>();
			this._ItemIconImage = base.transform.Find("ItemBG/QualityBg/ItemIcon").GetComponent<Image>();
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x0023B9E4 File Offset: 0x00239BE4
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x0003D598 File Offset: 0x0003B798
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0003D5A1 File Offset: 0x0003B7A1
		public void SetItemIcon(int id)
		{
			this._ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
			this._QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x0003D5D6 File Offset: 0x0003B7D6
		public void SetSkillIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetShenTongMiShuSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x04005575 RID: 21877
		protected Image _QualityImage;

		// Token: 0x04005576 RID: 21878
		protected Image _QualityUpImage;

		// Token: 0x04005577 RID: 21879
		protected Image _ItemIconImage;

		// Token: 0x04005578 RID: 21880
		protected RectTransform _HyContentTransform;

		// Token: 0x04005579 RID: 21881
		protected SymbolText _HyText;

		// Token: 0x0400557A RID: 21882
		public Color HyTextColor = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x0400557B RID: 21883
		public Color HyTextHoverColor = new Color(0.3764706f, 0.21960784f, 0.02745098f);
	}
}
