using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{

    public class FilterLinq<T>
    {
        public static Expression<Func<T, Boolean>> GetWherePredicate(params SearchField[] SearchFieldList)
        {

            //the 'IN' parameter for expression ie T=> condition
            ParameterExpression pe = Expression.Parameter(typeof(T), typeof(T).Name);

            //combine them with and 1=1 Like no expression
            Expression combined = null;

            if (SearchFieldList != null)
            {
                foreach (var fieldItem in SearchFieldList)
                {
                    var member = Expression.Property(pe, fieldItem.Name);
                    //var prop = fieldItem.Name.Split('.').Aggregate((Expression)pe, Expression.Property);
                    var propertyType = ((System.Reflection.PropertyInfo)member.Member).PropertyType;
                    //var propertyInfo = (System.Reflection.PropertyInfo)member.Member;
                    var converter = System.ComponentModel.TypeDescriptor.GetConverter(propertyType); // 1

                    if (!converter.CanConvertFrom(typeof(string))) // 2
                        throw new NotSupportedException();

                    //var propertyValue = propertyType != typeof(DateTime) ? converter.ConvertFromInvariantString(fieldItem.Value)
                    //    : converter.ConvertFromInvariantString(GetDateTimeValue(fieldItem.Value)); // 3
                    var propertyValue = converter.ConvertFromInvariantString(fieldItem.Value);
                    var constant = Expression.Constant(propertyValue);
                    var valueExpression = Expression.Convert(constant, propertyType); // 4

                    ////Expression for accessing Fields name property
                    //Expression columnNameProperty = Expression.Property(pe, fieldItem.Name);


                    ////the name constant to match 
                    //Expression columnValue = Expression.Constant(fieldItem.Value);


                    ////the first expression: PatientantLastName = ?
                    //e1 = Expression.Equal(columnNameProperty, columnValue);

                    //the first expression: PatientantLastName = ?
                    Expression e1;
                    switch (fieldItem.Operator)
                    {
                        case QueryOperators.NotEqual:
                            e1= Expression.NotEqual(member, valueExpression);
                            break;
                        case QueryOperators.GreaterThan:
                            e1 = Expression.GreaterThan(member, valueExpression);
                            break;
                        case QueryOperators.LessThan:
                            e1 = Expression.LessThan(member, valueExpression);
                            break;
                        case QueryOperators.GreaterThanEqual:
                            e1 = Expression.GreaterThanOrEqual(member, valueExpression);
                            break;
                        case QueryOperators.LessThanEqual:
                            e1 = Expression.LessThanOrEqual(member, valueExpression);
                            break;
                        case QueryOperators.StartsWith:
                            //MemberExpression m = Expression.MakeMemberAccess(pe, propertyInfo);
                            MethodInfo mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                            //Expression call = Expression.Call(m, mi, constant);
                            e1 = Expression.Call(member, mi, valueExpression);
                            break;
                        default:
                            e1 = Expression.Equal(member, valueExpression);
                            break;
                    }

                    if (combined == null)
                    {
                        combined = e1;
                    }
                    else
                    {
                        combined = Expression.And(combined, e1);
                    }
                }
            }

            //create and return the predicate
            return Expression.Lambda<Func<T, Boolean>>(combined, new ParameterExpression[] { pe });
        }

        private static string GetDateTimeValue(string value)
        {
            string separator = @"/";
            string[] arr = value.Split(separator.ToCharArray());
            string date = arr[1] + separator + arr[0] + separator + arr[2];
            return date;
        }

        public static Expression<Func<T, Boolean>> GetWherePredicate(Dictionary<SearchField,string> SearchFieldList)
        {

            //the 'IN' parameter for expression ie T=> condition
            ParameterExpression pe = Expression.Parameter(typeof(T), typeof(T).Name);

            //combine them with and 1=1 Like no expression
            Expression combined = null;

            if (SearchFieldList != null)
            {
                foreach (var fieldItem in SearchFieldList)
                {
                    //Expression for accessing Fields name property
                    Expression columnNameProperty = Expression.Property(pe, fieldItem.Key.Name);


                    //the name constant to match 
                    Expression columnValue = Expression.Constant(fieldItem.Key.Value);
                    Expression e1;
                    if (fieldItem.Value == "==")
                    {
                        //the first expression: PatientantLastName = ?
                        e1 = Expression.Equal(columnNameProperty, columnValue);
                    }
                    else
                    {
                        e1 = Expression.NotEqual(columnNameProperty, columnValue);
                    }

                    if (combined == null)
                    {
                        combined = e1;
                    }
                    else
                    {
                        combined = Expression.And(combined, e1);
                    }
                }
            }

            //create and return the predicate
            return Expression.Lambda<Func<T, Boolean>>(combined, new ParameterExpression[] { pe });
        }

    }
}
