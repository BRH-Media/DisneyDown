using Sprache;
using System;
using System.Linq;

namespace DisneyDown.Common.Parsers.HLS
{
    public static class EnumParser<T>
    {
        public static Parser<T> Create()
        {
            var names = Enum.GetNames(typeof(T));

            var parser = Parse.IgnoreCase(names.First()).Token()
                .Return((T)Enum.Parse(typeof(T), names.First()));

            foreach (var name in names.Skip(1))
            {
                var nameToParse = name.Replace('_', '-');
                parser = parser.Or(Parse.IgnoreCase(nameToParse).Token().Return((T)Enum.Parse(typeof(T), name)));
            }

            return parser;
        }
    }
}