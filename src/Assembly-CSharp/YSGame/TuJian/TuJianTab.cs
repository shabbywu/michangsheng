using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000AB3 RID: 2739
	public class TuJianTab : MonoBehaviour, ITuJianHyperlink
	{
		// Token: 0x06004CC9 RID: 19657 RVA: 0x0020D40C File Offset: 0x0020B60C
		public virtual void Awake()
		{
			this._TabButtonImage = this.TabButton.GetComponent<Image>();
			this._TabButtonText = this.TabButton.GetComponentInChildren<Text>();
			this._ChildRoot = base.transform.Find("Root");
			TuJianManager.TabDict.Add(this.TabType, this);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0020D462 File Offset: 0x0020B662
		public virtual void Start()
		{
			this.TabButton.onClick.AddListener(delegate()
			{
				TuJianManager.Inst.NowTuJianTab = this.TabType;
			});
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0020D480 File Offset: 0x0020B680
		public virtual void Show()
		{
			this._TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_light.png"];
			this._TabButtonText.color = TuJianTab._TabButtonTextShowColor;
			this._ChildRoot.gameObject.SetActive(true);
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0020D4BD File Offset: 0x0020B6BD
		public virtual void Hide()
		{
			this._TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_gn.png"];
			this._TabButtonText.color = TuJianTab._TabButtonTextHideColor;
			this._ChildRoot.gameObject.SetActive(false);
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnHyperlink(int[] args)
		{
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnButtonClick()
		{
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void RefreshPanel(bool isHyperLink = false)
		{
		}

		// Token: 0x04004BDA RID: 19418
		[HideInInspector]
		public TuJianTabType TabType;

		// Token: 0x04004BDB RID: 19419
		public Button TabButton;

		// Token: 0x04004BDC RID: 19420
		private Image _TabButtonImage;

		// Token: 0x04004BDD RID: 19421
		private Text _TabButtonText;

		// Token: 0x04004BDE RID: 19422
		private Transform _ChildRoot;

		// Token: 0x04004BDF RID: 19423
		private static Color _TabButtonTextShowColor = new Color(0.99215686f, 0.88235295f, 0.54901963f);

		// Token: 0x04004BE0 RID: 19424
		private static Color _TabButtonTextHideColor = new Color(0.28235295f, 0.68235296f, 0.7019608f);
	}
}
