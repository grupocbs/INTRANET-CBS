﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text; 

/// <summary>
/// Descripción breve de GPXLoader
/// </summary>

public class GPXLoader
{
    /// <summary> 
    /// Load the Xml document for parsing 
    /// </summary> 
    /// <param name="sFile">Fully qualified file name (local)</param> 
    /// <returns>XDocument</returns> 
    private XDocument GetGpxDoc(string sFile)
    {
        XDocument gpxDoc = XDocument.Load(sFile);
        return gpxDoc;
    }

    /// <summary> 
    /// Load the namespace for a standard GPX document 
    /// </summary> 
    /// <returns></returns> 
    private XNamespace GetGpxNameSpace()
    {
        XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");
        return gpx;
    }

    /// <summary> 
    /// When passed a file, open it and parse all waypoints from it. 
    /// </summary> 
    /// <param name="sFile">Fully qualified file name (local)</param> 
    /// <returns>string containing line delimited waypoints from 
    /// the file (for test)</returns> 
    /// <remarks>Normally, this would be used to populate the 
    /// appropriate object model</remarks> 
    public string LoadGPXWaypoints(string sFile)
    {
        XDocument gpxDoc = GetGpxDoc(sFile);
        XNamespace gpx = GetGpxNameSpace();

        var waypoints = from waypoint in gpxDoc.Descendants(gpx + "wpt")
                        select new
                        {
                            Latitude = waypoint.Attribute("lat").Value,
                            Longitude = waypoint.Attribute("lon").Value,
                            Elevation = waypoint.Element(gpx + "ele") != null ?
                                waypoint.Element(gpx + "ele").Value : null,
                            Name = waypoint.Element(gpx + "name") != null ?
                                waypoint.Element(gpx + "name").Value : null,
                            Dt = waypoint.Element(gpx + "cmt") != null ?
                                waypoint.Element(gpx + "cmt").Value : null
                        };

        StringBuilder sb = new StringBuilder();
        foreach (var wpt in waypoints)
        {
            // This is where we'd instantiate data 
            // containers for the information retrieved. 
            sb.Append(
              string.Format("Name:{0} Latitude:{1} Longitude:{2} Elevation:{3} Date:{4}\n",
              wpt.Name, wpt.Latitude, wpt.Longitude,
              wpt.Elevation, wpt.Dt));
        }

        return sb.ToString();
    }

    /// <summary> 
    /// When passed a file, open it and parse all tracks 
    /// and track segments from it. 
    /// </summary> 
    /// <param name="sFile">Fully qualified file name (local)</param> 
    /// <returns>string containing line delimited waypoints from the 
    /// file (for test)</returns> 
    public List<Punto> LoadGPXTracks(string sFile)
    {
        XDocument gpxDoc = GetGpxDoc(sFile);
        XNamespace gpx = GetGpxNameSpace();
        var tracks = from track in gpxDoc.Descendants(gpx + "trk")
                     select new
                     {
                         Name = track.Element(gpx + "name") != null ?
                          track.Element(gpx + "name").Value : null,
                       
                         Segs = (
                              from trackpoint in track.Descendants(gpx + "trkpt")
                              select new
                              {
                                  Latitude = trackpoint.Attribute("lat").Value,
                                  Longitude = trackpoint.Attribute("lon").Value,
                                  Elevation = trackpoint.Element(gpx + "ele") != null ?
                                    trackpoint.Element(gpx + "ele").Value : null,
                                  Time = trackpoint.Element(gpx + "time") != null ?
                                    trackpoint.Element(gpx + "time").Value : null,
                                  Speed = trackpoint.Element(gpx + "speed") != null ?
                                    trackpoint.Element(gpx + "speed").Value : null
                              }
                            )
                     };

        //StringBuilder sb = new StringBuilder();
        List<Punto> sb = new List<Punto>();
        foreach (var trk in tracks)
        {
            // Populate track data objects. 
            foreach (var trkSeg in trk.Segs)
            {
                // Populate detailed track segments 
                // in the object model here. 
               /* sb.Append(
                  string.Format("Track:{0} - Latitude:{1} Longitude:{2} " +
                               "Elevation:{3} Date:{4} Speed:{5}\n",
                  trk.Name, trkSeg.Latitude,
                  trkSeg.Longitude, trkSeg.Elevation,
                  trkSeg.Time, trkSeg.Speed));*/
                Punto pu = new Punto();
                pu.Track = trk.Name;
                pu.Latitude = trkSeg.Latitude;
                pu.Longitude = trkSeg.Longitude;
                pu.Elevation = trkSeg.Elevation;
                pu.Time = trkSeg.Time;
                pu.Speed = trkSeg.Speed;
                sb.Add(pu);
            }
        }
        
        return sb;
    }


    
}


public class Punto
{
    public  string Track;
    public string Latitude;
    public string Longitude;
    public string Elevation;
    public string Time;
    public string Speed;
} 
      
 
