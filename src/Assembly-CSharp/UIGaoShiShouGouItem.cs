using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000400 RID: 1024
public class UIGaoShiShouGouItem : MonoBehaviour
{
	// Token: 0x06001BAC RID: 7084 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000F7D50 File Offset: 0x000F5F50
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

	// Token: 0x06001BAE RID: 7086 RVA: 0x000F7DE4 File Offset: 0x000F5FE4
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

	// Token: 0x0400176D RID: 5997
	public GameObject JiaJi;

	// Token: 0x0400176E RID: 5998
	public Text Desc;

	// Token: 0x0400176F RID: 5999
	public Text LingShiTitle;

	// Token: 0x04001770 RID: 6000
	public Text LingShi;

	// Token: 0x04001771 RID: 6001
	public Image LingShiIcon;

	// Token: 0x04001772 RID: 6002
	public Text ShengWang;

	// Token: 0x04001773 RID: 6003
	public UIIconShow Item;

	// Token: 0x04001774 RID: 6004
	public FpBtn TiJiaoBtn;

	// Token: 0x04001775 RID: 6005
	public RectMask2D YinZhangMask;

	// Token: 0x04001776 RID: 6006
	public RectTransform YinZhang;

	// Token: 0x04001777 RID: 6007
	public GameObject XuYao;

	// Token: 0x04001778 RID: 6008
	public Material GreyMat;

	// Token: 0x04001779 RID: 6009
	private static Color _normalColor = new Color(1f, 0.92941177f, 0.76862746f);

	// Token: 0x0400177A RID: 6010
	private static Color _greyColor = new Color(0.5411765f, 0.5411765f, 0.5411765f);
}
