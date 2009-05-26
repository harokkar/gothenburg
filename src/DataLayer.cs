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
using Gothenburg.AssetProvider;

namespace Gothenburg
{
  public class DataLayer
  {
    static int icon_width = 25;
    static int icon_height = 25; //TODO: BOTH LINES BACK to GUI
    static List<Project> projects;
   
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
      projects = new List<Project> ();
     
     
     //TODO: LEAKY
      Project Neu = new Project ("Foo");
      Neu.AddTag ("Tomboy", "a");
      projects.Add (Neu);
      
      Project Neu2 = new Project ("Bar");
      Neu2.AddTag ("Tomboy", "abc");
      projects.Add (Neu2);
//      projects.Add ("Foo Project");

      Console.WriteLine (projects[0].Name);
    }

    public IAssetProvider AssetProviderType_GetInterface(string APType)
    {
      IAssetProvider IAP;
      if(APType == "Tomboy")
        IAP = new AssetProvider.Tomboy ();
      else
        return null;
      IAP.init ();
      return IAP;
    }
    
    public void DelAsset (TreePath path, int projID)
    {
    /*TreeIter iter;
      if(projs[projID].GetIter(out iter, path))
        projs[projID].Remove(ref iter);*/
    }
    
    public ListStore GetAssets(int projID)
    {
      ListStore assets = new Gtk.ListStore (typeof (Asset));
      IAssetProvider IAP;
      
      foreach (Tag tag in projects[projID].TagsAssociated)
      {
        //IAP = AssetProviderType_GetInterface(projects[projID].TagsAssociated[0].AppID);
        IAP = AssetProviderType_GetInterface(tag.AppID);
        //string [] notes = IAP.retrieve_by_tag (projects[projID].TagsAssociated[0].AppTag);
        string [] notes = IAP.retrieve_by_tag (tag.AppTag);
        foreach (string note in notes)
        {
          assets.AppendValues (new Asset("Tomboy", new Gdk.Pixbuf ("Icon.xpm", icon_width,icon_height), note, IAP.get_primary_info(note), "14.04.2009"));
        }
      }
      return assets;
    }
    
    public static string [] GetProjectNames ()
    { 
      List<string> projects_string = new List<string> ();
      foreach (Project project in projects)
        projects_string.Add (project.Name);
      return projects_string.ToArray ();
    }
  }
}
