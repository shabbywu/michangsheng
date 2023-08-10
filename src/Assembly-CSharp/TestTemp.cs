using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TestTemp : MonoBehaviour
{
	public List<int> random = new List<int>();

	public Text Text;

	private void Start()
	{
	}

	public int getSum(Dictionary<int, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> item in a)
		{
			num += item.Value;
		}
		return num;
	}

	public void click()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		int value = BitConverter.ToInt32(array, 0);
		value = Math.Abs(value);
		int num = 0;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < random.Count; i++)
		{
			dictionary.Add(i, random[i]);
		}
		int sum = getSum(dictionary);
		int num2 = value % sum;
		int num3 = 0;
		foreach (KeyValuePair<int, int> item in dictionary)
		{
			num3 += item.Value;
			if (num3 >= num2)
			{
				num = item.Key;
				break;
			}
		}
		Text.text = "随机到的值为" + num;
	}

	private void Update()
	{
	}
}
