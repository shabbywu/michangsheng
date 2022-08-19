using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class SiXuManager : MonoBehaviour, IESCClose
{
	// Token: 0x06001E49 RID: 7753 RVA: 0x000D5735 File Offset: 0x000D3935
	public void open()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.gameObject.SetActive(true);
		this.init();
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x000A3540 File Offset: 0x000A1740
	public void close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x000D5754 File Offset: 0x000D3954
	private void init()
	{
		this.clear();
		Avatar player = Tools.instance.getPlayer();
		this.initLingGuan(player);
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x000D577C File Offset: 0x000D397C
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

	// Token: 0x06001E4D RID: 7757 RVA: 0x000D57F0 File Offset: 0x000D39F0
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

	// Token: 0x06001E4E RID: 7758 RVA: 0x000D596C File Offset: 0x000D3B6C
	public bool TryEscClose()
	{
		this.close();
		return true;
	}

	// Token: 0x040018D9 RID: 6361
	[SerializeField]
	private GameObject SiXuCell;
}
