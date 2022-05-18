using System;
using JSONClass;
using Spine.Unity;
using UnityEngine;

// Token: 0x02000286 RID: 646
public class MapPlayerNormalShow : MonoBehaviour
{
	// Token: 0x060013C7 RID: 5063 RVA: 0x000127B9 File Offset: 0x000109B9
	public void Init(MapPlayerController controller)
	{
		this.controller = controller;
		this.lastx = base.transform.position.x;
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x000B6140 File Offset: 0x000B4340
	private void Update()
	{
		if (this.controller.ShowType == MapPlayerShowType.普通人物 || this.controller.ShowType == MapPlayerShowType.遁术)
		{
			float num = base.transform.position.x - this.lastx;
			if (Mathf.Abs(num) > 0.01f)
			{
				this.PlayerSpine.transform.localScale = ((num > 0f) ? Vector3.one : MapPlayerNormalShow.fanXiang);
				this.lastx = base.transform.position.x;
			}
		}
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x000B61C8 File Offset: 0x000B43C8
	public void Refresh()
	{
		if (this.meshRenderer == null)
		{
			this.meshRenderer = this.PlayerSpine.GetComponent<MeshRenderer>();
		}
		if (this.controller.IsOnSea)
		{
			this.meshRenderer.sortingOrder = 1;
		}
		MapPlayerShowType showType = this.controller.ShowType;
		if (showType == MapPlayerShowType.普通人物)
		{
			this.PlayerObj.SetActive(true);
			this.LoadSpine("MapPlayerWalk");
			return;
		}
		if (showType != MapPlayerShowType.遁术)
		{
			this.PlayerObj.SetActive(false);
			return;
		}
		this.PlayerObj.SetActive(true);
		this.LoadSpine(this.NowDunShuSpineSeid.Spine);
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x000B6264 File Offset: 0x000B4464
	public void LoadSpine(string spine)
	{
		if (this.nowSpineName != spine)
		{
			this.nowSpineName = spine;
			SkeletonDataAsset skeletonDataAsset = Resources.Load<SkeletonDataAsset>(string.Concat(new string[]
			{
				"Spine/MapPlayer/",
				spine,
				"/",
				spine,
				"_SkeletonData"
			}));
			this.PlayerSpine.skeletonDataAsset = skeletonDataAsset;
			string initialSkinName = this.controller.IsNan ? "男" : "女";
			this.PlayerSpine.initialSkinName = initialSkinName;
			this.PlayerSpine.Initialize(true);
		}
	}

	// Token: 0x04000F67 RID: 3943
	public GameObject PlayerObj;

	// Token: 0x04000F68 RID: 3944
	public SkeletonAnimation PlayerSpine;

	// Token: 0x04000F69 RID: 3945
	public Animator Anim;

	// Token: 0x04000F6A RID: 3946
	[HideInInspector]
	public StaticSkillSeidJsonData9 NowDunShuSpineSeid;

	// Token: 0x04000F6B RID: 3947
	private MapPlayerController controller;

	// Token: 0x04000F6C RID: 3948
	private string nowSpineName = "";

	// Token: 0x04000F6D RID: 3949
	private float lastx;

	// Token: 0x04000F6E RID: 3950
	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);

	// Token: 0x04000F6F RID: 3951
	private MeshRenderer meshRenderer;
}
