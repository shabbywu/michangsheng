using System;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
public class UI_Notice : MonoBehaviour
{
	// Token: 0x0600254D RID: 9549 RVA: 0x0001DEBA File Offset: 0x0001C0BA
	private void Awake()
	{
		this.heloanimation = this.NoticeText.gameObject.GetComponent<Animation>();
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x0012ACE0 File Offset: 0x00128EE0
	public void open()
	{
		this.heloanimation["notice2"].speed = 1f;
		this.heloanimation["notice2"].time = 0f;
		this.heloanimation.CrossFade("notice2");
		this.NoticeText.SetActive(true);
		this.opentype = 1;
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x0012AD44 File Offset: 0x00128F44
	public void close()
	{
		this.heloanimation["notice2"].speed = -1f;
		this.heloanimation["notice2"].time = this.heloanimation["notice2"].clip.length;
		this.heloanimation.CrossFade("notice2");
		this.opentype = 0;
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x0001DED2 File Offset: 0x0001C0D2
	public void buttondwon()
	{
		if (this.opentype == 0)
		{
			this.open();
			return;
		}
		this.close();
	}

	// Token: 0x04001FD0 RID: 8144
	public GameObject NoticeText;

	// Token: 0x04001FD1 RID: 8145
	public Animation heloanimation;

	// Token: 0x04001FD2 RID: 8146
	private int opentype;
}
