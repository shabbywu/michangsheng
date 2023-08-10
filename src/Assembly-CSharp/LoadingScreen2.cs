using System;
using System.Collections;
using UnityEngine;

public class LoadingScreen2 : MonoBehaviour
{
	private void Start()
	{
		((MonoBehaviour)this).StartCoroutine(LoadNextLevel());
	}

	private IEnumerator LoadNextLevel()
	{
		GC.Collect();
		Resources.UnloadUnusedAssets();
		yield return (object)new WaitForSeconds(3f);
		Application.LoadLevelAsync(1);
	}
}
