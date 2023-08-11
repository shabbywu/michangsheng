using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUISetLinGen : MonoBehaviour
{
	public List<MainUILinGenCell> lingGensList;

	public List<MainUILinGenCell> hasSelectLinst;

	public List<Text> lingGenPrecentList;

	public Text tips;

	private bool isInit;

	public void Init()
	{
		if (!isInit)
		{
			for (int i = 0; i < lingGensList.Count; i++)
			{
				lingGensList[i].clickEvent = ClickMethod;
			}
			isInit = true;
		}
		tips.text = "共选择" + MainUIPlayerInfo.inst.linggenNum.ToCNNumber() + "个灵根";
		int num = MainUIPlayerInfo.inst.linggenNum - hasSelectLinst.Count;
		if (num > 0)
		{
			for (int j = 0; j < lingGensList.Count; j++)
			{
				if (!lingGensList[j].isOn)
				{
					lingGensList[j].OnPointerUp(null);
					num--;
				}
				if (num <= 0)
				{
					break;
				}
			}
		}
		else if (num < 0)
		{
			for (int num2 = hasSelectLinst.Count - 1; num2 >= 0; num2--)
			{
				hasSelectLinst[num2].OnPointerUp(null);
				num++;
				if (num >= 0)
				{
					break;
				}
			}
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void ClickMethod(int i)
	{
		if (lingGensList[i].isOn)
		{
			if (hasSelectLinst.Count < MainUIPlayerInfo.inst.linggenNum)
			{
				hasSelectLinst.Add(lingGensList[i]);
				MainUIPlayerInfo.inst.lingGenList[i] += 10;
			}
			else if (hasSelectLinst.Count == MainUIPlayerInfo.inst.linggenNum)
			{
				hasSelectLinst[0].isOn = false;
				hasSelectLinst[0].OnvalueChange();
				hasSelectLinst.Add(lingGensList[i]);
				MainUIPlayerInfo.inst.lingGenList[i] += 10;
			}
			else
			{
				lingGensList[i].isOn = false;
			}
		}
		else
		{
			hasSelectLinst.Remove(lingGensList[i]);
			MainUIPlayerInfo.inst.lingGenList[i] -= 10;
		}
		UpdataPrecent();
	}

	public bool CheckLingGen()
	{
		if (((Component)this).gameObject.activeSelf)
		{
			if (hasSelectLinst.Count != MainUIPlayerInfo.inst.linggenNum)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private void UpdataPrecent()
	{
		int num = MainUIPlayerInfo.inst.lingGenList[0] + MainUIPlayerInfo.inst.lingGenList[1] + MainUIPlayerInfo.inst.lingGenList[2] + MainUIPlayerInfo.inst.lingGenList[3] + MainUIPlayerInfo.inst.lingGenList[4];
		for (int i = 0; i < 5; i++)
		{
			lingGenPrecentList[i].text = MainUIPlayerInfo.inst.lingGenList[i] * 100 / num + "%";
		}
	}
}
