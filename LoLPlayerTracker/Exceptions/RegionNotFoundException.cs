using System;

namespace LoLPlayerTracker.Exceptions {
    public class RegionNotFoundException : Exception {

        public RegionNotFoundException() {
        }

        public RegionNotFoundException(string message) : base(message) {
        }

        public RegionNotFoundException(string message, Exception inner) : base(message, inner) {
        }

    }
}
