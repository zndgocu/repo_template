using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Extensions.Helper
{
    public static class GenericHelper
    {
        public static T? GetValue<T>(object obj, string propertyName)
        {
            try
            {
                PropertyInfo? propInfo = obj.GetType().GetProperty(propertyName);
                if (propInfo is null) throw new Exception("error propertyName");
                object? itemValue = propInfo.GetValue(obj, null);
                if (itemValue is null) return default(T);
                return (T)itemValue;
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message); ;
            }
        }

        public static object? GetValueObject(object obj, string propertyName)
        {
            try
            {
                PropertyInfo? propInfo = obj.GetType().GetProperty(propertyName);
                if (propInfo is null) throw new Exception("error propertyName");
                object? itemValue = propInfo.GetValue(obj, null);
                if (itemValue is null) return null;
                return itemValue;
            }
            catch (System.Exception exception)
            {
                throw new Exception(exception.Message); ;
            }
        }

        public static Type? MakeGenericList(Type type)
        {
            try
            {
                Type listGeneric = typeof(List<>);
                Type typeListGeneric = listGeneric.MakeGenericType(type);
                return typeListGeneric;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static object? CallStaticMethod(Type type, bool baseSearch, string func, object[] parm)
        {
            try
            {
                MethodInfo? method = null;
                if (baseSearch == true)
                {
                    if (type.BaseType is not null)
                    {
                        method = type.BaseType.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                    }
                    method = method ?? type.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                }
                else
                {
                    method = type.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                }

                if (method is not null)
                {
                    return method.Invoke(null, parm);
                }

                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        public static T? CallStaticMethod<T>(Type type, bool baseSearch, string func, object[] parm)
        {
            try
            {
                return (T?)(CallStaticMethod(type, baseSearch, func, parm));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T?);
            }
        }

        public static T? CallGenericStaticMethod<T>(Type genericType, Type type, bool baseSearch, string func, object[] parm)
        {
            try
            {
                return (T?)(CallGenericStaticMethod(genericType, type, baseSearch, func, parm));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T?);
            }
        }

        public static object? CallGenericStaticMethod(Type genericType, Type type, bool baseSearch, string func, object[] parm)
        {
            try
            {
                MethodInfo? method = null;
                if (baseSearch == true)
                {
                    if (type.BaseType is not null)
                    {
                        method = type.BaseType.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                    }
                    method = method ?? type.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                }
                else
                {
                    method = type.GetMethod(func, BindingFlags.Public | BindingFlags.Static);
                }

                if (method is not null)
                {
                    MethodInfo genericMethod = method.MakeGenericMethod(genericType);
                    return genericMethod.Invoke(null, parm);
                }

                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        public static object? Instance(Type type)
        {
            try
            {
                object? t = Activator.CreateInstance(type);
                return t;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T? Instance<T>(Type type) where T : class
        {
            try
            {
                object? t = Activator.CreateInstance(type);
                if (t is not null)
                {
                    return t as T;
                }
                throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}