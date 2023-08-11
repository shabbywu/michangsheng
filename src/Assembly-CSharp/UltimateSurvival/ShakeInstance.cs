using UnityEngine;

namespace UltimateSurvival;

public class ShakeInstance
{
	public float Magnitude;

	public float Roughness;

	public Vector3 PositionInfluence;

	public Vector3 RotationInfluence;

	public bool DeleteOnInactive = true;

	private float roughMod = 1f;

	private float magnMod = 1f;

	private float fadeOutDuration;

	private float fadeInDuration;

	private bool sustain;

	private float currentFadeTime;

	private float tick;

	private Vector3 amt;

	public bool IsWeapon { get; set; }

	public float ScaleRoughness
	{
		get
		{
			return roughMod;
		}
		set
		{
			roughMod = value;
		}
	}

	public float ScaleMagnitude
	{
		get
		{
			return magnMod;
		}
		set
		{
			magnMod = value;
		}
	}

	public float NormalizedFadeTime => currentFadeTime;

	private bool IsShaking
	{
		get
		{
			if (!(currentFadeTime > 0f))
			{
				return sustain;
			}
			return true;
		}
	}

	private bool IsFadingOut
	{
		get
		{
			if (!sustain)
			{
				return currentFadeTime > 0f;
			}
			return false;
		}
	}

	private bool IsFadingIn
	{
		get
		{
			if (currentFadeTime < 1f && sustain)
			{
				return fadeInDuration > 0f;
			}
			return false;
		}
	}

	public ShakeState CurrentState
	{
		get
		{
			if (IsFadingIn)
			{
				return ShakeState.FadingIn;
			}
			if (IsFadingOut)
			{
				return ShakeState.FadingOut;
			}
			if (IsShaking)
			{
				return ShakeState.Sustained;
			}
			return ShakeState.Inactive;
		}
	}

	public ShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
	{
		Magnitude = magnitude;
		fadeOutDuration = fadeOutTime;
		fadeInDuration = fadeInTime;
		Roughness = roughness;
		if (fadeInTime > 0f)
		{
			sustain = true;
			currentFadeTime = 0f;
		}
		else
		{
			sustain = false;
			currentFadeTime = 1f;
		}
		tick = Random.Range(-100, 100);
	}

	public ShakeInstance(float magnitude, float roughness)
	{
		Magnitude = magnitude;
		Roughness = roughness;
		sustain = true;
		tick = Random.Range(-100, 100);
	}

	public Vector3 UpdateShake()
	{
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		if (IsWeapon)
		{
			amt.x = -1f;
		}
		else
		{
			amt.x = Mathf.PerlinNoise(tick, 0f) - 0.5f;
		}
		amt.y = Mathf.PerlinNoise(0f, tick) - 0.5f;
		amt.z = Mathf.PerlinNoise(tick, tick) - 0.5f;
		if (fadeInDuration > 0f && sustain)
		{
			if (currentFadeTime < 1f)
			{
				currentFadeTime += Time.fixedDeltaTime / fadeInDuration;
			}
			else if (fadeOutDuration > 0f)
			{
				sustain = false;
			}
		}
		if (!sustain)
		{
			currentFadeTime -= Time.fixedDeltaTime / fadeOutDuration;
		}
		if (sustain)
		{
			tick += Time.fixedDeltaTime * Roughness * roughMod;
		}
		else
		{
			tick += Time.fixedDeltaTime * Roughness * roughMod * currentFadeTime;
		}
		return amt * Magnitude * magnMod * currentFadeTime;
	}

	public void StartFadeOut(float fadeOutTime)
	{
		if (fadeOutTime == 0f)
		{
			currentFadeTime = 0f;
		}
		fadeOutDuration = fadeOutTime;
		fadeInDuration = 0f;
		sustain = false;
	}

	public void StartFadeIn(float fadeInTime)
	{
		if (fadeInTime == 0f)
		{
			currentFadeTime = 1f;
		}
		fadeInDuration = fadeInTime;
		fadeOutDuration = 0f;
		sustain = true;
	}
}
