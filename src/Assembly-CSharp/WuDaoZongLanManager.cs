using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class WuDaoZongLanManager : MonoBehaviour, IESCClose
{
	[SerializeField]
	private GameObject wuDaoType;

	[SerializeField]
	private List<Sprite> Icons = new List<Sprite>();

	private float x;

	private float y = -77.655f;

	private float DeafultHeight = 24.47f;

	public void open()
	{
		((Component)this).gameObject.SetActive(true);
		init();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	private void init()
	{
		clear();
		Avatar player = Tools.instance.getPlayer();
		initHasWuDaoList(player);
	}

	private void initHasWuDaoList(Avatar player)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f;
		float num2 = 0f;
		List<JSONObject> list = jsonData.instance.WuDaoAllTypeJson.list;
		for (int i = 0; i < list.Count; i++)
		{
			GameObject obj = Tools.InstantiateGameObject(wuDaoType, wuDaoType.transform.parent);
			WuDaoHasType component = obj.GetComponent<WuDaoHasType>();
			component.setIcon(Icons[i]);
			float height = 0f;
			component.init(list[i]["id"].I, player, out height);
			obj.transform.localPosition = new Vector3(x, y);
			y -= height + DeafultHeight;
			num2 = height;
			num += height + DeafultHeight;
		}
		num += num2;
		((Component)wuDaoType.transform.parent).GetComponent<RectTransform>().sizeDelta = new Vector2(1312.4f, num);
	}

	public void close()
	{
		x = 0f;
		y = -77.655f;
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	private void clear()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		foreach (Transform item in wuDaoType.transform.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public bool TryEscClose()
	{
		close();
		return true;
	}
}
