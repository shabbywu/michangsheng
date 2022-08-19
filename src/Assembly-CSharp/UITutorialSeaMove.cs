using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class UITutorialSeaMove : MonoBehaviour
{
	// Token: 0x06001D89 RID: 7561 RVA: 0x000D0B62 File Offset: 0x000CED62
	private void Awake()
	{
		UITutorialSeaMove.Inst = this;
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x000D0B6C File Offset: 0x000CED6C
	private void Update()
	{
		if (this.isShow)
		{
			using (List<KeyCode>.Enumerator enumerator = this.triggerKeyList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Input.GetKeyDown(enumerator.Current))
					{
						this.Close();
					}
				}
			}
		}
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x000D0BCC File Offset: 0x000CEDCC
	public void Show()
	{
		this.ScaleObj.SetActive(true);
		this.isShow = true;
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x000D0BE1 File Offset: 0x000CEDE1
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		this.isShow = false;
	}

	// Token: 0x0400181B RID: 6171
	public static UITutorialSeaMove Inst;

	// Token: 0x0400181C RID: 6172
	public GameObject ScaleObj;

	// Token: 0x0400181D RID: 6173
	private bool isShow;

	// Token: 0x0400181E RID: 6174
	private List<KeyCode> triggerKeyList = new List<KeyCode>
	{
		119,
		97,
		115,
		100,
		32
	};
}
