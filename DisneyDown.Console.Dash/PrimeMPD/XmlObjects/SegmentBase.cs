using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "SegmentBase", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class SegmentBase
    {
        [XmlElement(ElementName = "Initialization", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public Initialization Initialization { get; set; }

        [XmlElement(ElementName = "RepresentationIndex", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public RepresentationIndex RepresentationIndex { get; set; }

        [XmlAttribute(AttributeName = "timescale")]
        public string Timescale { get; set; }

        [XmlAttribute(AttributeName = "indexRange")]
        public string IndexRange { get; set; }

        [XmlAttribute(AttributeName = "presentationTimeOffset")]
        public string PresentationTimeOffset { get; set; }
    }
}