using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class wuDaoUICell : MonoBehaviour
{
	public int ID;

	public Image bg;

	public Image icon;

	public Text castNum;

	public Text wuDaoName;

	public Vector3 postion;

	public JSONObject WuDaoJson => jsonData.instance.WuDaoJson[ID.ToString()];

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		postion = ((Component)this).transform.position;
	}

	public void Click()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		WuDaoUIMag.inst.wuDaoCellTooltip.open(ID, icon);
		WuDaoUIMag.inst.wuDaoCellTooltip.action = (UnityAction)delegate
		{
			if (CanStudyWuDao())
			{
				studyWuDao();
				WuDaoUIMag.inst.upWuDaoDate();
			}
			else if (IsStudy())
			{
				UIPopTip.Inst.Pop("已经领悟过该大道");
			}
			else
			{
				UIPopTip.Inst.Pop("未达到领悟条件");
			}
			WuDaoUIMag.inst.ResetCellButton();
		};
	}

	public void studyWuDao()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = WuDaoJson;
		foreach (JSONObject item in wuDaoJson["Type"].list)
		{
			player.wuDaoMag.addWuDaoSkill(item.I, wuDaoJson["id"].I);
		}
	}

	public bool CanStudyWuDao()
	{
		JSONObject wuDaoJson = WuDaoJson;
		Tools.instance.getPlayer();
		if (IsStudy())
		{
			return false;
		}
		bool flag = true;
		foreach (JSONObject item in wuDaoJson["Type"].list)
		{
			if (!CanEx(item.I))
			{
				return false;
			}
			if (CanLastWuDao(item.I))
			{
				flag = false;
			}
		}
		if (flag)
		{
			return false;
		}
		if (!CanWuDaoDian())
		{
			return false;
		}
		return true;
	}

	public bool IsStudy()
	{
		foreach (SkillItem allWuDaoSkill in Tools.instance.getPlayer().wuDaoMag.GetAllWuDaoSkills())
		{
			if (allWuDaoSkill.itemId == ID)
			{
				return true;
			}
		}
		return false;
	}

	public bool CanEx(int WuDaoType)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(WuDaoType);
		int i = WuDaoJson["Lv"].I;
		if (wuDaoLevelByType >= i)
		{
			return true;
		}
		return false;
	}

	public bool CanWuDaoDian()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = WuDaoJson;
		if (player.wuDaoMag.GetNowWuDaoDian() >= wuDaoJson["Cast"].I)
		{
			return true;
		}
		return false;
	}

	public bool CanLastWuDao(int wudaoType)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = WuDaoJson;
		JSONObject wuDaoStudy = player.wuDaoMag.getWuDaoStudy(wudaoType);
		int i = wuDaoJson["Lv"].I;
		if (i == 1)
		{
			return true;
		}
		Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
		for (int j = 1; j < i; j++)
		{
			dictionary[j] = false;
		}
		foreach (JSONObject item in wuDaoStudy.list)
		{
			JSONObject jSONObject = jsonData.instance.WuDaoJson[item.I.ToString()];
			if (dictionary.ContainsKey(jSONObject["Lv"].I) && !dictionary[jSONObject["Lv"].I])
			{
				dictionary[jSONObject["Lv"].I] = true;
			}
		}
		foreach (KeyValuePair<int, bool> item2 in dictionary)
		{
			if (!item2.Value)
			{
				return false;
			}
		}
		return true;
	}

	private void Update()
	{
	}
}
