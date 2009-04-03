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
using System;
using Gothenburg.AssetProvider;
using Mono.Data.Sqlite;
using NDesk.DBus;

namespace Gothenburg
{
  public class Gui
  {
    Gtk.TreeView tree = new Gtk.TreeView ();
    Gtk.Entry filterEntry;
    Gtk.TreeModelFilter filter;
    //Array assets;
    ArrayList projects;
    DataLayer dlayer = new DataLayer ();
    int projID = -1;
    //Store AssetStore;
    
    
    Gtk.Window window = new Gtk.Window ("Gothenburg");
    Gtk.Window window_addtag = new Gtk.Window ("Gothenburg");
  
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
      if (asset == null)
        return;

      asset.Open();
    }

    void OnSelectorChanged (object obj, EventArgs args)
    {
      string title;
      ComboBox combo = obj as ComboBox;
      
      if (obj == null)
        return;
      TreeIter iter;
      
      if (combo.GetActiveIter (out iter))
      {
        title = (string) combo.Model.GetValue (iter, 0);
        window.Title = title + " - Gothenburg";

        projID = combo.Active;
        //Console.WriteLine (projID);
        //UpdateModel (); 

        /*filter = new Gtk.TreeModelFilter (dlayer.GetAssets (projID), null);
        filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
        tree.Model = filter;*/

        tree.Model = dlayer.GetAssets (projID);
        //AssetStore = (Store) dlayer.GetAssets (projID);
        //tree.Model = AssetStore;
        
        /*foreach (Asset asset in AssetStore)
        Console.WriteLine ("a"+(string)asset.GetPrimary ());*/

              /*if(title == "AFST")
                projID = 0;
              if(title == "Friends Of GNOME Website")
                projID = 1;              
              if(title == "Free Desktop Summit Gran Canaria")
                projID = 2;*/
                
              //assets = dlayer.GetAssets(projID);
              //Array proj = dlayer.projects;

              //Gtk.Store AssetStore = dlayer.assets;
              
              //Gtk.Store AssetStore = new Gtk.Store (typeof (Asset));
        //foreach (Array projs in proj)
        //{
        //  foreach (Asset asset in projs)
        /*foreach (Asset asset in assets)
        {
          //Console.WriteLine(asset.Primary);
          AssetStore.AppendValues (asset);
        }*/
        //}
        //tree.Model = AssetStore;
      }
    }
    
    /*void UpdateModel()
    {
      AssetStore = dlayer.GetAssets (projID);
        
      //Store AssetStore = dlayer.GetAssets (ProjID);
      
//      filter = new Gtk.TreeModelFilter (AssetStore, null);
//      filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
//      tree.Model = filter;
//      tree.Model = AssetStore;
    }*/
    
    void OnAddClicked (object obj, EventArgs args)
    {
      dlayer.GetAllNotes (projID);
      
      /*filter = new Gtk.TreeModelFilter (dlayer.GetAssets (projID), null);
      filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
      tree.Model = filter;*/
      
      tree.Model = dlayer.GetAssets (projID);
      
      //UpdateModel (projID);
      
      /*Gtk.Store AssetStore = new Gtk.Store (typeof (Asset));
      foreach (Asset asset in assets)
      {
        AssetStore.AppendValues (asset);
      }
    
      filter = new Gtk.TreeModelFilter (AssetStore, null);
      filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
      tree.Model = filter;  //tree.Model = AssetStore;  */
    }
    
    void OnMinusClicked (object obj, EventArgs args)
    {
      TreeSelection sel = tree.Selection;

                  /*TreeIter iter;
                  TreeModel model;
                  if (sel.GetSelected (out model, out iter))
                  {
                    Asset val = (Asset) model.GetValue (iter, 0);
                    Console.WriteLine ((string) val.Link );
            }
            */
           
      //This gives the index of the selected item in the TreeView
      if(sel.CountSelectedRows () != 0)
      {
        TreePath[] path; // = new TreePath ();
        path = sel.GetSelectedRows ();
        dlayer.DelAsset(path[0], projID);
        //Console.WriteLine (path[0].Indices[0]);
      }
    }


    public Gui ()
    {
      dlayer = new DataLayer ();
      
      window_addtag.SetSizeRequest (100, 100);
      window.Icon = new Gdk.Pixbuf ("lipsticktower.jpg");  //Kalle, Andreas :: Call for Icon!
      Gtk.Entry tagEntry = new Gtk.Entry ();
      Gtk.VBox tagbox = new Gtk.VBox (false, 0);
      tagbox.PackStart (tagEntry, true, true, 0);
      window_addtag.Add (tagbox);
      window_addtag.ShowAll ();


      window.SetSizeRequest (300, 500);
      window.DeleteEvent += DeleteEvent;
      window.Icon = new Gdk.Pixbuf ("lipsticktower.jpg");  //Kalle, Andreas :: Call for Icon!

      Gtk.VBox box = new Gtk.VBox (false, 0);
      Gtk.HBox top = new Gtk.HBox (false, 0);
      //Gtk.ComboBox selector = new Gtk.ComboBox ();
      Gtk.ComboBox selector = ComboBox.NewText ();
      ScrolledWindow scroll = new ScrolledWindow ();
      filterEntry = new Gtk.Entry ();
     filterEntry.PrimaryIcon = true ;
      filterEntry.MaxLength = 1;
      filterEntry.Changed += OnFilterEntryTextChanged;
      
      //Gtk.TreeView tree = new Gtk.TreeView ();
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
      
      projects = DataLayer.GetProjects ();
      foreach (String project in projects)
      {
        selector.AppendText (project);
      }
      selector.Changed += new EventHandler (OnSelectorChanged);
      
      //Store AssetStore = dlayer.GetAssets (projID);
      /*filter = new Gtk.TreeModelFilter (AssetStore, null);
      filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
      tree.Model = filter;*/
      //tree.Model = dlayer.GetAssets (projID);
      
      tree.AppendColumn (iconCol);
      tree.AppendColumn (primCol);
      tree.AppendColumn (secCol);

      MenuBar menu = new MenuBar ();
      Menu fileMenu = new Menu();
      MenuItem menuItem = new MenuItem("_File");
      menuItem.Submenu = fileMenu;
      menu.Append(menuItem); 
      
      Statusbar status = new Statusbar ();
      Toolbar tool = new Toolbar ();
      
      ToolButton plus = new ToolButton (Gtk.Stock.Add);
      plus.Clicked +=  OnAddClicked;
      ToolButton minus = new ToolButton (Gtk.Stock.Remove);
      minus.Clicked +=  OnMinusClicked;
      
      box.PackStart (menu, false, true, 0);
      box.PackStart (tool, false, true, 0);
      tool.Insert(minus, 0);
      tool.Insert(plus, 0);
      top.PackStart (filterEntry, true, true, 0);
      top.PackStart (selector, true, true, 0);
      box.PackStart (top, false, true, 0);
      scroll.Add (tree);
      box.PackStart (scroll, true, true, 0);
      box.PackStart (status, false, true, 0);
      window.Add (box);
      window.ShowAll ();
    }
  }
}
