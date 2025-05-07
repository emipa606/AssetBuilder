## Asset Builder for RimWorld


# General usage

Install (Unity 2022.3)[https://unity.com/releases/editor/archive]

Place asset content in Assets/Data in a folder with the mod-identifier as a name

Use the project-script to generate an asset bundle to be placed in your mod

The bundle can include Textures and Sounds but not custom shaders or other advanced types as these require a separate bundle for each operatingsystem (Windows, MacOS, Linux)

To verify that the bundle has the correct files with the correct paths, use a tool like (https://github.com/Perfare/AssetStudio/releases/latest)[Asset Studio]


# Example

Mod has the identifier 'author.modname'

This projects path is c:\Asset Builder

Copy the whole Textures-folder to 'c:\Asset Builder\Assets\Data\author.modname\'

Run Unity in batch mode with:

"C:\Program Files\Unity 2022.3.61f1\Editor\Unity.exe" -batchmode -quit -projectPath "c:\Asset Builder\Assets\Data\author.modname" -executeMethod ModAssetBundleBuilder.BuildBundles

The bundle should then be found in 'c:\Asset Builder\Assets\AssetBundles'

Copy the 'assetBundle' and the 'assetBundle.manifest' files to an AssetBundles-folder in the mod