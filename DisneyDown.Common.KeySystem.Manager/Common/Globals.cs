using DisneyDown.Common.KeySystem.Schemas;
using DisneyDown.Common.KeySystem.Schemas.UserSchema;

namespace DisneyDown.Common.KeySystem.Manager.Common
{
    public static class Globals
    {
        public static Connection KeyServerConnection { get; set; } = new Connection();
        public static User KeyServerUser { get; set; } = new User();
    }
}