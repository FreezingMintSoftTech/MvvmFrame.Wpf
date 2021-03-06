﻿using JwtTestAdapter.Helpers;
using System;

namespace JwtTestAdapter.Entities
{
    public class ThenBlock<TResult> : BlockBase<TResult>
    {
        internal ThenBlock() { }

        public virtual ThenBlock<TResult> And(string description, Action action)
        {
            LoggingHelper.Info($"[then] (start) {description}");

            action();

            LoggingHelper.Info($"[then] (end) {description}");

            return new ThenBlock<TResult> { Result = Result };
        }

        public virtual ThenBlock<TResult> And(string description, Action<TResult> action)
        {
            LoggingHelper.Info($"[then] (start) {description}");

            action((TResult)Result);

            LoggingHelper.Info($"[then] (end) {description}");

            return new ThenBlock<TResult> { Result = Result };
        }

        public virtual ThenBlock<TResult1> And<TResult1>(string description, Func<TResult1> func)
        {
            LoggingHelper.Info($"[then] (start) {description}");

            var then = new ThenBlock<TResult1> { Result = func() };

            LoggingHelper.Info($"[then] (end) {description}");

            return then;
        }

        public virtual ThenBlock<TResult1> And<TResult1>(string description, Func<TResult, TResult1> func)
        {
            LoggingHelper.Info($"[then] (start) {description}");

            var then = new ThenBlock<TResult1> { Result = func((TResult)Result) };

            LoggingHelper.Info($"[then] (end) {description}");

            return then;
        }
    }
}
