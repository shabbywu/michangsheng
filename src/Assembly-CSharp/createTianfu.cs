using System.Collections.Generic;
using UnityEngine;

public class createTianfu : MonoBehaviour
{
	public UILabel tianfudian;

	public GameObject TempObj;

	public GameObject grid;

	public int nowPage = 1;

	public int MaxPage = 8;

	public GameObject LingGengBiaoGe;

	public GameObject scrolw;

	public Texture nowPageTexture;

	public Texture EndPageTexture;

	public GameObject lightGrid;

	public GameObject shenyutianfu;

	public UIScrollBar uIScrollBar;

	public UILabel MiaoShu;

	public UILabel Title;

	private int _TianFuDian;

	private Dictionary<int, GameObject> NanduDict = new Dictionary<int, GameObject>();

	public List<createAvatarChoice> getSelectChoice
	{
		get
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			List<createAvatarChoice> list = new List<createAvatarChoice>();
			foreach (Transform item in grid.transform)
			{
				Transform val = item;
				if (((Component)val).GetComponentInChildren<UIToggle>().value)
				{
					list.Add(((Component)val).GetComponentInChildren<createAvatarChoice>());
				}
			}
			return list;
		}
	}

	public int TianFuDian
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			int num = 0;
			foreach (Transform item in grid.transform)
			{
				createAvatarChoice componentInChildren = ((Component)item).GetComponentInChildren<createAvatarChoice>();
				if (((Component)item).GetComponentInChildren<UIToggle>().value)
				{
					num += componentInChildren.cast;
				}
			}
			return _TianFuDian - num;
		}
		set
		{
			_TianFuDian = value;
		}
	}

	public int ZiZhi
	{
		get
		{
			int addzizhi = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getSeidValue1();
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["ziZhi"].n + addzizhi;
		}
	}

	public int Money
	{
		get
		{
			int _money = jsonData.instance.AvatarJsonData["1"]["MoneyType"].I;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				_money += aa.getValue(14);
			});
			return _money;
		}
	}

	public int LinGengZiZhi
	{
		get
		{
			int result = 4;
			foreach (JSONObject item in jsonData.instance.LinGenZiZhiJsonData.list)
			{
				if (ZiZhi >= (int)item["qujian"].n)
				{
					result = item["id"].I;
					break;
				}
			}
			return result;
		}
	}

	public int DunSu
	{
		get
		{
			int addNum = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(7);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["dunSu"].n + addNum;
		}
	}

	public int HP_Max
	{
		get
		{
			int addNum = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(8);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["HP"].n + addNum;
		}
	}

	public int XinJin
	{
		get
		{
			int addzizhi = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getValue(4);
			});
			return addzizhi;
		}
	}

	public int WuXin
	{
		get
		{
			int addzizhi = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getSeidValue2();
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["wuXin"].n + addzizhi;
		}
	}

	public int ShowYuan
	{
		get
		{
			int addzizhi = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addzizhi += aa.getValue(5);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["shouYuan"].n + addzizhi;
		}
	}

	public int ShenShi
	{
		get
		{
			int addNum = 0;
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				addNum += aa.getValue(6);
			});
			return (int)jsonData.instance.AvatarJsonData["1"]["shengShi"].n + addNum;
		}
	}

	public List<int> Items
	{
		get
		{
			List<int> addNum = new List<int>();
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				aa.getSeidValue10()?.ForEach(delegate(JSONObject ii)
				{
					addNum.Add((int)ii.n);
				});
			});
			return addNum;
		}
	}

	public List<int> StaticSkill
	{
		get
		{
			List<int> addNum = new List<int>();
			getSelectChoice.ForEach(delegate(createAvatarChoice aa)
			{
				aa.getSeidValue9()?.ForEach(delegate(JSONObject ii)
				{
					addNum.Add((int)ii.n);
				});
			});
			return addNum;
		}
	}

	private void Awake()
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		LingGengBiaoGe.SetActive(false);
		lightGrid.GetComponent<UIGrid>().repositionNow = true;
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject item in jsonData.instance.CreateAvatarJsonData.list)
		{
			list.Add(item);
		}
		list.Sort((JSONObject aa, JSONObject bb) => aa["feiYong"].n.CompareTo(bb["feiYong"].n));
		NanduDict.Clear();
		foreach (JSONObject item2 in list)
		{
			GameObject val = Object.Instantiate<GameObject>(TempObj);
			val.transform.SetParent(grid.transform);
			val.transform.localScale = Vector3.one;
			val.transform.position = Vector3.zero;
			val.SetActive(true);
			createAvatarChoice componentInChildren = val.GetComponentInChildren<createAvatarChoice>();
			componentInChildren.Title.text = Tools.instance.Code64ToString(item2["Title"].str);
			componentInChildren.desc = Tools.instance.Code64ToString(item2["Desc"].str);
			componentInChildren.descInfo = Tools.instance.Code64ToString(item2["Info"].str);
			componentInChildren.cast = (int)item2["feiYong"].n;
			componentInChildren.id = item2["id"].I;
			foreach (JSONObject item3 in item2["seid"].list)
			{
				componentInChildren.seid.Add((int)item3.n);
			}
			if (componentInChildren.id >= 1 && componentInChildren.id <= 5)
			{
				NanduDict.Add(componentInChildren.id, val);
			}
			UIToggle componentInChildren2 = val.GetComponentInChildren<UIToggle>();
			componentInChildren2.optionCanBeNone = true;
			componentInChildren2.group = (int)item2["fenZu"].n;
			if (item2["jiesuo"].I > CreateAvatarMag.inst.maxLevel)
			{
				componentInChildren.isLock = true;
				componentInChildren.LockMessager = Tools.Code64("剧情模式境界达到" + jsonData.instance.LevelUpDataJsonData[item2["jiesuo"].I.ToString()]["Name"].str + "解锁");
				((Behaviour)((Component)componentInChildren).GetComponent<UIButton>()).enabled = false;
				((Behaviour)((Component)componentInChildren).GetComponent<UIToggle>()).enabled = false;
				((Component)componentInChildren).GetComponent<UIButton>().normalSprite2D = CreateAvatarMag.inst.LockImage;
			}
		}
		for (int num = 5; num >= 1; num--)
		{
			NanduDict[num].transform.SetAsLastSibling();
		}
	}

	public void setText(int PageIndex)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		JSONObject jSONObject = jsonData.instance.CreateAvatarMiaoShu[PageIndex.ToString()];
		Title.text = Tools.Code64(jSONObject["title"].str);
		string text = "";
		foreach (Transform item in grid.transform)
		{
			Transform val = item;
			createAvatarChoice componentInChildren = ((Component)val).GetComponentInChildren<createAvatarChoice>();
			if (jsonData.instance.CreateAvatarJsonData.HasField(componentInChildren.id.ToString()) && componentInChildren.id > 5 && (int)jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["fenLeiGuanLian"].n == PageIndex - 1 && ((Component)val).GetComponentInChildren<UIToggle>().value)
			{
				text = text + Tools.Code64(jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["Info"].str) + "\n";
			}
		}
		text += Tools.Code64(jSONObject["Info"].str);
		MiaoShu.text = text;
	}

	public void showPage(int PageIndex)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		List<JSONObject> list = jsonData.instance.CreateAvatarJsonData.list.FindAll((JSONObject aa) => (int)aa["fenLeiGuanLian"].n == PageIndex);
		foreach (Transform item in lightGrid.transform)
		{
			Transform val = item;
			if (((Object)val).name == string.Concat(PageIndex))
			{
				((Component)val).GetComponent<UITexture>().mainTexture = nowPageTexture;
			}
			else
			{
				((Component)val).GetComponent<UITexture>().mainTexture = EndPageTexture;
			}
		}
		setText(PageIndex);
		if (PageIndex == 5)
		{
			scrolw.SetActive(false);
			shenyutianfu.SetActive(false);
			LingGengBiaoGe.SetActive(true);
			CreateAvatarMag.inst.lingenUI.resetLinGen();
		}
		else
		{
			scrolw.SetActive(true);
			shenyutianfu.SetActive(true);
			LingGengBiaoGe.SetActive(false);
		}
		foreach (Transform item2 in grid.transform)
		{
			Transform val2 = item2;
			createAvatarChoice choice = ((Component)val2).GetComponentInChildren<createAvatarChoice>();
			if ((bool)list.Find((JSONObject aa) => aa["id"].I == choice.id))
			{
				((Component)val2).gameObject.SetActive(true);
			}
			else
			{
				((Component)val2).gameObject.SetActive(false);
			}
		}
	}

	public void resteBar()
	{
		UIScrollView component = scrolw.GetComponent<UIScrollView>();
		component.UpdateScrollbars(recalculateBounds: true);
		component.verticalScrollBar.ForceUpdate();
		component.verticalScrollBar.value = 0.1f;
		component.Scroll(0.1f);
		component.UpdatePosition();
	}

	private void Start()
	{
	}

	public void createNode()
	{
	}

	private void Update()
	{
		tianfudian.text = string.Concat(TianFuDian);
	}
}
