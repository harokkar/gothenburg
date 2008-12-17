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
using Gtk;
using Mono.Unix;

namespace Gothenburg
{
  using AssetProvider;

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

    public void Open ()
    {
      if(this.Type == "Tomboy")
      {
        AssetProvider.Tomboy tomboy = new AssetProvider.Tomboy ();
        tomboy.init();
        tomboy.open (tomboy.retrieve_by_name(this.Primary));
      }
    }
    
    public string GetPrimary ()
    {
      return this.Primary;
    }
  }
}
