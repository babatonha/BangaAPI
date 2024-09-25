namespace Banga.Data.Queries
{
    public static class PropertyQueries
    {
        public static string WhereSearchTermsInCityOrSuburb(string searchTerms)
        {
            if (searchTerms.Length == 0) return string.Empty;

            return $"AND C.[Name] IN ('{searchTerms}') OR SB.[Name] IN ('{searchTerms}')";
        }

        public static string WhereBeds(int beds)
        {
            if(beds == 0) return  string.Empty;

            return $"AND P.NumberOfRooms >= {beds}";
        }

        public static string WhereBaths(int baths)
        {
            if (baths == 0) return string.Empty;

            return $"AND P.NumberOfBathrooms >= {baths}";
        }

        public static string WherePropertyType(int propertyTypeId)
        {
            if (propertyTypeId == 0) return string.Empty;

            return $"AND P.PropertyTypeId = {propertyTypeId}";
        }

        public static string WhereRegistrationType(int registrationTypeId)
        {
            if (registrationTypeId == 0) return string.Empty;

            return $"AND P.RegistrationTypeId = {registrationTypeId}";
        }

        public static string WhereMinPrice(double minPrice)
        {
            if (minPrice == 0) return string.Empty;

            return $"AND P.Price >= {minPrice}";
        }

        public static string WhereMaxPrice(double mmaxPrice)
        {
            if (mmaxPrice == 0) return string.Empty;

            return $"AND P.Price <= {mmaxPrice}";
        }
    }
}
