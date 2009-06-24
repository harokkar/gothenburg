using System.Collections;
using Gtk;
using Mono.Unix;
using System;

namespace Gothenburg
{
  public class ProjectWindow : Gtk.Window
  {
    
    Gtk.Entry tagEntry;

    public ProjectWindow (string name) : base (name)
    {
      this.SetSizeRequest (100, 100);
      this.Icon = new Gdk.Pixbuf ("lipsticktower.jpg");  //Kalle, Andreas :: Call for Icon!

      Gtk.Button button = new Gtk.Button ();
      button.Label = "Add";
      button.Clicked += OnButtonClicked;
      tagEntry = new Gtk.Entry (); 
      Gtk.VBox tagbox = new Gtk.VBox (false, 0); 
      tagbox.PackStart (tagEntry, true, true, 0); 
      tagbox.PackStart (button, true, true, 0);
      this.Add (tagbox);
      this.ShowAll (); 
    }

    void OnButtonClicked (object obj, EventArgs args)
    {
      Console.WriteLine(tagEntry.Text);
      this.Destroy ();
    }
  }
}
