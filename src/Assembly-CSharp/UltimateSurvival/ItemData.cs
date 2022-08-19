using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D5 RID: 1493
	[Serializable]
	public class ItemData
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x00159916 File Offset: 0x00157B16
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0015991E File Offset: 0x00157B1E
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x00159926 File Offset: 0x00157B26
		// (set) Token: 0x0600300D RID: 12301 RVA: 0x0015992E File Offset: 0x00157B2E
		public int Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x00159937 File Offset: 0x00157B37
		// (set) Token: 0x0600300F RID: 12303 RVA: 0x0015993F File Offset: 0x00157B3F
		public string Category
		{
			get
			{
				return this.m_Category;
			}
			set
			{
				this.m_Category = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x00159948 File Offset: 0x00157B48
		public Sprite Icon
		{
			get
			{
				return this.m_Icon;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x00159950 File Offset: 0x00157B50
		public GameObject WorldObject
		{
			get
			{
				return this.m_WorldObject;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x00159958 File Offset: 0x00157B58
		public string[] Descriptions
		{
			get
			{
				return this.m_Descriptions;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x00159960 File Offset: 0x00157B60
		public int StackSize
		{
			get
			{
				return this.m_StackSize;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x00159968 File Offset: 0x00157B68
		public List<ItemProperty.Value> PropertyValues
		{
			get
			{
				return this.m_PropertyValues;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x00159970 File Offset: 0x00157B70
		public bool IsBuildable
		{
			get
			{
				return this.m_IsBuildable;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x00159978 File Offset: 0x00157B78
		public bool IsCraftable
		{
			get
			{
				return this.m_IsCraftable;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x00159980 File Offset: 0x00157B80
		public Recipe Recipe
		{
			get
			{
				return this.m_Recipe;
			}
		}

		// Token: 0x04002A69 RID: 10857
		[SerializeField]
		private string m_Name;

		// Token: 0x04002A6A RID: 10858
		[SerializeField]
		private string m_DisplayName;

		// Token: 0x04002A6B RID: 10859
		[SerializeField]
		private int m_Id;

		// Token: 0x04002A6C RID: 10860
		[SerializeField]
		private string m_Category;

		// Token: 0x04002A6D RID: 10861
		[SerializeField]
		private Sprite m_Icon;

		// Token: 0x04002A6E RID: 10862
		[SerializeField]
		private GameObject m_WorldObject;

		// Token: 0x04002A6F RID: 10863
		[SerializeField]
		[Multiline]
		private string[] m_Descriptions;

		// Token: 0x04002A70 RID: 10864
		[SerializeField]
		private int m_StackSize = 1;

		// Token: 0x04002A71 RID: 10865
		[SerializeField]
		private List<ItemProperty.Value> m_PropertyValues;

		// Token: 0x04002A72 RID: 10866
		[SerializeField]
		private bool m_IsBuildable;

		// Token: 0x04002A73 RID: 10867
		[SerializeField]
		private bool m_IsCraftable;

		// Token: 0x04002A74 RID: 10868
		[SerializeField]
		private Recipe m_Recipe;
	}
}
