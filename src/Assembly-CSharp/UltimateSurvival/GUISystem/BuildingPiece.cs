using System;
using UltimateSurvival.Building;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000939 RID: 2361
	public class BuildingPiece : MonoBehaviour
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x0002B95C File Offset: 0x00029B5C
		public string PieceName
		{
			get
			{
				return this.m_PieceName;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x0002B964 File Offset: 0x00029B64
		public Sprite Icon
		{
			get
			{
				return this.m_Image.sprite;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x0002B971 File Offset: 0x00029B71
		public Vector2 DesiredOffset
		{
			get
			{
				return this.m_DesiredOffset;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x0002B979 File Offset: 0x00029B79
		public BuildingPiece BuildableObject
		{
			get
			{
				return this.m_BuildableObject;
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x0002B981 File Offset: 0x00029B81
		public void SetCustomColor(Color color)
		{
			this.m_Image.color = color;
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x0002B98F File Offset: 0x00029B8F
		public void SetDefaultColor()
		{
			this.m_Image.color = this.m_DefaultColor;
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x0002B9A2 File Offset: 0x00029BA2
		private void Awake()
		{
			this.m_Image = base.GetComponent<Image>();
			this.m_DefaultColor = this.m_Image.color;
		}

		// Token: 0x040036A6 RID: 13990
		[SerializeField]
		private string m_PieceName;

		// Token: 0x040036A7 RID: 13991
		[SerializeField]
		private Vector2 m_DesiredOffset;

		// Token: 0x040036A8 RID: 13992
		[SerializeField]
		private BuildingPiece m_BuildableObject;

		// Token: 0x040036A9 RID: 13993
		private Image m_Image;

		// Token: 0x040036AA RID: 13994
		private Color m_DefaultColor;
	}
}
