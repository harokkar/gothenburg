/*****
 * Gothenburg - A project-centric view on your GNOME Desktop       
 *                                                                   
 *        Copyright (C) 2008 Clemens N. Buss
 *                                                                   
 * This program is free software; you can redistribute it and/or     
 * modify it under the terms of the GNU General Public License       
 * as published by the Free Software Foundation; either version 3    
 * of the License, or any later version.                             
 *                                                                   
 *                                                                   
 * This program is distributed in the hope that it will be useful,   
 * but WITHOUT ANY WARRANTY; without even the implied warranty of    
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     
 * GNU General Public License for more details.                      
 *                                                                   
 * You should have received a copy of the GNU General Public License 
 * along with this program; if not, write to the Free Software       
 * Foundation, Inc., 51 Franklin Street, Boston,            
 * MA  02110-1301, USA.                                              
 ****/

using System.Collections;
using Gtk;
using Mono.Unix;

using Gothenburg.AssetProvider;

using Mono.Data.Sqlite;
using NDesk.DBus;

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
	 		Asset asset = (Asset) model.GetValue (iter, 0);
	 		string Name1 = asset.Primary;
	 		string Name2 = asset.Secondary;

			if (filterEntry.Text == "")
				return true;
	 
			if ((Name1.IndexOf (filterEntry.Text) > -1) || (Name2.IndexOf (filterEntry.Text) > -1))
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
		
		static void DeleteEvent (object obj, DeleteEventArgs args)
                {
                        Application.Quit ();
                }
                
                static void OnRowActivate (object obj, Gtk.RowActivatedArgs args)
                {
                	Gtk.TreeView tv = obj as Gtk.TreeView;
                	if (tv == null)
                		return;
                	
			Gtk.TreeModel model = tv.Model;
			Gtk.TreeIter iter;
			
			if (!model.GetIter (out iter, args.Path))
        			return;
     
     			Asset asset = (Asset) model.GetValue (iter, 0);
     			//ITask task = model.GetValue (iter, 0) as ITask;
      			if (asset == null)
       				return;
     
     			BusG.Init ();
			AssetProvider.Tomboy tomboy = new AssetProvider.Tomboy ();  
     			tomboy.init();
			tomboy.open (tomboy.retrieve_by_name("as"));
     			//OpenAsset (asset);
                }

		
		public Gui ()
		{
			Gtk.Window window = new Gtk.Window ("Project Foo");
			window.SetSizeRequest (300, 200);
                        window.DeleteEvent += DeleteEvent;
	
			Gtk.VBox box = new Gtk.VBox (false, 0);
			Gtk.HBox top = new Gtk.HBox (false, 0);
			Gtk.ComboBox selector = new Gtk.ComboBox ();
			ScrolledWindow scroll = new ScrolledWindow ();
	 		filterEntry = new Gtk.Entry ();
			filterEntry.Changed += OnFilterEntryTextChanged;
			
			Gtk.TreeView tree = new Gtk.TreeView ();
			tree.HeadersVisible = false;
			tree.Reorderable = true;
			tree.EnableSearch = false;
			tree.RowActivated += OnRowActivate;

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

	 		ArrayList assets = new ArrayList ();
	 		for(int i=1; i<3; i++)
	 		{
	 			assets.Add (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm"), "LinkFoo" ,"Meet Tom", "19.03.2008"));
	 			assets.Add (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm"), "LinkFoo" ,"Shop Groceries", "23.03.2008"));
			}
			
	  		Gtk.ListStore AssetListStore = new Gtk.ListStore (typeof (Asset));
			foreach (Asset asset in assets)
			{
				AssetListStore.AppendValues (asset);				
			}
			
			filter = new Gtk.TreeModelFilter (AssetListStore, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
			tree.Model = filter;
			
			//tree.Model = AssetListStore;
			
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
//Gtk.ListStore AssetListStore = new Gtk.ListStore (typeof (Gdk.Pixbuf), typeof (string),  typeof (string));
//tree.AppendColumn ("Icon", new Gtk.CellRendererPixbuf (), "pixbuf", 0);  
//tree.AppendColumn ("Primary Info", new Gtk.CellRendererText (), "text", 1);
//tree.AppendColumn ("Secondary Info", new Gtk.CellRendererText (), "text", 2);
