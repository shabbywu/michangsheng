using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003A RID: 58
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001711C File Offset: 0x0001531C
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001713A File Offset: 0x0001533A
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001713A File Offset: 0x0001533A
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00017144 File Offset: 0x00015344
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00017184 File Offset: 0x00015384
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000171BC File Offset: 0x000153BC
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		if (!(database != null))
		{
			return null;
		}
		return database.GetItem(id32 & 65535);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x000171EC File Offset: 0x000153EC
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00017250 File Offset: 0x00015450
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400025B RID: 603
	private static InvDatabase[] mList;

	// Token: 0x0400025C RID: 604
	private static bool mIsDirty = true;

	// Token: 0x0400025D RID: 605
	public int databaseID;

	// Token: 0x0400025E RID: 606
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x0400025F RID: 607
	public UIAtlas iconAtlas;
}
