using System.Collections.Generic;
using UnityEngine;

public class SSVPool
{
	private Stack<SSVItem> Items = new Stack<SSVItem>();

	public SSVItem Get()
	{
		SSVItem sSVItem = null;
		if (Items.Count > 0)
		{
			sSVItem = Items.Pop();
			((Component)sSVItem).gameObject.SetActive(true);
		}
		return sSVItem;
	}

	public void Recovery(SSVItem item)
	{
		((Component)item).gameObject.SetActive(false);
		Items.Push(item);
	}
}
