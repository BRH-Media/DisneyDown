using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "Initialization", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class Initialization
    {
        [XmlAttribute(AttributeName = "range")]
        public string Range { get; set; }

        [XmlAttribute(AttributeName = "sourceURL")]
        public string SourceUrl { get; set; }
    }
}