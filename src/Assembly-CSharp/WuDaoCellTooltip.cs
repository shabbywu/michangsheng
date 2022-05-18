using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200079D RID: 1949
public class WuDaoCellTooltip : MonoBehaviour
{
	// Token: 0x0600318F RID: 12687 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x0018A910 File Offset: 0x00188B10
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

	// Token: 0x06003191 RID: 12689 RVA: 0x0002448E File Offset: 0x0002268E
	private void Update()
	{
		this.bgImage.rectTransform.sizeDelta = this.ContentParent.GetComponent<RectTransform>().sizeDelta;
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x000244B0 File Offset: 0x000226B0
	public void close()
	{
		base.gameObject.SetActive(false);
		this.tooltipItem.showTooltip = false;
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x000244CA File Offset: 0x000226CA
	public void Btn()
	{
		if (this.action != null)
		{
			this.action.Invoke();
		}
		this.close();
	}

	// Token: 0x04002DCE RID: 11726
	public int ID;

	// Token: 0x04002DCF RID: 11727
	public TooltipItem tooltipItem;

	// Token: 0x04002DD0 RID: 11728
	public Text title;

	// Token: 0x04002DD1 RID: 11729
	public Text desc;

	// Token: 0x04002DD2 RID: 11730
	public Text seid;

	// Token: 0x04002DD3 RID: 11731
	public Text castText;

	// Token: 0x04002DD4 RID: 11732
	public Text qianzhiTemp;

	// Token: 0x04002DD5 RID: 11733
	public GameObject ContentParent;

	// Token: 0x04002DD6 RID: 11734
	public Image bgImage;

	// Token: 0x04002DD7 RID: 11735
	public UnityAction action;
}
