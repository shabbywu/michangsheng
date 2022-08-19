using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class SeaTargetUI : MonoBehaviour
{
	// Token: 0x06001FBD RID: 8125 RVA: 0x000DFB1F File Offset: 0x000DDD1F
	private void Start()
	{
		SeaTargetUI.Inst = this;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x000DFB34 File Offset: 0x000DDD34
	public void PlayDanfangIn()
	{
		if (this.DanfunPlan.transform.localPosition.x < -800f)
		{
			this.DanfunPlan.GetComponent<Animation>().Play("Danfangout");
			return;
		}
		this.DanfunPlan.GetComponent<Animation>().Play("Danfang");
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x000DFB8C File Offset: 0x000DDD8C
	private void Update()
	{
		List<int> list = new List<int>();
		Tools.instance.getPlayer();
		foreach (SeaAvatarObjBase seaAvatarObjBase in EndlessSeaMag.Inst.MonstarList)
		{
			if (seaAvatarObjBase.IsCollect)
			{
				list.Add(seaAvatarObjBase._EventId);
			}
		}
		int num = GlobalValue.Get(1000, "SeaTargetUI.Update");
		if (num > 0)
		{
			list.Add(num);
		}
		foreach (object obj in this.uigroup.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf && !list.Contains(transform.GetComponent<SeaTargetUICell>().eventId))
			{
				Object.Destroy(transform.gameObject, 0.01f);
			}
		}
		bool flag = false;
		foreach (int num2 in list)
		{
			bool flag2 = true;
			using (IEnumerator enumerator2 = this.uigroup.transform.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (((Transform)enumerator2.Current).GetComponent<SeaTargetUICell>().eventId == num2)
					{
						flag2 = false;
					}
				}
			}
			if (flag2)
			{
				this.CreatObj(num2);
				flag = true;
			}
		}
		if (flag && this.DanfunPlan.transform.localPosition.x < -800f)
		{
			this.PlayDanfangIn();
		}
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x000DFD70 File Offset: 0x000DDF70
	public void CreatObj(int temp)
	{
		SeaTargetUICell seaTargetUICell = Object.Instantiate<SeaTargetUICell>(this.eventuiBase);
		seaTargetUICell.gameObject.SetActive(true);
		seaTargetUICell.transform.parent = this.uigroup.transform;
		seaTargetUICell.transform.localScale = Vector3.one;
		seaTargetUICell.transform.localPosition = Vector3.zero;
		SeaTargetUICell component = seaTargetUICell.GetComponent<SeaTargetUICell>();
		component.eventId = temp;
		JToken jtoken = jsonData.instance.EndlessSeaNPCData[string.Concat(temp)];
		component.Title.text = (string)jtoken["EventName"];
	}

	// Token: 0x040019D0 RID: 6608
	public static SeaTargetUI Inst;

	// Token: 0x040019D1 RID: 6609
	public SeaTargetUICell eventuiBase;

	// Token: 0x040019D2 RID: 6610
	public GameObject uigroup;

	// Token: 0x040019D3 RID: 6611
	public GameObject DanfunPlan;
}
