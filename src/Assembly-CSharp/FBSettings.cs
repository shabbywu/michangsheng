using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class FBSettings : ScriptableObject
{
	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0005299A File Offset: 0x00050B9A
	private static FBSettings Instance
	{
		get
		{
			if (FBSettings.instance == null)
			{
				FBSettings.instance = (Resources.Load("FacebookSettings") as FBSettings);
				if (FBSettings.instance == null)
				{
					FBSettings.instance = ScriptableObject.CreateInstance<FBSettings>();
				}
			}
			return FBSettings.instance;
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x000529D9 File Offset: 0x00050BD9
	public void SetAppIndex(int index)
	{
		if (this.selectedAppIndex != index)
		{
			this.selectedAppIndex = index;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000DEC RID: 3564 RVA: 0x000529F0 File Offset: 0x00050BF0
	public int SelectedAppIndex
	{
		get
		{
			return this.selectedAppIndex;
		}
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x000529F8 File Offset: 0x00050BF8
	public void SetAppId(int index, string value)
	{
		if (this.appIds[index] != value)
		{
			this.appIds[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00052A18 File Offset: 0x00050C18
	// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00052A20 File Offset: 0x00050C20
	public string[] AppIds
	{
		get
		{
			return this.appIds;
		}
		set
		{
			if (this.appIds != value)
			{
				this.appIds = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00052A37 File Offset: 0x00050C37
	public void SetAppLabel(int index, string value)
	{
		if (this.appLabels[index] != value)
		{
			this.AppLabels[index] = value;
			FBSettings.DirtyEditor();
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00052A57 File Offset: 0x00050C57
	// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00052A5F File Offset: 0x00050C5F
	public string[] AppLabels
	{
		get
		{
			return this.appLabels;
		}
		set
		{
			if (this.appLabels != value)
			{
				this.appLabels = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00052A76 File Offset: 0x00050C76
	public static string AppId
	{
		get
		{
			return FBSettings.Instance.AppIds[FBSettings.Instance.SelectedAppIndex];
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00052A8D File Offset: 0x00050C8D
	public static bool IsValidAppId
	{
		get
		{
			return FBSettings.AppId != null && FBSettings.AppId.Length > 0 && !FBSettings.AppId.Equals("0");
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00052AB7 File Offset: 0x00050CB7
	// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00052AC3 File Offset: 0x00050CC3
	public static bool Cookie
	{
		get
		{
			return FBSettings.Instance.cookie;
		}
		set
		{
			if (FBSettings.Instance.cookie != value)
			{
				FBSettings.Instance.cookie = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00052AE2 File Offset: 0x00050CE2
	// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00052AEE File Offset: 0x00050CEE
	public static bool Logging
	{
		get
		{
			return FBSettings.Instance.logging;
		}
		set
		{
			if (FBSettings.Instance.logging != value)
			{
				FBSettings.Instance.logging = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00052B0D File Offset: 0x00050D0D
	// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00052B19 File Offset: 0x00050D19
	public static bool Status
	{
		get
		{
			return FBSettings.Instance.status;
		}
		set
		{
			if (FBSettings.Instance.status != value)
			{
				FBSettings.Instance.status = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00052B38 File Offset: 0x00050D38
	// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00052B44 File Offset: 0x00050D44
	public static bool Xfbml
	{
		get
		{
			return FBSettings.Instance.xfbml;
		}
		set
		{
			if (FBSettings.Instance.xfbml != value)
			{
				FBSettings.Instance.xfbml = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00052B63 File Offset: 0x00050D63
	// (set) Token: 0x06000DFE RID: 3582 RVA: 0x00052B6F File Offset: 0x00050D6F
	public static string IosURLSuffix
	{
		get
		{
			return FBSettings.Instance.iosURLSuffix;
		}
		set
		{
			if (FBSettings.Instance.iosURLSuffix != value)
			{
				FBSettings.Instance.iosURLSuffix = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00052B93 File Offset: 0x00050D93
	public static string ChannelUrl
	{
		get
		{
			return "/channel.html";
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00052B9A File Offset: 0x00050D9A
	// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00052BA6 File Offset: 0x00050DA6
	public static bool FrictionlessRequests
	{
		get
		{
			return FBSettings.Instance.frictionlessRequests;
		}
		set
		{
			if (FBSettings.Instance.frictionlessRequests != value)
			{
				FBSettings.Instance.frictionlessRequests = value;
				FBSettings.DirtyEditor();
			}
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00004095 File Offset: 0x00002295
	private static void DirtyEditor()
	{
	}

	// Token: 0x040009DC RID: 2524
	private const string facebookSettingsAssetName = "FacebookSettings";

	// Token: 0x040009DD RID: 2525
	private const string facebookSettingsPath = "Facebook/Resources";

	// Token: 0x040009DE RID: 2526
	private const string facebookSettingsAssetExtension = ".asset";

	// Token: 0x040009DF RID: 2527
	private static FBSettings instance;

	// Token: 0x040009E0 RID: 2528
	[SerializeField]
	private int selectedAppIndex;

	// Token: 0x040009E1 RID: 2529
	[SerializeField]
	private string[] appIds = new string[]
	{
		"0"
	};

	// Token: 0x040009E2 RID: 2530
	[SerializeField]
	private string[] appLabels = new string[]
	{
		"App Name"
	};

	// Token: 0x040009E3 RID: 2531
	[SerializeField]
	private bool cookie = true;

	// Token: 0x040009E4 RID: 2532
	[SerializeField]
	private bool logging = true;

	// Token: 0x040009E5 RID: 2533
	[SerializeField]
	private bool status = true;

	// Token: 0x040009E6 RID: 2534
	[SerializeField]
	private bool xfbml;

	// Token: 0x040009E7 RID: 2535
	[SerializeField]
	private bool frictionlessRequests = true;

	// Token: 0x040009E8 RID: 2536
	[SerializeField]
	private string iosURLSuffix = "";
}
