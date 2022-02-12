using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TF.DebugMenu.Attributes;

namespace TF.DebugMenu.Core
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
            //could register the passed value here

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
        /// Initialize the debug registry by fetching the attribute references
        /// <summary>
        public static void RetreivePaths()
        {
            for (int i = 0; i < Assemblies.Length; i++)
            {
                var assembly = Assemblies[i];
                var assemblyTypes = assembly.GetTypes();
                var methods = assemblyTypes.SelectMany(classType => classType.GetMethods())
                                           .Where(classMethod => classMethod.GetCustomAttributes(typeof(DebugMenuAttribute), true)
                                                                            .Any() 
                                                                            && !classMethod.IsPrivate 
                                                                            && classMethod.GetParameters().Length < 2);//may be extended later
                                                                            
                var methodDictionary = methods.ToDictionary(methodInfo => methodInfo.GetCustomAttribute<DebugMenuAttribute>()
                                                                                    .Path);

                foreach (var item in methodDictionary)
                {
                    if(Methods.ContainsKey(item.Key)) continue;
                    
                    Methods.Add(item.Key, item.Value);
                }
            }
        }

        #endregion


        #region Utils

        /// <summary>
        /// Retreive paths marked as quick paths
        /// <summary>
        public static string[] GetQuickPaths()
        {
            var result = new List<string>();
            foreach (var method in Methods)
            {
                if (!method.Value.GetCustomAttribute<DebugMenuAttribute>().IsQuickMenu) continue;
                
                result.Add(method.Key);
            }

            return result.ToArray();
        }

        /* public static Type GetParameterType(string methodPath)
        {
            var method = Methods[methodPath];
            var parameters = method.GetParameters();
            if(parameters.Length == 0) return null;
            return parameters[0].ParameterType;
        } */

        public static DebugMenuAttribute GetAttribute(string methodPath)
        {
            var method = Methods[methodPath];
            return method.GetCustomAttribute<DebugMenuAttribute>();
        }

        public static bool HasKey(string path) => Methods.ContainsKey(path);

        public static string GetMethodName(string methodPath)
        {
            return _methods[methodPath].Name;
        }

        #endregion


        #region Private Properties

        public static Assembly[] Assemblies
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

        private static Dictionary<string, MethodInfo> Methods
        {
            get
            {
                if(_methods is null)
                {
                    _methods = new Dictionary<string, MethodInfo>();
                    RetreivePaths();
                }

                return _methods;
            }
        }

        public static string[] Paths => Methods.Keys.ToArray();
             
        #endregion


        #region Private Fields

        private static Assembly[] _assemblies;
        private static Dictionary<string, MethodInfo> _methods;

        #endregion
    }
}