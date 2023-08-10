using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangHuaGroup : MonoBehaviour
{
	private List<LangHuaItem> langHuaList = new List<LangHuaItem>();

	[Header("播放总时间")]
	[Tooltip("每个浪花间隔 总时间/浪花总数 开启播放")]
	public float TotalTime = 10f;

	private void Start()
	{
		Init();
		((MonoBehaviour)this).StartCoroutine("RandomPlay");
	}

	public void Init()
	{
		LangHuaItem[] componentsInChildren = ((Component)this).GetComponentsInChildren<LangHuaItem>();
		langHuaList.Clear();
		LangHuaItem[] array = componentsInChildren;
		foreach (LangHuaItem langHuaItem in array)
		{
			langHuaList.Add(langHuaItem);
			((Component)langHuaItem).gameObject.SetActive(false);
		}
		langHuaList = langHuaList.RandomSort();
	}

	private IEnumerator RandomPlay()
	{
		if (langHuaList.Count <= 0)
		{
			yield break;
		}
		float timeSpace = TotalTime / (float)langHuaList.Count;
		for (int i = 0; i < langHuaList.Count; i++)
		{
			if ((Object)(object)langHuaList[i] != (Object)null)
			{
				langHuaList[i].Show();
				yield return (object)new WaitForSeconds(timeSpace);
			}
		}
	}
}
