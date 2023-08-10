using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab;

[Serializable]
public class TabWuDaoLevelBase : UIBase
{
	private int _id;

	private int _level;

	private GameObject _active;

	private GameObject _noActive;

	private Image _cloud;

	public TabWuDaoLevelBase(GameObject go, int id, int level)
	{
		try
		{
			_go = go;
			_active = Get("Active");
			_noActive = Get("NoActive");
			_level = level;
			Get<Text>("Active/Name").text = WuDaoJinJieJson.DataDict[level].Text;
			Get<Text>("NoActive/Name").text = WuDaoJinJieJson.DataDict[level].Text;
			Get<Text>("Active/Value").text = WuDaoJinJieJson.DataDict[level - 1].Max.ToString();
			Get<Text>("NoActive/Value").text = WuDaoJinJieJson.DataDict[level - 1].Max.ToString();
			_cloud = Get<Image>("Active/Cloud");
			UpdateUI(id);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void UpdateUI(int id)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		_id = id;
		bool flag = IsCanActive();
		if (flag)
		{
			_active.SetActive(true);
			_noActive.SetActive(false);
		}
		else
		{
			_active.SetActive(false);
			_noActive.SetActive(true);
		}
		if (_go.transform.childCount > 2)
		{
			Object.Destroy((Object)(object)((Component)_go.transform.GetChild(2)).gameObject);
		}
		List<int> wuDaoSkillCount = GetWuDaoSkillCount();
		int count = wuDaoSkillCount.Count;
		_cloud.sprite = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoBgImgDict[count.ToString()];
		GameObject val = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoSkillListDict[count].Inst(_go.transform);
		float y = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoSkillListDict[count].transform.position.y;
		val.transform.SetPostionY(y).SetLocalPositionX(0f);
		((Object)val).name = count.ToString();
		for (int i = 0; i < count; i++)
		{
			WuDaoJson wuDaoJson = WuDaoJson.DataDict[wuDaoSkillCount[i]];
			WuDaoSlot wuDaoSlot = new WuDaoSlot(((Component)val.transform.GetChild(i)).gameObject, wuDaoJson.id);
			if (flag)
			{
				if (Tools.instance.getPlayer().wuDaoMag.IsStudy(wuDaoJson.id))
				{
					wuDaoSlot.SetState(1);
				}
				else
				{
					wuDaoSlot.SetState(2);
				}
			}
			else
			{
				wuDaoSlot.SetState(3);
			}
		}
	}

	private List<int> GetWuDaoSkillCount()
	{
		List<int> list = new List<int>();
		foreach (WuDaoJson data in WuDaoJson.DataList)
		{
			if (data.Type.Contains(_id) && data.Lv == _level)
			{
				list.Add(data.id);
			}
		}
		return list;
	}

	private bool IsCanActive()
	{
		if (Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(_id) >= _level)
		{
			return true;
		}
		return false;
	}
}
