using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Loc = PixelWizards.PackageUtil.PackageUtilLoc;                                 // string localization table

namespace PixelWizards.PackageUtil
{
    /// <summary>
    /// A single entry of our package dependencies
    /// </summary>
    public class PackageDependency
    {
        public string packageName;
        public string packageVersion;
    }

    /// <summary>
    /// Json serializable package manifest
    /// </summary>
    public class PackageUtilModel
    {
        public string packageName;
        public string displayName;
        public string packageVersion;
        public string unityVersion;
        public string description;
        public string category;
        public List<string> keywords = new List<string>();
        public List<PackageDependency> dependencies = new List<PackageDependency>();
    }

    /// <summary>
    /// Our controller, manages the data model and handles the actual publishing of the package
    /// </summary>
    public static class PackageUtilController
    {
        public static bool initialized = false;
        private static PackageUtilModel model = new PackageUtilModel();
        public static PackageUtilModel Model { get => model; set => model = value; }
        public static string packageSourcePath;
        public static string packageDestinationPath;

        /// <summary>
        /// When the UI window opens we initalize ourself
        /// </summary>
        public static void Init()
        {
            if (initialized)
                return;

            model = new PackageUtilModel();
            
            initialized = true;
        }

        /// <summary>
        /// Do the actual export / copy of the package
        /// </summary>
        public static void ExportPackage()
        {

        }

        /// <summary>
        /// Add a new keyword to our package
        /// </summary>
        public static void AddNewKeyword()
        {
            model.keywords.Add(string.Empty);
        }

        /// <summary>
        /// Remove the keyword at the given entry from our package
        /// </summary>
        /// <param name="entry"></param>
        public static void RemoveKeyword(int entry)
        {
            model.keywords.RemoveAt(entry);
        }

        /// <summary>
        /// Add a new dependency to our package
        /// </summary>
        public static void AddNewDependency()
        {
            model.dependencies.Add(new PackageDependency());
        }

        /// <summary>
        /// Remove the dependency at a given entry from our package
        /// </summary>
        /// <param name="entry"></param>
        public static void RemoveDependency(int entry)
        {
            model.dependencies.RemoveAt(entry);
        }
    }
}