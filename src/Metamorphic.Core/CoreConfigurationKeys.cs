﻿//-----------------------------------------------------------------------
// <copyright company="Metamorphic">
//     Copyright 2013 Metamorphic. Licensed under the Apache License, Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Nuclei.Configuration;

namespace Metamorphic.Core
{
    /// <summary>
    /// Defines the configuration keys for the core part of the application.
    /// </summary>
    public static class CoreConfigurationKeys
    {
        /// <summary>
        /// The configuration key that is used to retrieve path for the directory in 
        /// which the rule files will be placed.
        /// </summary>
        internal static readonly ConfigurationKey ScriptDirectory
            = new ConfigurationKey("ScriptPath", typeof(string));

        /// <summary>
        /// Returns a collection containing all the configuration keys for the application.
        /// </summary>
        /// <returns>A collection containing all the configuration keys for the application.</returns>
        public static IEnumerable<ConfigurationKey> ToCollection()
        {
            return new List<ConfigurationKey>
                {
                    ScriptDirectory
                };
        }
    }
}
