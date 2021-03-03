using System;
using System.DirectoryServices.AccountManagement;

namespace ReadCnOfUser
{
    /// <summary>
    /// This advanced filters object allows custom attributes to be set for the filter principal before performing a search 
    /// </summary>
    public class CustomAdvancedFilters : AdvancedFilters
    {
        /// <summary>
        /// Creates a new CustomAdvancedFilters object
        /// </summary>
        /// <param name="principal">The filter principal</param>
        public CustomAdvancedFilters(Principal principal): base(principal) { }

        /// <summary>
        /// Sets a custom attribute for the filter principal
        /// </summary>
        /// <param name="attribute">The attribute to set</param>
        /// <param name="value">The attribute value</param>
        /// <param name="objectType">The type of the value</param>
        /// <param name="matchType">Indicates how to match the result</param>
        public void CustomPropertySet(string attribute, object value, Type objectType, MatchType matchType)
        {
            // Set the custom property
            AdvancedFilterSet(attribute, value, objectType, matchType);
        }
    }
}
