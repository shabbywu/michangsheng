using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

public class MainUITianFuCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public MainUIToggle toggle;

	public List<int> seidList;

	public string desc;

	public int id;

	public int costNum;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private Text cost;

	public int page;

	public void Init(JSONObject json)
	{
		id = json["id"].I;
		costNum = json["feiYong"].I;
		cost.text = costNum.ToString();
		toggle.group = json["fenZu"].I;
		seidList = json["seid"].ToList();
		Name.text = json["Title"].Str;
		desc = "<color=#24a5d6>效果：</color>" + json["Desc"].Str;
		desc = desc + "\n\n<color=#db9a53>说明：</color>" + json["Info"].Str;
		page = json["fenLeiGuanLian"].I;
		if (json["jiesuo"].I > MainUIMag.inst.maxLevel)
		{
			toggle.SetDisable();
			desc = desc + "\n\n<color=#db9a53>解锁：</color>剧情模式境界达到" + jsonData.instance.LevelUpDataJsonData[json["jiesuo"].I.ToString()]["Name"].Str + "解锁";
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		MainUIMag.inst.tooltip.Show(desc, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z));
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}

	public int getValue(int _seid)
	{
		if (seidList.Contains(_seid))
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
		if (seidList.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][id.ToString()]["value1"].list;
		}
		return null;
	}

	public List<JSONObject> getSeidValue10()
	{
		if (seidList.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][id.ToString()]["value1"].list;
		}
		return null;
	}

	public List<int> getSeidValue11()
	{
		if (seidList.Contains(11))
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
