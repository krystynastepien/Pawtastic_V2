using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FindUsedInFolder
{
    const string UsedMenu = "Assets/Find Used Textures In Folder";
    const string UnusedMenu = "Assets/Find Unused Textures In Folder";

    [MenuItem(UsedMenu, true)]
    [MenuItem(UnusedMenu, true)]
    static bool Validate()
    {
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return !string.IsNullOrEmpty(path) && AssetDatabase.IsValidFolder(path);
    }

    [MenuItem(UsedMenu)]
    static void ShowUsed() => Run(showUsed: true);

    [MenuItem(UnusedMenu)]
    static void ShowUnused() => Run(showUsed: false);

    static void Run(bool showUsed)
    {
        string folder = AssetDatabase.GetAssetPath(Selection.activeObject);

        string[] rootGuids = AssetDatabase.FindAssets(
            "t:Scene t:Prefab t:Material t:ScriptableObject t:AnimationClip t:AnimatorController",
            new[] { "Assets" });

        // texture path -> list of assets that reference it
        var usedBy = new Dictionary<string, List<string>>();

        for (int i = 0; i < rootGuids.Length; i++)
        {
            string root = AssetDatabase.GUIDToAssetPath(rootGuids[i]);
            EditorUtility.DisplayProgressBar("Scanning references",
                root, (float)i / rootGuids.Length);

            foreach (var dep in AssetDatabase.GetDependencies(root, true))
            {
                if (dep == root) continue;
                if (!usedBy.TryGetValue(dep, out var list))
                    usedBy[dep] = list = new List<string>();
                list.Add(root);
            }
        }
        EditorUtility.ClearProgressBar();

        var textures = AssetDatabase.FindAssets("t:Texture2D", new[] { folder })
            .Select(AssetDatabase.GUIDToAssetPath)
            .ToList();

        var used = textures.Where(usedBy.ContainsKey).ToList();
        var unused = textures.Where(t => !usedBy.ContainsKey(t)).ToList();

        if (showUsed)
        {
            var lines = used.Select(t =>
            {
                var refs = usedBy[t].Distinct().Take(5).ToList();
                string more = usedBy[t].Distinct().Count() > 5 ? ", ..." : "";
                return $"{t}\n    used by: {string.Join(", ", refs)}{more}";
            });

            Debug.Log($"[{folder}] {used.Count} used of {textures.Count} textures:\n"
                      + string.Join("\n", lines));
        }
        else
        {
            Debug.Log($"[{folder}] {unused.Count} unused of {textures.Count} textures:\n"
                      + string.Join("\n", unused));
        }

        // Select the result in the Project window
        var target = showUsed ? used : unused;
        Selection.objects = target
            .Select(p => AssetDatabase.LoadAssetAtPath<Object>(p))
            .ToArray();
    }
}