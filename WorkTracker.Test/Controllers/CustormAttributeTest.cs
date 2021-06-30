using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Controllers.Attributes;

namespace WorkTracker.Test.Controllers
{
    public class CustormAttributeTest
    {
        private readonly ValidateTokenAttribute _validateTokenAttribute;
        private readonly string _token;
        public CustormAttributeTest()
        {
            _validateTokenAttribute = new ValidateTokenAttribute();
            // Todo: fetch from db for tests
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyMiIsIlVzZXJSb2xlIjpbImNyZWF0ZV91c2VyIiwiZGVsZXRlX3VzZXIiLCJjcmVhdGVfc3RvcnkiLCJ2aWV3X3N0b3J5Il0sIm5iZiI6MTYyNTA4Njk2MSwiZXhwIjoxNjI1MDg4NzYxLCJpYXQiOjE2MjUwODY5NjF9.rMSgeQN9MQdkFg0NBpuUcNnOHBTtAGn46CjHKQT5xR8";
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
