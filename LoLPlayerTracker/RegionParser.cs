using RiotSharp;
using System;

namespace LoLPlayerTracker {
    public static class RegionParser {

        public static Region Parse(string regionString) {
            return (Region) Enum.Parse(typeof(Region), regionString.ToLower());
        }

    }
}
