using System;
using System.Collections.Generic;

// Token: 0x020002CE RID: 718
public class PermutationAndCombination<T>
{
	// Token: 0x060015A6 RID: 5542 RVA: 0x000C43CC File Offset: 0x000C25CC
	public static void Swap(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x000C43F4 File Offset: 0x000C25F4
	private static void GetCombination(ref List<T[]> list, T[] t, int n, int m, int[] b, int M)
	{
		for (int i = n; i >= m; i--)
		{
			b[m - 1] = i - 1;
			if (m > 1)
			{
				PermutationAndCombination<T>.GetCombination(ref list, t, i - 1, m - 1, b, M);
			}
			else
			{
				if (list == null)
				{
					list = new List<T[]>();
				}
				T[] array = new T[M];
				for (int j = 0; j < b.Length; j++)
				{
					array[j] = t[b[j]];
				}
				list.Add(array);
			}
		}
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000C4468 File Offset: 0x000C2668
	private static void GetPermutation(ref List<T[]> list, T[] t, int startIndex, int endIndex)
	{
		if (startIndex == endIndex)
		{
			if (list == null)
			{
				list = new List<T[]>();
			}
			T[] array = new T[t.Length];
			t.CopyTo(array, 0);
			list.Add(array);
			return;
		}
		for (int i = startIndex; i <= endIndex; i++)
		{
			PermutationAndCombination<T>.Swap(ref t[startIndex], ref t[i]);
			PermutationAndCombination<T>.GetPermutation(ref list, t, startIndex + 1, endIndex);
			PermutationAndCombination<T>.Swap(ref t[startIndex], ref t[i]);
		}
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000C44DC File Offset: 0x000C26DC
	public static List<T[]> GetPermutation(T[] t, int startIndex, int endIndex)
	{
		if (startIndex < 0 || endIndex > t.Length - 1)
		{
			return null;
		}
		List<T[]> result = new List<T[]>();
		PermutationAndCombination<T>.GetPermutation(ref result, t, startIndex, endIndex);
		return result;
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x00013843 File Offset: 0x00011A43
	public static List<T[]> GetPermutation(T[] t)
	{
		return PermutationAndCombination<T>.GetPermutation(t, 0, t.Length - 1);
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000C4508 File Offset: 0x000C2708
	public static List<T[]> GetPermutation(T[] t, int n)
	{
		if (n > t.Length)
		{
			return null;
		}
		List<T[]> list = new List<T[]>();
		List<T[]> combination = PermutationAndCombination<T>.GetCombination(t, n);
		for (int i = 0; i < combination.Count; i++)
		{
			List<T[]> collection = new List<T[]>();
			PermutationAndCombination<T>.GetPermutation(ref collection, combination[i], 0, n - 1);
			list.AddRange(collection);
		}
		return list;
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x000C455C File Offset: 0x000C275C
	public static List<T[]> GetCombination(T[] t, int n)
	{
		if (t.Length < n)
		{
			return null;
		}
		int[] b = new int[n];
		List<T[]> result = new List<T[]>();
		PermutationAndCombination<T>.GetCombination(ref result, t, t.Length, n, b, n);
		return result;
	}
}
