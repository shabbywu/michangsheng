using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093D RID: 2365
	public class ObjectHolder
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x0002BA9E File Offset: 0x00029C9E
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x0002BAA6 File Offset: 0x00029CA6
		public List<GameObject> ObjectList
		{
			get
			{
				return this.m_ObjectList;
			}
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(ObjectHolder holder)
		{
			return holder != null;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x0002BAAE File Offset: 0x00029CAE
		public ObjectHolder(string name, List<GameObject> objectList)
		{
			this.m_Name = name;
			this.m_ObjectList = objectList;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x001B0F1C File Offset: 0x001AF11C
		public void ActivateObjects(bool active)
		{
			for (int i = 0; i < this.m_ObjectList.Count; i++)
			{
				this.m_ObjectList[i].SetActive(active);
			}
		}

		// Token: 0x040036C3 RID: 14019
		private string m_Name;

		// Token: 0x040036C4 RID: 14020
		private List<GameObject> m_ObjectList;
	}
}
