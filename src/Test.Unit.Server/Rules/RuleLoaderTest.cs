﻿//-----------------------------------------------------------------------
// <copyright company="Metamorphic">
//     Copyright 2013 Metamorphic. Licensed under the Apache License, Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Metamorphic.Core.Actions;
using Metamorphic.Core.Rules;
using Nuclei.Diagnostics;
using NUnit.Framework;

namespace Metamorphic.Server.Rules
{
    [TestFixture]
    public sealed class RuleLoaderTest
    {
        [Test]
        public void CreateDefinitionFromFileWithActionWithParameters()
        {
            var fileName = "ActionWithParameters.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(0, definition.Condition.Count);
            Assert.AreEqual("Signal", definition.Signal.Id);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithActionWithParametersReferencingSignal()
        {
            var fileName = "ActionWithParametersReferencingSignal.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(0, definition.Condition.Count);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("{{signal.bar}}", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithEndsWithCondition()
        {
            var fileName = "EndsWithCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual("a", definition.Condition[0].Pattern);
            Assert.AreEqual("endswith", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithEqualsCondition()
        {
            var fileName = "EqualsCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual("a", definition.Condition[0].Pattern);
            Assert.AreEqual("equals", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithGreaterThanCondition()
        {
            var fileName = "GreaterThanCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual(10, definition.Condition[0].Pattern);
            Assert.AreEqual("greaterthan", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithLessThanCondition()
        {
            var fileName = "LessThanCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual(10, definition.Condition[0].Pattern);
            Assert.AreEqual("lessthan", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithMatchRegexCondition()
        {
            var fileName = "MatchRegexCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual("(.*)", definition.Condition[0].Pattern);
            Assert.AreEqual("matchregex", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithNotEqualsCondition()
        {
            var fileName = "NotEqualsCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual("a", definition.Condition[0].Pattern);
            Assert.AreEqual("notequals", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void CreateDefinitionFromFileWithNotMatchRegexCondition()
        {
            var fileName = "NotMatchRegexCondition.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var definition = loader.CreateDefinitionFromFile(Path.Combine(RulePath(), fileName));

            Assert.AreEqual("Name", definition.Name);
            Assert.AreEqual("Description", definition.Description);
            Assert.IsTrue(definition.Enabled);

            Assert.AreEqual(1, definition.Condition.Count);
            Assert.AreEqual("bar", definition.Condition[0].Name);
            Assert.AreEqual("(.*)", definition.Condition[0].Pattern);
            Assert.AreEqual("notmatchregex", definition.Condition[0].Type);

            Assert.AreEqual("Signal", definition.Signal.Id);
            Assert.AreEqual(1, definition.Signal.Parameters.Count);
            Assert.IsTrue(definition.Signal.Parameters.ContainsKey("bar"));
            Assert.AreEqual("stuff", definition.Action.Parameters["bar"]);

            Assert.AreEqual("Action", definition.Action.Id);
            Assert.AreEqual(1, definition.Action.Parameters.Count);
            Assert.IsTrue(definition.Action.Parameters.ContainsKey("foo"));
            Assert.AreEqual("bar", definition.Action.Parameters["foo"]);
        }

        [Test]
        public void IsValidWithActionWithInvalidId()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
                {
                    Name = "a",
                    Action = new ActionRuleDefinition
                        {
                            Id = "b",
                            Parameters = new Dictionary<string, object>(),
                        },
                    Condition = new List<ConditionRuleDefinition>(),
                    Enabled = true,
                    Signal = new SignalRuleDefinition
                        {
                            Id = "c",
                            Parameters = new Dictionary<string, object>(),
                        }
                };
            Assert.IsFalse(loader.IsValid(definition, s => false, s => true));
        }

        [Test]
        public void IsValidWithActionWithInvalidSignalParameterReference()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>
                    {
                        ["c"] = "{{signal.d}}"
                    },
                },
                Condition = new List<ConditionRuleDefinition>(),
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "d",
                    Parameters = new Dictionary<string, object>
                    {
                        ["e"] = "f"
                    },
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithConditionWithIncorrectName()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>(),
                },
                Condition = new List<ConditionRuleDefinition>
                {
                    new ConditionRuleDefinition
                    {
                        Name = "b",
                        Pattern = "d",
                        Type = "equals"
                    },
                },
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "c",
                    Parameters = new Dictionary<string, object>(),
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithConditionWithInvalidType()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>(),
                },
                Condition = new List<ConditionRuleDefinition>
                {
                    new ConditionRuleDefinition
                    {
                        Name = "c",
                        Pattern = "d",
                        Type = "operator"
                    },
                },
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "c",
                    Parameters = new Dictionary<string, object>(),
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithMissingAction()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = null,
                Condition = new List<ConditionRuleDefinition>(),
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "c",
                    Parameters = new Dictionary<string, object>(),
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithMissingName()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = string.Empty,
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>(),
                },
                Condition = new List<ConditionRuleDefinition>(),
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "c",
                    Parameters = new Dictionary<string, object>(),
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithMissingSignal()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>(),
                },
                Condition = new List<ConditionRuleDefinition>(),
                Enabled = true,
                Signal = null
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => true));
        }

        [Test]
        public void IsValidWithSignalWithInvalidType()
        {
            var loader = new RuleLoader(
                s => false,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));

            var definition = new RuleDefinition
            {
                Name = "a",
                Action = new ActionRuleDefinition
                {
                    Id = "b",
                    Parameters = new Dictionary<string, object>(),
                },
                Condition = new List<ConditionRuleDefinition>(),
                Enabled = true,
                Signal = new SignalRuleDefinition
                {
                    Id = "c",
                    Parameters = new Dictionary<string, object>(),
                }
            };
            Assert.IsFalse(loader.IsValid(definition, s => true, s => false));
        }

        [Test]
        public void LoadWithActionWithoutReference()
        {
            var fileName = "ActionWithoutReference.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithConditionWithoutName()
        {
            var fileName = "ConditionWithoutName.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithConditionWithoutPattern()
        {
            var fileName = "ConditionWithoutPattern.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithConditionWithoutType()
        {
            var fileName = "ConditionWithoutType.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithEmptyFile()
        {
            var fileName = "EmptyFile.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithEmptyFilePath()
        {
            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(string.Empty);
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithMissingRuleEnabledSwitch()
        {
            var fileName = "MissingEnabledFlag.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithMissingRuleDescription()
        {
            var fileName = "MissingDescription.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithMissingRuleName()
        {
            var fileName = "MissingName.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithNonExistingFilePath()
        {
            var fileName = "MissingFile.mmrule";

            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(Path.Combine(RulePath(), fileName));
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithNullFilePath()
        {
            var loader = new RuleLoader(
                s => true,
                s => true,
                new SystemDiagnostics((l, m) => { }, null));
            var rule = loader.Load(null);
            Assert.IsNull(rule);
        }

        [Test]
        public void LoadWithoutAction()
        {
        }

        [Test]
        public void LoadWithoutTrigger()
        {
        }

        [Test]
        public void LoadWithTriggerWithoutType()
        {
        }

        [Test]
        public void Load()
        {
            foobar();
        }

        private static string RulePath()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return path;
        }
    }
}
