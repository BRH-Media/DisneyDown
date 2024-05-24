using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DisneyDown.Common.KeySystem.Widevine
{
    public static class WidevineDeviceManager
    {
        /// <summary>
        /// Current directory of the Widevine library module
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string WorkingDir => $@"{AssemblyDirectory}\cfg\cdm";
        public static string DeviceIdBlobFile => $@"{WorkingDir}\client_id.bin";
        public static string DeviceKeyFile => $@"{WorkingDir}\private_key.pem";

        private static void EnsureWorkingDir()
        {
            if (!Directory.Exists(WorkingDir))
            {
                Directory.CreateDirectory(WorkingDir);
            }
        }

        private static byte[] GetFileSafe(string fileName)
        {
            try
            {
                EnsureWorkingDir();
                if (File.Exists(fileName))
                {
                    return File.ReadAllBytes(fileName);
                }
            }
            catch
            {
                //nothing
            }

            //default
            return null;
        }

        public static byte[] GetDeviceIdBlob()
            => GetFileSafe(DeviceIdBlobFile);

        public static string GetDeviceIdBlobB64()
            => GetDeviceIdBlob()?.Length > 0 ? Convert.ToBase64String(GetDeviceIdBlob()) : null;

        public static byte[] GetDeviceKey()
            => GetFileSafe(DeviceKeyFile);

        public static string GetDeviceKeyB64()
            => GetDeviceKey()?.Length > 0 ? Convert.ToBase64String(GetDeviceKey()) : null;
    }
}