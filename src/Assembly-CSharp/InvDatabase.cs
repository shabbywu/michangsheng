using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004E RID: 78
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x00007F20 File Offset: 0x00006120
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

	// Token: 0x0600046D RID: 1133 RVA: 0x00007F3E File Offset: 0x0000613E
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00007F3E File Offset: 0x0000613E
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0006E6B8 File Offset: 0x0006C8B8
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

	// Token: 0x06000470 RID: 1136 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
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

	// Token: 0x06000471 RID: 1137 RVA: 0x0006E730 File Offset: 0x0006C930
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		if (!(database != null))
		{
			return null;
		}
		return database.GetItem(id32 & 65535);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0006E760 File Offset: 0x0006C960
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

	// Token: 0x06000473 RID: 1139 RVA: 0x0006E7C4 File Offset: 0x0006C9C4
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

	// Token: 0x040002AB RID: 683
	private static InvDatabase[] mList;

	// Token: 0x040002AC RID: 684
	private static bool mIsDirty = true;

	// Token: 0x040002AD RID: 685
	public int databaseID;

	// Token: 0x040002AE RID: 686
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x040002AF RID: 687
	public UIAtlas iconAtlas;
}
