using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000AA8 RID: 2728
	public class InfoPanelBase2 : InfoPanelBase
	{
		// Token: 0x06004C7B RID: 19579 RVA: 0x0020A9E9 File Offset: 0x00208BE9
		public virtual void Start()
		{
			this.Init();
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0020A9F1 File Offset: 0x00208BF1
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0020AA00 File Offset: 0x00208C00
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

		// Token: 0x06004C7E RID: 19582 RVA: 0x0020AACC File Offset: 0x00208CCC
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

		// Token: 0x06004C7F RID: 19583 RVA: 0x0020A94B File Offset: 0x00208B4B
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0020AB9D File Offset: 0x00208D9D
		public void SetItemIcon(int id)
		{
			this._ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
			this._QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0020ABD2 File Offset: 0x00208DD2
		public void SetSkillIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetShenTongMiShuSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0020ABF6 File Offset: 0x00208DF6
		public void SetGongFaIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetGongFaSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x04004B9E RID: 19358
		protected Image _QualityImage;

		// Token: 0x04004B9F RID: 19359
		protected Image _ItemIconImage;

		// Token: 0x04004BA0 RID: 19360
		protected Image _QualityUpImage;

		// Token: 0x04004BA1 RID: 19361
		protected RectTransform _HyContentTransform;

		// Token: 0x04004BA2 RID: 19362
		protected RectTransform _Hy2ContentTransform;

		// Token: 0x04004BA3 RID: 19363
		protected SymbolText _HyText;

		// Token: 0x04004BA4 RID: 19364
		protected SymbolText _HyText2;

		// Token: 0x04004BA5 RID: 19365
		public Color HyTextColor = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x04004BA6 RID: 19366
		public Color HyTextHoverColor = new Color(0.3764706f, 0.21960784f, 0.02745098f);

		// Token: 0x04004BA7 RID: 19367
		public Color HyText2Color = new Color(0.003921569f, 0.4745098f, 0.43529412f);

		// Token: 0x04004BA8 RID: 19368
		public Color HyText2HoverColor = new Color(0.015686275f, 0.3882353f, 0.35686275f);
	}
}
