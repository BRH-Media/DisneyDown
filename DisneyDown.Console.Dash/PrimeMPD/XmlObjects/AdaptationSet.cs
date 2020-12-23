using System.Collections.Generic;
using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "AdaptationSet", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class AdaptationSet
    {
        [XmlElement(ElementName = "Role", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public Role Role { get; set; }

        [XmlElement(ElementName = "Representation", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public List<Representation> Representation { get; set; }

        [XmlAttribute(AttributeName = "contentType")]
        public string ContentType { get; set; }

        [XmlAttribute(AttributeName = "group")]
        public string Group { get; set; }

        [XmlAttribute(AttributeName = "lang")]
        public string Lang { get; set; }

        [XmlAttribute(AttributeName = "maxBandwidth")]
        public string MaxBandwidth { get; set; }

        [XmlAttribute(AttributeName = "mimeType")]
        public string MimeType { get; set; }

        [XmlAttribute(AttributeName = "minBandwidth")]
        public string MinBandwidth { get; set; }

        [XmlAttribute(AttributeName = "segmentAlignment")]
        public string SegmentAlignment { get; set; }

        [XmlAttribute(AttributeName = "startWithSAP")]
        public string StartWithSap { get; set; }

        [XmlAttribute(AttributeName = "subsegmentAlignment")]
        public string SubsegmentAlignment { get; set; }

        [XmlAttribute(AttributeName = "subsegmentStartsWithSAP")]
        public string SubsegmentStartsWithSap { get; set; }

        [XmlAttribute(AttributeName = "maxHeight")]
        public string MaxHeight { get; set; }

        [XmlAttribute(AttributeName = "maxWidth")]
        public string MaxWidth { get; set; }

        [XmlAttribute(AttributeName = "par")]
        public string Par { get; set; }

        [XmlElement(ElementName = "ContentProtection", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public List<ContentProtection> ContentProtection { get; set; }
    }
}