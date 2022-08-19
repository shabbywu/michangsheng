using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class showDanfangImage : MonoBehaviour
{
	// Token: 0x0600236D RID: 9069 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x000F25C8 File Offset: 0x000F07C8
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

	// Token: 0x0600236F RID: 9071 RVA: 0x000F2754 File Offset: 0x000F0954
	public void setNowPage()
	{
		List<Transform> child = this.getChild();
		if (child.Count > 0)
		{
			child[this.uIselect.NowIndex].GetComponent<DanGeDanFang_UI>().SetLianDanThis();
		}
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x000F278C File Offset: 0x000F098C
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

	// Token: 0x04001C6D RID: 7277
	public GameObject content;

	// Token: 0x04001C6E RID: 7278
	public UIselect uIselect;
}
