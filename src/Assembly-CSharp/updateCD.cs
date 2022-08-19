using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200048A RID: 1162
public class updateCD : MonoBehaviour
{
	// Token: 0x0600249A RID: 9370 RVA: 0x000FC8F8 File Offset: 0x000FAAF8
	private void Start()
	{
		this.currentTime = this.coolingTimer;
		this.coolingImage = base.transform.Find("Image").GetComponent<Image>();
		this.cooltext = base.transform.Find("cooltime").GetComponent<Text>();
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x000FC948 File Offset: 0x000FAB48
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

	// Token: 0x0600249C RID: 9372 RVA: 0x000FC9E0 File Offset: 0x000FABE0
	public void OnBtnClickSkill()
	{
		if (this.currentTime >= this.coolingTimer)
		{
			this.currentTime = 0f;
			this.coolingImage.fillAmount = 1f;
		}
	}

	// Token: 0x04001D49 RID: 7497
	public float coolingTimer = 2f;

	// Token: 0x04001D4A RID: 7498
	private float currentTime;

	// Token: 0x04001D4B RID: 7499
	public Image coolingImage;

	// Token: 0x04001D4C RID: 7500
	public Text cooltext;
}
