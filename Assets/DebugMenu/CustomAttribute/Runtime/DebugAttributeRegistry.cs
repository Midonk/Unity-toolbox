using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DebugMenu.CustomAttribute.Runtime
{
    public class DebugAttributeRegistry
    {
        #region Main

        public static void ValidateMethods()
        {
            InitializeDictionnary();
        }


        public static void InvokeMethod(string path)
        {
            if (!Methods.ContainsKey(path)) return;

            var method = Methods[path];
            //Debug.Log(method.);
            try
            {
                method.Invoke(method.ReflectedType, null);
            }

            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }

        public static ReturnType InvokeMethod<ReturnType>(string path)
        {
            if (!Methods.ContainsKey(path) || Methods[path].IsPrivate) return default(ReturnType);

            var result = default(ReturnType);
            var method = Methods[path];

            try
            {
                result = (ReturnType)method.Invoke(method.ReflectedType, new object[0]);
            }

            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return result;
        }

        public static void InvokeMethod(string path, object[] parameters)
        {
            if (!Methods.ContainsKey(path) || Methods[path].IsPrivate) return;

            var method = Methods[path];
            try
            {
                method.Invoke(method.ReflectedType, parameters);
            }

            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }

        public static ReturnType InvokeMethod<ReturnType>(string path, object[] parameters)
        {
            if (!Methods.ContainsKey(path) || Methods[path].IsPrivate) return default(ReturnType);

            var result = default(ReturnType);
            var method = Methods[path];
            try
            {
                result = (ReturnType)method.Invoke(method.ReflectedType, parameters);
            }
            
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return result;
        }

        public static string[] GetQuickPaths()
        {
            var result = new List<string>();
            foreach (var method in Methods)
            {
                if (method.Value.GetCustomAttribute<DebugMenuAttribute>().IsQuickMenu)
                {
                    result.Add(method.Key);
                }
            }

            return result.ToArray();
        }

        #endregion


        #region Utils

        private static void InitializeDictionnary()
        {
            _methods = new MergeableDictionary<string, MethodInfo>();

            for (int i = 0; i < Assemblies.Length; i++)
            {
                var assembly = Assemblies[i];
                var assemblyTypes = assembly.GetTypes();
                var methods = assemblyTypes.SelectMany(classType => classType.GetMethods())
                                           .Where(classMethod => classMethod.GetCustomAttributes()
                                                                            .OfType<DebugMenuAttribute>()
                                                                            .Any() && !classMethod.IsPrivate);
                var methodDictionary = methods.ToDictionary(methodInfo => methodInfo.GetCustomAttributes()
                                                                                    .OfType<DebugMenuAttribute>()
                                                                                    .FirstOrDefault<DebugMenuAttribute>()
                                                                                    .Path);

                if (methodDictionary != null)
                {
                    _methods.Merge(methodDictionary);
                }
            }
        }

        #endregion


        #region Private

        private static Assembly[] _assemblies;
        private static Assembly[] Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    _assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                }

                return _assemblies;
            }
        }

        private static MergeableDictionary<string, MethodInfo> _methods;
        private static MergeableDictionary<string, MethodInfo> Methods
        {
            get
            {
                if(_methods == null)
                {
                    InitializeDictionnary();
                }

                return _methods;
            }
        }

        public static string[] Paths => Methods.Keys.ToArray();

        #endregion
    
        private class PathBinding<T>
        {
            public string Paths { get; set; }
            public T Instances { get; set; }
        }
    }
}