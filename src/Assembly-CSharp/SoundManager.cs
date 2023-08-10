using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static bool soundOn;

	public static bool musicOn;

	private void Awake()
	{
		((Object)this).name = "SoundManager";
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		if (PlayerPrefs.HasKey("soundOn"))
		{
			soundOn = PlayerPrefs.GetInt("soundOn") == 1;
			musicOn = PlayerPrefs.GetInt("musicOn") == 1;
			return;
		}
		soundOn = (musicOn = true);
		PlayerPrefs.SetInt("soundOn", 1);
		PlayerPrefs.SetInt("musicOn", 1);
		PlayerPrefs.Save();
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("soundOn", soundOn ? 1 : 0);
		PlayerPrefs.SetInt("musicOn", musicOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.SetInt("soundOn", soundOn ? 1 : 0);
			PlayerPrefs.SetInt("musicOn", musicOn ? 1 : 0);
			PlayerPrefs.Save();
		}
	}
}
