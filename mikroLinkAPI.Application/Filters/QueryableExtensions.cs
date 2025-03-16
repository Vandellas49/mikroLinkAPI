using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mikroLinkAPI.Application.Features;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace mikroLinkAPI.Application.Filters
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> FilterBy<T, Tp>(
                         this IQueryable<T> source,
                         string propertyName,
                         string queryType,
                         List<Tp> searchTerms)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");
            }

            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyInfo);
            Expression body;

            switch (queryType.ToLower())
            {
                case "in":
                    if (searchTerms == null || !searchTerms.Any())
                    {
                        throw new ArgumentException("Search terms must be provided for 'in' query type.");
                    }

                    // Create the 'Contains' expression for "in" query type
                    var constant = Expression.Constant(searchTerms);
                    body = Expression.Call(
                        typeof(Enumerable),
                        "Contains",
                        new[] { propertyInfo.PropertyType },
                        constant,
                        property
                    );
                    break;

                default:
                    throw new ArgumentException("Invalid query type");
            }

            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
            return source.Where(predicate);
        }
        public static IQueryable<T> FilterBy<T, Tp>(
                            this IQueryable<T> source,
                            Expression<Func<T, object>> propertySelector,
                            string queryType,
                            Tp searchTerm)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpr = propertySelector.Body as UnaryExpression;
                if (unaryExpr != null)
                    memberExpression = unaryExpr.Operand as MemberExpression;

                if (memberExpression == null)
                    throw new ArgumentException("Invalid property selector expression.");
            }

            var parameter = Expression.Parameter(typeof(T), "e");
            MemberExpression property = null;
            var members = memberExpression.ToString().Split(new[] { '.', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var member in members)
            {
                if (string.IsNullOrEmpty(member) || member == "p") continue;

                if (property == null)
                {
                    property = Expression.Property(parameter, member);
                }
                else
                {
                    property = Expression.Property(property, member);
                }
            }
            Expression body;

            switch (queryType.ToLower())
            {
                case "contains":
                    body = Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(searchTerm));
                    break;
                case "notcontains":
                    body = Expression.Not(
                        Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(searchTerm))
                    );
                    break;
                case "startswith":
                    body = Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(searchTerm));
                    break;
                case "endswith":
                    body = Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(searchTerm));
                    break;
                case "equals":
                    var type = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
                    if (type.IsNullableType())
                        body = Expression.Equal(Expression.Property(property, "Value"), Expression.Constant(searchTerm));
                    else
                        body = Expression.Equal(property, Expression.Constant(searchTerm));
                    break;
                case "lt":
                    body = Expression.LessThan(property, Expression.Constant(searchTerm));
                    break;
                case "lte":
                    body = Expression.LessThanOrEqual(property, Expression.Constant(searchTerm));
                    break;
                case "gt":
                    body = Expression.GreaterThan(property, Expression.Constant(searchTerm));
                    break;
                case "gte":
                    body = Expression.GreaterThanOrEqual(property, Expression.Constant(searchTerm));
                    break;
                case "is":
                    body = Expression.Equal(property, Expression.Constant(searchTerm));
                    break;
                case "isnot":
                    body = Expression.NotEqual(property, Expression.Constant(searchTerm));
                    break;
                case "before":
                    body = Expression.LessThan(property, Expression.Constant(searchTerm));
                    break;
                case "after":
                    body = Expression.GreaterThan(property, Expression.Constant(searchTerm));
                    break;
                default:
                    throw new ArgumentException("Invalid query type");
            }

            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
            return source.Where(predicate);
        }
        public static IQueryable<T> OrderByName<T>(this IQueryable<T> source, string attributeName, int order)
        {
            if (string.IsNullOrEmpty(attributeName))
                return source;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, attributeName);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            // Sıralama işlemi
            return order == 1 ? source.OrderBy(lambda) : source.OrderByDescending(lambda);
        }
    }
}
