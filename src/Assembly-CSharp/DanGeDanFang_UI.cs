using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DanGeDanFang_UI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Text zhuyao;

	public Text fuyao;

	public Text yaoyin;

	public GameObject removebtn;

	public GameObject zhidingbtn;

	public Image icon;

	public List<Sprite> iconsprite;

	public List<int> danyao = new List<int>();

	public List<int> num = new List<int>();

	public bool shouldUpDate = true;

	public GameObject TuJianPlan;

	private void Start()
	{
	}

	public void delet()
	{
		selectBox._instence.setChoice("确认删除该丹方？", new EventDelegate(delegate
		{
			int danyaoID = ((Component)((Component)this).transform.parent.parent).GetComponent<DanFang_UI>().ItemID;
			Tools.instance.getPlayer().DanFang.list.Find(delegate(JSONObject aa)
			{
				if (danyaoID == aa["ID"].I)
				{
					bool flag = true;
					for (int i = 0; i < aa["Type"].list.Count; i++)
					{
						if (danyao[i] != (int)aa["Type"][i].n)
						{
							flag = false;
						}
						if (num[i] != (int)aa["Num"][i].n)
						{
							flag = false;
						}
					}
					if (flag)
					{
						return true;
					}
				}
				return false;
			}).SetField("ID", 0);
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}), null);
	}

	public void Top()
	{
		((Component)this).transform.SetSiblingIndex(0);
	}

	public bool CanAdd()
	{
		bool result = true;
		if (LianDanMag.instence.InventoryShowDanlu.inventory[0].itemID <= 0)
		{
			return false;
		}
		int itemID = LianDanMag.instence.InventoryShowDanlu.inventory[0].itemID;
		int num = (int)jsonData.instance.ItemJsonData[itemID.ToString()]["quality"].n;
		for (int i = 0; i < danyao.Count; i++)
		{
			if (danyao[i] <= 0)
			{
				continue;
			}
			if (!LianDanMag.instence.duiying[num - 1].Contains(i))
			{
				return false;
			}
			bool flag = false;
			foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
			{
				if (danyao[i] == value.itemId && this.num[i] <= value.itemCount)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				result = false;
			}
		}
		return result;
	}

	public void SetLianDanThis()
	{
		if (shouldUpDate)
		{
			if (CanAdd())
			{
				LianDanMag.instence.addYaoCai(danyao, this.num);
			}
		}
		else
		{
			if (!((Object)(object)TuJianPlan != (Object)null))
			{
				return;
			}
			int itemID = ((Component)((Component)this).transform.parent.parent).GetComponent<DanFang_UI>().ItemID;
			Text component = ((Component)TuJianPlan.transform.Find("name")).GetComponent<Text>();
			Text component2 = ((Component)TuJianPlan.transform.Find("yaoxiao")).GetComponent<Text>();
			Text component3 = ((Component)TuJianPlan.transform.Find("shuoming")).GetComponent<Text>();
			Text component4 = ((Component)TuJianPlan.transform.Find("zhuyao")).GetComponent<Text>();
			Text component5 = ((Component)TuJianPlan.transform.Find("fuyao")).GetComponent<Text>();
			Text component6 = ((Component)TuJianPlan.transform.Find("yaoyin")).GetComponent<Text>();
			JSONObject jSONObject = jsonData.instance.ItemJsonData[itemID.ToString()];
			component.text = Tools.Code64(jSONObject["name"].str);
			component2.text = "药效：" + Tools.Code64(jSONObject["desc"].str);
			component3.text = "说明：" + Tools.Code64(jSONObject["desc2"].str);
			component6.text = "药引：";
			component4.text = "主药：";
			component5.text = "辅药：";
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 5; i++)
			{
				if (danyao[i] <= 0)
				{
					continue;
				}
				switch (i)
				{
				case 0:
				{
					Text val = component6;
					val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
					break;
				}
				case 1:
				case 2:
				{
					if (num == 1)
					{
						component4.text += "、";
					}
					Text val = component4;
					val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
					num++;
					break;
				}
				case 3:
				case 4:
				{
					if (num2 == 1)
					{
						component4.text += "、";
					}
					Text val = component5;
					val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
					num2++;
					break;
				}
				}
			}
			component6.text += "。";
			component4.text += ((num >= 1) ? "。" : "无");
			component5.text += ((num2 >= 1) ? "。" : "无");
		}
	}

	public void init()
	{
		yaoyin.text = "药引：";
		zhuyao.text = "主药：";
		fuyao.text = "辅药：";
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 5; i++)
		{
			if (danyao[i] <= 0)
			{
				continue;
			}
			switch (i)
			{
			case 0:
			{
				Text val = yaoyin;
				val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
				break;
			}
			case 1:
			case 2:
			{
				if (num == 1)
				{
					Text obj2 = zhuyao;
					obj2.text += "、";
				}
				Text val = zhuyao;
				val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
				num++;
				break;
			}
			case 3:
			case 4:
			{
				if (num2 == 1)
				{
					Text obj = zhuyao;
					obj.text += "、";
				}
				Text val = fuyao;
				val.text = val.text + Tools.Code64(jsonData.instance.ItemJsonData[danyao[i].ToString()]["name"].str) + "*" + this.num[i];
				num2++;
				break;
			}
			}
		}
		Text obj3 = yaoyin;
		obj3.text += "。";
		Text obj4 = zhuyao;
		obj4.text += ((num >= 1) ? "。" : "无");
		Text obj5 = fuyao;
		obj5.text += ((num2 >= 1) ? "。" : "无");
		if ((Object)(object)removebtn != (Object)null)
		{
			removebtn.SetActive(false);
		}
		if ((Object)(object)zhidingbtn != (Object)null)
		{
			zhidingbtn.SetActive(false);
		}
	}

	private void Update()
	{
		if (shouldUpDate)
		{
			if (CanAdd())
			{
				icon.sprite = iconsprite[0];
			}
			else
			{
				icon.sprite = iconsprite[1];
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if ((Object)(object)removebtn != (Object)null)
		{
			removebtn.SetActive(true);
		}
		if ((Object)(object)zhidingbtn != (Object)null)
		{
			zhidingbtn.SetActive(true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if ((Object)(object)removebtn != (Object)null)
		{
			removebtn.SetActive(false);
		}
		if ((Object)(object)zhidingbtn != (Object)null)
		{
			zhidingbtn.SetActive(false);
		}
	}
}
