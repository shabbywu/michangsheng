using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F8 RID: 504
public class MyDrawFpsText : MonoBehaviour
{
	// Token: 0x0600149B RID: 5275 RVA: 0x00084068 File Offset: 0x00082268
	private void Start()
	{
		this.timeleft = this.updateInterval;
		this.fpsText = base.GetComponent<Text>();
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x00084084 File Offset: 0x00082284
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F2} FPS", num);
			this.fpsText.text = text;
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x04000F64 RID: 3940
	public float updateInterval = 0.5f;

	// Token: 0x04000F65 RID: 3941
	private float accum;

	// Token: 0x04000F66 RID: 3942
	private int frames;

	// Token: 0x04000F67 RID: 3943
	private float timeleft;

	// Token: 0x04000F68 RID: 3944
	private Text fpsText;
}
