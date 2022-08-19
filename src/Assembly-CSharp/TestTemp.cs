using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000178 RID: 376
public class TestTemp : MonoBehaviour
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0005E3B8 File Offset: 0x0005C5B8
	public int getSum(Dictionary<int, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in a)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x0005E40C File Offset: 0x0005C60C
	public void click()
	{
		byte[] array = new byte[8];
		new RNGCryptoServiceProvider().GetBytes(array);
		int num = BitConverter.ToInt32(array, 0);
		num = Math.Abs(num);
		int num2 = 0;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < this.random.Count; i++)
		{
			dictionary.Add(i, this.random[i]);
		}
		int sum = this.getSum(dictionary);
		int num3 = num % sum;
		int num4 = 0;
		foreach (KeyValuePair<int, int> keyValuePair in dictionary)
		{
			num4 += keyValuePair.Value;
			if (num4 >= num3)
			{
				num2 = keyValuePair.Key;
				break;
			}
		}
		this.Text.text = "随机到的值为" + num2;
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000BC2 RID: 3010
	public List<int> random = new List<int>();

	// Token: 0x04000BC3 RID: 3011
	public Text Text;
}
