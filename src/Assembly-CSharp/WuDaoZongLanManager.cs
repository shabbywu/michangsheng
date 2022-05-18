using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class WuDaoZongLanManager : MonoBehaviour, IESCClose
{
	// Token: 0x060021E2 RID: 8674 RVA: 0x0001BCE1 File Offset: 0x00019EE1
	public void open()
	{
		base.gameObject.SetActive(true);
		this.init();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x001196C0 File Offset: 0x001178C0
	private void init()
	{
		this.clear();
		Avatar player = Tools.instance.getPlayer();
		this.initHasWuDaoList(player);
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x001196E8 File Offset: 0x001178E8
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

	// Token: 0x060021E5 RID: 8677 RVA: 0x0001BD00 File Offset: 0x00019F00
	public void close()
	{
		this.x = 0f;
		this.y = -77.655f;
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x001197EC File Offset: 0x001179EC
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

	// Token: 0x060021E7 RID: 8679 RVA: 0x0001BD2F File Offset: 0x00019F2F
	public bool TryEscClose()
	{
		this.close();
		return true;
	}

	// Token: 0x04001D51 RID: 7505
	[SerializeField]
	private GameObject wuDaoType;

	// Token: 0x04001D52 RID: 7506
	[SerializeField]
	private List<Sprite> Icons = new List<Sprite>();

	// Token: 0x04001D53 RID: 7507
	private float x;

	// Token: 0x04001D54 RID: 7508
	private float y = -77.655f;

	// Token: 0x04001D55 RID: 7509
	private float DeafultHeight = 24.47f;
}
