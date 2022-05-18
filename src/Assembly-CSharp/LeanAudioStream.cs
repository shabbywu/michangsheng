using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class LeanAudioStream
{
	// Token: 0x06000053 RID: 83 RVA: 0x00004257 File Offset: 0x00002457
	public LeanAudioStream(float[] audioArr)
	{
		this.audioArr = audioArr;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0005E358 File Offset: 0x0005C558
	public void OnAudioRead(float[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.audioArr[this.position];
			this.position++;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004266 File Offset: 0x00002466
	public void OnAudioSetPosition(int newPosition)
	{
		this.position = newPosition;
	}

	// Token: 0x0400005A RID: 90
	public int position;

	// Token: 0x0400005B RID: 91
	public AudioClip audioClip;

	// Token: 0x0400005C RID: 92
	public float[] audioArr;
}
