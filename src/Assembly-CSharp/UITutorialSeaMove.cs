using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class UITutorialSeaMove : MonoBehaviour
{
	// Token: 0x060020EE RID: 8430 RVA: 0x0001B215 File Offset: 0x00019415
	private void Awake()
	{
		UITutorialSeaMove.Inst = this;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x00114C20 File Offset: 0x00112E20
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

	// Token: 0x060020F0 RID: 8432 RVA: 0x0001B21D File Offset: 0x0001941D
	public void Show()
	{
		this.ScaleObj.SetActive(true);
		this.isShow = true;
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x0001B232 File Offset: 0x00019432
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		this.isShow = false;
	}

	// Token: 0x04001C68 RID: 7272
	public static UITutorialSeaMove Inst;

	// Token: 0x04001C69 RID: 7273
	public GameObject ScaleObj;

	// Token: 0x04001C6A RID: 7274
	private bool isShow;

	// Token: 0x04001C6B RID: 7275
	private List<KeyCode> triggerKeyList = new List<KeyCode>
	{
		119,
		97,
		115,
		100,
		32
	};
}
