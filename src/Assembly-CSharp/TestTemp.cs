using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000255 RID: 597
public class TestTemp : MonoBehaviour
{
	// Token: 0x06001217 RID: 4631 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000ADDE8 File Offset: 0x000ABFE8
	public int getSum(Dictionary<int, int> a)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in a)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000ADE3C File Offset: 0x000AC03C
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

	// Token: 0x0600121A RID: 4634 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000E93 RID: 3731
	public List<int> random = new List<int>();

	// Token: 0x04000E94 RID: 3732
	public Text Text;
}
