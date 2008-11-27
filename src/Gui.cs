using System;
using Gtk;
using Mono.Unix;
namespace Gothenburg
{
public class Gui
{
	public Gui ()
	{
		Gtk.Window window = new Gtk.Window ("TreeVieExample");
		window.SetSizeRequest (300, 200);
		
		Gtk.VBox box = new Gtk.VBox (false, 0);
		Gtk.HBox top = new Gtk.HBox (false, 0);
		Gtk.ComboBox selector = new Gtk.ComboBox ();
		
		ScrolledWindow scroll = new ScrolledWindow ();
 
		Gtk.TreeView tree = new Gtk.TreeView ();
		
		tree.HeadersVisible = false;
		tree.Reorderable = true;
		tree.EnableSearch = false;
		
		Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (Gdk.Pixbuf), 
			typeof (string),  typeof (string));
 
		tree.AppendColumn ("A", new Gtk.CellRendererPixbuf (), "pixbuf", 0);  
		tree.AppendColumn ("B", new Gtk.CellRendererText (), "text", 1);
		tree.AppendColumn ("C", new Gtk.CellRendererText (), "text", 2);
 
 		for(int i=1; i<3; i++)
 		{
			musicListStore.AppendValues (new Gdk.Pixbuf ("Icon.png"), "Rupert", "ABC");
			musicListStore.AppendValues (new Gdk.Pixbuf ("Icon.png"), "Rupert", "Yellowananas");
		}
 
		tree.Model = musicListStore;
		
 		Label myLabel = new Label();
     		myLabel.Text = "Hello World!!!!";
     		
     		Label myLabel2 = new Label();
     		myLabel2.Text = "123!!!!";
     		
     		MenuBar menu = new MenuBar ();
     		
     		Menu fileMenu = new Menu();
		MenuItem menuItem = new MenuItem("_File");
      		menuItem.Submenu = fileMenu;
      		menu.Append(menuItem); 
		
		box.PackStart (menu, false, true, 0);
 		top.PackStart (myLabel, true, true, 0);
 		top.PackStart (selector, true, true, 0);
 		box.PackStart (top, false, true, 0);
 		
 		scroll.Add (tree);
 		box.PackStart (scroll, true, true, 0);
		window.Add (box);
		window.ShowAll ();
	}
}
}
