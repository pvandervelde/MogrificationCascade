﻿//-----------------------------------------------------------------------
// <copyright company="Metamorphic">
//     Copyright 2013 Metamorphic. Licensed under the Apache License, Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using Nuclei.Configuration;

namespace Metamorphic.Server
{
    /// <summary>
    /// Defines all the configuration keys.
    /// </summary>
    internal static class ServerConfigurationKeys
    {
        /// <summary>
        /// The configuration key that is used to retrieve path for the directory in 
        /// which the rule files will be placed.
        /// </summary>
        internal static readonly ConfigurationKey s_RulePath
            = new ConfigurationKey("UploadPath", typeof(string));
    }
}
