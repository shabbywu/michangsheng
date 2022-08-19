using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063E RID: 1598
	public class BuildingCategory : MonoBehaviour
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x001675FE File Offset: 0x001657FE
		public string CategoryName
		{
			get
			{
				return this.m_CategoryName;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x00167606 File Offset: 0x00165806
		public Vector2 DesiredOffset
		{
			get
			{
				return this.m_DesiredOffset;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x0016760E File Offset: 0x0016580E
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x00167618 File Offset: 0x00165818
		public bool ShowPieces
		{
			get
			{
				return this.m_ShowPieces;
			}
			set
			{
				BuildingPiece[] pieces = this.m_Pieces;
				for (int i = 0; i < pieces.Length; i++)
				{
					pieces[i].gameObject.SetActive(value);
				}
				this.m_ShowPieces = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x0016764F File Offset: 0x0016584F
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x00167657 File Offset: 0x00165857
		public float Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x0016765F File Offset: 0x0016585F
		public float Spacing
		{
			get
			{
				return this.m_Spacing;
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x00167667 File Offset: 0x00165867
		public BuildingPiece SelectFirst()
		{
			if (this.m_Pieces.Length != 0)
			{
				this.Select(0);
				return this.m_HighlightedPiece;
			}
			return null;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x00167681 File Offset: 0x00165881
		public BuildingPiece SelectNext()
		{
			this.Select(this.m_CurrentIndex + 1);
			return this.m_HighlightedPiece;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x00167697 File Offset: 0x00165897
		public BuildingPiece SelectPrevious()
		{
			this.Select(this.m_CurrentIndex - 1);
			return this.m_HighlightedPiece;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x001676B0 File Offset: 0x001658B0
		private void Select(int index)
		{
			this.m_CurrentIndex = (int)Mathf.Repeat((float)index, (float)this.m_Pieces.Length);
			this.m_HighlightedPiece = this.m_Pieces[this.m_CurrentIndex];
			for (int i = 0; i < this.m_Pieces.Length; i++)
			{
				if (this.m_Pieces[i] == this.m_HighlightedPiece)
				{
					this.m_Pieces[i].transform.localScale = this.m_SelectionScale * Vector3.one;
					this.m_Pieces[i].SetCustomColor(new Color(0f, 1f, 0f, 0.85f));
				}
				else
				{
					this.m_Pieces[i].transform.localScale = Vector3.one;
					this.m_Pieces[i].SetDefaultColor();
				}
			}
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x00167783 File Offset: 0x00165983
		private void Awake()
		{
			this.m_Pieces = base.GetComponentsInChildren<BuildingPiece>();
		}

		// Token: 0x04002D32 RID: 11570
		[SerializeField]
		private string m_CategoryName;

		// Token: 0x04002D33 RID: 11571
		[SerializeField]
		private Vector2 m_DesiredOffset;

		// Token: 0x04002D34 RID: 11572
		[SerializeField]
		[Range(0.5f, 2f)]
		private float m_SelectionScale = 1.1f;

		// Token: 0x04002D35 RID: 11573
		[Header("Layout")]
		[SerializeField]
		private float m_Distance = 211.7f;

		// Token: 0x04002D36 RID: 11574
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Offset;

		// Token: 0x04002D37 RID: 11575
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Spacing;

		// Token: 0x04002D38 RID: 11576
		private BuildingPiece[] m_Pieces;

		// Token: 0x04002D39 RID: 11577
		private bool m_ShowPieces;

		// Token: 0x04002D3A RID: 11578
		private BuildingPiece m_HighlightedPiece;

		// Token: 0x04002D3B RID: 11579
		private float m_ClosestPieceAngle;

		// Token: 0x04002D3C RID: 11580
		private int m_CurrentIndex;
	}
}
