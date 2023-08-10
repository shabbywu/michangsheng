using UnityEngine;

public class SystemConfig
{
	private static SystemConfig _inst;

	public static SystemConfig Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new SystemConfig();
			}
			return _inst;
		}
	}

	public float GetBackGroundVolume()
	{
		return PlayerPrefs.GetFloat("MusicBg", 0.5f);
	}

	public float GetEffectVolume()
	{
		return PlayerPrefs.GetFloat("MusicEffect", 0.5f);
	}

	public int GetSaveTimes()
	{
		return PlayerPrefs.GetInt("SaveTimes", 10);
	}

	public void SetSaveTimes(int value)
	{
		PlayerPrefs.SetInt("SaveTimes", value);
	}

	public int GetNpcActionTimes()
	{
		return PlayerPrefs.GetInt("NpcActionTimes", 0);
	}

	public void SetActionTimes(int value)
	{
		PlayerPrefs.SetInt("NpcActionTimes", value);
	}

	public void Reset()
	{
		Screen.SetResolution(1280, 720, false);
	}
}
