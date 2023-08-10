using UnityEngine;
using UnityEngine.UI;

public class help_UI : MonoBehaviour
{
	public UIPopupList mList;

	public Image helpImage;

	public GameObject content;

	private void Start()
	{
		foreach (JSONObject item in jsonData.instance.helpJsonData.list)
		{
			mList.items.Add(Tools.instance.Code64ToString(item["Titile"].str));
		}
		mList.value = mList.items[0];
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

	public void excheng()
	{
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		Tools.instance.getPlayer();
		int inputID = getInputID(mList.value);
		int i = jsonData.instance.helpJsonData.list[inputID]["id"].I;
		if ((int)jsonData.instance.helpJsonData.list[inputID]["Image"].n > 0)
		{
			Sprite val = Resources.Load<Sprite>("Ui Icon/Help/" + (int)jsonData.instance.helpJsonData.list[inputID]["Image"].n);
			if ((Object)(object)val != (Object)null)
			{
				helpImage.sprite = val;
			}
		}
		else
		{
			helpImage.sprite = null;
		}
		for (int j = 2; j < content.transform.childCount; j++)
		{
			Object.Destroy((Object)(object)((Component)content.transform.GetChild(j)).gameObject);
		}
		Transform child = content.transform.GetChild(0);
		foreach (JSONObject item in jsonData.instance.helpTextJsonData.list)
		{
			if (i == (int)item["link"].n)
			{
				Transform obj = Object.Instantiate<Transform>(child);
				obj.SetParent(content.transform);
				((Component)obj).transform.localScale = Vector3.one;
				((Component)obj).gameObject.SetActive(true);
				((Component)obj).GetComponent<Text>().text = Tools.instance.Code64ToString(item["desc"].str);
				((Component)((Component)obj).transform.Find("Title")).GetComponent<Text>().text = Tools.instance.Code64ToString(item["Titile"].str);
			}
		}
	}

	private void Update()
	{
	}
}
