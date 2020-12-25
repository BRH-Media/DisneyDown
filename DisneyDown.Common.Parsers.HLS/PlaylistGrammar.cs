using DisneyDown.Common.Parsers.HLS.Playlist;
using Sprache;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace DisneyDown.Common.Parsers.HLS
{
    public class PlaylistGrammar
    {
        public static readonly Parser<string> TagIdStringParser =
           from tagStartDelimiter in Parse.Char('#').Once()
           from tagId in Parse.AnyChar.Until(Parse.Char(':')).Text()
                      .Or(Parse.AnyChar.Until(Parse.LineTerminator)).Text()
           select tagId;

        private static readonly Parser<char> DoubleQuote = Parse.Char('"');

        private static readonly Parser<char> DoubleQuotedContent =
            Parse.AnyChar.Except(DoubleQuote);

        internal static readonly Parser<string> DoubleQuotedString =
            from open in DoubleQuote
            from content in DoubleQuotedContent.Many().Text()
            from end in DoubleQuote
            select content;

        public static readonly Parser<PlaylistTagAttribute> AttributeWithQuotesParser =
           from key in Parse.AnyChar.Until(Parse.Char('=')).Text()
           from open in DoubleQuote
           from value in Parse.AnyChar.Until(DoubleQuote).Text()
           from separator in Parse.Char(',').Optional()
           select new PlaylistTagAttribute(key, value);

        public static readonly Parser<PlaylistTagAttribute> AttributeWithoutQuotesParser =
           from key in Parse.AnyChar.Until(Parse.Char('=')).Text()
           from value in Parse.AnyChar.Until(Parse.Char(',')).Text()
                      .Or(Parse.AnyChar.Until(Parse.LineTerminator)).Text()
           from separator in Parse.Char(',').Optional()
           select new PlaylistTagAttribute(key, value);

        public static readonly Parser<PlaylistTagAttribute> AttributeWithSingleValueParser =
           from key in Parse.AnyChar.Until(Parse.LineTerminator).Text()
           select new PlaylistTagAttribute(key, string.Empty);

        public static readonly Parser<string> TagAttributeStringParser =
           from attributeOrValue in Parse.AnyChar.Until(Parse.Char('"')).Then(_ => DoubleQuotedString)
                                .Or(Parse.AnyChar.Until(Parse.Char(',')))
                                .Or(Parse.AnyChar.Until(Parse.LineTerminator))
           select (new string(attributeOrValue.ToArray()));

        public static readonly Parser<PlaylistTagAttribute> TagAttributeParser =
           from attribute in AttributeWithQuotesParser
                          .Or(AttributeWithoutQuotesParser)
                          .Or(AttributeWithSingleValueParser)
           select attribute;

        public static readonly Parser<List<PlaylistTagAttribute>> MultipleTagAttributesParser =
           from attributes in TagAttributeParser.Many()
           select new List<PlaylistTagAttribute>(attributes);

        public static readonly Parser<PlaylistTagId> TagIdParser =
           from tagIdString in TagIdStringParser
           select TypedTagIdParser.Parse(tagIdString);

        public static readonly Parser<PlaylistTagAttribute> AttributeSplitParser =
           from valueOrKey in Parse.AnyChar.Until(Parse.Char('='))
                          .Or(Parse.AnyChar.Until(Parse.LineTerminator))
           from value in Parse.AnyChar.Until(Parse.Char(','))
                     .Or(Parse.AnyChar.Until(Parse.LineTerminator)).Optional()
           select new PlaylistTagAttribute(
              new string(valueOrKey.ToArray()),
              value.IsDefined ? new string(value.Get().ToArray()) : "");

        public static readonly Parser<PlaylistTagId> TypedTagIdParser =
           EnumParser<PlaylistTagId>.Create();

        public static readonly Parser<PlaylistItem> UriStringParser =
           from uri in Parse.AnyChar.Except(Parse.Char('#')).Until(Parse.LineTerminator)
           from lineTerminator in Parse.LineTerminator.Optional()
           select (new PlaylistUriItem(new string(uri.ToArray())));

        public static readonly Parser<PlaylistItem> PlaylistTagParser =
           from tagId in TagIdParser
           from attributes in MultipleTagAttributesParser
           select new PlaylistTagItem(tagId, attributes);

        public static readonly Parser<PlaylistItem> PlaylistParser =
           from item in PlaylistTagParser.Or(UriStringParser)
           select item;
    }
}