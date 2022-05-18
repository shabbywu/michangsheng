using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DF0 RID: 3568
	public class TuJianTab : MonoBehaviour, ITuJianHyperlink
	{
		// Token: 0x06005616 RID: 22038 RVA: 0x0023E0D4 File Offset: 0x0023C2D4
		public virtual void Awake()
		{
			this._TabButtonImage = this.TabButton.GetComponent<Image>();
			this._TabButtonText = this.TabButton.GetComponentInChildren<Text>();
			this._ChildRoot = base.transform.Find("Root");
			TuJianManager.TabDict.Add(this.TabType, this);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0003D9D7 File Offset: 0x0003BBD7
		public virtual void Start()
		{
			this.TabButton.onClick.AddListener(delegate()
			{
				TuJianManager.Inst.NowTuJianTab = this.TabType;
			});
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
		public virtual void Show()
		{
			this._TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_light.png"];
			this._TabButtonText.color = TuJianTab._TabButtonTextShowColor;
			this._ChildRoot.gameObject.SetActive(true);
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x0003DA32 File Offset: 0x0003BC32
		public virtual void Hide()
		{
			this._TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_gn.png"];
			this._TabButtonText.color = TuJianTab._TabButtonTextHideColor;
			this._ChildRoot.gameObject.SetActive(false);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnHyperlink(int[] args)
		{
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnButtonClick()
		{
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void RefreshPanel(bool isHyperLink = false)
		{
		}

		// Token: 0x040055B8 RID: 21944
		[HideInInspector]
		public TuJianTabType TabType;

		// Token: 0x040055B9 RID: 21945
		public Button TabButton;

		// Token: 0x040055BA RID: 21946
		private Image _TabButtonImage;

		// Token: 0x040055BB RID: 21947
		private Text _TabButtonText;

		// Token: 0x040055BC RID: 21948
		private Transform _ChildRoot;

		// Token: 0x040055BD RID: 21949
		private static Color _TabButtonTextShowColor = new Color(0.99215686f, 0.88235295f, 0.54901963f);

		// Token: 0x040055BE RID: 21950
		private static Color _TabButtonTextHideColor = new Color(0.28235295f, 0.68235296f, 0.7019608f);
	}
}
