using System;
using UltimateSurvival.Building;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063F RID: 1599
	public class BuildingPiece : MonoBehaviour
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x001677AF File Offset: 0x001659AF
		public string PieceName
		{
			get
			{
				return this.m_PieceName;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x001677B7 File Offset: 0x001659B7
		public Sprite Icon
		{
			get
			{
				return this.m_Image.sprite;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x001677C4 File Offset: 0x001659C4
		public Vector2 DesiredOffset
		{
			get
			{
				return this.m_DesiredOffset;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x001677CC File Offset: 0x001659CC
		public BuildingPiece BuildableObject
		{
			get
			{
				return this.m_BuildableObject;
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x001677D4 File Offset: 0x001659D4
		public void SetCustomColor(Color color)
		{
			this.m_Image.color = color;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x001677E2 File Offset: 0x001659E2
		public void SetDefaultColor()
		{
			this.m_Image.color = this.m_DefaultColor;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x001677F5 File Offset: 0x001659F5
		private void Awake()
		{
			this.m_Image = base.GetComponent<Image>();
			this.m_DefaultColor = this.m_Image.color;
		}

		// Token: 0x04002D3D RID: 11581
		[SerializeField]
		private string m_PieceName;

		// Token: 0x04002D3E RID: 11582
		[SerializeField]
		private Vector2 m_DesiredOffset;

		// Token: 0x04002D3F RID: 11583
		[SerializeField]
		private BuildingPiece m_BuildableObject;

		// Token: 0x04002D40 RID: 11584
		private Image m_Image;

		// Token: 0x04002D41 RID: 11585
		private Color m_DefaultColor;
	}
}
