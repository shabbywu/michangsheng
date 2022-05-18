using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000590 RID: 1424
public class createTianfu : MonoBehaviour
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06002417 RID: 9239 RVA: 0x00127234 File Offset: 0x00125434
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

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06002418 RID: 9240 RVA: 0x001272AC File Offset: 0x001254AC
	// (set) Token: 0x06002419 RID: 9241 RVA: 0x0001D1CC File Offset: 0x0001B3CC
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

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x0600241A RID: 9242 RVA: 0x00127328 File Offset: 0x00125528
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

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x0600241B RID: 9243 RVA: 0x00127384 File Offset: 0x00125584
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

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x0600241C RID: 9244 RVA: 0x001273E0 File Offset: 0x001255E0
	public int LinGengZiZhi
	{
		get
		{
			int result = 4;
			foreach (JSONObject jsonobject in jsonData.instance.LinGenZiZhiJsonData.list)
			{
				if (this.ZiZhi >= (int)jsonobject["qujian"].n)
				{
					result = (int)jsonobject["id"].n;
					break;
				}
			}
			return result;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x0600241D RID: 9245 RVA: 0x00127468 File Offset: 0x00125668
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

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x0600241E RID: 9246 RVA: 0x001274C4 File Offset: 0x001256C4
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

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x0600241F RID: 9247 RVA: 0x00127520 File Offset: 0x00125720
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

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06002420 RID: 9248 RVA: 0x00127558 File Offset: 0x00125758
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

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06002421 RID: 9249 RVA: 0x001275B4 File Offset: 0x001257B4
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

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06002422 RID: 9250 RVA: 0x00127610 File Offset: 0x00125810
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

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06002423 RID: 9251 RVA: 0x0012766C File Offset: 0x0012586C
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

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06002424 RID: 9252 RVA: 0x001276A8 File Offset: 0x001258A8
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

	// Token: 0x06002425 RID: 9253 RVA: 0x001276E4 File Offset: 0x001258E4
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
			componentInChildren.id = (int)jsonobject["id"].n;
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

	// Token: 0x06002426 RID: 9254 RVA: 0x00127A5C File Offset: 0x00125C5C
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

	// Token: 0x06002427 RID: 9255 RVA: 0x00127BCC File Offset: 0x00125DCC
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
			if (list.Find((JSONObject aa) => (int)aa["id"].n == choice.id))
			{
				transform2.gameObject.SetActive(true);
			}
			else
			{
				transform2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x0001D1D5 File Offset: 0x0001B3D5
	public void resteBar()
	{
		UIScrollView component = this.scrolw.GetComponent<UIScrollView>();
		component.UpdateScrollbars(true);
		component.verticalScrollBar.ForceUpdate();
		component.verticalScrollBar.value = 0.1f;
		component.Scroll(0.1f);
		component.UpdatePosition();
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x000042DD File Offset: 0x000024DD
	public void createNode()
	{
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x0001D214 File Offset: 0x0001B414
	private void Update()
	{
		this.tianfudian.text = string.Concat(this.TianFuDian);
	}

	// Token: 0x04001F18 RID: 7960
	public UILabel tianfudian;

	// Token: 0x04001F19 RID: 7961
	public GameObject TempObj;

	// Token: 0x04001F1A RID: 7962
	public GameObject grid;

	// Token: 0x04001F1B RID: 7963
	public int nowPage = 1;

	// Token: 0x04001F1C RID: 7964
	public int MaxPage = 8;

	// Token: 0x04001F1D RID: 7965
	public GameObject LingGengBiaoGe;

	// Token: 0x04001F1E RID: 7966
	public GameObject scrolw;

	// Token: 0x04001F1F RID: 7967
	public Texture nowPageTexture;

	// Token: 0x04001F20 RID: 7968
	public Texture EndPageTexture;

	// Token: 0x04001F21 RID: 7969
	public GameObject lightGrid;

	// Token: 0x04001F22 RID: 7970
	public GameObject shenyutianfu;

	// Token: 0x04001F23 RID: 7971
	public UIScrollBar uIScrollBar;

	// Token: 0x04001F24 RID: 7972
	public UILabel MiaoShu;

	// Token: 0x04001F25 RID: 7973
	public UILabel Title;

	// Token: 0x04001F26 RID: 7974
	private int _TianFuDian;

	// Token: 0x04001F27 RID: 7975
	private Dictionary<int, GameObject> NanduDict = new Dictionary<int, GameObject>();
}
