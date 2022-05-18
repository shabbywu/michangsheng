using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000490 RID: 1168
public class MainUISelectTianFu : MonoBehaviour
{
	// Token: 0x06001F2B RID: 7979 RVA: 0x0010CB88 File Offset: 0x0010AD88
	public void Init()
	{
		if (!this.isInit)
		{
			this.tianfuNum.text = "0";
			foreach (JSONObject json in jsonData.instance.CreateAvatarJsonData.list)
			{
				MainUITianFuCell tianfuCell = Object.Instantiate<GameObject>(this.tianFuCell, this.tianFuList).GetComponent<MainUITianFuCell>();
				tianfuCell.Init(json);
				if (this.tianFuPageList.ContainsKey(tianfuCell.page))
				{
					this.tianFuPageList[tianfuCell.page].Add(tianfuCell);
				}
				else
				{
					this.tianFuPageList.Add(tianfuCell.page, new List<MainUITianFuCell>
					{
						tianfuCell
					});
				}
				tianfuCell.toggle.valueChange.AddListener(delegate()
				{
					if (tianfuCell.toggle.isOn)
					{
						if (this.hasSelectSeidList.ContainsKey(20))
						{
							int num = -1;
							foreach (int num2 in this.hasSelectSeidList[20])
							{
								if (jsonData.instance.CrateAvatarSeidJsonData[20][num2.ToString()]["value1"].ToList().Contains(tianfuCell.toggle.group))
								{
									num = num2;
									break;
								}
							}
							if (num > 0)
							{
								this.hasSelectList[num].toggle.isOn = false;
								this.hasSelectList[num].toggle.OnValueChange();
							}
						}
						foreach (int key in tianfuCell.seidList)
						{
							if (this.hasSelectSeidList.ContainsKey(key))
							{
								this.hasSelectSeidList[key].Add(tianfuCell.id);
							}
							else
							{
								this.hasSelectSeidList.Add(key, new List<int>
								{
									tianfuCell.id
								});
							}
						}
						this.hasSelectList.Add(tianfuCell.id, tianfuCell);
						this.AddTianFuDian(-tianfuCell.costNum);
						if (tianfuCell.seidList.Contains(19))
						{
							this.AddTianFuDian(jsonData.instance.CrateAvatarSeidJsonData[19][tianfuCell.id.ToString()]["value1"].I);
						}
						if (tianfuCell.seidList.Contains(20))
						{
							List<int> list = jsonData.instance.CrateAvatarSeidJsonData[20][tianfuCell.id.ToString()]["value1"].ToList();
							List<MainUITianFuCell> list2 = new List<MainUITianFuCell>();
							foreach (int key2 in this.hasSelectList.Keys)
							{
								if (list.Contains(this.hasSelectList[key2].toggle.group))
								{
									list2.Add(this.hasSelectList[key2]);
								}
							}
							for (int i = 0; i < list2.Count; i++)
							{
								list2[i].toggle.isOn = false;
								list2[i].toggle.OnValueChange();
							}
							return;
						}
					}
					else
					{
						bool flag = false;
						foreach (int key3 in tianfuCell.seidList)
						{
							if (this.hasSelectSeidList.ContainsKey(key3))
							{
								if (this.hasSelectSeidList[key3].Contains(tianfuCell.id))
								{
									this.hasSelectSeidList[key3].Remove(tianfuCell.id);
									if (!flag)
									{
										this.AddTianFuDian(tianfuCell.costNum);
										if (tianfuCell.seidList.Contains(19))
										{
											this.AddTianFuDian(-jsonData.instance.CrateAvatarSeidJsonData[19][tianfuCell.id.ToString()]["value1"].I);
										}
										this.hasSelectList.Remove(tianfuCell.id);
										flag = true;
									}
								}
								if (this.hasSelectSeidList[key3].Count == 0)
								{
									this.hasSelectSeidList.Remove(key3);
								}
							}
						}
					}
				});
				tianfuCell.toggle.clickEvent.AddListener(new UnityAction(MainUIPlayerInfo.inst.UpdataBase));
			}
			this.CostSort();
			this.isInit = true;
		}
		if (MainUIPlayerInfo.inst.sex == 1)
		{
			this.man.SetActive(true);
			this.woman.SetActive(false);
		}
		else if (MainUIPlayerInfo.inst.sex == 2)
		{
			this.man.SetActive(false);
			this.woman.SetActive(true);
		}
		this.ShowCurPageList();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x000042DD File Offset: 0x000024DD
	public void ClickCell(bool isOn)
	{
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x0010CD48 File Offset: 0x0010AF48
	private void CostSort()
	{
		foreach (int num in this.tianFuPageList.Keys)
		{
			if (num > 2)
			{
				this.tianFuPageList[num].Sort((MainUITianFuCell x, MainUITianFuCell y) => x.costNum.CompareTo(y.costNum));
			}
		}
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0010CDD0 File Offset: 0x0010AFD0
	public void ShowCurPageList()
	{
		this.UpdateDesc();
		foreach (MainUITianFuCell mainUITianFuCell in this.tianFuPageList[this.curPage])
		{
			mainUITianFuCell.transform.SetAsLastSibling();
			mainUITianFuCell.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x0010CE44 File Offset: 0x0010B044
	public void NextPage()
	{
		if (!this.CheckCurHasSelect())
		{
			UIPopTip.Inst.Pop("至少选择一个天赋", PopTipIconType.叹号);
			return;
		}
		if (this.setLingGen.CheckLingGen())
		{
			UIPopTip.Inst.Pop("请选择对应数目的灵根", PopTipIconType.叹号);
			return;
		}
		if (this.curPage == 8)
		{
			if (this.tianfuDian < 0)
			{
				UIPopTip.Inst.Pop("天赋点不能为负数", PopTipIconType.叹号);
				return;
			}
			this.HideCurPage();
			this.curPage++;
			this.ShowFinallyPage();
			return;
		}
		else
		{
			this.HideCurPage();
			this.curPage++;
			if (this.curPage == 5)
			{
				this.UpdateDesc();
				this.shenYuNum.SetActive(false);
				this.setLingGen.Init();
				return;
			}
			if (!this.shenYuNum.activeSelf)
			{
				this.shenYuNum.SetActive(true);
				this.setLingGen.gameObject.SetActive(false);
			}
			this.ShowCurPageList();
			return;
		}
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x0010CF34 File Offset: 0x0010B134
	public void LastPage()
	{
		if (this.curPage == 9)
		{
			this.nextBtn.SetActive(true);
			this.finallyPage.SetActive(false);
		}
		this.HideCurPage();
		this.curPage--;
		if (this.curPage == 5)
		{
			this.UpdateDesc();
			this.shenYuNum.SetActive(false);
			this.setLingGen.Init();
			return;
		}
		if (this.curPage == 0)
		{
			this.curPage = 1;
			base.gameObject.SetActive(false);
			MainUIMag.inst.createAvatarPanel.setFacePanel.Init();
			return;
		}
		if (!this.shenYuNum.activeSelf)
		{
			this.shenYuNum.SetActive(true);
			this.setLingGen.gameObject.SetActive(false);
		}
		this.ShowCurPageList();
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x0010D000 File Offset: 0x0010B200
	public void HideCurPage()
	{
		if (this.tianFuPageList.ContainsKey(this.curPage))
		{
			foreach (MainUITianFuCell mainUITianFuCell in this.tianFuPageList[this.curPage])
			{
				mainUITianFuCell.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x0010D074 File Offset: 0x0010B274
	public bool CheckCurHasSelect()
	{
		if (this.tianFuPageList.ContainsKey(this.curPage))
		{
			using (List<MainUITianFuCell>.Enumerator enumerator = this.tianFuPageList[this.curPage].GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.toggle.isOn)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}
		return true;
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x0010D0F4 File Offset: 0x0010B2F4
	public void UpdateDesc()
	{
		this.title.text = CreateAvatarMiaoShu.DataDict[this.curPage].title;
		int num = this.curPage - 1;
		this.desc.text = "";
		if (num > 1 && num != 5)
		{
			foreach (int key in this.hasSelectList.Keys)
			{
				if (this.hasSelectList[key].page == num)
				{
					Text text = this.desc;
					text.text = text.text + jsonData.instance.CreateAvatarJsonData[this.hasSelectList[key].id.ToString()]["Info"].Str + "\n";
				}
			}
		}
		Text text2 = this.desc;
		text2.text = text2.text + CreateAvatarMiaoShu.DataDict[this.curPage].Info + "\n";
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x00019C6D File Offset: 0x00017E6D
	public void AddTianFuDian(int num)
	{
		this.tianfuDian += num;
		this.tianfuNum.text = this.tianfuDian.ToString();
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x0010D220 File Offset: 0x0010B420
	private void ShowFinallyPage()
	{
		this.title.text = "经历";
		this.desc.text = "";
		this.shenYuNum.SetActive(false);
		this.nextBtn.SetActive(false);
		this.finallyDesc.text = "\n";
		List<int> list = new List<int>();
		foreach (int item in this.hasSelectList.Keys)
		{
			list.Add(item);
		}
		list.Sort((int x, int y) => x.CompareTo(y));
		foreach (int key in list)
		{
			if (this.hasSelectList[key].page != 1)
			{
				Text text = this.finallyDesc;
				text.text = text.text + jsonData.instance.CreateAvatarJsonData[this.hasSelectList[key].id.ToString()]["Info"].Str + "\n\n";
			}
		}
		Text text2 = this.finallyDesc;
		text2.text += "十六岁那年，你意外捡到了一把满是锈迹的钝剑，无意间唤醒了其中沉睡的老者灵魂。在老者的指引下，长生之途的大门缓缓为你敞开——\n";
		this.finallyPage.SetActive(true);
	}

	// Token: 0x04001AA7 RID: 6823
	public Dictionary<int, List<int>> hasSelectSeidList = new Dictionary<int, List<int>>();

	// Token: 0x04001AA8 RID: 6824
	public Dictionary<int, MainUITianFuCell> hasSelectList = new Dictionary<int, MainUITianFuCell>();

	// Token: 0x04001AA9 RID: 6825
	public Dictionary<int, List<MainUITianFuCell>> tianFuPageList = new Dictionary<int, List<MainUITianFuCell>>();

	// Token: 0x04001AAA RID: 6826
	[SerializeField]
	private GameObject tianFuCell;

	// Token: 0x04001AAB RID: 6827
	[SerializeField]
	private MainUISetLinGen setLingGen;

	// Token: 0x04001AAC RID: 6828
	[SerializeField]
	private GameObject shenYuNum;

	// Token: 0x04001AAD RID: 6829
	[SerializeField]
	private GameObject finallyPage;

	// Token: 0x04001AAE RID: 6830
	[SerializeField]
	private Transform tianFuList;

	// Token: 0x04001AAF RID: 6831
	[SerializeField]
	private GameObject man;

	// Token: 0x04001AB0 RID: 6832
	[SerializeField]
	private GameObject woman;

	// Token: 0x04001AB1 RID: 6833
	[SerializeField]
	private Text tianfuNum;

	// Token: 0x04001AB2 RID: 6834
	[SerializeField]
	private Text title;

	// Token: 0x04001AB3 RID: 6835
	[SerializeField]
	private Text desc;

	// Token: 0x04001AB4 RID: 6836
	[SerializeField]
	private Text finallyDesc;

	// Token: 0x04001AB5 RID: 6837
	[SerializeField]
	private GameObject nextBtn;

	// Token: 0x04001AB6 RID: 6838
	private bool isInit;

	// Token: 0x04001AB7 RID: 6839
	public int tianfuDian;

	// Token: 0x04001AB8 RID: 6840
	public int curPage = 1;
}
