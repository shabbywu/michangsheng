using UnityEngine;

public class SfxrParams
{
	public bool paramsDirty;

	private uint _waveType;

	private float _masterVolume = 0.5f;

	private float _attackTime;

	private float _sustainTime;

	private float _sustainPunch;

	private float _decayTime;

	private float _startFrequency;

	private float _minFrequency;

	private float _slide;

	private float _deltaSlide;

	private float _vibratoDepth;

	private float _vibratoSpeed;

	private float _changeAmount;

	private float _changeSpeed;

	private float _squareDuty;

	private float _dutySweep;

	private float _repeatSpeed;

	private float _phaserOffset;

	private float _phaserSweep;

	private float _lpFilterCutoff;

	private float _lpFilterCutoffSweep;

	private float _lpFilterResonance;

	private float _hpFilterCutoff;

	private float _hpFilterCutoffSweep;

	private float _changeRepeat;

	private float _changeAmount2;

	private float _changeSpeed2;

	private float _compressionAmount;

	private float _overtones;

	private float _overtoneFalloff;

	private float _bitCrush;

	private float _bitCrushSweep;

	public uint waveType
	{
		get
		{
			return _waveType;
		}
		set
		{
			_waveType = ((value <= 8) ? value : 0u);
			paramsDirty = true;
		}
	}

	public float masterVolume
	{
		get
		{
			return _masterVolume;
		}
		set
		{
			_masterVolume = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float attackTime
	{
		get
		{
			return _attackTime;
		}
		set
		{
			_attackTime = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float sustainTime
	{
		get
		{
			return _sustainTime;
		}
		set
		{
			_sustainTime = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float sustainPunch
	{
		get
		{
			return _sustainPunch;
		}
		set
		{
			_sustainPunch = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float decayTime
	{
		get
		{
			return _decayTime;
		}
		set
		{
			_decayTime = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float startFrequency
	{
		get
		{
			return _startFrequency;
		}
		set
		{
			_startFrequency = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float minFrequency
	{
		get
		{
			return _minFrequency;
		}
		set
		{
			_minFrequency = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float slide
	{
		get
		{
			return _slide;
		}
		set
		{
			_slide = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float deltaSlide
	{
		get
		{
			return _deltaSlide;
		}
		set
		{
			_deltaSlide = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float vibratoDepth
	{
		get
		{
			return _vibratoDepth;
		}
		set
		{
			_vibratoDepth = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float vibratoSpeed
	{
		get
		{
			return _vibratoSpeed;
		}
		set
		{
			_vibratoSpeed = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float changeAmount
	{
		get
		{
			return _changeAmount;
		}
		set
		{
			_changeAmount = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float changeSpeed
	{
		get
		{
			return _changeSpeed;
		}
		set
		{
			_changeSpeed = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float squareDuty
	{
		get
		{
			return _squareDuty;
		}
		set
		{
			_squareDuty = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float dutySweep
	{
		get
		{
			return _dutySweep;
		}
		set
		{
			_dutySweep = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float repeatSpeed
	{
		get
		{
			return _repeatSpeed;
		}
		set
		{
			_repeatSpeed = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float phaserOffset
	{
		get
		{
			return _phaserOffset;
		}
		set
		{
			_phaserOffset = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float phaserSweep
	{
		get
		{
			return _phaserSweep;
		}
		set
		{
			_phaserSweep = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float lpFilterCutoff
	{
		get
		{
			return _lpFilterCutoff;
		}
		set
		{
			_lpFilterCutoff = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float lpFilterCutoffSweep
	{
		get
		{
			return _lpFilterCutoffSweep;
		}
		set
		{
			_lpFilterCutoffSweep = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float lpFilterResonance
	{
		get
		{
			return _lpFilterResonance;
		}
		set
		{
			_lpFilterResonance = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float hpFilterCutoff
	{
		get
		{
			return _hpFilterCutoff;
		}
		set
		{
			_hpFilterCutoff = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float hpFilterCutoffSweep
	{
		get
		{
			return _hpFilterCutoffSweep;
		}
		set
		{
			_hpFilterCutoffSweep = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float changeRepeat
	{
		get
		{
			return _changeRepeat;
		}
		set
		{
			_changeRepeat = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float changeAmount2
	{
		get
		{
			return _changeAmount2;
		}
		set
		{
			_changeAmount2 = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public float changeSpeed2
	{
		get
		{
			return _changeSpeed2;
		}
		set
		{
			_changeSpeed2 = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float compressionAmount
	{
		get
		{
			return _compressionAmount;
		}
		set
		{
			_compressionAmount = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float overtones
	{
		get
		{
			return _overtones;
		}
		set
		{
			_overtones = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float overtoneFalloff
	{
		get
		{
			return _overtoneFalloff;
		}
		set
		{
			_overtoneFalloff = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float bitCrush
	{
		get
		{
			return _bitCrush;
		}
		set
		{
			_bitCrush = Mathf.Clamp(value, 0f, 1f);
			paramsDirty = true;
		}
	}

	public float bitCrushSweep
	{
		get
		{
			return _bitCrushSweep;
		}
		set
		{
			_bitCrushSweep = Mathf.Clamp(value, -1f, 1f);
			paramsDirty = true;
		}
	}

	public void GeneratePickupCoin()
	{
		resetParams();
		_startFrequency = 0.4f + GetRandom() * 0.5f;
		_sustainTime = GetRandom() * 0.1f;
		_decayTime = 0.1f + GetRandom() * 0.4f;
		_sustainPunch = 0.3f + GetRandom() * 0.3f;
		if (GetRandomBool())
		{
			_changeSpeed = 0.5f + GetRandom() * 0.2f;
			int num = (int)(GetRandom() * 7f) + 1;
			int num2 = num + (int)(GetRandom() * 7f) + 2;
			_changeAmount = (float)num / (float)num2;
		}
	}

	public void GenerateLaserShoot()
	{
		resetParams();
		_waveType = (uint)(GetRandom() * 3f);
		if (_waveType == 2 && GetRandomBool())
		{
			_waveType = (uint)(GetRandom() * 2f);
		}
		_startFrequency = 0.5f + GetRandom() * 0.5f;
		_minFrequency = _startFrequency - 0.2f - GetRandom() * 0.6f;
		if (_minFrequency < 0.2f)
		{
			_minFrequency = 0.2f;
		}
		_slide = -0.15f - GetRandom() * 0.2f;
		if (GetRandom() < 0.33f)
		{
			_startFrequency = 0.3f + GetRandom() * 0.6f;
			_minFrequency = GetRandom() * 0.1f;
			_slide = -0.35f - GetRandom() * 0.3f;
		}
		if (GetRandomBool())
		{
			_squareDuty = GetRandom() * 0.5f;
			_dutySweep = GetRandom() * 0.2f;
		}
		else
		{
			_squareDuty = 0.4f + GetRandom() * 0.5f;
			_dutySweep = (0f - GetRandom()) * 0.7f;
		}
		_sustainTime = 0.1f + GetRandom() * 0.2f;
		_decayTime = GetRandom() * 0.4f;
		if (GetRandomBool())
		{
			_sustainPunch = GetRandom() * 0.3f;
		}
		if (GetRandom() < 0.33f)
		{
			_phaserOffset = GetRandom() * 0.2f;
			_phaserSweep = (0f - GetRandom()) * 0.2f;
		}
		if (GetRandomBool())
		{
			_hpFilterCutoff = GetRandom() * 0.3f;
		}
	}

	public void GenerateExplosion()
	{
		resetParams();
		_waveType = 3u;
		if (GetRandomBool())
		{
			_startFrequency = 0.1f + GetRandom() * 0.4f;
			_slide = -0.1f + GetRandom() * 0.4f;
		}
		else
		{
			_startFrequency = 0.2f + GetRandom() * 0.7f;
			_slide = -0.2f - GetRandom() * 0.2f;
		}
		_startFrequency *= _startFrequency;
		if (GetRandom() < 0.2f)
		{
			_slide = 0f;
		}
		if (GetRandom() < 0.33f)
		{
			_repeatSpeed = 0.3f + GetRandom() * 0.5f;
		}
		_sustainTime = 0.1f + GetRandom() * 0.3f;
		_decayTime = GetRandom() * 0.5f;
		_sustainPunch = 0.2f + GetRandom() * 0.6f;
		if (GetRandomBool())
		{
			_phaserOffset = -0.3f + GetRandom() * 0.9f;
			_phaserSweep = (0f - GetRandom()) * 0.3f;
		}
		if (GetRandom() < 0.33f)
		{
			_changeSpeed = 0.6f + GetRandom() * 0.3f;
			_changeAmount = 0.8f - GetRandom() * 1.6f;
		}
	}

	public void GeneratePowerup()
	{
		resetParams();
		if (GetRandomBool())
		{
			_waveType = 1u;
		}
		else
		{
			_squareDuty = GetRandom() * 0.6f;
		}
		if (GetRandomBool())
		{
			_startFrequency = 0.2f + GetRandom() * 0.3f;
			_slide = 0.1f + GetRandom() * 0.4f;
			_repeatSpeed = 0.4f + GetRandom() * 0.4f;
		}
		else
		{
			_startFrequency = 0.2f + GetRandom() * 0.3f;
			_slide = 0.05f + GetRandom() * 0.2f;
			if (GetRandomBool())
			{
				_vibratoDepth = GetRandom() * 0.7f;
				_vibratoSpeed = GetRandom() * 0.6f;
			}
		}
		_sustainTime = GetRandom() * 0.4f;
		_decayTime = 0.1f + GetRandom() * 0.4f;
	}

	public void GenerateHitHurt()
	{
		resetParams();
		_waveType = (uint)(GetRandom() * 3f);
		if (_waveType == 2)
		{
			_waveType = 3u;
		}
		else if (_waveType == 0)
		{
			_squareDuty = GetRandom() * 0.6f;
		}
		_startFrequency = 0.2f + GetRandom() * 0.6f;
		_slide = -0.3f - GetRandom() * 0.4f;
		_sustainTime = GetRandom() * 0.1f;
		_decayTime = 0.1f + GetRandom() * 0.2f;
		if (GetRandomBool())
		{
			_hpFilterCutoff = GetRandom() * 0.3f;
		}
	}

	public void GenerateJump()
	{
		resetParams();
		_waveType = 0u;
		_squareDuty = GetRandom() * 0.6f;
		_startFrequency = 0.3f + GetRandom() * 0.3f;
		_slide = 0.1f + GetRandom() * 0.2f;
		_sustainTime = 0.1f + GetRandom() * 0.3f;
		_decayTime = 0.1f + GetRandom() * 0.2f;
		if (GetRandomBool())
		{
			_hpFilterCutoff = GetRandom() * 0.3f;
		}
		if (GetRandomBool())
		{
			_lpFilterCutoff = 1f - GetRandom() * 0.6f;
		}
	}

	public void GenerateBlipSelect()
	{
		resetParams();
		_waveType = (uint)(GetRandom() * 2f);
		if (_waveType == 0)
		{
			_squareDuty = GetRandom() * 0.6f;
		}
		_startFrequency = 0.2f + GetRandom() * 0.4f;
		_sustainTime = 0.1f + GetRandom() * 0.1f;
		_decayTime = GetRandom() * 0.2f;
		_hpFilterCutoff = 0.1f;
	}

	protected void resetParams()
	{
		paramsDirty = true;
		_waveType = 0u;
		_startFrequency = 0.3f;
		_minFrequency = 0f;
		_slide = 0f;
		_deltaSlide = 0f;
		_squareDuty = 0f;
		_dutySweep = 0f;
		_vibratoDepth = 0f;
		_vibratoSpeed = 0f;
		_attackTime = 0f;
		_sustainTime = 0.3f;
		_decayTime = 0.4f;
		_sustainPunch = 0f;
		_lpFilterResonance = 0f;
		_lpFilterCutoff = 1f;
		_lpFilterCutoffSweep = 0f;
		_hpFilterCutoff = 0f;
		_hpFilterCutoffSweep = 0f;
		_phaserOffset = 0f;
		_phaserSweep = 0f;
		_repeatSpeed = 0f;
		_changeSpeed = 0f;
		_changeAmount = 0f;
		_changeRepeat = 0f;
		_changeAmount2 = 0f;
		_changeSpeed2 = 0f;
		_compressionAmount = 0.3f;
		_overtones = 0f;
		_overtoneFalloff = 0f;
		_bitCrush = 0f;
		_bitCrushSweep = 0f;
	}

	public void Mutate(float __mutation = 0.05f)
	{
		if (GetRandomBool())
		{
			startFrequency += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			minFrequency += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			slide += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			deltaSlide += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			squareDuty += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			dutySweep += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			vibratoDepth += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			vibratoSpeed += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			attackTime += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			sustainTime += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			decayTime += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			sustainPunch += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			lpFilterCutoff += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			lpFilterCutoffSweep += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			lpFilterResonance += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			hpFilterCutoff += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			hpFilterCutoffSweep += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			phaserOffset += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			phaserSweep += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			repeatSpeed += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			changeSpeed += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			changeAmount += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			changeRepeat += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			changeAmount2 += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			changeSpeed2 += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			compressionAmount += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			overtones += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			overtoneFalloff += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			bitCrush += GetRandom() * __mutation * 2f - __mutation;
		}
		if (GetRandomBool())
		{
			bitCrushSweep += GetRandom() * __mutation * 2f - __mutation;
		}
	}

	public void Randomize()
	{
		resetParams();
		_waveType = (uint)(GetRandom() * 9f);
		_attackTime = Pow(GetRandom() * 2f - 1f, 4);
		_sustainTime = Pow(GetRandom() * 2f - 1f, 2);
		_sustainPunch = Pow(GetRandom() * 0.8f, 2);
		_decayTime = GetRandom();
		_startFrequency = (GetRandomBool() ? Pow(GetRandom() * 2f - 1f, 2) : (Pow(GetRandom() * 0.5f, 3) + 0.5f));
		_minFrequency = 0f;
		_slide = Pow(GetRandom() * 2f - 1f, 3);
		_deltaSlide = Pow(GetRandom() * 2f - 1f, 3);
		_vibratoDepth = Pow(GetRandom() * 2f - 1f, 3);
		_vibratoSpeed = GetRandom() * 2f - 1f;
		_changeAmount = GetRandom() * 2f - 1f;
		_changeSpeed = GetRandom() * 2f - 1f;
		_squareDuty = GetRandom() * 2f - 1f;
		_dutySweep = Pow(GetRandom() * 2f - 1f, 3);
		_repeatSpeed = GetRandom() * 2f - 1f;
		_phaserOffset = Pow(GetRandom() * 2f - 1f, 3);
		_phaserSweep = Pow(GetRandom() * 2f - 1f, 3);
		_lpFilterCutoff = 1f - Pow(GetRandom(), 3);
		_lpFilterCutoffSweep = Pow(GetRandom() * 2f - 1f, 3);
		_lpFilterResonance = GetRandom() * 2f - 1f;
		_hpFilterCutoff = Pow(GetRandom(), 5);
		_hpFilterCutoffSweep = Pow(GetRandom() * 2f - 1f, 5);
		if (_attackTime + _sustainTime + _decayTime < 0.2f)
		{
			_sustainTime = 0.2f + GetRandom() * 0.3f;
			_decayTime = 0.2f + GetRandom() * 0.3f;
		}
		if ((_startFrequency > 0.7f && (double)_slide > 0.2) || ((double)_startFrequency < 0.2 && (double)_slide < -0.05))
		{
			_slide = 0f - _slide;
		}
		if (_lpFilterCutoff < 0.1f && _lpFilterCutoffSweep < -0.05f)
		{
			_lpFilterCutoffSweep = 0f - _lpFilterCutoffSweep;
		}
		_changeRepeat = GetRandom();
		_changeAmount2 = GetRandom() * 2f - 1f;
		_changeSpeed2 = GetRandom();
		_compressionAmount = GetRandom();
		_overtones = GetRandom();
		_overtoneFalloff = GetRandom();
		_bitCrush = GetRandom();
		_bitCrushSweep = GetRandom() * 2f - 1f;
	}

	public string GetSettingsStringLegacy()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("" + waveType + ",", To4DP(_attackTime), ","), To4DP(_sustainTime), ","), To4DP(_sustainPunch), ","), To4DP(_decayTime), ","), To4DP(_startFrequency), ","), To4DP(_minFrequency), ","), To4DP(_slide), ","), To4DP(_deltaSlide), ","), To4DP(_vibratoDepth), ","), To4DP(_vibratoSpeed), ","), To4DP(_changeAmount), ","), To4DP(_changeSpeed), ","), To4DP(_squareDuty), ","), To4DP(_dutySweep), ","), To4DP(_repeatSpeed), ","), To4DP(_phaserOffset), ","), To4DP(_phaserSweep), ","), To4DP(_lpFilterCutoff), ","), To4DP(_lpFilterCutoffSweep), ","), To4DP(_lpFilterResonance), ","), To4DP(_hpFilterCutoff), ","), To4DP(_hpFilterCutoffSweep), ","), To4DP(_masterVolume));
	}

	public string GetSettingsString()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("" + waveType + ",", To4DP(_masterVolume), ","), To4DP(_attackTime), ","), To4DP(_sustainTime), ","), To4DP(_sustainPunch), ","), To4DP(_decayTime), ","), To4DP(_compressionAmount), ","), To4DP(_startFrequency), ","), To4DP(_minFrequency), ","), To4DP(_slide), ","), To4DP(_deltaSlide), ","), To4DP(_vibratoDepth), ","), To4DP(_vibratoSpeed), ","), To4DP(_overtones), ","), To4DP(_overtoneFalloff), ","), To4DP(_changeRepeat), ","), To4DP(_changeAmount), ","), To4DP(_changeSpeed), ","), To4DP(_changeAmount2), ","), To4DP(_changeSpeed2), ","), To4DP(_squareDuty), ","), To4DP(_dutySweep), ","), To4DP(_repeatSpeed), ","), To4DP(_phaserOffset), ","), To4DP(_phaserSweep), ","), To4DP(_lpFilterCutoff), ","), To4DP(_lpFilterCutoffSweep), ","), To4DP(_lpFilterResonance), ","), To4DP(_hpFilterCutoff), ","), To4DP(_hpFilterCutoffSweep), ","), To4DP(_bitCrush), ","), To4DP(_bitCrushSweep));
	}

	public bool SetSettingsString(string __string)
	{
		string[] array = __string.Split(new char[1] { ',' });
		if (array.Length == 24)
		{
			resetParams();
			waveType = ParseUint(array[0]);
			attackTime = ParseFloat(array[1]);
			sustainTime = ParseFloat(array[2]);
			sustainPunch = ParseFloat(array[3]);
			decayTime = ParseFloat(array[4]);
			startFrequency = ParseFloat(array[5]);
			minFrequency = ParseFloat(array[6]);
			slide = ParseFloat(array[7]);
			deltaSlide = ParseFloat(array[8]);
			vibratoDepth = ParseFloat(array[9]);
			vibratoSpeed = ParseFloat(array[10]);
			changeAmount = ParseFloat(array[11]);
			changeSpeed = ParseFloat(array[12]);
			squareDuty = ParseFloat(array[13]);
			dutySweep = ParseFloat(array[14]);
			repeatSpeed = ParseFloat(array[15]);
			phaserOffset = ParseFloat(array[16]);
			phaserSweep = ParseFloat(array[17]);
			lpFilterCutoff = ParseFloat(array[18]);
			lpFilterCutoffSweep = ParseFloat(array[19]);
			lpFilterResonance = ParseFloat(array[20]);
			hpFilterCutoff = ParseFloat(array[21]);
			hpFilterCutoffSweep = ParseFloat(array[22]);
			masterVolume = ParseFloat(array[23]);
		}
		else
		{
			if (array.Length < 32)
			{
				Debug.LogError((object)("Could not paste settings string: parameters contain " + array.Length + " values (was expecting 24 or >32)"));
				return false;
			}
			resetParams();
			waveType = ParseUint(array[0]);
			masterVolume = ParseFloat(array[1]);
			attackTime = ParseFloat(array[2]);
			sustainTime = ParseFloat(array[3]);
			sustainPunch = ParseFloat(array[4]);
			decayTime = ParseFloat(array[5]);
			compressionAmount = ParseFloat(array[6]);
			startFrequency = ParseFloat(array[7]);
			minFrequency = ParseFloat(array[8]);
			slide = ParseFloat(array[9]);
			deltaSlide = ParseFloat(array[10]);
			vibratoDepth = ParseFloat(array[11]);
			vibratoSpeed = ParseFloat(array[12]);
			overtones = ParseFloat(array[13]);
			overtoneFalloff = ParseFloat(array[14]);
			changeRepeat = ParseFloat(array[15]);
			changeAmount = ParseFloat(array[16]);
			changeSpeed = ParseFloat(array[17]);
			changeAmount2 = ParseFloat(array[18]);
			changeSpeed2 = ParseFloat(array[19]);
			squareDuty = ParseFloat(array[20]);
			dutySweep = ParseFloat(array[21]);
			repeatSpeed = ParseFloat(array[22]);
			phaserOffset = ParseFloat(array[23]);
			phaserSweep = ParseFloat(array[24]);
			lpFilterCutoff = ParseFloat(array[25]);
			lpFilterCutoffSweep = ParseFloat(array[26]);
			lpFilterResonance = ParseFloat(array[27]);
			hpFilterCutoff = ParseFloat(array[28]);
			hpFilterCutoffSweep = ParseFloat(array[29]);
			bitCrush = ParseFloat(array[30]);
			bitCrushSweep = ParseFloat(array[31]);
		}
		return true;
	}

	public SfxrParams Clone()
	{
		SfxrParams sfxrParams = new SfxrParams();
		sfxrParams.CopyFrom(this);
		return sfxrParams;
	}

	public void CopyFrom(SfxrParams __params, bool __makeDirty = false)
	{
		bool flag = paramsDirty;
		SetSettingsString(GetSettingsString());
		paramsDirty = flag || __makeDirty;
	}

	private float Pow(float __pbase, int __power)
	{
		return __power switch
		{
			2 => __pbase * __pbase, 
			3 => __pbase * __pbase * __pbase, 
			4 => __pbase * __pbase * __pbase * __pbase, 
			5 => __pbase * __pbase * __pbase * __pbase * __pbase, 
			_ => 1f, 
		};
	}

	private string To4DP(float __value)
	{
		if (__value < 0.0001f && __value > -0.0001f)
		{
			return "";
		}
		return __value.ToString("#.####");
	}

	private uint ParseUint(string __value)
	{
		if (__value.Length == 0)
		{
			return 0u;
		}
		return uint.Parse(__value);
	}

	private float ParseFloat(string __value)
	{
		if (__value.Length == 0)
		{
			return 0f;
		}
		return float.Parse(__value);
	}

	private float GetRandom()
	{
		return Random.value % 1f;
	}

	private bool GetRandomBool()
	{
		return Random.value > 0.5f;
	}
}
