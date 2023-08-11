using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame.TuJian;

public class createAvatarChoice : MonoBehaviour
{
	public enum ChoiceID
	{
		Seid12 = 12,
		Seid13,
		Seid14,
		Seid15,
		Seid16,
		Seid17,
		Seid18,
		Seid19,
		Seid20,
		Seid21,
		Seid22,
		Seid23
	}

	public int id;

	public UILabel Title;

	public UILabel descUI;

	public string desc;

	public string descInfo;

	public string LockMessager;

	public List<int> seid = new List<int>();

	public int cast;

	public UILabel CastTianFu;

	public TooltipScale Tooltips;

	private createTianfu tianfuUI;

	public UI2DSprite Background;

	public bool isLock;

	private void Start()
	{
		tianfuUI = ((Component)((Component)this).transform.parent.parent.parent).GetComponent<createTianfu>();
	}

	private void Update()
	{
		CastTianFu.text = string.Concat((int)jsonData.instance.CreateAvatarJsonData[id.ToString()]["feiYong"].n);
	}

	protected void OnHover(bool isOver)
	{
		if (isOver)
		{
			Tooltips.uILabel.text = "";
			UILabel uILabel = Tooltips.uILabel;
			uILabel.text = uILabel.text + "[24a5d6]效果：[-]" + desc;
			UILabel uILabel2 = Tooltips.uILabel;
			uILabel2.text = uILabel2.text + "\n\n[db9a53]说明：[-]" + descInfo;
			if (isLock)
			{
				UILabel uILabel3 = Tooltips.uILabel;
				uILabel3.text = uILabel3.text + "\n\n[db9a53]解锁：[-]" + LockMessager;
			}
			Tooltips.showTooltip = true;
		}
		else
		{
			Tooltips.showTooltip = false;
		}
	}

	private void OnPress()
	{
		Tooltips.showTooltip = false;
	}

	public void setValue()
	{
		UIToggle toggle = ((Component)this).GetComponent<UIToggle>();
		((EventDelegate.Callback)delegate
		{
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Expected O, but got Unknown
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f7: Expected O, but got Unknown
			if (cast > 0 && toggle.value && CreateAvatarMag.inst.tianfuUI.TianFuDian < 0 && !seid.Contains(19))
			{
				toggle.value = false;
				UIPopTip.Inst.Pop("天赋点不足");
			}
			if (toggle.value && seid.Contains(19))
			{
				CreateAvatarMag.inst.tianfuUI.TianFuDian = getValue(19);
			}
			foreach (Transform item in CreateAvatarMag.inst.tianfuUI.grid.transform)
			{
				Transform val = item;
				createAvatarChoice componentInChildren = ((Component)val).GetComponentInChildren<createAvatarChoice>();
				UIToggle componentInChildren2 = ((Component)val).GetComponentInChildren<UIToggle>();
				if (componentInChildren2.value && componentInChildren.seid.Contains(20) && Tools.JsonListToList(jsonData.instance.CrateAvatarSeidJsonData[20][componentInChildren.id.ToString()]["value1"]).Contains(toggle.group) && componentInChildren2.value)
				{
					componentInChildren2.value = false;
					((Component)val.GetChild(0).Find("Background")).gameObject.SetActive(true);
				}
			}
			if (toggle.value && seid.Contains(20))
			{
				List<int> list = Tools.JsonListToList(jsonData.instance.CrateAvatarSeidJsonData[20][id.ToString()]["value1"]);
				foreach (Transform item2 in CreateAvatarMag.inst.tianfuUI.grid.transform)
				{
					Transform val2 = item2;
					UIToggle componentInChildren3 = ((Component)val2).GetComponentInChildren<UIToggle>();
					if (list.Contains(componentInChildren3.group) && componentInChildren3.value)
					{
						componentInChildren3.value = false;
						((Component)val2.GetChild(0).Find("Background")).gameObject.SetActive(true);
					}
				}
			}
		})();
	}

	public int getValue(int _seid)
	{
		if (seid.Contains(_seid))
		{
			return (int)jsonData.instance.CrateAvatarSeidJsonData[_seid][id.ToString()]["value1"].n;
		}
		return 0;
	}

	public int getSeidValue1()
	{
		return getValue(1);
	}

	public int getSeidValue2()
	{
		return getValue(2);
	}

	public int getSeidValue3()
	{
		return getValue(3);
	}

	public int getSeidValue4()
	{
		return getValue(4);
	}

	public List<JSONObject> getSeidValue9()
	{
		if (seid.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][id.ToString()]["value1"].list;
		}
		return null;
	}

	public List<JSONObject> getSeidValue10()
	{
		if (seid.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][id.ToString()]["value1"].list;
		}
		return null;
	}

	public List<int> getSeidValue11()
	{
		if (seid.Contains(11))
		{
			List<int> list = new List<int>();
			int item = (int)jsonData.instance.CrateAvatarSeidJsonData[11][id.ToString()]["value1"].n;
			int item2 = (int)jsonData.instance.CrateAvatarSeidJsonData[11][id.ToString()]["value2"].n;
			list.Add(item);
			list.Add(item2);
			return list;
		}
		return null;
	}

	public void PlayerSetSeid(int Seid)
	{
		Avatar player = Tools.instance.getPlayer();
		int value = getValue(Seid);
		if (!player.TianFuID.HasField(string.Concat(Seid)))
		{
			player.TianFuID.AddField(string.Concat(Seid), 0);
		}
		player.TianFuID.SetField(string.Concat(Seid), (int)player.TianFuID[string.Concat(Seid)].n + value);
	}

	public void realizedSeid12()
	{
		PlayerSetSeid(12);
	}

	public void realizedSeid13()
	{
		PlayerSetSeid(13);
	}

	public void realizedSeid15()
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)jsonData.instance.CrateAvatarSeidJsonData[15][id.ToString()]["value1"].n;
		int num2 = (int)jsonData.instance.CrateAvatarSeidJsonData[15][id.ToString()]["value2"].n;
		if (!player.TianFuID.HasField(string.Concat(15)))
		{
			player.TianFuID.SetField(string.Concat(15), new JSONObject(JSONObject.Type.ARRAY));
		}
		for (int i = num; i <= num2; i++)
		{
			player.TianFuID[string.Concat(15)].Add(i);
			foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
			{
				if (item["TuJianType"].I == 1 && item["quality"].I == i)
				{
					int i2 = item["id"].I;
					TuJianManager.Inst.UnlockItem(i2);
					TuJianManager.Inst.UnlockZhuYao(i2);
					TuJianManager.Inst.UnlockFuYao(i2);
					TuJianManager.Inst.UnlockYaoYin(i2);
				}
			}
		}
	}

	public void realizedSeid16()
	{
		Avatar player = Tools.instance.getPlayer();
		int value = getValue(16);
		if (player.TianFuID.HasField(string.Concat(16)))
		{
			player.TianFuID[string.Concat(16)].Add(value);
			return;
		}
		player.TianFuID.SetField(string.Concat(16), new JSONObject(JSONObject.Type.ARRAY));
		player.TianFuID[string.Concat(16)].Add(value);
	}

	public void realizedSeid17()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(17), 0);
	}

	public void realizedSeid18()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(18), 0);
	}

	public void realizedSeid22()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(22), id.ToString());
	}

	public void realizedSeid23()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jSONObject = jsonData.instance.CrateAvatarSeidJsonData[23][id.ToString()]["value1"];
		int count = jSONObject.Count;
		JSONObject jSONObject2 = jsonData.instance.CrateAvatarSeidJsonData[23][id.ToString()]["value2"];
		for (int i = 0; i < count; i++)
		{
			player.wuDaoMag.addWuDaoEx(jSONObject[i].I, jSONObject2[i].I);
		}
	}
}
