namespace Facebook;

public class AndroidFacebookLoader : FB.CompiledFacebookLoader
{
	protected override IFacebook fb => (IFacebook)(object)FBComponentFactory.GetComponent<AndroidFacebook>((IfNotExist)0);
}
