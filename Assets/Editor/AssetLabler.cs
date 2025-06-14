using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetLabeler
{
    // The folder that holds your mod textures.
    private static readonly string assetsFolder = "Assets/Data";

    /// <summary>
    ///     Converts a texture asset from Sprite to Default to prevent Unity from generating sprite sub-assets.
    /// </summary>
    /// <param name="assetPath">The path to the texture asset.</param>
    private static void ConvertSpriteToDefault(string assetPath)
    {
        if (AssetImporter.GetAtPath(assetPath) is not TextureImporter
            {
                textureType: TextureImporterType.Sprite
            } importer)
        {
            return;
        }

        importer.textureType = TextureImporterType.Default;
        importer.SaveAndReimport();
        Debug.Log($"Converted {assetPath} from Sprite to Default.");
    }

    /// <summary>
    ///     Labels all assets  with a single common asset bundle name.
    /// </summary>
    /// <returns>The number of textures labeled.</returns>
    public static int LabelAllAssetsWithCommonName(string assetFileName)
    {
        if (!Directory.Exists(assetsFolder))
        {
            Debug.LogError($"Folder not found: {assetsFolder}");
            return 0;
        }

        // Get all the files under modTexturesFolder (and its subdirectories).
        var filePaths = Directory.GetFiles(assetsFolder, "*.*", SearchOption.AllDirectories);
        var assetsLabeled = 0;

        foreach (var filePath in filePaths)
        {
            // Normalize the path format.
            var assetPath = filePath.Replace("\\", "/");
            var extension = Path.GetExtension(assetPath).ToLower();

            // Process only common texture and audio file types .
            if (extension != ".png" && extension != ".jpeg" && extension != ".jpg" && extension != ".psd" &&
                extension != ".wav" && extension != ".mp3" && extension != ".ogg")
            {
                continue;
            }

            // Confirm that the asset is located under the Assets folder.
            if (!assetPath.StartsWith("Assets"))
            {
                continue;
            }

            // Convert Sprite textures to Default to avoid additional sprite sub-assets.
            ConvertSpriteToDefault(assetPath);

            // Set a common asset bundle name for every texture.
            var importer = AssetImporter.GetAtPath(assetPath);
            if (importer is null)
            {
                Debug.LogWarning($"Could not get importer for: {assetPath}");
                continue;
            }

            importer.assetBundleName = assetFileName;
            assetsLabeled++;
            Debug.Log($"Labeled asset: {assetPath} as {assetFileName}");
        }

        Debug.Log($"Labeling complete: {assetsLabeled} assets labeled with \"{assetFileName}\".");
        return assetsLabeled;
    }

    // For manual testing from the Editor.
    [MenuItem("Assets/Label All Assets")]
    public static void Menu_LabelAllTexturesWithCommonName()
    {
        LabelAllAssetsWithCommonName("DefaultAssetBundleName");
    }
}