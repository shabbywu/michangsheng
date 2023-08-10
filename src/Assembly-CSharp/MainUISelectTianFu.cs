using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUISelectTianFu : MonoBehaviour
{
	public Dictionary<int, List<int>> hasSelectSeidList = new Dictionary<int, List<int>>();

	public Dictionary<int, MainUITianFuCell> hasSelectList = new Dictionary<int, MainUITianFuCell>();

	public Dictionary<int, List<MainUITianFuCell>> tianFuPageList = new Dictionary<int, List<MainUITianFuCell>>();

	[SerializeField]
	private GameObject tianFuCell;

	[SerializeField]
	private MainUISetLinGen setLingGen;

	[SerializeField]
	private GameObject shenYuNum;

	[SerializeField]
	private GameObject finallyPage;

	[SerializeField]
	private Transform tianFuList;

	[SerializeField]
	private GameObject man;

	[SerializeField]
	private GameObject woman;

	[SerializeField]
	private Text tianfuNum;

	[SerializeField]
	private Text title;

	[SerializeField]
	private Text desc;

	[SerializeField]
	private Text finallyDesc;

	[SerializeField]
	private GameObject nextBtn;

	private bool isInit;

	public int tianfuDian;

	public int curPage = 1;

	public void Init()
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		if (!isInit)
		{
			tianfuNum.text = "0";
			foreach (JSONObject item in jsonData.instance.CreateAvatarJsonData.list)
			{
				MainUITianFuCell tianfuCell = Object.Instantiate<GameObject>(tianFuCell, tianFuList).GetComponent<MainUITianFuCell>();
				tianfuCell.Init(item);
				if (tianFuPageList.ContainsKey(tianfuCell.page))
				{
					tianFuPageList[tianfuCell.page].Add(tianfuCell);
				}
				else
				{
					tianFuPageList.Add(tianfuCell.page, new List<MainUITianFuCell> { tianfuCell });
				}
				tianfuCell.toggle.valueChange.AddListener((UnityAction)delegate
				{
					if (tianfuCell.toggle.isOn)
					{
						if (hasSelectSeidList.ContainsKey(20))
						{
							int num = -1;
							foreach (int item2 in hasSelectSeidList[20])
							{
								if (jsonData.instance.CrateAvatarSeidJsonData[20][item2.ToString()]["value1"].ToList().Contains(tianfuCell.toggle.group))
								{
									num = item2;
									break;
								}
							}
							if (num > 0)
							{
								hasSelectList[num].toggle.isOn = false;
								hasSelectList[num].toggle.OnValueChange();
							}
						}
						foreach (int seid in tianfuCell.seidList)
						{
							if (hasSelectSeidList.ContainsKey(seid))
							{
								hasSelectSeidList[seid].Add(tianfuCell.id);
							}
							else
							{
								hasSelectSeidList.Add(seid, new List<int> { tianfuCell.id });
							}
						}
						hasSelectList.Add(tianfuCell.id, tianfuCell);
						AddTianFuDian(-tianfuCell.costNum);
						if (tianfuCell.seidList.Contains(19))
						{
							AddTianFuDian(jsonData.instance.CrateAvatarSeidJsonData[19][tianfuCell.id.ToString()]["value1"].I);
						}
						if (tianfuCell.seidList.Contains(20))
						{
							List<int> list = jsonData.instance.CrateAvatarSeidJsonData[20][tianfuCell.id.ToString()]["value1"].ToList();
							List<MainUITianFuCell> list2 = new List<MainUITianFuCell>();
							foreach (int key in hasSelectList.Keys)
							{
								if (list.Contains(hasSelectList[key].toggle.group))
								{
									list2.Add(hasSelectList[key]);
								}
							}
							for (int i = 0; i < list2.Count; i++)
							{
								list2[i].toggle.isOn = false;
								list2[i].toggle.OnValueChange();
							}
						}
						return;
					}
					bool flag = false;
					foreach (int seid2 in tianfuCell.seidList)
					{
						if (hasSelectSeidList.ContainsKey(seid2))
						{
							if (hasSelectSeidList[seid2].Contains(tianfuCell.id))
							{
								hasSelectSeidList[seid2].Remove(tianfuCell.id);
								if (!flag)
								{
									AddTianFuDian(tianfuCell.costNum);
									if (tianfuCell.seidList.Contains(19))
									{
										AddTianFuDian(-jsonData.instance.CrateAvatarSeidJsonData[19][tianfuCell.id.ToString()]["value1"].I);
									}
									hasSelectList.Remove(tianfuCell.id);
									flag = true;
								}
							}
							if (hasSelectSeidList[seid2].Count == 0)
							{
								hasSelectSeidList.Remove(seid2);
							}
						}
					}
				});
				tianfuCell.toggle.clickEvent.AddListener(new UnityAction(MainUIPlayerInfo.inst.UpdataBase));
			}
			CostSort();
			isInit = true;
		}
		if (MainUIPlayerInfo.inst.sex == 1)
		{
			man.SetActive(true);
			woman.SetActive(false);
		}
		else if (MainUIPlayerInfo.inst.sex == 2)
		{
			man.SetActive(false);
			woman.SetActive(true);
		}
		ShowCurPageList();
		((Component)this).gameObject.SetActive(true);
	}

	public void ClickCell(bool isOn)
	{
	}

	private void CostSort()
	{
		foreach (int key in tianFuPageList.Keys)
		{
			if (key > 2)
			{
				tianFuPageList[key].Sort((MainUITianFuCell x, MainUITianFuCell y) => x.costNum.CompareTo(y.costNum));
			}
		}
	}

	public void ShowCurPageList()
	{
		UpdateDesc();
		foreach (MainUITianFuCell item in tianFuPageList[curPage])
		{
			((Component)item).transform.SetAsLastSibling();
			((Component)item).gameObject.SetActive(true);
		}
	}

	public void NextPage()
	{
		if (!CheckCurHasSelect())
		{
			UIPopTip.Inst.Pop("至少选择一个天赋");
			return;
		}
		if (setLingGen.CheckLingGen())
		{
			UIPopTip.Inst.Pop("请选择对应数目的灵根");
			return;
		}
		if (curPage == 8)
		{
			if (tianfuDian < 0)
			{
				UIPopTip.Inst.Pop("天赋点不能为负数");
				return;
			}
			HideCurPage();
			curPage++;
			ShowFinallyPage();
			return;
		}
		HideCurPage();
		curPage++;
		if (curPage == 5)
		{
			UpdateDesc();
			shenYuNum.SetActive(false);
			setLingGen.Init();
			return;
		}
		if (!shenYuNum.activeSelf)
		{
			shenYuNum.SetActive(true);
			((Component)setLingGen).gameObject.SetActive(false);
		}
		ShowCurPageList();
	}

	public void LastPage()
	{
		if (curPage == 9)
		{
			nextBtn.SetActive(true);
			finallyPage.SetActive(false);
		}
		HideCurPage();
		curPage--;
		if (curPage == 5)
		{
			UpdateDesc();
			shenYuNum.SetActive(false);
			setLingGen.Init();
			return;
		}
		if (curPage == 0)
		{
			curPage = 1;
			((Component)this).gameObject.SetActive(false);
			MainUIMag.inst.createAvatarPanel.setFacePanel.Init();
			return;
		}
		if (!shenYuNum.activeSelf)
		{
			shenYuNum.SetActive(true);
			((Component)setLingGen).gameObject.SetActive(false);
		}
		ShowCurPageList();
	}

	public void HideCurPage()
	{
		if (!tianFuPageList.ContainsKey(curPage))
		{
			return;
		}
		foreach (MainUITianFuCell item in tianFuPageList[curPage])
		{
			((Component)item).gameObject.SetActive(false);
		}
	}

	public bool CheckCurHasSelect()
	{
		if (tianFuPageList.ContainsKey(curPage))
		{
			foreach (MainUITianFuCell item in tianFuPageList[curPage])
			{
				if (item.toggle.isOn)
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	public void UpdateDesc()
	{
		title.text = CreateAvatarMiaoShu.DataDict[curPage].title;
		int num = curPage - 1;
		desc.text = "";
		if (num > 1 && num != 5)
		{
			foreach (int key in hasSelectList.Keys)
			{
				if (hasSelectList[key].page == num)
				{
					Text obj = desc;
					obj.text = obj.text + jsonData.instance.CreateAvatarJsonData[hasSelectList[key].id.ToString()]["Info"].Str + "\n";
				}
			}
		}
		Text obj2 = desc;
		obj2.text = obj2.text + CreateAvatarMiaoShu.DataDict[curPage].Info + "\n";
	}

	public void AddTianFuDian(int num)
	{
		tianfuDian += num;
		tianfuNum.text = tianfuDian.ToString();
	}

	private void ShowFinallyPage()
	{
		title.text = "经历";
		desc.text = "";
		shenYuNum.SetActive(false);
		nextBtn.SetActive(false);
		finallyDesc.text = "\n";
		List<int> list = new List<int>();
		foreach (int key in hasSelectList.Keys)
		{
			list.Add(key);
		}
		list.Sort((int x, int y) => x.CompareTo(y));
		foreach (int item in list)
		{
			if (hasSelectList[item].page != 1)
			{
				Text obj = finallyDesc;
				obj.text = obj.text + jsonData.instance.CreateAvatarJsonData[hasSelectList[item].id.ToString()]["Info"].Str + "\n\n";
			}
		}
		Text obj2 = finallyDesc;
		obj2.text += "十六岁那年，你意外捡到了一把满是锈迹的钝剑，无意间唤醒了其中沉睡的老者灵魂。在老者的指引下，长生之途的大门缓缓为你敞开——\n";
		finallyPage.SetActive(true);
	}
}
