﻿namespace DisneyDown.Common.Util.Kit
{
    public static class ConsoleSpinnerSequence
    {
        //the sequence of characters for each turn

        public static readonly string[][][] Sequence = {
            new[]
            {
                new[]
                {
                    @" ", @" ", @" ", @" ", @"/"
                },
                new[]
                {
                    @" ", @" ", @" ", @"/", @" "
                },
                new[]
                {
                    @" ", @" ", @"/", @" ", @" "
                },
                new[]
                {
                    @" ", @"/", @" ", @" ", @" "
                },
                new[]
                {
                    @"/", @" ", @" ", @" ", @" "
                }
            },
            new[]
            {
                new[]
                {
                    @" ", @" ", @" ", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @" ", @" ", @" "
                },
                new[]
                {
                    @"-", @"-", @"-", @"-", @"-"
                },
                new[]
                {
                    @" ", @" ", @" ", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @" ", @" ", @" "
                }
            },
            new[]
            {
                new[]
                {
                    @"\", @" ", @" ", @" ", @" "
                },
                new[]
                {
                    @" ", @"\", @" ", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @"\", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @" ", @"\", @" "
                },
                new[]
                {
                    @" ", @" ", @" ", @" ", @"\"
                }
            },
            new[]
            {
                new[]
                {
                    @" ", @" ", @"|", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @"|", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @"|", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @"|", @" ", @" "
                },
                new[]
                {
                    @" ", @" ", @"|", @" ", @" "
                }
            }
        };
    }
}