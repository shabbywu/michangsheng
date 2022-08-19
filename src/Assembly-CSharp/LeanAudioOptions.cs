using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class LeanAudioOptions
{
	// Token: 0x06000066 RID: 102 RVA: 0x00004237 File Offset: 0x00002437
	public LeanAudioOptions setFrequency(int frequencyRate)
	{
		this.frequencyRate = frequencyRate;
		return this;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004241 File Offset: 0x00002441
	public LeanAudioOptions setVibrato(Vector3[] vibrato)
	{
		this.vibrato = vibrato;
		return this;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000424B File Offset: 0x0000244B
	public LeanAudioOptions setWaveSine()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Sine;
		return this;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00004255 File Offset: 0x00002455
	public LeanAudioOptions setWaveSquare()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Square;
		return this;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0000425F File Offset: 0x0000245F
	public LeanAudioOptions setWaveSawtooth()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Sawtooth;
		return this;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004269 File Offset: 0x00002469
	public LeanAudioOptions setWaveNoise()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Noise;
		return this;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004273 File Offset: 0x00002473
	public LeanAudioOptions setWaveStyle(LeanAudioOptions.LeanAudioWaveStyle style)
	{
		this.waveStyle = style;
		return this;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000427D File Offset: 0x0000247D
	public LeanAudioOptions setWaveNoiseScale(float waveScale)
	{
		this.waveNoiseScale = waveScale;
		return this;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00004287 File Offset: 0x00002487
	public LeanAudioOptions setWaveNoiseInfluence(float influence)
	{
		this.waveNoiseInfluence = influence;
		return this;
	}

	// Token: 0x0400005C RID: 92
	public LeanAudioOptions.LeanAudioWaveStyle waveStyle;

	// Token: 0x0400005D RID: 93
	public Vector3[] vibrato;

	// Token: 0x0400005E RID: 94
	public Vector3[] modulation;

	// Token: 0x0400005F RID: 95
	public int frequencyRate = 44100;

	// Token: 0x04000060 RID: 96
	public float waveNoiseScale = 1000f;

	// Token: 0x04000061 RID: 97
	public float waveNoiseInfluence = 1f;

	// Token: 0x04000062 RID: 98
	public bool useSetData = true;

	// Token: 0x04000063 RID: 99
	public LeanAudioStream stream;

	// Token: 0x020011C6 RID: 4550
	public enum LeanAudioWaveStyle
	{
		// Token: 0x04006356 RID: 25430
		Sine,
		// Token: 0x04006357 RID: 25431
		Square,
		// Token: 0x04006358 RID: 25432
		Sawtooth,
		// Token: 0x04006359 RID: 25433
		Noise
	}
}
