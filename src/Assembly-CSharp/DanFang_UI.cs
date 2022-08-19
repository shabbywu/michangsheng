using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200045A RID: 1114
public class DanFang_UI : MonoBehaviour
{
	// Token: 0x060022FB RID: 8955 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x000EEDC0 File Offset: 0x000ECFC0
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

	// Token: 0x060022FD RID: 8957 RVA: 0x000EEEE8 File Offset: 0x000ED0E8
	public void closeContent()
	{
		this.content.SetActive(false);
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 76.5f);
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x000EEF10 File Offset: 0x000ED110
	private void Update()
	{
		if (this.toggle.isOn)
		{
			this.showContent();
			return;
		}
		this.closeContent();
	}

	// Token: 0x04001C2B RID: 7211
	public int ItemID = -1;

	// Token: 0x04001C2C RID: 7212
	public GameObject content;

	// Token: 0x04001C2D RID: 7213
	public Toggle toggle;

	// Token: 0x04001C2E RID: 7214
	public LianDanDanFang lianDanDanFang;

	// Token: 0x04001C2F RID: 7215
	public Text text;

	// Token: 0x04001C30 RID: 7216
	public float ASize = 60f;

	// Token: 0x04001C31 RID: 7217
	private bool isShow;

	// Token: 0x04001C32 RID: 7218
	private float IsShowPastTime;

	// Token: 0x04001C33 RID: 7219
	private float ShowHait;

	// Token: 0x04001C34 RID: 7220
	public bool ChildWeigh;
}
