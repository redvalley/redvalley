using System.Text.Json;
using ColorValley.Settings;
using RedValley.Extensions;
using RedValley.Helper;

namespace RedValley.Settings
{
    /// <summary>
    /// Holds the current user settings
    /// </summary>
    public class UserSettings : IUserSettings
    {
        private readonly string _userSettingsFileName;
        internal const string AesKey = "/Wb/ZrmA/4MyZTkiAPqDdlj+eckptlIXSFL7G3LcPW0=";
        internal const string AesIV = "WPKR4co1m/7NFLIDi2QQqw==";


        /// <summary>
        /// The current user settings file.
        /// </summary>
        public string UserSettingsFileName => _userSettingsFileName;

        /// <summary>
        /// The current user settings file path.
        /// </summary>
        public string UserSettingsFilePath => GetUserSettingsFilePath(_userSettingsFileName);

        /// <summary>
        /// Gets the user settings file path using the file name specified by <paramref name="userSettingsFileName"/>
        /// </summary>
        /// <param name="userSettingsFileName">The file name of the user settings file.</param>
        public static string GetUserSettingsFilePath(string userSettingsFileName)
        {
            return Path.Combine(ContentHelper.ContentFolder, userSettingsFileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings(string userSettingsFileName)
        {
            _userSettingsFileName = userSettingsFileName;
        }

        /// <summary>
        /// Loads the user settings.
        /// </summary>
        /// <param name="filePath">The file path from which the settings should be loaded.</param>
        public static TUserSettings? LoadDecrypted<TUserSettings>(string filePath) where TUserSettings : IUserSettings, new()
        {
            var userSettings = ExceptionHelper.Try("UserSettings.LoadDecrypted", () =>
            {
                if (!File.Exists(filePath))
                {
                    return CreateNewUserSettings<TUserSettings>(filePath);
                }

                var encryptedText = File.ReadAllText(filePath);
                
                if (encryptedText.IsEmpty())
                {
                    return CreateNewUserSettings<TUserSettings>(filePath);
                }

                var decryptedText = CryptoHelper.DecryptBase64TextWithAes(encryptedText, AesKey, AesIV);

                return JsonSerializer.Deserialize<TUserSettings>(decryptedText);
            }, Logging.CreateBootstrappingLogger());
            
            return userSettings??new TUserSettings();
        }

        private static TUserSettings? CreateNewUserSettings<TUserSettings>(string filePath)
            where TUserSettings : IUserSettings, new()
        {
            var newUserSettings = new TUserSettings();

            newUserSettings.SaveEncrypted(filePath);
            return newUserSettings;
        }
    }
}