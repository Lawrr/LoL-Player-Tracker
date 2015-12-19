using LoLPlayerTracker.Exceptions;
using RiotSharp;
using System;

namespace LoLPlayerTracker {
    public static class RegionParser {

        public static Region Parse(string regionString) {
            Region region;
            try {
                region = (Region)Enum.Parse(typeof(Region), regionString, true);
            } catch (ArgumentException e) {
                throw new RegionNotFoundException(String.Format("Could not find region '{0}'", regionString));
            }
            return region;
        }

    }
}
