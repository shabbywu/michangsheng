using System;
using UnityEngine;

public class SfxrSynth
{
	private enum Endian
	{
		BIG_ENDIAN,
		LITTLE_ENDIAN
	}

	private const int LO_RES_NOISE_PERIOD = 8;

	private SfxrParams _params = new SfxrParams();

	private GameObject _gameObject;

	private SfxrAudioPlayer _audioPlayer;

	private Transform _parentTransform;

	private bool _mutation;

	private float[] _cachedWave;

	private uint _cachedWavePos;

	private bool _cachingNormal;

	private int _cachingMutation;

	private float[] _cachedMutation;

	private uint _cachedMutationPos;

	private float[][] _cachedMutations;

	private uint _cachedMutationsNum;

	private float _cachedMutationAmount;

	private bool _cachingAsync;

	private float[] _waveData;

	private uint _waveDataPos;

	private SfxrParams _original;

	private bool _finished;

	private float _masterVolume;

	private uint _waveType;

	private float _envelopeVolume;

	private int _envelopeStage;

	private float _envelopeTime;

	private float _envelopeLength;

	private float _envelopeLength0;

	private float _envelopeLength1;

	private float _envelopeLength2;

	private float _envelopeOverLength0;

	private float _envelopeOverLength1;

	private float _envelopeOverLength2;

	private uint _envelopeFullLength;

	private float _sustainPunch;

	private int _phase;

	private float _pos;

	private float _period;

	private float _periodTemp;

	private int _periodTempInt;

	private float _maxPeriod;

	private float _slide;

	private float _deltaSlide;

	private float _minFrequency;

	private float _vibratoPhase;

	private float _vibratoSpeed;

	private float _vibratoAmplitude;

	private float _changeAmount;

	private int _changeTime;

	private int _changeLimit;

	private float _squareDuty;

	private float _dutySweep;

	private int _repeatTime;

	private int _repeatLimit;

	private bool _phaser;

	private float _phaserOffset;

	private float _phaserDeltaOffset;

	private int _phaserInt;

	private int _phaserPos;

	private float[] _phaserBuffer;

	private bool _filters;

	private float _lpFilterPos;

	private float _lpFilterOldPos;

	private float _lpFilterDeltaPos;

	private float _lpFilterCutoff;

	private float _lpFilterDeltaCutoff;

	private float _lpFilterDamping;

	private bool _lpFilterOn;

	private float _hpFilterPos;

	private float _hpFilterCutoff;

	private float _hpFilterDeltaCutoff;

	private float _changePeriod;

	private int _changePeriodTime;

	private bool _changeReached;

	private float _changeAmount2;

	private int _changeTime2;

	private int _changeLimit2;

	private bool _changeReached2;

	private int _overtones;

	private float _overtoneFalloff;

	private float _bitcrushFreq;

	private float _bitcrushFreqSweep;

	private float _bitcrushPhase;

	private float _bitcrushLast;

	private float _compressionFactor;

	private float[] _noiseBuffer;

	private float[] _pinkNoiseBuffer;

	private PinkNumber _pinkNumber;

	private float[] _loResNoiseBuffer;

	private float _superSample;

	private float _sample;

	private float _sample2;

	private float amp;

	private Random randomGenerator = new Random();

	public SfxrParams parameters
	{
		get
		{
			return _params;
		}
		set
		{
			_params = value;
			_params.paramsDirty = true;
		}
	}

	public void Play()
	{
		if (!_cachingAsync)
		{
			Stop();
			_mutation = false;
			if (_params.paramsDirty || _cachingNormal || _cachedWave == null)
			{
				_cachedWavePos = 0u;
				_cachingNormal = true;
				_waveData = null;
				Reset(__totalReset: true);
				_cachedWave = new float[_envelopeFullLength];
			}
			else
			{
				_waveData = _cachedWave;
				_waveDataPos = 0u;
			}
			createGameObject();
		}
	}

	public void PlayMutated(float __mutationAmount = 0.05f, uint __mutationsNum = 15u)
	{
		Stop();
		if (!_cachingAsync)
		{
			_mutation = true;
			_cachedMutationsNum = __mutationsNum;
			if (_params.paramsDirty || _cachedMutations == null)
			{
				_cachedMutations = new float[_cachedMutationsNum][];
				_cachingMutation = 0;
			}
			if (_cachingMutation != -1)
			{
				Reset(__totalReset: true);
				_cachedMutation = new float[_envelopeFullLength];
				_cachedMutationPos = 0u;
				_cachedMutations[_cachingMutation] = _cachedMutation;
				_waveData = null;
				_original = _params.Clone();
				_params.Mutate(__mutationAmount);
				Reset(__totalReset: true);
			}
			else
			{
				_waveData = _cachedMutations[(uint)((float)_cachedMutations.Length * getRandom())];
				_waveDataPos = 0u;
			}
			createGameObject();
		}
	}

	public void Stop()
	{
		if ((Object)(object)_audioPlayer != (Object)null)
		{
			_audioPlayer.Destroy();
			_audioPlayer = null;
		}
		if (_original != null)
		{
			_params.CopyFrom(_original);
			_original = null;
		}
	}

	private int WriteSamples(float[] __originSamples, int __originPos, float[] __targetSamples, int __targetChannels)
	{
		int num = __targetSamples.Length / __targetChannels;
		if (__originPos + num > __originSamples.Length)
		{
			num = __originSamples.Length - __originPos;
		}
		if (num > 0)
		{
			for (int i = 0; i < __targetChannels; i++)
			{
				for (int j = 0; j < num; j++)
				{
					__targetSamples[j * __targetChannels + i] = __originSamples[j + __originPos];
				}
			}
		}
		return num;
	}

	public bool GenerateAudioFilterData(float[] __data, int __channels)
	{
		bool flag = false;
		if (_waveData != null)
		{
			int num = WriteSamples(_waveData, (int)_waveDataPos, __data, __channels);
			_waveDataPos += (uint)num;
			if (num == 0)
			{
				flag = true;
			}
		}
		else if (_mutation)
		{
			if (_original != null)
			{
				_waveDataPos = _cachedMutationPos;
				int num2 = (int)Mathf.Min((float)(__data.Length / __channels), (float)(_cachedMutation.Length - _cachedMutationPos));
				if (SynthWave(_cachedMutation, (int)_cachedMutationPos, (uint)num2) || num2 == 0)
				{
					_params.CopyFrom(_original);
					_original = null;
					_cachingMutation++;
					flag = true;
					if (_cachingMutation >= _cachedMutationsNum)
					{
						_cachingMutation = -1;
					}
				}
				else
				{
					_cachedMutationPos += (uint)num2;
				}
				WriteSamples(_cachedMutation, (int)_waveDataPos, __data, __channels);
			}
		}
		else if (_cachingNormal)
		{
			_waveDataPos = _cachedWavePos;
			int num3 = (int)Mathf.Min((float)(__data.Length / __channels), (float)(_cachedWave.Length - _cachedWavePos));
			if (SynthWave(_cachedWave, (int)_cachedWavePos, (uint)num3) || num3 == 0)
			{
				_cachingNormal = false;
				flag = true;
			}
			else
			{
				_cachedWavePos += (uint)num3;
			}
			WriteSamples(_cachedWave, (int)_waveDataPos, __data, __channels);
		}
		return !flag;
	}

	public void CacheSound(Action __callback = null, bool __isFromCoroutine = false)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Stop();
		if (!_cachingAsync || __isFromCoroutine)
		{
			if (__callback != null)
			{
				_mutation = false;
				_cachingNormal = true;
				_cachingAsync = true;
				new GameObject("SfxrGameObjectSurrogate-" + Time.realtimeSinceStartup).AddComponent<SfxrCacheSurrogate>().CacheSound(this, __callback);
			}
			else
			{
				Reset(__totalReset: true);
				_cachedWave = new float[_envelopeFullLength];
				SynthWave(_cachedWave, 0, _envelopeFullLength);
				_cachingNormal = false;
				_cachingAsync = false;
			}
		}
	}

	public void CacheMutations(uint __mutationsNum = 15u, float __mutationAmount = 0.05f, Action __callback = null, bool __isFromCoroutine = false)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Stop();
		if (_cachingAsync && !__isFromCoroutine)
		{
			return;
		}
		_cachedMutationsNum = __mutationsNum;
		_cachedMutations = new float[_cachedMutationsNum][];
		if (__callback != null)
		{
			_mutation = true;
			_cachingAsync = true;
			new GameObject("SfxrGameObjectSurrogate-" + Time.realtimeSinceStartup).AddComponent<SfxrCacheSurrogate>().CacheMutations(this, __mutationsNum, __mutationAmount, __callback);
			return;
		}
		Reset(__totalReset: true);
		SfxrParams sfxrParams = _params.Clone();
		for (uint num = 0u; num < _cachedMutationsNum; num++)
		{
			_params.Mutate(__mutationAmount);
			CacheSound();
			_cachedMutations[num] = _cachedWave;
			_params.CopyFrom(sfxrParams);
		}
		_cachingAsync = false;
		_cachingMutation = -1;
	}

	public void SetParentTransform(Transform __transform)
	{
		_parentTransform = __transform;
	}

	public byte[] GetWavFile(uint __sampleRate = 44100u, uint __bitDepth = 16u)
	{
		Stop();
		Reset(__totalReset: true);
		if (__sampleRate != 44100)
		{
			__sampleRate = 22050u;
		}
		if (__bitDepth != 16)
		{
			__bitDepth = 8u;
		}
		uint num = _envelopeFullLength;
		if (__bitDepth == 16)
		{
			num *= 2;
		}
		if (__sampleRate == 22050)
		{
			num /= 2;
		}
		uint num2 = 36 + num;
		uint num3 = __bitDepth / 8;
		uint _newUint = __sampleRate * num3;
		byte[] array = new byte[num2 + 8];
		int __position = 0;
		writeUintToBytes(array, ref __position, 1380533830u, Endian.BIG_ENDIAN);
		writeUintToBytes(array, ref __position, num2, Endian.LITTLE_ENDIAN);
		writeUintToBytes(array, ref __position, 1463899717u, Endian.BIG_ENDIAN);
		writeUintToBytes(array, ref __position, 1718449184u, Endian.BIG_ENDIAN);
		writeUintToBytes(array, ref __position, 16u, Endian.LITTLE_ENDIAN);
		writeShortToBytes(array, ref __position, 1, Endian.LITTLE_ENDIAN);
		writeShortToBytes(array, ref __position, 1, Endian.LITTLE_ENDIAN);
		writeUintToBytes(array, ref __position, __sampleRate, Endian.LITTLE_ENDIAN);
		writeUintToBytes(array, ref __position, _newUint, Endian.LITTLE_ENDIAN);
		writeShortToBytes(array, ref __position, (short)num3, Endian.LITTLE_ENDIAN);
		writeShortToBytes(array, ref __position, (short)__bitDepth, Endian.LITTLE_ENDIAN);
		writeUintToBytes(array, ref __position, 1684108385u, Endian.BIG_ENDIAN);
		writeUintToBytes(array, ref __position, num, Endian.LITTLE_ENDIAN);
		float[] array2 = new float[_envelopeFullLength];
		SynthWave(array2, 0, _envelopeFullLength);
		int num4 = 0;
		float num5 = 0f;
		for (int i = 0; i < array2.Length; i++)
		{
			num5 += array2[i];
			num4++;
			if (__sampleRate == 44100 || num4 == 2)
			{
				num5 /= (float)num4;
				num4 = 0;
				if (__bitDepth == 16)
				{
					writeShortToBytes(array, ref __position, (short)Math.Round(32000f * num5), Endian.LITTLE_ENDIAN);
				}
				else
				{
					writeBytes(array, ref __position, new byte[1] { (byte)(Math.Round(num5 * 127f) + 128.0) }, Endian.LITTLE_ENDIAN);
				}
				num5 = 0f;
			}
		}
		return array;
	}

	private void Reset(bool __totalReset)
	{
		SfxrParams @params = _params;
		_period = 100f / (@params.startFrequency * @params.startFrequency + 0.001f);
		_maxPeriod = 100f / (@params.minFrequency * @params.minFrequency + 0.001f);
		_slide = 1f - @params.slide * @params.slide * @params.slide * 0.01f;
		_deltaSlide = (0f - @params.deltaSlide) * @params.deltaSlide * @params.deltaSlide * 1E-06f;
		if (@params.waveType == 0)
		{
			_squareDuty = 0.5f - @params.squareDuty * 0.5f;
			_dutySweep = (0f - @params.dutySweep) * 5E-05f;
		}
		_changePeriod = Mathf.Max(new float[1] { (1f - @params.changeRepeat + 0.1f) / 1.1f }) * 20000f + 32f;
		_changePeriodTime = 0;
		if ((double)@params.changeAmount > 0.0)
		{
			_changeAmount = 1f - @params.changeAmount * @params.changeAmount * 0.9f;
		}
		else
		{
			_changeAmount = 1f + @params.changeAmount * @params.changeAmount * 10f;
		}
		_changeTime = 0;
		_changeReached = false;
		if (@params.changeSpeed == 1f)
		{
			_changeLimit = 0;
		}
		else
		{
			_changeLimit = (int)((1f - @params.changeSpeed) * (1f - @params.changeSpeed) * 20000f + 32f);
		}
		if (@params.changeAmount2 > 0f)
		{
			_changeAmount2 = 1f - @params.changeAmount2 * @params.changeAmount2 * 0.9f;
		}
		else
		{
			_changeAmount2 = 1f + @params.changeAmount2 * @params.changeAmount2 * 10f;
		}
		_changeTime2 = 0;
		_changeReached2 = false;
		if (@params.changeSpeed2 == 1f)
		{
			_changeLimit2 = 0;
		}
		else
		{
			_changeLimit2 = (int)((1f - @params.changeSpeed2) * (1f - @params.changeSpeed2) * 20000f + 32f);
		}
		_changeLimit = (int)((float)_changeLimit * ((1f - @params.changeRepeat + 0.1f) / 1.1f));
		_changeLimit2 = (int)((float)_changeLimit2 * ((1f - @params.changeRepeat + 0.1f) / 1.1f));
		if (__totalReset)
		{
			@params.paramsDirty = false;
			_masterVolume = @params.masterVolume * @params.masterVolume;
			_waveType = @params.waveType;
			if ((double)@params.sustainTime < 0.01)
			{
				@params.sustainTime = 0.01f;
			}
			float num = @params.attackTime + @params.sustainTime + @params.decayTime;
			if (num < 0.18f)
			{
				float num2 = 0.18f / num;
				@params.attackTime *= num2;
				@params.sustainTime *= num2;
				@params.decayTime *= num2;
			}
			_sustainPunch = @params.sustainPunch;
			_phase = 0;
			_overtones = (int)(@params.overtones * 10f);
			_overtoneFalloff = @params.overtoneFalloff;
			_minFrequency = @params.minFrequency;
			_bitcrushFreq = 1f - Mathf.Pow(@params.bitCrush, 1f / 3f);
			_bitcrushFreqSweep = (0f - @params.bitCrushSweep) * 1.5E-05f;
			_bitcrushPhase = 0f;
			_bitcrushLast = 0f;
			_compressionFactor = 1f / (1f + 4f * @params.compressionAmount);
			_filters = (double)@params.lpFilterCutoff != 1.0 || (double)@params.hpFilterCutoff != 0.0;
			_lpFilterPos = 0f;
			_lpFilterDeltaPos = 0f;
			_lpFilterCutoff = @params.lpFilterCutoff * @params.lpFilterCutoff * @params.lpFilterCutoff * 0.1f;
			_lpFilterDeltaCutoff = 1f + @params.lpFilterCutoffSweep * 0.0001f;
			_lpFilterDamping = 5f / (1f + @params.lpFilterResonance * @params.lpFilterResonance * 20f) * (0.01f + _lpFilterCutoff);
			if (_lpFilterDamping > 0.8f)
			{
				_lpFilterDamping = 0.8f;
			}
			_lpFilterDamping = 1f - _lpFilterDamping;
			_lpFilterOn = @params.lpFilterCutoff != 1f;
			_hpFilterPos = 0f;
			_hpFilterCutoff = @params.hpFilterCutoff * @params.hpFilterCutoff * 0.1f;
			_hpFilterDeltaCutoff = 1f + @params.hpFilterCutoffSweep * 0.0003f;
			_vibratoPhase = 0f;
			_vibratoSpeed = @params.vibratoSpeed * @params.vibratoSpeed * 0.01f;
			_vibratoAmplitude = @params.vibratoDepth * 0.5f;
			_envelopeVolume = 0f;
			_envelopeStage = 0;
			_envelopeTime = 0f;
			_envelopeLength0 = @params.attackTime * @params.attackTime * 100000f;
			_envelopeLength1 = @params.sustainTime * @params.sustainTime * 100000f;
			_envelopeLength2 = @params.decayTime * @params.decayTime * 100000f + 10f;
			_envelopeLength = _envelopeLength0;
			_envelopeFullLength = (uint)(_envelopeLength0 + _envelopeLength1 + _envelopeLength2);
			_envelopeOverLength0 = 1f / _envelopeLength0;
			_envelopeOverLength1 = 1f / _envelopeLength1;
			_envelopeOverLength2 = 1f / _envelopeLength2;
			_phaser = @params.phaserOffset != 0f || @params.phaserSweep != 0f;
			_phaserOffset = @params.phaserOffset * @params.phaserOffset * 1020f;
			if (@params.phaserOffset < 0f)
			{
				_phaserOffset = 0f - _phaserOffset;
			}
			_phaserDeltaOffset = @params.phaserSweep * @params.phaserSweep * @params.phaserSweep * 0.2f;
			_phaserPos = 0;
			if (_phaserBuffer == null)
			{
				_phaserBuffer = new float[1024];
			}
			if (_noiseBuffer == null)
			{
				_noiseBuffer = new float[32];
			}
			if (_pinkNoiseBuffer == null)
			{
				_pinkNoiseBuffer = new float[32];
			}
			if (_pinkNumber == null)
			{
				_pinkNumber = new PinkNumber();
			}
			if (_loResNoiseBuffer == null)
			{
				_loResNoiseBuffer = new float[32];
			}
			for (uint num3 = 0u; num3 < 1024; num3++)
			{
				_phaserBuffer[num3] = 0f;
			}
			for (uint num3 = 0u; num3 < 32; num3++)
			{
				_noiseBuffer[num3] = getRandom() * 2f - 1f;
			}
			for (uint num3 = 0u; num3 < 32; num3++)
			{
				_pinkNoiseBuffer[num3] = _pinkNumber.getNextValue();
			}
			for (uint num3 = 0u; num3 < 32; num3++)
			{
				_loResNoiseBuffer[num3] = ((num3 % 8 == 0) ? (getRandom() * 2f - 1f) : _loResNoiseBuffer[num3 - 1]);
			}
			_repeatTime = 0;
			if ((double)@params.repeatSpeed == 0.0)
			{
				_repeatLimit = 0;
			}
			else
			{
				_repeatLimit = (int)((1.0 - (double)@params.repeatSpeed) * (1.0 - (double)@params.repeatSpeed) * 20000.0) + 32;
			}
		}
	}

	private bool SynthWave(float[] __buffer, int __bufferPos, uint __length)
	{
		_finished = false;
		for (int i = 0; i < (int)__length; i++)
		{
			if (_finished)
			{
				return true;
			}
			if (_repeatLimit != 0 && ++_repeatTime >= _repeatLimit)
			{
				_repeatTime = 0;
				Reset(__totalReset: false);
			}
			_changePeriodTime++;
			if ((float)_changePeriodTime >= _changePeriod)
			{
				_changeTime = 0;
				_changeTime2 = 0;
				_changePeriodTime = 0;
				if (_changeReached)
				{
					_period /= _changeAmount;
					_changeReached = false;
				}
				if (_changeReached2)
				{
					_period /= _changeAmount2;
					_changeReached2 = false;
				}
			}
			if (!_changeReached && ++_changeTime >= _changeLimit)
			{
				_changeReached = true;
				_period *= _changeAmount;
			}
			if (!_changeReached2 && ++_changeTime2 >= _changeLimit2)
			{
				_changeReached2 = true;
				_period *= _changeAmount2;
			}
			_slide += _deltaSlide;
			_period *= _slide;
			if (_period > _maxPeriod)
			{
				_period = _maxPeriod;
				if (_minFrequency > 0f)
				{
					_finished = true;
				}
			}
			_periodTemp = _period;
			if (_vibratoAmplitude > 0f)
			{
				_vibratoPhase += _vibratoSpeed;
				_periodTemp = _period * (1f + Mathf.Sin(_vibratoPhase) * _vibratoAmplitude);
			}
			_periodTempInt = (int)_periodTemp;
			if (_periodTemp < 8f)
			{
				_periodTemp = (_periodTempInt = 8);
			}
			if (_waveType == 0)
			{
				_squareDuty += _dutySweep;
				if ((double)_squareDuty < 0.0)
				{
					_squareDuty = 0f;
				}
				else if ((double)_squareDuty > 0.5)
				{
					_squareDuty = 0.5f;
				}
			}
			if ((_envelopeTime += 1f) > _envelopeLength)
			{
				_envelopeTime = 0f;
				switch (++_envelopeStage)
				{
				case 1:
					_envelopeLength = _envelopeLength1;
					break;
				case 2:
					_envelopeLength = _envelopeLength2;
					break;
				}
			}
			switch (_envelopeStage)
			{
			case 0:
				_envelopeVolume = _envelopeTime * _envelopeOverLength0;
				break;
			case 1:
				_envelopeVolume = 1f + (1f - _envelopeTime * _envelopeOverLength1) * 2f * _sustainPunch;
				break;
			case 2:
				_envelopeVolume = 1f - _envelopeTime * _envelopeOverLength2;
				break;
			case 3:
				_envelopeVolume = 0f;
				_finished = true;
				break;
			}
			if (_phaser)
			{
				_phaserOffset += _phaserDeltaOffset;
				_phaserInt = (int)_phaserOffset;
				if (_phaserInt < 0)
				{
					_phaserInt = -_phaserInt;
				}
				else if (_phaserInt > 1023)
				{
					_phaserInt = 1023;
				}
			}
			if (_filters && _hpFilterDeltaCutoff != 0f)
			{
				_hpFilterCutoff *= _hpFilterDeltaCutoff;
				if (_hpFilterCutoff < 1E-05f)
				{
					_hpFilterCutoff = 1E-05f;
				}
				else if (_hpFilterCutoff > 0.1f)
				{
					_hpFilterCutoff = 0.1f;
				}
			}
			_superSample = 0f;
			for (int j = 0; j < 8; j++)
			{
				_phase++;
				if (_phase >= _periodTempInt)
				{
					_phase %= _periodTempInt;
					if (_waveType == 3)
					{
						for (int k = 0; k < 32; k++)
						{
							_noiseBuffer[k] = getRandom() * 2f - 1f;
						}
					}
					else if (_waveType == 5)
					{
						for (int k = 0; k < 32; k++)
						{
							_pinkNoiseBuffer[k] = _pinkNumber.getNextValue();
						}
					}
					else if (_waveType == 6)
					{
						for (int k = 0; k < 32; k++)
						{
							_loResNoiseBuffer[k] = ((k % 8 == 0) ? (getRandom() * 2f - 1f) : _loResNoiseBuffer[k - 1]);
						}
					}
				}
				_sample = 0f;
				float num = 0f;
				float num2 = 1f;
				for (int l = 0; l <= _overtones; l++)
				{
					float num3 = (float)(_phase * (l + 1)) % _periodTemp;
					switch (_waveType)
					{
					case 0u:
						_sample = ((num3 / _periodTemp < _squareDuty) ? 0.5f : (-0.5f));
						break;
					case 1u:
						_sample = 1f - num3 / _periodTemp * 2f;
						break;
					case 2u:
						_pos = num3 / _periodTemp;
						_pos = ((_pos > 0.5f) ? ((_pos - 1f) * ((float)Math.PI * 2f)) : (_pos * ((float)Math.PI * 2f)));
						_sample = ((_pos < 0f) ? (4f / (float)Math.PI * _pos + 0.40528473f * _pos * _pos) : (4f / (float)Math.PI * _pos - 0.40528473f * _pos * _pos));
						_sample = ((_sample < 0f) ? (0.225f * (_sample * (0f - _sample) - _sample) + _sample) : (0.225f * (_sample * _sample - _sample) + _sample));
						break;
					case 3u:
						_sample = _noiseBuffer[(uint)(num3 * 32f / (float)_periodTempInt) % 32];
						break;
					case 4u:
						_sample = Math.Abs(1f - num3 / _periodTemp * 2f) - 1f;
						break;
					case 5u:
						_sample = _pinkNoiseBuffer[(uint)(num3 * 32f / (float)_periodTempInt) % 32];
						break;
					case 6u:
						_sample = (float)Math.Tan(Math.PI * (double)num3 / (double)_periodTemp);
						break;
					case 7u:
						_pos = num3 / _periodTemp;
						_pos = ((_pos > 0.5f) ? ((_pos - 1f) * ((float)Math.PI * 2f)) : (_pos * ((float)Math.PI * 2f)));
						_sample = ((_pos < 0f) ? (4f / (float)Math.PI * _pos + 0.40528473f * _pos * _pos) : (4f / (float)Math.PI * _pos - 0.40528473f * _pos * _pos));
						_sample = 0.75f * ((_sample < 0f) ? (0.225f * (_sample * (0f - _sample) - _sample) + _sample) : (0.225f * (_sample * _sample - _sample) + _sample));
						_pos = num3 * 20f % _periodTemp / _periodTemp;
						_pos = ((_pos > 0.5f) ? ((_pos - 1f) * ((float)Math.PI * 2f)) : (_pos * ((float)Math.PI * 2f)));
						_sample2 = ((_pos < 0f) ? (4f / (float)Math.PI * _pos + 0.40528473f * _pos * _pos) : (4f / (float)Math.PI * _pos - 0.40528473f * _pos * _pos));
						_sample += 0.25f * ((_sample2 < 0f) ? (0.225f * (_sample2 * (0f - _sample2) - _sample2) + _sample2) : (0.225f * (_sample2 * _sample2 - _sample2) + _sample2));
						break;
					case 8u:
						amp = num3 / _periodTemp;
						_sample = Math.Abs(1f - amp * amp * 2f) - 1f;
						break;
					}
					num += num2 * _sample;
					num2 *= 1f - _overtoneFalloff;
				}
				_sample = num;
				if (_filters)
				{
					_lpFilterOldPos = _lpFilterPos;
					_lpFilterCutoff *= _lpFilterDeltaCutoff;
					if ((double)_lpFilterCutoff < 0.0)
					{
						_lpFilterCutoff = 0f;
					}
					else if ((double)_lpFilterCutoff > 0.1)
					{
						_lpFilterCutoff = 0.1f;
					}
					if (_lpFilterOn)
					{
						_lpFilterDeltaPos += (_sample - _lpFilterPos) * _lpFilterCutoff;
						_lpFilterDeltaPos *= _lpFilterDamping;
					}
					else
					{
						_lpFilterPos = _sample;
						_lpFilterDeltaPos = 0f;
					}
					_lpFilterPos += _lpFilterDeltaPos;
					_hpFilterPos += _lpFilterPos - _lpFilterOldPos;
					_hpFilterPos *= 1f - _hpFilterCutoff;
					_sample = _hpFilterPos;
				}
				if (_phaser)
				{
					_phaserBuffer[_phaserPos & 0x3FF] = _sample;
					_sample += _phaserBuffer[(_phaserPos - _phaserInt + 1024) & 0x3FF];
					_phaserPos = (_phaserPos + 1) & 0x3FF;
				}
				_superSample += _sample;
			}
			_superSample = _masterVolume * _envelopeVolume * _superSample * 0.125f;
			_bitcrushPhase += _bitcrushFreq;
			if (_bitcrushPhase > 1f)
			{
				_bitcrushPhase = 0f;
				_bitcrushLast = _superSample;
			}
			_bitcrushFreq = Mathf.Max(Mathf.Min(_bitcrushFreq + _bitcrushFreqSweep, 1f), 0f);
			_superSample = _bitcrushLast;
			if (_superSample > 0f)
			{
				_superSample = Mathf.Pow(_superSample, _compressionFactor);
			}
			else
			{
				_superSample = 0f - Mathf.Pow(0f - _superSample, _compressionFactor);
			}
			if (_superSample < -1f)
			{
				_superSample = -1f;
			}
			else if (_superSample > 1f)
			{
				_superSample = 1f;
			}
			__buffer[i + __bufferPos] = _superSample;
		}
		return false;
	}

	private void createGameObject()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		_gameObject = new GameObject("SfxrGameObject-" + Time.realtimeSinceStartup);
		fixGameObjectParent();
		_audioPlayer = _gameObject.AddComponent<SfxrAudioPlayer>();
		_audioPlayer.SetSfxrSynth(this);
		_audioPlayer.SetRunningInEditMode(Application.isEditor && !Application.isPlaying);
	}

	private void fixGameObjectParent()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Transform val = _parentTransform;
		if ((Object)(object)val == (Object)null)
		{
			val = ((Component)Camera.main).transform;
		}
		if ((Object)(object)val != (Object)null)
		{
			_gameObject.transform.parent = val;
		}
		_gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	private float getRandom()
	{
		return (float)(randomGenerator.NextDouble() % 1.0);
	}

	private void writeShortToBytes(byte[] __bytes, ref int __position, short __newShort, Endian __endian)
	{
		writeBytes(__bytes, ref __position, new byte[2]
		{
			(byte)((uint)(__newShort >> 8) & 0xFFu),
			(byte)((uint)__newShort & 0xFFu)
		}, __endian);
	}

	private void writeUintToBytes(byte[] __bytes, ref int __position, uint __newUint, Endian __endian)
	{
		writeBytes(__bytes, ref __position, new byte[4]
		{
			(byte)((__newUint >> 24) & 0xFFu),
			(byte)((__newUint >> 16) & 0xFFu),
			(byte)((__newUint >> 8) & 0xFFu),
			(byte)(__newUint & 0xFFu)
		}, __endian);
	}

	private void writeBytes(byte[] __bytes, ref int __position, byte[] __newBytes, Endian __endian)
	{
		for (int i = 0; i < __newBytes.Length; i++)
		{
			__bytes[__position] = __newBytes[(__endian == Endian.BIG_ENDIAN) ? i : (__newBytes.Length - i - 1)];
			__position++;
		}
	}
}
