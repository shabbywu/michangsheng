using UnityEngine;

public class LianDanDanFang : MonoBehaviour
{
	public GameObject DanFangItem;

	public GameObject DanFang;

	public UIPopupList mList;

	public int showtype;

	public void InitDanFang()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		foreach (JSONObject item in Tools.instance.getPlayer().DanFang.list)
		{
			if (item["ID"].I <= 0)
			{
				continue;
			}
			bool flag = true;
			Transform val = null;
			foreach (Transform item2 in ((Component)this).transform)
			{
				Transform val2 = item2;
				if (item["ID"].I == ((Component)val2).GetComponent<DanFang_UI>().ItemID)
				{
					flag = false;
					val = val2;
				}
			}
			if (flag)
			{
				GameObject obj = Object.Instantiate<GameObject>(DanFangItem);
				obj.GetComponent<DanFang_UI>().ItemID = item["ID"].I;
				obj.GetComponent<DanFang_UI>().text.text = Tools.Code64(jsonData.instance.ItemJsonData[item["ID"].I.ToString()]["name"].str);
				obj.transform.parent = ((Component)this).transform;
				obj.SetActive(true);
				obj.transform.localScale = Vector3.one;
				val = obj.transform;
			}
			bool flag2 = true;
			foreach (Transform item3 in ((Component)val).GetComponent<DanFang_UI>().content.transform)
			{
				Transform val3 = item3;
				if (!((Component)val3).gameObject.activeSelf)
				{
					continue;
				}
				bool flag3 = true;
				for (int i = 0; i < item["Type"].list.Count; i++)
				{
					if ((int)item["Type"][i].n != ((Component)val3).GetComponent<DanGeDanFang_UI>().danyao[i])
					{
						flag3 = false;
					}
				}
				for (int j = 0; j < item["Num"].list.Count; j++)
				{
					if ((int)item["Num"][j].n != ((Component)val3).GetComponent<DanGeDanFang_UI>().num[j])
					{
						flag3 = false;
					}
				}
				if (flag3)
				{
					flag2 = false;
				}
			}
			if (!flag2)
			{
				continue;
			}
			GameObject val4 = Object.Instantiate<GameObject>(DanFang);
			foreach (JSONObject item4 in item["Type"].list)
			{
				val4.GetComponent<DanGeDanFang_UI>().danyao.Add((int)item4.n);
			}
			foreach (JSONObject item5 in item["Num"].list)
			{
				val4.GetComponent<DanGeDanFang_UI>().num.Add((int)item5.n);
			}
			val4.GetComponent<DanGeDanFang_UI>().init();
			val4.transform.parent = ((Component)val).GetComponent<DanFang_UI>().content.transform;
			val4.SetActive(true);
			val4.transform.localScale = Vector3.one;
			val4.transform.localPosition = new Vector3(50f, -30f);
		}
	}

	public int getInputID(string name)
	{
		int num = 0;
		foreach (string item in mList.items)
		{
			if (name == item)
			{
				break;
			}
			num++;
		}
		return num;
	}

	private void Start()
	{
		InitDanFang();
	}

	public void setShowType()
	{
		showtype = getInputID(mList.value);
	}

	private void Update()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (showtype != 0)
		{
			foreach (Transform item in ((Component)this).transform)
			{
				Transform val = item;
				if (((Component)val).GetComponent<DanFang_UI>().ItemID > 0)
				{
					if ((int)jsonData.instance.ItemJsonData[((Component)val).GetComponent<DanFang_UI>().ItemID.ToString()]["quality"].n == showtype)
					{
						if (!((Component)val).gameObject.activeSelf)
						{
							((Component)val).gameObject.SetActive(true);
						}
					}
					else
					{
						((Component)val).gameObject.SetActive(false);
					}
				}
			}
			return;
		}
		foreach (Transform item2 in ((Component)this).transform)
		{
			Transform val2 = item2;
			if (((Component)val2).GetComponent<DanFang_UI>().ItemID > 0)
			{
				((Component)val2).gameObject.SetActive(true);
			}
		}
	}
}
