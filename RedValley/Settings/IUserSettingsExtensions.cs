using System.Text;
using System.Text.Json;
using ColorValley.Settings;
using RedValley.Helper;

namespace RedValley.Settings
{
    /// <summary>
    /// Holds the current user settings
    /// </summary>
    public static class IUserSettingsExtensions
    {
        /// <summary>
        /// Saves the user settings as encrypted file.
        /// </summary>
        /// <param name="filePath">The file path into which the settings should be saved.</param>
        public static void SaveEncrypted<TUserSettings>(this TUserSettings userSettings, string filePath) where TUserSettings : IUserSettings
        {
            var userSettingsJson = JsonSerializer.Serialize<TUserSettings>(userSettings);
            var encryptedText = CryptoHelper.EncryptDataWithAesAsBase64String(userSettingsJson, UserSettings.AesKey, UserSettings.AesIV);
            File.WriteAllText(filePath, encryptedText, Encoding.UTF8);
        }
    }
}