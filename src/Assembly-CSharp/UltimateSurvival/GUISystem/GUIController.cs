using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000637 RID: 1591
	public class GUIController : MonoSingleton<GUIController>
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x001659DE File Offset: 0x00163BDE
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x001659E6 File Offset: 0x00163BE6
		public PlayerEventHandler Player { get; private set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x001659EF File Offset: 0x00163BEF
		public Canvas Canvas
		{
			get
			{
				return this.m_Canvas;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x001659F7 File Offset: 0x00163BF7
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x001659FF File Offset: 0x00163BFF
		public ItemContainer[] Containers { get; private set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00165A08 File Offset: 0x00163C08
		public Font Font
		{
			get
			{
				return this.m_Font;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00165A10 File Offset: 0x00163C10
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

		// Token: 0x06003285 RID: 12933 RVA: 0x00165A64 File Offset: 0x00163C64
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

		// Token: 0x06003286 RID: 12934 RVA: 0x00165AC8 File Offset: 0x00163CC8
		public void ApplyForAllCollections()
		{
			ItemContainer[] componentsInChildren = base.GetComponentsInChildren<ItemContainer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ApplyAll();
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x00165AF3 File Offset: 0x00163CF3
		private void Awake()
		{
			this.Containers = base.GetComponentsInChildren<ItemContainer>(true);
			this.Player = GameController.LocalPlayer;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x00165B0D File Offset: 0x00163D0D
		private void Start()
		{
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x00165B2A File Offset: 0x00163D2A
		private void OnChanged_InventoryState()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				GameController.Audio.Play2D(this.m_InventoryOpenClip, 0.6f);
				return;
			}
			GameController.Audio.Play2D(this.m_InventoryCloseClip, 0.6f);
		}

		// Token: 0x04002CF7 RID: 11511
		[Header("Setup")]
		[SerializeField]
		private Canvas m_Canvas;

		// Token: 0x04002CF8 RID: 11512
		[SerializeField]
		private Camera m_GUICamera;

		// Token: 0x04002CF9 RID: 11513
		[SerializeField]
		private Font m_Font;

		// Token: 0x04002CFA RID: 11514
		[SerializeField]
		[Reorderable]
		[Tooltip("If the player clicks while on those rects, the current selection will not be lost.")]
		private ReorderableRectTransformList m_SelectionBlockers;

		// Token: 0x04002CFB RID: 11515
		[Header("Audio")]
		[SerializeField]
		private AudioClip m_InventoryOpenClip;

		// Token: 0x04002CFC RID: 11516
		[SerializeField]
		private AudioClip m_InventoryCloseClip;
	}
}
