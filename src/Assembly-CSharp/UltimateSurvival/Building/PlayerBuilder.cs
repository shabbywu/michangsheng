using System;
using KBEngine;
using UltimateSurvival.GUISystem;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000665 RID: 1637
	public class PlayerBuilder : MonoBehaviour
	{
		// Token: 0x06003405 RID: 13317 RVA: 0x0016C2B0 File Offset: 0x0016A4B0
		private void Start()
		{
			this.m_Player = GameController.LocalPlayer;
			this.m_BuildingHelpers.Initialize(base.transform, this.m_Player, this.m_AudioSource);
			this.m_Player.PlaceObject.SetTryer(new TryerDelegate(this.Try_Place));
			this.m_Player.PlaceObject.AddListener(new Action(this.m_BuildingHelpers.PlacePiece));
			this.m_Player.RotateObject.SetTryer(new Attempt<float>.GenericTryerDelegate(this.Try_RotateObject));
			this.m_Player.EquippedItem.AddChangeListener(new Action(this.OnChanged_EquippedItem));
			this.m_Player.SelectedBuildable.AddChangeListener(new Action(this.OnChanged_SelectedBuildable));
			this.m_InventoryContainer = MonoSingleton<GUIController>.Instance.GetContainer("Inventory");
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x0016C38C File Offset: 0x0016A58C
		private void OnChanged_EquippedItem()
		{
			SavableItem savableItem = this.m_Player.EquippedItem.Get();
			if (savableItem != null && savableItem.HasProperty("Is Placeable"))
			{
				GameObject gameObject = Object.Instantiate<GameObject>(savableItem.ItemData.WorldObject);
				gameObject.transform.position = Vector3.one * 10000f;
				this.m_Player.SelectedBuildable.Set(gameObject.GetComponent<BuildingPiece>());
			}
			if (savableItem != null && savableItem.HasProperty("Allows Building"))
			{
				this.m_Player.SelectedBuildable.Set(this.m_SelectedPiece);
			}
			this.UpdatePreview();
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x0016C428 File Offset: 0x0016A628
		private void OnChanged_SelectedBuildable()
		{
			if (this.m_Player.EquippedItem.Get() != null && this.m_Player.EquippedItem.Get().HasProperty("Allows Building"))
			{
				this.m_SelectedPiece = this.m_Player.SelectedBuildable.Get();
			}
			this.UpdatePreview();
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x0016C480 File Offset: 0x0016A680
		private void UpdatePreview()
		{
			if (this.m_BuildingHelpers.PreviewExists())
			{
				this.m_BuildingHelpers.ClearPreview();
			}
			BuildingPiece buildingPiece = this.m_Player.SelectedBuildable.Get();
			if (buildingPiece != null && this.m_Player.EquippedItem.Get() != null && (this.m_Player.EquippedItem.Get().HasProperty("Allows Building") || this.m_Player.EquippedItem.Get().HasProperty("Is Placeable")))
			{
				this.m_BuildingHelpers.SpawnPreview(buildingPiece.gameObject);
				this.m_BuildingHelpers.PreviewColor = this.m_BuildingHelpers.CurrentPreviewPiece.Renderers[0].material.color;
				return;
			}
			this.m_BuildingHelpers.ClearPreview();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x0016C554 File Offset: 0x0016A754
		private bool Try_RotateObject(float rotationSign)
		{
			if (this.m_Player.ViewLocked.Is(false) && this.m_BuildingHelpers.PreviewExists())
			{
				this.m_BuildingHelpers.RotationOffset += this.m_RotationSpeed * rotationSign;
				return true;
			}
			return false;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x0016C594 File Offset: 0x0016A794
		private bool Try_Place()
		{
			if (!this.m_BuildingHelpers.PreviewExists() || !this.m_BuildingHelpers.PlacementAllowed)
			{
				return false;
			}
			if (this.m_Player.EquippedItem.Get().HasProperty("Is Placeable"))
			{
				ItemData itemData;
				MonoSingleton<InventoryController>.Instance.Database.FindItemByName(this.m_BuildingHelpers.CurrentPreviewPiece.Name, out itemData);
				ulong itemUUID = MonoSingleton<GUIController>.Instance.GetContainer("Hotbar").gameObject.GetComponent<UltimateSurvival.GUISystem.Hotbar>().SelectedSlot.gameObject.GetComponent<ItemOnObject>().item.itemUUID;
				double num = (double)this.m_BuildingHelpers.CurrentPreview.transform.eulerAngles.x / 360.0 * 6.283185307179586;
				double num2 = (double)this.m_BuildingHelpers.CurrentPreview.transform.eulerAngles.z / 360.0 * 6.283185307179586;
				double num3 = (double)this.m_BuildingHelpers.CurrentPreview.transform.eulerAngles.y / 360.0 * 6.283185307179586;
				if (num - 3.141592653589793 > 0.0)
				{
					num -= 6.283185307179586;
				}
				if (num2 - 3.141592653589793 > 0.0)
				{
					num2 -= 6.283185307179586;
				}
				if (num3 - 3.141592653589793 > 0.0)
				{
					num3 -= 6.283185307179586;
				}
				((Avatar)KBEngineApp.app.player()).createBuild(itemUUID, this.m_BuildingHelpers.CurrentPreview.transform.position, new Vector3((float)num, (float)num2, (float)num3));
				return false;
			}
			if (this.HasRequiredItems())
			{
				this.m_BuildingHelpers.CurrentPreviewPiece.BuildAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, this.m_AudioSource, 1f);
				this.m_PlaceShake.Shake(1f);
				for (int i = 0; i < this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; i++)
				{
					RequiredItem requiredItem = this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[i];
					this.m_InventoryContainer.RemoveItems(requiredItem.Name, requiredItem.Amount);
				}
				return true;
			}
			string text = "You don't have all the required materials: \n";
			for (int j = 0; j < this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; j++)
			{
				RequiredItem requiredItem2 = this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[j];
				text += string.Format("<color=yellow>{0}</color> x {1}, ", requiredItem2.Name, requiredItem2.Amount);
			}
			MonoSingleton<MessageDisplayer>.Instance.PushMessage(text, default(Color), 48);
			return false;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x0016C86C File Offset: 0x0016AA6C
		private bool HasRequiredItems()
		{
			for (int i = 0; i < this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems.Length; i++)
			{
				RequiredItem requiredItem = this.m_BuildingHelpers.CurrentPreviewPiece.RequiredItems[i];
				if (this.m_InventoryContainer.GetItemCount(requiredItem.Name) < requiredItem.Amount)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x0016C8C5 File Offset: 0x0016AAC5
		private void Update()
		{
			this.m_BuildingHelpers.HasSocket = false;
			if (this.m_BuildingHelpers.PreviewExists())
			{
				this.m_BuildingHelpers.LookForSnaps();
			}
			if (this.m_BuildingHelpers.PreviewExists())
			{
				this.m_BuildingHelpers.ManagePreview();
			}
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x0016C903 File Offset: 0x0016AB03
		public bool CanPlace()
		{
			return this.m_BuildingHelpers.PlacementAllowed;
		}

		// Token: 0x04002E48 RID: 11848
		[SerializeField]
		private BuildingHelpers m_BuildingHelpers;

		// Token: 0x04002E49 RID: 11849
		[SerializeField]
		[Tooltip("How fast the player can rotate buildables.")]
		private float m_RotationSpeed = 120f;

		// Token: 0x04002E4A RID: 11850
		[SerializeField]
		private AudioSource m_AudioSource;

		// Token: 0x04002E4B RID: 11851
		[SerializeField]
		[Tooltip("A shake that will be played when the player places an object.")]
		private GenericShake m_PlaceShake;

		// Token: 0x04002E4C RID: 11852
		private float m_NextTimeCanPlayAudio;

		// Token: 0x04002E4D RID: 11853
		private PlayerEventHandler m_Player;

		// Token: 0x04002E4E RID: 11854
		private ItemContainer m_InventoryContainer;

		// Token: 0x04002E4F RID: 11855
		private BuildingPiece m_SelectedPiece;
	}
}
