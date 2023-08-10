using UnityEngine;

public class showYaoCaiMag : MonoBehaviour
{
	public GameObject InstantObj;

	public UIPopupList mList;

	public GameObject content;

	public UIToggle caoYaoUIToggle;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void open()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		AddItems();
		foreach (Transform item in content.transform)
		{
			showCaiLiaoImage component = ((Component)item).GetComponent<showCaiLiaoImage>();
			if ((Object)(object)component != (Object)null && component.ItemID != -1 && ((Component)component).gameObject.activeSelf)
			{
				component.Click();
				break;
			}
		}
	}

	public void AddItems()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		foreach (JSONObject item in Tools.instance.getPlayer().YaoCaiIsGet.list)
		{
			bool flag = true;
			foreach (Transform item2 in content.transform)
			{
				showCaiLiaoImage component = ((Component)item2).GetComponent<showCaiLiaoImage>();
				if ((Object)(object)component != (Object)null && component.ItemID == item.I)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				GameObject obj = Tools.InstantiateGameObject(InstantObj, InstantObj.transform.parent);
				obj.GetComponent<showCaiLiaoImage>().ItemID = item.I;
				obj.GetComponent<showCaiLiaoImage>().TextName.text = Tools.Code64(jsonData.instance.ItemJsonData[item.I.ToString()]["name"].str);
			}
		}
	}

	public void setShowType()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		int inputID = getInputID(mList.value);
		foreach (Transform item in content.transform)
		{
			Transform val = item;
			showCaiLiaoImage component = ((Component)val).GetComponent<showCaiLiaoImage>();
			if (!((Object)(object)component != (Object)null) || component.ItemID == -1)
			{
				continue;
			}
			int i = jsonData.instance.ItemJsonData[component.ItemID.ToString()]["quality"].I;
			if (inputID != 0)
			{
				if (i == inputID)
				{
					((Component)val).gameObject.SetActive(true);
				}
				else
				{
					((Component)val).gameObject.SetActive(false);
				}
			}
			else
			{
				((Component)val).gameObject.SetActive(true);
			}
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

	private void Update()
	{
	}
}
