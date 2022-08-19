using System;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class UI_Notice : MonoBehaviour
{
	// Token: 0x06002195 RID: 8597 RVA: 0x000E94B5 File Offset: 0x000E76B5
	private void Awake()
	{
		this.heloanimation = this.NoticeText.gameObject.GetComponent<Animation>();
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x000E94D0 File Offset: 0x000E76D0
	public void open()
	{
		this.heloanimation["notice2"].speed = 1f;
		this.heloanimation["notice2"].time = 0f;
		this.heloanimation.CrossFade("notice2");
		this.NoticeText.SetActive(true);
		this.opentype = 1;
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x000E9534 File Offset: 0x000E7734
	public void close()
	{
		this.heloanimation["notice2"].speed = -1f;
		this.heloanimation["notice2"].time = this.heloanimation["notice2"].clip.length;
		this.heloanimation.CrossFade("notice2");
		this.opentype = 0;
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x000E95A1 File Offset: 0x000E77A1
	public void buttondwon()
	{
		if (this.opentype == 0)
		{
			this.open();
			return;
		}
		this.close();
	}

	// Token: 0x04001B11 RID: 6929
	public GameObject NoticeText;

	// Token: 0x04001B12 RID: 6930
	public Animation heloanimation;

	// Token: 0x04001B13 RID: 6931
	private int opentype;
}
