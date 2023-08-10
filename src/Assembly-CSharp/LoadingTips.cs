using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTips : MonoBehaviour
{
	public float UpdateTime = 4f;

	public Text TipText;

	[Multiline]
	public string Tips = "";

	private List<string> _tips = new List<string>();

	private List<int> playIndexs = new List<int>();

	private float cd;

	private void Awake()
	{
		bool flag = false;
		string[] array2;
		try
		{
			string[] array = ModResources.LoadText("tips").Split(new char[1] { '\n' });
			if (array.Length != 0)
			{
				array2 = array;
				foreach (string text in array2)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						_tips.Add(text);
					}
				}
			}
			else
			{
				flag = true;
				Debug.Log((object)"未读取到外部tips，使用内置tips");
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"未读取到外部tips，使用内置tips\n{arg}");
			flag = true;
		}
		if (!flag)
		{
			return;
		}
		array2 = Tips.Split(new char[1] { '\n' });
		foreach (string text2 in array2)
		{
			if (!string.IsNullOrWhiteSpace(text2))
			{
				_tips.Add(text2);
			}
		}
	}

	private void Update()
	{
		cd -= Time.deltaTime;
		if (cd < 0f)
		{
			cd = UpdateTime;
			RandomTip();
		}
	}

	public void RandomTip()
	{
		if (_tips.Count == 0)
		{
			return;
		}
		if (_tips.Count == 1)
		{
			TipText.text = _tips[0];
			return;
		}
		if (playIndexs.Count == 0)
		{
			for (int i = 0; i < _tips.Count; i++)
			{
				playIndexs.Add(i);
			}
			playIndexs.RandomSort();
		}
		int index = playIndexs[0];
		playIndexs.RemoveAt(0);
		TipText.text = _tips[index];
	}
}
public class loadingTips : MonoBehaviour
{
	public List<string> Tips;

	public Text text;

	private void Start()
	{
		int index = jsonData.GetRandom() % Tips.Count;
		text.text = Tips[index];
	}
}
