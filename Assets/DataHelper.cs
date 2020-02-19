﻿using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MHW_Editor.Models;
using MHW_Template;
using Newtonsoft.Json;

namespace MHW_Editor.Assets {
    public static class DataHelper {
        public static readonly Dictionary<string, Dictionary<ushort, IdNamePair>> itemData = new Dictionary<string, Dictionary<ushort, IdNamePair>>();
        public static readonly Dictionary<string, Dictionary<ushort, IdNamePair>> skillData = new Dictionary<string, Dictionary<ushort, IdNamePair>>();
        public static readonly Dictionary<string, Dictionary<ushort, string>> skillDataDescriptions = new Dictionary<string, Dictionary<ushort, string>>();
        public static readonly Dictionary<string, Dictionary<ushort, string>> armorData = new Dictionary<string, Dictionary<ushort, string>>();
        public static readonly Dictionary<string, Dictionary<string, Dictionary<uint, string>>> weaponData = new Dictionary<string, Dictionary<string, Dictionary<uint, string>>>();
        public static readonly Dictionary<string, Dictionary<ushort, string>> otomoArmorData = new Dictionary<string, Dictionary<ushort, string>>();
        public static readonly Dictionary<string, Dictionary<ushort, string>> otomoWeaponData = new Dictionary<string, Dictionary<ushort, string>>();
        public static readonly Dictionary<string, Dictionary<ushort, IdNamePair>> songData = new Dictionary<string, Dictionary<ushort, IdNamePair>>();
        public static readonly Dictionary<string, Dictionary<byte, string>> insectData = new Dictionary<string, Dictionary<byte, string>>();
        public static readonly Dictionary<string, Dictionary<uint, string>> bountyData = new Dictionary<string, Dictionary<uint, string>>();
        public static readonly Dictionary<string, Dictionary<uint, string>> bountyDataDescriptions = new Dictionary<string, Dictionary<uint, string>>();

        private static readonly Regex songMatch = new Regex("^HUD_HUE_(\\d+)$");

        static DataHelper() {
            foreach (var lang in Global.LANGUAGES) {
                ParseItemData(lang);
                ParseSkillData(lang);
                ParseSongData(lang);
                ParseBountyData(lang);

                armorData[lang] = LoadDict<ushort, string>(GetAsset($"{lang}_armorData"));
                otomoArmorData[lang] = LoadDict<ushort, string>(GetAsset($"{lang}_otomo_armorData"));
                otomoWeaponData[lang] = LoadDict<ushort, string>(GetAsset($"{lang}_otomo_weaponData"));
                insectData[lang] = LoadDict<byte, string>(GetAsset($"{lang}_insectData"));

                weaponData[lang] = new Dictionary<string, Dictionary<uint, string>>();
                foreach (var weapon in Global.WEAPONS) {
                    weaponData[lang][weapon] = LoadDict<uint, string>(GetAsset($"{lang}_{weapon}"));
                }
            }
        }

        private static void ParseItemData(string lang) {
            itemData[lang] = new Dictionary<ushort, IdNamePair>();

            var rawItemData = LoadDict<uint, string>(GetAsset($"{lang}_itemData"));
            rawItemData[0] = "--------";

            const uint step = 2;
            for (uint index = 0; index < rawItemData.Count; index += step) {
                var key = (ushort) (index / step);
                var value = new IdNamePair(key, rawItemData[index]);

                itemData[lang][key] = value;
            }
        }

        private static void ParseSkillData(string lang) {
            skillData[lang] = new Dictionary<ushort, IdNamePair>();
            skillDataDescriptions[lang] = new Dictionary<ushort, string>();

            var rawSkillData = LoadDict<uint, string>(GetAsset($"{lang}_skillData"));
            rawSkillData[0] = "--------";

            const uint step = 3;
            for (uint index = 0; index < rawSkillData.Count; index += step) {
                var key = (ushort) (index / step);
                var value = new IdNamePair(key, rawSkillData[index]);
                var desc = rawSkillData[index + 2].Replace("\r\n", " ");

                skillData[lang][key] = value;
                skillDataDescriptions[lang][key] = desc;
            }
        }

        private static void ParseSongData(string lang) {
            var rawSongData = LoadDict<ushort, List<string>>(GetAsset($"{lang}_musicSkillData"));

            songData[lang] = new Dictionary<ushort, IdNamePair>();

            foreach (var pair in rawSongData) {
                var match = songMatch.Match(pair.Value[0]);
                if (!match.Success) continue;
                var key = ushort.Parse(match.Captures[0].Value.Replace("HUD_HUE_", ""));
                if (key >= 17) key++;

                songData[lang][key] = new IdNamePair(key, pair.Value[1]);
            }
        }

        private static void ParseBountyData(string lang) {
            bountyData[lang] = new Dictionary<uint, string>();
            bountyDataDescriptions[lang] = new Dictionary<uint, string>();

            var rawItemData = LoadDict<uint, string>(GetAsset($"{lang}_bountyData"));

            const uint step = 2;
            for (uint index = 0; index < rawItemData.Count; index += step) {
                var key = index / step;

                bountyData[lang][key] = rawItemData[index];
                bountyDataDescriptions[lang][key] = rawItemData[index + 1];
            }
        }

        private static Dictionary<K, V> LoadDict<K, V>(byte[] data) {
            var json = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<Dictionary<K, V>>(json);
        }

        private static byte[] GetAsset(string assetName) {
            return (byte[]) Assets.ResourceManager.GetObject(assetName);
        }
    }
}