using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Control = PixelWizards.PackageUtil.PackageUtilController;
using Loc = PixelWizards.PackageUtil.PackageUtilLoc;                                 // string localization table

namespace PixelWizards.PackageUtil
{
    /// <summary>
    /// Localization text for the View
    /// </summary>
    public static class PackageUtilLoc
    {
        public const string MENUITEMPATH = "Window/General/Package Utilities";
        public const string WINDOWTITLE = "Package Utilities Editor";
        
        public const string WINDOW_HEADER = "Utility to help creation of custom packages";
        public const string HELP_HEADER = "Create and manage custom packages.";
        public const string BASIC_INFO = "Basic Info";
        public const string PACKAGE_PATHS = "Package Paths";

        public const string PACKAGE_NAME = "Package Name";
        public const string DISPLAY_NAME = "Display Name";
        public const string PACKAGE_VERSION = "Package Version";
        public const string UNITY_VERSION = "Unity Version";
        public const string PACKAGE_DESCRIPTION = "Description";
        public const string PACKAGE_CATEGORY = "Category";
        public const string KEYWORDS = "Keywords";
        public const string KEYWORD = "Keyword";
        public const string DEPENDENCIES = "Dependencies";
        public const string PACKAGE_SOURCE_PATH = "Package Source Path";
        public const string PACKAGE_DESTINATION_PATH = "Package Destination Path";

        public const string EXPORT_PACKAGE = "Export Package";
    }

    /// <summary>
    /// The View model for the system
    /// </summary>
    public class PackageUtilEditor : EditorWindow
    {
        public static Vector2 curWindowSize = Vector2.zero;
        public static Vector2 minWindowSize = new Vector2(350, 50);
        public static Vector2 scrollPosition = Vector2.zero;
        public static float windowWidth = 0f;
        public static float leftColumnWidth = 150f;
        
        [MenuItem(Loc.MENUITEMPATH)]
        public static void ShowWindow()
        {
            var thisWindow = GetWindow<PackageUtilEditor>(false, Loc.WINDOWTITLE, true);
            thisWindow.minSize = minWindowSize;
            thisWindow.Reset();
        }

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            Control.Init();
        }
        
        
        private void OnGUI()
        {
            windowWidth = position.width;

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Space(10f);

                    GUILayout.Label(Loc.WINDOW_HEADER, EditorStyles.boldLabel);
                    GUILayout.Label(Loc.HELP_HEADER, EditorStyles.helpBox);

                    GUILayout.Space(10f);

                    GUILayout.Label(Loc.BASIC_INFO, EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        GUILayout.Space(5f);
                        GUILayout.BeginVertical();
                        {
                            RenderTextField(Loc.PACKAGE_NAME, ref Control.Model.packageName);
                            RenderTextField(Loc.DISPLAY_NAME, ref Control.Model.displayName);
                            RenderTextField(Loc.PACKAGE_VERSION, ref Control.Model.packageVersion);
                            RenderTextField(Loc.UNITY_VERSION, ref Control.Model.unityVersion);
                            RenderTextField(Loc.PACKAGE_DESCRIPTION, ref Control.Model.description);
                            RenderTextField(Loc.PACKAGE_CATEGORY, ref Control.Model.category);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(10f);

                    GUILayout.Label(Loc.KEYWORDS, EditorStyles.boldLabel);
                    GUILayout.BeginVertical();
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(5f);
                            GUILayout.BeginVertical(GUI.skin.box);
                            {
                                if (Control.Model.keywords.Count > 0)
                                {
                                    for (var i = 0; i < Control.Model.keywords.Count; i++)
                                    {
                                        var keyword = Control.Model.keywords[i];
                                        RenderTextField(Loc.KEYWORD + " " + i, ref keyword);
                                        Control.Model.keywords[i] = keyword;
                                    }
                                }
                                else
                                {
                                    GUILayout.Label("List is Empty");
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(windowWidth - 65f);
                            if (GUILayout.Button("+"))
                            {
                                Control.AddNewKeyword();
                                // add
                            }
                            if (GUILayout.Button("-"))
                            {
                                Control.RemoveKeyword(Control.Model.keywords.Count - 1);
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10f);

                    GUILayout.Label(Loc.DEPENDENCIES, EditorStyles.boldLabel);
                    GUILayout.BeginVertical();
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(5f);
                            GUILayout.BeginVertical(GUI.skin.box);
                            {
                                if (Control.Model.dependencies.Count > 0)
                                {
                                    for (var i = 0; i < Control.Model.dependencies.Count; i++)
                                    {
                                        var dependency = Control.Model.dependencies[i];
                                        GUILayout.BeginHorizontal();
                                        {
                                            GUILayout.Space(10f);
                                            GUILayout.BeginVertical(GUI.skin.box);
                                            {
                                                RenderTextField(Loc.PACKAGE_NAME, ref dependency.packageName);
                                                RenderTextField(Loc.PACKAGE_VERSION, ref dependency.packageVersion);
                                            }
                                            GUILayout.EndVertical();
                                        }
                                        GUILayout.EndHorizontal();

                                        Control.Model.dependencies[i] = dependency;
                                    }
                                }
                                else
                                {
                                    GUILayout.Label("List is Empty");
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(windowWidth - 65f);
                            if (GUILayout.Button("+"))
                            {
                                Control.AddNewDependency();
                                // add
                            }
                            if (GUILayout.Button("-"))
                            {
                                Control.RemoveDependency(Control.Model.dependencies.Count - 1);
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10f);

                    GUILayout.Label(Loc.PACKAGE_PATHS, EditorStyles.boldLabel);

                    GUILayout.BeginVertical();
                    {
                        RenderPathBrowserField(Loc.PACKAGE_SOURCE_PATH, ref Control.packageSourcePath, "Assets");
                        RenderPathBrowserField(Loc.PACKAGE_DESTINATION_PATH, ref Control.packageDestinationPath, "");
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(10f);
                    if (GUILayout.Button(Loc.EXPORT_PACKAGE, GUILayout.Height(35f)))
                    {
                        Control.ExportPackage();
                    }

                }
                GUILayout.EndVertical();
                GUILayout.Space(10f);
            }
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// Helper UI methods
        /// </summary>

        private void RenderTextField( string loc, ref string field)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(loc, GUILayout.Width(leftColumnWidth));
                field = GUILayout.TextField(field, GUILayout.ExpandWidth(true));
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(2f);
        }

        private void RenderPathBrowserField( string loc, ref string field, string startingPath)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(loc, GUILayout.Width(leftColumnWidth));
                field = GUILayout.TextField(field, GUILayout.ExpandWidth(true));
                if( GUILayout.Button("...", GUILayout.Width(35f)))
                {
                    field = EditorUtility.OpenFolderPanel("Browse for Path", startingPath, "");
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(2f);
        }
    }
}