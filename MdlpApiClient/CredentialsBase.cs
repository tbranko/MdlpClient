﻿namespace MdlpApiClient
{
    using DataContracts;

    /// <summary>
    /// MDLP REST API credentials base class.
    /// </summary>
    public abstract class CredentialsBase
    {
        /// <summary>
        /// Client identifier.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Performs authentication, returns access token with a limited lifetime.
        /// </summary>
        /// <param name="apiClient">MDLP client to perform API calls.</param>
        /// <returns><see cref="AuthToken"/> instance.</returns>
        public abstract AuthToken Authenticate(MdlpClient restClient);
    }
}
