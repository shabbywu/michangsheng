using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class SfxrParams
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000316 RID: 790 RVA: 0x00006E45 File Offset: 0x00005045
	// (set) Token: 0x06000317 RID: 791 RVA: 0x00006E4D File Offset: 0x0000504D
	public uint waveType
	{
		get
		{
			return this._waveType;
		}
		set
		{
			this._waveType = ((value > 8U) ? 0U : value);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000318 RID: 792 RVA: 0x00006E64 File Offset: 0x00005064
	// (set) Token: 0x06000319 RID: 793 RVA: 0x00006E6C File Offset: 0x0000506C
	public float masterVolume
	{
		get
		{
			return this._masterVolume;
		}
		set
		{
			this._masterVolume = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600031A RID: 794 RVA: 0x00006E8B File Offset: 0x0000508B
	// (set) Token: 0x0600031B RID: 795 RVA: 0x00006E93 File Offset: 0x00005093
	public float attackTime
	{
		get
		{
			return this._attackTime;
		}
		set
		{
			this._attackTime = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600031C RID: 796 RVA: 0x00006EB2 File Offset: 0x000050B2
	// (set) Token: 0x0600031D RID: 797 RVA: 0x00006EBA File Offset: 0x000050BA
	public float sustainTime
	{
		get
		{
			return this._sustainTime;
		}
		set
		{
			this._sustainTime = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600031E RID: 798 RVA: 0x00006ED9 File Offset: 0x000050D9
	// (set) Token: 0x0600031F RID: 799 RVA: 0x00006EE1 File Offset: 0x000050E1
	public float sustainPunch
	{
		get
		{
			return this._sustainPunch;
		}
		set
		{
			this._sustainPunch = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000320 RID: 800 RVA: 0x00006F00 File Offset: 0x00005100
	// (set) Token: 0x06000321 RID: 801 RVA: 0x00006F08 File Offset: 0x00005108
	public float decayTime
	{
		get
		{
			return this._decayTime;
		}
		set
		{
			this._decayTime = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000322 RID: 802 RVA: 0x00006F27 File Offset: 0x00005127
	// (set) Token: 0x06000323 RID: 803 RVA: 0x00006F2F File Offset: 0x0000512F
	public float startFrequency
	{
		get
		{
			return this._startFrequency;
		}
		set
		{
			this._startFrequency = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000324 RID: 804 RVA: 0x00006F4E File Offset: 0x0000514E
	// (set) Token: 0x06000325 RID: 805 RVA: 0x00006F56 File Offset: 0x00005156
	public float minFrequency
	{
		get
		{
			return this._minFrequency;
		}
		set
		{
			this._minFrequency = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000326 RID: 806 RVA: 0x00006F75 File Offset: 0x00005175
	// (set) Token: 0x06000327 RID: 807 RVA: 0x00006F7D File Offset: 0x0000517D
	public float slide
	{
		get
		{
			return this._slide;
		}
		set
		{
			this._slide = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000328 RID: 808 RVA: 0x00006F9C File Offset: 0x0000519C
	// (set) Token: 0x06000329 RID: 809 RVA: 0x00006FA4 File Offset: 0x000051A4
	public float deltaSlide
	{
		get
		{
			return this._deltaSlide;
		}
		set
		{
			this._deltaSlide = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600032A RID: 810 RVA: 0x00006FC3 File Offset: 0x000051C3
	// (set) Token: 0x0600032B RID: 811 RVA: 0x00006FCB File Offset: 0x000051CB
	public float vibratoDepth
	{
		get
		{
			return this._vibratoDepth;
		}
		set
		{
			this._vibratoDepth = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600032C RID: 812 RVA: 0x00006FEA File Offset: 0x000051EA
	// (set) Token: 0x0600032D RID: 813 RVA: 0x00006FF2 File Offset: 0x000051F2
	public float vibratoSpeed
	{
		get
		{
			return this._vibratoSpeed;
		}
		set
		{
			this._vibratoSpeed = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600032E RID: 814 RVA: 0x00007011 File Offset: 0x00005211
	// (set) Token: 0x0600032F RID: 815 RVA: 0x00007019 File Offset: 0x00005219
	public float changeAmount
	{
		get
		{
			return this._changeAmount;
		}
		set
		{
			this._changeAmount = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000330 RID: 816 RVA: 0x00007038 File Offset: 0x00005238
	// (set) Token: 0x06000331 RID: 817 RVA: 0x00007040 File Offset: 0x00005240
	public float changeSpeed
	{
		get
		{
			return this._changeSpeed;
		}
		set
		{
			this._changeSpeed = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000332 RID: 818 RVA: 0x0000705F File Offset: 0x0000525F
	// (set) Token: 0x06000333 RID: 819 RVA: 0x00007067 File Offset: 0x00005267
	public float squareDuty
	{
		get
		{
			return this._squareDuty;
		}
		set
		{
			this._squareDuty = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000334 RID: 820 RVA: 0x00007086 File Offset: 0x00005286
	// (set) Token: 0x06000335 RID: 821 RVA: 0x0000708E File Offset: 0x0000528E
	public float dutySweep
	{
		get
		{
			return this._dutySweep;
		}
		set
		{
			this._dutySweep = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000336 RID: 822 RVA: 0x000070AD File Offset: 0x000052AD
	// (set) Token: 0x06000337 RID: 823 RVA: 0x000070B5 File Offset: 0x000052B5
	public float repeatSpeed
	{
		get
		{
			return this._repeatSpeed;
		}
		set
		{
			this._repeatSpeed = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000338 RID: 824 RVA: 0x000070D4 File Offset: 0x000052D4
	// (set) Token: 0x06000339 RID: 825 RVA: 0x000070DC File Offset: 0x000052DC
	public float phaserOffset
	{
		get
		{
			return this._phaserOffset;
		}
		set
		{
			this._phaserOffset = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x0600033A RID: 826 RVA: 0x000070FB File Offset: 0x000052FB
	// (set) Token: 0x0600033B RID: 827 RVA: 0x00007103 File Offset: 0x00005303
	public float phaserSweep
	{
		get
		{
			return this._phaserSweep;
		}
		set
		{
			this._phaserSweep = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600033C RID: 828 RVA: 0x00007122 File Offset: 0x00005322
	// (set) Token: 0x0600033D RID: 829 RVA: 0x0000712A File Offset: 0x0000532A
	public float lpFilterCutoff
	{
		get
		{
			return this._lpFilterCutoff;
		}
		set
		{
			this._lpFilterCutoff = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600033E RID: 830 RVA: 0x00007149 File Offset: 0x00005349
	// (set) Token: 0x0600033F RID: 831 RVA: 0x00007151 File Offset: 0x00005351
	public float lpFilterCutoffSweep
	{
		get
		{
			return this._lpFilterCutoffSweep;
		}
		set
		{
			this._lpFilterCutoffSweep = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000340 RID: 832 RVA: 0x00007170 File Offset: 0x00005370
	// (set) Token: 0x06000341 RID: 833 RVA: 0x00007178 File Offset: 0x00005378
	public float lpFilterResonance
	{
		get
		{
			return this._lpFilterResonance;
		}
		set
		{
			this._lpFilterResonance = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000342 RID: 834 RVA: 0x00007197 File Offset: 0x00005397
	// (set) Token: 0x06000343 RID: 835 RVA: 0x0000719F File Offset: 0x0000539F
	public float hpFilterCutoff
	{
		get
		{
			return this._hpFilterCutoff;
		}
		set
		{
			this._hpFilterCutoff = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000344 RID: 836 RVA: 0x000071BE File Offset: 0x000053BE
	// (set) Token: 0x06000345 RID: 837 RVA: 0x000071C6 File Offset: 0x000053C6
	public float hpFilterCutoffSweep
	{
		get
		{
			return this._hpFilterCutoffSweep;
		}
		set
		{
			this._hpFilterCutoffSweep = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000346 RID: 838 RVA: 0x000071E5 File Offset: 0x000053E5
	// (set) Token: 0x06000347 RID: 839 RVA: 0x000071ED File Offset: 0x000053ED
	public float changeRepeat
	{
		get
		{
			return this._changeRepeat;
		}
		set
		{
			this._changeRepeat = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000348 RID: 840 RVA: 0x0000720C File Offset: 0x0000540C
	// (set) Token: 0x06000349 RID: 841 RVA: 0x00007214 File Offset: 0x00005414
	public float changeAmount2
	{
		get
		{
			return this._changeAmount2;
		}
		set
		{
			this._changeAmount2 = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600034A RID: 842 RVA: 0x00007233 File Offset: 0x00005433
	// (set) Token: 0x0600034B RID: 843 RVA: 0x0000723B File Offset: 0x0000543B
	public float changeSpeed2
	{
		get
		{
			return this._changeSpeed2;
		}
		set
		{
			this._changeSpeed2 = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600034C RID: 844 RVA: 0x0000725A File Offset: 0x0000545A
	// (set) Token: 0x0600034D RID: 845 RVA: 0x00007262 File Offset: 0x00005462
	public float compressionAmount
	{
		get
		{
			return this._compressionAmount;
		}
		set
		{
			this._compressionAmount = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600034E RID: 846 RVA: 0x00007281 File Offset: 0x00005481
	// (set) Token: 0x0600034F RID: 847 RVA: 0x00007289 File Offset: 0x00005489
	public float overtones
	{
		get
		{
			return this._overtones;
		}
		set
		{
			this._overtones = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000350 RID: 848 RVA: 0x000072A8 File Offset: 0x000054A8
	// (set) Token: 0x06000351 RID: 849 RVA: 0x000072B0 File Offset: 0x000054B0
	public float overtoneFalloff
	{
		get
		{
			return this._overtoneFalloff;
		}
		set
		{
			this._overtoneFalloff = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000352 RID: 850 RVA: 0x000072CF File Offset: 0x000054CF
	// (set) Token: 0x06000353 RID: 851 RVA: 0x000072D7 File Offset: 0x000054D7
	public float bitCrush
	{
		get
		{
			return this._bitCrush;
		}
		set
		{
			this._bitCrush = Mathf.Clamp(value, 0f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000354 RID: 852 RVA: 0x000072F6 File Offset: 0x000054F6
	// (set) Token: 0x06000355 RID: 853 RVA: 0x000072FE File Offset: 0x000054FE
	public float bitCrushSweep
	{
		get
		{
			return this._bitCrushSweep;
		}
		set
		{
			this._bitCrushSweep = Mathf.Clamp(value, -1f, 1f);
			this.paramsDirty = true;
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00066EC8 File Offset: 0x000650C8
	public void GeneratePickupCoin()
	{
		this.resetParams();
		this._startFrequency = 0.4f + this.GetRandom() * 0.5f;
		this._sustainTime = this.GetRandom() * 0.1f;
		this._decayTime = 0.1f + this.GetRandom() * 0.4f;
		this._sustainPunch = 0.3f + this.GetRandom() * 0.3f;
		if (this.GetRandomBool())
		{
			this._changeSpeed = 0.5f + this.GetRandom() * 0.2f;
			int num = (int)(this.GetRandom() * 7f) + 1;
			int num2 = num + (int)(this.GetRandom() * 7f) + 2;
			this._changeAmount = (float)num / (float)num2;
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00066F84 File Offset: 0x00065184
	public void GenerateLaserShoot()
	{
		this.resetParams();
		this._waveType = (uint)(this.GetRandom() * 3f);
		if (this._waveType == 2U && this.GetRandomBool())
		{
			this._waveType = (uint)(this.GetRandom() * 2f);
		}
		this._startFrequency = 0.5f + this.GetRandom() * 0.5f;
		this._minFrequency = this._startFrequency - 0.2f - this.GetRandom() * 0.6f;
		if (this._minFrequency < 0.2f)
		{
			this._minFrequency = 0.2f;
		}
		this._slide = -0.15f - this.GetRandom() * 0.2f;
		if (this.GetRandom() < 0.33f)
		{
			this._startFrequency = 0.3f + this.GetRandom() * 0.6f;
			this._minFrequency = this.GetRandom() * 0.1f;
			this._slide = -0.35f - this.GetRandom() * 0.3f;
		}
		if (this.GetRandomBool())
		{
			this._squareDuty = this.GetRandom() * 0.5f;
			this._dutySweep = this.GetRandom() * 0.2f;
		}
		else
		{
			this._squareDuty = 0.4f + this.GetRandom() * 0.5f;
			this._dutySweep = -this.GetRandom() * 0.7f;
		}
		this._sustainTime = 0.1f + this.GetRandom() * 0.2f;
		this._decayTime = this.GetRandom() * 0.4f;
		if (this.GetRandomBool())
		{
			this._sustainPunch = this.GetRandom() * 0.3f;
		}
		if (this.GetRandom() < 0.33f)
		{
			this._phaserOffset = this.GetRandom() * 0.2f;
			this._phaserSweep = -this.GetRandom() * 0.2f;
		}
		if (this.GetRandomBool())
		{
			this._hpFilterCutoff = this.GetRandom() * 0.3f;
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00067170 File Offset: 0x00065370
	public void GenerateExplosion()
	{
		this.resetParams();
		this._waveType = 3U;
		if (this.GetRandomBool())
		{
			this._startFrequency = 0.1f + this.GetRandom() * 0.4f;
			this._slide = -0.1f + this.GetRandom() * 0.4f;
		}
		else
		{
			this._startFrequency = 0.2f + this.GetRandom() * 0.7f;
			this._slide = -0.2f - this.GetRandom() * 0.2f;
		}
		this._startFrequency *= this._startFrequency;
		if (this.GetRandom() < 0.2f)
		{
			this._slide = 0f;
		}
		if (this.GetRandom() < 0.33f)
		{
			this._repeatSpeed = 0.3f + this.GetRandom() * 0.5f;
		}
		this._sustainTime = 0.1f + this.GetRandom() * 0.3f;
		this._decayTime = this.GetRandom() * 0.5f;
		this._sustainPunch = 0.2f + this.GetRandom() * 0.6f;
		if (this.GetRandomBool())
		{
			this._phaserOffset = -0.3f + this.GetRandom() * 0.9f;
			this._phaserSweep = -this.GetRandom() * 0.3f;
		}
		if (this.GetRandom() < 0.33f)
		{
			this._changeSpeed = 0.6f + this.GetRandom() * 0.3f;
			this._changeAmount = 0.8f - this.GetRandom() * 1.6f;
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x000672F8 File Offset: 0x000654F8
	public void GeneratePowerup()
	{
		this.resetParams();
		if (this.GetRandomBool())
		{
			this._waveType = 1U;
		}
		else
		{
			this._squareDuty = this.GetRandom() * 0.6f;
		}
		if (this.GetRandomBool())
		{
			this._startFrequency = 0.2f + this.GetRandom() * 0.3f;
			this._slide = 0.1f + this.GetRandom() * 0.4f;
			this._repeatSpeed = 0.4f + this.GetRandom() * 0.4f;
		}
		else
		{
			this._startFrequency = 0.2f + this.GetRandom() * 0.3f;
			this._slide = 0.05f + this.GetRandom() * 0.2f;
			if (this.GetRandomBool())
			{
				this._vibratoDepth = this.GetRandom() * 0.7f;
				this._vibratoSpeed = this.GetRandom() * 0.6f;
			}
		}
		this._sustainTime = this.GetRandom() * 0.4f;
		this._decayTime = 0.1f + this.GetRandom() * 0.4f;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00067408 File Offset: 0x00065608
	public void GenerateHitHurt()
	{
		this.resetParams();
		this._waveType = (uint)(this.GetRandom() * 3f);
		if (this._waveType == 2U)
		{
			this._waveType = 3U;
		}
		else if (this._waveType == 0U)
		{
			this._squareDuty = this.GetRandom() * 0.6f;
		}
		this._startFrequency = 0.2f + this.GetRandom() * 0.6f;
		this._slide = -0.3f - this.GetRandom() * 0.4f;
		this._sustainTime = this.GetRandom() * 0.1f;
		this._decayTime = 0.1f + this.GetRandom() * 0.2f;
		if (this.GetRandomBool())
		{
			this._hpFilterCutoff = this.GetRandom() * 0.3f;
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000674D0 File Offset: 0x000656D0
	public void GenerateJump()
	{
		this.resetParams();
		this._waveType = 0U;
		this._squareDuty = this.GetRandom() * 0.6f;
		this._startFrequency = 0.3f + this.GetRandom() * 0.3f;
		this._slide = 0.1f + this.GetRandom() * 0.2f;
		this._sustainTime = 0.1f + this.GetRandom() * 0.3f;
		this._decayTime = 0.1f + this.GetRandom() * 0.2f;
		if (this.GetRandomBool())
		{
			this._hpFilterCutoff = this.GetRandom() * 0.3f;
		}
		if (this.GetRandomBool())
		{
			this._lpFilterCutoff = 1f - this.GetRandom() * 0.6f;
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00067598 File Offset: 0x00065798
	public void GenerateBlipSelect()
	{
		this.resetParams();
		this._waveType = (uint)(this.GetRandom() * 2f);
		if (this._waveType == 0U)
		{
			this._squareDuty = this.GetRandom() * 0.6f;
		}
		this._startFrequency = 0.2f + this.GetRandom() * 0.4f;
		this._sustainTime = 0.1f + this.GetRandom() * 0.1f;
		this._decayTime = this.GetRandom() * 0.2f;
		this._hpFilterCutoff = 0.1f;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00067628 File Offset: 0x00065828
	protected void resetParams()
	{
		this.paramsDirty = true;
		this._waveType = 0U;
		this._startFrequency = 0.3f;
		this._minFrequency = 0f;
		this._slide = 0f;
		this._deltaSlide = 0f;
		this._squareDuty = 0f;
		this._dutySweep = 0f;
		this._vibratoDepth = 0f;
		this._vibratoSpeed = 0f;
		this._attackTime = 0f;
		this._sustainTime = 0.3f;
		this._decayTime = 0.4f;
		this._sustainPunch = 0f;
		this._lpFilterResonance = 0f;
		this._lpFilterCutoff = 1f;
		this._lpFilterCutoffSweep = 0f;
		this._hpFilterCutoff = 0f;
		this._hpFilterCutoffSweep = 0f;
		this._phaserOffset = 0f;
		this._phaserSweep = 0f;
		this._repeatSpeed = 0f;
		this._changeSpeed = 0f;
		this._changeAmount = 0f;
		this._changeRepeat = 0f;
		this._changeAmount2 = 0f;
		this._changeSpeed2 = 0f;
		this._compressionAmount = 0.3f;
		this._overtones = 0f;
		this._overtoneFalloff = 0f;
		this._bitCrush = 0f;
		this._bitCrushSweep = 0f;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00067790 File Offset: 0x00065990
	public void Mutate(float __mutation = 0.05f)
	{
		if (this.GetRandomBool())
		{
			this.startFrequency += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.minFrequency += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.slide += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.deltaSlide += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.squareDuty += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.dutySweep += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.vibratoDepth += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.vibratoSpeed += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.attackTime += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.sustainTime += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.decayTime += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.sustainPunch += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.lpFilterCutoff += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.lpFilterCutoffSweep += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.lpFilterResonance += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.hpFilterCutoff += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.hpFilterCutoffSweep += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.phaserOffset += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.phaserSweep += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.repeatSpeed += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.changeSpeed += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.changeAmount += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.changeRepeat += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.changeAmount2 += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.changeSpeed2 += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.compressionAmount += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.overtones += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.overtoneFalloff += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.bitCrush += this.GetRandom() * __mutation * 2f - __mutation;
		}
		if (this.GetRandomBool())
		{
			this.bitCrushSweep += this.GetRandom() * __mutation * 2f - __mutation;
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00067BF4 File Offset: 0x00065DF4
	public void Randomize()
	{
		this.resetParams();
		this._waveType = (uint)(this.GetRandom() * 9f);
		this._attackTime = this.Pow(this.GetRandom() * 2f - 1f, 4);
		this._sustainTime = this.Pow(this.GetRandom() * 2f - 1f, 2);
		this._sustainPunch = this.Pow(this.GetRandom() * 0.8f, 2);
		this._decayTime = this.GetRandom();
		this._startFrequency = (this.GetRandomBool() ? this.Pow(this.GetRandom() * 2f - 1f, 2) : (this.Pow(this.GetRandom() * 0.5f, 3) + 0.5f));
		this._minFrequency = 0f;
		this._slide = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._deltaSlide = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._vibratoDepth = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._vibratoSpeed = this.GetRandom() * 2f - 1f;
		this._changeAmount = this.GetRandom() * 2f - 1f;
		this._changeSpeed = this.GetRandom() * 2f - 1f;
		this._squareDuty = this.GetRandom() * 2f - 1f;
		this._dutySweep = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._repeatSpeed = this.GetRandom() * 2f - 1f;
		this._phaserOffset = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._phaserSweep = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._lpFilterCutoff = 1f - this.Pow(this.GetRandom(), 3);
		this._lpFilterCutoffSweep = this.Pow(this.GetRandom() * 2f - 1f, 3);
		this._lpFilterResonance = this.GetRandom() * 2f - 1f;
		this._hpFilterCutoff = this.Pow(this.GetRandom(), 5);
		this._hpFilterCutoffSweep = this.Pow(this.GetRandom() * 2f - 1f, 5);
		if (this._attackTime + this._sustainTime + this._decayTime < 0.2f)
		{
			this._sustainTime = 0.2f + this.GetRandom() * 0.3f;
			this._decayTime = 0.2f + this.GetRandom() * 0.3f;
		}
		if ((this._startFrequency > 0.7f && (double)this._slide > 0.2) || ((double)this._startFrequency < 0.2 && (double)this._slide < -0.05))
		{
			this._slide = -this._slide;
		}
		if (this._lpFilterCutoff < 0.1f && this._lpFilterCutoffSweep < -0.05f)
		{
			this._lpFilterCutoffSweep = -this._lpFilterCutoffSweep;
		}
		this._changeRepeat = this.GetRandom();
		this._changeAmount2 = this.GetRandom() * 2f - 1f;
		this._changeSpeed2 = this.GetRandom();
		this._compressionAmount = this.GetRandom();
		this._overtones = this.GetRandom();
		this._overtoneFalloff = this.GetRandom();
		this._bitCrush = this.GetRandom();
		this._bitCrushSweep = this.GetRandom() * 2f - 1f;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00067FB8 File Offset: 0x000661B8
	public string GetSettingsStringLegacy()
	{
		return "" + this.waveType.ToString() + "," + this.To4DP(this._attackTime) + "," + this.To4DP(this._sustainTime) + "," + this.To4DP(this._sustainPunch) + "," + this.To4DP(this._decayTime) + "," + this.To4DP(this._startFrequency) + "," + this.To4DP(this._minFrequency) + "," + this.To4DP(this._slide) + "," + this.To4DP(this._deltaSlide) + "," + this.To4DP(this._vibratoDepth) + "," + this.To4DP(this._vibratoSpeed) + "," + this.To4DP(this._changeAmount) + "," + this.To4DP(this._changeSpeed) + "," + this.To4DP(this._squareDuty) + "," + this.To4DP(this._dutySweep) + "," + this.To4DP(this._repeatSpeed) + "," + this.To4DP(this._phaserOffset) + "," + this.To4DP(this._phaserSweep) + "," + this.To4DP(this._lpFilterCutoff) + "," + this.To4DP(this._lpFilterCutoffSweep) + "," + this.To4DP(this._lpFilterResonance) + "," + this.To4DP(this._hpFilterCutoff) + "," + this.To4DP(this._hpFilterCutoffSweep) + "," + this.To4DP(this._masterVolume);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x000681D8 File Offset: 0x000663D8
	public string GetSettingsString()
	{
		return "" + this.waveType.ToString() + "," + this.To4DP(this._masterVolume) + "," + this.To4DP(this._attackTime) + "," + this.To4DP(this._sustainTime) + "," + this.To4DP(this._sustainPunch) + "," + this.To4DP(this._decayTime) + "," + this.To4DP(this._compressionAmount) + "," + this.To4DP(this._startFrequency) + "," + this.To4DP(this._minFrequency) + "," + this.To4DP(this._slide) + "," + this.To4DP(this._deltaSlide) + "," + this.To4DP(this._vibratoDepth) + "," + this.To4DP(this._vibratoSpeed) + "," + this.To4DP(this._overtones) + "," + this.To4DP(this._overtoneFalloff) + "," + this.To4DP(this._changeRepeat) + "," + this.To4DP(this._changeAmount) + "," + this.To4DP(this._changeSpeed) + "," + this.To4DP(this._changeAmount2) + "," + this.To4DP(this._changeSpeed2) + "," + this.To4DP(this._squareDuty) + "," + this.To4DP(this._dutySweep) + "," + this.To4DP(this._repeatSpeed) + "," + this.To4DP(this._phaserOffset) + "," + this.To4DP(this._phaserSweep) + "," + this.To4DP(this._lpFilterCutoff) + "," + this.To4DP(this._lpFilterCutoffSweep) + "," + this.To4DP(this._lpFilterResonance) + "," + this.To4DP(this._hpFilterCutoff) + "," + this.To4DP(this._hpFilterCutoffSweep) + "," + this.To4DP(this._bitCrush) + "," + this.To4DP(this._bitCrushSweep);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x000684A8 File Offset: 0x000666A8
	public bool SetSettingsString(string __string)
	{
		string[] array = __string.Split(new char[]
		{
			','
		});
		if (array.Length == 24)
		{
			this.resetParams();
			this.waveType = this.ParseUint(array[0]);
			this.attackTime = this.ParseFloat(array[1]);
			this.sustainTime = this.ParseFloat(array[2]);
			this.sustainPunch = this.ParseFloat(array[3]);
			this.decayTime = this.ParseFloat(array[4]);
			this.startFrequency = this.ParseFloat(array[5]);
			this.minFrequency = this.ParseFloat(array[6]);
			this.slide = this.ParseFloat(array[7]);
			this.deltaSlide = this.ParseFloat(array[8]);
			this.vibratoDepth = this.ParseFloat(array[9]);
			this.vibratoSpeed = this.ParseFloat(array[10]);
			this.changeAmount = this.ParseFloat(array[11]);
			this.changeSpeed = this.ParseFloat(array[12]);
			this.squareDuty = this.ParseFloat(array[13]);
			this.dutySweep = this.ParseFloat(array[14]);
			this.repeatSpeed = this.ParseFloat(array[15]);
			this.phaserOffset = this.ParseFloat(array[16]);
			this.phaserSweep = this.ParseFloat(array[17]);
			this.lpFilterCutoff = this.ParseFloat(array[18]);
			this.lpFilterCutoffSweep = this.ParseFloat(array[19]);
			this.lpFilterResonance = this.ParseFloat(array[20]);
			this.hpFilterCutoff = this.ParseFloat(array[21]);
			this.hpFilterCutoffSweep = this.ParseFloat(array[22]);
			this.masterVolume = this.ParseFloat(array[23]);
		}
		else
		{
			if (array.Length < 32)
			{
				Debug.LogError("Could not paste settings string: parameters contain " + array.Length + " values (was expecting 24 or >32)");
				return false;
			}
			this.resetParams();
			this.waveType = this.ParseUint(array[0]);
			this.masterVolume = this.ParseFloat(array[1]);
			this.attackTime = this.ParseFloat(array[2]);
			this.sustainTime = this.ParseFloat(array[3]);
			this.sustainPunch = this.ParseFloat(array[4]);
			this.decayTime = this.ParseFloat(array[5]);
			this.compressionAmount = this.ParseFloat(array[6]);
			this.startFrequency = this.ParseFloat(array[7]);
			this.minFrequency = this.ParseFloat(array[8]);
			this.slide = this.ParseFloat(array[9]);
			this.deltaSlide = this.ParseFloat(array[10]);
			this.vibratoDepth = this.ParseFloat(array[11]);
			this.vibratoSpeed = this.ParseFloat(array[12]);
			this.overtones = this.ParseFloat(array[13]);
			this.overtoneFalloff = this.ParseFloat(array[14]);
			this.changeRepeat = this.ParseFloat(array[15]);
			this.changeAmount = this.ParseFloat(array[16]);
			this.changeSpeed = this.ParseFloat(array[17]);
			this.changeAmount2 = this.ParseFloat(array[18]);
			this.changeSpeed2 = this.ParseFloat(array[19]);
			this.squareDuty = this.ParseFloat(array[20]);
			this.dutySweep = this.ParseFloat(array[21]);
			this.repeatSpeed = this.ParseFloat(array[22]);
			this.phaserOffset = this.ParseFloat(array[23]);
			this.phaserSweep = this.ParseFloat(array[24]);
			this.lpFilterCutoff = this.ParseFloat(array[25]);
			this.lpFilterCutoffSweep = this.ParseFloat(array[26]);
			this.lpFilterResonance = this.ParseFloat(array[27]);
			this.hpFilterCutoff = this.ParseFloat(array[28]);
			this.hpFilterCutoffSweep = this.ParseFloat(array[29]);
			this.bitCrush = this.ParseFloat(array[30]);
			this.bitCrushSweep = this.ParseFloat(array[31]);
		}
		return true;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0000731D File Offset: 0x0000551D
	public SfxrParams Clone()
	{
		SfxrParams sfxrParams = new SfxrParams();
		sfxrParams.CopyFrom(this, false);
		return sfxrParams;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0006887C File Offset: 0x00066A7C
	public void CopyFrom(SfxrParams __params, bool __makeDirty = false)
	{
		bool flag = this.paramsDirty;
		this.SetSettingsString(this.GetSettingsString());
		this.paramsDirty = (flag || __makeDirty);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0000732C File Offset: 0x0000552C
	private float Pow(float __pbase, int __power)
	{
		switch (__power)
		{
		case 2:
			return __pbase * __pbase;
		case 3:
			return __pbase * __pbase * __pbase;
		case 4:
			return __pbase * __pbase * __pbase * __pbase;
		case 5:
			return __pbase * __pbase * __pbase * __pbase * __pbase;
		default:
			return 1f;
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00007369 File Offset: 0x00005569
	private string To4DP(float __value)
	{
		if (__value < 0.0001f && __value > -0.0001f)
		{
			return "";
		}
		return __value.ToString("#.####");
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0000738D File Offset: 0x0000558D
	private uint ParseUint(string __value)
	{
		if (__value.Length == 0)
		{
			return 0U;
		}
		return uint.Parse(__value);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0000739F File Offset: 0x0000559F
	private float ParseFloat(string __value)
	{
		if (__value.Length == 0)
		{
			return 0f;
		}
		return float.Parse(__value);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000073B5 File Offset: 0x000055B5
	private float GetRandom()
	{
		return Random.value % 1f;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x000073C2 File Offset: 0x000055C2
	private bool GetRandomBool()
	{
		return Random.value > 0.5f;
	}

	// Token: 0x04000197 RID: 407
	public bool paramsDirty;

	// Token: 0x04000198 RID: 408
	private uint _waveType;

	// Token: 0x04000199 RID: 409
	private float _masterVolume = 0.5f;

	// Token: 0x0400019A RID: 410
	private float _attackTime;

	// Token: 0x0400019B RID: 411
	private float _sustainTime;

	// Token: 0x0400019C RID: 412
	private float _sustainPunch;

	// Token: 0x0400019D RID: 413
	private float _decayTime;

	// Token: 0x0400019E RID: 414
	private float _startFrequency;

	// Token: 0x0400019F RID: 415
	private float _minFrequency;

	// Token: 0x040001A0 RID: 416
	private float _slide;

	// Token: 0x040001A1 RID: 417
	private float _deltaSlide;

	// Token: 0x040001A2 RID: 418
	private float _vibratoDepth;

	// Token: 0x040001A3 RID: 419
	private float _vibratoSpeed;

	// Token: 0x040001A4 RID: 420
	private float _changeAmount;

	// Token: 0x040001A5 RID: 421
	private float _changeSpeed;

	// Token: 0x040001A6 RID: 422
	private float _squareDuty;

	// Token: 0x040001A7 RID: 423
	private float _dutySweep;

	// Token: 0x040001A8 RID: 424
	private float _repeatSpeed;

	// Token: 0x040001A9 RID: 425
	private float _phaserOffset;

	// Token: 0x040001AA RID: 426
	private float _phaserSweep;

	// Token: 0x040001AB RID: 427
	private float _lpFilterCutoff;

	// Token: 0x040001AC RID: 428
	private float _lpFilterCutoffSweep;

	// Token: 0x040001AD RID: 429
	private float _lpFilterResonance;

	// Token: 0x040001AE RID: 430
	private float _hpFilterCutoff;

	// Token: 0x040001AF RID: 431
	private float _hpFilterCutoffSweep;

	// Token: 0x040001B0 RID: 432
	private float _changeRepeat;

	// Token: 0x040001B1 RID: 433
	private float _changeAmount2;

	// Token: 0x040001B2 RID: 434
	private float _changeSpeed2;

	// Token: 0x040001B3 RID: 435
	private float _compressionAmount;

	// Token: 0x040001B4 RID: 436
	private float _overtones;

	// Token: 0x040001B5 RID: 437
	private float _overtoneFalloff;

	// Token: 0x040001B6 RID: 438
	private float _bitCrush;

	// Token: 0x040001B7 RID: 439
	private float _bitCrushSweep;
}
