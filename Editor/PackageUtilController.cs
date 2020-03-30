using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Loc = PixelWizards.PackageUtil.PackageUtilLoc;                                 // string localization table

namespace PixelWizards.PackageUtil
{
    public class PackageDependency
    {
        public string packageName;
        public string packageVersion;
    }

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
        public string packageSourcePath;
        public string packageDestinationPath;
        
    }

    public static class PackageUtilController
    {
        public static bool initialized = false;
        private static PackageUtilModel model = new PackageUtilModel();
        public static PackageUtilModel Model { get => model; set => model = value; }

        public static void Init()
        {
            if (initialized)
                return;

            model = new PackageUtilModel();
            
            initialized = true;
        }
        
        public static void ExportPackage()
        {

        }

        public static void AddNewKeyword()
        {
            model.keywords.Add(string.Empty);
        }

        public static void RemoveKeyword(int entry)
        {
            model.keywords.RemoveAt(entry);
        }

        public static void AddNewDependency()
        {
            model.dependencies.Add(new PackageDependency());
        }

        public static void RemoveDependency(int entry)
        {
            model.dependencies.RemoveAt(entry);
        }
    }
}