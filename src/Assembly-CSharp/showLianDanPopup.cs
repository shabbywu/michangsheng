using UnityEngine;

public class showLianDanPopup : MonoBehaviour
{
	public LianDanDanFang lianDanDanFang;

	private UIPopupList mList;

	private void Start()
	{
		mList = ((Component)this).GetComponent<UIPopupList>();
		EventDelegate.Add(mList.onChange, OnChange);
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

	private void OnChange()
	{
		lianDanDanFang.showtype = getInputID(mList.value);
	}

	public void onchenge()
	{
	}

	private void Update()
	{
	}
}
