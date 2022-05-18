using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class SfxrSynth
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600036C RID: 876 RVA: 0x000073E3 File Offset: 0x000055E3
	// (set) Token: 0x0600036D RID: 877 RVA: 0x000073EB File Offset: 0x000055EB
	public SfxrParams parameters
	{
		get
		{
			return this._params;
		}
		set
		{
			this._params = value;
			this._params.paramsDirty = true;
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x000688A8 File Offset: 0x00066AA8
	public void Play()
	{
		if (this._cachingAsync)
		{
			return;
		}
		this.Stop();
		this._mutation = false;
		if (this._params.paramsDirty || this._cachingNormal || this._cachedWave == null)
		{
			this._cachedWavePos = 0U;
			this._cachingNormal = true;
			this._waveData = null;
			this.Reset(true);
			this._cachedWave = new float[this._envelopeFullLength];
		}
		else
		{
			this._waveData = this._cachedWave;
			this._waveDataPos = 0U;
		}
		this.createGameObject();
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00068930 File Offset: 0x00066B30
	public void PlayMutated(float __mutationAmount = 0.05f, uint __mutationsNum = 15U)
	{
		this.Stop();
		if (this._cachingAsync)
		{
			return;
		}
		this._mutation = true;
		this._cachedMutationsNum = __mutationsNum;
		if (this._params.paramsDirty || this._cachedMutations == null)
		{
			this._cachedMutations = new float[this._cachedMutationsNum][];
			this._cachingMutation = 0;
		}
		if (this._cachingMutation != -1)
		{
			this.Reset(true);
			this._cachedMutation = new float[this._envelopeFullLength];
			this._cachedMutationPos = 0U;
			this._cachedMutations[this._cachingMutation] = this._cachedMutation;
			this._waveData = null;
			this._original = this._params.Clone();
			this._params.Mutate(__mutationAmount);
			this.Reset(true);
		}
		else
		{
			this._waveData = this._cachedMutations[(int)((uint)((float)this._cachedMutations.Length * this.getRandom()))];
			this._waveDataPos = 0U;
		}
		this.createGameObject();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00068A1C File Offset: 0x00066C1C
	public void Stop()
	{
		if (this._audioPlayer != null)
		{
			this._audioPlayer.Destroy();
			this._audioPlayer = null;
		}
		if (this._original != null)
		{
			this._params.CopyFrom(this._original, false);
			this._original = null;
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00068A6C File Offset: 0x00066C6C
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

	// Token: 0x06000372 RID: 882 RVA: 0x00068ABC File Offset: 0x00066CBC
	public bool GenerateAudioFilterData(float[] __data, int __channels)
	{
		bool flag = false;
		if (this._waveData != null)
		{
			int num = this.WriteSamples(this._waveData, (int)this._waveDataPos, __data, __channels);
			this._waveDataPos += (uint)num;
			if (num == 0)
			{
				flag = true;
			}
		}
		else if (this._mutation)
		{
			if (this._original != null)
			{
				this._waveDataPos = this._cachedMutationPos;
				int num2 = (int)Mathf.Min((float)(__data.Length / __channels), (float)((long)this._cachedMutation.Length - (long)((ulong)this._cachedMutationPos)));
				if (this.SynthWave(this._cachedMutation, (int)this._cachedMutationPos, (uint)num2) || num2 == 0)
				{
					this._params.CopyFrom(this._original, false);
					this._original = null;
					this._cachingMutation++;
					flag = true;
					if ((long)this._cachingMutation >= (long)((ulong)this._cachedMutationsNum))
					{
						this._cachingMutation = -1;
					}
				}
				else
				{
					this._cachedMutationPos += (uint)num2;
				}
				this.WriteSamples(this._cachedMutation, (int)this._waveDataPos, __data, __channels);
			}
		}
		else if (this._cachingNormal)
		{
			this._waveDataPos = this._cachedWavePos;
			int num3 = (int)Mathf.Min((float)(__data.Length / __channels), (float)((long)this._cachedWave.Length - (long)((ulong)this._cachedWavePos)));
			if (this.SynthWave(this._cachedWave, (int)this._cachedWavePos, (uint)num3) || num3 == 0)
			{
				this._cachingNormal = false;
				flag = true;
			}
			else
			{
				this._cachedWavePos += (uint)num3;
			}
			this.WriteSamples(this._cachedWave, (int)this._waveDataPos, __data, __channels);
		}
		return !flag;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00068C40 File Offset: 0x00066E40
	public void CacheSound(Action __callback = null, bool __isFromCoroutine = false)
	{
		this.Stop();
		if (this._cachingAsync && !__isFromCoroutine)
		{
			return;
		}
		if (__callback != null)
		{
			this._mutation = false;
			this._cachingNormal = true;
			this._cachingAsync = true;
			new GameObject("SfxrGameObjectSurrogate-" + Time.realtimeSinceStartup).AddComponent<SfxrCacheSurrogate>().CacheSound(this, __callback);
			return;
		}
		this.Reset(true);
		this._cachedWave = new float[this._envelopeFullLength];
		this.SynthWave(this._cachedWave, 0, this._envelopeFullLength);
		this._cachingNormal = false;
		this._cachingAsync = false;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00068CD8 File Offset: 0x00066ED8
	public void CacheMutations(uint __mutationsNum = 15U, float __mutationAmount = 0.05f, Action __callback = null, bool __isFromCoroutine = false)
	{
		this.Stop();
		if (this._cachingAsync && !__isFromCoroutine)
		{
			return;
		}
		this._cachedMutationsNum = __mutationsNum;
		this._cachedMutations = new float[this._cachedMutationsNum][];
		if (__callback != null)
		{
			this._mutation = true;
			this._cachingAsync = true;
			new GameObject("SfxrGameObjectSurrogate-" + Time.realtimeSinceStartup).AddComponent<SfxrCacheSurrogate>().CacheMutations(this, __mutationsNum, __mutationAmount, __callback);
			return;
		}
		this.Reset(true);
		SfxrParams sfxrParams = this._params.Clone();
		for (uint num = 0U; num < this._cachedMutationsNum; num += 1U)
		{
			this._params.Mutate(__mutationAmount);
			this.CacheSound(null, false);
			this._cachedMutations[(int)num] = this._cachedWave;
			this._params.CopyFrom(sfxrParams, false);
		}
		this._cachingAsync = false;
		this._cachingMutation = -1;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00007400 File Offset: 0x00005600
	public void SetParentTransform(Transform __transform)
	{
		this._parentTransform = __transform;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00068DAC File Offset: 0x00066FAC
	public byte[] GetWavFile(uint __sampleRate = 44100U, uint __bitDepth = 16U)
	{
		this.Stop();
		this.Reset(true);
		if (__sampleRate != 44100U)
		{
			__sampleRate = 22050U;
		}
		if (__bitDepth != 16U)
		{
			__bitDepth = 8U;
		}
		uint num = this._envelopeFullLength;
		if (__bitDepth == 16U)
		{
			num *= 2U;
		}
		if (__sampleRate == 22050U)
		{
			num /= 2U;
		}
		uint num2 = 36U + num;
		uint num3 = __bitDepth / 8U;
		uint _newUint = __sampleRate * num3;
		byte[] array = new byte[num2 + 8U];
		int num4 = 0;
		this.writeUintToBytes(array, ref num4, 1380533830U, SfxrSynth.Endian.BIG_ENDIAN);
		this.writeUintToBytes(array, ref num4, num2, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeUintToBytes(array, ref num4, 1463899717U, SfxrSynth.Endian.BIG_ENDIAN);
		this.writeUintToBytes(array, ref num4, 1718449184U, SfxrSynth.Endian.BIG_ENDIAN);
		this.writeUintToBytes(array, ref num4, 16U, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeShortToBytes(array, ref num4, 1, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeShortToBytes(array, ref num4, 1, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeUintToBytes(array, ref num4, __sampleRate, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeUintToBytes(array, ref num4, _newUint, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeShortToBytes(array, ref num4, (short)num3, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeShortToBytes(array, ref num4, (short)__bitDepth, SfxrSynth.Endian.LITTLE_ENDIAN);
		this.writeUintToBytes(array, ref num4, 1684108385U, SfxrSynth.Endian.BIG_ENDIAN);
		this.writeUintToBytes(array, ref num4, num, SfxrSynth.Endian.LITTLE_ENDIAN);
		float[] array2 = new float[this._envelopeFullLength];
		this.SynthWave(array2, 0, this._envelopeFullLength);
		int num5 = 0;
		float num6 = 0f;
		for (int i = 0; i < array2.Length; i++)
		{
			num6 += array2[i];
			num5++;
			if (__sampleRate == 44100U || num5 == 2)
			{
				num6 /= (float)num5;
				num5 = 0;
				if (__bitDepth == 16U)
				{
					this.writeShortToBytes(array, ref num4, (short)Math.Round((double)(32000f * num6)), SfxrSynth.Endian.LITTLE_ENDIAN);
				}
				else
				{
					this.writeBytes(array, ref num4, new byte[]
					{
						(byte)(Math.Round((double)(num6 * 127f)) + 128.0)
					}, SfxrSynth.Endian.LITTLE_ENDIAN);
				}
				num6 = 0f;
			}
		}
		return array;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00068F84 File Offset: 0x00067184
	private void Reset(bool __totalReset)
	{
		SfxrParams @params = this._params;
		this._period = 100f / (@params.startFrequency * @params.startFrequency + 0.001f);
		this._maxPeriod = 100f / (@params.minFrequency * @params.minFrequency + 0.001f);
		this._slide = 1f - @params.slide * @params.slide * @params.slide * 0.01f;
		this._deltaSlide = -@params.deltaSlide * @params.deltaSlide * @params.deltaSlide * 1E-06f;
		if (@params.waveType == 0U)
		{
			this._squareDuty = 0.5f - @params.squareDuty * 0.5f;
			this._dutySweep = -@params.dutySweep * 5E-05f;
		}
		this._changePeriod = Mathf.Max(new float[]
		{
			(1f - @params.changeRepeat + 0.1f) / 1.1f
		}) * 20000f + 32f;
		this._changePeriodTime = 0;
		if ((double)@params.changeAmount > 0.0)
		{
			this._changeAmount = 1f - @params.changeAmount * @params.changeAmount * 0.9f;
		}
		else
		{
			this._changeAmount = 1f + @params.changeAmount * @params.changeAmount * 10f;
		}
		this._changeTime = 0;
		this._changeReached = false;
		if (@params.changeSpeed == 1f)
		{
			this._changeLimit = 0;
		}
		else
		{
			this._changeLimit = (int)((1f - @params.changeSpeed) * (1f - @params.changeSpeed) * 20000f + 32f);
		}
		if (@params.changeAmount2 > 0f)
		{
			this._changeAmount2 = 1f - @params.changeAmount2 * @params.changeAmount2 * 0.9f;
		}
		else
		{
			this._changeAmount2 = 1f + @params.changeAmount2 * @params.changeAmount2 * 10f;
		}
		this._changeTime2 = 0;
		this._changeReached2 = false;
		if (@params.changeSpeed2 == 1f)
		{
			this._changeLimit2 = 0;
		}
		else
		{
			this._changeLimit2 = (int)((1f - @params.changeSpeed2) * (1f - @params.changeSpeed2) * 20000f + 32f);
		}
		this._changeLimit = (int)((float)this._changeLimit * ((1f - @params.changeRepeat + 0.1f) / 1.1f));
		this._changeLimit2 = (int)((float)this._changeLimit2 * ((1f - @params.changeRepeat + 0.1f) / 1.1f));
		if (__totalReset)
		{
			@params.paramsDirty = false;
			this._masterVolume = @params.masterVolume * @params.masterVolume;
			this._waveType = @params.waveType;
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
			this._sustainPunch = @params.sustainPunch;
			this._phase = 0;
			this._overtones = (int)(@params.overtones * 10f);
			this._overtoneFalloff = @params.overtoneFalloff;
			this._minFrequency = @params.minFrequency;
			this._bitcrushFreq = 1f - Mathf.Pow(@params.bitCrush, 0.33333334f);
			this._bitcrushFreqSweep = -@params.bitCrushSweep * 1.5E-05f;
			this._bitcrushPhase = 0f;
			this._bitcrushLast = 0f;
			this._compressionFactor = 1f / (1f + 4f * @params.compressionAmount);
			this._filters = ((double)@params.lpFilterCutoff != 1.0 || (double)@params.hpFilterCutoff != 0.0);
			this._lpFilterPos = 0f;
			this._lpFilterDeltaPos = 0f;
			this._lpFilterCutoff = @params.lpFilterCutoff * @params.lpFilterCutoff * @params.lpFilterCutoff * 0.1f;
			this._lpFilterDeltaCutoff = 1f + @params.lpFilterCutoffSweep * 0.0001f;
			this._lpFilterDamping = 5f / (1f + @params.lpFilterResonance * @params.lpFilterResonance * 20f) * (0.01f + this._lpFilterCutoff);
			if (this._lpFilterDamping > 0.8f)
			{
				this._lpFilterDamping = 0.8f;
			}
			this._lpFilterDamping = 1f - this._lpFilterDamping;
			this._lpFilterOn = (@params.lpFilterCutoff != 1f);
			this._hpFilterPos = 0f;
			this._hpFilterCutoff = @params.hpFilterCutoff * @params.hpFilterCutoff * 0.1f;
			this._hpFilterDeltaCutoff = 1f + @params.hpFilterCutoffSweep * 0.0003f;
			this._vibratoPhase = 0f;
			this._vibratoSpeed = @params.vibratoSpeed * @params.vibratoSpeed * 0.01f;
			this._vibratoAmplitude = @params.vibratoDepth * 0.5f;
			this._envelopeVolume = 0f;
			this._envelopeStage = 0;
			this._envelopeTime = 0f;
			this._envelopeLength0 = @params.attackTime * @params.attackTime * 100000f;
			this._envelopeLength1 = @params.sustainTime * @params.sustainTime * 100000f;
			this._envelopeLength2 = @params.decayTime * @params.decayTime * 100000f + 10f;
			this._envelopeLength = this._envelopeLength0;
			this._envelopeFullLength = (uint)(this._envelopeLength0 + this._envelopeLength1 + this._envelopeLength2);
			this._envelopeOverLength0 = 1f / this._envelopeLength0;
			this._envelopeOverLength1 = 1f / this._envelopeLength1;
			this._envelopeOverLength2 = 1f / this._envelopeLength2;
			this._phaser = (@params.phaserOffset != 0f || @params.phaserSweep != 0f);
			this._phaserOffset = @params.phaserOffset * @params.phaserOffset * 1020f;
			if (@params.phaserOffset < 0f)
			{
				this._phaserOffset = -this._phaserOffset;
			}
			this._phaserDeltaOffset = @params.phaserSweep * @params.phaserSweep * @params.phaserSweep * 0.2f;
			this._phaserPos = 0;
			if (this._phaserBuffer == null)
			{
				this._phaserBuffer = new float[1024];
			}
			if (this._noiseBuffer == null)
			{
				this._noiseBuffer = new float[32];
			}
			if (this._pinkNoiseBuffer == null)
			{
				this._pinkNoiseBuffer = new float[32];
			}
			if (this._pinkNumber == null)
			{
				this._pinkNumber = new PinkNumber();
			}
			if (this._loResNoiseBuffer == null)
			{
				this._loResNoiseBuffer = new float[32];
			}
			for (uint num3 = 0U; num3 < 1024U; num3 += 1U)
			{
				this._phaserBuffer[(int)num3] = 0f;
			}
			for (uint num3 = 0U; num3 < 32U; num3 += 1U)
			{
				this._noiseBuffer[(int)num3] = this.getRandom() * 2f - 1f;
			}
			for (uint num3 = 0U; num3 < 32U; num3 += 1U)
			{
				this._pinkNoiseBuffer[(int)num3] = this._pinkNumber.getNextValue();
			}
			for (uint num3 = 0U; num3 < 32U; num3 += 1U)
			{
				this._loResNoiseBuffer[(int)num3] = ((num3 % 8U == 0U) ? (this.getRandom() * 2f - 1f) : this._loResNoiseBuffer[(int)(num3 - 1U)]);
			}
			this._repeatTime = 0;
			if ((double)@params.repeatSpeed == 0.0)
			{
				this._repeatLimit = 0;
				return;
			}
			this._repeatLimit = (int)((1.0 - (double)@params.repeatSpeed) * (1.0 - (double)@params.repeatSpeed) * 20000.0) + 32;
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00069760 File Offset: 0x00067960
	private bool SynthWave(float[] __buffer, int __bufferPos, uint __length)
	{
		this._finished = false;
		for (int i = 0; i < (int)__length; i++)
		{
			if (this._finished)
			{
				return true;
			}
			if (this._repeatLimit != 0)
			{
				int num = this._repeatTime + 1;
				this._repeatTime = num;
				if (num >= this._repeatLimit)
				{
					this._repeatTime = 0;
					this.Reset(false);
				}
			}
			this._changePeriodTime++;
			if ((float)this._changePeriodTime >= this._changePeriod)
			{
				this._changeTime = 0;
				this._changeTime2 = 0;
				this._changePeriodTime = 0;
				if (this._changeReached)
				{
					this._period /= this._changeAmount;
					this._changeReached = false;
				}
				if (this._changeReached2)
				{
					this._period /= this._changeAmount2;
					this._changeReached2 = false;
				}
			}
			if (!this._changeReached)
			{
				int num = this._changeTime + 1;
				this._changeTime = num;
				if (num >= this._changeLimit)
				{
					this._changeReached = true;
					this._period *= this._changeAmount;
				}
			}
			if (!this._changeReached2)
			{
				int num = this._changeTime2 + 1;
				this._changeTime2 = num;
				if (num >= this._changeLimit2)
				{
					this._changeReached2 = true;
					this._period *= this._changeAmount2;
				}
			}
			this._slide += this._deltaSlide;
			this._period *= this._slide;
			if (this._period > this._maxPeriod)
			{
				this._period = this._maxPeriod;
				if (this._minFrequency > 0f)
				{
					this._finished = true;
				}
			}
			this._periodTemp = this._period;
			if (this._vibratoAmplitude > 0f)
			{
				this._vibratoPhase += this._vibratoSpeed;
				this._periodTemp = this._period * (1f + Mathf.Sin(this._vibratoPhase) * this._vibratoAmplitude);
			}
			this._periodTempInt = (int)this._periodTemp;
			if (this._periodTemp < 8f)
			{
				this._periodTemp = (float)(this._periodTempInt = 8);
			}
			if (this._waveType == 0U)
			{
				this._squareDuty += this._dutySweep;
				if ((double)this._squareDuty < 0.0)
				{
					this._squareDuty = 0f;
				}
				else if ((double)this._squareDuty > 0.5)
				{
					this._squareDuty = 0.5f;
				}
			}
			float num2 = this._envelopeTime + 1f;
			this._envelopeTime = num2;
			if (num2 > this._envelopeLength)
			{
				this._envelopeTime = 0f;
				int num = this._envelopeStage + 1;
				this._envelopeStage = num;
				num = num;
				if (num != 1)
				{
					if (num == 2)
					{
						this._envelopeLength = this._envelopeLength2;
					}
				}
				else
				{
					this._envelopeLength = this._envelopeLength1;
				}
			}
			switch (this._envelopeStage)
			{
			case 0:
				this._envelopeVolume = this._envelopeTime * this._envelopeOverLength0;
				break;
			case 1:
				this._envelopeVolume = 1f + (1f - this._envelopeTime * this._envelopeOverLength1) * 2f * this._sustainPunch;
				break;
			case 2:
				this._envelopeVolume = 1f - this._envelopeTime * this._envelopeOverLength2;
				break;
			case 3:
				this._envelopeVolume = 0f;
				this._finished = true;
				break;
			}
			if (this._phaser)
			{
				this._phaserOffset += this._phaserDeltaOffset;
				this._phaserInt = (int)this._phaserOffset;
				if (this._phaserInt < 0)
				{
					this._phaserInt = -this._phaserInt;
				}
				else if (this._phaserInt > 1023)
				{
					this._phaserInt = 1023;
				}
			}
			if (this._filters && this._hpFilterDeltaCutoff != 0f)
			{
				this._hpFilterCutoff *= this._hpFilterDeltaCutoff;
				if (this._hpFilterCutoff < 1E-05f)
				{
					this._hpFilterCutoff = 1E-05f;
				}
				else if (this._hpFilterCutoff > 0.1f)
				{
					this._hpFilterCutoff = 0.1f;
				}
			}
			this._superSample = 0f;
			for (int j = 0; j < 8; j++)
			{
				this._phase++;
				if (this._phase >= this._periodTempInt)
				{
					this._phase %= this._periodTempInt;
					if (this._waveType == 3U)
					{
						for (int k = 0; k < 32; k++)
						{
							this._noiseBuffer[k] = this.getRandom() * 2f - 1f;
						}
					}
					else if (this._waveType == 5U)
					{
						for (int k = 0; k < 32; k++)
						{
							this._pinkNoiseBuffer[k] = this._pinkNumber.getNextValue();
						}
					}
					else if (this._waveType == 6U)
					{
						for (int k = 0; k < 32; k++)
						{
							this._loResNoiseBuffer[k] = ((k % 8 == 0) ? (this.getRandom() * 2f - 1f) : this._loResNoiseBuffer[k - 1]);
						}
					}
				}
				this._sample = 0f;
				float num3 = 0f;
				float num4 = 1f;
				for (int l = 0; l <= this._overtones; l++)
				{
					float num5 = (float)(this._phase * (l + 1)) % this._periodTemp;
					switch (this._waveType)
					{
					case 0U:
						this._sample = ((num5 / this._periodTemp < this._squareDuty) ? 0.5f : -0.5f);
						break;
					case 1U:
						this._sample = 1f - num5 / this._periodTemp * 2f;
						break;
					case 2U:
						this._pos = num5 / this._periodTemp;
						this._pos = ((this._pos > 0.5f) ? ((this._pos - 1f) * 6.2831855f) : (this._pos * 6.2831855f));
						this._sample = ((this._pos < 0f) ? (1.2732395f * this._pos + 0.40528473f * this._pos * this._pos) : (1.2732395f * this._pos - 0.40528473f * this._pos * this._pos));
						this._sample = ((this._sample < 0f) ? (0.225f * (this._sample * -this._sample - this._sample) + this._sample) : (0.225f * (this._sample * this._sample - this._sample) + this._sample));
						break;
					case 3U:
						this._sample = this._noiseBuffer[(int)((uint)(num5 * 32f / (float)this._periodTempInt) % 32U)];
						break;
					case 4U:
						this._sample = Math.Abs(1f - num5 / this._periodTemp * 2f) - 1f;
						break;
					case 5U:
						this._sample = this._pinkNoiseBuffer[(int)((uint)(num5 * 32f / (float)this._periodTempInt) % 32U)];
						break;
					case 6U:
						this._sample = (float)Math.Tan(3.141592653589793 * (double)num5 / (double)this._periodTemp);
						break;
					case 7U:
						this._pos = num5 / this._periodTemp;
						this._pos = ((this._pos > 0.5f) ? ((this._pos - 1f) * 6.2831855f) : (this._pos * 6.2831855f));
						this._sample = ((this._pos < 0f) ? (1.2732395f * this._pos + 0.40528473f * this._pos * this._pos) : (1.2732395f * this._pos - 0.40528473f * this._pos * this._pos));
						this._sample = 0.75f * ((this._sample < 0f) ? (0.225f * (this._sample * -this._sample - this._sample) + this._sample) : (0.225f * (this._sample * this._sample - this._sample) + this._sample));
						this._pos = num5 * 20f % this._periodTemp / this._periodTemp;
						this._pos = ((this._pos > 0.5f) ? ((this._pos - 1f) * 6.2831855f) : (this._pos * 6.2831855f));
						this._sample2 = ((this._pos < 0f) ? (1.2732395f * this._pos + 0.40528473f * this._pos * this._pos) : (1.2732395f * this._pos - 0.40528473f * this._pos * this._pos));
						this._sample += 0.25f * ((this._sample2 < 0f) ? (0.225f * (this._sample2 * -this._sample2 - this._sample2) + this._sample2) : (0.225f * (this._sample2 * this._sample2 - this._sample2) + this._sample2));
						break;
					case 8U:
						this.amp = num5 / this._periodTemp;
						this._sample = Math.Abs(1f - this.amp * this.amp * 2f) - 1f;
						break;
					}
					num3 += num4 * this._sample;
					num4 *= 1f - this._overtoneFalloff;
				}
				this._sample = num3;
				if (this._filters)
				{
					this._lpFilterOldPos = this._lpFilterPos;
					this._lpFilterCutoff *= this._lpFilterDeltaCutoff;
					if ((double)this._lpFilterCutoff < 0.0)
					{
						this._lpFilterCutoff = 0f;
					}
					else if ((double)this._lpFilterCutoff > 0.1)
					{
						this._lpFilterCutoff = 0.1f;
					}
					if (this._lpFilterOn)
					{
						this._lpFilterDeltaPos += (this._sample - this._lpFilterPos) * this._lpFilterCutoff;
						this._lpFilterDeltaPos *= this._lpFilterDamping;
					}
					else
					{
						this._lpFilterPos = this._sample;
						this._lpFilterDeltaPos = 0f;
					}
					this._lpFilterPos += this._lpFilterDeltaPos;
					this._hpFilterPos += this._lpFilterPos - this._lpFilterOldPos;
					this._hpFilterPos *= 1f - this._hpFilterCutoff;
					this._sample = this._hpFilterPos;
				}
				if (this._phaser)
				{
					this._phaserBuffer[this._phaserPos & 1023] = this._sample;
					this._sample += this._phaserBuffer[this._phaserPos - this._phaserInt + 1024 & 1023];
					this._phaserPos = (this._phaserPos + 1 & 1023);
				}
				this._superSample += this._sample;
			}
			this._superSample = this._masterVolume * this._envelopeVolume * this._superSample * 0.125f;
			this._bitcrushPhase += this._bitcrushFreq;
			if (this._bitcrushPhase > 1f)
			{
				this._bitcrushPhase = 0f;
				this._bitcrushLast = this._superSample;
			}
			this._bitcrushFreq = Mathf.Max(Mathf.Min(this._bitcrushFreq + this._bitcrushFreqSweep, 1f), 0f);
			this._superSample = this._bitcrushLast;
			if (this._superSample > 0f)
			{
				this._superSample = Mathf.Pow(this._superSample, this._compressionFactor);
			}
			else
			{
				this._superSample = -Mathf.Pow(-this._superSample, this._compressionFactor);
			}
			if (this._superSample < -1f)
			{
				this._superSample = -1f;
			}
			else if (this._superSample > 1f)
			{
				this._superSample = 1f;
			}
			__buffer[i + __bufferPos] = this._superSample;
		}
		return false;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0006A3B8 File Offset: 0x000685B8
	private void createGameObject()
	{
		this._gameObject = new GameObject("SfxrGameObject-" + Time.realtimeSinceStartup);
		this.fixGameObjectParent();
		this._audioPlayer = this._gameObject.AddComponent<SfxrAudioPlayer>();
		this._audioPlayer.SetSfxrSynth(this);
		this._audioPlayer.SetRunningInEditMode(Application.isEditor && !Application.isPlaying);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0006A424 File Offset: 0x00068624
	private void fixGameObjectParent()
	{
		Transform transform = this._parentTransform;
		if (transform == null)
		{
			transform = Camera.main.transform;
		}
		if (transform != null)
		{
			this._gameObject.transform.parent = transform;
		}
		this._gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00007409 File Offset: 0x00005609
	private float getRandom()
	{
		return (float)(this.randomGenerator.NextDouble() % 1.0);
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00007421 File Offset: 0x00005621
	private void writeShortToBytes(byte[] __bytes, ref int __position, short __newShort, SfxrSynth.Endian __endian)
	{
		this.writeBytes(__bytes, ref __position, new byte[]
		{
			(byte)(__newShort >> 8 & 255),
			(byte)(__newShort & 255)
		}, __endian);
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0006A48C File Offset: 0x0006868C
	private void writeUintToBytes(byte[] __bytes, ref int __position, uint __newUint, SfxrSynth.Endian __endian)
	{
		this.writeBytes(__bytes, ref __position, new byte[]
		{
			(byte)(__newUint >> 24 & 255U),
			(byte)(__newUint >> 16 & 255U),
			(byte)(__newUint >> 8 & 255U),
			(byte)(__newUint & 255U)
		}, __endian);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0006A4E0 File Offset: 0x000686E0
	private void writeBytes(byte[] __bytes, ref int __position, byte[] __newBytes, SfxrSynth.Endian __endian)
	{
		for (int i = 0; i < __newBytes.Length; i++)
		{
			__bytes[__position] = __newBytes[(__endian == SfxrSynth.Endian.BIG_ENDIAN) ? i : (__newBytes.Length - i - 1)];
			__position++;
		}
	}

	// Token: 0x040001B8 RID: 440
	private const int LO_RES_NOISE_PERIOD = 8;

	// Token: 0x040001B9 RID: 441
	private SfxrParams _params = new SfxrParams();

	// Token: 0x040001BA RID: 442
	private GameObject _gameObject;

	// Token: 0x040001BB RID: 443
	private SfxrAudioPlayer _audioPlayer;

	// Token: 0x040001BC RID: 444
	private Transform _parentTransform;

	// Token: 0x040001BD RID: 445
	private bool _mutation;

	// Token: 0x040001BE RID: 446
	private float[] _cachedWave;

	// Token: 0x040001BF RID: 447
	private uint _cachedWavePos;

	// Token: 0x040001C0 RID: 448
	private bool _cachingNormal;

	// Token: 0x040001C1 RID: 449
	private int _cachingMutation;

	// Token: 0x040001C2 RID: 450
	private float[] _cachedMutation;

	// Token: 0x040001C3 RID: 451
	private uint _cachedMutationPos;

	// Token: 0x040001C4 RID: 452
	private float[][] _cachedMutations;

	// Token: 0x040001C5 RID: 453
	private uint _cachedMutationsNum;

	// Token: 0x040001C6 RID: 454
	private float _cachedMutationAmount;

	// Token: 0x040001C7 RID: 455
	private bool _cachingAsync;

	// Token: 0x040001C8 RID: 456
	private float[] _waveData;

	// Token: 0x040001C9 RID: 457
	private uint _waveDataPos;

	// Token: 0x040001CA RID: 458
	private SfxrParams _original;

	// Token: 0x040001CB RID: 459
	private bool _finished;

	// Token: 0x040001CC RID: 460
	private float _masterVolume;

	// Token: 0x040001CD RID: 461
	private uint _waveType;

	// Token: 0x040001CE RID: 462
	private float _envelopeVolume;

	// Token: 0x040001CF RID: 463
	private int _envelopeStage;

	// Token: 0x040001D0 RID: 464
	private float _envelopeTime;

	// Token: 0x040001D1 RID: 465
	private float _envelopeLength;

	// Token: 0x040001D2 RID: 466
	private float _envelopeLength0;

	// Token: 0x040001D3 RID: 467
	private float _envelopeLength1;

	// Token: 0x040001D4 RID: 468
	private float _envelopeLength2;

	// Token: 0x040001D5 RID: 469
	private float _envelopeOverLength0;

	// Token: 0x040001D6 RID: 470
	private float _envelopeOverLength1;

	// Token: 0x040001D7 RID: 471
	private float _envelopeOverLength2;

	// Token: 0x040001D8 RID: 472
	private uint _envelopeFullLength;

	// Token: 0x040001D9 RID: 473
	private float _sustainPunch;

	// Token: 0x040001DA RID: 474
	private int _phase;

	// Token: 0x040001DB RID: 475
	private float _pos;

	// Token: 0x040001DC RID: 476
	private float _period;

	// Token: 0x040001DD RID: 477
	private float _periodTemp;

	// Token: 0x040001DE RID: 478
	private int _periodTempInt;

	// Token: 0x040001DF RID: 479
	private float _maxPeriod;

	// Token: 0x040001E0 RID: 480
	private float _slide;

	// Token: 0x040001E1 RID: 481
	private float _deltaSlide;

	// Token: 0x040001E2 RID: 482
	private float _minFrequency;

	// Token: 0x040001E3 RID: 483
	private float _vibratoPhase;

	// Token: 0x040001E4 RID: 484
	private float _vibratoSpeed;

	// Token: 0x040001E5 RID: 485
	private float _vibratoAmplitude;

	// Token: 0x040001E6 RID: 486
	private float _changeAmount;

	// Token: 0x040001E7 RID: 487
	private int _changeTime;

	// Token: 0x040001E8 RID: 488
	private int _changeLimit;

	// Token: 0x040001E9 RID: 489
	private float _squareDuty;

	// Token: 0x040001EA RID: 490
	private float _dutySweep;

	// Token: 0x040001EB RID: 491
	private int _repeatTime;

	// Token: 0x040001EC RID: 492
	private int _repeatLimit;

	// Token: 0x040001ED RID: 493
	private bool _phaser;

	// Token: 0x040001EE RID: 494
	private float _phaserOffset;

	// Token: 0x040001EF RID: 495
	private float _phaserDeltaOffset;

	// Token: 0x040001F0 RID: 496
	private int _phaserInt;

	// Token: 0x040001F1 RID: 497
	private int _phaserPos;

	// Token: 0x040001F2 RID: 498
	private float[] _phaserBuffer;

	// Token: 0x040001F3 RID: 499
	private bool _filters;

	// Token: 0x040001F4 RID: 500
	private float _lpFilterPos;

	// Token: 0x040001F5 RID: 501
	private float _lpFilterOldPos;

	// Token: 0x040001F6 RID: 502
	private float _lpFilterDeltaPos;

	// Token: 0x040001F7 RID: 503
	private float _lpFilterCutoff;

	// Token: 0x040001F8 RID: 504
	private float _lpFilterDeltaCutoff;

	// Token: 0x040001F9 RID: 505
	private float _lpFilterDamping;

	// Token: 0x040001FA RID: 506
	private bool _lpFilterOn;

	// Token: 0x040001FB RID: 507
	private float _hpFilterPos;

	// Token: 0x040001FC RID: 508
	private float _hpFilterCutoff;

	// Token: 0x040001FD RID: 509
	private float _hpFilterDeltaCutoff;

	// Token: 0x040001FE RID: 510
	private float _changePeriod;

	// Token: 0x040001FF RID: 511
	private int _changePeriodTime;

	// Token: 0x04000200 RID: 512
	private bool _changeReached;

	// Token: 0x04000201 RID: 513
	private float _changeAmount2;

	// Token: 0x04000202 RID: 514
	private int _changeTime2;

	// Token: 0x04000203 RID: 515
	private int _changeLimit2;

	// Token: 0x04000204 RID: 516
	private bool _changeReached2;

	// Token: 0x04000205 RID: 517
	private int _overtones;

	// Token: 0x04000206 RID: 518
	private float _overtoneFalloff;

	// Token: 0x04000207 RID: 519
	private float _bitcrushFreq;

	// Token: 0x04000208 RID: 520
	private float _bitcrushFreqSweep;

	// Token: 0x04000209 RID: 521
	private float _bitcrushPhase;

	// Token: 0x0400020A RID: 522
	private float _bitcrushLast;

	// Token: 0x0400020B RID: 523
	private float _compressionFactor;

	// Token: 0x0400020C RID: 524
	private float[] _noiseBuffer;

	// Token: 0x0400020D RID: 525
	private float[] _pinkNoiseBuffer;

	// Token: 0x0400020E RID: 526
	private PinkNumber _pinkNumber;

	// Token: 0x0400020F RID: 527
	private float[] _loResNoiseBuffer;

	// Token: 0x04000210 RID: 528
	private float _superSample;

	// Token: 0x04000211 RID: 529
	private float _sample;

	// Token: 0x04000212 RID: 530
	private float _sample2;

	// Token: 0x04000213 RID: 531
	private float amp;

	// Token: 0x04000214 RID: 532
	private Random randomGenerator = new Random();

	// Token: 0x02000032 RID: 50
	private enum Endian
	{
		// Token: 0x04000216 RID: 534
		BIG_ENDIAN,
		// Token: 0x04000217 RID: 535
		LITTLE_ENDIAN
	}
}
