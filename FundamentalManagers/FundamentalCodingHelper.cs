using System;
using System.Collections.Generic;
using HarmonyLib;
using System.Linq;
using UnityEngine;
using System.IO;

namespace bbpfer.FundamentalManagers
{
	public static class FundamentalCodingHelper
	{
		public static T FindLastObjectOfType<T>(bool includeDisabled = false) where T : UnityEngine.Object
		{
			var collection = UnityEngine.Object.FindObjectsOfType<T>(includeDisabled);
			return collection[collection.Length - 1];
		}

		public static T FindObjectContainingName<T>(string name, bool includeDisabled = false) where T : UnityEngine.Object
		{
			string sName = name.ToLower();
			return UnityEngine.Object.FindObjectsOfType<T>(includeDisabled).First(x => x.name.ToLower().Contains(sName));
		}

		public static T[] FindObjectsContainingName<T>(string name, bool includeDisabled = false) where T : UnityEngine.Object
		{
			string sName = name.ToLower();
			return UnityEngine.Object.FindObjectsOfType<T>(includeDisabled).Where(x => x.name.ToLower().Contains(sName)).ToArray();
		}

		public static T FindResourceObjectContainingName<T>(string name) where T : UnityEngine.Object
		{
			string sName = name.ToLower();
			return Resources.FindObjectsOfTypeAll<T>().First(x => x.name.ToLower().Contains(sName));
		}

		public static T FindResourceObjectWithName<T>(string name) where T : UnityEngine.Object
		{
			string sName = name.ToLower();
			return Resources.FindObjectsOfTypeAll<T>().First(x => x.name.ToLower() == sName);
		}

		public static T[] FindResourceObjectsContainingName<T>(string name) where T : UnityEngine.Object
		{
			string sName = name.ToLower();
			return Resources.FindObjectsOfTypeAll<T>().Where(x => x.name.ToLower().Contains(sName)).ToArray();
		}


		public static T FindResourceObject<T>() where T : UnityEngine.Object
		{
			return Resources.FindObjectsOfTypeAll<T>()[0];
		}

		public static T[] FindResourceObjects<T>() where T : UnityEngine.Object
		{
			return Resources.FindObjectsOfTypeAll<T>();
		}
		public static void UseMethod(object instance, string methodName, params object[] parameters)
		{
			Traverse.Create(instance).Method(methodName, parameters).GetValue();
		}
		public static T GetVariable<T>(object instance, string fieldName)
		{
			var field = Traverse.Create(instance).Field(fieldName);
			T value;

			try
			{
				value = field.GetValue<T>();
			}
			catch (InvalidCastException ex)
			{
				Debug.LogError($"[Error: Unity Log] InvalidCastException: Specified cast is not valid for field '{fieldName}' to type '{typeof(T).Name}'.\n{ex}");
				throw; // rethrow the exception after logging it
			}

			if (value == null)
			{
				Debug.Log($"{fieldName} is not of type {typeof(T).Name}");
			}

			return value;
		}



		public static void SetValue<T>(object instance, string fieldName, object setVal)
		{
			Traverse.Create(instance).Field(fieldName).SetValue(setVal);
		}

		public static GameObject FindChildByName(GameObject parent, string name)
		{
			foreach (Transform child in parent.transform)
			{
				if (child.gameObject.name == name)
				{
					return child.gameObject;
				}

				GameObject result = FindChildByName(child.gameObject, name);
				if (result != null)
				{
					return result;
				}
			}
			return null;
		}

		public static Dictionary<string, string> LoadLocalizedPartText(string fileName)
		{
			Dictionary<string, string> localizedText = new Dictionary<string, string>();
			string path = Path.Combine(Application.streamingAssetsPath, fileName);
			if (File.Exists(path))
			{
				LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(File.ReadAllText(path));
				for (int i = 0; i < localizationData.items.Length; i++)
					localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);
			}
			return localizedText;
		}
	}
}
