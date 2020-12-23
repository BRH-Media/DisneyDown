using System.Collections.Generic;
using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "Period", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class Period
    {
        [XmlElement(ElementName = "AdaptationSet", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public List<AdaptationSet> AdaptationSet { get; set; }

        [XmlAttribute(AttributeName = "duration")]
        public string Duration { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
    }
}