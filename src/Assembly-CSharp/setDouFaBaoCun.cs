using UnityEngine;

public class setDouFaBaoCun : MonoBehaviour
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			((Component)this).gameObject.SetActive(false);
		}
	}

	private void Update()
	{
	}
}
