using System.Collections.Generic;
using UnityEngine;

namespace Bag;

public class SlotList : MonoBehaviour
{
	public List<ISlot> mItemList;

	public void Init()
	{
		mItemList = new List<ISlot>();
		for (int i = 0; i < ((Component)this).transform.childCount; i++)
		{
			mItemList.Add(((Component)((Component)this).transform.GetChild(i)).GetComponent<ISlot>());
		}
	}
}
