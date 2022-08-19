using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200050C RID: 1292
public class WuDaoCellTooltip : MonoBehaviour
{
	// Token: 0x0600298A RID: 10634 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x0013D678 File Offset: 0x0013B878
	public void open(int _id, Image icon)
	{
		base.gameObject.SetActive(true);
		this.tooltipItem.Clear();
		this.tooltipItem.wudao.SetActive(true);
		if (this.tooltipItem.Slot != null)
		{
			this.tooltipItem.Slot.SetActive(false);
		}
		this.tooltipItem.wudao_Icon.sprite2D = icon.sprite;
		this.ID = _id;
		JSONObject jsonobject = jsonData.instance.WuDaoJson[_id.ToString()];
		this.tooltipItem.Label4.text = "[ff744d]" + Tools.Code64(jsonobject["name"].str) + "[-]";
		this.tooltipItem.Label5.text = "";
		this.tooltipItem.pingji.SetActive(false);
		this.tooltipItem.Label1.text = "[E0DDB4]" + Tools.Code64(jsonobject["xiaoguo"].str) + "[-]";
		string text = "";
		for (int i = 0; i < jsonobject["Type"].Count; i++)
		{
			text += Tools.Code64(jsonData.instance.WuDaoAllTypeJson[jsonobject["Type"][i].I.ToString()]["name"].str);
			if (i < jsonobject["Type"].Count - 1)
			{
				text += ",";
			}
		}
		string text2 = Tools.Code64(jsonData.instance.WuDaoJinJieJson[jsonobject["Lv"].I.ToString()]["Text"].str);
		this.tooltipItem.DownBtn.GetComponent<UIButton>().onClick.Clear();
		this.tooltipItem.DownBtn.GetComponent<UIButton>().onClick.Add(new EventDelegate(new EventDelegate.Callback(this.Btn)));
		this.tooltipItem.DownBtn.GetComponentInChildren<UILabel>().text = "感悟";
		this.tooltipItem.DownBtn.SetActive(true);
		this.tooltipItem.wuDaoCast.gameObject.SetActive(true);
		this.tooltipItem.wudaoYaoQiu.gameObject.SetActive(true);
		this.tooltipItem.setCenterTextTitle("[FDE52B]需求点数：[-]" + jsonobject["Cast"].I, "", "[FDE52B]领悟条件：[-]对" + text + "之道的感悟达到" + text2);
		this.tooltipItem.Label7.gameObject.SetActive(false);
		this.tooltipItem.Label8.gameObject.SetActive(false);
		this.tooltipItem.Label9.gameObject.SetActive(false);
		this.tooltipItem.CenterText1.gameObject.SetActive(false);
		this.tooltipItem.CenterText2.gameObject.SetActive(false);
		this.tooltipItem.CenterText3.gameObject.SetActive(false);
		this.tooltipItem.wuDaoCast.text = "[ffb143]【需求点数】[-][E0DDB4]" + jsonobject["Cast"].I + "[-]";
		this.tooltipItem.wudaoYaoQiu.text = string.Concat(new string[]
		{
			"[ffb143]【领悟条件】[-][E0DDB4]对",
			text,
			"之道的感悟达到",
			text2,
			"[-]"
		});
		this.tooltipItem.Label2.text = "[bfba7d]" + Tools.Code64(jsonobject["desc"].str) + "[-]";
		this.tooltipItem.showTooltip = true;
		this.tooltipItem.showType = 3;
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x0013DA79 File Offset: 0x0013BC79
	private void Update()
	{
		this.bgImage.rectTransform.sizeDelta = this.ContentParent.GetComponent<RectTransform>().sizeDelta;
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x0013DA9B File Offset: 0x0013BC9B
	public void close()
	{
		base.gameObject.SetActive(false);
		this.tooltipItem.showTooltip = false;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x0013DAB5 File Offset: 0x0013BCB5
	public void Btn()
	{
		if (this.action != null)
		{
			this.action.Invoke();
		}
		this.close();
	}

	// Token: 0x040025E6 RID: 9702
	public int ID;

	// Token: 0x040025E7 RID: 9703
	public TooltipItem tooltipItem;

	// Token: 0x040025E8 RID: 9704
	public Text title;

	// Token: 0x040025E9 RID: 9705
	public Text desc;

	// Token: 0x040025EA RID: 9706
	public Text seid;

	// Token: 0x040025EB RID: 9707
	public Text castText;

	// Token: 0x040025EC RID: 9708
	public Text qianzhiTemp;

	// Token: 0x040025ED RID: 9709
	public GameObject ContentParent;

	// Token: 0x040025EE RID: 9710
	public Image bgImage;

	// Token: 0x040025EF RID: 9711
	public UnityAction action;
}
