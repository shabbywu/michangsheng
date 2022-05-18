using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class LeanAudioOptions
{
	// Token: 0x06000066 RID: 102 RVA: 0x00004370 File Offset: 0x00002570
	public LeanAudioOptions setFrequency(int frequencyRate)
	{
		this.frequencyRate = frequencyRate;
		return this;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000437A File Offset: 0x0000257A
	public LeanAudioOptions setVibrato(Vector3[] vibrato)
	{
		this.vibrato = vibrato;
		return this;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004384 File Offset: 0x00002584
	public LeanAudioOptions setWaveSine()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Sine;
		return this;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000438E File Offset: 0x0000258E
	public LeanAudioOptions setWaveSquare()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Square;
		return this;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004398 File Offset: 0x00002598
	public LeanAudioOptions setWaveSawtooth()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Sawtooth;
		return this;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000043A2 File Offset: 0x000025A2
	public LeanAudioOptions setWaveNoise()
	{
		this.waveStyle = LeanAudioOptions.LeanAudioWaveStyle.Noise;
		return this;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000043AC File Offset: 0x000025AC
	public LeanAudioOptions setWaveStyle(LeanAudioOptions.LeanAudioWaveStyle style)
	{
		this.waveStyle = style;
		return this;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000043B6 File Offset: 0x000025B6
	public LeanAudioOptions setWaveNoiseScale(float waveScale)
	{
		this.waveNoiseScale = waveScale;
		return this;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000043C0 File Offset: 0x000025C0
	public LeanAudioOptions setWaveNoiseInfluence(float influence)
	{
		this.waveNoiseInfluence = influence;
		return this;
	}

	// Token: 0x04000062 RID: 98
	public LeanAudioOptions.LeanAudioWaveStyle waveStyle;

	// Token: 0x04000063 RID: 99
	public Vector3[] vibrato;

	// Token: 0x04000064 RID: 100
	public Vector3[] modulation;

	// Token: 0x04000065 RID: 101
	public int frequencyRate = 44100;

	// Token: 0x04000066 RID: 102
	public float waveNoiseScale = 1000f;

	// Token: 0x04000067 RID: 103
	public float waveNoiseInfluence = 1f;

	// Token: 0x04000068 RID: 104
	public bool useSetData = true;

	// Token: 0x04000069 RID: 105
	public LeanAudioStream stream;

	// Token: 0x02000017 RID: 23
	public enum LeanAudioWaveStyle
	{
		// Token: 0x0400006B RID: 107
		Sine,
		// Token: 0x0400006C RID: 108
		Square,
		// Token: 0x0400006D RID: 109
		Sawtooth,
		// Token: 0x0400006E RID: 110
		Noise
	}
}
