using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "ContentProtection", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class ContentProtection
    {
        [XmlAttribute(AttributeName = "default_KID", Namespace = "urn:mpeg:cenc:2013")]
        public string DefaultKid { get; set; }

        [XmlAttribute(AttributeName = "schemeIdUri")]
        public string SchemeIdUri { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlElement(ElementName = "pssh", Namespace = "urn:mpeg:cenc:2013")]
        public string Pssh { get; set; }

        [XmlElement(ElementName = "MarlinContentIds", Namespace = "urn:marlin:mas:1-0:services:schemas:mpd")]
        public MarlinContentIds MarlinContentIds { get; set; }
    }
}