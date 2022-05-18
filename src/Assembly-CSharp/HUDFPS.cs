using System;
using UnityEngine;

// Token: 0x020006A5 RID: 1701
public class HUDFPS : MonoBehaviour
{
	// Token: 0x06002A8E RID: 10894 RVA: 0x0002102E File Offset: 0x0001F22E
	private void Start()
	{
		this.timeleft = this.updateInterval;
		base.transform.GetComponent<GUIText>().fontSize = Screen.height / 20;
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x00147DFC File Offset: 0x00145FFC
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F2} FPS", num);
			base.GetComponent<GUIText>().text = text;
			if (num < 25f)
			{
				base.GetComponent<GUIText>().material.color = Color.yellow;
			}
			else if (num < 15f)
			{
				base.GetComponent<GUIText>().material.color = Color.red;
			}
			else
			{
				base.GetComponent<GUIText>().material.color = Color.green;
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x04002437 RID: 9271
	public float updateInterval = 0.5f;

	// Token: 0x04002438 RID: 9272
	private float accum;

	// Token: 0x04002439 RID: 9273
	private int frames;

	// Token: 0x0400243A RID: 9274
	private float timeleft;
}
