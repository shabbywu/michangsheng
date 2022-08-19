using System;
using System.Collections.Generic;
using UnityEngine;

namespace script.SetFace
{
	// Token: 0x020009EE RID: 2542
	public class MainUISetFaceB : MonoBehaviour
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x001DD184 File Offset: 0x001DB384
		private void SelectBigType(faceInfoList face)
		{
			Tools.ClearObj(this.setType.transform);
			int num = 0;
			using (List<faceInfoDataBaseList>.Enumerator enumerator = face.faceList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MainUISetFaceB.<>c__DisplayClass6_0 CS$<>8__locals1 = new MainUISetFaceB.<>c__DisplayClass6_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.data = enumerator.Current;
					if (CS$<>8__locals1.data != null)
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.setType, this.setTypeList);
						MainUIToggle toggle = gameObject.GetComponent<MainUIToggle>();
						toggle.Text = CS$<>8__locals1.data.Name;
						toggle.valueChange.AddListener(delegate()
						{
							if (toggle.isOn)
							{
								CS$<>8__locals1.<>4__this.SelectSmallType(CS$<>8__locals1.data);
							}
						});
						gameObject.SetActive(true);
						if (num == 0)
						{
							toggle.MoNiClick();
						}
						num++;
					}
				}
			}
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x001DD288 File Offset: 0x001DB488
		private void SelectSmallType(faceInfoDataBaseList face)
		{
			Tools.ClearObj(this.faceImage.transform);
			int num = 0;
			int i = jsonData.instance.AvatarRandomJsonData["1"][face.JsonInfoName].I;
			string str = jsonData.instance.SuiJiTouXiangGeShuJsonData[face.JsonInfoName]["colorset"].str;
			if (str == "")
			{
				this.SetColor.HideColorSelect();
			}
			else
			{
				this.SetColor.HasColorSelect(str, face);
			}
			foreach (faceInfoDataBase faceInfoDataBase in face.faceList)
			{
				if (faceInfoDataBase != null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.faceImage, this.faceImageList);
					createImageCell imageCell = gameObject.GetComponent<createImageCell>();
					imageCell.SkinIndex = num;
					imageCell.SkinType = face.JsonInfoName;
					imageCell.SetImage(faceInfoDataBase.Image1, faceInfoDataBase.Image2, faceInfoDataBase.Image3);
					if (i == num)
					{
						imageCell.toggle.isOn = true;
					}
					imageCell.toggle.onValueChanged.AddListener(delegate(bool b)
					{
						if (b)
						{
							this.setFace(imageCell.SkinType, imageCell.SkinIndex);
						}
					});
					gameObject.SetActive(true);
					num++;
				}
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001DD41C File Offset: 0x001DB61C
		public void setFace(string skinType, int skinIndex)
		{
			List<int> suijiList = jsonData.instance.getSuijiList(skinType, "AvatarSex" + AvatarFaceDatabase.inst.ListType);
			jsonData.instance.AvatarRandomJsonData["1"].SetField(skinType, suijiList[skinIndex]);
			SetFaceUI.Inst.Face.randomAvatar(1);
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x001DD480 File Offset: 0x001DB680
		public void Init()
		{
			int num = 0;
			using (List<faceInfoList>.Enumerator enumerator = AvatarFaceDatabase.inst.AllList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MainUISetFaceB.<>c__DisplayClass9_0 CS$<>8__locals1 = new MainUISetFaceB.<>c__DisplayClass9_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.temp = enumerator.Current;
					MainUIToggle toggle = this.bigTypeList[num];
					toggle.Text = CS$<>8__locals1.temp.Name;
					toggle.valueChange.RemoveAllListeners();
					if (CS$<>8__locals1.temp.Name == "脸部" && SetFaceUI.Inst.Lock.activeSelf)
					{
						toggle.Text = "<color=#a6a697>" + CS$<>8__locals1.temp.Name + "</color>";
						toggle.isDisable = true;
					}
					else
					{
						toggle.isDisable = false;
						toggle.valueChange.AddListener(delegate()
						{
							if (toggle.isOn)
							{
								CS$<>8__locals1.<>4__this.SelectBigType(CS$<>8__locals1.temp);
							}
						});
					}
					num++;
					if (num > 2)
					{
						break;
					}
				}
			}
			if (SetFaceUI.Inst.Lock.activeSelf)
			{
				this.bigTypeList[1].MoNiClick();
				return;
			}
			this.bigTypeList[0].MoNiClick();
		}

		// Token: 0x040047EC RID: 18412
		[SerializeField]
		private Transform setTypeList;

		// Token: 0x040047ED RID: 18413
		[SerializeField]
		private GameObject setType;

		// Token: 0x040047EE RID: 18414
		[SerializeField]
		private GameObject faceImage;

		// Token: 0x040047EF RID: 18415
		[SerializeField]
		private Transform faceImageList;

		// Token: 0x040047F0 RID: 18416
		public List<MainUIToggle> bigTypeList;

		// Token: 0x040047F1 RID: 18417
		public SetColor SetColor;
	}
}
