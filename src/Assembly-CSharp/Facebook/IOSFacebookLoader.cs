namespace Facebook;

public class IOSFacebookLoader : FB.CompiledFacebookLoader
{
	protected override IFacebook fb => (IFacebook)(object)FBComponentFactory.GetComponent<IOSFacebook>((IfNotExist)0);
}
