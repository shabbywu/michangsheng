using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000613 RID: 1555
public class DanFang_UI : MonoBehaviour
{
	// Token: 0x060026AC RID: 9900 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x0012F35C File Offset: 0x0012D55C
	public void showContent()
	{
		this.content.SetActive(true);
		if (this.ChildWeigh)
		{
			float num = this.ASize;
			foreach (object obj in this.content.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					num += transform.GetComponent<RectTransform>().sizeDelta.y + 10f;
				}
			}
			float num2 = 0.2f;
			if (this.ShowHait > 0.05f)
			{
				this.ShowHait = 0f;
				num2 = 0f;
			}
			base.GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 20f + num + num2);
			this.ShowHait += Time.deltaTime;
			return;
		}
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 10f + (float)this.content.transform.childCount * this.ASize);
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x0001ED30 File Offset: 0x0001CF30
	public void closeContent()
	{
		this.content.SetActive(false);
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 76.5f);
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x0001ED58 File Offset: 0x0001CF58
	private void Update()
	{
		if (this.toggle.isOn)
		{
			this.showContent();
			return;
		}
		this.closeContent();
	}

	// Token: 0x040020F9 RID: 8441
	public int ItemID = -1;

	// Token: 0x040020FA RID: 8442
	public GameObject content;

	// Token: 0x040020FB RID: 8443
	public Toggle toggle;

	// Token: 0x040020FC RID: 8444
	public LianDanDanFang lianDanDanFang;

	// Token: 0x040020FD RID: 8445
	public Text text;

	// Token: 0x040020FE RID: 8446
	public float ASize = 60f;

	// Token: 0x040020FF RID: 8447
	private bool isShow;

	// Token: 0x04002100 RID: 8448
	private float IsShowPastTime;

	// Token: 0x04002101 RID: 8449
	private float ShowHait;

	// Token: 0x04002102 RID: 8450
	public bool ChildWeigh;
}
