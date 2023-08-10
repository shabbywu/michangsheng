using KBEngine;
using UnityEngine;

public class YSAutoSaveGame : MonoBehaviour
{
	public static bool IsSave;

	public static int RestTime;

	private void Start()
	{
	}

	private void OnDestroy()
	{
		RestTime = 0;
		IsSave = false;
	}

	public void AutoSave()
	{
		((MonoBehaviour)this).Invoke("WaitTime", 0.5f);
	}

	public void WaitTime()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.age <= player.shouYuan && !IsSave)
		{
			IsSave = true;
		}
	}

	private void Update()
	{
	}
}
