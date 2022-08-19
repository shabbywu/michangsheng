using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C0 RID: 704
public class UIGaoShiShouGouItem : MonoBehaviour
{
	// Token: 0x060018B1 RID: 6321 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x000B16E8 File Offset: 0x000AF8E8
	public void SetYiShouGou(bool yiShouGou, JSONObject pos, bool anim = false)
	{
		if (yiShouGou)
		{
			this.XuYao.SetActive(false);
			this.Item.gameObject.SetActive(false);
			this.TiJiaoBtn.gameObject.SetActive(false);
			GaoShiManager.SetYinZhangShow(this.YinZhangMask, this.YinZhang, pos, anim);
			return;
		}
		this.XuYao.SetActive(true);
		this.Item.gameObject.SetActive(true);
		this.TiJiaoBtn.gameObject.SetActive(true);
		this.YinZhang.gameObject.SetActive(false);
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000B177C File Offset: 0x000AF97C
	public void SetButtonCanClick(bool canClick)
	{
		this.TiJiaoBtn.enabled = canClick;
		if (canClick)
		{
			this.TiJiaoBtn.GetComponent<Image>().material = null;
			this.TiJiaoBtn.GetComponentInChildren<Text>().color = UIGaoShiShouGouItem._normalColor;
			return;
		}
		this.TiJiaoBtn.GetComponent<Image>().material = this.GreyMat;
		this.TiJiaoBtn.GetComponentInChildren<Text>().color = UIGaoShiShouGouItem._greyColor;
	}

	// Token: 0x040013BD RID: 5053
	public GameObject JiaJi;

	// Token: 0x040013BE RID: 5054
	public Text Desc;

	// Token: 0x040013BF RID: 5055
	public Text LingShiTitle;

	// Token: 0x040013C0 RID: 5056
	public Text LingShi;

	// Token: 0x040013C1 RID: 5057
	public Image LingShiIcon;

	// Token: 0x040013C2 RID: 5058
	public Text ShengWang;

	// Token: 0x040013C3 RID: 5059
	public UIIconShow Item;

	// Token: 0x040013C4 RID: 5060
	public FpBtn TiJiaoBtn;

	// Token: 0x040013C5 RID: 5061
	public RectMask2D YinZhangMask;

	// Token: 0x040013C6 RID: 5062
	public RectTransform YinZhang;

	// Token: 0x040013C7 RID: 5063
	public GameObject XuYao;

	// Token: 0x040013C8 RID: 5064
	public Material GreyMat;

	// Token: 0x040013C9 RID: 5065
	private static Color _normalColor = new Color(1f, 0.92941177f, 0.76862746f);

	// Token: 0x040013CA RID: 5066
	private static Color _greyColor = new Color(0.5411765f, 0.5411765f, 0.5411765f);
}
