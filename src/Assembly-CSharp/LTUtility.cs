using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class LTUtility
{
	// Token: 0x0600012C RID: 300 RVA: 0x00060F20 File Offset: 0x0005F120
	public static Vector3[] reverse(Vector3[] arr)
	{
		int num = arr.Length;
		int i = 0;
		int num2 = num - 1;
		while (i < num2)
		{
			Vector3 vector = arr[i];
			arr[i] = arr[num2];
			arr[num2] = vector;
			i++;
			num2--;
		}
		return arr;
	}
}
