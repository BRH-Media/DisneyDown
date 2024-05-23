# DisneyDown
### Builds
Download builds from the [Azure DevOps Pipeline](https://dev.azure.com/brhmedia/DisneyDown/_build)

### About
Downloads Disney+ content using a Widevine Hex-formatted key (32-character AES content key).

You need to obtain this key using either using the Chrome WidevineDecryptor plugin or another method. We cannot provide keys; don't bother asking.

### Usage
`DisneyDown.Console.exe -?` for help on using this program. Please always encapsulate manifest URLs in double quotes e.g. `DisneyDown.Console.exe "manifest.m3u8" 3FC0A5E3D357AA1AD16DD8F18F1EB40B -m -d`

### Notes on CDRM Project
Create a file in the program folder called `cdrmToken.txt` and fill it with your account token for Disney+. If you need help finding this, open the network inspector while logged into Disney+ and you will find this token being sent to an endpoint called `/dust` very frequently. **Do not specify a key** if you intend to use CDRM, simply provide the manifest and any desired flags (make sure you use `-m` if you find trouble with very long manifest URLs).
