using System;
using UnityEngine;

// Token: 0x020006A6 RID: 1702
public class HUDFPS1 : MonoBehaviour
{
	// Token: 0x06002A91 RID: 10897 RVA: 0x00021067 File Offset: 0x0001F267
	private void Start()
	{
		this.timeleft = this.updateInterval;
		this.guiText1 = base.transform.GetComponent<TextMesh>();
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x00147EF4 File Offset: 0x001460F4
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F2} FPS", num);
			this.guiText1.text = text;
			if (num < 25f)
			{
				this.guiText1.color = Color.yellow;
			}
			else if (num < 15f)
			{
				this.guiText1.color = Color.red;
			}
			else
			{
				this.guiText1.color = Color.green;
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x0400243B RID: 9275
	public float updateInterval = 0.5f;

	// Token: 0x0400243C RID: 9276
	private float accum;

	// Token: 0x0400243D RID: 9277
	private int frames;

	// Token: 0x0400243E RID: 9278
	private float timeleft;

	// Token: 0x0400243F RID: 9279
	private TextMesh guiText1;
}
