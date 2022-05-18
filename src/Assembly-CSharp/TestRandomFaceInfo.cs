using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using YSGame;

// Token: 0x02000569 RID: 1385
[ExecuteInEditMode]
public class TestRandomFaceInfo : MonoBehaviour
{
	// Token: 0x06002342 RID: 9026 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x001229A8 File Offset: 0x00120BA8
	private void Update()
	{
		if (this.updateFace)
		{
			this.updateFace = false;
			SkeletonAnimation component = this.playerSetRandomFace.gameObject.GetComponent<SkeletonAnimation>();
			component.initialSkinName = "0";
			component.Initialize(true);
			this.playerSetRandomFace.ChangeSlot(this.SkinType.ToString(), this.SkinID, null);
		}
		if (this.showAvatar)
		{
			this.showAvatar = false;
			SkeletonAnimation component2 = this.playerSetRandomFace.gameObject.GetComponent<SkeletonAnimation>();
			component2.initialSkinName = "0";
			component2.Initialize(true);
			StaticFaceInfo staticFace = this.setAvatarFaceRandom.getStaticFace(this.AvatarID);
			if (staticFace != null)
			{
				using (List<StaticFaceRandomInfo>.Enumerator enumerator = staticFace.faceinfoList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StaticFaceRandomInfo staticFaceRandomInfo = enumerator.Current;
						this.playerSetRandomFace.ChangeSlot(staticFaceRandomInfo.SkinTypeName.ToString(), staticFaceRandomInfo.SkinTypeScope, null);
					}
					return;
				}
			}
			foreach (object obj in Enum.GetValues(typeof(SetAvatarFaceRandomInfo.InfoName)))
			{
				int num = (int)obj;
				string name = Enum.GetName(typeof(SetAvatarFaceRandomInfo.InfoName), num);
				int face = this.setAvatarFaceRandom.getFace(this.AvatarID, (SetAvatarFaceRandomInfo.InfoName)Enum.Parse(typeof(SetAvatarFaceRandomInfo.InfoName), name));
				if (face != -100)
				{
					this.playerSetRandomFace.ChangeSlot(this.AvatarID.ToString(), face, null);
				}
			}
		}
	}

	// Token: 0x04001E63 RID: 7779
	public SetAvatarFaceRandomInfo setAvatarFaceRandom;

	// Token: 0x04001E64 RID: 7780
	public PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x04001E65 RID: 7781
	public SetAvatarFaceRandomInfo.InfoName SkinType;

	// Token: 0x04001E66 RID: 7782
	public int SkinID;

	// Token: 0x04001E67 RID: 7783
	public bool updateFace;

	// Token: 0x04001E68 RID: 7784
	public int AvatarID;

	// Token: 0x04001E69 RID: 7785
	public bool showAvatar;
}
