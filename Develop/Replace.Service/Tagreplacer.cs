using System.Collections.Generic;
using Replace.Service.DataModel;

namespace Replace.Service
{
    public class TagReplacer
    {
        public static ReplaceConfig ReplaceTags(Config config)
        {

            ReplaceConfig replacedConfig = new ReplaceConfig();
            replacedConfig.FileExtensions = new List<string>();
            replacedConfig.RegexReplaceValues = new List<RegexReplaceValue>();

            foreach (RegexReplaceValue value in config.RegexReplaceValues)
            {
                string regex = ReplaceValueTag(config, value.Regex);
                string replaceValue = ReplaceValueTag(config, value.ReplaceValue);
                replacedConfig.RegexReplaceValues.Add(new RegexReplaceValue
                {
                    Regex = regex,
                    ReplaceValue = replaceValue
                });
            }

            for (int i = 0; i < config.FileNames.Count; i++)
            {
                replacedConfig.FileExtensions.Add(ReplaceValueTag(config, config.FileNames[i]));
            }

            replacedConfig.PathToSearch = ReplaceValueTag(config, config.PathToSearch);

            return replacedConfig;
        }

        private static string ReplaceValueTag(Config config, string value)
        {
            if (config.TagReplacements == null || string.IsNullOrEmpty(value))
            {
                return value;
            }

            foreach (KeyValuePair<string, string> keyValuePair in config.TagReplacements)
            {
                if (value.Contains(keyValuePair.Key))
                {
                    value = value.Replace(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return value;
        }
    }
}
