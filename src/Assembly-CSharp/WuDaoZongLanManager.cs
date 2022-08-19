using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class WuDaoZongLanManager : MonoBehaviour, IESCClose
{
	// Token: 0x06001E61 RID: 7777 RVA: 0x000D5E88 File Offset: 0x000D4088
	public void open()
	{
		base.gameObject.SetActive(true);
		this.init();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x000D5EA8 File Offset: 0x000D40A8
	private void init()
	{
		this.clear();
		Avatar player = Tools.instance.getPlayer();
		this.initHasWuDaoList(player);
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x000D5ED0 File Offset: 0x000D40D0
	private void initHasWuDaoList(Avatar player)
	{
		float num = 0f;
		float num2 = 0f;
		List<JSONObject> list = jsonData.instance.WuDaoAllTypeJson.list;
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = Tools.InstantiateGameObject(this.wuDaoType, this.wuDaoType.transform.parent);
			WuDaoHasType component = gameObject.GetComponent<WuDaoHasType>();
			component.setIcon(this.Icons[i]);
			float num3 = 0f;
			component.init(list[i]["id"].I, player, out num3);
			gameObject.transform.localPosition = new Vector3(this.x, this.y);
			this.y -= num3 + this.DeafultHeight;
			num2 = num3;
			num += num3 + this.DeafultHeight;
		}
		num += num2;
		this.wuDaoType.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(1312.4f, num);
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x000D5FD1 File Offset: 0x000D41D1
	public void close()
	{
		this.x = 0f;
		this.y = -77.655f;
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x000D6000 File Offset: 0x000D4200
	private void clear()
	{
		foreach (object obj in this.wuDaoType.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x000D6074 File Offset: 0x000D4274
	public bool TryEscClose()
	{
		this.close();
		return true;
	}

	// Token: 0x040018E8 RID: 6376
	[SerializeField]
	private GameObject wuDaoType;

	// Token: 0x040018E9 RID: 6377
	[SerializeField]
	private List<Sprite> Icons = new List<Sprite>();

	// Token: 0x040018EA RID: 6378
	private float x;

	// Token: 0x040018EB RID: 6379
	private float y = -77.655f;

	// Token: 0x040018EC RID: 6380
	private float DeafultHeight = 24.47f;
}
