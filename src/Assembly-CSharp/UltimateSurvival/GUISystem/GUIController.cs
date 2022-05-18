using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000929 RID: 2345
	public class GUIController : MonoSingleton<GUIController>
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x0002B2E9 File Offset: 0x000294E9
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x0002B2F1 File Offset: 0x000294F1
		public PlayerEventHandler Player { get; private set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x0002B2FA File Offset: 0x000294FA
		public Canvas Canvas
		{
			get
			{
				return this.m_Canvas;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x0002B302 File Offset: 0x00029502
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x0002B30A File Offset: 0x0002950A
		public ItemContainer[] Containers { get; private set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x0002B313 File Offset: 0x00029513
		public Font Font
		{
			get
			{
				return this.m_Font;
			}
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x001AEED8 File Offset: 0x001AD0D8
		public ItemContainer GetContainer(string name)
		{
			for (int i = 0; i < this.Containers.Length; i++)
			{
				if (this.Containers[i].Name == name)
				{
					return this.Containers[i];
				}
			}
			Debug.LogWarning("No container with the name " + name + " found!");
			return null;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x001AEF2C File Offset: 0x001AD12C
		public bool MouseOverSelectionKeeper()
		{
			for (int i = 0; i < this.m_SelectionBlockers.Count; i++)
			{
				if (this.m_SelectionBlockers[i].gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(this.m_SelectionBlockers[i], Input.mousePosition, this.m_GUICamera))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x001AEF90 File Offset: 0x001AD190
		public void ApplyForAllCollections()
		{
			ItemContainer[] componentsInChildren = base.GetComponentsInChildren<ItemContainer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ApplyAll();
			}
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x0002B31B File Offset: 0x0002951B
		private void Awake()
		{
			this.Containers = base.GetComponentsInChildren<ItemContainer>(true);
			this.Player = GameController.LocalPlayer;
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x0002B335 File Offset: 0x00029535
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x0002B352 File Offset: 0x00029552
		private void OnChanged_InventoryState()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				GameController.Audio.Play2D(this.m_InventoryOpenClip, 0.6f);
				return;
			}
			GameController.Audio.Play2D(this.m_InventoryCloseClip, 0.6f);
		}

		// Token: 0x04003648 RID: 13896
		[Header("Setup")]
		[SerializeField]
		private Canvas m_Canvas;

		// Token: 0x04003649 RID: 13897
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x0400364A RID: 13898
		[SerializeField]
		private Font m_Font;

		// Token: 0x0400364B RID: 13899
		[SerializeField]
		[Reorderable]
		[Tooltip("If the player clicks while on those rects, the current selection will not be lost.")]
		private ReorderableRectTransformList m_SelectionBlockers;

		// Token: 0x0400364C RID: 13900
		[Header("Audio")]
		[SerializeField]
		private AudioClip m_InventoryOpenClip;

		// Token: 0x0400364D RID: 13901
		[SerializeField]
		private AudioClip m_InventoryCloseClip;
	}
}
