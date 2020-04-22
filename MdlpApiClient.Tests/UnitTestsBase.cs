﻿namespace MdlpApiClient.Tests
{
    using System.Security.Cryptography.X509Certificates;
    using NUnit.Framework;
    using MdlpApiClient.Toolbox;
    using System.Linq;
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class UnitTestsBase
    {
        // MDLP test stage data
        public const string SystemID = "9dedee17-e43a-47f1-910e-3a88ff6bc81b"; // идентификатор субъекта обращения
        public const string ClientID1 = "01db16f2-9a4e-4d9f-b5e8-c68f12566fd5"; // идентификатор учетной системы
        public const string ClientSecret1 = "9199fe04-42c3-4e81-83b5-120eb5f129f2"; // секретный ключ учетной системы
        public const string UserStarter1 = "starter_resident_1"; // имя тестового пользователя
        public const string UserPassword1 = "password"; // пароль тестового пользователя
        public const string ClientID2 = "c9c307fd-dcb0-4505-8178-13ba2f362339";
        public const string ClientSecret2 = "4d3a2f91-992f-4604-a8a1-71378a1eb75e";
        public const string UserStarter2 = "starter_resident_2";
        public const string UserPassword2 = "password";
        public const string TestDocumentID = "60786bb4-fcb5-4587-b703-d0147e3f9d1c";
        public const string TestDocRequestID = "528700e0-f967-4ddb-995d-5c6c7b73bcc9";
        public const string TestTicketID = "e6afe4b3-4cb3-43af-b94e-83fcc358f4b7";

        // Custom test data, feel free to replace with your own certificate information
        public const string TestCertificateSubjectName = @"Тестовый УКЭП им. Юрия Гагарина";
        public const string TestCertificateThumbprint = "1F9CA1F4DA4BE1A78A260D45376A8F71F5FFBA90";
        public const string TestUserID = TestCertificateThumbprint;

        static UnitTestsBase()
        {
            // for unit tests: use current user's certificates
            GostCryptoHelpers.DefaultStoreLocation = StoreLocation.CurrentUser;
        }

        public UnitTestsBase()
        {
            TestContext.Progress.WriteLine("====> {0} <====", GetType().Name);
        }

        [SetUp]
        public void SetupBeforeEachTest()
        {
            TestContext.Progress.WriteLine("------> {0} <------", TestContext.CurrentContext.Test.MethodName);
        }


        /// <summary>
        /// Asserts that all required data members are not empty.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The instance of the data contract.</param>
        protected void AssertRequiredItems<T>(IEnumerable<T> dataContract)
        {
            Assert.NotNull(dataContract);
            foreach (var item in dataContract)
            {
                AssertRequired(item);
            }
        }

        /// <summary>
        /// Asserts that all required data members are not empty.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The instance of the data contract.</param>
        protected void AssertRequired<T>(T dataContract)
        {
            Assert.NotNull(dataContract);

            // numeric properties can be 0, that's ok
            var skipNumericTypes = new[] { typeof(int), typeof(long), typeof(short), typeof(decimal), typeof(float) };
            var requiredMembers =
                from p in typeof(T).GetProperties()
                let dm = p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                    .OfType<DataMemberAttribute>()
                    .FirstOrDefault()
                where dm != null && dm.IsRequired == true
                where !skipNumericTypes.Contains(p.PropertyType)
                select p;

            var required = requiredMembers.ToArray();
            if (!required.Any())
            {
                return;
            }

            foreach (var p in required)
            {
                var value = p.GetValue(dataContract);
                var defaultValue = p.PropertyType.IsClass ? null : Activator.CreateInstance(p.PropertyType);
                Assert.AreNotEqual(value, defaultValue, "Property " + p.DeclaringType.Name + "." + p.Name + " is not set");
            }
        }
    }
}
