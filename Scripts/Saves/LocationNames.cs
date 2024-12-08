namespace Saves
{
    public static class LocationNames
    {
        public static readonly string Hub = "hub";
        public static readonly string Urmartalyk = "urmartalyk";

        public static string GetFromEnum(Location location)
        {
            switch (location)
            {
                case Location.Hub:
                    return Hub;

                case Location.Urmartalyk:
                    return Urmartalyk;

                default:
                    return null;
            }
        }
    }
}
