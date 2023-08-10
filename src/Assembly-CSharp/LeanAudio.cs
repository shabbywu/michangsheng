using System;
using UnityEngine;

public class LeanAudio
{
	public static float MIN_FREQEUNCY_PERIOD = 0.000115f;

	public static int PROCESSING_ITERATIONS_MAX = 50000;

	public static float[] generatedWaveDistances;

	public static int generatedWaveDistancesCount = 0;

	private static float[] longList;

	public static LeanAudioOptions options()
	{
		if (generatedWaveDistances == null)
		{
			generatedWaveDistances = new float[PROCESSING_ITERATIONS_MAX];
			longList = new float[PROCESSING_ITERATIONS_MAX];
		}
		return new LeanAudioOptions();
	}

	public static LeanAudioStream createAudioStream(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		options.useSetData = false;
		createAudioFromWave(createAudioWave(volume, frequency, options), options);
		return options.stream;
	}

	public static AudioClip createAudio(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options = null)
	{
		if (options == null)
		{
			options = new LeanAudioOptions();
		}
		return createAudioFromWave(createAudioWave(volume, frequency, options), options);
	}

	private static int createAudioWave(AnimationCurve volume, AnimationCurve frequency, LeanAudioOptions options)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Keyframe val = volume[volume.length - 1];
		float time = ((Keyframe)(ref val)).time;
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < PROCESSING_ITERATIONS_MAX; i++)
		{
			float num3 = frequency.Evaluate(num2);
			if (num3 < MIN_FREQEUNCY_PERIOD)
			{
				num3 = MIN_FREQEUNCY_PERIOD;
			}
			float num4 = volume.Evaluate(num2 + 0.5f * num3);
			if (options.vibrato != null)
			{
				for (int j = 0; j < options.vibrato.Length; j++)
				{
					float num5 = Mathf.Abs(Mathf.Sin(1.5708f + num2 * (1f / ((Vector3)(ref options.vibrato[j]))[0]) * (float)Math.PI));
					float num6 = 1f - ((Vector3)(ref options.vibrato[j]))[1];
					num5 = ((Vector3)(ref options.vibrato[j]))[1] + num6 * num5;
					num4 *= num5;
				}
			}
			if (num2 + 0.5f * num3 >= time)
			{
				break;
			}
			if (num >= PROCESSING_ITERATIONS_MAX - 1)
			{
				Debug.LogError((object)("LeanAudio has reached it's processing cap. To avoid this error increase the number of iterations ex: LeanAudio.PROCESSING_ITERATIONS_MAX = " + PROCESSING_ITERATIONS_MAX * 2));
				break;
			}
			int num7 = num / 2;
			num2 += num3;
			generatedWaveDistances[num7] = num2;
			longList[num] = num2;
			longList[num + 1] = ((i % 2 == 0) ? (0f - num4) : num4);
			num += 2;
		}
		num += -2;
		generatedWaveDistancesCount = num / 2;
		return num;
	}

	private static AudioClip createAudioFromWave(int waveLength, LeanAudioOptions options)
	{
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_028b: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		float num = longList[waveLength - 2];
		float[] array = new float[(int)((float)options.frequencyRate * num)];
		int num2 = 0;
		float num3 = longList[num2];
		float num4 = 0f;
		_ = longList[num2];
		float num5 = longList[num2 + 1];
		for (int i = 0; i < array.Length; i++)
		{
			float num6 = (float)i / (float)options.frequencyRate;
			if (num6 > longList[num2])
			{
				num4 = longList[num2];
				num2 += 2;
				num3 = longList[num2] - longList[num2 - 2];
				num5 = longList[num2 + 1];
			}
			float num7 = (num6 - num4) / num3;
			float num8 = Mathf.Sin(num7 * (float)Math.PI);
			if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Square)
			{
				if (num8 > 0f)
				{
					num8 = 1f;
				}
				if (num8 < 0f)
				{
					num8 = -1f;
				}
			}
			else if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Sawtooth)
			{
				float num9 = ((num8 > 0f) ? 1f : (-1f));
				num8 = ((!(num7 < 0.5f)) ? ((1f - num7) * 2f * num9) : (num7 * 2f * num9));
			}
			else if (options.waveStyle == LeanAudioOptions.LeanAudioWaveStyle.Noise)
			{
				float num10 = 1f - options.waveNoiseInfluence + Mathf.PerlinNoise(0f, num6 * options.waveNoiseScale) * options.waveNoiseInfluence;
				num8 *= num10;
			}
			num8 *= num5;
			if (options.modulation != null)
			{
				for (int j = 0; j < options.modulation.Length; j++)
				{
					float num11 = Mathf.Abs(Mathf.Sin(1.5708f + num6 * (1f / ((Vector3)(ref options.modulation[j]))[0]) * (float)Math.PI));
					float num12 = 1f - ((Vector3)(ref options.modulation[j]))[1];
					num11 = ((Vector3)(ref options.modulation[j]))[1] + num12 * num11;
					num8 *= num11;
				}
			}
			array[i] = num8;
		}
		int num13 = array.Length;
		AudioClip val = null;
		if (options.useSetData)
		{
			val = AudioClip.Create("Generated Audio", num13, 1, options.frequencyRate, false, (PCMReaderCallback)null, new PCMSetPositionCallback(OnAudioSetPosition));
			val.SetData(array, 0);
		}
		else
		{
			options.stream = new LeanAudioStream(array);
			val = AudioClip.Create("Generated Audio", num13, 1, options.frequencyRate, false, new PCMReaderCallback(options.stream.OnAudioRead), new PCMSetPositionCallback(options.stream.OnAudioSetPosition));
			options.stream.audioClip = val;
		}
		return val;
	}

	private static void OnAudioSetPosition(int newPosition)
	{
	}

	public static AudioClip generateAudioFromCurve(AnimationCurve curve, int frequencyRate = 44100)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Keyframe val = curve[curve.length - 1];
		float time = ((Keyframe)(ref val)).time;
		float[] array = new float[(int)((float)frequencyRate * time)];
		for (int i = 0; i < array.Length; i++)
		{
			float num = (float)i / (float)frequencyRate;
			array[i] = curve.Evaluate(num);
		}
		int num2 = array.Length;
		AudioClip obj = AudioClip.Create("Generated Audio", num2, 1, frequencyRate, false);
		obj.SetData(array, 0);
		return obj;
	}

	public static AudioSource play(AudioClip audio, float volume)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		AudioSource obj = playClipAt(audio, Vector3.zero);
		obj.volume = volume;
		return obj;
	}

	public static AudioSource play(AudioClip audio)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return playClipAt(audio, Vector3.zero);
	}

	public static AudioSource play(AudioClip audio, Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return playClipAt(audio, pos);
	}

	public static AudioSource play(AudioClip audio, Vector3 pos, float volume)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		AudioSource obj = playClipAt(audio, pos);
		obj.minDistance = 1f;
		obj.volume = volume;
		return obj;
	}

	public static AudioSource playClipAt(AudioClip clip, Vector3 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject();
		val.transform.position = pos;
		AudioSource obj = val.AddComponent<AudioSource>();
		obj.clip = clip;
		obj.Play();
		Object.Destroy((Object)(object)val, clip.length);
		return obj;
	}

	public static void printOutAudioClip(AudioClip audioClip, ref AnimationCurve curve, float scaleX = 1f)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		float[] array = new float[audioClip.samples * audioClip.channels];
		audioClip.GetData(array, 0);
		int i = 0;
		Keyframe[] array2 = (Keyframe[])(object)new Keyframe[array.Length];
		for (; i < array.Length; i++)
		{
			array2[i] = new Keyframe((float)i * scaleX, array[i]);
		}
		curve = new AnimationCurve(array2);
	}
}
