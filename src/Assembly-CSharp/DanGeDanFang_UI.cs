using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000614 RID: 1556
public class DanGeDanFang_UI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060026B1 RID: 9905 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x0001ED8E File Offset: 0x0001CF8E
	public void delet()
	{
		selectBox._instence.setChoice("确认删除该丹方？", new EventDelegate(delegate()
		{
			int danyaoID = base.transform.parent.parent.GetComponent<DanFang_UI>().ItemID;
			Tools.instance.getPlayer().DanFang.list.Find(delegate(JSONObject aa)
			{
				if (danyaoID == (int)aa["ID"].n)
				{
					bool flag = true;
					for (int i = 0; i < aa["Type"].list.Count; i++)
					{
						if (this.danyao[i] != (int)aa["Type"][i].n)
						{
							flag = false;
						}
						if (this.num[i] != (int)aa["Num"][i].n)
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
			Object.Destroy(base.gameObject);
		}), null);
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x00012FEF File Offset: 0x000111EF
	public void Top()
	{
		base.transform.SetSiblingIndex(0);
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x0012F484 File Offset: 0x0012D684
	public bool CanAdd()
	{
		bool result = true;
		if (LianDanMag.instence.InventoryShowDanlu.inventory[0].itemID <= 0)
		{
			return false;
		}
		int itemID = LianDanMag.instence.InventoryShowDanlu.inventory[0].itemID;
		int num = (int)jsonData.instance.ItemJsonData[itemID.ToString()]["quality"].n;
		for (int i = 0; i < this.danyao.Count; i++)
		{
			if (this.danyao[i] > 0)
			{
				if (!LianDanMag.instence.duiying[num - 1].Contains(i))
				{
					return false;
				}
				bool flag = false;
				foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
				{
					if (this.danyao[i] == item_INFO.itemId && (long)this.num[i] <= (long)((ulong)item_INFO.itemCount))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x0012F5C8 File Offset: 0x0012D7C8
	public void SetLianDanThis()
	{
		if (this.shouldUpDate)
		{
			if (this.CanAdd())
			{
				LianDanMag.instence.addYaoCai(this.danyao, this.num);
				return;
			}
		}
		else if (this.TuJianPlan != null)
		{
			int itemID = base.transform.parent.parent.GetComponent<DanFang_UI>().ItemID;
			Text component = this.TuJianPlan.transform.Find("name").GetComponent<Text>();
			Text component2 = this.TuJianPlan.transform.Find("yaoxiao").GetComponent<Text>();
			Text component3 = this.TuJianPlan.transform.Find("shuoming").GetComponent<Text>();
			Text component4 = this.TuJianPlan.transform.Find("zhuyao").GetComponent<Text>();
			Text component5 = this.TuJianPlan.transform.Find("fuyao").GetComponent<Text>();
			Text component6 = this.TuJianPlan.transform.Find("yaoyin").GetComponent<Text>();
			JSONObject jsonobject = jsonData.instance.ItemJsonData[itemID.ToString()];
			component.text = Tools.Code64(jsonobject["name"].str);
			component2.text = "药效：" + Tools.Code64(jsonobject["desc"].str);
			component3.text = "说明：" + Tools.Code64(jsonobject["desc2"].str);
			component6.text = "药引：";
			component4.text = "主药：";
			component5.text = "辅药：";
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 5; i++)
			{
				if (this.danyao[i] > 0)
				{
					if (i == 0)
					{
						Text text = component6;
						text.text = string.Concat(new object[]
						{
							text.text,
							Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
							"*",
							this.num[i]
						});
					}
					else if (i == 1 || i == 2)
					{
						if (num == 1)
						{
							Text text2 = component4;
							text2.text += "、";
						}
						Text text = component4;
						text.text = string.Concat(new object[]
						{
							text.text,
							Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
							"*",
							this.num[i]
						});
						num++;
					}
					else if (i == 3 || i == 4)
					{
						if (num2 == 1)
						{
							Text text3 = component4;
							text3.text += "、";
						}
						Text text = component5;
						text.text = string.Concat(new object[]
						{
							text.text,
							Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
							"*",
							this.num[i]
						});
						num2++;
					}
				}
			}
			Text text4 = component6;
			text4.text += "。";
			Text text5 = component4;
			text5.text += ((num >= 1) ? "。" : "无");
			Text text6 = component5;
			text6.text += ((num2 >= 1) ? "。" : "无");
		}
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x0012F9D8 File Offset: 0x0012DBD8
	public void init()
	{
		this.yaoyin.text = "药引：";
		this.zhuyao.text = "主药：";
		this.fuyao.text = "辅药：";
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 5; i++)
		{
			if (this.danyao[i] > 0)
			{
				if (i == 0)
				{
					Text text = this.yaoyin;
					text.text = string.Concat(new object[]
					{
						text.text,
						Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
						"*",
						this.num[i]
					});
				}
				else if (i == 1 || i == 2)
				{
					if (num == 1)
					{
						Text text2 = this.zhuyao;
						text2.text += "、";
					}
					Text text = this.zhuyao;
					text.text = string.Concat(new object[]
					{
						text.text,
						Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
						"*",
						this.num[i]
					});
					num++;
				}
				else if (i == 3 || i == 4)
				{
					if (num2 == 1)
					{
						Text text3 = this.zhuyao;
						text3.text += "、";
					}
					Text text = this.fuyao;
					text.text = string.Concat(new object[]
					{
						text.text,
						Tools.Code64(jsonData.instance.ItemJsonData[this.danyao[i].ToString()]["name"].str),
						"*",
						this.num[i]
					});
					num2++;
				}
			}
		}
		Text text4 = this.yaoyin;
		text4.text += "。";
		Text text5 = this.zhuyao;
		text5.text += ((num >= 1) ? "。" : "无");
		Text text6 = this.fuyao;
		text6.text += ((num2 >= 1) ? "。" : "无");
		if (this.removebtn != null)
		{
			this.removebtn.SetActive(false);
		}
		if (this.zhidingbtn != null)
		{
			this.zhidingbtn.SetActive(false);
		}
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x0012FCAC File Offset: 0x0012DEAC
	private void Update()
	{
		if (this.shouldUpDate)
		{
			if (this.CanAdd())
			{
				this.icon.sprite = this.iconsprite[0];
				return;
			}
			this.icon.sprite = this.iconsprite[1];
		}
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.removebtn != null)
		{
			this.removebtn.SetActive(true);
		}
		if (this.zhidingbtn != null)
		{
			this.zhidingbtn.SetActive(true);
		}
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x0001EDE7 File Offset: 0x0001CFE7
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.removebtn != null)
		{
			this.removebtn.SetActive(false);
		}
		if (this.zhidingbtn != null)
		{
			this.zhidingbtn.SetActive(false);
		}
	}

	// Token: 0x04002103 RID: 8451
	public Text zhuyao;

	// Token: 0x04002104 RID: 8452
	public Text fuyao;

	// Token: 0x04002105 RID: 8453
	public Text yaoyin;

	// Token: 0x04002106 RID: 8454
	public GameObject removebtn;

	// Token: 0x04002107 RID: 8455
	public GameObject zhidingbtn;

	// Token: 0x04002108 RID: 8456
	public Image icon;

	// Token: 0x04002109 RID: 8457
	public List<Sprite> iconsprite;

	// Token: 0x0400210A RID: 8458
	public List<int> danyao = new List<int>();

	// Token: 0x0400210B RID: 8459
	public List<int> num = new List<int>();

	// Token: 0x0400210C RID: 8460
	public bool shouldUpDate = true;

	// Token: 0x0400210D RID: 8461
	public GameObject TuJianPlan;
}
