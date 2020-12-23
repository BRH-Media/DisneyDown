using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "AudioChannelConfiguration", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class AudioChannelConfiguration
    {
        [XmlAttribute(AttributeName = "schemeIdUri")]
        public string SchemeIdUri { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}