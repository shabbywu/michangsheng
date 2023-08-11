using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SeaTargetUI : MonoBehaviour
{
	public static SeaTargetUI Inst;

	public SeaTargetUICell eventuiBase;

	public GameObject uigroup;

	public GameObject DanfunPlan;

	private void Start()
	{
		Inst = this;
		((Component)this).gameObject.SetActive(false);
	}

	public void PlayDanfangIn()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (DanfunPlan.transform.localPosition.x < -800f)
		{
			DanfunPlan.GetComponent<Animation>().Play("Danfangout");
		}
		else
		{
			DanfunPlan.GetComponent<Animation>().Play("Danfang");
		}
	}

	private void Update()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		Tools.instance.getPlayer();
		foreach (SeaAvatarObjBase monstar in EndlessSeaMag.Inst.MonstarList)
		{
			if (monstar.IsCollect)
			{
				list.Add(monstar._EventId);
			}
		}
		int num = GlobalValue.Get(1000, "SeaTargetUI.Update");
		if (num > 0)
		{
			list.Add(num);
		}
		foreach (Transform item in uigroup.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf && !list.Contains(((Component)val).GetComponent<SeaTargetUICell>().eventId))
			{
				Object.Destroy((Object)(object)((Component)val).gameObject, 0.01f);
			}
		}
		bool flag = false;
		foreach (int item2 in list)
		{
			bool flag2 = true;
			foreach (Transform item3 in uigroup.transform)
			{
				if (((Component)item3).GetComponent<SeaTargetUICell>().eventId == item2)
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				CreatObj(item2);
				flag = true;
			}
		}
		if (flag && DanfunPlan.transform.localPosition.x < -800f)
		{
			PlayDanfangIn();
		}
	}

	public void CreatObj(int temp)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		SeaTargetUICell seaTargetUICell = Object.Instantiate<SeaTargetUICell>(eventuiBase);
		((Component)seaTargetUICell).gameObject.SetActive(true);
		((Component)seaTargetUICell).transform.parent = uigroup.transform;
		((Component)seaTargetUICell).transform.localScale = Vector3.one;
		((Component)seaTargetUICell).transform.localPosition = Vector3.zero;
		SeaTargetUICell component = ((Component)seaTargetUICell).GetComponent<SeaTargetUICell>();
		component.eventId = temp;
		JToken val = jsonData.instance.EndlessSeaNPCData[string.Concat(temp)];
		component.Title.text = (string)val[(object)"EventName"];
	}
}
