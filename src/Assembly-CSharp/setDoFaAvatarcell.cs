using UnityEngine;

public class setDoFaAvatarcell : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			((Component)this).gameObject.SetActive(false);
		}
	}
}
