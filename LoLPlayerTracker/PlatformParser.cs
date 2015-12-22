using RiotSharp;

namespace LoLPlayerTracker {
    public static class PlatformParser {
        public static Platform Parse(Region region) {
            Platform platform;
            switch (region) {
                case Region.br:
                    platform = Platform.BR1;
                    break;
                case Region.eune:
                    platform = Platform.EUN1;
                    break;
                case Region.euw:
                    platform = Platform.EUW1;
                    break;
                case Region.kr:
                    platform = Platform.KR;
                    break;
                case Region.lan:
                    platform = Platform.LA1;
                    break;
                case Region.las:
                    platform = Platform.LA2;
                    break;
                case Region.na:
                    platform = Platform.NA1;
                    break;
                case Region.oce:
                    platform = Platform.OC1;
                    break;
                case Region.ru:
                    platform = Platform.RU;
                    break;
                case Region.tr:
                    platform = Platform.TR1;
                    break;
                default:
                    platform = Platform.NA1;
                    break;
            }
            return platform;
        }
    }
}
