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

using System;
using System.Collections.Generic;
using Gtk;
using Mono.Unix;

namespace Gothenburg
{
  public class DataLayer
  {
    static int icon_width = 25;
    static int icon_height = 25; //TODO: BOTH LINES BACK to GUI
    ListStore [] projs;
    List<string> projects;
   
    /*public Gtk.ListStore assets
    {
      get;
      set;
    }*/
    //private ArrayList projects;
    /*{
      get;
      set;
    }*/
    
    public DataLayer ()
    {
	    projects = new List<string> ();

      projects.Add ("AFST2");
      projects.Add ("Friends Of GNOME Website");
      projects.Add ("Free Desktop Summit Gran Canaria");

      projs = new ListStore [3];
      
      
      projs[0] = new Gtk.ListStore (typeof (Asset));
      for(int i=1; i<3; i++)
      {
        projs[0].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm",icon_width,icon_height), "LinkFoo" ,"Meet Tom", "19.03.2008"));
        projs[0].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.png",icon_width,icon_height), "LinkFoo" ,"Shop Groceries", "23.03.2008"));
        projs[0].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("lipsticktower.jpg",icon_width,icon_height), "LinkFoo" ,"New Lipstick", "19.03.2008"));
      }
      //projects.Add (assets.Clone ());
      //projs [0] = (ListStore) assets;
      //projs [0] = assets.Copy ();
      //assets.Clear ();
      
      projs[1] = new Gtk.ListStore (typeof (Asset));
      for(int i=1; i<3; i++)
      {
        projs[1].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.png",icon_width,icon_height), "LinkFoo" ,"XPPD", "23.03.2008"));
        projs[1].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm",icon_width,icon_height), "LinkFoo" ,"RD", "19.03.2008"));
        projs[1].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("lipsticktower.jpg",icon_width,icon_height), "XX0" ,"XX0", "19.03.2008"));
      }
      
      projs[2] = new Gtk.ListStore (typeof (Asset));
      AssetProvider.Tomboy tomboy = new AssetProvider.Tomboy ();  
      tomboy.init();
      //string[] notes = tomboy.show_all ();
      string[] notes = tomboy.retrieve_by_tag ("a");
      foreach (string note in notes)
      {
        projs[2].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm", icon_width,icon_height), note, tomboy.get_primary_info(note), "12.12.2008"));
      }
    }
    
  public void DelAsset (TreePath path, int projID)
  {
    TreeIter iter;
      if(projs[projID].GetIter(out iter, path))
        projs[projID].Remove(ref iter);
  }
    
  public ListStore GetAssets(int projID)
  {
    return projs[projID];
  }
    
  public string [] GetProjects ()
  {
    return projects.ToArray ();
  }

  public void GetAllNotes (int projID)
  {
    AssetProvider.Tomboy tomboy = new AssetProvider.Tomboy ();  
    tomboy.init();
	
    string[] notes = tomboy.retrieve_by_tag("a");
     
    foreach (string note in notes)
    {
      projs[projID].AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm", icon_width,icon_height), note, tomboy.get_primary_info(note), "12.12.2008"));
    }
  }
}
}
