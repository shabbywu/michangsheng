using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200089A RID: 2202
	[Serializable]
	public class ItemData
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x000293EF File Offset: 0x000275EF
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000293F7 File Offset: 0x000275F7
		public string DisplayName
		{
			get
			{
				return this.m_DisplayName;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x000293FF File Offset: 0x000275FF
		// (set) Token: 0x060038A9 RID: 14505 RVA: 0x00029407 File Offset: 0x00027607
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x00029410 File Offset: 0x00027610
		// (set) Token: 0x060038AB RID: 14507 RVA: 0x00029418 File Offset: 0x00027618
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

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x00029421 File Offset: 0x00027621
		public Sprite Icon
		{
			get
			{
				return this.m_Icon;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x00029429 File Offset: 0x00027629
		public GameObject WorldObject
		{
			get
			{
				return this.m_WorldObject;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x00029431 File Offset: 0x00027631
		public string[] Descriptions
		{
			get
			{
				return this.m_Descriptions;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x00029439 File Offset: 0x00027639
		public int StackSize
		{
			get
			{
				return this.m_StackSize;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x00029441 File Offset: 0x00027641
		public List<ItemProperty.Value> PropertyValues
		{
			get
			{
				return this.m_PropertyValues;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x00029449 File Offset: 0x00027649
		public bool IsBuildable
		{
			get
			{
				return this.m_IsBuildable;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x00029451 File Offset: 0x00027651
		public bool IsCraftable
		{
			get
			{
				return this.m_IsCraftable;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x00029459 File Offset: 0x00027659
		public Recipe Recipe
		{
			get
			{
				return this.m_Recipe;
			}
		}

		// Token: 0x040032FF RID: 13055
		[SerializeField]
		private string m_Name;

		// Token: 0x04003300 RID: 13056
		[SerializeField]
		private string m_DisplayName;

		// Token: 0x04003301 RID: 13057
		[SerializeField]
		private int m_Id;

		// Token: 0x04003302 RID: 13058
		[SerializeField]
		private string m_Category;

		// Token: 0x04003303 RID: 13059
		[SerializeField]
		private Sprite m_Icon;

		// Token: 0x04003304 RID: 13060
		[SerializeField]
		private GameObject m_WorldObject;

		// Token: 0x04003305 RID: 13061
		[SerializeField]
		[Multiline]
		private string[] m_Descriptions;

		// Token: 0x04003306 RID: 13062
		[SerializeField]
		private int m_StackSize = 1;

		// Token: 0x04003307 RID: 13063
		[SerializeField]
		private List<ItemProperty.Value> m_PropertyValues;

		// Token: 0x04003308 RID: 13064
		[SerializeField]
		private bool m_IsBuildable;

		// Token: 0x04003309 RID: 13065
		[SerializeField]
		private bool m_IsCraftable;

		// Token: 0x0400330A RID: 13066
		[SerializeField]
		private Recipe m_Recipe;
	}
}
