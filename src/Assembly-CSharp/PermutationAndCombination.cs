using System;
using System.Collections.Generic;

// Token: 0x020001C7 RID: 455
public class PermutationAndCombination<T>
{
	// Token: 0x060012F1 RID: 4849 RVA: 0x0007703C File Offset: 0x0007523C
	public static void Swap(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00077064 File Offset: 0x00075264
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

	// Token: 0x060012F3 RID: 4851 RVA: 0x000770D8 File Offset: 0x000752D8
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

	// Token: 0x060012F4 RID: 4852 RVA: 0x0007714C File Offset: 0x0007534C
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

	// Token: 0x060012F5 RID: 4853 RVA: 0x00077178 File Offset: 0x00075378
	public static List<T[]> GetPermutation(T[] t)
	{
		return PermutationAndCombination<T>.GetPermutation(t, 0, t.Length - 1);
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00077188 File Offset: 0x00075388
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

	// Token: 0x060012F7 RID: 4855 RVA: 0x000771DC File Offset: 0x000753DC
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
