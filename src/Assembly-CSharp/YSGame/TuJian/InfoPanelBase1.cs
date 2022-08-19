using System;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000AA7 RID: 2727
	public class InfoPanelBase1 : InfoPanelBase
	{
		// Token: 0x06004C73 RID: 19571 RVA: 0x0020A832 File Offset: 0x00208A32
		public virtual void Start()
		{
			this.Init();
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0020A83A File Offset: 0x00208A3A
		public override void Update()
		{
			base.Update();
			this.RefreshSVHeight();
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0020A848 File Offset: 0x00208A48
		public void Init()
		{
			this._QualityImage = base.transform.Find("ItemBG/QualityBg").GetComponent<Image>();
			this._QualityUpImage = base.transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp").GetComponent<Image>();
			this._ItemIconImage = base.transform.Find("ItemBG/QualityBg/ItemIcon").GetComponent<Image>();
			this._HyContentTransform = (base.transform.Find("HyTextSV/Viewport/Content") as RectTransform);
			this._HyText = base.transform.Find("HyTextSV/Viewport/Content/Text").GetComponent<SymbolText>();
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0020A8DC File Offset: 0x00208ADC
		public void RefreshSVHeight()
		{
			if (this._HyContentTransform != null && this._HyContentTransform.sizeDelta.y != this._HyText.preferredHeight + 34f)
			{
				this._HyContentTransform.sizeDelta = new Vector2(this._HyContentTransform.sizeDelta.x, this._HyText.preferredHeight + 34f);
			}
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0020A94B File Offset: 0x00208B4B
		public override void OnHyperlink(int[] args)
		{
			base.OnHyperlink(args);
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0020A954 File Offset: 0x00208B54
		public void SetItemIcon(int id)
		{
			this._ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
			this._QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0020A989 File Offset: 0x00208B89
		public void SetSkillIcon(int id, int quality)
		{
			this._ItemIconImage.sprite = TuJianDB.GetShenTongMiShuSprite(id);
			this._QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
		}

		// Token: 0x04004B97 RID: 19351
		protected Image _QualityImage;

		// Token: 0x04004B98 RID: 19352
		protected Image _QualityUpImage;

		// Token: 0x04004B99 RID: 19353
		protected Image _ItemIconImage;

		// Token: 0x04004B9A RID: 19354
		protected RectTransform _HyContentTransform;

		// Token: 0x04004B9B RID: 19355
		protected SymbolText _HyText;

		// Token: 0x04004B9C RID: 19356
		public Color HyTextColor = new Color(0.4627451f, 0.26666668f, 0.02745098f);

		// Token: 0x04004B9D RID: 19357
		public Color HyTextHoverColor = new Color(0.3764706f, 0.21960784f, 0.02745098f);
	}
}
