/*
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */

using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "Role", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class Role
    {
        [XmlAttribute(AttributeName = "schemeIdUri")]
        public string SchemeIdUri { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}