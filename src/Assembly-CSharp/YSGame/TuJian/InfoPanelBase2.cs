using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000DE5 RID: 3557
	public class InfoPanelBase2 : InfoPanelBase
	{
		// Token: 0x060055C8 RID: 21960 RVA: 0x0003D636 File Offset: 0x0003B836
		public virtual void Start()
		{
			this.Init();
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0003D63E File Offset: 0x0003B83E
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x0023BA54 File Offset: 0x00239C54
		public void Init()
		{
			this._QualityImage = base.transform.Find("ItemBG/QualityBg").GetComponent<Image>();
			this._ItemIconImage = base.transform.Find("ItemBG/QualityBg/ItemIcon").GetComponent<Image>();
			this._QualityUpImage = base.transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp").GetComponent<Image>();
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
			this._Hy2ContentTransform = (base.transform.Find("HyTextSV2/Viewport/Content") as RectTransform);
			this._HyText2 = base.transform.Find("HyTextSV2/Viewport/Content/Text").GetComponent<SymbolText>();
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x0023BB20 File Offset: 0x00239D20
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
			if (this._Hy2ContentTransform != null && this._Hy2ContentTransform.sizeDelta.y != this._HyText2.preferredHeight + 34f)
			{
				this._Hy2ContentTransform.sizeDelta = new Vector2(this._Hy2ContentTransform.sizeDelta.x, this._HyText2.preferredHeight + 34f);
			}
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0003D598 File Offset: 0x0003B798
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0003D64C File Offset: 0x0003B84C
		public void SetItemIcon(int id)
		{
			this._ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
			this._QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0003D681 File Offset: 0x0003B881
		public void SetSkillIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetShenTongMiShuSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0003D6A5 File Offset: 0x0003B8A5
		public void SetGongFaIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetGongFaSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x0400557C RID: 21884
		protected Image _QualityImage;

		// Token: 0x0400557D RID: 21885
		protected Image _ItemIconImage;

		// Token: 0x0400557E RID: 21886
		protected Image _QualityUpImage;

		// Token: 0x0400557F RID: 21887
		protected RectTransform _HyContentTransform;

		// Token: 0x04005580 RID: 21888
		protected RectTransform _Hy2ContentTransform;

		// Token: 0x04005581 RID: 21889
		protected SymbolText _HyText;

		// Token: 0x04005582 RID: 21890
		protected SymbolText _HyText2;

		// Token: 0x04005583 RID: 21891
		public Color HyTextColor = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x04005584 RID: 21892
		public Color HyTextHoverColor = new Color(0.3764706f, 0.21960784f, 0.02745098f);

		// Token: 0x04005585 RID: 21893
		public Color HyText2Color = new Color(0.003921569f, 0.4745098f, 0.43529412f);

		// Token: 0x04005586 RID: 21894
		public Color HyText2HoverColor = new Color(0.015686275f, 0.3882353f, 0.35686275f);
	}
}
