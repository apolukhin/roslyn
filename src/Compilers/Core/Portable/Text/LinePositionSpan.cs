﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
    /// <summary>
    /// Immutable span represented by a pair of line number and index within the line.
    /// </summary>
    public struct LinePositionSpan : IEquatable<LinePositionSpan>
    {
        private readonly LinePosition start;
        private readonly LinePosition end;

        /// <summary>
        /// Creates <see cref="LinePositionSpan"/>.
        /// </summary>
        /// <param name="start">Start position.</param>
        /// <param name="end">End position.</param>
        /// <exception cref="ArgumentException"><paramref name="end"/> preceeds <paramref name="start"/>.</exception>
        public LinePositionSpan(LinePosition start, LinePosition end)
        {
            if (end < start)
            {
                throw new ArgumentException("end", CodeAnalysisResources.EndMustNotBeLessThanStart);
            }

            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Gets the start position of the span.
        /// </summary>
        public LinePosition Start
        {
            get { return start; }
        }

        /// <summary>
        /// Gets the end position of the span.
        /// </summary>
        public LinePosition End
        {
            get { return end; }
        }

        public override bool Equals(object obj)
        {
            return obj is LinePositionSpan && Equals((LinePositionSpan)obj);
        }

        public bool Equals(LinePositionSpan other)
        {
            return this.start.Equals(other.start)
                && this.end.Equals(other.end);
        }

        public override int GetHashCode()
        {
            return Hash.Combine(start.GetHashCode(), end.GetHashCode());
        }

        public static bool operator ==(LinePositionSpan left, LinePositionSpan right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LinePositionSpan left, LinePositionSpan right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Provides a string representation for <see cref="LinePositionSpan"/>.
        /// </summary>
        /// <example>(0,0)-(5,6)</example>
        public override string ToString()
        {
            return string.Format("({0})-({1})", this.start, this.end);
        }
    }
}
