using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000640 RID: 1600
	public class BuildingWheel : GUIBehaviour
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x00167814 File Offset: 0x00165A14
		public RectTransform SelectionHighlight
		{
			get
			{
				return this.m_SelectionHighlight;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x0016781C File Offset: 0x00165A1C
		public float Distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x00167824 File Offset: 0x00165A24
		public float Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x0016782C File Offset: 0x00165A2C
		public float Spacing
		{
			get
			{
				return this.m_Spacing;
			}
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x00167834 File Offset: 0x00165A34
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

		// Token: 0x06003319 RID: 13081 RVA: 0x00167BC4 File Offset: 0x00165DC4
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

		// Token: 0x0600331A RID: 13082 RVA: 0x00167C5B File Offset: 0x00165E5B
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

		// Token: 0x0600331B RID: 13083 RVA: 0x00167C90 File Offset: 0x00165E90
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

		// Token: 0x0600331C RID: 13084 RVA: 0x00167CFB File Offset: 0x00165EFB
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

		// Token: 0x04002D42 RID: 11586
		[SerializeField]
		private Window m_Window;

		// Token: 0x04002D43 RID: 11587
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x04002D44 RID: 11588
		[SerializeField]
		private RectTransform m_SelectionHighlight;

		// Token: 0x04002D45 RID: 11589
		[SerializeField]
		private Text m_CategoryName;

		// Token: 0x04002D46 RID: 11590
		[SerializeField]
		private Text m_PieceName;

		// Token: 0x04002D47 RID: 11591
		[SerializeField]
		[Range(0f, 50f)]
		private float m_ScrollThreeshold = 1f;

		// Token: 0x04002D48 RID: 11592
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_RefreshAudio;

		// Token: 0x04002D49 RID: 11593
		[SerializeField]
		private SoundPlayer m_SelectPieceAudio;

		// Token: 0x04002D4A RID: 11594
		[Header("Layout")]
		[SerializeField]
		private float m_Distance = 211.7f;

		// Token: 0x04002D4B RID: 11595
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Offset;

		// Token: 0x04002D4C RID: 11596
		[SerializeField]
		[Range(-90f, 90f)]
		private float m_Spacing;

		// Token: 0x04002D4D RID: 11597
		private BuildingCategory[] m_Categories;

		// Token: 0x04002D4E RID: 11598
		private BuildingCategory m_SelectedCategory;

		// Token: 0x04002D4F RID: 11599
		private int m_CategoryIndex;

		// Token: 0x04002D50 RID: 11600
		private bool m_ChoosingPiece;

		// Token: 0x04002D51 RID: 11601
		private BuildingPiece m_SelectedPiece;

		// Token: 0x04002D52 RID: 11602
		private BuildingPiece m_HighlightedPiece;

		// Token: 0x04002D53 RID: 11603
		private float m_CategoryScrollPos;

		// Token: 0x04002D54 RID: 11604
		private float m_PieceScrollPos;
	}
}
