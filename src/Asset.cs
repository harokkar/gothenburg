using System;
using Gtk;
using Mono.Unix;

namespace Gothenburg
{
	public class Asset
	{
		public string Type;
	 	public Gdk.Pixbuf Icon;
	 	public string Link;
	 	public string Primary;
	 	public string Secondary;
	 	
		public Asset (string providertype, Gdk.Pixbuf providericon, string resourcelink, string primary, string secondary)
		//public Asset (string providertype, string resourcelink, string primary, string secondary)
		{
			this.Type = providertype;
			this.Icon = providericon;
			this.Link = resourcelink;
			this.Primary = primary;
			this.Secondary = secondary;
		}
	}
}
