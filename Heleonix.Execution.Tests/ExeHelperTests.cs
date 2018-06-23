﻿// <copyright file="ExeHelperTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2017-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Execution.Tests
{
    using Heleonix.Execution;
    using Heleonix.Execution.Tests.Common;
    using Heleonix.Testing.NUnit.Aaa;
    using NUnit.Framework;
    using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

    /// <summary>
    /// Tests the <see cref="ExeHelper"/>.
    /// </summary>
    [ComponentTest(Type = typeof(ExeHelper))]
    public static class ExeHelperTests
    {
        /// <summary>
        /// Tests the <see cref="ExeHelper.Execute(string,string,bool,string,int)"/>
        /// </summary>
        [MemberTest(Name = nameof(ExeHelper.Execute) + "(string,string,bool,string,int)")]
        public static void Execute1()
        {
            When("the method is executed", () =>
            {
                var extractOutput = false;
                ExeResult result = null;

                Act(() =>
                {
                    result = ExeHelper.Execute(
                        ExeSimulatorPath.ExePath,
                        $"WriteOutput={extractOutput} ExitCode=1",
                        extractOutput,
                        string.Empty,
                        200);
                });

                And("output should be extracted", () =>
                {
                    extractOutput = true;

                    Should("return result with extracted output", () =>
                    {
                        Assert.That(result.ExitCode, Is.EqualTo(1));
                        Assert.That(result.Error, Contains.Substring("-error-"));
                        Assert.That(result.Output, Contains.Substring("-output-"));
                    });
                });

                And("output should not be extracted", () =>
                {
                    extractOutput = false;

                    Should("return result without extracted output", () =>
                    {
                        Assert.That(result.ExitCode, Is.EqualTo(1));
                        Assert.That(result.Error, Is.Null);
                        Assert.That(result.Output, Is.Null);
                    });
                });
            });
        }
    }
}