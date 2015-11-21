using RiotSharp;
using System;

namespace LoLPlayerTracker {
    public static class RegionParser {

        public static Region parse(string regionString) {
            return (Region) Enum.Parse(typeof(Region), regionString.ToLower());
        }

    }
}
