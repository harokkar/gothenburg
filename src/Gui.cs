//using System;
using System.Collections;

using Gtk;
using Mono.Unix;


namespace Gothenburg
{
	public class Gui
	{
		Gtk.Entry filterEntry;
		Gtk.TreeModelFilter filter;
	
		private void OnFilterEntryTextChanged (object Obj, System.EventArgs args)
		{
			filter.Refilter ();
		}
	
		private bool FilterTree (Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			//string Name = model.GetValue (iter, 1).ToString ();
	 		
	 		Asset asset = (Asset) model.GetValue (iter, 0);
	 		string Name = asset.Primary;
	 		
	 		
			if (filterEntry.Text == "")
				return true;
	 
			if (Name.IndexOf (filterEntry.Text) > -1)
				return true;
			else
				return false;
		}
		
		private void RenderIcon (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			Asset asset = (Asset) model.GetValue (iter, 0);
			(cell as Gtk.CellRendererPixbuf).Pixbuf = asset.Icon;
		}
		
		private void RenderPrimary (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			Asset asset = (Asset) model.GetValue (iter, 0);
			(cell as Gtk.CellRendererText).Text = asset.Primary;
		}
		
		private void RenderSecondary (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			Asset asset = (Asset) model.GetValue (iter, 0);
			(cell as Gtk.CellRendererText).Text = asset.Secondary;
		}
		
		ArrayList assets;

		public Gui ()
		{
			Gtk.Window window = new Gtk.Window ("Project Foo");
			window.SetSizeRequest (300, 200);
		
			Gtk.VBox box = new Gtk.VBox (false, 0);
			Gtk.HBox top = new Gtk.HBox (false, 0);
			Gtk.ComboBox selector = new Gtk.ComboBox ();
		
			ScrolledWindow scroll = new ScrolledWindow ();
	 
			Gtk.TreeView tree = new Gtk.TreeView ();
		
			filterEntry = new Gtk.Entry ();
			filterEntry.Changed += OnFilterEntryTextChanged;
		
			tree.HeadersVisible = false;
			tree.Reorderable = true;
			tree.EnableSearch = false;

	 		Gtk.TreeViewColumn iconCol = new Gtk.TreeViewColumn ( );
			iconCol.Title = "Icon";
			Gtk.CellRendererPixbuf iconCell = new Gtk.CellRendererPixbuf ();
			iconCol.PackStart (iconCell, true);
			iconCol.SetCellDataFunc (iconCell, new Gtk.TreeCellDataFunc (RenderIcon));

			Gtk.TreeViewColumn primCol = new Gtk.TreeViewColumn ( );
			primCol.Title = "Primary Info";
			Gtk.CellRendererText primInfoCell = new Gtk.CellRendererText ();
			primCol.PackStart (primInfoCell, true);
			primCol.SetCellDataFunc (primInfoCell, new Gtk.TreeCellDataFunc (RenderPrimary));

			Gtk.TreeViewColumn secCol = new Gtk.TreeViewColumn ( );
			secCol.Title = "Secondary Info";
			Gtk.CellRendererText secInfoCell = new Gtk.CellRendererText ();
			secCol.PackStart (secInfoCell, true);
			secCol.SetCellDataFunc (secInfoCell, new Gtk.TreeCellDataFunc (RenderSecondary));

	 		assets = new ArrayList ();
	 		for(int i=1; i<3; i++)
	 		{
	 			assets.Add (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.png"), "LinkFoo" ,"Meet Tom", "19.03.2008"));
			}
			
	  		Gtk.ListStore ProjectListStore = new Gtk.ListStore (typeof (Asset));
			foreach (Asset asset in assets)
			{
				ProjectListStore.AppendValues (asset);				
			}
			
			filter = new Gtk.TreeModelFilter (ProjectListStore, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
			tree.Model = filter;
			
			//tree.Model = ProjectListStore;
			
			tree.AppendColumn (iconCol);
			tree.AppendColumn (primCol);
			tree.AppendColumn (secCol);

	     		MenuBar menu = new MenuBar ();
	     		Menu fileMenu = new Menu();
			MenuItem menuItem = new MenuItem("_File");
	      		menuItem.Submenu = fileMenu;
	      		menu.Append(menuItem); 
		
			box.PackStart (menu, false, true, 0);
	 		top.PackStart (filterEntry, true, true, 0);
	 		top.PackStart (selector, true, true, 0);
	 		box.PackStart (top, false, true, 0);
	 		
	 		scroll.Add (tree);
	 		box.PackStart (scroll, true, true, 0);
			window.Add (box);
			window.ShowAll ();
		}
	}
}


//old:
//Gtk.ListStore ProjectListStore = new Gtk.ListStore (typeof (Gdk.Pixbuf), typeof (string),  typeof (string));
//tree.AppendColumn ("Icon", new Gtk.CellRendererPixbuf (), "pixbuf", 0);  
//tree.AppendColumn ("Primary Info", new Gtk.CellRendererText (), "text", 1);
//tree.AppendColumn ("Secondary Info", new Gtk.CellRendererText (), "text", 2);
