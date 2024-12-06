// Copyright (c) Erlimar Silva Campos. All rights reserved.
// Licensed under the terms described in the LICENSE file.

using System.CommandLine;

using LibGit2Sharp;

namespace ConventionalReleaseNotes;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Conventional Release Notes");

        rootCommand.SetHandler(() =>
            {
                PrintGitLog();
            });

        return await rootCommand.InvokeAsync(args);
    }

    static void PrintGitLog()
    {
        var nl = Environment.NewLine;
        var repo = new Repository(".");

        Console.WriteLine("Print git logging...");

        foreach (var commit in repo.Commits.Reverse())
        {
            var notes = string.Join(nl, commit.Notes);

            Console.WriteLine($"{commit.Sha}: {commit.MessageShort}{nl}  - {(string.IsNullOrWhiteSpace(notes) ? "<null>" : notes)}");
        }

    }
}