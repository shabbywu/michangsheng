using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class LeanAudio
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003BFE File Offset: 0x00001DFE
	public static LeanAudioOptions options()
	{
		if (LeanAudio.generatedWaveDistances == null)
		{
			LeanAudio.generatedWaveDistances = new float[LeanAudio.PROCESSING_ITERATIONS_MAX];
			LeanAudio.longList = new float[LeanAudio.PROCESSING_ITERATIONS_MAX];
		}
		return new LeanAudioOptions();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003C2A File Offset: 0x00001E2A
	public static LeanAudioStream createAudioStream(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		options.useSetData = false;
		LeanAudio.createAudioFromWave(LeanAudio.createAudioWave(volume, frequency, options), options);
		return options.stream;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003C52 File Offset: 0x00001E52
	public static AudioClip createAudio(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		return LeanAudio.createAudioFromWave(LeanAudio.createAudioWave(volume, frequency, options), options);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003C6C File Offset: 0x00001E6C
	private static int createAudioWave(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options)
	{
		float time = volume[volume.length - 1].time;
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < LeanAudio.PROCESSING_ITERATIONS_MAX; i++)
		{
			float num3 = frequency.Evaluate(num2);
			if (num3 < LeanAudio.MIN_FREQEUNCY_PERIOD)
			{
				num3 = LeanAudio.MIN_FREQEUNCY_PERIOD;
			}
			float num4 = volume.Evaluate(num2 + 0.5f * num3);
			if (options.vibrato != null)
			{
				for (int j = 0; j < options.vibrato.Length; j++)
				{
					float num5 = Mathf.Abs(Mathf.Sin(1.5708f + num2 * (1f / options.vibrato[j][0]) * 3.1415927f));
					float num6 = 1f - options.vibrato[j][1];
					num5 = options.vibrato[j][1] + num6 * num5;
					num4 *= num5;
				}
			}
			if (num2 + 0.5f * num3 >= time)
			{
				break;
			}
			if (num >= LeanAudio.PROCESSING_ITERATIONS_MAX - 1)
			{
				Debug.LogError("LeanAudio has reached it's processing cap. To avoid this error increase the number of iterations ex: LeanAudio.PROCESSING_ITERATIONS_MAX = " + LeanAudio.PROCESSING_ITERATIONS_MAX * 2);
				break;
			}
			int num7 = num / 2;
			num2 += num3;
			LeanAudio.generatedWaveDistances[num7] = num2;
			LeanAudio.longList[num] = num2;
			LeanAudio.longList[num + 1] = ((i % 2 == 0) ? (-num4) : num4);
			num += 2;
		}
		num += -2;
		LeanAudio.generatedWaveDistancesCount = num / 2;
		return num;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003DEC File Offset: 0x00001FEC
	private static AudioClip createAudioFromWave(int waveLength, LeanAudioOptions options)
	{
		float num = LeanAudio.longList[waveLength - 2];
		float[] array = new float[(int)((float)options.frequencyRate * num)];
		int num2 = 0;
		float num3 = LeanAudio.longList[num2];
		float num4 = 0f;
		float num5 = LeanAudio.longList[num2];
		float num6 = LeanAudio.longList[num2 + 1];
		for (int i = 0; i < array.Length; i++)
		{
			float num7 = (float)i / (float)options.frequencyRate;
			if (num7 > LeanAudio.longList[num2])
			{
				num4 = LeanAudio.longList[num2];
				num2 += 2;
				num3 = LeanAudio.longList[num2] - LeanAudio.longList[num2 - 2];
				num6 = LeanAudio.longList[num2 + 1];
			}
			float num8 = (num7 - num4) / num3;
			float num9 = Mathf.Sin(num8 * 3.1415927f);
			if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Square)
			{
				if (num9 > 0f)
				{
					num9 = 1f;
				}
				if (num9 < 0f)
				{
					num9 = -1f;
				}
			}
			else if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Sawtooth)
			{
				float num10 = (num9 > 0f) ? 1f : -1f;
				if (num8 < 0.5f)
				{
					num9 = num8 * 2f * num10;
				}
				else
				{
					num9 = (1f - num8) * 2f * num10;
				}
			}
			else if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Noise)
			{
				float num11 = 1f - options.waveNoiseInfluence + Mathf.PerlinNoise(0f, num7 * options.waveNoiseScale) * options.waveNoiseInfluence;
				num9 *= num11;
			}
			num9 *= num6;
			if (options.modulation != null)
			{
				for (int j = 0; j < options.modulation.Length; j++)
				{
					float num12 = Mathf.Abs(Mathf.Sin(1.5708f + num7 * (1f / options.modulation[j][0]) * 3.1415927f));
					float num13 = 1f - options.modulation[j][1];
					num12 = options.modulation[j][1] + num13 * num12;
					num9 *= num12;
				}
			}
			array[i] = num9;
		}
		int num14 = array.Length;
		AudioClip audioClip;
		if (options.useSetData)
		{
			audioClip = AudioClip.Create("Generated Audio", num14, 1, options.frequencyRate, false, null, new AudioClip.PCMSetPositionCallback(LeanAudio.OnAudioSetPosition));
			audioClip.SetData(array, 0);
		}
		else
		{
			options.stream = new LeanAudioStream(array);
			audioClip = AudioClip.Create("Generated Audio", num14, 1, options.frequencyRate, false, new AudioClip.PCMReaderCallback(options.stream.OnAudioRead), new AudioClip.PCMSetPositionCallback(options.stream.OnAudioSetPosition));
			options.stream.audioClip = audioClip;
		}
		return audioClip;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004095 File Offset: 0x00002295
	private static void OnAudioSetPosition(int newPosition)
	{
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004098 File Offset: 0x00002298
	public static AudioClip generateAudioFromCurve(AnimationCurve curve, int frequencyRate = 44100)
	{
		float time = curve[curve.length - 1].time;
		float[] array = new float[(int)((float)frequencyRate * time)];
		for (int i = 0; i < array.Length; i++)
		{
			float num = (float)i / (float)frequencyRate;
			array[i] = curve.Evaluate(num);
		}
		int num2 = array.Length;
		AudioClip audioClip = AudioClip.Create("Generated Audio", num2, 1, frequencyRate, false);
		audioClip.SetData(array, 0);
		return audioClip;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004108 File Offset: 0x00002308
	public static AudioSource play(AudioClip audio, float volume)
	{
		AudioSource audioSource = LeanAudio.playClipAt(audio, Vector3.zero);
		audioSource.volume = volume;
		return audioSource;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0000411C File Offset: 0x0000231C
	public static AudioSource play(AudioClip audio)
	{
		return LeanAudio.playClipAt(audio, Vector3.zero);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004129 File Offset: 0x00002329
	public static AudioSource play(AudioClip audio, Vector3 pos)
	{
		return LeanAudio.playClipAt(audio, pos);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004132 File Offset: 0x00002332
	public static AudioSource play(AudioClip audio, Vector3 pos, float volume)
	{
		AudioSource audioSource = LeanAudio.playClipAt(audio, pos);
		audioSource.minDistance = 1f;
		audioSource.volume = volume;
		return audioSource;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004150 File Offset: 0x00002350
	public static AudioSource playClipAt(AudioClip clip, Vector3 pos)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.position = pos;
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.Play();
		Object.Destroy(gameObject, clip.length);
		return audioSource;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004190 File Offset: 0x00002390
	public static void printOutAudioClip(AudioClip audioClip, ref AnimationCurve curve, float scaleX = 1f)
	{
		float[] array = new float[audioClip.samples * audioClip.channels];
		audioClip.GetData(array, 0);
		int i = 0;
		Keyframe[] array2 = new Keyframe[array.Length];
		while (i < array.Length)
		{
			array2[i] = new Keyframe((float)i * scaleX, array[i]);
			i++;
		}
		curve = new AnimationCurve(array2);
	}

	// Token: 0x04000057 RID: 87
	public static float MIN_FREQEUNCY_PERIOD = 0.000115f;

	// Token: 0x04000058 RID: 88
	public static int PROCESSING_ITERATIONS_MAX = 50000;

	// Token: 0x04000059 RID: 89
	public static float[] generatedWaveDistances;

	// Token: 0x0400005A RID: 90
	public static int generatedWaveDistancesCount = 0;

	// Token: 0x0400005B RID: 91
	private static float[] longList;
}
