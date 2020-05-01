﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
namespace Microsoft.Azure.Cosmos.Sql
{
    using System;
    using System.Collections.Generic;

    internal sealed class SqlInScalarExpression : SqlScalarExpression
    {
        private SqlInScalarExpression(SqlScalarExpression needle, bool not, IReadOnlyList<SqlScalarExpression> haystack)
            : base(SqlObjectKind.InScalarExpression)
        {
            if (needle == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (haystack == null)
            {
                throw new ArgumentNullException("items");
            }

            if (haystack.Count == 0)
            {
                throw new ArgumentException("items can't be empty.");
            }

            foreach (SqlScalarExpression item in haystack)
            {
                if (item == null)
                {
                    throw new ArgumentException("items can't have a null item.");
                }
            }

            this.Needle = needle;
            this.Not = not;
            this.Haystack = new List<SqlScalarExpression>(haystack);
        }

        public SqlScalarExpression Needle
        {
            get;
        }

        public bool Not
        {
            get;
        }

        public IReadOnlyList<SqlScalarExpression> Haystack
        {
            get;
        }

        public static SqlInScalarExpression Create(SqlScalarExpression needle, bool not, params SqlScalarExpression[] haystack)
        {
            return new SqlInScalarExpression(needle, not, haystack);
        }

        public static SqlInScalarExpression Create(SqlScalarExpression needle, bool not, IReadOnlyList<SqlScalarExpression> haystack)
        {
            return new SqlInScalarExpression(needle, not, haystack);
        }

        public override void Accept(SqlObjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult>(SqlObjectVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        public override TResult Accept<T, TResult>(SqlObjectVisitor<T, TResult> visitor, T input)
        {
            return visitor.Visit(this, input);
        }

        public override void Accept(SqlScalarExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult>(SqlScalarExpressionVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        public override TResult Accept<T, TResult>(SqlScalarExpressionVisitor<T, TResult> visitor, T input)
        {
            return visitor.Visit(this, input);
        }
    }
}
