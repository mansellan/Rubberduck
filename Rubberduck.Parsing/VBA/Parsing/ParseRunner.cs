﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rubberduck.VBEditor;

namespace Rubberduck.Parsing.VBA.Parsing
{
    public class ParseRunner : ParseRunnerBase
    {
        private const int _maxDegreeOfParserParallelism = -1;

        public ParseRunner(
            RubberduckParserState state, 
            IParserStateManager parserStateManager, 
            IModuleParser parser) 
        :base(state, 
            parserStateManager, 
            parser)
        { }

        protected override IReadOnlyCollection<(QualifiedModuleName module, ModuleParseResults results)> ModulePareResults(IReadOnlyCollection<QualifiedModuleName> modules, CancellationToken token)
        {
            if (!modules.Any())
            {
                return new List<(QualifiedModuleName module, ModuleParseResults results)>();
            }

            token.ThrowIfCancellationRequested();

            var results = new List<(QualifiedModuleName module, ModuleParseResults results)>();
            var lockObject = new object();

            var options = new ParallelOptions
            {
                CancellationToken = token,
                MaxDegreeOfParallelism = _maxDegreeOfParserParallelism
            };

            try
            {
                Parallel.ForEach(modules,
                    options,
                    () => new List<(QualifiedModuleName module, ModuleParseResults results)>(), 
                    (module, state, localList) =>
                    {
                        localList.Add((module, ModuleParseResults(module, token)));
                        return localList;
                    },
                    (finalResult) =>
                    {
                        lock (lockObject)
                        {
                            results.AddRange(finalResult);
                        }
                    }
                );
            }
            catch (AggregateException exception)
            {
                if (exception.Flatten().InnerExceptions.All(ex => ex is OperationCanceledException))
                {
                    throw exception.InnerException ?? exception; //This eliminates the stack trace, but for the cancellation, this is irrelevant.
                }
                StateManager.SetStatusAndFireStateChanged(this, ParserState.Error, token);
                throw;
            }

            return results;
        }
    }
}
