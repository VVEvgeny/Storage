using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace MixTests
{
    public class TestClass
    {
        private string Field = "Field";
        private string GetClassName(string add) => GetType().Name + " " + add;

        private string _property = "Property";
        private string Property
        {
            get => _property;
            set => _property = value;
        }

        private class PrivateClass
        {
            private string PrivateField = "PrivateField";
        }
    }

    public static class Extension
    {
        public static BindingFlags GetAllBindingFlags()
        {
            return BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase
                   | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public
                   | BindingFlags.FlattenHierarchy
                   | BindingFlags.InvokeMethod | BindingFlags.CreateInstance
                   | BindingFlags.GetField | BindingFlags.SetField
                   | BindingFlags.GetProperty | BindingFlags.SetProperty
                   | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty
                   | BindingFlags.ExactBinding | BindingFlags.SuppressChangeType
                   | BindingFlags.OptionalParamBinding;
        }

        public static object GetMethodViaReflection(this object obj, string name, params object[] parameters)
        {
            return GetMethodViaReflection(obj, name, GetAllBindingFlags(), parameters);
        }
        public static object GetMethodViaReflection(this object obj, string name, BindingFlags bindingFlags, params object[] parameters)
        {
            return obj.GetType().
                GetMethods(bindingFlags).
                FirstOrDefault(n => n.Name == name)?.
                Invoke(obj, parameters);
        }

        public static object GetPropertyViaReflection(this object obj, string name, params object[] parameters)
        {
            return GetPropertyViaReflection(obj, name, GetAllBindingFlags(), parameters);
        }
        public static object GetPropertyViaReflection(this object obj, string name, BindingFlags bindingFlags, params object[] parameters)
        {
            var property = obj.GetType().
                GetProperties(bindingFlags).
                FirstOrDefault(n => n.Name == name);

            if (parameters.Length != 0)
            {
                property?.SetValue(obj, parameters.FirstOrDefault());
            }
            return property?.GetValue(obj);
        }

        public static object GetFieldViaReflection(this object obj, string name, params object[] parameters)
        {
            return GetFieldViaReflection(obj, name, GetAllBindingFlags(), parameters);
        }
        public static object GetFieldViaReflection(this object obj, string name, BindingFlags bindingFlags, params object[] parameters)
        {
            /*
            object workObj = null;
            if (obj.GetType() == typeof(Type))
            {
                workObj = Activator.CreateInstance(obj);
            }
            */
            var field = obj.GetType().
                GetFields(bindingFlags).
                FirstOrDefault(n => n.Name == name);

            if (parameters.Length != 0)
            {
                field?.SetValue(obj, parameters.FirstOrDefault());
            }
            return field?.GetValue(obj);
        }

        public static object GetViaReflection(this object obj, string name, params object[] parameters)
        {
            return GetViaReflection( obj,  name, GetAllBindingFlags(), parameters);
        }
        public static object GetViaReflection(this object obj, string name, BindingFlags bindingFlags, params object[] parameters)
        {
            return GetMethodViaReflection(obj, name, bindingFlags, parameters) ??
                   GetPropertyViaReflection(obj, name, bindingFlags, parameters) ??
                   GetFieldViaReflection(obj, name, bindingFlags, parameters);
        }

        public static Type GetClassViaReflection(this object obj, string name)
        {
            return obj.GetType().GetNestedTypes(GetAllBindingFlags()).FirstOrDefault(n => n.Name == name);
        }
        public static Type GetClassViaReflection(this object obj, string name, BindingFlags bindingFlags)
        {
            return obj.GetType().GetNestedTypes(bindingFlags).FirstOrDefault(n => n.Name == name);
        }
    }

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestPrivateMethod()
        {
            const string addText = nameof(addText);
            Assert.AreEqual(nameof(TestClass) + " " + addText, new TestClass().GetViaReflection("GetClassName", addText));
        }
        [TestMethod]
        public void TestPrivatePropertyGet()
        {
            Assert.AreEqual("Property", new TestClass().GetViaReflection("Property"));
        }
        [TestMethod]
        public void TestPrivatePropertySet()
        {
            const string newValue = nameof(newValue);
            Assert.AreEqual(newValue, new TestClass().GetViaReflection("Property", newValue));
        }
        [TestMethod]
        public void TestPrivateFieldGet()
        {
            Assert.AreEqual("Field", new TestClass().GetViaReflection("Field"));
        }
        [TestMethod]
        public void TestPrivateFieldSet()
        {
            const string newValue = nameof(newValue);
            Assert.AreEqual(newValue, new TestClass().GetViaReflection("Field", newValue));
        }
        [TestMethod]
        public void TestPrivateFieldInPrivateClassGet()
        {
            var type = new TestClass().GetClassViaReflection("PrivateClass");
            Assert.IsNotNull(type);
            Assert.AreEqual("PrivateField", Activator.CreateInstance(type).GetFieldViaReflection("PrivateField"));
        }
        [TestMethod]
        public void TestPrivateFieldInPrivateClassSet()
        {
            var type = new TestClass().GetClassViaReflection("PrivateClass");
            Assert.IsNotNull(type);
            const string privateFieldNewValue = nameof(privateFieldNewValue);
            Assert.AreEqual(privateFieldNewValue, Activator.CreateInstance(type).GetFieldViaReflection("PrivateField", privateFieldNewValue));
        }
    }
}
