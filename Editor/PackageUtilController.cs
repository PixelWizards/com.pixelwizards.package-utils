using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace PixelWizards.PackageUtil
{
    /// <summary>
    /// A single entry of our package dependencies
    /// </summary>
    public class PackageDependency
    {
        public string name;
        public string version;
    }

    /// <summary>
    /// Wrapper for the dependencies so we can serialize the list properly
    /// </summary>
    [JsonConverter(typeof(DependencySerializer))]
    public class DependencyList
    {
        public List<PackageDependency> entries = new List<PackageDependency>();
    }

    /// <summary>
    /// Author data for the manifest
    /// </summary>
    public class Author
    {
        public string name = "Pixel Wizards";
        public string email = "support@pixelwizards.ca";
        public string url = "www.pixelwizards.ca";
    }

    /// <summary>
    /// Json serializable package manifest
    /// </summary>
    public class PackageUtilModel
    {
        public string name = "com.mycompany.mypackage";
        public string displayName;
        public string version = "0.1.0-preview.1";
        public string unity = "2018.4";
        public string description;
        public string category;
        public Author author = new Author();
        public List<string> keywords = new List<string>();
        public DependencyList dependencies = new DependencyList();
    }

    /// <summary>
    /// Our controller, manages the data model and handles the actual publishing of the package
    /// </summary>
    public static class PackageUtilController
    {
        private static PackageUtilModel model = new PackageUtilModel();
        public static PackageUtilModel Model { get => model; set => model = value; }
        public static string packageSourcePath;
        public static string packageDestinationPath;

        public static StringBuilder outputLog = new StringBuilder();

        /// <summary>
        /// When the UI window opens we initalize ourself
        /// </summary>
        public static void Init()
        {
            model = new PackageUtilModel();
            packageSourcePath = string.Empty;
            packageDestinationPath = string.Empty;
            outputLog.Clear();
        }

        /// <summary>
        /// Do the actual export / copy of the package
        /// </summary>
        public static void ExportPackage()
        {
            outputLog.Clear();
            // validate package info

            outputLog.AppendLine("VALIDATING PACKAGE DETAILS");
            if(! ValidatePackage())
            {
                outputLog.AppendLine("Validation failed, see error log above");
            }

            outputLog.AppendLine("EXPORTING PACKAGE...");
            outputLog.AppendLine("");
            outputLog.AppendLine("Source folder: " + packageSourcePath);
            outputLog.AppendLine("Destination folder: " + packageDestinationPath);
            outputLog.AppendLine("");
            outputLog.AppendLine("Package Manifest:");
            outputLog.AppendLine("-----------------");
            outputLog.AppendLine("");

            var manifest = JsonConvert.SerializeObject(Model, Formatting.Indented);
            outputLog.Append(manifest);

            outputLog.AppendLine("");
            outputLog.AppendLine("-----------------");
            outputLog.AppendLine("Copying files....");

            var outputPath = packageDestinationPath + "/" + Model.name;

            if (Directory.Exists(packageDestinationPath))
            {
                if (!Directory.Exists(outputPath))
                {
                    // create the output directory
                    Directory.CreateDirectory(outputPath);
                }
                else
                {
                    outputLog.AppendLine("Output directory already exists, canceling operation... " + outputPath);
                    return;
                }
            }
            else
            {
                outputLog.AppendLine("Error creating output directory: " + outputPath);
                return;
            }

            // do the copy
            DirectoryCopy(packageSourcePath, outputPath, true);

            // write the manifest to the new folder
            outputLog.AppendLine("");
            outputLog.AppendLine("Writing Manifest...");
            
            if( !Directory.Exists(outputPath))
            {
                outputLog.AppendLine("Error: output directory doesn't exist?");
                return;
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(outputPath, "package.json")))
            {
                outputFile.Write(manifest);
            }

            // write the manifest to the new folder
            outputLog.AppendLine("");
            outputLog.AppendLine("Package Export complete!");
        }

        private static bool ValidatePackage()
        {
            var success = true;

            if (string.IsNullOrEmpty(Model.name))
                success = false;
            if (string.IsNullOrEmpty(Model.displayName))
                success = false;
            if (string.IsNullOrEmpty(packageDestinationPath))
            {
                success = false;
            }

            return success;
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
            if( model.keywords.Count > 0)
            {
                if( model.keywords[entry] != null)
                {
                    model.keywords.RemoveAt(entry);
                }
            }
        }

        /// <summary>
        /// Add a new dependency to our package
        /// </summary>
        public static void AddNewDependency()
        {
            model.dependencies.entries.Add(new PackageDependency());
        }

        /// <summary>
        /// Remove the dependency at a given entry from our package
        /// </summary>
        /// <param name="entry"></param>
        public static void RemoveDependency(int entry)
        {
            if (model.dependencies.entries.Count > 0)
            {
                if (model.dependencies.entries[entry] != null)
                {
                    model.dependencies.entries.RemoveAt(entry);
                }
            }
        }


        /// <summary>
        /// Directory copy method, blatantly stolen from: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}