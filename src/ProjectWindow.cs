using System.Collections;
using Gtk;
using Mono.Unix;
using System;

namespace Gothenburg
{
  public class ProjectWindow : Gtk.Window
  {
    public ProjectWindow (string name) : base(name)
    {
      this.SetSizeRequest (100, 100);
      this.Icon = new Gdk.Pixbuf ("lipsticktower.jpg");  //Kalle, Andreas :: Call for Icon!
      Gtk.Entry tagEntry = new Gtk.Entry (); 
      Gtk.VBox tagbox = new Gtk.VBox (false, 0); 
      tagbox.PackStart (tagEntry, true, true, 0); 
      this.Add (tagbox);
      this.ShowAll (); 
    }
  }
}
