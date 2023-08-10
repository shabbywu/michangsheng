using System.Collections.Generic;
using UnityEngine;

public class showDanfangImage : MonoBehaviour
{
	public GameObject content;

	public UIselect uIselect;

	private void Start()
	{
	}

	public void click()
	{
		List<Transform> child = getChild();
		if (child.Count > 0)
		{
			((Component)child[0]).GetComponent<DanGeDanFang_UI>().SetLianDanThis();
		}
		uIselect.list.Clear();
		int num = 0;
		foreach (Transform item in child)
		{
			_ = item;
			uIselect.list.Add("第" + Tools.getStr("shuzi" + (num + 1)) + "页");
			num++;
		}
		uIselect.setIndex(0);
		uIselect.left.onClick.Clear();
		uIselect.right.onClick.Clear();
		uIselect.left.onClick.Add(new EventDelegate(uIselect.clickLeft));
		uIselect.right.onClick.Add(new EventDelegate(uIselect.clickRight));
		uIselect.left.onClick.Add(new EventDelegate(setNowPage));
		uIselect.right.onClick.Add(new EventDelegate(setNowPage));
	}

	public void setNowPage()
	{
		List<Transform> child = getChild();
		if (child.Count > 0)
		{
			((Component)child[uIselect.NowIndex]).GetComponent<DanGeDanFang_UI>().SetLianDanThis();
		}
	}

	public List<Transform> getChild()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		List<Transform> list = new List<Transform>();
		foreach (Transform item in content.transform)
		{
			DanGeDanFang_UI component = ((Component)item).GetComponent<DanGeDanFang_UI>();
			if ((Object)(object)component != (Object)null && component.danyao.Count > 0)
			{
				list.Add(((Component)component).transform);
			}
		}
		return list;
	}
}
