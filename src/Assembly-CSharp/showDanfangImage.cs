using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000627 RID: 1575
public class showDanfangImage : MonoBehaviour
{
	// Token: 0x06002723 RID: 10019 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x00132734 File Offset: 0x00130934
	public void click()
	{
		List<Transform> child = this.getChild();
		if (child.Count > 0)
		{
			child[0].GetComponent<DanGeDanFang_UI>().SetLianDanThis();
		}
		this.uIselect.list.Clear();
		int num = 0;
		foreach (Transform transform in child)
		{
			this.uIselect.list.Add("第" + Tools.getStr("shuzi" + (num + 1)) + "页");
			num++;
		}
		this.uIselect.setIndex(0);
		this.uIselect.left.onClick.Clear();
		this.uIselect.right.onClick.Clear();
		this.uIselect.left.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.uIselect.clickLeft)));
		this.uIselect.right.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.uIselect.clickRight)));
		this.uIselect.left.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.setNowPage)));
		this.uIselect.right.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.setNowPage)));
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x001328C0 File Offset: 0x00130AC0
	public void setNowPage()
	{
		List<Transform> child = this.getChild();
		if (child.Count > 0)
		{
			child[this.uIselect.NowIndex].GetComponent<DanGeDanFang_UI>().SetLianDanThis();
		}
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x001328F8 File Offset: 0x00130AF8
	public List<Transform> getChild()
	{
		List<Transform> list = new List<Transform>();
		foreach (object obj in this.content.transform)
		{
			DanGeDanFang_UI component = ((Transform)obj).GetComponent<DanGeDanFang_UI>();
			if (component != null && component.danyao.Count > 0)
			{
				list.Add(component.transform);
			}
		}
		return list;
	}

	// Token: 0x04002142 RID: 8514
	public GameObject content;

	// Token: 0x04002143 RID: 8515
	public UIselect uIselect;
}
