using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030D RID: 781
public class MyDrawFpsText : MonoBehaviour
{
	// Token: 0x06001745 RID: 5957 RVA: 0x000148ED File Offset: 0x00012AED
	private void Start()
	{
		this.timeleft = this.updateInterval;
		this.fpsText = base.GetComponent<Text>();
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000CCCFC File Offset: 0x000CAEFC
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

	// Token: 0x040012AA RID: 4778
	public float updateInterval = 0.5f;

	// Token: 0x040012AB RID: 4779
	private float accum;

	// Token: 0x040012AC RID: 4780
	private int frames;

	// Token: 0x040012AD RID: 4781
	private float timeleft;

	// Token: 0x040012AE RID: 4782
	private Text fpsText;
}
