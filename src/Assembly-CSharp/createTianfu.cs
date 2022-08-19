using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class createTianfu : MonoBehaviour
{
	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06002084 RID: 8324 RVA: 0x000E4F40 File Offset: 0x000E3140
	public List<createAvatarChoice> getSelectChoice
	{
		get
		{
			List<createAvatarChoice> list = new List<createAvatarChoice>();
			foreach (object obj in this.grid.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponentInChildren<UIToggle>().value)
				{
					list.Add(transform.GetComponentInChildren<createAvatarChoice>());
				}
			}
			return list;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06002085 RID: 8325 RVA: 0x000E4FB8 File Offset: 0x000E31B8
	// (set) Token: 0x06002086 RID: 8326 RVA: 0x000E5034 File Offset: 0x000E3234
	public int TianFuDian
	{
		get
		{
			int num = 0;
			foreach (object obj in this.grid.transform)
			{
				Transform transform = (Transform)obj;
				createAvatarChoice componentInChildren = transform.GetComponentInChildren<createAvatarChoice>();
				if (transform.GetComponentInChildren<UIToggle>().value)
				{
					num += componentInChildren.cast;
				}
			}
			return this._TianFuDian - num;
		}
		set
		{
			this._TianFuDian = value;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06002087 RID: 8327 RVA: 0x000E5040 File Offset: 0x000E3240
	public int ZiZhi
	{
		get
		{
			int addzizhi = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getSeidValue1();
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["ziZhi"].n + addzizhi;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06002088 RID: 8328 RVA: 0x000E509C File Offset: 0x000E329C
	public int Money
	{
		get
		{
			int _money = jsonData.instance.AvatarJsonData["1"]["MoneyType"].I;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				_money += aa.getValue(14);
			});
			return _money;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06002089 RID: 8329 RVA: 0x000E50F8 File Offset: 0x000E32F8
	public int LinGengZiZhi
	{
		get
		{
			int result = 4;
			foreach (JSONObject jsonobject in jsonData.instance.LinGenZiZhiJsonData.list)
			{
				if (this.ZiZhi >= (int)jsonobject["qujian"].n)
				{
					result = jsonobject["id"].I;
					break;
				}
			}
			return result;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x0600208A RID: 8330 RVA: 0x000E517C File Offset: 0x000E337C
	public int DunSu
	{
		get
		{
			int addNum = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(7);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["dunSu"].n + addNum;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x0600208B RID: 8331 RVA: 0x000E51D8 File Offset: 0x000E33D8
	public int HP_Max
	{
		get
		{
			int addNum = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(8);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["HP"].n + addNum;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x0600208C RID: 8332 RVA: 0x000E5234 File Offset: 0x000E3434
	public int XinJin
	{
		get
		{
			int addzizhi = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getValue(4);
			});
			return addzizhi;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x0600208D RID: 8333 RVA: 0x000E526C File Offset: 0x000E346C
	public int WuXin
	{
		get
		{
			int addzizhi = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getSeidValue2();
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["wuXin"].n + addzizhi;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x0600208E RID: 8334 RVA: 0x000E52C8 File Offset: 0x000E34C8
	public int ShowYuan
	{
		get
		{
			int addzizhi = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getValue(5);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["shouYuan"].n + addzizhi;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x0600208F RID: 8335 RVA: 0x000E5324 File Offset: 0x000E3524
	public int ShenShi
	{
		get
		{
			int addNum = 0;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(6);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["shengShi"].n + addNum;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06002090 RID: 8336 RVA: 0x000E5380 File Offset: 0x000E3580
	public List<int> Items
	{
		get
		{
			List<int> addNum = new List<int>();
			Action<JSONObject> <>9__1;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				List<JSONObject> seidValue = aa.getSeidValue10();
				if (seidValue != null)
				{
					List<JSONObject> list = seidValue;
					Action<JSONObject> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(JSONObject ii)
						{
							addNum.Add((int)ii.n);
						});
					}
					list.ForEach(action);
				}
			});
			return addNum;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06002091 RID: 8337 RVA: 0x000E53BC File Offset: 0x000E35BC
	public List<int> StaticSkill
	{
		get
		{
			List<int> addNum = new List<int>();
			Action<JSONObject> <>9__1;
			this.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				List<JSONObject> seidValue = aa.getSeidValue9();
				if (seidValue != null)
				{
					List<JSONObject> list = seidValue;
					Action<JSONObject> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(JSONObject ii)
						{
							addNum.Add((int)ii.n);
						});
					}
					list.ForEach(action);
				}
			});
			return addNum;
		}
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x000E53F8 File Offset: 0x000E35F8
	private void Awake()
	{
		this.LingGengBiaoGe.SetActive(false);
		this.lightGrid.GetComponent<UIGrid>().repositionNow = true;
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject item in jsonData.instance.CreateAvatarJsonData.list)
		{
			list.Add(item);
		}
		list.Sort((JSONObject aa, JSONObject bb) => aa["feiYong"].n.CompareTo(bb["feiYong"].n));
		this.NanduDict.Clear();
		foreach (JSONObject jsonobject in list)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.TempObj);
			gameObject.transform.SetParent(this.grid.transform);
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.position = Vector3.zero;
			gameObject.SetActive(true);
			createAvatarChoice componentInChildren = gameObject.GetComponentInChildren<createAvatarChoice>();
			componentInChildren.Title.text = Tools.instance.Code64ToString(jsonobject["Title"].str);
			componentInChildren.desc = Tools.instance.Code64ToString(jsonobject["Desc"].str);
			componentInChildren.descInfo = Tools.instance.Code64ToString(jsonobject["Info"].str);
			componentInChildren.cast = (int)jsonobject["feiYong"].n;
			componentInChildren.id = jsonobject["id"].I;
			foreach (JSONObject jsonobject2 in jsonobject["seid"].list)
			{
				componentInChildren.seid.Add((int)jsonobject2.n);
			}
			if (componentInChildren.id >= 1 && componentInChildren.id <= 5)
			{
				this.NanduDict.Add(componentInChildren.id, gameObject);
			}
			UIToggle componentInChildren2 = gameObject.GetComponentInChildren<UIToggle>();
			componentInChildren2.optionCanBeNone = true;
			componentInChildren2.group = (int)jsonobject["fenZu"].n;
			if (jsonobject["jiesuo"].I > CreateAvatarMag.inst.maxLevel)
			{
				componentInChildren.isLock = true;
				componentInChildren.LockMessager = Tools.Code64("剧情模式境界达到" + jsonData.instance.LevelUpDataJsonData[jsonobject["jiesuo"].I.ToString()]["Name"].str + "解锁");
				componentInChildren.GetComponent<UIButton>().enabled = false;
				componentInChildren.GetComponent<UIToggle>().enabled = false;
				componentInChildren.GetComponent<UIButton>().normalSprite2D = CreateAvatarMag.inst.LockImage;
			}
		}
		for (int i = 5; i >= 1; i--)
		{
			this.NanduDict[i].transform.SetAsLastSibling();
		}
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x000E576C File Offset: 0x000E396C
	public void setText(int PageIndex)
	{
		JSONObject jsonobject = jsonData.instance.CreateAvatarMiaoShu[PageIndex.ToString()];
		this.Title.text = Tools.Code64(jsonobject["title"].str);
		string text = "";
		foreach (object obj in this.grid.transform)
		{
			Transform transform = (Transform)obj;
			createAvatarChoice componentInChildren = transform.GetComponentInChildren<createAvatarChoice>();
			if (jsonData.instance.CreateAvatarJsonData.HasField(componentInChildren.id.ToString()) && componentInChildren.id > 5 && (int)jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["fenLeiGuanLian"].n == PageIndex - 1 && transform.GetComponentInChildren<UIToggle>().value)
			{
				text = text + Tools.Code64(jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["Info"].str) + "\n";
			}
		}
		text += Tools.Code64(jsonobject["Info"].str);
		this.MiaoShu.text = text;
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x000E58DC File Offset: 0x000E3ADC
	public void showPage(int PageIndex)
	{
		List<JSONObject> list = jsonData.instance.CreateAvatarJsonData.list.FindAll((JSONObject aa) => (int)aa["fenLeiGuanLian"].n == PageIndex);
		foreach (object obj in this.lightGrid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == string.Concat(PageIndex))
			{
				transform.GetComponent<UITexture>().mainTexture = this.nowPageTexture;
			}
			else
			{
				transform.GetComponent<UITexture>().mainTexture = this.EndPageTexture;
			}
		}
		this.setText(PageIndex);
		if (PageIndex == 5)
		{
			this.scrolw.SetActive(false);
			this.shenyutianfu.SetActive(false);
			this.LingGengBiaoGe.SetActive(true);
			CreateAvatarMag.inst.lingenUI.resetLinGen();
		}
		else
		{
			this.scrolw.SetActive(true);
			this.shenyutianfu.SetActive(true);
			this.LingGengBiaoGe.SetActive(false);
		}
		foreach (object obj2 in this.grid.transform)
		{
			Transform transform2 = (Transform)obj2;
			createAvatarChoice choice = transform2.GetComponentInChildren<createAvatarChoice>();
			if (list.Find((JSONObject aa) => aa["id"].I == choice.id))
			{
				transform2.gameObject.SetActive(true);
			}
			else
			{
				transform2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x000E5AA8 File Offset: 0x000E3CA8
	public void resteBar()
	{
		UIScrollView component = this.scrolw.GetComponent<UIScrollView>();
		component.UpdateScrollbars(true);
		component.verticalScrollBar.ForceUpdate();
		component.verticalScrollBar.value = 0.1f;
		component.Scroll(0.1f);
		component.UpdatePosition();
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x00004095 File Offset: 0x00002295
	public void createNode()
	{
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000E5AE7 File Offset: 0x000E3CE7
	private void Update()
	{
		this.tianfudian.text = string.Concat(this.TianFuDian);
	}

	// Token: 0x04001A70 RID: 6768
	public UILabel tianfudian;

	// Token: 0x04001A71 RID: 6769
	public GameObject TempObj;

	// Token: 0x04001A72 RID: 6770
	public GameObject grid;

	// Token: 0x04001A73 RID: 6771
	public int nowPage = 1;

	// Token: 0x04001A74 RID: 6772
	public int MaxPage = 8;

	// Token: 0x04001A75 RID: 6773
	public GameObject LingGengBiaoGe;

	// Token: 0x04001A76 RID: 6774
	public GameObject scrolw;

	// Token: 0x04001A77 RID: 6775
	public Texture nowPageTexture;

	// Token: 0x04001A78 RID: 6776
	public Texture EndPageTexture;

	// Token: 0x04001A79 RID: 6777
	public GameObject lightGrid;

	// Token: 0x04001A7A RID: 6778
	public GameObject shenyutianfu;

	// Token: 0x04001A7B RID: 6779
	public UIScrollBar uIScrollBar;

	// Token: 0x04001A7C RID: 6780
	public UILabel MiaoShu;

	// Token: 0x04001A7D RID: 6781
	public UILabel Title;

	// Token: 0x04001A7E RID: 6782
	private int _TianFuDian;

	// Token: 0x04001A7F RID: 6783
	private Dictionary<int, GameObject> NanduDict = new Dictionary<int, GameObject>();
}
