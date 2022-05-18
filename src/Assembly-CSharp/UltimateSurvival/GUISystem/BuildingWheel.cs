using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200093A RID: 2362
	public class BuildingWheel : GUIBehaviour
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x0002B9C1 File Offset: 0x00029BC1
		public RectTransform SelectionHighlight
		{
			get
			{
				return this.m_SelectionHighlight;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x0002B9C9 File Offset: 0x00029BC9
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x0002B9D1 File Offset: 0x00029BD1
		public float Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x0002B9D9 File Offset: 0x00029BD9
		public float Spacing
		{
			get
			{
				return this.m_Spacing;
			}
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x001B0900 File Offset: 0x001AEB00
		private void Update()
		{
			if (!this.m_Window.IsOpen)
			{
				return;
			}
			float num = base.Player.ScrollValue.Get();
			if (this.m_SelectedCategory != null)
			{
				if (this.m_ChoosingPiece && this.m_HighlightedPiece != null)
				{
					if (!this.m_PieceName.enabled)
					{
						this.m_PieceName.enabled = true;
					}
					this.m_PieceName.text = this.m_HighlightedPiece.PieceName;
				}
				else if (!this.m_ChoosingPiece)
				{
					if (!this.m_PieceName.enabled)
					{
						this.m_PieceName.enabled = true;
					}
					this.m_PieceName.text = ((this.m_SelectedPiece == null) ? "" : this.m_SelectedPiece.PieceName);
				}
				if (Input.GetKeyDown(323))
				{
					if (this.m_SelectedCategory.CategoryName == "None")
					{
						base.Player.SelectedBuildable.Set(null);
						base.Player.SelectBuildable.TryStop();
						this.m_PieceName.enabled = false;
						return;
					}
					if (this.m_ChoosingPiece)
					{
						this.m_SelectedPiece = this.m_HighlightedPiece;
						base.Player.SelectedBuildable.Set(this.m_SelectedPiece.BuildableObject);
						this.m_SelectPieceAudio.Play2D(ItemSelectionMethod.RandomlyButExcludeLast);
					}
					this.m_ChoosingPiece = !this.m_ChoosingPiece;
				}
				if (this.m_ChoosingPiece)
				{
					if (!this.m_SelectedCategory.ShowPieces)
					{
						this.m_SelectedCategory.ShowPieces = true;
						this.m_PieceScrollPos = 0f;
						this.m_HighlightedPiece = this.m_SelectedCategory.SelectFirst();
						return;
					}
					this.m_PieceScrollPos += num;
					this.m_PieceScrollPos = Mathf.Clamp(this.m_PieceScrollPos, -this.m_ScrollThreeshold, this.m_ScrollThreeshold);
					if (Mathf.Abs(this.m_PieceScrollPos - this.m_ScrollThreeshold * Mathf.Sign(num)) < Mathf.Epsilon)
					{
						this.m_PieceScrollPos = 0f;
						if (num > 0f)
						{
							this.m_HighlightedPiece = this.m_SelectedCategory.SelectNext();
							return;
						}
						this.m_HighlightedPiece = this.m_SelectedCategory.SelectPrevious();
					}
					return;
				}
				else if (this.m_SelectedCategory.ShowPieces)
				{
					this.m_SelectedCategory.ShowPieces = false;
				}
			}
			this.m_CategoryScrollPos += num;
			this.m_CategoryScrollPos = Mathf.Clamp(this.m_CategoryScrollPos, -this.m_ScrollThreeshold, this.m_ScrollThreeshold);
			Object selectedCategory = this.m_SelectedCategory;
			if (Mathf.Abs(this.m_CategoryScrollPos - this.m_ScrollThreeshold * Mathf.Sign(num)) < Mathf.Epsilon)
			{
				this.m_CategoryScrollPos = 0f;
				this.m_CategoryIndex = (int)Mathf.Repeat((float)(this.m_CategoryIndex + ((num > 0f) ? 1 : -1)), (float)this.m_Categories.Length);
				this.m_SelectedCategory = this.m_Categories[this.m_CategoryIndex];
			}
			if (selectedCategory != this.m_SelectedCategory)
			{
				this.m_Window.Refresh();
				this.m_RefreshAudio.Play2D(ItemSelectionMethod.RandomlyButExcludeLast);
				this.m_CategoryName.text = this.m_SelectedCategory.CategoryName;
			}
			if (this.m_SelectedCategory != null)
			{
				float num2 = this.Offset + this.Spacing * (float)this.m_CategoryIndex;
				this.m_SelectionHighlight.localPosition = Quaternion.Euler(Vector3.back * num2) * Vector3.up * this.Distance;
				this.m_SelectionHighlight.localRotation = Quaternion.Euler(Vector3.back * num2);
			}
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001B0C90 File Offset: 0x001AEE90
		private void Start()
		{
			this.m_Categories = base.GetComponentsInChildren<BuildingCategory>(false);
			base.Player.SelectBuildable.AddStartTryer(new TryerDelegate(this.TryStart_SelectBuildable));
			base.Player.SelectBuildable.AddStopTryer(new TryerDelegate(this.TryStop_SelectBuildable));
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			BuildingCategory[] categories = this.m_Categories;
			for (int i = 0; i < categories.Length; i++)
			{
				categories[i].ShowPieces = false;
			}
			this.m_PieceName.enabled = false;
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x0002B9E1 File Offset: 0x00029BE1
		private void OnChanged_InventoryState()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				while (base.Player.SelectBuildable.Active)
				{
					base.Player.SelectBuildable.TryStop();
				}
			}
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x001B0D28 File Offset: 0x001AEF28
		private bool TryStart_SelectBuildable()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed || !base.Player.EquippedItem.Get() || !base.Player.EquippedItem.Get().HasProperty("Allows Building"))
			{
				return false;
			}
			this.m_Window.Open();
			base.Player.ViewLocked.Set(true);
			return true;
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0002BA14 File Offset: 0x00029C14
		private bool TryStop_SelectBuildable()
		{
			if (this.m_ChoosingPiece)
			{
				this.m_ChoosingPiece = false;
				return false;
			}
			this.m_Window.Close(false);
			base.Player.ViewLocked.Set(false);
			return true;
		}

		// Token: 0x040036AB RID: 13995
		[SerializeField]
		private Window m_Window;

		// Token: 0x040036AC RID: 13996
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x040036AD RID: 13997
		[SerializeField]
		private RectTransform m_SelectionHighlight;

		// Token: 0x040036AE RID: 13998
		[SerializeField]
		private Text m_CategoryName;

		// Token: 0x040036AF RID: 13999
		[SerializeField]
		private Text m_PieceName;

		// Token: 0x040036B0 RID: 14000
		[SerializeField]
		[Range(0f, 50f)]
		private float m_ScrollThreeshold = 1f;

		// Token: 0x040036B1 RID: 14001
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_RefreshAudio;

		// Token: 0x040036B2 RID: 14002
		[SerializeField]
		private SoundPlayer m_SelectPieceAudio;

		// Token: 0x040036B3 RID: 14003
		[Header("Layout")]
		[SerializeField]
		private float m_Distance = 211.7f;

		// Token: 0x040036B4 RID: 14004
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Offset;

		// Token: 0x040036B5 RID: 14005
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Spacing;

		// Token: 0x040036B6 RID: 14006
		private BuildingCategory[] m_Categories;

		// Token: 0x040036B7 RID: 14007
		private BuildingCategory m_SelectedCategory;

		// Token: 0x040036B8 RID: 14008
		private int m_CategoryIndex;

		// Token: 0x040036B9 RID: 14009
		private bool m_ChoosingPiece;

		// Token: 0x040036BA RID: 14010
		private BuildingPiece m_SelectedPiece;

		// Token: 0x040036BB RID: 14011
		private BuildingPiece m_HighlightedPiece;

		// Token: 0x040036BC RID: 14012
		private float m_CategoryScrollPos;

		// Token: 0x040036BD RID: 14013
		private float m_PieceScrollPos;
	}
}
