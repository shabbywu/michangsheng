using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class LTUtility
{
	// Token: 0x06000126 RID: 294 RVA: 0x0000749C File Offset: 0x0000569C
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
