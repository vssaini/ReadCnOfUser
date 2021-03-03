using System;
using System.DirectoryServices.AccountManagement;

namespace ReadCnOfUser
{
    class Program
    {
        static void Main()
        {
            const string userWithCn = "David B. Starcevich";
            Console.WriteLine("Let's get sAMAccountName of user with cn as {0}", userWithCn);

            var user = FindUser("cn", userWithCn, "DC1.domain.com", "Administrator", "Pass99");
            if (user != null)
            {
                Console.WriteLine("We got sAMAccountName of the user as '{0}'", user.SamAccountName);
            }
            else
            {
                Console.WriteLine("User not found.");
            }

            // Awaiting for quit
            Console.ReadKey();
        }

        /// <summary>
        /// Finds a user by quering a property value
        /// </summary>
        /// <param name="queryProperty">The property to query</param>
        /// <param name="propertyValue">The property value to match</param>
        /// <param name="domainController">The domain controller's address</param>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="container">The destination's ou path</param>
        /// <returns>The user principal returned by the search</returns>
        public static UserPrincipal FindUser(string queryProperty, object propertyValue, string domainController, string username, string password, string container = null)
        {
            // Create the principal context
            var principalContext = string.IsNullOrEmpty(container) ? new PrincipalContext(ContextType.Domain, domainController, username, password) : new PrincipalContext(ContextType.Domain, domainController, container, username, password);

            // Create the query filter user principal
            var filterPrincipal = new UserPrincipal(principalContext);

            // Create an advanced filters object so that we can set a custom attribute
            var customFilter = new CustomAdvancedFilters(filterPrincipal);
            customFilter.CustomPropertySet(queryProperty, propertyValue, propertyValue.GetType(), MatchType.Equals);

            // Create the principal searcher
            var principalSearcher = new PrincipalSearcher(filterPrincipal);

            // Find the principal
            var principal = principalSearcher.FindOne();

            // Cast it to user principal
            var userPrincipal = principal as UserPrincipal;

            return userPrincipal;
        }
    }
}
