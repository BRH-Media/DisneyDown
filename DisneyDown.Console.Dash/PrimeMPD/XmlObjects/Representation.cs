using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "Representation", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
    public class Representation
    {
        [XmlElement(ElementName = "AudioChannelConfiguration", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public AudioChannelConfiguration AudioChannelConfiguration { get; set; }

        [XmlElement(ElementName = "BaseURL", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public string BaseUrl { get; set; }

        [XmlElement(ElementName = "SegmentBase", Namespace = "urn:mpeg:dash:schema:mpd:2011")]
        public SegmentBase SegmentBase { get; set; }

        [XmlAttribute(AttributeName = "audioSamplingRate")]
        public string AudioSamplingRate { get; set; }

        [XmlAttribute(AttributeName = "bandwidth")]
        public string Bandwidth { get; set; }

        [XmlAttribute(AttributeName = "codecs")]
        public string Codecs { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "trackName")]
        public string TrackName { get; set; }

        [XmlAttribute(AttributeName = "aspectRatio")]
        public string AspectRatio { get; set; }

        [XmlAttribute(AttributeName = "codecPrivateData")]
        public string CodecPrivateData { get; set; }

        [XmlAttribute(AttributeName = "frameRate")]
        public string FrameRate { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }

        [XmlAttribute(AttributeName = "sar")]
        public string Sar { get; set; }

        [XmlAttribute(AttributeName = "scanType")]
        public string ScanType { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
    }
}