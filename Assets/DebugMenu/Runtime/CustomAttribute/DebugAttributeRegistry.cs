using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DebugMenu
{
    public class DebugAttributeRegistry
    {
        #region Main

        /// <summary>
        ///     Invoke the specific method attached to a path
        /// <summary>
        public static void InvokeMethod(string path, object[] parameters = null)
        {
            if (!Methods.ContainsKey(path)) return;

            var method = Methods[path];
            Type type = method.ReflectedType;
            var instances = GameObject.FindObjectsOfType(type);

            foreach (var instance in instances)
            {
                method.Invoke(instance, parameters);
            }
        }

        /// <summary>
        ///     Invoke the specific method attached to a path
        /// <summary>
        public static T InvokeMethod<T>(string path, object[] parameters = null)
        {
            if (!Methods.ContainsKey(path)) return default(T);

            var result = default(T);
            var method = Methods[path];
            Type type = method.ReflectedType;
            var instances = GameObject.FindObjectsOfType(type);

            for (int i = 0; i < instances.Length; i++)
            {
                result = (T)method.Invoke(method.ReflectedType, parameters);
            }

            return result;
        }

        /// <summary>
        ///     Initialize the debug registry by fetching the attribute references
        /// <summary>
        public static void Initialize()
        {
            _methods = new MergeableDictionary<string, MethodInfo>();

            for (int i = 0; i < Assemblies.Length; i++)
            {
                var assembly = Assemblies[i];
                var assemblyTypes = assembly.GetTypes();
                var methods = assemblyTypes.SelectMany(classType => classType.GetMethods())
                                           .Where(classMethod => classMethod.GetCustomAttributes(typeof(DebugMenuAttribute), true)
                                                                            .Any() 
                                                                            && !classMethod.IsPrivate 
                                                                            && classMethod.GetParameters().Length < 2);
                                                                            
                var methodDictionary = methods.ToDictionary(methodInfo => methodInfo.GetCustomAttribute<DebugMenuAttribute>()
                                                                                    .Path);

                if (methodDictionary is null) return;
                
                _methods.Merge(methodDictionary);
            }
        }

        #endregion


        #region Utils

        /// <summary>
        ///     Retreive paths marked as quick paths
        /// <summary>
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

        public static Type GetParameterType(string methodPath)
        {
            var method = Methods[methodPath];
            var parameters = method.GetParameters();
            if(parameters.Length == 0) return null;
            return parameters[0].ParameterType;
        }

        public static DebugMenuAttribute GetAttribute(string methodPath)
        {
            var method = Methods[methodPath];
            return method.GetCustomAttribute<DebugMenuAttribute>();
        }

        public static bool HasKey(string path) => _methods.ContainsKey(path);

        #endregion


        #region Private Properties

        private static Assembly[] Assemblies
        {
            get
            {
                if (_assemblies is null)
                {
                    _assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                }

                return _assemblies;
            }
        }

        private static MergeableDictionary<string, MethodInfo> Methods
        {
            get
            {
                if(_methods is null)
                {
                    Initialize();
                }

                return _methods;
            }
        }

        public static string[] Paths => Methods.Keys.ToArray();
             
        #endregion


        #region Private Fields

        private static Assembly[] _assemblies;
        private static MergeableDictionary<string, MethodInfo> _methods;

        #endregion
    }
}