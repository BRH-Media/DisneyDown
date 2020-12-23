using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "RepresentationIndex", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class RepresentationIndex
    {
        [XmlAttribute(AttributeName = "range")]
        public string Range { get; set; }

        [XmlAttribute(AttributeName = "sourceURL")]
        public string SourceUrl { get; set; }
    }
}