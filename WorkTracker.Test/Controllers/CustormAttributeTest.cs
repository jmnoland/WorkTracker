using NUnit.Framework;
using WorkTracker.Controllers.Attributes;
using WorkTracker.Models;

namespace WorkTracker.Test.Controllers
{
    public class CustormAttributeTest
    {
        private readonly ValidateTokenAttribute _validateTokenAttribute;
        private readonly AppSettings _appSettings;
        private readonly string _token;
        public CustormAttributeTest()
        {
            _validateTokenAttribute = new ValidateTokenAttribute();
            _appSettings = Helper.getAppSettings();
            var permissions = new string[] { "create_story", "create_user", "view_story", "edit_story" };
            _token = Services.Helper.GenerateToken(0, permissions, _appSettings.JwtSecret);
        }

        [Test]
        public void PermissionAllowed_Successful()
        {
            var result = _validateTokenAttribute.PermissionAllowed(_token, "create_user");
            Assert.IsTrue(result);
        }

        [Test]
        public void PermissionAllowed_InValid_Permission()
        {
            var result = _validateTokenAttribute.PermissionAllowed(_token, "");
            Assert.IsFalse(result);
        }

        [Test]
        public void PermissionAllowed_Empty_Token()
        {
            var result = _validateTokenAttribute.PermissionAllowed("", "");
            Assert.IsFalse(result);
        }

        [Test]
        public void PermissionAllowed_Null_Token()
        {
            var result = _validateTokenAttribute.PermissionAllowed(null, "");
            Assert.IsFalse(result);
        }

        [Test]
        public void PermissionAllowed_InValid_Token()
        {
            var result = _validateTokenAttribute.PermissionAllowed("test", "");
            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateCurrentToken_Successful()
        {
            var result = _validateTokenAttribute.ValidateCurrentToken(_token);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateCurrentToken_Invalid_Token()
        {
            var result = _validateTokenAttribute.ValidateCurrentToken(null);
            Assert.IsFalse(result);
        }
    }
}
