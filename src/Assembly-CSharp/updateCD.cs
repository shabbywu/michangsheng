using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000658 RID: 1624
public class updateCD : MonoBehaviour
{
	// Token: 0x0600286B RID: 10347 RVA: 0x0013BDF0 File Offset: 0x00139FF0
	private void Start()
	{
		this.currentTime = this.coolingTimer;
		this.coolingImage = base.transform.Find("Image").GetComponent<Image>();
		this.cooltext = base.transform.Find("cooltime").GetComponent<Text>();
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x0013BE40 File Offset: 0x0013A040
	private void Update()
	{
		if (this.currentTime < this.coolingTimer)
		{
			this.currentTime += Time.deltaTime;
			this.coolingImage.fillAmount = 1f - this.currentTime / this.coolingTimer;
			if ((double)(this.coolingTimer - this.currentTime) < 0.1)
			{
				this.cooltext.text = "";
				return;
			}
			this.cooltext.text = string.Concat((int)(this.coolingTimer - this.currentTime));
		}
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x0001FA29 File Offset: 0x0001DC29
	public void OnBtnClickSkill()
	{
		if (this.currentTime >= this.coolingTimer)
		{
			this.currentTime = 0f;
			this.coolingImage.fillAmount = 1f;
		}
	}

	// Token: 0x04002226 RID: 8742
	public float coolingTimer = 2f;

	// Token: 0x04002227 RID: 8743
	private float currentTime;

	// Token: 0x04002228 RID: 8744
	public Image coolingImage;

	// Token: 0x04002229 RID: 8745
	public Text cooltext;
}
