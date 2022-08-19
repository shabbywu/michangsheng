using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000643 RID: 1603
	public class ObjectHolder
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x00167F0D File Offset: 0x0016610D
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x00167F15 File Offset: 0x00166115
		public List<GameObject> ObjectList
		{
			get
			{
				return this.m_ObjectList;
			}
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(ObjectHolder holder)
		{
			return holder != null;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x00167F1D File Offset: 0x0016611D
		public ObjectHolder(string name, List<GameObject> objectList)
		{
			this.m_Name = name;
			this.m_ObjectList = objectList;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x00167F34 File Offset: 0x00166134
		public void ActivateObjects(bool active)
		{
			for (int i = 0; i < this.m_ObjectList.Count; i++)
			{
				this.m_ObjectList[i].SetActive(active);
			}
		}

		// Token: 0x04002D5A RID: 11610
		private string m_Name;

		// Token: 0x04002D5B RID: 11611
		private List<GameObject> m_ObjectList;
	}
}
