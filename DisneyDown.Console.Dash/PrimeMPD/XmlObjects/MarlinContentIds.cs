using System.Xml.Serialization;

namespace DisneyDown.Console.Dash.PrimeMPD.XmlObjects
{
    [XmlRoot(ElementName = "MarlinContentIds", Namespace = "urn:marlin:mas:1-0:services:schemas:mpd")]
    public class MarlinContentIds
    {
        [XmlElement(ElementName = "MarlinContentId", Namespace = "urn:marlin:mas:1-0:services:schemas:mpd")]
        public string MarlinContentId { get; set; }
    }
}