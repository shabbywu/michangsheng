using System;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class HUDFPS : MonoBehaviour
{
	// Token: 0x06002610 RID: 9744 RVA: 0x00107A0B File Offset: 0x00105C0B
	private void Start()
	{
		this.timeleft = this.updateInterval;
		base.transform.GetComponent<GUIText>().fontSize = Screen.height / 20;
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x00107A34 File Offset: 0x00105C34
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

	// Token: 0x04001EC4 RID: 7876
	public float updateInterval = 0.5f;

	// Token: 0x04001EC5 RID: 7877
	private float accum;

	// Token: 0x04001EC6 RID: 7878
	private int frames;

	// Token: 0x04001EC7 RID: 7879
	private float timeleft;
}
