using System.Collections.Generic;
using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "MPD", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class Manifest
    {
        [XmlElement(ElementName = "Period", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public List<Period> Period { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "cenc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Cenc { get; set; }

        [XmlAttribute(AttributeName = "mas", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Mas { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "majorVersion")]
        public string MajorVersion { get; set; }

        [XmlAttribute(AttributeName = "mediaPresentationDuration")]
        public string MediaPresentationDuration { get; set; }

        [XmlAttribute(AttributeName = "minBufferTime")]
        public string MinBufferTime { get; set; }

        [XmlAttribute(AttributeName = "minorVersion")]
        public string MinorVersion { get; set; }

        [XmlAttribute(AttributeName = "profiles")]
        public string Profiles { get; set; }

        [XmlAttribute(AttributeName = "revision")]
        public string Revision { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
    }
}