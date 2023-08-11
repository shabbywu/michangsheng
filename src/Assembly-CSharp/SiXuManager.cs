using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class SiXuManager : MonoBehaviour, IESCClose
{
	[SerializeField]
	private GameObject SiXuCell;

	public void open()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
		init();
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	private void init()
	{
		clear();
		Avatar player = Tools.instance.getPlayer();
		initLingGuan(player);
	}

	private void clear()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in SiXuCell.transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	private void initLingGuan(Avatar player)
	{
		List<JSONObject> list = player.LingGuang.list;
		for (int i = 0; i < list.Count; i++)
		{
			SiXuTypeCell component = Tools.InstantiateGameObject(SiXuCell, SiXuCell.transform.parent).GetComponent<SiXuTypeCell>();
			int days = (DateTime.Parse(list[i]["startTime"].str).AddDays(list[i]["guoqiTime"].I) - player.worldTimeMag.getNowTime()).Days;
			if (days <= 30)
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", $"<color=#ff6554>{days}</color>");
			}
			else if (days <= 90)
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", $"<color=#f2a16b>{days}</color>");
			}
			else
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", $"<color=#cfcea8>{days}日后</color>");
			}
		}
	}

	public bool TryEscClose()
	{
		close();
		return true;
	}
}
