using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x02000566 RID: 1382
public class SeaTargetUI : MonoBehaviour
{
	// Token: 0x06002336 RID: 9014 RVA: 0x0001C97F File Offset: 0x0001AB7F
	private void Start()
	{
		SeaTargetUI.Inst = this;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x00122618 File Offset: 0x00120818
	public void PlayDanfangIn()
	{
		if (this.DanfunPlan.transform.localPosition.x < -800f)
		{
			this.DanfunPlan.GetComponent<Animation>().Play("Danfangout");
			return;
		}
		this.DanfunPlan.GetComponent<Animation>().Play("Danfang");
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x00122670 File Offset: 0x00120870
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

	// Token: 0x06002339 RID: 9017 RVA: 0x00122854 File Offset: 0x00120A54
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

	// Token: 0x04001E5A RID: 7770
	public static SeaTargetUI Inst;

	// Token: 0x04001E5B RID: 7771
	public SeaTargetUICell eventuiBase;

	// Token: 0x04001E5C RID: 7772
	public GameObject uigroup;

	// Token: 0x04001E5D RID: 7773
	public GameObject DanfunPlan;
}
