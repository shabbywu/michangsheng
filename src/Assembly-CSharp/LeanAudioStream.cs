using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class LeanAudioStream
{
	// Token: 0x06000053 RID: 83 RVA: 0x00003BAD File Offset: 0x00001DAD
	public LeanAudioStream(float[] audioArr)
	{
		this.audioArr = audioArr;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00003BBC File Offset: 0x00001DBC
	public void OnAudioRead(float[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.audioArr[this.position];
			this.position++;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003BF5 File Offset: 0x00001DF5
	public void OnAudioSetPosition(int newPosition)
	{
		this.position = newPosition;
	}

	// Token: 0x04000054 RID: 84
	public int position;

	// Token: 0x04000055 RID: 85
	public AudioClip audioClip;

	// Token: 0x04000056 RID: 86
	public float[] audioArr;
}
