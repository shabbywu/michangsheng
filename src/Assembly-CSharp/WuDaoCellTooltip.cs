using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WuDaoCellTooltip : MonoBehaviour
{
	public int ID;

	public TooltipItem tooltipItem;

	public Text title;

	public Text desc;

	public Text seid;

	public Text castText;

	public Text qianzhiTemp;

	public GameObject ContentParent;

	public Image bgImage;

	public UnityAction action;

	private void Start()
	{
	}

	public void open(int _id, Image icon)
	{
		((Component)this).gameObject.SetActive(true);
		tooltipItem.Clear();
		tooltipItem.wudao.SetActive(true);
		if ((Object)(object)tooltipItem.Slot != (Object)null)
		{
			tooltipItem.Slot.SetActive(false);
		}
		tooltipItem.wudao_Icon.sprite2D = icon.sprite;
		ID = _id;
		JSONObject jSONObject = jsonData.instance.WuDaoJson[_id.ToString()];
		tooltipItem.Label4.text = "[ff744d]" + Tools.Code64(jSONObject["name"].str) + "[-]";
		tooltipItem.Label5.text = "";
		tooltipItem.pingji.SetActive(false);
		tooltipItem.Label1.text = "[E0DDB4]" + Tools.Code64(jSONObject["xiaoguo"].str) + "[-]";
		string text = "";
		for (int i = 0; i < jSONObject["Type"].Count; i++)
		{
			text += Tools.Code64(jsonData.instance.WuDaoAllTypeJson[jSONObject["Type"][i].I.ToString()]["name"].str);
			if (i < jSONObject["Type"].Count - 1)
			{
				text += ",";
			}
		}
		string text2 = Tools.Code64(jsonData.instance.WuDaoJinJieJson[jSONObject["Lv"].I.ToString()]["Text"].str);
		tooltipItem.DownBtn.GetComponent<UIButton>().onClick.Clear();
		tooltipItem.DownBtn.GetComponent<UIButton>().onClick.Add(new EventDelegate(Btn));
		tooltipItem.DownBtn.GetComponentInChildren<UILabel>().text = "感悟";
		tooltipItem.DownBtn.SetActive(true);
		((Component)tooltipItem.wuDaoCast).gameObject.SetActive(true);
		((Component)tooltipItem.wudaoYaoQiu).gameObject.SetActive(true);
		tooltipItem.setCenterTextTitle("[FDE52B]需求点数：[-]" + jSONObject["Cast"].I, "", "[FDE52B]领悟条件：[-]对" + text + "之道的感悟达到" + text2);
		((Component)tooltipItem.Label7).gameObject.SetActive(false);
		((Component)tooltipItem.Label8).gameObject.SetActive(false);
		((Component)tooltipItem.Label9).gameObject.SetActive(false);
		((Component)tooltipItem.CenterText1).gameObject.SetActive(false);
		((Component)tooltipItem.CenterText2).gameObject.SetActive(false);
		((Component)tooltipItem.CenterText3).gameObject.SetActive(false);
		tooltipItem.wuDaoCast.text = "[ffb143]【需求点数】[-][E0DDB4]" + jSONObject["Cast"].I + "[-]";
		tooltipItem.wudaoYaoQiu.text = "[ffb143]【领悟条件】[-][E0DDB4]对" + text + "之道的感悟达到" + text2 + "[-]";
		tooltipItem.Label2.text = "[bfba7d]" + Tools.Code64(jSONObject["desc"].str) + "[-]";
		tooltipItem.showTooltip = true;
		tooltipItem.showType = 3;
	}

	private void Update()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)bgImage).rectTransform.sizeDelta = ContentParent.GetComponent<RectTransform>().sizeDelta;
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
		tooltipItem.showTooltip = false;
	}

	public void Btn()
	{
		if (action != null)
		{
			action.Invoke();
		}
		close();
	}
}
