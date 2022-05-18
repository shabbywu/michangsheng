using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000938 RID: 2360
	public class BuildingCategory : MonoBehaviour
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x0002B8BA File Offset: 0x00029ABA
		public string CategoryName
		{
			get
			{
				return this.m_CategoryName;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x0002B8C2 File Offset: 0x00029AC2
		public Vector2 DesiredOffset
		{
			get
			{
				return this.m_DesiredOffset;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x0002B8CA File Offset: 0x00029ACA
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x001B07F4 File Offset: 0x001AE9F4
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

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x0002B8D2 File Offset: 0x00029AD2
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x0002B8DA File Offset: 0x00029ADA
		public float Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x0002B8E2 File Offset: 0x00029AE2
		public float Spacing
		{
			get
			{
				return this.m_Spacing;
			}
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x0002B8EA File Offset: 0x00029AEA
		public BuildingPiece SelectFirst()
		{
			if (this.m_Pieces.Length != 0)
			{
				this.Select(0);
				return this.m_HighlightedPiece;
			}
			return null;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x0002B904 File Offset: 0x00029B04
		public BuildingPiece SelectNext()
		{
			this.Select(this.m_CurrentIndex + 1);
			return this.m_HighlightedPiece;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x0002B91A File Offset: 0x00029B1A
		public BuildingPiece SelectPrevious()
		{
			this.Select(this.m_CurrentIndex - 1);
			return this.m_HighlightedPiece;
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x001B082C File Offset: 0x001AEA2C
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

		// Token: 0x06003C66 RID: 15462 RVA: 0x0002B930 File Offset: 0x00029B30
		private void Awake()
		{
			this.m_Pieces = base.GetComponentsInChildren<BuildingPiece>();
		}

		// Token: 0x0400369B RID: 13979
		[SerializeField]
		private string m_CategoryName;

		// Token: 0x0400369C RID: 13980
		[SerializeField]
		private Vector2 m_DesiredOffset;

		// Token: 0x0400369D RID: 13981
		[SerializeField]
		[Range(0.5f, 2f)]
		private float m_SelectionScale = 1.1f;

		// Token: 0x0400369E RID: 13982
		[Header("Layout")]
		[SerializeField]
		private float m_Distance = 211.7f;

		// Token: 0x0400369F RID: 13983
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Offset;

		// Token: 0x040036A0 RID: 13984
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Spacing;

		// Token: 0x040036A1 RID: 13985
		private BuildingPiece[] m_Pieces;

		// Token: 0x040036A2 RID: 13986
		private bool m_ShowPieces;

		// Token: 0x040036A3 RID: 13987
		private BuildingPiece m_HighlightedPiece;

		// Token: 0x040036A4 RID: 13988
		private float m_ClosestPieceAngle;

		// Token: 0x040036A5 RID: 13989
		private int m_CurrentIndex;
	}
}
