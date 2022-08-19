using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class SfxrParams
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000F2CF File Offset: 0x0000D4CF
	// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000F2D7 File Offset: 0x0000D4D7
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

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060002FA RID: 762 RVA: 0x0000F2EE File Offset: 0x0000D4EE
	// (set) Token: 0x060002FB RID: 763 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
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

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060002FC RID: 764 RVA: 0x0000F315 File Offset: 0x0000D515
	// (set) Token: 0x060002FD RID: 765 RVA: 0x0000F31D File Offset: 0x0000D51D
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

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060002FE RID: 766 RVA: 0x0000F33C File Offset: 0x0000D53C
	// (set) Token: 0x060002FF RID: 767 RVA: 0x0000F344 File Offset: 0x0000D544
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

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000300 RID: 768 RVA: 0x0000F363 File Offset: 0x0000D563
	// (set) Token: 0x06000301 RID: 769 RVA: 0x0000F36B File Offset: 0x0000D56B
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

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000302 RID: 770 RVA: 0x0000F38A File Offset: 0x0000D58A
	// (set) Token: 0x06000303 RID: 771 RVA: 0x0000F392 File Offset: 0x0000D592
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

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000304 RID: 772 RVA: 0x0000F3B1 File Offset: 0x0000D5B1
	// (set) Token: 0x06000305 RID: 773 RVA: 0x0000F3B9 File Offset: 0x0000D5B9
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

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000306 RID: 774 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
	// (set) Token: 0x06000307 RID: 775 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
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

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000308 RID: 776 RVA: 0x0000F3FF File Offset: 0x0000D5FF
	// (set) Token: 0x06000309 RID: 777 RVA: 0x0000F407 File Offset: 0x0000D607
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

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600030A RID: 778 RVA: 0x0000F426 File Offset: 0x0000D626
	// (set) Token: 0x0600030B RID: 779 RVA: 0x0000F42E File Offset: 0x0000D62E
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

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600030C RID: 780 RVA: 0x0000F44D File Offset: 0x0000D64D
	// (set) Token: 0x0600030D RID: 781 RVA: 0x0000F455 File Offset: 0x0000D655
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

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600030E RID: 782 RVA: 0x0000F474 File Offset: 0x0000D674
	// (set) Token: 0x0600030F RID: 783 RVA: 0x0000F47C File Offset: 0x0000D67C
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

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000310 RID: 784 RVA: 0x0000F49B File Offset: 0x0000D69B
	// (set) Token: 0x06000311 RID: 785 RVA: 0x0000F4A3 File Offset: 0x0000D6A3
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

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000312 RID: 786 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
	// (set) Token: 0x06000313 RID: 787 RVA: 0x0000F4CA File Offset: 0x0000D6CA
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

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000314 RID: 788 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
	// (set) Token: 0x06000315 RID: 789 RVA: 0x0000F4F1 File Offset: 0x0000D6F1
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

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000316 RID: 790 RVA: 0x0000F510 File Offset: 0x0000D710
	// (set) Token: 0x06000317 RID: 791 RVA: 0x0000F518 File Offset: 0x0000D718
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

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000318 RID: 792 RVA: 0x0000F537 File Offset: 0x0000D737
	// (set) Token: 0x06000319 RID: 793 RVA: 0x0000F53F File Offset: 0x0000D73F
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

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600031A RID: 794 RVA: 0x0000F55E File Offset: 0x0000D75E
	// (set) Token: 0x0600031B RID: 795 RVA: 0x0000F566 File Offset: 0x0000D766
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

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600031C RID: 796 RVA: 0x0000F585 File Offset: 0x0000D785
	// (set) Token: 0x0600031D RID: 797 RVA: 0x0000F58D File Offset: 0x0000D78D
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

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600031E RID: 798 RVA: 0x0000F5AC File Offset: 0x0000D7AC
	// (set) Token: 0x0600031F RID: 799 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
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

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000320 RID: 800 RVA: 0x0000F5D3 File Offset: 0x0000D7D3
	// (set) Token: 0x06000321 RID: 801 RVA: 0x0000F5DB File Offset: 0x0000D7DB
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

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000322 RID: 802 RVA: 0x0000F5FA File Offset: 0x0000D7FA
	// (set) Token: 0x06000323 RID: 803 RVA: 0x0000F602 File Offset: 0x0000D802
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

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000324 RID: 804 RVA: 0x0000F621 File Offset: 0x0000D821
	// (set) Token: 0x06000325 RID: 805 RVA: 0x0000F629 File Offset: 0x0000D829
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

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000326 RID: 806 RVA: 0x0000F648 File Offset: 0x0000D848
	// (set) Token: 0x06000327 RID: 807 RVA: 0x0000F650 File Offset: 0x0000D850
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

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000328 RID: 808 RVA: 0x0000F66F File Offset: 0x0000D86F
	// (set) Token: 0x06000329 RID: 809 RVA: 0x0000F677 File Offset: 0x0000D877
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

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600032A RID: 810 RVA: 0x0000F696 File Offset: 0x0000D896
	// (set) Token: 0x0600032B RID: 811 RVA: 0x0000F69E File Offset: 0x0000D89E
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

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600032C RID: 812 RVA: 0x0000F6BD File Offset: 0x0000D8BD
	// (set) Token: 0x0600032D RID: 813 RVA: 0x0000F6C5 File Offset: 0x0000D8C5
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

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600032E RID: 814 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
	// (set) Token: 0x0600032F RID: 815 RVA: 0x0000F6EC File Offset: 0x0000D8EC
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

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F70B File Offset: 0x0000D90B
	// (set) Token: 0x06000331 RID: 817 RVA: 0x0000F713 File Offset: 0x0000D913
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

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F732 File Offset: 0x0000D932
	// (set) Token: 0x06000333 RID: 819 RVA: 0x0000F73A File Offset: 0x0000D93A
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

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000334 RID: 820 RVA: 0x0000F759 File Offset: 0x0000D959
	// (set) Token: 0x06000335 RID: 821 RVA: 0x0000F761 File Offset: 0x0000D961
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

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000336 RID: 822 RVA: 0x0000F780 File Offset: 0x0000D980
	// (set) Token: 0x06000337 RID: 823 RVA: 0x0000F788 File Offset: 0x0000D988
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

	// Token: 0x06000338 RID: 824 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
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

	// Token: 0x06000339 RID: 825 RVA: 0x0000F864 File Offset: 0x0000DA64
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

	// Token: 0x0600033A RID: 826 RVA: 0x0000FA50 File Offset: 0x0000DC50
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

	// Token: 0x0600033B RID: 827 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
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

	// Token: 0x0600033C RID: 828 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
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

	// Token: 0x0600033D RID: 829 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
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

	// Token: 0x0600033E RID: 830 RVA: 0x0000FE78 File Offset: 0x0000E078
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

	// Token: 0x0600033F RID: 831 RVA: 0x0000FF08 File Offset: 0x0000E108
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

	// Token: 0x06000340 RID: 832 RVA: 0x00010070 File Offset: 0x0000E270
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

	// Token: 0x06000341 RID: 833 RVA: 0x000104D4 File Offset: 0x0000E6D4
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

	// Token: 0x06000342 RID: 834 RVA: 0x00010898 File Offset: 0x0000EA98
	public string GetSettingsStringLegacy()
	{
		return "" + this.waveType.ToString() + "," + this.To4DP(this._attackTime) + "," + this.To4DP(this._sustainTime) + "," + this.To4DP(this._sustainPunch) + "," + this.To4DP(this._decayTime) + "," + this.To4DP(this._startFrequency) + "," + this.To4DP(this._minFrequency) + "," + this.To4DP(this._slide) + "," + this.To4DP(this._deltaSlide) + "," + this.To4DP(this._vibratoDepth) + "," + this.To4DP(this._vibratoSpeed) + "," + this.To4DP(this._changeAmount) + "," + this.To4DP(this._changeSpeed) + "," + this.To4DP(this._squareDuty) + "," + this.To4DP(this._dutySweep) + "," + this.To4DP(this._repeatSpeed) + "," + this.To4DP(this._phaserOffset) + "," + this.To4DP(this._phaserSweep) + "," + this.To4DP(this._lpFilterCutoff) + "," + this.To4DP(this._lpFilterCutoffSweep) + "," + this.To4DP(this._lpFilterResonance) + "," + this.To4DP(this._hpFilterCutoff) + "," + this.To4DP(this._hpFilterCutoffSweep) + "," + this.To4DP(this._masterVolume);
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00010AB8 File Offset: 0x0000ECB8
	public string GetSettingsString()
	{
		return "" + this.waveType.ToString() + "," + this.To4DP(this._masterVolume) + "," + this.To4DP(this._attackTime) + "," + this.To4DP(this._sustainTime) + "," + this.To4DP(this._sustainPunch) + "," + this.To4DP(this._decayTime) + "," + this.To4DP(this._compressionAmount) + "," + this.To4DP(this._startFrequency) + "," + this.To4DP(this._minFrequency) + "," + this.To4DP(this._slide) + "," + this.To4DP(this._deltaSlide) + "," + this.To4DP(this._vibratoDepth) + "," + this.To4DP(this._vibratoSpeed) + "," + this.To4DP(this._overtones) + "," + this.To4DP(this._overtoneFalloff) + "," + this.To4DP(this._changeRepeat) + "," + this.To4DP(this._changeAmount) + "," + this.To4DP(this._changeSpeed) + "," + this.To4DP(this._changeAmount2) + "," + this.To4DP(this._changeSpeed2) + "," + this.To4DP(this._squareDuty) + "," + this.To4DP(this._dutySweep) + "," + this.To4DP(this._repeatSpeed) + "," + this.To4DP(this._phaserOffset) + "," + this.To4DP(this._phaserSweep) + "," + this.To4DP(this._lpFilterCutoff) + "," + this.To4DP(this._lpFilterCutoffSweep) + "," + this.To4DP(this._lpFilterResonance) + "," + this.To4DP(this._hpFilterCutoff) + "," + this.To4DP(this._hpFilterCutoffSweep) + "," + this.To4DP(this._bitCrush) + "," + this.To4DP(this._bitCrushSweep);
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00010D88 File Offset: 0x0000EF88
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

	// Token: 0x06000345 RID: 837 RVA: 0x0001115B File Offset: 0x0000F35B
	public SfxrParams Clone()
	{
		SfxrParams sfxrParams = new SfxrParams();
		sfxrParams.CopyFrom(this, false);
		return sfxrParams;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001116C File Offset: 0x0000F36C
	public void CopyFrom(SfxrParams __params, bool __makeDirty = false)
	{
		bool flag = this.paramsDirty;
		this.SetSettingsString(this.GetSettingsString());
		this.paramsDirty = (flag || __makeDirty);
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00011196 File Offset: 0x0000F396
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

	// Token: 0x06000348 RID: 840 RVA: 0x000111D3 File Offset: 0x0000F3D3
	private string To4DP(float __value)
	{
		if (__value < 0.0001f && __value > -0.0001f)
		{
			return "";
		}
		return __value.ToString("#.####");
	}

	// Token: 0x06000349 RID: 841 RVA: 0x000111F7 File Offset: 0x0000F3F7
	private uint ParseUint(string __value)
	{
		if (__value.Length == 0)
		{
			return 0U;
		}
		return uint.Parse(__value);
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00011209 File Offset: 0x0000F409
	private float ParseFloat(string __value)
	{
		if (__value.Length == 0)
		{
			return 0f;
		}
		return float.Parse(__value);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0001121F File Offset: 0x0000F41F
	private float GetRandom()
	{
		return Random.value % 1f;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001122C File Offset: 0x0000F42C
	private bool GetRandomBool()
	{
		return Random.value > 0.5f;
	}

	// Token: 0x04000176 RID: 374
	public bool paramsDirty;

	// Token: 0x04000177 RID: 375
	private uint _waveType;

	// Token: 0x04000178 RID: 376
	private float _masterVolume = 0.5f;

	// Token: 0x04000179 RID: 377
	private float _attackTime;

	// Token: 0x0400017A RID: 378
	private float _sustainTime;

	// Token: 0x0400017B RID: 379
	private float _sustainPunch;

	// Token: 0x0400017C RID: 380
	private float _decayTime;

	// Token: 0x0400017D RID: 381
	private float _startFrequency;

	// Token: 0x0400017E RID: 382
	private float _minFrequency;

	// Token: 0x0400017F RID: 383
	private float _slide;

	// Token: 0x04000180 RID: 384
	private float _deltaSlide;

	// Token: 0x04000181 RID: 385
	private float _vibratoDepth;

	// Token: 0x04000182 RID: 386
	private float _vibratoSpeed;

	// Token: 0x04000183 RID: 387
	private float _changeAmount;

	// Token: 0x04000184 RID: 388
	private float _changeSpeed;

	// Token: 0x04000185 RID: 389
	private float _squareDuty;

	// Token: 0x04000186 RID: 390
	private float _dutySweep;

	// Token: 0x04000187 RID: 391
	private float _repeatSpeed;

	// Token: 0x04000188 RID: 392
	private float _phaserOffset;

	// Token: 0x04000189 RID: 393
	private float _phaserSweep;

	// Token: 0x0400018A RID: 394
	private float _lpFilterCutoff;

	// Token: 0x0400018B RID: 395
	private float _lpFilterCutoffSweep;

	// Token: 0x0400018C RID: 396
	private float _lpFilterResonance;

	// Token: 0x0400018D RID: 397
	private float _hpFilterCutoff;

	// Token: 0x0400018E RID: 398
	private float _hpFilterCutoffSweep;

	// Token: 0x0400018F RID: 399
	private float _changeRepeat;

	// Token: 0x04000190 RID: 400
	private float _changeAmount2;

	// Token: 0x04000191 RID: 401
	private float _changeSpeed2;

	// Token: 0x04000192 RID: 402
	private float _compressionAmount;

	// Token: 0x04000193 RID: 403
	private float _overtones;

	// Token: 0x04000194 RID: 404
	private float _overtoneFalloff;

	// Token: 0x04000195 RID: 405
	private float _bitCrush;

	// Token: 0x04000196 RID: 406
	private float _bitCrushSweep;
}
