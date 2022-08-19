using System;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
public class HUDFPS1 : MonoBehaviour
{
	// Token: 0x06002613 RID: 9747 RVA: 0x00107B3E File Offset: 0x00105D3E
	private void Start()
	{
		this.timeleft = this.updateInterval;
		this.guiText1 = base.transform.GetComponent<TextMesh>();
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x00107B60 File Offset: 0x00105D60
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

	// Token: 0x04001EC8 RID: 7880
	public float updateInterval = 0.5f;

	// Token: 0x04001EC9 RID: 7881
	private float accum;

	// Token: 0x04001ECA RID: 7882
	private int frames;

	// Token: 0x04001ECB RID: 7883
	private float timeleft;

	// Token: 0x04001ECC RID: 7884
	private TextMesh guiText1;
}
