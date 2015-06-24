﻿//-----------------------------------------------------------------------
// <copyright company="Metamorphic">
//     Copyright 2013 Metamorphic. Licensed under the Apache License, Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using Metamorphic.Core.Jobs;
using Metamorphic.Core.Signals;

namespace Metamorphic.Core.Rules
{
    /// <summary>
    /// Defines methods for transforming signals into work.
    /// </summary>
    public sealed class Rule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signalId">The ID of the signal.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="signalId"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="signalId"/> is an empty string.
        /// </exception>
        public Rule(string signalId)
        {
            {
                Lokad.Enforce.Argument(() => signalId);
                Lokad.Enforce.Argument(() => signalId, Lokad.Rules.StringIs.NotEmpty);
            }

            SignalId = signalId;
        }

        /// <summary>
        /// Returns a value indicating whether or not the current rule applies to the given signal.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>
        ///   <see langword="true" /> if the current rule applies to the given signal; otherwise, <see langword="false" />.
        /// </returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
            Justification = "Documentation can start with a language keyword")]
        public bool ShouldProcess(Signal signal)
        {
            if (signal == null)
            {
                return false;
            }

            return signal.SignalType.Equals(SignalId);
        }

        /// <summary>
        /// Gets or sets the signal identifier to which this rule applies.
        /// </summary>
        public SignalId SignalId
        {
            get;
        }

        /// <summary>
        /// Creates a new <see cref="Job"/> based on the current rule and the given signal.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns>The newly created job.</returns>
        public Job ToJob(Signal signal)
        {


            return new Job();
        }
    }
}
