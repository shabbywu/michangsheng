using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using YSGame;

// Token: 0x020003D2 RID: 978
[ExecuteInEditMode]
public class TestRandomFaceInfo : MonoBehaviour
{
	// Token: 0x06001FC9 RID: 8137 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x000DFEC4 File Offset: 0x000DE0C4
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

	// Token: 0x040019D9 RID: 6617
	public SetAvatarFaceRandomInfo setAvatarFaceRandom;

	// Token: 0x040019DA RID: 6618
	public PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x040019DB RID: 6619
	public SetAvatarFaceRandomInfo.InfoName SkinType;

	// Token: 0x040019DC RID: 6620
	public int SkinID;

	// Token: 0x040019DD RID: 6621
	public bool updateFace;

	// Token: 0x040019DE RID: 6622
	public int AvatarID;

	// Token: 0x040019DF RID: 6623
	public bool showAvatar;
}
