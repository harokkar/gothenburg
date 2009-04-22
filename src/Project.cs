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
  public struct Tag
  {
    public string AppID;
    public string AppTag;
  }

  public class Project
  {
    public string Name;
    public List<Tag> TagsAssociated;

    public Project (string Name)
    {
      this.Name = Name;
      this.TagsAssociated = new List<Tag> ();
    }

    public void AddTag (string appID, string apptag)
    {
      Tag tag = new Tag ();
      tag.AppID = appID;
      tag.AppTag = apptag;
      this.TagsAssociated.Add (tag);
    }
  }
}
