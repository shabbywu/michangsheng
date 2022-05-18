using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x02000527 RID: 1319
public class SiXuManager : MonoBehaviour, IESCClose
{
	// Token: 0x060021CA RID: 8650 RVA: 0x0001BC3D File Offset: 0x00019E3D
	public void open()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.gameObject.SetActive(true);
		this.init();
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x0001694F File Offset: 0x00014B4F
	public void close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x001190A4 File Offset: 0x001172A4
	private void init()
	{
		this.clear();
		Avatar player = Tools.instance.getPlayer();
		this.initLingGuan(player);
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x001190CC File Offset: 0x001172CC
	private void clear()
	{
		foreach (object obj in this.SiXuCell.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x00119140 File Offset: 0x00117340
	private void initLingGuan(Avatar player)
	{
		List<JSONObject> list = player.LingGuang.list;
		for (int i = 0; i < list.Count; i++)
		{
			SiXuTypeCell component = Tools.InstantiateGameObject(this.SiXuCell, this.SiXuCell.transform.parent).GetComponent<SiXuTypeCell>();
			int days = (DateTime.Parse(list[i]["startTime"].str).AddDays((double)list[i]["guoqiTime"].I) - player.worldTimeMag.getNowTime()).Days;
			if (days <= 30)
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", string.Format("<color=#ff6554>{0}</color>", days));
			}
			else if (days <= 90)
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", string.Format("<color=#f2a16b>{0}</color>", days));
			}
			else
			{
				component.setContent("<color=#cfcea8>" + Tools.Code64(list[i]["name"].str) + "</color>", string.Format("<color=#cfcea8>{0}日后</color>", days));
			}
		}
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x0001BC5C File Offset: 0x00019E5C
	public bool TryEscClose()
	{
		this.close();
		return true;
	}

	// Token: 0x04001D42 RID: 7490
	[SerializeField]
	private GameObject SiXuCell;
}
