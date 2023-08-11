using System;
using Spine.Unity;
using UnityEngine;
using YSGame;

[ExecuteInEditMode]
public class TestRandomFaceInfo : MonoBehaviour
{
	public SetAvatarFaceRandomInfo setAvatarFaceRandom;

	public PlayerSetRandomFace playerSetRandomFace;

	public SetAvatarFaceRandomInfo.InfoName SkinType;

	public int SkinID;

	public bool updateFace;

	public int AvatarID;

	public bool showAvatar;

	private void Start()
	{
	}

	private void Update()
	{
		if (updateFace)
		{
			updateFace = false;
			SkeletonAnimation component = ((Component)playerSetRandomFace).gameObject.GetComponent<SkeletonAnimation>();
			((SkeletonRenderer)component).initialSkinName = "0";
			((SkeletonRenderer)component).Initialize(true);
			playerSetRandomFace.ChangeSlot(SkinType.ToString(), SkinID, null);
		}
		if (!showAvatar)
		{
			return;
		}
		showAvatar = false;
		SkeletonAnimation component2 = ((Component)playerSetRandomFace).gameObject.GetComponent<SkeletonAnimation>();
		((SkeletonRenderer)component2).initialSkinName = "0";
		((SkeletonRenderer)component2).Initialize(true);
		StaticFaceInfo staticFace = setAvatarFaceRandom.getStaticFace(AvatarID);
		if (staticFace != null)
		{
			foreach (StaticFaceRandomInfo faceinfo in staticFace.faceinfoList)
			{
				playerSetRandomFace.ChangeSlot(faceinfo.SkinTypeName.ToString(), faceinfo.SkinTypeScope, null);
			}
			return;
		}
		foreach (int value in Enum.GetValues(typeof(SetAvatarFaceRandomInfo.InfoName)))
		{
			string name = Enum.GetName(typeof(SetAvatarFaceRandomInfo.InfoName), value);
			int face = setAvatarFaceRandom.getFace(AvatarID, (SetAvatarFaceRandomInfo.InfoName)Enum.Parse(typeof(SetAvatarFaceRandomInfo.InfoName), name));
			if (face != -100)
			{
				playerSetRandomFace.ChangeSlot(AvatarID.ToString(), face, null);
			}
		}
	}
}
