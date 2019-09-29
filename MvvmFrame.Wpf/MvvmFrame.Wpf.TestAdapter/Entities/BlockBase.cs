﻿using System;
using System.Threading.Tasks;

namespace MvvmFrame.Wpf.TestAdapter.Entities
{
    /// <summary>
    /// Code block
    /// </summary>
    public abstract class BlockBase
    {
        internal BlockBase PreviousBlock { get; set; }

        /// <summary>
        /// Discription
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// Is async code block
        /// </summary>
        public virtual bool IsAsync => false;

        /// <summary>
        /// execute
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        internal abstract ValueTask<object> ExecuteAsync(object param);

        internal abstract object Execute(object param);
    }

    /// <summary>
    /// Code block
    /// </summary>
    public abstract class BlockBase<TInput, TOutput>: BlockBase
    {
        internal Func<TInput, TOutput> CodeBlock { get; set; }

        internal override ValueTask<object> ExecuteAsync(object param) => throw new NotImplementedException();

        internal override object Execute(object param)
        {
            if (param is TInput input)
                return CodeBlock(input);

            throw new ArgumentException($"{nameof(param)} must be of a different type");
        }
    }
}