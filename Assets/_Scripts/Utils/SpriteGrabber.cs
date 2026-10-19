using UnityEngine;
using Pawtastic.Utils.Enums;

namespace Pawtastic.Utils
{
    public static class SpriteGrabber
    {
        #region CONST - Sprite paths
        private const string HEART_SPRITE_PATH = "UI_Assets/HUD";
        private const string ITEM_SPRITE_PATH = "UI_Assets/HUD";
        #endregion

        #region  Sprite Loaders
        /// <summary>
        /// Returns heart sprite for HUD UI Life board
        /// </summary>
        /// <param name="state">heart state enum</param>
        /// <returns></returns>
        public static Sprite LoadHeartSprite(HUD_HeartState state)
        {
            Sprite spriteObj;
            string spriteName = GetHeartSpriteName(state);

            spriteObj = Resources.Load<Sprite>(HEART_SPRITE_PATH + "/" + spriteName);
            if (spriteObj == null)
            {
                GameLogger.LogError("[SpriteGrabber][LoadHeartSprite(HeartState state)] The sprite cannot be found. Path is wrong or the sprite doesnt exist", LoggerConfig.LogCategoryFlags.UI);
            }

            return spriteObj;
        }

        /// <summary>
        /// Returns item sprite for HUD UI Inventory
        /// </summary>
        /// <param name="state">heart state enum</param>
        /// <returns></returns>
        public static Sprite LoadItemSprite(Items state)
        {
            Sprite spriteObj;
            string spriteName = GetItemSpriteName(state);

            spriteObj = Resources.Load<Sprite>(ITEM_SPRITE_PATH + "/" + spriteName);
            if (spriteObj == null)
            {
                GameLogger.LogError("[SpriteGrabber][LoadItemSprite] The sprite cannot be found. Path is wrong or the sprite doesnt exist", LoggerConfig.LogCategoryFlags.UI);
            }

            return spriteObj;
        }
        #endregion


        #region  Sprite Name Getters
        /// <summary>
        /// Gets name of Heart sprite from HUD health bar depending on which state is needed
        /// </summary>
        /// <param name="state">heart state enum</param>
        /// <returns></returns>
        private static string GetHeartSpriteName(HUD_HeartState state)
        {
            string spriteName = "";

            switch (state)
            {
                case HUD_HeartState.Full:
                    spriteName = "heart_full";
                    break;
                case HUD_HeartState.Empty:
                    spriteName = "heart_outline";
                    break;
                case HUD_HeartState.Broken:
                    spriteName = "heart_broken";
                    break;

                    // default:
                    //     spriteName = ""; // TODO: make default wrong SPRITE LOADER
                    //     break;
            }

            if (string.IsNullOrEmpty(spriteName))
            {
                GameLogger.LogError("[SpriteGrabber] Can't find the HeartState", LoggerConfig.LogCategoryFlags.UI);
            }

            return spriteName;
        }

        /// <summary>
        /// Gets name of Heart sprite from HUD health bar depending on which state is needed
        /// </summary>
        /// <param name="item">heart state enum</param>
        /// <returns></returns>
        private static string GetItemSpriteName(Items item)
        {
            string spriteName = "";

            switch (item)
            {
                case Items.Fish:
                    spriteName = "Fish";
                    break;
                case Items.Apple:
                    spriteName = "Apple";
                    break;
                case Items.Badge:
                    spriteName = "Badge";
                    break;
                case Items.Feather:
                    spriteName = "Feather";
                    break;
                case Items.Helmet:
                    spriteName = "Helmet";
                    break;
                case Items.Key:
                    spriteName = "Key2";
                    break;
                case Items.Mask:
                    spriteName = "Mask";
                    break;
                case Items.Potion:
                    spriteName = "Potion";
                    break;

                    // default:
                    //     spriteName = ""; // TODO: make default wrong SPRITE LOADER
                    //     break;
            }

            if (string.IsNullOrEmpty(spriteName))
            {
                GameLogger.LogError("[SpriteGrabber] Can't find the HeartState", LoggerConfig.LogCategoryFlags.UI);
            }

            return spriteName;
        }
        #endregion


        #region Enums
        public enum HUD_HeartState
        {
            Full,
            Empty,
            Broken
        }
        #endregion
    }
}