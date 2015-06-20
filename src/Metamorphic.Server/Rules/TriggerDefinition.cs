﻿//-----------------------------------------------------------------------
// <copyright company="Metamorphic">
//     Copyright 2013 Metamorphic. Licensed under the Apache License, Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Metamorphic.Server.Rules
{
    /// <summary>
    /// Stores information about a trigger in a rule.
    /// </summary>
    public class TriggerDefinition
    {
        /// <summary>
        /// The type of trigger.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// The collection of parameters for the trigger.
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}