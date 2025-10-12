using YamlDotNet.RepresentationModel;

namespace DevXpert.Modulo3.API.Configurations;

public static class VersionInfo
{
    public static string GetVersionInfo()
    {
        var path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "gitversion.yml");

        using var reader = new StreamReader(path);
        var yaml = new YamlStream();
        yaml.Load(reader);

        var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

        foreach (var entry in mapping.Children)
            if (((YamlScalarNode)entry.Key).Value == "next-version")
                return ((YamlScalarNode)entry.Value).Value;

        return string.Empty;
    }
}
