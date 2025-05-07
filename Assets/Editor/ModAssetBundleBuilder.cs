using UnityEditor;
using UnityEngine;

public class ModAssetBundleBuilder
{
    private static readonly string outputDirectoryRoot = "Assets/AssetBundles";

    [MenuItem("Assets/Build Compressed Asset Bundle (LZ4)")]
    public static void BuildBundles()
    {
        // Ensure textures are labeled correctly before proceeding.
        var count = AssetLabeler.LabelAllAssetsWithCommonName();
        if (count == 0)
        {
            Debug.LogError("No assets were labeled; aborting asset bundle build.");
            return;
        }

        // Since the bundle only includes generic assets like textures or sounds
        // and not platform-specific assets, we can build for all platforms.
        Debug.Log("Building asset bundle.");

        // Build the asset bundles with LZ4 compression.
        BuildPipeline.BuildAssetBundles(
            outputDirectoryRoot,
            BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64
        );

        Debug.Log("Asset bundles built successfully.");
    }
}